using System.Diagnostics;
using System.Drawing;
using CommunityToolkit.HighPerformance.Helpers;
using Silk.NET.Maths;

namespace Create.General;

public static class GeneralCasts
{
    public static T Apply<T>(this T element, Action<T> action)
    {
        action(element);
        return element;
    }
    
    extension(Color color)
    {
        public Vector4D<byte> AsVector() => new(color.R, color.G, color.B, color.A);
        public Vector4D<float> AsFloatVector() => new(color.R / (float)byte.MaxValue,
                                                      color.G / (float)byte.MaxValue, 
                                                      color.B / (float)byte.MaxValue, 
                                                   color.A / (float)byte.MaxValue);
    }

    extension(object obj)
    {
        [DebuggerHidden]
        public T ForceUnbox<T>() where T : struct
        {
            if(obj.TryUnbox<T>(out var rez))
                return rez;
            throw new InvalidCastException();
        }
    }
}