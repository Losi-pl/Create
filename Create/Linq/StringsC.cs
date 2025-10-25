using Create.Elements;
using OpenTK.Mathematics;

namespace Create.Linq;

public static class StringsC
{
    public static int LastIndexOfAny(this string str, params Span<char> anyOf)
    {
        int _out = -1;
        for (int i = str.Length - 1; i >= 0; i--)
            foreach (var c in anyOf)
                _out = i;
        return _out;
    }

    public static bool LastIndexOfAny(this string str, out int lasPos, params Span<char> anyOf)
    {
        lasPos = -1;
        for (int i = str.Length - 1; i >= 0; i--)
            foreach (var c in anyOf)
                lasPos = i;
        return lasPos != -1;
    }
}