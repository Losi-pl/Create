using Create.Linq;
using System.Collections;
using System.Diagnostics;

namespace Create.Virtuals;

/// <summary>
/// Wirtualna lista złorzona z kilku funkcji zastempujące podstawowe funkcje
/// </summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(VirtualList<>.Proxy))]
public struct VirtualList<TValue> : IList<TValue>
{
    Func<int, TValue> get;
    Func<int> count;
    Func<TValue, bool> contain;
    Func<IEnumerable<TValue>> enumerable;

    /// <summary>
    /// Konstrukto do <see cref="VirtualList{TValue}"/>
    /// </summary>
    public struct Creator
    {
        Func<int, TValue>? get;
        Func<int>? count;
        Func<TValue, bool>? contain;
        Func<IEnumerable<TValue>>? enumerable;

        /// <summary>
        /// Gdzy wartość jest pobierana z listy
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Creator GetMethod(Func<int, TValue> func)
        {
            get = func;
            return this;
        }
        
        /// <summary>
        /// Gdy długość listy jest pobierana
        /// </summary>
        public Creator CountMethod(Func<int> func)
        {
            count = func;
            return this;
        }
        
        /// <summary>
        /// Czy obiekt jest zawarty w liście
        /// </summary>
        public Creator IsContainMethod(Func<TValue, bool> func)
        {
            contain = func;
            return this;
        }
        
        /// <summary>
        /// Gdy pobiera kolekcje obiektów z listy
        /// </summary>
        public Creator EnumerableMethod(Func<IEnumerable<TValue>> func)
        {
            enumerable = func;
            return this;
        }

        /// <summary>
        /// Zakończenie konstrukcji
        /// </summary>
        public VirtualList<TValue> Finish() => new() { get = get!, count = count!, contain = contain!, enumerable = enumerable! };
    }

    /// <summary>
    /// <inheritdoc cref="Creator.GetMethod(Func{int, TValue})"/>
    /// </summary>
    TValue get_(int i) => get != null ? get(i) : default!;

    /// <summary>
    /// <inheritdoc cref="Creator.CountMethod(Func{int})"/>
    /// </summary>
    int count_() => count != null ? count() : 0;

    /// <summary>
    /// <inheritdoc cref="Creator.IsContainMethod(Func{TValue, bool})"/>
    /// </summary>
    bool contain_(TValue i) => contain != null ? contain(i) : false;

    /// <summary>
    /// <inheritdoc cref="Creator.EnumerableMethod(Func{IEnumerable{TValue}})"/>
    /// </summary>
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

    /// <summary>
    /// Debugowy podgląd dla <see cref="VirtualList{TValue}"/>
    /// </summary>
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