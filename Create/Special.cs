using Create.OpenGL;
using Create.Render;

namespace Create;

internal static partial class Special
{
    public static void AddRange<T>(this List<T> list, Span<T> span)
    {
        for(int i = 0; i < span.Length; i++)
            list.Add(span[i]);
    }
    public static void AddRange<T>(this List<T> list, ReadOnlySpan<T> span)
    {
        for (int i = 0; i < span.Length; i++)
            list.Add(span[i]);
    }
    public static bool FindAny<T>(this IEnumerable<T> list, Func<T, bool> condition)
    {
        foreach (var item in list)
            if (condition(item))
                return true;
        return false;
    }
    public static void ForEvery<T>(this IEnumerable<T> enumerables, Action<T> action)
    {
        foreach(var element in enumerables)
            action(element);
    }
    public static IEnumerable<T> Combine<T>(this IEnumerable<IEnumerable<T>> enumerators)
    {
        foreach(var enume in enumerators)
            foreach(var elem in enume)
                yield return elem;
    }
    public static T GetModelMekanizm<T>(this ModelConstructor constructor) where T : ChunkModel =>
        (T)constructor.ModelMekanizm[typeof(T)];
    public static IEnumerable<Mesh> AllModelParts(this ChunkConstructor.FinischedChunkModel model)
    {
        foreach (var quard in model.ModelParts)
            foreach (var mesh in quard)
                yield return mesh.Value;
    }
}
