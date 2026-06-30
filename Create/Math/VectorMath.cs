using System.Numerics;
using Silk.NET.Maths;

namespace Create.Math;

public static class VectorMath
{
    extension(Vector2)
    {
        public static Vector2 operator +(Vector2 v1, Vector2D<int> v2) => new(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator +(Vector2 v1, Vector2D<float> v2) => new(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2D<int> ToInt(Vector2 v) => new((int)v.X, (int)v.Y);
    }
}