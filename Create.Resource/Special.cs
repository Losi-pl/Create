namespace Create.Resource;

internal static class Special
{
    /// <summary>
    /// Odpowiednik <see cref="Stream.Read(byte[], int, int)"/> obsługujący wastości więkrze niż <see cref="int.MaxValue"/>
    /// </summary>
    public static long ReadLong(this Stream stream, byte[] bytes) => ReadLong(stream, bytes, 0, bytes.LongLength);

    /// <summary>
    /// <inheritdoc cref="ReadLong(Stream, byte[])"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static long ReadLong(this Stream stream, byte[] bytes, long offset, long count)
    {
        if (count < 0 || offset < 0)
            throw new ArgumentOutOfRangeException();
        if (bytes.LongLength - offset < count)
            count = bytes.LongLength - offset;
        if (count == 0)
            return 0;
        if(count > int.MaxValue)
        {
            byte[] bytes1 = new byte[count];
            for (long i = 0; i < count; i++)
                bytes[i + offset] = bytes1[i];
            return stream.Read(bytes1, 0, (int)count);
        }
        else
        {
            long wynik = 0;
            byte[] bytes2 = new byte[int.MaxValue];
            int repet = (int)(count / int.MaxValue);
            for(int i = 0; i < repet; i++)
            {
                int ee = stream.Read(bytes2, 0, int.MaxValue);
                long start_point = i * int.MaxValue;
                for(int ii = 0; ii < int.MaxValue; ii++)
                    bytes[start_point + ii] = bytes2[ii];
                if (ee == 0)
                    return 0;
                else
                    wynik += ee;
            }
            if(count % int.MaxValue > 0)
            {
                int rest_count = (int)(count % int.MaxValue);
                byte[] bytes3 = new byte[rest_count];
                int add = stream.Read(bytes3, 0, rest_count);
                wynik += add;
                long offset_ = count - rest_count;
                for(int i = 0; i< rest_count;i++)
                    bytes[offset_ + i] = bytes3[i];
            }
            return wynik;
        }
    }


    /// <summary>
    /// Odpowiednik <see cref="Stream.Write(byte[], int, int)"/> obsługujący wastości więkrze niż <see cref="int.MaxValue"/>
    /// </summary>
    public static void WriteLong(this Stream stream, byte[] bytes) => WriteLong(stream, bytes, 0, bytes.LongLength);

    /// <summary>
    /// <inheritdoc cref="WriteLong(Stream, byte[])"/>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void WriteLong(this Stream stream, byte[] bytes, long offset, long count)
    {
        if (count < 0 || offset < 0)
            throw new ArgumentOutOfRangeException();
        if (bytes.LongLength < count)
            count = bytes.LongLength;
        if (count == 0)
            return;
        if(count > int.MaxValue)
        {
            var sec_array = new byte[int.MaxValue];
            for(int s = 0; s < count / int.MaxValue; s++)
            {
                for (int i = 0; i < int.MaxValue; i++)
                    sec_array[i] = bytes[offset + ((long)s * int.MaxValue) + i];
                stream.Write(sec_array, 0, int.MaxValue);
            }
            if(count % int.MaxValue > 0)
            {
                long arr_in_off = bytes.LongLength - offset - (count % int.MaxValue);
                sec_array = new byte[count % int.MaxValue];
                for (int i = 0; i < int.MaxValue; i++)
                    sec_array[i] = bytes[arr_in_off + i];
                stream.Write(sec_array, 0, (int)(count % int.MaxValue));
            }
        }
        else
        {
            var small_arr = new byte[count];
            for(long i = 0; i < count; i++)
                small_arr[i] = bytes[offset + i];
            stream.Write(small_arr);
        }
    }
    
    /// <summary>
    /// Kopiuje jeden <see cref="Stream"/> do drugiego
    /// </summary>
    /// <param name="dest"></param>
    /// <param name="src"></param>
    public static void WriteStream(this Stream dest, Stream src)
    {
        if (dest.Length - dest.Position < src.Length)
            return;
        Span<byte> buffer = stackalloc byte[2048];
        for(int i = 0; i < src.Length / buffer.Length; i++)
        {
            src.Read(buffer);
            dest.Write(buffer);
        }
        if(src.Length % buffer.Length > 0)
        {
            buffer = buffer[0..(int)(src.Length % buffer.Length)];
            src.Read(buffer);
            dest.Write(buffer);
        }
    }

