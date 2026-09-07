using System.Drawing;
using Silk.NET.Maths;

namespace Create.General;

public static class GeneralCasts
{
    extension(Color color)
    {
        public Vector4D<byte> AsVector() => new(color.R, color.G, color.B, color.A);
        public Vector4D<float> AsFloatVector() => new(color.R / (float)byte.MaxValue,
                                                      color.G / (float)byte.MaxValue, 
                                                      color.B / (float)byte.MaxValue, 
                                                   color.A / (float)byte.MaxValue);
    }
}