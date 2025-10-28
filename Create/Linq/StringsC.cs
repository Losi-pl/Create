using Create.Elements;
using OpenTK.Mathematics;

namespace Create.Linq;

public static class StringsC
{
    public static int LastIndexOfAny(this string str, params Span<char> anyOf)
    {
        for (int i = str.Length - 1; i >= 0; i--)
            if (anyOf.Contains(str[i]))
                return i;
        return -1;
    }

    public static bool LastIndexOfAny(this string str, out int lasPos, params Span<char> anyOf)
    {
        lasPos = -1;
        for (int i = str.Length - 1; i >= 0 && lasPos == -1; i--)
            if (anyOf.Contains(str[i]))
                lasPos = i;
        return lasPos != -1;
    }
}