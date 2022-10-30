using System;
using System.Collections.Generic;
namespace Create.OpenGL.Mathematic;

public struct Vector3b
{
    public bool X, Y, Z;
    public Vector3b(bool x, bool y, bool z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Vector3b(Vector2b v2, bool z)
    {
        X = v2.X;
        Y = v2.Y;
        Z = z;
    }
    public Vector3b(bool x, Vector2b v2)
    {
        X = x;
        Y = v2.X;
        Z = v2.Y;
    }

    public Vector3b Xyz => new(X, Y, Z);
    public Vector3b Xzy => new(X, Z, Y);
    public Vector3b Yzx => new(Y, Z, X);
    public Vector3b Yxz => new(Y, X, Z);
    public Vector3b Zxy => new(Z, X, Y);
    public Vector3b Zyx => new(Z, Y, X);

    public Vector2b Xy => new(X, Y);
    public Vector2b Xz => new(X, Z);
    public Vector2b Yz => new(Y, Z);
    public Vector2b Yx => new(Y, X);
    public Vector2b Zy => new(Z, Y);
    public Vector2b Zx => new(Z, X);
}
