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
}
