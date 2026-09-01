using Create.Registry;
using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBePrivate.Global, ConvertToAutoProperty

namespace Create.Assets;

public class FrozenElementDictionary<T> : IDictionary<ElementIdent, T>
{
    #region =============================================== Main ===============================================
    private readonly FrozenDictionary<IMod, FrozenDictionary<string, T>> _elements;
    private readonly int _count;

    internal FrozenElementDictionary(FrozenDictionary<IMod, FrozenDictionary<string, T>> elements)
    {
        _elements = elements;
        _count = elements.Count;
    }

    public T this[ElementIdent key] { get {
            if(!_elements.TryGetValue(key.Mod, out var folder))
                throw new KeyNotFoundException();
            return folder[key.Element];
        }
    }
    
    public T this[RefElementIdent key] { get {
            if(!_elements.TryGetValue(key.Mod, out var folder))
                throw new KeyNotFoundException();
            return folder.GetAlternateLookup()[key.Element];
        }
    }
    
    public int Count => _count;

    bool ICollection<KeyValuePair<ElementIdent, T>>.IsReadOnly => true;
    T IDictionary<ElementIdent, T>.this[ElementIdent key] { get => this[key]; set => throw new InvalidOperationException(); }
    void IDictionary<ElementIdent, T>.Add(ElementIdent key, T value) => throw new InvalidOperationException();
    void ICollection<KeyValuePair<ElementIdent, T>>.Add(KeyValuePair<ElementIdent, T> item) => throw new InvalidOperationException();
    void ICollection<KeyValuePair<ElementIdent, T>>.Clear() => throw new InvalidOperationException();
    bool IDictionary<ElementIdent, T>.Remove(ElementIdent key) => throw new InvalidOperationException();
    bool ICollection<KeyValuePair<ElementIdent, T>>.Remove(KeyValuePair<ElementIdent, T> item) => throw new InvalidOperationException();

    public bool Contains(KeyValuePair<ElementIdent, T> item)
    {
        if (!_elements.TryGetValue(item.Key.Mod, out var folder))
            return false;
        if(folder.TryGetValue(item.Key.Element, out var value))
            return EqualityComparer<T>.Default.Equals(item.Value, value);
        return false;
    }
    /// <inheritdoc cref="Contains(KeyValuePair{ElementIdent, T})"/>
    public bool Contains(RefElementIdent identity, T element)
    {
        if (!_elements.TryGetValue(identity.Mod, out var folder))
            return false;
        if(folder.GetAlternateLookup().TryGetValue(identity.Element, out var value))
            return EqualityComparer<T>.Default.Equals(element, value);
        return false;
    }

