using System.Collections;

namespace Create.General;

public static class RefEnumerators
{
    public static ReadOnlyCollection<T> GetRefEnum<T>(this System.Collections.ObjectModel.ReadOnlyCollection<T> collection) =>
        new(collection);
    
    public readonly ref struct ReadOnlyCollection<T>(System.Collections.ObjectModel.ReadOnlyCollection<T> collection)
    {
        public Enumerator GetEnumerator() => new Enumerator(collection);
        
        public struct Enumerator(System.Collections.ObjectModel.ReadOnlyCollection<T> collection) : IEnumerator<T>
        {
            private int _index = 0;

            public void Dispose() {  }
            public bool MoveNext() => _index < collection.Count;

            public void Reset() { _index = 0; }

            public T Current => collection[_index++];
            object? IEnumerator.Current => collection[_index++];
        }
        
    }
}