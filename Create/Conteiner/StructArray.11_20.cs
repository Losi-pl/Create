using System.Collections;
namespace Create.Conteiner;

partial class StructArray
{
    public struct Count11<T> : IList<T>, IList
    {
        Count10<T> count10;
        T e1;

        public int Count => 11;
        public T this[int index]
        {
            get
            {
                if (index == 10)
                    return e1;
                else if(index >= 0 && index <= 9)
                    return count10[index];
                throw new IndexOutOfRangeException("Index must be in range [0..10]");
            }
            set
            {
                if (index == 10)
                    e1 = value;
                else if (index >= 0 && index <= 9)
                    count10[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..10]");
            }
        }
        public void Clear()
        {
            count10.Clear();
            e1 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e1, item)) return true;
            if (count10.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count10.CopyTo(array, arrayIndex);
            if (arrayIndex + 10 < Count) array[arrayIndex + 10] = e1;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var tmp = count10.IndexOf(item);
            if (tmp != -1) return tmp;
            if (equals(e1, item)) return 10;
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
            Count11<T> array;

            public Enumerator(Count11<T> array) => this.array = array;
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
    public struct Count12<T> : IList<T>, IList
    {
        Count10<T> count10;
        T e1, e2;

        public int Count => 12;
        public T this[int index]
        {
            get
            {
                if (index == 10) return e1;
                if (index == 11) return e2;
                if (index >= 0 && index <= 9) return count10[index];
                throw new IndexOutOfRangeException("Index must be in range [0..11]");
            }
            set
            {
                if (index == 10) e1 = value;
                if (index == 11) e2 = value;
                if (index >= 0 && index <= 9) count10[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..11]");
            }
        }
        public void Clear()
        {
            count10.Clear();
            e1 = default!;
            e2 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (count10.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count10.CopyTo(array, arrayIndex);
            if (arrayIndex + 10 < Count) array[arrayIndex + 10] = e1;
            if (arrayIndex + 11 < Count) array[arrayIndex + 11] = e2;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var tmp = count10.IndexOf(item);
            if (tmp != -1) return tmp;
            if (equals(e1, item)) return 10;
            if (equals(e2, item)) return 11;
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
            Count12<T> array;

            public Enumerator(Count12<T> array) => this.array = array;
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
    public struct Count13<T> : IList<T>, IList
    {
        Count10<T> count10;
        T e1, e2, e3;

        public int Count => 13;
        public T this[int index]
        {
            get
            {
                if (index == 10) return e1;
                if (index == 11) return e2;
                if (index == 12) return e3;
                if (index >= 0 && index <= 9) return count10[index];
                throw new IndexOutOfRangeException("Index must be in range [0..12]");
            }
            set
            {
                if (index == 10) e1 = value;
                if (index == 11) e2 = value;
                if (index == 12) e3 = value;
                if (index >= 0 && index <= 9) count10[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..12]");
            }
        }
        public void Clear()
        {
            count10.Clear();
            e1 = default!;
            e2 = default!;
            e3 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (count10.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count10.CopyTo(array, arrayIndex);
            if (arrayIndex + 10 < Count) array[arrayIndex + 10] = e1;
            if (arrayIndex + 11 < Count) array[arrayIndex + 11] = e2;
            if (arrayIndex + 12 < Count) array[arrayIndex + 12] = e3;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var tmp = count10.IndexOf(item);
            if (tmp != -1) return tmp;
            if (equals(e1, item)) return 10;
            if (equals(e2, item)) return 11;
            if (equals(e3, item)) return 12;
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
            Count13<T> array;

            public Enumerator(Count13<T> array) => this.array = array;
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
    public struct Count14<T> : IList<T>, IList
    {
        Count10<T> count10;
        T e1, e2, e3, e4;

        public int Count => 14;
        public T this[int index]
        {
            get
            {
                if (index == 10) return e1;
                if (index == 11) return e2;
                if (index == 12) return e3;
                if (index == 13) return e4;
                if (index >= 0 && index <= 9) return count10[index];
                throw new IndexOutOfRangeException("Index must be in range [0..13]");
            }
            set
            {
                if (index == 10) e1 = value;
                if (index == 11) e2 = value;
                if (index == 12) e3 = value;
                if (index == 13) e4 = value;
                if (index >= 0 && index <= 9) count10[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..13]");
            }
        }
        public void Clear()
        {
            count10.Clear();
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (count10.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count10.CopyTo(array, arrayIndex);
            if (arrayIndex + 10 < Count) array[arrayIndex + 10] = e1;
            if (arrayIndex + 11 < Count) array[arrayIndex + 11] = e2;
            if (arrayIndex + 12 < Count) array[arrayIndex + 12] = e3;
            if (arrayIndex + 13 < Count) array[arrayIndex + 13] = e4;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var tmp = count10.IndexOf(item);
            if (tmp != -1) return tmp;
            if (equals(e1, item)) return 10;
            if (equals(e2, item)) return 11;
            if (equals(e3, item)) return 12;
            if (equals(e4, item)) return 13;
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
            Count14<T> array;

            public Enumerator(Count14<T> array) => this.array = array;
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
    public struct Count15<T> : IList<T>, IList
    {
        Count10<T> count10;
        T e1, e2, e3, e4, e5;

        public int Count => 15;
        public T this[int index]
        {
            get
            {
                if (index == 10) return e1;
                if (index == 11) return e2;
                if (index == 12) return e3;
                if (index == 13) return e4;
                if (index == 14) return e5;
                if (index >= 0 && index <= 9) return count10[index];
                throw new IndexOutOfRangeException("Index must be in range [0..14]");
            }
            set
            {
                if (index == 10) e1 = value;
                if (index == 11) e2 = value;
                if (index == 12) e3 = value;
                if (index == 13) e4 = value;
                if (index == 14) e5 = value;
                if (index >= 0 && index <= 9) count10[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..14]");
            }
        }
        public void Clear()
        {
            count10.Clear();
            e1 = default!;
            e2 = default!;
            e3 = default!;
            e4 = default!;
            e5 = default!;
        }
        public bool Contains(T item)
        {
            if (equals(e1, item)) return true;
            if (equals(e2, item)) return true;
            if (equals(e3, item)) return true;
            if (equals(e4, item)) return true;
            if (equals(e5, item)) return true;
            if (count10.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count10.CopyTo(array, arrayIndex);
            if (arrayIndex + 10 < Count) array[arrayIndex + 10] = e1;
            if (arrayIndex + 11 < Count) array[arrayIndex + 11] = e2;
            if (arrayIndex + 12 < Count) array[arrayIndex + 12] = e3;
            if (arrayIndex + 13 < Count) array[arrayIndex + 13] = e4;
            if (arrayIndex + 14 < Count) array[arrayIndex + 14] = e5;
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var tmp = count10.IndexOf(item);
            if (tmp != -1) return tmp;
            if (equals(e1, item)) return 10;
            if (equals(e2, item)) return 11;
            if (equals(e3, item)) return 12;
            if (equals(e4, item)) return 13;
            if (equals(e5, item)) return 14;
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
            Count15<T> array;

            public Enumerator(Count15<T> array) => this.array = array;
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
    public struct Count16<T> : IList<T>, IList
    {
        Count10<T> count_main;
        Count6<T> count_secendary;

        public int Count => 16;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
        }
        public void Clear()
        {
            count_main.Clear();
            count_secendary.Clear();
        }
        public bool Contains(T item)
        {
            if (count_main.Contains(item)) return true;
            if (count_secendary.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count_main.CopyTo(array, arrayIndex);
            count_secendary.CopyTo(array, arrayIndex + count_main.Count);
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var ind = count_main.IndexOf(item);
            if (ind != -1) return ind;
            ind = count_secendary.IndexOf(item);
            if (ind != -1) return ind;
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
            Count16<T> array;

            public Enumerator(Count16<T> array) => this.array = array;
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
    public struct Count17<T> : IList<T>, IList
    {
        Count10<T> count_main;
        Count7<T> count_secendary;

        public int Count => 17;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
        }
        public void Clear()
        {
            count_main.Clear();
            count_secendary.Clear();
        }
        public bool Contains(T item)
        {
            if (count_main.Contains(item)) return true;
            if (count_secendary.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count_main.CopyTo(array, arrayIndex);
            count_secendary.CopyTo(array, arrayIndex + count_main.Count);
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var ind = count_main.IndexOf(item);
            if (ind != -1) return ind;
            ind = count_secendary.IndexOf(item);
            if (ind != -1) return ind;
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
            Count17<T> array;

            public Enumerator(Count17<T> array) => this.array = array;
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
    public struct Count18<T> : IList<T>, IList
    {
        Count10<T> count_main;
        Count8<T> count_secendary;

        public int Count => 18;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
        }
        public void Clear()
        {
            count_main.Clear();
            count_secendary.Clear();
        }
        public bool Contains(T item)
        {
            if (count_main.Contains(item)) return true;
            if (count_secendary.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count_main.CopyTo(array, arrayIndex);
            count_secendary.CopyTo(array, arrayIndex + count_main.Count);
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var ind = count_main.IndexOf(item);
            if (ind != -1) return ind;
            ind = count_secendary.IndexOf(item);
            if (ind != -1) return ind;
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
            Count18<T> array;

            public Enumerator(Count18<T> array) => this.array = array;
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
    public struct Count19<T> : IList<T>, IList
    {
        Count10<T> count_main;
        Count9<T> count_secendary;

        public int Count => 19;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..15]");
            }
        }
        public void Clear()
        {
            count_main.Clear();
            count_secendary.Clear();
        }
        public bool Contains(T item)
        {
            if (count_main.Contains(item)) return true;
            if (count_secendary.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count_main.CopyTo(array, arrayIndex);
            count_secendary.CopyTo(array, arrayIndex + count_main.Count);
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var ind = count_main.IndexOf(item);
            if (ind != -1) return ind;
            ind = count_secendary.IndexOf(item);
            if (ind != -1) return ind;
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
            Count19<T> array;

            public Enumerator(Count19<T> array) => this.array = array;
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
    public struct Count20<T> : IList<T>, IList
    {
        Count9<T> count_main;
        Count2<T> count_secendary;

        public int Count => 20;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..19]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..19]");
            }
        }
        public void Clear()
        {
            count_main.Clear();
            count_secendary.Clear();
        }
        public bool Contains(T item)
        {
            if (count_main.Contains(item)) return true;
            if (count_secendary.Contains(item)) return true;
            return false;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            count_main.CopyTo(array, arrayIndex);
            count_secendary.CopyTo(array, arrayIndex + count_main.Count);
        }
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        public int IndexOf(T item)
        {
            var ind = count_main.IndexOf(item);
            if (ind != -1) return ind;
            ind = count_secendary.IndexOf(item);
            if (ind != -1) return ind;
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
            Count20<T> array;

            public Enumerator(Count20<T> array) => this.array = array;
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