    public bool ContainsKey(ElementIdent key)
    {
        if (!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        return folder.ContainsKey(key.Element);
    }
    /// <inheritdoc cref="ContainsKey(ElementIdent)"/>
    public bool ContainsKey(RefElementIdent key)
    {
        if (!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        return folder.GetAlternateLookup().ContainsKey(key.Element);
    }
    
    public void CopyTo(KeyValuePair<ElementIdent, T>[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        Guard.NotNegative(arrayIndex, nameof(arrayIndex));

        var i = 0;
        foreach(var item in this)
        {
            if (i + arrayIndex >= array.Length) break;
            array[i++ + arrayIndex] = item;
        }
    }

    public bool TryGetValue(ElementIdent key, [MaybeNullWhen(false)] out T value)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.TryGetValue(key.Element, out value);
    }
    /// <inheritdoc cref="TryGetValue(Create.Registry.ElementIdent,out T)"/>
    public bool TryGetValue(RefElementIdent key, [MaybeNullWhen(false)] out T value)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.GetAlternateLookup().TryGetValue(key.Element, out value);
    }

    public struct Enumerator : IEnumerator<KeyValuePair<ElementIdent, T>>
    {
        private FrozenDictionary<IMod, FrozenDictionary<string, T>>.Enumerator _folder;
        private FrozenDictionary<string, T>.Enumerator _elements;
        private KeyValuePair<ElementIdent, T> _current;
        private bool _hasNext, _isSet;

        public Enumerator(FrozenElementDictionary<T> source)
        {
            _folder = source._elements.GetEnumerator();
            while (_folder.MoveNext())
            {
                _elements = _folder.Current.Value.GetEnumerator();
                _hasNext = true;
                return;
            }

            _elements = default;
            _hasNext = false;
        }

        public bool MoveNext()
        {
            if (!_hasNext)
                return false;
            _isSet = true;

            while (true)
            {
                if (_elements.MoveNext())
                {
                    _current = new((_folder.Current.Key, _elements.Current.Key), _elements.Current.Value);
                    return true;
                }

                if (_folder.MoveNext())
                {
                    _elements = _folder.Current.Value.GetEnumerator();
                }
                else
                {
                    _hasNext = false;
                    return false;
                }
            }
        }

        void IEnumerator.Reset() { }

        public KeyValuePair<ElementIdent, T> Current
        {
            get
            {
                if (!_hasNext || !_isSet)
                    throw new InvalidOperationException("There is no available value");
                return _current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            if (!_hasNext)
                return;
            _hasNext = false;
        }
    }

    public Enumerator GetEnumerator() => new(this);
    IEnumerator<KeyValuePair<ElementIdent, T>> IEnumerable<KeyValuePair<ElementIdent, T>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    #region =============================================== Keys ===============================================

    public class ElementKeys(FrozenElementDictionary<T> source) : ICollection<ElementIdent>
    {
        public int Count => source.Count;

        bool ICollection<ElementIdent>.IsReadOnly => true;
        void ICollection<ElementIdent>.Clear() => throw new InvalidOperationException();
        void ICollection<ElementIdent>.Add(ElementIdent item) => throw new InvalidOperationException();
        bool ICollection<ElementIdent>.Remove(ElementIdent item) => throw new InvalidOperationException();

        public bool Contains(ElementIdent item) => source.TryGetValue(item, out _);

        public void CopyTo(ElementIdent[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            Guard.NotNegative(arrayIndex, nameof(arrayIndex));

            var i = 0;
            foreach (var item in this)
            {
                if (i + arrayIndex >= array.Length) break;
                array[i++ + arrayIndex] = item;
            }
        }

        public struct Enumerator : IEnumerator<ElementIdent>
        {
            FrozenElementDictionary<T>.Enumerator _enumerator;

            public Enumerator(FrozenElementDictionary<T> source) => _enumerator = source.GetEnumerator();

            public ElementIdent Current => _enumerator.Current.Key;
            object IEnumerator.Current => _enumerator.Current.Key;

            public void Dispose() => _enumerator.Dispose();
            public bool MoveNext() => _enumerator.MoveNext();

            void IEnumerator.Reset() => throw new InvalidOperationException();
        }

        public Enumerator GetEnumerator() => new(source);
        IEnumerator<ElementIdent> IEnumerable<ElementIdent>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public ElementKeys Keys { get => field ??= new(this); } = null;
    ICollection<ElementIdent> IDictionary<ElementIdent, T>.Keys => Keys;
    #endregion

    #region ============================================== Values ==============================================
    public class ElementValues(FrozenElementDictionary<T> source) : ICollection<T>
    {
        public int Count => source.Count;

        bool ICollection<T>.IsReadOnly => true;
        void ICollection<T>.Clear() => throw new InvalidOperationException();
        void ICollection<T>.Add(T item) => throw new InvalidOperationException();
        bool ICollection<T>.Remove(T item) => throw new InvalidOperationException();

        public bool Contains(T item)
        {
            foreach (var folder in source._elements)
                if (folder.Value.Values.Contains(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            Guard.NotNegative(arrayIndex, nameof(arrayIndex));

            var i = 0;
            foreach (var item in this)
            {
                if (i + arrayIndex >= array.Length) break;
                array[i++ + arrayIndex] = item;
            }
        }

        public struct Enumerator : IEnumerator<T>
        {
            FrozenElementDictionary<T>.Enumerator _enumerator;

            public Enumerator(FrozenElementDictionary<T> source) => _enumerator = source.GetEnumerator();

            public T Current => _enumerator.Current.Value;
            object IEnumerator.Current => _enumerator.Current.Value!;

            public void Dispose() => _enumerator.Dispose();
            public bool MoveNext() => _enumerator.MoveNext();

            void IEnumerator.Reset() => throw new InvalidOperationException();
        }

        public Enumerator GetEnumerator() => new(source);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public ElementValues Values { get => field ??= new(this); } = null;
    ICollection<T> IDictionary<ElementIdent, T>.Values => Values;
    #endregion
}
