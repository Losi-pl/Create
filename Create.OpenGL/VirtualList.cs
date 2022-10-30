using Create.OpenGL;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Create.Virtuals;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(VirtualList<>.Proxy))]
public struct VirtualList<TValue> : IList<TValue>
{
    Func<int, TValue> get;
    Func<int> count;
    Func<TValue, bool> contain;
    Func<IEnumerable<TValue>> enumerable;

    public struct Creator
    {
        Func<int, TValue>? get;
        Func<int>? count;
        Func<TValue, bool>? contain;
        Func<IEnumerable<TValue>>? enumerable;

        public Creator GetMethod(Func<int, TValue> func)
        {
            get = func;
            return this;
        }
        public Creator CountMethod(Func<int> func)
        {
            count = func;
            return this;
        }
        public Creator IsContainMethod(Func<TValue, bool> func)
        {
            contain = func;
            return this;
        }
        public Creator EnumerableMethod(Func<IEnumerable<TValue>> func)
        {
            enumerable = func;
            return this;
        }

        public VirtualList<TValue> Finish() => new() { get = get!, count = count!, contain = contain!, enumerable = enumerable! };
    }

    TValue get_(int i) => get != null ? get(i) : default!;
    int count_() => count != null ? count() : 0;
    bool contain_(TValue i) => contain != null ? contain(i) : false;
    IEnumerator<TValue> enumerator_() => (enumerable != null ? enumerable() : Enumerable.Empty<TValue>()).GetEnumerator();

    TValue IList<TValue>.this[int index] { get => get_(index); set => throw new NotImplementedException(); }
    public TValue this[int index] => get_(index);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int Count => count_();
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection<TValue>.IsReadOnly => true;

    void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex) => throw new NotImplementedException();
    void ICollection<TValue>.Add(TValue item) => throw new NotImplementedException();
    void ICollection<TValue>.Clear() => throw new NotImplementedException();
    public bool Contains(TValue item) => contain_(item);
    public IEnumerator<TValue> GetEnumerator() => enumerator_();

    public int IndexOf(TValue item) => throw new NotImplementedException();
    public void Insert(int index, TValue item) => throw new NotImplementedException();
    public bool Remove(TValue item) => throw new NotImplementedException();
    public void RemoveAt(int index) => throw new NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator() => enumerator_();

    internal class Proxy
    {
        TValue[] list;
        Exception? ex;

        public Proxy(VirtualList<TValue> o)
        {
            try
            {
                list = o.enumerator_().AsEnumerable().ToArray();
            }
            catch (Exception e)
            {
                ex = e;
                list = null!;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public TValue[] array => list;
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Exception[] exception => ex != null ? new[] { ex } : Array.Empty<Exception>();
    }
}

public static class VirtualList
{
    
    public static VirtualList<T>.Creator Create<T>() => new();
    public static VirtualList<T>.Creator Create<T>(T[] array) =>
        Create<T>()
        .GetMethod(i => array[i])
        .CountMethod(() => array.Length)
        .IsContainMethod(o => ((IList<T>)array).Contains(o))
        .EnumerableMethod(() => array);
    public static VirtualList<T>.Creator Create<T>(IList<T> list) =>
        Create<T>()
        .GetMethod(i => list[i])
        .CountMethod(() => list.Count)
        .IsContainMethod(list.Contains)
        .EnumerableMethod(() => list);
}
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(VirtualDictionaty<,>.Proxy))]
public struct VirtualDictionaty<TKey, TValue> : IDictionary<TKey, TValue>
{
    Func<TKey, TValue> get;
    Func<int> lengh;
    Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerator;
    Func<TKey, bool> contain_key;

    IEnumerator<KeyValuePair<TKey, TValue>> enumerator_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();
    ICollection<TValue> value_list_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).ConvertAll(t => t.Value).ToArray();
    ICollection<TKey> key_list_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).ConvertAll(t => t.Key).ToArray();
    bool contain_key_(TKey key) => contain_key != null ? contain_key(key) : false;
    TValue get_(TKey key) => get != null ? get(key) : default!;
    int lenght_() => lengh != null ? lengh() : 0;

    public TValue this[TKey key] => get_(key);

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new NotImplementedException();
    TValue IDictionary<TKey, TValue>.this[TKey key] { get => get_(key); set => throw new NotImplementedException(); }
    void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new NotImplementedException();
    bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new NotImplementedException();
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => enumerator_();
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public bool ContainsKey(TKey key) => contain_key_(key);
    public ICollection<TValue> Values => value_list_();
    public ICollection<TKey> Keys => key_list_();
    public int Count => lenght_();
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (var value in this)
            array[arrayIndex++] = value;
    }
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        if (!ContainsKey(item.Key))
            return false;
        return get_(item.Key)!.Equals(item.Value);
    }
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        try
        {
            value = get_(key);
            return true;
        }
        catch
        {
            value = default!;
            return false;
        }
    }

    public struct Constructor
    {
        Func<TKey, TValue> get;
        Func<int> lengh;
        Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerator;
        Func<TKey, bool> contain_key;

        public Constructor GetMethod(Func<TKey, TValue> get)
        {
            this.get = get;
            return this;
        }
        public Constructor CountMethod(Func<int> count)
        {
            lengh = count;
            return this;
        }
        public Constructor EnumerableMethod(Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerable)
        {
            enumerator = enumerable;
            return this;
        }
        public Constructor IsConteinedMethod(Func<TKey, bool> contain)
        {
            contain_key = contain;
            return this;
        }

        public VirtualDictionaty<TKey, TValue> Finsh()
        {
            VirtualDictionaty<TKey, TValue> dictionary = new();
            dictionary.get = get;
            dictionary.lengh = lengh;
            dictionary.enumerator = enumerator;
            dictionary.contain_key = contain_key;
            return dictionary;
        }
    }
    struct empty_collection<T> : ICollection<T>
    {
        public int Count => 0;

        public bool IsReadOnly => true;

        public void Add(T item) => throw new NotImplementedException();

        public void Clear() { }

        public bool Contains(T item) => false;

        public void CopyTo(T[] array, int arrayIndex) { }

        public IEnumerator<T> GetEnumerator() => Enumerable.Empty<T>().GetEnumerator();

        public bool Remove(T item) => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Proxy
    {
        KeyValuePair<TKey, TValue>[] list;
        Exception? ex;

        public Proxy(VirtualDictionaty<TKey, TValue> o)
        {
            try
            {
                list = o.enumerator_().AsEnumerable().ToArray();
            }
            catch (Exception ex)
            {
                this.ex = ex;
                list = null!;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] array => list;
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Exception[] exception => ex != null ? new[] { ex } : Array.Empty<Exception>();
    }
}
public static class VirtualDictionaty
{
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>() => new();
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>(IDictionary<TKey, TValue> dictionary) => Create<TKey, TValue>()
        .GetMethod(d => dictionary[d])
        .CountMethod(() => dictionary.Count)
        .EnumerableMethod(() => dictionary)
        .IsConteinedMethod(dictionary.ContainsKey);
}