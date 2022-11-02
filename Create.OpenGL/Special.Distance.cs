using OpenTK.Mathematics;

namespace Create;

partial class Special
{
    public static float Distance(this Vector2 v1, Vector2 v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }
    public static float Distance(this Vector2i v1, Vector2i v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }
    public static float Distance(this Vector2h v1, Vector2h v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }
    public static double Distance(this Vector2d v1, Vector2d v2)
    {
        var poi = (Math.Abs(v1.X - v2.X), Math.Abs(v1.Y - v2.Y));
        return Math.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }
    public static float Distance(this Vector3 v1, Vector3 v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }
    public static float Distance(this Vector3i v1, Vector3i v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }
    public static float Distance(this Vector3h v1, Vector3h v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }
    public static double Distance(this Vector3d v1, Vector3d v2)
    {
        var poi = (Math.Abs(v1.X - v2.X), Math.Abs(v1.Y - v2.Y), Math.Abs(v1.Z - v2.Z));
        var d = Math.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return Math.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }
}
