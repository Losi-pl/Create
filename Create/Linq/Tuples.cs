namespace Create;

public static class Tuples
{
    #region Deconstruct
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T)> values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T)> values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T)> values)
    {
        foreach (var value in values)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }
    }
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T)> values)
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
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T, T)> values)
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
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T, T, T)> values)
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
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T, T, T, T)> values)
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
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T, T, T, T, T)> values)
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
    public static IEnumerable<T> Deconstruct<T>(this IEnumerable<(T, T, T, T, T, T, T, T, T, T)> values)
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
    #endregion

    #region GroupInX
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
    #endregion

    #region Any - bool
    public static bool Any(this (bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        if (values.Item6)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        if (values.Item6)
            return true;
        if (values.Item7)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        if (values.Item6)
            return true;
        if (values.Item7)
            return true;
        if (values.Item8)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        if (values.Item6)
            return true;
        if (values.Item7)
            return true;
        if (values.Item8)
            return true;
        if (values.Item9)
            return true;
        return false;
    }
    public static bool Any(this (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool) values)
    {
        if (values.Item1)
            return true;
        if (values.Item2)
            return true;
        if (values.Item3)
            return true;
        if (values.Item4)
            return true;
        if (values.Item5)
            return true;
        if (values.Item6)
            return true;
        if (values.Item7)
            return true;
        if (values.Item8)
            return true;
        if (values.Item9)
            return true;
        if (values.Item10)
            return true;
        return false;
    }
    #endregion
}