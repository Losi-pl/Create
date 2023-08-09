using Create.Render;

namespace Create.Linq;

/// <summary>
/// Dodatkowe specjalne motody do obrubki danych
/// </summary>
public static class Renders
{
    /// <summary>
    /// Dodaje  lub wyciąga instancje konstruktora modelu terenu
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructor">Konstruktor terenu</param>
    /// <returns></returns>
    public static T GetModelMekanizm<T>(this ModelConstructor constructor) where T : ChunkModel =>
        (T)constructor.ModelMekanizm[typeof(T)];
}