    /// <summary>
    /// Wytnij pierwszy ciąg <paramref name="remove"/> z całaści <paramref name="main"/>
    /// </summary>
    /// <param name="main"></param>
    /// <param name="remove"></param>
    /// <returns></returns>
    public static string RemoveFirstSubString(this string main, string remove)
    {
        int index = main.IndexOf(remove);
        return (index < 0)
            ? main
            : main.Remove(index, remove.Length);
    }

    /// <summary>
    /// Połącz kilka kolekcji w jedną
    /// </summary>
    public static IEnumerable<T> MargEnumerables<T>(this IEnumerable<IEnumerable<T>> values)
    {
        foreach(var en in values)
            foreach(var e in en)
                yield return e;
    }
    
    /// <summary>
    /// Przekonwertuj obiekt za pobocą <paramref name="func"/>
    /// </summary>
    public static IEnumerable<TOut> Cast<TIn, TOut>(this IEnumerable<TIn> values, Func<TIn, TOut> func)
    {
        foreach(var v in values)
            yield return func(v);
    }

    /// <summary>
    /// Czy na liście znajduje się element <paramref name="element"/>
    /// <para>W sparawdzaniu pomiń <paramref name="exclude"/></para>
    /// </summary>
    /// <param name="exclude">Numer obiektu który ma być pominięty w sprawdzaniu</param>
    /// <returns></returns>
    public static bool IsConteinedEx<T>(this List<T> list, T element, int exclude) => IsConteinedEx(list, el => el?.Equals(element) ?? false, exclude);
    
    /// <summary>
    /// Czy na liście znajduje się element spełniający warunki <paramref name="condition"/>
    /// <para>W sparawdzaniu pomiń <paramref name="exclude"/></para>
    /// </summary>
    /// <param name="exclude">Numer obiektu który ma być pominięty w sprawdzaniu</param>
    /// <returns></returns>
    public static bool IsConteinedEx<T>(this List<T> list, Func<T, bool> condition, int exclude)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == exclude)
                continue;
            if (condition(list[i]))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Znajdź jeden ciąg w drugim
    /// <para>Szuka od końca</para>
    /// </summary>
    /// <param name="text">Ciąg główny</param>
    /// <param name="subString">Ciąg do znalezienia</param>
    /// <param name="notFound">Jeśli ciąg nie został znajeziony</param>
    public static int FindFromEnd(this string text, string subString, int notFound = -1)
    {
        for(int i = text.Length - subString.Length - 1; i >= 0; i--)
        {
            bool good = true;
            for(int I = 0; I < subString.Length && good; I++)
                if (text[i + I] != subString[I])
                    good = false;
            if (good)
                return i;
        }
        return notFound;
    }

    /// <summary>
    /// Znajdź jeden z ciągów w głównym ciągu
    /// <para>Szuka od końca</para>
    /// </summary>
    /// <param name="text">Ciąg główny</param>
    /// <param name="find">Jeden z poszukiwanych ciągów</param>
    /// <param name="notFound">Jeśli ciąg nie został znajeziony</param>
    public static int FindFromEnd(this string text, string[] find, int notFound = -1)
    {
        int? last = null;

        for (int i = 0; i < find.Length; i++)
        {
            int wyn = text.FindFromEnd(find[i], -2);
            if (wyn != -2)
                continue;
            if (last.HasValue)
            {
                if (last < wyn)
                    last = wyn;
            }
            else
                last = wyn;
        }

        return last ?? notFound;
    }

    /// <summary>
    /// Znajdź litere w ciągu
    /// <para>Szuka od końca</para>
    /// </summary>
    /// <param name="text">Przeszukiwany ciąg</param>
    /// <param name="find">Ciąg do znalezienia</param>
    /// <param name="notFound">Jeśli ciąg nie został znajeziony</param>
    public static int FindFromEnd(this string text, char find, int notFound = -1)
    {
        for (int i = text.Length - 1; i >= 0; i--)
            if (text[i] == find)
                return i;
        return notFound;
    }

    /// <summary>
    /// Znajdź jedną z liter w ciągu
    /// <para>Szuka od końca</para>
    /// </summary>
    /// <param name="text">Przeszukiwany ciąg</param>
    /// <param name="find">Litery do znalezienia</param>
    /// <param name="notFound">Jeśli ciąg nie został znajeziony</param>
    public static int FindFromEnd(this string text, char[] find, int notFound = -1)
    {
        int? last = null;

        for(int i = 0; i < find.Length; i++)
        {
            int wyn = text.FindFromEnd(find[i], -2);
            if (wyn != -2)
                continue;
            if (last.HasValue)
            {
                if (last < wyn)
                    last = wyn;
            }
            else
                last = wyn;
        }

        return last ?? notFound;
    }
}
