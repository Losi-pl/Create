using System.Collections;
using System.Diagnostics;

namespace Create.Linq;

public static class EnumerablesGL
{
    /// <summary>
    /// Tworzy kolekcje z kopią zawartości <see cref="System.ReadOnlySpan{T}"/>
    /// </summary>
    public static IEnumerable<T> ToEnumerable<T>(this ReadOnlySpan<T> span)
    {
        T[] array = new T[span.Length];
        for (int i = 0; i < span.Length; i++)
            array[i] = span[i];
        return array.Secure();
    }

    /// <summary>
    /// Zwraca obiekt i gdzie w kolekcji się on znajduje który spełnia warunki <paramref name="condition"/>
    /// </summary>
    public static (T element, int index)? FindAndWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> condition)
    {
        int i = 0;
        foreach (var element in enumerable)
        {
            if (condition(element))
                return (element, i);
            ++i;
        }
        return null;
    }

    /// <summary>
    /// Zwraca element z kolekcji spełnaijący warunki <paramref name="condition"/>
    /// </summary>
    [DebuggerHidden]
    public static T Find<T>(this IEnumerable<T> enume, Func<T, bool> condition, Exception? ifNotFound = null)
    {
        foreach (var element in enume)
            if (condition(element))
                return element;
        if (ifNotFound != null)
            throw ifNotFound;
        return default!;
    }

    /// <summary>
    /// Powtarza ciąg <paramref name="count"/> ilość razy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="count">Ile razy powturzyć ciąg</param>
    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> values, int count)
    {
        for (; count > 0; count--)
            foreach (var t in values)
                yield return t;
    }

    /// <summary>
    /// Konwertuje przediał wartości w kolekcje numerów
    /// </summary>
    public static IEnumerator<int> GetEnumerator(this Range range) => new foreach_range(range);

    /// <summary>
    /// <inheritdoc cref="GetEnumerator(Range)"/>
    /// </summary>
    public static IEnumerable<int> GetEnumerable(this Range range) => new foreach_range(range);

    /// <summary>
    /// Do zabespieczania elementów od prostego modyfikowania
    /// </summary>
    public static IEnumerable<T> Secure<T>(this IEnumerable<T> enumerable) => new SecuredEnumerable<T>() { values = enumerable };

    /// <summary>
    /// <inheritdoc cref="GetEnumerable(Range)"/>
    /// </summary>
    public struct foreach_range : IEnumerator<int>, IEnumerable<int>
    {
        int index, end;
        public foreach_range(Range range)
        {
            if (range.Start.IsFromEnd)
                throw new NotSupportedException();
            index = range.Start.Value - 1;
            if (range.End.IsFromEnd)
                throw new NotSupportedException();
            end = range.End.Value;
        }
        public bool MoveNext()
        {
            index++;
            return index <= end;
        }
        public IEnumerator<int> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
        public void Reset() { }
        public void Dispose() { }
        public int Current => index;
        object IEnumerator.Current => Current;
    }

    /// <summary>
    /// <inheritdoc cref="Secure{T}(IEnumerable{T})"/>
    /// </summary>
    private struct SecuredEnumerable<T> : IEnumerable<T>
    {
        public IEnumerable<T> values;

        public IEnumerator<T> GetEnumerator() => new Enumerator() { enumerator = values.GetEnumerator() };
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        struct Enumerator : IEnumerator<T>
        {
            public IEnumerator<T> enumerator;

            public T Current => enumerator.Current;
            object IEnumerator.Current => enumerator.Current!;
            public void Dispose() => enumerator.Dispose();
            public bool MoveNext() => enumerator.MoveNext();
            public void Reset() => enumerator.Reset();
        }
    }

    public static IEnumerable<T> Combine<T>(this IEnumerable<IEnumerable<T>> values)
    {
        foreach (var @enum in values)
            foreach (var v in @enum)
                yield return v;
    }
}
