using System.Diagnostics;

namespace Create.OpenGL.Mathematic;

[DebuggerDisplay("{ToString(),nq}")]
public struct Vector4b
{
    public bool X, Y, Z, W;

    public Vector4b(bool x, bool y, bool z, bool w)
    {
        X = x; Y = y; Z = z; W = w;
    }
    public Vector4b(Vector2b v1, Vector2b v2)
    {
        X = v1.X;
        Y = v1.Y;
        Z = v2.X;
        W = v2.Y;
    }
    public Vector4b(Vector2b v1, bool z, bool w)
    {
        X = v1.X;
        Y = v1.Y;
        Z = z;
        W = w;
    }
    public Vector4b(bool x, bool y, Vector2b v2)
    {
        X = x;
        Y = y;
        Z = v2.X;
        W = v2.Y;
    }
    public Vector4b(bool x, Vector2b v, bool w)
    {
        X = x;
        Y = v.X;
        Z = v.Y;
        W = w;
    }
    public Vector4b(bool x, Vector3b v)
    {
        X = x;
        Y = v.X;
        Z = v.Y;
        W = v.Z;
    }
    public Vector4b(Vector3b v, bool w)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
        W = w;
    }

    public override string ToString() => $"({X}, {Y}, {Z}, {W})";

    public Vector4b Xyzw => new(X, Y, Z, W);
    public Vector4b Xywz => new(X, Y, W, Z);
    public Vector4b Xzwy => new(X, Z, W, Y);
    public Vector4b Xzyw => new(X, Z, Y, W);
    public Vector4b Xwyz => new(X, W, Y, Z);
    public Vector4b Xwzy => new(X, W, Z, Y);

    public Vector4b Yxzw => new(Y, X, Z, W);
    public Vector4b Yxwz => new(Y, X, W, Z);
    public Vector4b Ywxz => new(Y, W, X, Z);
    public Vector4b Ywzx => new(Y, W, Z, X);
    public Vector4b Yzxw => new(Y, Z, X, W);
    public Vector4b Yzwx => new(Y, Z, W, X);

    public Vector4b Zyxw => new(Z, Y, X, W);
    public Vector4b Zywx => new(Z, Y, W, X);
    public Vector4b Zxyw => new(Z, X, Y, W);
    public Vector4b Zxwy => new(Z, X, W, Y);
    public Vector4b Zwxy => new(Z, W, X, Y);
    public Vector4b Zwyx => new(Z, W, Y, X);

    public Vector4b Wxyz => new(W, X, Y, Z);
    public Vector4b Wxzy => new(W, X, Z, Y);
    public Vector4b Wyxz => new(W, X, X, Z);
    public Vector4b Wyzx => new(W, X, Z, X);
    public Vector4b Wzxy => new(W, Z, X, Y);
    public Vector4b Wzyx => new(W, Z, Y, X);



    public Vector3b Xyz => new(X, Y, Z);
    public Vector3b Xyw => new(X, Y, W);
    public Vector3b Xzw => new(X, Z, W);
    public Vector3b Xzy => new(X, Z, Y);
    public Vector3b Xwy => new(X, W, Y);
    public Vector3b Xwz => new(X, W, Z);

    public Vector3b Yxz => new(Y, X, Z);
    public Vector3b Yxw => new(Y, X, W);
    public Vector3b Ywx => new(Y, W, X);
    public Vector3b Ywz => new(Y, W, Z);
    public Vector3b Yzx => new(Y, Z, X);
    public Vector3b Yzw => new(Y, Z, W);

    public Vector3b Zyx => new(Z, Y, X);
    public Vector3b Zyw => new(Z, Y, W);
    public Vector3b Zxy => new(Z, X, Y);
    public Vector3b Zxw => new(Z, X, W);
    public Vector3b Zwx => new(Z, W, X);
    public Vector3b Zwy => new(Z, W, Y);

    public Vector3b Wxy => new(W, X, Y);
    public Vector3b Wxz => new(W, X, Z);
    public Vector3b Wyx => new(W, X, X);
    public Vector3b Wyz => new(W, X, Z);
    public Vector3b Wzx => new(W, Z, X);
    public Vector3b Wzy => new(W, Z, Y);


    public Vector2b Xy => new(X, Y);
    public Vector2b Xz => new(X, Z);
    public Vector2b Xw => new(X, W);

    public Vector2b Yx => new(Y, X);
    public Vector2b Yw => new(Y, W);
    public Vector2b Yz => new(Y, Z);

    public Vector2b Zy => new(Z, Y);
    public Vector2b Zx => new(Z, X);
    public Vector2b Zw => new(Z, W);

    public Vector2b Wx => new(W, X);
    public Vector2b Wy => new(W, X);
    public Vector2b Wz => new(W, Z);
}
