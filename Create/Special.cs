using Create.OpenGL;
using Create.Render;
using Create.Space;

namespace Create;

/// <summary>
/// Dodatkowe specjalne motody do obrubki danych
/// </summary>
internal static partial class Special
{
    /// <summary>
    /// Dodawanie przedmioty z <paramref name="span"/> do <paramref name="list"/>
    /// </summary>
    /// <typeparam name="T">Typ elementó</typeparam>
    /// <param name="list">Docelowa lista</param>
    /// <param name="span">Span z danymi</param>
    public static void AddRange<T>(this List<T> list, Span<T> span)
    {
        for(int i = 0; i < span.Length; i++)
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
        foreach(var element in enumerables)
            action(element);
    }
    
    /// <summary>
    /// Łączy Kilka kolekcji w jedną
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerators">Kolekcje przedmiotów</param>
    /// <returns></returns>
    public static IEnumerable<T> Combine<T>(this IEnumerable<IEnumerable<T>> enumerators)
    {
        foreach(var enume in enumerators)
            foreach(var elem in enume)
                yield return elem;
    }
    
    /// <summary>
    /// Dodaje  lub wyciąga instancje konstruktora modelu terenu
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructor">Konstruktor terenu</param>
    /// <returns></returns>
    public static T GetModelMekanizm<T>(this ModelConstructor constructor) where T : ChunkModel =>
        (T)constructor.ModelMekanizm[typeof(T)];
    
    /// <summary>
    /// Wyciąga wrzystkie elementy modelu terenu z konstruktora
    /// </summary>
    /// <param name="model">Konstruktor terenu</param>
    /// <returns></returns>
    public static IEnumerable<Mesh> AllModelParts(this ChunkConstructor.FinischedChunkModel model)
    {
        foreach (var quard in model.ModelParts)
            foreach (var mesh in quard)
                yield return mesh.Value;
    }
    
    /// <summary>
    /// Odległość między dwoma Chunkami
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float Distance(this ChunkPoz v1, ChunkPoz v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Z - v2.Z));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }

    /// <summary>
    /// Sprawdza czy wartość <paramref name="value"/> jest w przedziale
    /// </summary>
    /// <param name="range"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool Contains(this Range range, int value)
    {
        if(range.Start.Value > value) return false;
        if (range.End.Value < value) return false;
        return true;
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

        foreach(var e in enume)
        {
            if (index == 0)
                return e;
            index--;
        }
        return default!;
    }

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
        foreach(var e in dictionary)
            if(condition(e.Key))
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
    public static bool ContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<TKey, bool> condition) where TKey: notnull
    {
        foreach (var e in dictionary)
            if (condition(e.Key))
                return true;
        return false;
    }
}
