using Create.Space;
using OpenTK.Mathematics;

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

    public static (int x, int y, int z) Add(this (int x, int y, int z) a, Vector3i b)
    {
        return (a.x + b.X, a.y + b.Y, a.z + b.Z);
    }

    public static float DeNaN(this float v1) => v1 == float.NaN ? 0f : v1;
    public static Vector2 DeNaN(this Vector2 v2) => new(DeNaN(v2.X), DeNaN(v2.Y));
    public static Vector3 DeNaN(this Vector3 v3) => new(DeNaN(v3.X), DeNaN(v3.Y), DeNaN(v3.Z));
    public static Vector4 DeNaN(this Vector4 v4) => new(DeNaN(v4.X), DeNaN(v4.Y), DeNaN(v4.Z), DeNaN(v4.W));
}
