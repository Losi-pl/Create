using System.Collections;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CommunityToolkit.HighPerformance;
using Create.Elements;

// ReSharper disable MemberCanBePrivate.Global

namespace Create.Registry;

public static class GameElements
{
    private static (Dictionary<Type, TypeLibrary>? mutable, FrozenDictionary<Type, TypeLibrary>? immutable) _librarys = (new(), null);

    public static bool IsFrozen => _librarys.immutable is not null;

    internal static TypeLibrary<T> OpenLibrary<T>() where T : ElementBase
    {
        if(IsFrozen)
            throw new InvalidOperationException("Element registration is already finished");
        lock (_librarys.mutable!)
        {
            if (_librarys.mutable!.TryGetValue(typeof(T), out var typeLibrary))
                return (TypeLibrary<T>)typeLibrary;
            TypeLibrary<T> library = new();
            _librarys.mutable[typeof(T)] = library;
            return library;
        }
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static TypeLibrary<T> Get<T>() where T : ElementBase
    {
        if (_librarys.immutable is { } imDict)
        {
            if(imDict.TryGetValue(typeof(T), out var typeLibrary))
                return (TypeLibrary<T>)typeLibrary;
            throw new ArgumentException($"A library for type {typeof(T)} was not found.");
        }
        if(_librarys.mutable is { } mDict)
        {
            if(mDict.TryGetValue(typeof(T), out var typeLibrary))
                return (TypeLibrary<T>)typeLibrary;
            return (TypeLibrary<T>)(mDict[typeof(T)] = new TypeLibrary<T>());
        }
        throw new InvalidOperationException("Game Element libraries corrupted");
    }

    public static T? Get<T>(int index) where T : ElementBase => Get<T>().Get(index);
    public static T? Get<T>(ushort id) where T : ElementBase => Get<T>().Get(id);
    public static T? Get<T>(ElementIdent identity) where T : ElementBase => Get<T>().Get(identity);
    public static FilteredEnumerable<T> Get<T>(Predicate<T> predicate) where T : ElementBase => new(Get<T>(), predicate);

    internal static void FreezeElements()
    {
        if(IsFrozen)
            return;
        lock (_librarys.mutable!)
            _librarys.immutable = _librarys.mutable!.ToFrozenDictionary();
        _librarys.mutable = null;
        foreach (var library in _librarys.immutable)
            library.Value.Freeze();
    }

    public static IEnumerable<(TElement Element, string Name)> FindElements<TElement>(Type source)
    {
        foreach (var element in source.GetFields())
        {
            if(!element.IsPublic)
                continue;
            if(!element.IsStatic)
                continue;
            
            if(element.GetCustomAttribute<IgnoreElementAttribute>() is not null)
                continue;
            
            if(element.FieldType != typeof(TElement) && !element.FieldType.IsSubclassOf(typeof(TElement)))
                continue;
            
            var name = element.GetCustomAttribute<ElementNameAttribute>()?.Name ?? KebabCase(element.Name);

            yield return ((TElement)element.GetValue(null)!, name);
        }
        
        string KebabCase(string s) =>
            string.Concat(s.Select((c, i) =>
                char.IsUpper(c) && i > 0 ? $"-{char.ToLower(c)}" : c.ToString().ToLower()));
    }
    
    public readonly struct FilteredEnumerable<T>(TypeLibrary<T> library, Predicate<T> predicate) : IEnumerable<T> where T : ElementBase
    {
        public TypeLibrary<T>.Enumerator GetEnumerator() => new(library, predicate);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public abstract class TypeLibrary
    {
        // ReSharper disable MemberHidesStaticFromOuterClass
        internal abstract void Freeze();
        public abstract bool IsFrozen { get; }
        // ReSharper restore MemberHidesStaticFromOuterClass
    }
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class TypeLibrary<T> : TypeLibrary, IEnumerable<T> where T : ElementBase
    {
        // ReSharper disable InconsistentNaming
        private (List<T>? Elements, Dictionary<ElementIdent, int>? ByIdentity) Mutable = ([], []);
        private (ImmutableArray<T>? Elements, FrozenDictionary<ElementIdent, int>? ByIdentity) Immutable = (null, null);
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private FrozenDictionary<ushort, int>? ByID = null;
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Lock Lock = new();
        // ReSharper restore InconsistentNaming

        public int Count => Immutable.Elements?.Length ?? Mutable.Elements?.Count ?? throw new InvalidOperationException("Type Library corrupted");
        
        public T? Get(int index)
        {
            if (Immutable.Elements is { } imList)
            {
                if(imList.Length > index && index >= 0)
                    return imList[index];
            }
            else if (Mutable.Elements is { } mList)
            {
                if(mList.Count > index && index >= 0)
                    return mList[index];
            }

            return null;
        }
        public T? Get(ElementIdent identity)
        {
            var index = IdentityToIndex(identity);
            return index != null ? Get(index.Value) : null;
        }
        public T? Get(ushort id)
        {
            var index = IDToIndex(id);
            return index != null ? Get(index.Value) : null;
        }

        public int? IdentityToIndex(ElementIdent identity)
        {
            if (Immutable.ByIdentity is { } imDict)
            {
                if(imDict.TryGetValue(identity, out var id))
                    return id;
            }
            else if (Mutable.ByIdentity is { } mDict)
            {
                if(mDict.TryGetValue(identity, out var id))
                    return id;
            }

            return null;
        }
        // ReSharper disable once InconsistentNaming
        public int? IDToIndex(ushort identity)
        {
            if (ByID is { } imDict)
                if(imDict.TryGetValue(identity, out var id))
                    return id;

            return null;
        }

        internal void RegisterElement(ElementIdent identity, T element)
        {
            if (IsFrozen)
                throw new InvalidOperationException("Element registration is already finished");
            int index;
            lock (Lock)
            {
                if(element.IsRegistered)
                    throw new ArgumentException("This element has already been registered");
                if(Mutable.ByIdentity!.ContainsKey(identity))
                    throw new ArgumentException($"Identity {identity} is already registered");
                
                index = Mutable.Elements!.Count;
                Mutable.Elements.Add(element);
                Mutable.ByIdentity[identity] = index;
            }
            element.ElementRegistered(identity, index);
        }
        
        internal override void Freeze()
        {
            lock (Lock)
            {
                if(IsFrozen)
                    return;
            
                // ReSharper disable once UseCollectionExpression
                Immutable.Elements = ImmutableArray.Create(Mutable.Elements.AsSpan());
                Mutable.Elements = null;

                Immutable.ByIdentity = Mutable.ByIdentity!.ToFrozenDictionary();
                Mutable.ByIdentity = null;
            }
        }

        // ReSharper disable once MemberHidesStaticFromOuterClass
        public override bool IsFrozen => Immutable.Elements is not null;

        public Enumerator GetEnumerator() => new(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            private ImmutableArray<T>.Enumerator _immutable;
            private List<T>.Enumerator _mutable;
            private readonly bool _isFrozen;
            private readonly Predicate<T>? _predicate;

            public Enumerator(TypeLibrary<T> source, Predicate<T>? predicate = null)
            {
                _predicate = predicate;
                _isFrozen = source.IsFrozen;
                if (_isFrozen)
                    _immutable = source.Immutable.Elements!.Value.GetEnumerator();
                else
                    _mutable = source.Mutable.Elements!.GetEnumerator();
            }


            public bool MoveNext()
            {
                if (_isFrozen)
                {
                    do
                    {
                        if(!_immutable.MoveNext())
                            return false;
                    } while (!_predicate?.Invoke(_immutable.Current) ?? false);
                    return true;
                }
                
                do
                {
                    if(!_mutable.MoveNext())
                        return false;
                } while (!_predicate?.Invoke(_mutable.Current) ?? false);
                return true;
            }

            public void Reset() => throw new NotSupportedException();

            public T Current => _isFrozen ? _immutable.Current : _mutable.Current;
            object IEnumerator.Current => Current;

            void IDisposable.Dispose() { }
        }
    }
}