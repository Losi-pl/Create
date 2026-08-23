using Create.Registry;
using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace Create.Assets;

public class FrozenElementDictionary<T> : IDictionary<(IMod mod, string identity), T>
{
    #region =============================================== Main ===============================================
    private readonly FrozenDictionary<IMod, FrozenDictionary<string, T>> _elements;
    private readonly int _count;

    internal FrozenElementDictionary(FrozenDictionary<IMod, FrozenDictionary<string, T>> elements)
    {
        _elements = elements;
        _count = elements.Count;
    }

    public T this[(IMod mod, string identity) key] { get {
            if(!_elements.TryGetValue(key.mod, out var folder))
                throw new KeyNotFoundException();
            return folder[key.identity];
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
            return folder.GetAlternateLookup()[identity.AsSpan()[(identity.IndexOf(':') + 1)..]];
        }
    }

    public int Count => _count;

    bool ICollection<KeyValuePair<(IMod mod, string identity), T>>.IsReadOnly => true;
    T IDictionary<(IMod mod, string identity), T>.this[(IMod mod, string identity) key] { get => this[key]; set => throw new InvalidOperationException(); }
    void IDictionary<(IMod mod, string identity), T>.Add((IMod mod, string identity) key, T value) => throw new InvalidOperationException();
    void ICollection<KeyValuePair<(IMod mod, string identity), T>>.Add(KeyValuePair<(IMod mod, string identity), T> item) => throw new InvalidOperationException();
    void ICollection<KeyValuePair<(IMod mod, string identity), T>>.Clear() => throw new InvalidOperationException();
    bool IDictionary<(IMod mod, string identity), T>.Remove((IMod mod, string identity) key) => throw new InvalidOperationException();
    bool ICollection<KeyValuePair<(IMod mod, string identity), T>>.Remove(KeyValuePair<(IMod mod, string identity), T> item) => throw new InvalidOperationException();

    public bool Contains(KeyValuePair<(IMod mod, string identity), T> item)
    {
        if (!_elements.TryGetValue(item.Key.mod, out var folder))
            return false;
        if(folder.TryGetValue(item.Key.identity, out var value))
            return EqualityComparer<T>.Default.Equals(item.Value, value);
        return false;
    }
    public bool Contains(KeyValuePair<string, T> item)
    {
        if (!item.Key.AsSpan().Contains(':'))
            throw new ArgumentException("Improper identity structure, expected: \"mod:element\"");
            
        if(!IMod.Mods.GetAlternateLookup().TryGetValue(item.Key.AsSpan()[..item.Key.IndexOf(':')], out var mod))
            return false;
            
        if (!_elements.TryGetValue(mod, out var folder))
            return false;
        if(folder.GetAlternateLookup().TryGetValue(item.Key.AsSpan()[(item.Key.IndexOf(':') + 1)..], out var value))
            return EqualityComparer<T>.Default.Equals(item.Value, value);
        return false;
    }

    public bool ContainsKey((IMod mod, string identity) key)
    {
        if (!_elements.TryGetValue(key.mod, out var folder))
            return false;
        return folder.ContainsKey(key.identity);
    }
    public bool ContainsKey(string identity)
    {
        if (!identity.AsSpan().Contains(':'))
            throw new ArgumentException("Improper identity structure, expected: \"mod:element\"");
            
        if(!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
            return false;
            
        if (!_elements.TryGetValue(mod, out var folder))
            return false;
        return folder.GetAlternateLookup().ContainsKey(identity.AsSpan()[(identity.IndexOf(':') + 1)..]);
    }

    public void CopyTo(KeyValuePair<(IMod mod, string identity), T>[] array, int arrayIndex)
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

    public bool TryGetValue((IMod mod, string identity) key, [MaybeNullWhen(false)] out T value)
    {
        if(!_elements.TryGetValue(key.mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.TryGetValue(key.identity, out value);
    }
    public bool TryGetValue(string identity, [MaybeNullWhen(false)] out T value)
    {
        if (!identity.AsSpan().Contains(':'))
            throw new ArgumentException("Improper identity structure, expected: \"mod:element\"");

        if (!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
        {
            value = default;
            return false;
        }
        
        if(!_elements.TryGetValue(mod, out var folder))
        {
            value = default;
            return false;
        }
        return folder.GetAlternateLookup().TryGetValue(identity.AsSpan()[(identity.IndexOf(':') + 1)..], out value);
    }

    public struct Enumerator : IEnumerator<KeyValuePair<(IMod mod, string identity), T>>
    {
        private FrozenDictionary<IMod, FrozenDictionary<string, T>>.Enumerator _folder;
        private FrozenDictionary<string, T>.Enumerator _elements;
        private KeyValuePair<(IMod mod, string identity), T> _current;
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
            if (!_hasNext)
                return;
            _hasNext = false;
        }
    }

    public Enumerator GetEnumerator() => new(this);
    IEnumerator<KeyValuePair<(IMod mod, string identity), T>> IEnumerable<KeyValuePair<(IMod mod, string identity), T>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    #region =============================================== Keys ===============================================

    public class ElementKeys(FrozenElementDictionary<T> source) : ICollection<(IMod mod, string identity)>
    {
        public int Count => source.Count;

        bool ICollection<(IMod mod, string identity)>.IsReadOnly => true;
        void ICollection<(IMod mod, string identity)>.Clear() => throw new InvalidOperationException();
        void ICollection<(IMod mod, string identity)>.Add((IMod mod, string identity) item) => throw new InvalidOperationException();
        bool ICollection<(IMod mod, string identity)>.Remove((IMod mod, string identity) item) => throw new InvalidOperationException();

        public bool Contains((IMod mod, string identity) item) => source.TryGetValue(item, out _);

        public void CopyTo((IMod mod, string identity)[] array, int arrayIndex)
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

        public struct Enumerator : IEnumerator<(IMod mod, string identity)>
        {
            FrozenElementDictionary<T>.Enumerator _enumerator;

            public Enumerator(FrozenElementDictionary<T> source) => _enumerator = source.GetEnumerator();

            public (IMod mod, string identity) Current => _enumerator.Current.Key;
            object IEnumerator.Current => _enumerator.Current.Key;

            public void Dispose() => _enumerator.Dispose();
            public bool MoveNext() => _enumerator.MoveNext();

            void IEnumerator.Reset() => throw new InvalidOperationException();
        }

        public Enumerator GetEnumerator() => new(source);
        IEnumerator<(IMod mod, string identity)> IEnumerable<(IMod mod, string identity)>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public ElementKeys Keys { get => field ??= new(this); } = null;
    ICollection<(IMod mod, string identity)> IDictionary<(IMod mod, string identity), T>.Keys => Keys;
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
    ICollection<T> IDictionary<(IMod mod, string identity), T>.Values => Values;
    #endregion
}
