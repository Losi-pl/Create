namespace Create;

partial class Special
{
    public static IEnumerable<T> Deconstruct<T>(this (T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Item8;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Item8;
            yield return value.Item9;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this (T, T, T, T, T, T, T, T, T, T)[] values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
            yield return value.Item6;
            yield return value.Item7;
            yield return value.Item8;
            yield return value.Item9;
            yield return value.Item10;
        }
    }

    public static IEnumerable<(T, T)> GroupIn2<T>(this IEnumerable<T> enumerable)
    {
        (T, T) val;
        var numera = enumerable.GetEnumerator();
        while(true)
        {
            val = (default!, default!);
            if(move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if(!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T)> GroupIn3<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T)> GroupIn4<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T)> GroupIn5<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T, T)> GroupIn6<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T, T, T)> GroupIn7<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item6))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item7))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T, T, T, T)> GroupIn8<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item6))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item7))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item8))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T, T, T, T, T)> GroupIn9<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item6))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item7))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item8))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item9))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
    public static IEnumerable<(T, T, T, T, T, T, T, T, T, T)> GroupIn10<T>(this IEnumerable<T> enumerable)
    {
        (T, T, T, T, T, T, T, T, T, T) val;
        var numera = enumerable.GetEnumerator();
        while (true)
        {
            val = (default!, default!, default!, default!, default!, default!, default!, default!, default!, default!);
            if (move_next(out val.Item1))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item2))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item3))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item4))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item5))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item6))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item7))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item8))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item9))
            {
                yield return val;
                continue;
            }
            if (move_next(out val.Item10))
            {
                yield return val;
                continue;
            }
            yield return val;
        }

        bool move_next(out T val)
        {
            var v = numera!.MoveNext();
            if (!v)
            {
                val = default!;
                return true;
            }
            val = numera!.Current;
            return !v;
        }
    }
}