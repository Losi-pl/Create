using Create.OpenGL.Mathematic;
using OpenTK.Mathematics;

namespace Create.Linq;

public static class Vectors
{
    /// <summary>
    /// Przekłada <see cref="Vector2"/> na krotke
    /// </summary>
    public static (float X, float Y) ToTumple(this Vector2 vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3"/> na krotke
    /// </summary>
    public static (float X, float Y, float Z) ToTumple(this Vector3 vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4"/> na krotke
    /// </summary>
    public static (float X, float Y, float Z, float W) ToTumple(this Vector4 vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2"/>
    /// </summary>
    public static Vector2 ToVector(this (float X, float Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3"/>
    /// </summary>
    public static Vector3 ToVector(this (float X, float Y, float Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4"/>
    /// </summary>
    public static Vector4 ToVector(this (float X, float Y, float Z, float W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada <see cref="Vector2i"/> na krotke
    /// </summary>
    public static (int X, int Y) ToTumple(this Vector2i vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3i"/> na krotke
    /// </summary>
    public static (int X, int Y, int Z) ToTumple(this Vector3i vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4i"/> na krotke
    /// </summary>
    public static (int X, int Y, int Z, int W) ToTumple(this Vector4i vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2i"/>
    /// </summary>
    public static Vector2i ToVector(this (int X, int Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3i"/>
    /// </summary>
    public static Vector3i ToVector(this (int X, int Y, int Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4i"/>
    /// </summary>
    public static Vector4i ToVector(this (int X, int Y, int Z, int W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada <see cref="Vector2d"/> na krotke
    /// </summary>
    public static (double X, double Y) ToTumple(this Vector2d vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3d"/> na krotke
    /// </summary>
    public static (double X, double Y, double Z) ToTumple(this Vector3d vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4d"/> na krotke
    /// </summary>
    public static (double X, double Y, double Z, double W) ToTumple(this Vector4d vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2d"/>
    /// </summary>
    public static Vector2d ToVector(this (double X, double Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3d"/>
    /// </summary>
    public static Vector3d ToVector(this (double X, double Y, double Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4d"/>
    /// </summary>
    public static Vector4d ToVector(this (double X, double Y, double Z, double W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada <see cref="Vector2b"/> na krotke
    /// </summary>
    public static (bool X, bool Y) ToTumple(this Vector2b vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3b"/> na krotke
    /// </summary>
    public static (bool X, bool Y, bool Z) ToTumple(this Vector3b vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4b"/> na krotke
    /// </summary>
    public static (bool X, bool Y, bool Z, bool W) ToTumple(this Vector4b vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2b"/>
    /// </summary>
    public static Vector2b ToVector(this (bool X, bool Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3b"/>
    /// </summary>
    public static Vector3b ToVector(this (bool X, bool Y, bool Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4b"/>
    /// </summary>
    public static Vector4b ToVector(this (bool X, bool Y, bool Z, bool W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);


    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static Vector2 ToOpenGL(this System.Numerics.Vector2 vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector2 ToNumeric(this Vector2 vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector2 ToNumeric(this Vector2i vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static Vector3 ToOpenGL(this System.Numerics.Vector3 vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector3 ToNumeric(this Vector3 vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector3 ToNumeric(this Vector3i vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static Vector4 ToOpenGL(this System.Numerics.Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector4 ToNumeric(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector4 ToNumeric(this Vector4i vector) => new(vector.X, vector.Y, vector.Z, vector.W);


    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector2 v1, Vector2 v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector2i v1, Vector2i v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector2h v1, Vector2h v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y));
        return MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static double Distance(this Vector2d v1, Vector2d v2)
    {
        var poi = (Math.Abs(v1.X - v2.X), Math.Abs(v1.Y - v2.Y));
        return Math.Sqrt((poi.Item1 * poi.Item1) + (poi.Item2 * poi.Item2));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector3 v1, Vector3 v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector3i v1, Vector3i v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static float Distance(this Vector3h v1, Vector3h v2)
    {
        var poi = (MathF.Abs(v1.X - v2.X), MathF.Abs(v1.Y - v2.Y), MathF.Abs(v1.Z - v2.Z));
        var d = MathF.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return MathF.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }

    /// <summary>
    /// Dystans między punktem <paramref name="v1"/> a punktem <paramref name="v2"/>
    /// </summary>
    public static double Distance(this Vector3d v1, Vector3d v2)
    {
        var poi = (Math.Abs(v1.X - v2.X), Math.Abs(v1.Y - v2.Y), Math.Abs(v1.Z - v2.Z));
        var d = Math.Sqrt((poi.Item1 * poi.Item1) + (poi.Item3 * poi.Item3));
        return Math.Sqrt((poi.Item2 * poi.Item2) + (d * d));
    }
}
