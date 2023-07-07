namespace Create;

partial class Special
{
    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static OpenTK.Mathematics.Vector2 ToOpenGL(this System.Numerics.Vector2 vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector2 ToNumeric(this OpenTK.Mathematics.Vector2 vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector2 ToNumeric(this OpenTK.Mathematics.Vector2i vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static OpenTK.Mathematics.Vector3 ToOpenGL(this System.Numerics.Vector3 vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector3 ToNumeric(this OpenTK.Mathematics.Vector3 vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector3 ToNumeric(this OpenTK.Mathematics.Vector3i vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekładanie wektora z <c>System.Numerics</c> do <c>OpenTK.Mathematics</c>
    /// </summary>
    public static OpenTK.Mathematics.Vector4 ToOpenGL(this System.Numerics.Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector4 ToNumeric(this OpenTK.Mathematics.Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekładanie wektora z <c>OpenTK.Mathematics</c> do <c>System.Numerics</c>
    /// </summary>
    public static System.Numerics.Vector4 ToNumeric(this OpenTK.Mathematics.Vector4i vector) => new(vector.X, vector.Y, vector.Z, vector.W);
}
