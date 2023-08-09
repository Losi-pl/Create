namespace Create.Linq;

/// <summary>
/// Dodatkowe specjalne motody do obrubki danych
/// </summary>
public static class EnumerablesC
{
    /// <summary>
    /// Czy jakiś przedmiot spełniający warunki <paramref name="condition"/> jest w kolekcji <paramref name="list"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">Kolekcja elementów</param>
    /// <param name="condition">Warunki</param>
    /// <returns></returns>
    public static bool FindAny<T>(this IEnumerable<T> list, Func<T, bool> condition)
    {
        foreach (var item in list)
            if (condition(item))
                return true;
        return false;
    }

    /// <summary>
    /// Wykonywanie metody <paramref name="action"/> dla karzdego przedmiotu w kolekcji <paramref name="enumerables"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerables">Kolekcja elementów</param>
    /// <param name="action">Operacja do wykonania</param>
    public static void ForEvery<T>(this IEnumerable<T> enumerables, Action<T> action)
    {
        foreach (var element in enumerables)
            action(element);
    }

    /// <summary>
    /// Pobiera element z <paramref name="enume"/> o numerze <paramref name="index"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enume"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T Index<T>(this IEnumerable<T> enume, int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be grater than 0");

        foreach (var e in enume)
        {
            if (index == 0)
                return e;
            index--;
        }
        return default!;
    }

    /// <summary>
    /// Zastępuje wzorzec wewnątrz ciągu
    /// </summary>
    /// <param name="replacement">Wzorzec do zastąpienia</param>
    /// <param name="pattern">Wzorzec którym zostanie zastąpione</param>
    public static IEnumerable<char> Pattern(this IEnumerable<char> enums, string replacement, string pattern)
    {
        int progress = 0;

        foreach (char c in enums)
        {
            if (c != pattern[progress])
            {
                for (int i = 0; i < progress; i++)
                    yield return pattern[i];
                yield return c;
                progress = 0;
            }
            else
            {
                progress++;
                if (!(pattern.Length > progress))
                {
                    foreach (var nc in replacement)
                        yield return nc;
                    progress = 0;
                }
            }
        }
    }

    /// <summary>
    /// Zastępuje elementy w ciągu które spełniają warunki <paramref name="condition"/> wartością w <paramref name="new"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition">Warunki do zastapiania elementów w ciągu</param>
    /// <param name="new">Wartość do którą zastąpi się wartości spełniające warunki <paramref name="condition"/></param>
    public static IEnumerable<T> Replace<T>(this IEnumerable<T> enums, Func<T, bool> condition, T @new)
    {
        ArgumentNullException.ThrowIfNull(condition, nameof(condition));

        foreach (var t in enums)
            if (condition(t))
                yield return @new;
            else
                yield return t;
    }

    /// <summary>
    /// Dodaje wrzystkim elementom w ciągu numery
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static IEnumerable<(T item, int index)> Numerate<T>(this IEnumerable<T> values)
    {
        int i = 0;
        foreach (var e in values)
            yield return (e, i++);
    }
}
