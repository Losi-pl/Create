using System.Diagnostics;

namespace Create.OpenGL.Mathematic;

[DebuggerDisplay("{ToString(),nq}")]
public struct Vector2b
{
    public bool X, Y;
    public Vector2b(bool x, bool y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";

    public Vector2b Xy => new(X, Y);
    public Vector2b Yy => new(Y, X);
}
