using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Create.Registry;

namespace Create.Assets;

public class ElementDictionary<T>: IDictionary<(IMod mod, string identity), T>
{
    #region =============================================== Main ===============================================

    public ElementDictionary()
    {
        _elements = [];
    }
    
    // ReSharper disable once InconsistentNaming
    private Dictionary<IMod, Dictionary<string, T>> _elements { get; }

    public struct Enumerator: IEnumerator<KeyValuePair<(IMod mod, string identity), T>>
    {
        private Dictionary<IMod, Dictionary<string, T>>.Enumerator _folder;
        private Dictionary<string, T>.Enumerator _elements;
        private KeyValuePair<(IMod mod, string identity), T> _current;
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

        public KeyValuePair<(IMod mod, string identity), T> Current
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
    IEnumerator<KeyValuePair<(IMod mod, string identity), T>> IEnumerable<KeyValuePair<(IMod mod, string identity), T>>.
        GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(KeyValuePair<(IMod mod, string identity), T> item)
    {
        if(!_elements.TryGetValue(item.Key.mod, out var folder))
            folder = _elements[item.Key.mod] = [];
        folder.Add(item.Key.identity, item.Value);
    }

    public void Clear() => _elements.Clear();

    public bool Contains(KeyValuePair<(IMod mod, string identity), T> item)
    {
        if(!_elements.TryGetValue(item.Key.mod, out var folder))
            return false;
        if(!folder.TryGetValue(item.Key.identity, out var value))
            return false;
        return EqualityComparer<T>.Default.Equals(value, item.Value);
    }

    public void CopyTo(KeyValuePair<(IMod mod, string identity), T>[] array, int arrayIndex)
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

    public bool Remove(KeyValuePair<(IMod mod, string identity), T> item)
    {
        if(!_elements.TryGetValue(item.Key.mod, out var folder))
            return false;
        if (!folder.Remove(item.Key.identity)) return false;
        
        if (folder.Count == 0)
            _elements.Remove(item.Key.mod);
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

    bool ICollection<KeyValuePair<(IMod mod, string identity), T>>.IsReadOnly => false;
    public void Add((IMod mod, string identity) key, T value)
    {
        if(!_elements.TryGetValue(key.mod, out var folder))
            folder = _elements[key.mod] = [];
        folder.Add(key.identity, value);
    }

    public bool ContainsKey((IMod mod, string identity) key)
    {
        if(!_elements.TryGetValue(key.mod, out var folder))
            return false;
        return folder.ContainsKey(key.identity);
    }

    public bool Remove((IMod mod, string identity) key)
    {
        if(!_elements.TryGetValue(key.mod, out var folder))
            return false;
        if (folder.Remove(key.identity))
        {
            if (folder.Count == 0)
                _elements.Remove(key.mod);
            return true;
        }
        return false;
    }

    public bool TryGetValue((IMod mod, string identity) key, [MaybeNullWhen(false)] out T value)
    {
        if (!_elements.TryGetValue(key.mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.TryGetValue(key.identity, out value);
    }

    public T this[(IMod mod, string identity) key]
    {
        get
        {
            if (!_elements.TryGetValue(key.mod, out var folder))
                throw new KeyNotFoundException();
            return folder[key.identity];
        }
        set
        {
            if (!_elements.TryGetValue(key.mod, out var folder))
                folder = _elements[key.mod] = [];
            folder[key.identity] = value;
        }
    }
    
    public T this[string identity]
    {
        get
        {
            if (!identity.AsSpan().Contains(':'))
                throw new ArgumentException("Improper identity structure, expected: \"mod:element\"");
            
            if(!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
                throw new ArgumentException($"Mod \"{identity[..identity.IndexOf(':')]}\" could not be found");
            
            if (!_elements.TryGetValue(mod, out var folder))
                throw new KeyNotFoundException();
            return folder.GetAlternateLookup<ReadOnlySpan<char>>()[identity.AsSpan()[(identity.IndexOf(':') + 1)..]];
        }
        set
        {
            if (!identity.AsSpan().Contains(':'))
                throw new ArgumentException("Improper identity structure, expected: \"mod:element\"");
            
            if(!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
                throw new ArgumentException($"Mod \"{identity[..identity.IndexOf(':')]}\" could not be found");
            
            if (!_elements.TryGetValue(mod, out var folder))
                folder = _elements[mod] = [];
            folder[identity[(identity.IndexOf(':') + 1)..]] = value;
        }
    }

    #endregion

    #region =============================================== Keys ===============================================

    public class ElementKeys: ICollection<(IMod mod, string identity)>
    {
        private readonly ElementDictionary<T> _source;

        internal ElementKeys(ElementDictionary<T> source) => _source = source;
        
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public struct Enumerator(ElementDictionary<T> source) : IEnumerator<(IMod mod, string identity)>
        {
            private ElementDictionary<T>.Enumerator _source = source.GetEnumerator();
            public bool MoveNext() => _source.MoveNext();
            public void Reset() => throw new NotSupportedException();
            public (IMod mod, string identity) Current => _source.Current.Key;
            object IEnumerator.Current => _source.Current.Key;
            public void Dispose() => _source.Dispose();
        }

        public Enumerator GetEnumerator() => new(_source);
        IEnumerator<(IMod mod, string identity)> IEnumerable<(IMod mod, string identity)>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
        public bool Contains((IMod mod, string identity) item)
        {
            foreach (var key in this)
                if (key == item)
                    return true;
            return false;
        }
    
        public void CopyTo((IMod mod, string identity)[] array, int arrayIndex)
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
        bool ICollection<(IMod mod, string identity)>.IsReadOnly => true;
        
        void ICollection<(IMod mod, string identity)>.Add((IMod mod, string identity) item) => throw new NotSupportedException();
        void ICollection<(IMod mod, string identity)>.Clear() => throw new NotSupportedException();
        bool ICollection<(IMod mod, string identity)>.Remove((IMod mod, string identity) item) => throw new NotSupportedException();
    }
    
    public ElementKeys Keys { get => field ??= new(this); } = null;
    ICollection<(IMod mod, string identity)> IDictionary<(IMod mod, string identity), T>.Keys => Keys;
    
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
    ICollection<T> IDictionary<(IMod mod, string identity), T>.Values => Values;
    
    #endregion
}