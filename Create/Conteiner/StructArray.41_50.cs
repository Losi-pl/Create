using System.Collections;
namespace Create.Conteiner;

partial class StructArray
{
    public struct Count41<T> : IList<T>, IList
    {
        Count35<T> count_main;
        Count6<T> count_secendary;

        public int Count => 41;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..41]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..41]");
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
            Count41<T> array;

            public Enumerator(Count41<T> array) => this.array = array;
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
    public struct Count42<T> : IList<T>, IList
    {
        Count39<T> count_main;
        Count3<T> count_secendary;

        public int Count => 42;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..42]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..42]");
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
            Count42<T> array;

            public Enumerator(Count42<T> array) => this.array = array;
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
    public struct Count43<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count3<T> count_secendary;

        public int Count => 43;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..43]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..43]");
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
            Count43<T> array;

            public Enumerator(Count43<T> array) => this.array = array;
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
    public struct Count44<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count4<T> count_secendary;

        public int Count => 44;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..44]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..44]");
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
            Count44<T> array;

            public Enumerator(Count44<T> array) => this.array = array;
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
    public struct Count45<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count5<T> count_secendary;

        public int Count => 45;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..45]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..45]");
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
            Count45<T> array;

            public Enumerator(Count45<T> array) => this.array = array;
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
    public struct Count46<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count6<T> count_secendary;

        public int Count => 46;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..46]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..46]");
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
            Count46<T> array;

            public Enumerator(Count46<T> array) => this.array = array;
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
    public struct Count47<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count7<T> count_secendary;

        public int Count => 47;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..47]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..47]");
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
            Count47<T> array;

            public Enumerator(Count47<T> array) => this.array = array;
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
    public struct Count48<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count8<T> count_secendary;

        public int Count => 48;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..48]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..48]");
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
            Count48<T> array;

            public Enumerator(Count48<T> array) => this.array = array;
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
    public struct Count49<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count9<T> count_secendary;

        public int Count => 49;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..49]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..49]");
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
            Count49<T> array;

            public Enumerator(Count49<T> array) => this.array = array;
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
    public struct Count50<T> : IList<T>, IList
    {
        Count40<T> count_main;
        Count10<T> count_secendary;

        public int Count => 50;
        public T this[int index]
        {
            get
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) return count_secendary[index - count_main.Count];
                if (index >= 0 && index < count_main.Count) return count_main[index];
                throw new IndexOutOfRangeException("Index must be in range [0..50]");
            }
            set
            {
                if (index >= count_main.Count && index < count_main.Count + count_secendary.Count) count_secendary[index - count_main.Count] = value;
                if (index >= 0 && index < count_main.Count) count_main[index] = value;
                throw new IndexOutOfRangeException("Index must be in range [0..50]");
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
            Count50<T> array;

            public Enumerator(Count50<T> array) => this.array = array;
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
