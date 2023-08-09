using System.Runtime.InteropServices;

namespace Create.Linq;

/// <summary>
/// Dodatkowe specjalne motody do obrubki danych
/// </summary>
public static class ListsC
{
    /// <summary>
    /// <inheritdoc cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="condition">Test czy klucz spełnia warunki</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<TKey, bool> condition, out TValue value)
        where TKey : notnull => TryGetValue(dictionary, condition, out value, out _);

    /// <summary>
    /// <inheritdoc cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="condition">Test czy klucz spełnia warunki</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<TKey, bool> condition, out TValue value, out TKey key) where TKey : notnull
    {
        foreach (var e in dictionary)
            if (condition(e.Key))
            {
                value = e.Value;
                key = e.Key;
                return true;
            }
        value = default!;
        key = default!;
        return false;
    }

    /// <summary>
    /// <inheritdoc cref="Dictionary{TKey, TValue}.ContainsKey(TKey)"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="condition">Test czy klucz spełnia warunki</param>
    /// <returns></returns>
    public static bool ContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<TKey, bool> condition) where TKey : notnull
    {
        foreach (var e in dictionary)
            if (condition(e.Key))
                return true;
        return false;
    }

    /// <summary>
    /// <inheritdoc cref="List{T}.IndexOf(T)"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition">Warunki oczekiwanego elementu</param>
    /// <returns></returns>
    public static int IndexOf<T>(this List<T> list, Func<T, bool> condition)
    {
        Span<T> span = CollectionsMarshal.AsSpan(list);
        for (int i = 0; i < span.Length; i++)
            if (condition(span[i]))
                return i;
        return -1;
    }

    /// <summary>
    /// Wywołuje metode <paramref name="action"/> dla elementu
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static T InvokeFor<T>(this T element, Action<T> action)
    {
        action(element);
        return element;
    }

    /// <summary>
    /// Dodawanie przedmioty z <paramref name="span"/> do <paramref name="list"/>
    /// </summary>
    /// <typeparam name="T">Typ elementó</typeparam>
    /// <param name="list">Docelowa lista</param>
    /// <param name="span">Span z danymi</param>
    public static void AddRange<T>(this List<T> list, Span<T> span)
    {
        for (int i = 0; i < span.Length; i++)
            list.Add(span[i]);
    }

    /// <summary>
    /// Dodawanie przedmioty z <paramref name="span"/> do <paramref name="list"/>
    /// </summary>
    /// <typeparam name="T">Typ elementó</typeparam>
    /// <param name="list">Docelowa lista</param>
    /// <param name="span">Span z danymi</param>
    public static void AddRange<T>(this List<T> list, ReadOnlySpan<T> span)
    {
        for (int i = 0; i < span.Length; i++)
            list.Add(span[i]);
    }
}
