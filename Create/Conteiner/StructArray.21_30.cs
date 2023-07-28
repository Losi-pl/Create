using System.Collections;
namespace Create.Conteiner;

partial class StructArray
{
    public struct Count21<T> : IList<T>, IList
    {
        Count15<T> count_main;
        Count6<T> count_secendary;

        public int Count => 21;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..21]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..21]");
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
            Count21<T> array;

            public Enumerator(Count21<T> array) => this.array = array;
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
    public struct Count22<T> : IList<T>, IList
    {
        Count19<T> count_main;
        Count3<T> count_secendary;

        public int Count => 22;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..22]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..22]");
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
            Count22<T> array;

            public Enumerator(Count22<T> array) => this.array = array;
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
    public struct Count23<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count3<T> count_secendary;

        public int Count => 23;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..23]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..23]");
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
            Count23<T> array;

            public Enumerator(Count23<T> array) => this.array = array;
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
    public struct Count24<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count4<T> count_secendary;

        public int Count => 24;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..24]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..24]");
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
            Count24<T> array;

            public Enumerator(Count24<T> array) => this.array = array;
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
    public struct Count25<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count5<T> count_secendary;

        public int Count => 25;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..25]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..25]");
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
            Count26<T> array;

            public Enumerator(Count26<T> array) => this.array = array;
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
    public struct Count26<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count6<T> count_secendary;

        public int Count => 26;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..26]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..26]");
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
            Count26<T> array;

            public Enumerator(Count26<T> array) => this.array = array;
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
    public struct Count27<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count7<T> count_secendary;

        public int Count => 27;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..27]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..27]");
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
            Count27<T> array;

            public Enumerator(Count27<T> array) => this.array = array;
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
    public struct Count28<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count8<T> count_secendary;

        public int Count => 28;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..28]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..28]");
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
            Count28<T> array;

            public Enumerator(Count28<T> array) => this.array = array;
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
    public struct Count29<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count9<T> count_secendary;

        public int Count => 29;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..29]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..29]");
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
            Count29<T> array;

            public Enumerator(Count29<T> array) => this.array = array;
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
    public struct Count30<T> : IList<T>, IList
    {
        Count20<T> count_main;
        Count10<T> count_secendary;

        public int Count => 30;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..30]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..30]");
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
            Count30<T> array;

            public Enumerator(Count30<T> array) => this.array = array;
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
