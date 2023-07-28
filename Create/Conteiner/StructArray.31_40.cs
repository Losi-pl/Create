using System.Collections;
namespace Create.Conteiner;

partial class StructArray
{
    public struct Count31<T> : IList<T>, IList
    {
        Count25<T> count_main;
        Count6<T> count_secendary;

        public int Count => 31;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..31]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..31]");
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
            Count31<T> array;

            public Enumerator(Count31<T> array) => this.array = array;
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
    public struct Count32<T> : IList<T>, IList
    {
        Count29<T> count_main;
        Count3<T> count_secendary;

        public int Count => 32;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..32]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..32]");
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
            Count32<T> array;

            public Enumerator(Count32<T> array) => this.array = array;
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
    public struct Count33<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count3<T> count_secendary;

        public int Count => 33;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..33]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..33]");
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
            Count33<T> array;

            public Enumerator(Count33<T> array) => this.array = array;
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
    public struct Count34<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count4<T> count_secendary;

        public int Count => 34;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..34]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..34]");
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
            Count34<T> array;

            public Enumerator(Count34<T> array) => this.array = array;
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
    public struct Count35<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count5<T> count_secendary;

        public int Count => 35;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..35]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..35]");
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
            Count35<T> array;

            public Enumerator(Count35<T> array) => this.array = array;
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
    public struct Count36<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count6<T> count_secendary;

        public int Count => 36;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..36]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..36]");
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
            Count36<T> array;

            public Enumerator(Count36<T> array) => this.array = array;
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
    public struct Count37<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count7<T> count_secendary;

        public int Count => 37;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..37]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..37]");
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
            Count37<T> array;

            public Enumerator(Count37<T> array) => this.array = array;
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
    public struct Count38<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count8<T> count_secendary;

        public int Count => 38;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..38]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..38]");
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
            Count38<T> array;

            public Enumerator(Count38<T> array) => this.array = array;
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
    public struct Count39<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count9<T> count_secendary;

        public int Count => 39;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..39]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..39]");
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
            Count39<T> array;

            public Enumerator(Count39<T> array) => this.array = array;
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
    public struct Count40<T> : IList<T>, IList
    {
        Count30<T> count_main;
        Count10<T> count_secendary;

        public int Count => 40;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..40]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..40]");
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
            Count40<T> array;

            public Enumerator(Count40<T> array) => this.array = array;
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
