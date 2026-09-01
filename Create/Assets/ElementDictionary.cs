using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Create.Registry;

namespace Create.Assets;

public class ElementDictionary<T>: IDictionary<ElementIdent, T>
{
    #region =============================================== Main ===============================================

    // ReSharper disable once InconsistentNaming
    private Dictionary<IMod, Dictionary<string, T>> _elements { get; } = [];

    public struct Enumerator: IEnumerator<KeyValuePair<ElementIdent, T>>
    {
        private Dictionary<IMod, Dictionary<string, T>>.Enumerator _folder;
        private Dictionary<string, T>.Enumerator _elements;
        private KeyValuePair<ElementIdent, T> _current;
        private bool _hasNext, _isSet;

        public Enumerator(ElementDictionary<T> source)
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
            _folder.Dispose();
        }
        
        public bool MoveNext()
        {
            if(!_hasNext)
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
                    _elements.Dispose();
                    _elements = _folder.Current.Value.GetEnumerator();
                }
                else
                {
                    _hasNext = false;
                    _folder.Dispose();
                    _elements.Dispose();
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
            if(!_hasNext)
                return;
            _hasNext = false;
            _folder.Dispose();
            _elements.Dispose();
        }
    }
    public Enumerator GetEnumerator() => new(this);
    IEnumerator<KeyValuePair<ElementIdent, T>> IEnumerable<KeyValuePair<ElementIdent, T>>.
    GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    void ICollection<KeyValuePair<ElementIdent, T>>.Add(KeyValuePair<ElementIdent, T> item) => Add(item.Key, item.Value);
    
    public void Clear() => _elements.Clear();

    public bool Contains(KeyValuePair<ElementIdent, T> item)
    {
        if(!_elements.TryGetValue(item.Key.Mod, out var folder))
            return false;
        if(!folder.TryGetValue(item.Key.Element, out var value))
            return false;
        return EqualityComparer<T>.Default.Equals(value, item.Value);
    }
    public void CopyTo(KeyValuePair<ElementIdent, T>[] array, int arrayIndex)
    {
        var index = 0;

        foreach (var (mod, folder) in _elements)
            foreach (var element in folder)
            {
                if(index + arrayIndex >= array.Length)
                    return;
                array[arrayIndex + index++] = new((mod, element.Key), element.Value);
            }
    }

    bool ICollection<KeyValuePair<ElementIdent, T>>.Remove(KeyValuePair<ElementIdent, T> item)
    {
        if(!_elements.TryGetValue(item.Key.Mod, out var folder))
            return false;
        if (!folder.Remove(item.Key.Element)) return false;
        
        if (folder.Count == 0)
            _elements.Remove(item.Key.Mod);
        return true;
    }

    public int Count
    {
        get
        {
            var sum = 0;
            foreach (var (_, subElements) in _elements)
                sum += subElements.Count;
            return sum;
        }
    }

    bool ICollection<KeyValuePair<ElementIdent, T>>.IsReadOnly => false;
    public void Add(ElementIdent key, T value)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            folder = _elements[key.Mod] = [];
        folder.Add(key.Element, value);
    }
    ///<inheritdoc cref="Add(ElementIdent, T)" />
    public void Add(RefElementIdent key, T value)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            folder = _elements[key.Mod] = [];
        folder.Add(key.Element.ToString(), value);
    }

    public bool ContainsKey(ElementIdent key)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        return folder.ContainsKey(key.Element);
    }
    ///<inheritdoc cref="ContainsKey(ElementIdent)" />
    public bool ContainsKey(RefElementIdent key)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        return folder.GetAlternateLookup().ContainsKey(key.Element);
    }

    public bool Remove(ElementIdent key)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        if (folder.Remove(key.Element))
        {
            if (folder.Count == 0)
                _elements.Remove(key.Mod);
            return true;
        }
        return false;
    }
    ///<inheritdoc cref="Remove(ElementIdent)" />
    public bool Remove(RefElementIdent key)
    {
        if(!_elements.TryGetValue(key.Mod, out var folder))
            return false;
        if (folder.GetAlternateLookup().Remove(key.Element))
        {
            if (folder.Count == 0)
                _elements.Remove(key.Mod);
            return true;
        }
        return false;
    }

    public bool TryGetValue(ElementIdent key, [MaybeNullWhen(false)] out T value)
    {
        if (!_elements.TryGetValue(key.Mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.TryGetValue(key.Element, out value);
    }
    ///<inheritdoc cref="TryGetValue(ElementIdent, out T)" />
    public bool TryGetValue(RefElementIdent key, [MaybeNullWhen(false)] out T value)
    {
        if (!_elements.TryGetValue(key.Mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.GetAlternateLookup().TryGetValue(key.Element, out value);
    }

    public FrozenElementDictionary<T> ToFrozenDictionary() => new(_elements.ToFrozenDictionary(kvp => kvp.Key, kvp => kvp.Value.ToFrozenDictionary()));

    public FrozenElementDictionary<TRez> ToFrozenDictionary<TRez>(Func<KeyValuePair<ElementIdent, T>, TRez> keyValueSelector)
    {
        var result = 
            this.Select(kvp => new KeyValuePair<ElementIdent, TRez>(kvp.Key, keyValueSelector(kvp))) // Alter the values to wanted results
            .GroupBy(kvp => kvp.Key.Mod).ToFrozenDictionary(group => group.Key, // Brake down into per mod gropings
                group => group.Select(kvp => new KeyValuePair<string, TRez>(kvp.Key.Element, kvp.Value)).ToFrozenDictionary());// Freeze per element gropings
        return new(result);
    }
    
    public T this[ElementIdent key]
    {
        get
        {
            if (!_elements.TryGetValue(key.Mod, out var folder))
                throw new KeyNotFoundException();
            return folder[key.Element];
        }
        set
        {
            if (!_elements.TryGetValue(key.Mod, out var folder))
                folder = _elements[key.Mod] = [];
            folder[key.Element] = value;
        }
    }
    
    public T this[RefElementIdent key]
    {
        get
        {
            if (!_elements.TryGetValue(key.Mod, out var folder))
                throw new KeyNotFoundException();
            return folder.GetAlternateLookup()[key.Element];
        }
        set
        {
            if (!_elements.TryGetValue(key.Mod, out var folder))
                folder = _elements[key.Mod] = [];
            var alt = folder.GetAlternateLookup();
            alt[key.Element] = value;
        }
    }
    #endregion

    #region =============================================== Keys ===============================================

    public class ElementKeys: ICollection<ElementIdent>
    {
        private readonly ElementDictionary<T> _source;

        internal ElementKeys(ElementDictionary<T> source) => _source = source;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public struct Enumerator(ElementDictionary<T> source) : IEnumerator<ElementIdent>
        {
            private ElementDictionary<T>.Enumerator _source = source.GetEnumerator();
            public bool MoveNext() => _source.MoveNext();
            public void Reset() => throw new NotSupportedException();
            public ElementIdent Current => _source.Current.Key;
            object IEnumerator.Current => _source.Current.Key;
            public void Dispose() => _source.Dispose();
        }

        public Enumerator GetEnumerator() => new(_source);
        IEnumerator<ElementIdent> IEnumerable<ElementIdent>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        public bool Contains(ElementIdent item)
        {
            foreach (var key in this)
                if (key == item)
                    return true;
            return false;
        }
    
        public void CopyTo(ElementIdent[] array, int arrayIndex)
        {
            var i = 0;
            foreach (var key in this)
            {
                if(arrayIndex +  i >= array.Length)
                    return;
                        
                array[i++ + arrayIndex] = key;
            }
        }
    
        public int Count => _source.Count;
        bool ICollection<ElementIdent>.IsReadOnly => true;
        
        void ICollection<ElementIdent>.Add(ElementIdent item) => throw new NotSupportedException();
        void ICollection<ElementIdent>.Clear() => throw new NotSupportedException();
        bool ICollection<ElementIdent>.Remove(ElementIdent item) => throw new NotSupportedException();
    }
    
    public ElementKeys Keys { get => field ??= new(this); } = null;
    ICollection<ElementIdent> IDictionary<ElementIdent, T>.Keys => Keys;
    
    #endregion 
    
    #region ============================================== Values ==============================================
    
    public class ElementValues: ICollection<T>
    {
        private readonly ElementDictionary<T> _source;
        internal ElementValues(ElementDictionary<T> source) => _source = source;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public struct Enumerator(ElementDictionary<T> source): IEnumerator<T>
        {
            private ElementDictionary<T>.Enumerator _source = source.GetEnumerator();

            public bool MoveNext() =>  _source.MoveNext();
            void IEnumerator.Reset() { }
            public T Current => _source.Current.Value;
            object? IEnumerator.Current => _source.Current.Value;
            public void Dispose() => _source.Dispose();
        }

        public Enumerator GetEnumerator() => new(_source);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Contains(T item)
        {
            foreach (var value in this)
                if (EqualityComparer<T>.Default.Equals(value, item))
                    return true;
                
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var i = 0;
            foreach (var value in this)
            {
                if(arrayIndex +  i >= array.Length)
                    return;
                        
                array[i++ + arrayIndex] = value;
            }
        }

        public int Count => _source.Count;
        bool ICollection<T>.IsReadOnly => true;
        
        void ICollection<T>.Add(T item) => throw new NotSupportedException();
        void ICollection<T>.Clear() => throw new NotSupportedException();
        bool ICollection<T>.Remove(T item) => throw new NotSupportedException();
    }

    public ElementValues Values { get => field ??= new(this); } = null;
    ICollection<T> IDictionary<ElementIdent, T>.Values => Values;
    
    #endregion
}