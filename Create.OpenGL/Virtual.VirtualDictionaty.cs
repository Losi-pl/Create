using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Collections;

namespace Create.Virtuals;

/// <summary>
/// Wirtualna bibliotega złorzona z kilku funkcji zastempujące podstawowe funkcje
/// </summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(VirtualDictionaty<,>.Proxy))]
public struct VirtualDictionaty<TKey, TValue> : IDictionary<TKey, TValue>
{
    Func<TKey, TValue> get;
    Func<int> lengh;
    Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerator;
    Func<TKey, bool> contain_key;

    /// <summary>
    /// <inheritdoc cref="Constructor.GetMethod(Func{TKey, TValue})"/>
    /// </summary>
    IEnumerator<KeyValuePair<TKey, TValue>> enumerator_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();

    /// <summary>
    /// Przetwarza biblioteke w <see cref="ICollection{T}"/> wartości
    /// </summary>
    ICollection<TValue> value_list_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).ConvertAll(t => t.Value).ToArray();

    /// <summary>
    /// Przetwarza biblioteke w <see cref="ICollection{T}"/> kluczy
    /// </summary>
    ICollection<TKey> key_list_() => (enumerator != null ? enumerator() : Enumerable.Empty<KeyValuePair<TKey, TValue>>()).ConvertAll(t => t.Key).ToArray();

    /// <summary>
    /// <inheritdoc cref="Constructor.IsConteinedMethod(Func{TKey, bool})"/>
    /// </summary>
    bool contain_key_(TKey key) => contain_key != null ? contain_key(key) : false;

    /// <summary>
    /// <inheritdoc cref="Constructor.GetMethod(Func{TKey, TValue})"/>
    /// </summary>
    TValue get_(TKey key) => get != null ? get(key) : default!;

    /// <summary>
    /// <inheritdoc cref="Constructor.CountMethod(Func{int})"/>
    /// </summary>
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

    /// <summary>
    /// Konstruktor do <see cref="VirtualDictionaty{TKey, TValue}"/>
    /// </summary>
    public struct Constructor
    {
        Func<TKey, TValue> get;
        Func<int> lengh;
        Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerator;
        Func<TKey, bool> contain_key;

        /// <summary>
        /// Gdzy wartość jest pobierana z biblioteki
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Constructor GetMethod(Func<TKey, TValue> get)
        {
            this.get = get;
            return this;
        }

        /// <summary>
        /// Gdy długość biblioteki jest pobierana
        /// </summary>
        public Constructor CountMethod(Func<int> count)
        {
            lengh = count;
            return this;
        }

        /// <summary>
        /// Gdy pobiera kolekcje obiektów z biblioteki
        /// </summary>
        public Constructor EnumerableMethod(Func<IEnumerable<KeyValuePair<TKey, TValue>>> enumerable)
        {
            enumerator = enumerable;
            return this;
        }

        /// <summary>
        /// Czy klucz jest zawarty w bibliotece
        /// </summary>
        public Constructor IsConteinedMethod(Func<TKey, bool> contain)
        {
            contain_key = contain;
            return this;
        }

        /// <summary>
        /// Zakończenie konstrukcji
        /// </summary>
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
    
    /// <summary>
    /// Pusta kolekcja
    /// </summary>
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

    /// <summary>
    /// Debuger do <see cref="VirtualDictionaty{TKey, TValue}"/>
    /// </summary>
    internal class Proxy
    {
#pragma warning disable CS8714
        Dictionary<TKey, TValue> dir;
        Exception? ex;
#pragma warning restore CS8714

        public Proxy(VirtualDictionaty<TKey, TValue> o)
        {
            try
            {
#pragma warning disable CS8714
                dir = o.enumerator_().AsEnumerable().ToDictionary(v => v.Key, kvp => kvp.Value);
#pragma warning restore CS8714
            }
            catch (Exception ex)
            {
                this.ex = ex;
                dir = null!;
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object? array => dir != null ? new ReadOnlyDictionaryView<TKey, TValue>(dir).GetViewList() : new Except() { Exception = ex! };

        struct Except
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            Exception ex;
            public Exception Exception
            {
                init => ex = value;
                get => throw ex;
            }
        }
    }
}