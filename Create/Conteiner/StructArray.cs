using System.Collections;
namespace Create.Conteiner;

public static partial class StructArray
{
    static bool equals<T>(T b, T e) => (b is null) ? (e is null) : b.Equals(e);

    public struct Count2<T> : IList<T>, IList
    {
        T e0, e1;

        public int Count => 2;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..1]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..1]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count2<T> array;

            public Enumerator(Count2<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count3<T> : IList<T>, IList
    {
        T e0, e1, e2;

        public int Count => 3;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..2]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..2]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count3<T> array;

            public Enumerator(Count3<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count4<T> : IList<T>, IList
    {
        T e0, e1, e2, e3;

        public int Count => 4;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..3]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..3]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count4<T> array;

            public Enumerator(Count4<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count5<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4;

        public int Count => 5;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..4]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..4]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count5<T> array;

            public Enumerator(Count5<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count6<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4, e5;

        public int Count => 6;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    case 5: return e5;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..5]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    case 5: e5 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..5]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
            if (arrayIndex + 5 < Count) array[arrayIndex + 5] = e5;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            if (equals(e5, item)) return 5;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count6<T> array;

            public Enumerator(Count6<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count7<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4, e5, e6;

        public int Count => 7;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    case 5: return e5;
                    case 6: return e6;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..6]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    case 5: e5 = value; break;
                    case 6: e6 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..6]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
            e6 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            if (equals(e6, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
            if (arrayIndex + 5 < Count) array[arrayIndex + 5] = e5;
            if (arrayIndex + 6 < Count) array[arrayIndex + 6] = e6;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            if (equals(e5, item)) return 5;
            if (equals(e6, item)) return 6;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count7<T> array;

            public Enumerator(Count7<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count8<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4, e5, e6, e7;

        public int Count => 8;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    case 5: return e5;
                    case 6: return e6;
                    case 7: return e7;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..7]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    case 5: e5 = value; break;
                    case 6: e6 = value; break;
                    case 7: e7 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..7]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
            e6 = default!;
            e7 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            if (equals(e6, item)) return true;
            if (equals(e7, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
            if (arrayIndex + 5 < Count) array[arrayIndex + 5] = e5;
            if (arrayIndex + 6 < Count) array[arrayIndex + 6] = e6;
            if (arrayIndex + 7 < Count) array[arrayIndex + 7] = e7;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            if (equals(e5, item)) return 5;
            if (equals(e6, item)) return 6;
            if (equals(e7, item)) return 7;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count8<T> array;

            public Enumerator(Count8<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count9<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4, e5, e6, e7, e8;

        public int Count => 9;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    case 5: return e5;
                    case 6: return e6;
                    case 7: return e7;
                    case 8: return e8;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..8]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    case 5: e5 = value; break;
                    case 6: e6 = value; break;
                    case 7: e7 = value; break;
                    case 8: e8 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..8]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
            e6 = default!;
            e7 = default!;
            e8 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            if (equals(e6, item)) return true;
            if (equals(e7, item)) return true;
            if (equals(e8, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
            if (arrayIndex + 5 < Count) array[arrayIndex + 5] = e5;
            if (arrayIndex + 6 < Count) array[arrayIndex + 6] = e6;
            if (arrayIndex + 7 < Count) array[arrayIndex + 7] = e7;
            if (arrayIndex + 8 < Count) array[arrayIndex + 8] = e8;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            if (equals(e5, item)) return 5;
            if (equals(e6, item)) return 6;
            if (equals(e7, item)) return 7;
            if (equals(e8, item)) return 8;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count9<T> array;

            public Enumerator(Count9<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
    public struct Count10<T> : IList<T>, IList
    {
        T e0, e1, e2, e3, e4, e5, e6, e7, e8, e9;

        public int Count => 9;
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return e0;
                    case 1: return e1;
                    case 2: return e2;
                    case 3: return e3;
                    case 4: return e4;
                    case 5: return e5;
                    case 6: return e6;
                    case 7: return e7;
                    case 8: return e8;
                    case 9: return e9;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..9]");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: e0 = value; break;
                    case 1: e1 = value; break;
                    case 2: e2 = value; break;
                    case 3: e3 = value; break;
                    case 4: e4 = value; break;
                    case 5: e5 = value; break;
                    case 6: e6 = value; break;
                    case 7: e7 = value; break;
                    case 8: e8 = value; break;
                    case 9: e9 = value; break;
                    default: throw new IndexOutOfRangeException("Index must be in range [0..9]");
                }
            }
        }
        public void Clear()
        {
            e0 = default!;
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
            e6 = default!;
            e7 = default!;
            e8 = default!;
            e9 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e0, item)) return true;
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            if (equals(e6, item)) return true;
            if (equals(e7, item)) return true;
            if (equals(e8, item)) return true;
            if (equals(e9, item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < Count) array[arrayIndex] = e0;
            if (arrayIndex + 1 < Count) array[arrayIndex + 1] = e1;
            if (arrayIndex + 2 < Count) array[arrayIndex + 2] = e2;
            if (arrayIndex + 3 < Count) array[arrayIndex + 3] = e3;
            if (arrayIndex + 4 < Count) array[arrayIndex + 4] = e4;
            if (arrayIndex + 5 < Count) array[arrayIndex + 5] = e5;
            if (arrayIndex + 6 < Count) array[arrayIndex + 6] = e6;
            if (arrayIndex + 7 < Count) array[arrayIndex + 7] = e7;
            if (arrayIndex + 8 < Count) array[arrayIndex + 8] = e8;
            if (arrayIndex + 9 < Count) array[arrayIndex + 9] = e9;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            if (equals(e0, item)) return 0;
            if (equals(e1, item)) return 1;
            if (equals(e2, item)) return 2;
            if (equals(e3, item)) return 3;
            if (equals(e4, item)) return 4;
            if (equals(e5, item)) return 5;
            if (equals(e6, item)) return 6;
            if (equals(e7, item)) return 7;
            if (equals(e8, item)) return 8;
            if (equals(e9, item)) return 9;
            return -1;
        }

        object? IList.this[int index] { get => this[index]; set => this[index] = (T)value!; }
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection.CopyTo(Array array, int index) => CopyTo((T[])array, index);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {
            byte index = 0;
            Count10<T> array;

            public Enumerator(Count10<T> array) => this.array = array;
            public T Current => array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() { }
            public void Reset() => index = 0;
            public bool MoveNext()
            {
                index++;
                return index - 1 < array.Count;
            }
        }
    }
}