using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Create.General;

public static class RefEnumerators
{
    private const MethodImplOptions Aggressive = MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining;
    
    extension<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> collection)
    {
        /// <summary>
        /// Creates an allocation free enumerator for <see cref="System.Collections.ObjectModel.ReadOnlyCollection{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(Aggressive)]
        public ReadOnlyCollection<T> GetRefEnum() => new(collection);
    }

    extension<T>(T value) where T : struct, IComparable<T>, IBinaryInteger<T>
    {
        /// <summary>
        /// Used to allow for simply putting and Binary Intiger into as foreach to not need to construct a loop manually
        /// </summary>
        /// <returns>An iterator of 0 ≤ x &lt; value</returns>
        [MethodImpl(Aggressive)]
        public RangeIterable<T> GetEnumerator() => new(value);
    }
    
    /// <summary>
    /// Purely for <see cref="RefEnumerators.GetRefEnum{T}"/>
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="T"></typeparam>
    public readonly ref struct ReadOnlyCollection<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> collection)
    {
        [MethodImpl(Aggressive)]
        public Enumerator GetEnumerator() => new Enumerator(collection);
        
        public ref struct Enumerator(System.Collections.ObjectModel.ReadOnlyCollection<T> collection) : IEnumerator<T>
        {
            private int _index = -1;

            [MethodImpl(Aggressive)]
            public void Dispose() => Reset();
            [MethodImpl(Aggressive)]
            public bool MoveNext() => ++_index < collection.Count;
            [MethodImpl(Aggressive)]
            public void Reset() => _index = -1;

            public T Current { [MethodImpl(Aggressive)] get => collection[_index]; } 
            object? IEnumerator.Current => collection[_index];
        }
    }

    public ref struct RangeIterable<T>(T upTo) : IEnumerator<T> where T : struct, IComparable<T>, IBinaryInteger<T>
    {
        private T _value = T.Zero - T.One;

        [MethodImpl(Aggressive)]
        public bool MoveNext()
        {
            _value += T.One;
            return _value < upTo;
        }
        [MethodImpl(Aggressive)]
        public void Reset() => _value = T.Zero - T.One;

        public T Current { [MethodImpl(Aggressive)] get => _value; }

        object? IEnumerator.Current => _value;
        [MethodImpl(Aggressive)]
        public void Dispose() => Reset();
    }
}