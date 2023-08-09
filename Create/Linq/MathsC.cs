using Create.Space;

namespace Create.Linq;

/// <summary>
/// Dodatkowe specjalne motody do obrubki danych
/// </summary>
public static class MathsC
{
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
        if (range.Start.Value > value) return false;
        if (range.End.Value < value) return false;
        return true;
    }
}
