namespace Create.OpenGL.Mathematic;

public struct Vector2b
{
    public bool X, Y;
    public Vector2b(bool x, bool y)
    {
        X = x;
        Y = y;
    }

    public Vector2b Xy => new(X, Y);
    public Vector2b Yy => new(Y, X);
}
