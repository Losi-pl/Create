using Create.OpenGL.Mathematic;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace Create.Linq;

public static class Casts
{
    /// <summary>
    /// Przekłada <see cref="ActiveAttribType"/> z OpenGL na <see cref="Type"/> z C#
    /// </summary>
    internal static Type? GetCSharpType(this ActiveAttribType type) => type switch
    {
        ActiveAttribType.None => null,
        //Int
        ActiveAttribType.Int => typeof(int),
        ActiveAttribType.IntVec2 => typeof(Vector2i),
        ActiveAttribType.IntVec3 => typeof(Vector3i),
        ActiveAttribType.IntVec4 => typeof(Vector4i),
        //Float
        ActiveAttribType.Float => typeof(float),
        ActiveAttribType.FloatVec2 => typeof(Vector2),
        ActiveAttribType.FloatVec3 => typeof(Vector3),
        ActiveAttribType.FloatVec4 => typeof(Vector4),
        ActiveAttribType.FloatMat2 => typeof(Matrix2),
        ActiveAttribType.FloatMat2x3 => typeof(Matrix2x3),
        ActiveAttribType.FloatMat2x4 => typeof(Matrix2x4),
        ActiveAttribType.FloatMat3x2 => typeof(Matrix3x2),
        ActiveAttribType.FloatMat3 => typeof(Matrix3),
        ActiveAttribType.FloatMat3x4 => typeof(Matrix3x4),
        ActiveAttribType.FloatMat4x2 => typeof(Matrix4x2),
        ActiveAttribType.FloatMat4x3 => typeof(Matrix4x3),
        ActiveAttribType.FloatMat4 => typeof(Matrix4),
        //Double
        ActiveAttribType.Double => typeof(double),
        ActiveAttribType.DoubleVec2 => typeof(Vector2d),
        ActiveAttribType.DoubleVec3 => typeof(Vector3d),
        ActiveAttribType.DoubleVec4 => typeof(Vector4d),
        ActiveAttribType.DoubleMat2 => typeof(Matrix2d),
        ActiveAttribType.DoubleMat2x3 => typeof(Matrix2x3d),
        ActiveAttribType.DoubleMat2x4 => typeof(Matrix2x4d),
        ActiveAttribType.DoubleMat3x2 => typeof(Matrix3x2d),
        ActiveAttribType.DoubleMat3 => typeof(Matrix3d),
        ActiveAttribType.DoubleMat3x4 => typeof(Matrix3x4d),
        ActiveAttribType.DoubleMat4x2 => typeof(Matrix4x2d),
        ActiveAttribType.DoubleMat4x3 => typeof(Matrix4x3d),
        ActiveAttribType.DoubleMat4 => typeof(Matrix4d),
        //unsigned
        ActiveAttribType.UnsignedInt => typeof(int),
        ActiveAttribType.UnsignedIntVec2 => typeof(Vector2),
        ActiveAttribType.UnsignedIntVec3 => typeof(Vector3),
        ActiveAttribType.UnsignedIntVec4 => typeof(Vector4),

        _ => throw new("Not valid enume")
    };

    /// <summary>
    /// Przekłada <see cref="ActiveUniformType"/> z OpenGL na <see cref="Type"/> z C#
    /// </summary>
    internal static Type GetCSharpType(this ActiveUniformType type) => type switch
    {
        //Int
        ActiveUniformType.Int => typeof(int),
        ActiveUniformType.IntVec2 => typeof(Vector2i),
        ActiveUniformType.IntVec3 => typeof(Vector3i),
        ActiveUniformType.IntVec4 => typeof(Vector4i),
        //Float
        ActiveUniformType.Float => typeof(float),
        ActiveUniformType.FloatVec2 => typeof(Vector2),
        ActiveUniformType.FloatVec3 => typeof(Vector3),
        ActiveUniformType.FloatVec4 => typeof(Vector4),
        ActiveUniformType.FloatMat2 => typeof(Matrix2),
        ActiveUniformType.FloatMat2x3 => typeof(Matrix2x3),
        ActiveUniformType.FloatMat2x4 => typeof(Matrix2x4),
        ActiveUniformType.FloatMat3x2 => typeof(Matrix3x2),
        ActiveUniformType.FloatMat3 => typeof(Matrix3),
        ActiveUniformType.FloatMat3x4 => typeof(Matrix3x4),
        ActiveUniformType.FloatMat4x2 => typeof(Matrix2),
        ActiveUniformType.FloatMat4x3 => typeof(Matrix2x3),
        ActiveUniformType.FloatMat4 => typeof(Matrix2x4),
        //Double
        ActiveUniformType.Double => typeof(double),
        ActiveUniformType.DoubleVec2 => typeof(Vector2d),
        ActiveUniformType.DoubleVec3 => typeof(Vector3d),
        ActiveUniformType.DoubleVec4 => typeof(Vector4d),
        //unsigned
        ActiveUniformType.UnsignedInt => typeof(int),
        ActiveUniformType.UnsignedIntVec2 => typeof(Vector2),
        ActiveUniformType.UnsignedIntVec3 => typeof(Vector3),
        ActiveUniformType.UnsignedIntVec4 => typeof(Vector4),

        ActiveUniformType.Bool => typeof(bool),
        ActiveUniformType.BoolVec2 => typeof(Vector2b),
        ActiveUniformType.BoolVec3 => typeof(Vector3b),
        ActiveUniformType.BoolVec4 => typeof(Vector4b),

        ActiveUniformType.Sampler2D => typeof(Texture2D),
        ActiveUniformType.UnsignedIntSampler2D => typeof(Texture2D),
        ActiveUniformType.Sampler2DArray => typeof(Texture2DArray),
        ActiveUniformType.UnsignedIntSampler2DArray => typeof(Texture2DArray),

        _ => typeof(object)
    };

    /// <summary>
    /// Ile bajtów w pamięci karty graficznej zajmuje typ <paramref name="type"/>
    /// </summary>
    internal static int ElementByteSize(this ActiveAttribType type) => type switch
    {
        ActiveAttribType.None => 0,
        //Int
        ActiveAttribType.Int => sizeof(int),
        ActiveAttribType.IntVec2 => sizeof(int) * 2,
        ActiveAttribType.IntVec3 => sizeof(int) * 3,
        ActiveAttribType.IntVec4 => sizeof(int) * 4,
        //Float
        ActiveAttribType.Float => sizeof(float),
        ActiveAttribType.FloatVec2 => sizeof(float) * 2,
        ActiveAttribType.FloatVec3 => sizeof(float) * 3,
        ActiveAttribType.FloatVec4 => sizeof(float) * 4,
        ActiveAttribType.FloatMat2 => sizeof(float) * (2 * 2),
        ActiveAttribType.FloatMat2x3 => sizeof(float) * (2 * 3),
        ActiveAttribType.FloatMat2x4 => sizeof(float) * (2 * 4),
        ActiveAttribType.FloatMat3x2 => sizeof(float) * (3 * 2),
        ActiveAttribType.FloatMat3 => sizeof(float) * (3 * 3),
        ActiveAttribType.FloatMat3x4 => sizeof(float) * (3 * 4),
        ActiveAttribType.FloatMat4x2 => sizeof(float) * (4 * 2),
        ActiveAttribType.FloatMat4x3 => sizeof(float) * (4 * 3),
        ActiveAttribType.FloatMat4 => sizeof(float) * (4 * 4),
        //Double
        ActiveAttribType.Double => sizeof(double),
        ActiveAttribType.DoubleVec2 => sizeof(double) * 2,
        ActiveAttribType.DoubleVec3 => sizeof(double) * 3,
        ActiveAttribType.DoubleVec4 => sizeof(double) * 4,
        ActiveAttribType.DoubleMat2 => sizeof(double) * (2 * 2),
        ActiveAttribType.DoubleMat2x3 => sizeof(double) * (2 * 3),
        ActiveAttribType.DoubleMat2x4 => sizeof(double) * (2 * 4),
        ActiveAttribType.DoubleMat3x2 => sizeof(double) * (3 * 2),
        ActiveAttribType.DoubleMat3 => sizeof(double) * (3 * 3),
        ActiveAttribType.DoubleMat3x4 => sizeof(double) * (3 * 4),
        ActiveAttribType.DoubleMat4x2 => sizeof(double) * (4 * 2),
        ActiveAttribType.DoubleMat4x3 => sizeof(double) * (4 * 3),
        ActiveAttribType.DoubleMat4 => sizeof(double) * (4 * 4),
        //unsigned
        ActiveAttribType.UnsignedInt => sizeof(int),
        ActiveAttribType.UnsignedIntVec2 => sizeof(int) * 2,
        ActiveAttribType.UnsignedIntVec3 => sizeof(int) * 3,
        ActiveAttribType.UnsignedIntVec4 => sizeof(int) * 4,


        _ => throw new("Ungnown")
    };

    /// <summary>
    /// Przekłada z jakiego typu informacji zbudowane są wyrzsze typy zmiennych i ile zajmują one w pamięci karty graficznej
    /// </summary>
    internal static (int values, VertexAttribPointerType type) ValueBindData(this ActiveAttribType value) => value switch
    {
        ActiveAttribType.None => (0, VertexAttribPointerType.Float),
        //Int
        ActiveAttribType.Int => (1, VertexAttribPointerType.Float),
        ActiveAttribType.IntVec2 => (2, VertexAttribPointerType.Float),
        ActiveAttribType.IntVec3 => (3, VertexAttribPointerType.Float),
        ActiveAttribType.IntVec4 => (4, VertexAttribPointerType.Float),
        //Float
        ActiveAttribType.Float => (1, VertexAttribPointerType.Float),
        ActiveAttribType.FloatVec2 => (2, VertexAttribPointerType.Float),
        ActiveAttribType.FloatVec3 => (3, VertexAttribPointerType.Float),
        ActiveAttribType.FloatVec4 => (4, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat2 => (4, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat2x3 => (6, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat2x4 => (8, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat3x2 => (6, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat3 => (9, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat3x4 => (12, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat4x2 => (8, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat4x3 => (12, VertexAttribPointerType.Float),
        ActiveAttribType.FloatMat4 => (16, VertexAttribPointerType.Float),
        //Double
        ActiveAttribType.Double => (1, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleVec2 => (2, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleVec3 => (3, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleVec4 => (4, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat2 => (4, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat2x3 => (6, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat2x4 => (8, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat3x2 => (6, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat3 => (9, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat3x4 => (12, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat4x2 => (8, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat4x3 => (12, VertexAttribPointerType.Double),
        ActiveAttribType.DoubleMat4 => (16, VertexAttribPointerType.Double),
        //unsigned
        ActiveAttribType.UnsignedInt => (1, VertexAttribPointerType.UnsignedInt),
        ActiveAttribType.UnsignedIntVec2 => (2, VertexAttribPointerType.UnsignedInt),
        ActiveAttribType.UnsignedIntVec3 => (3, VertexAttribPointerType.UnsignedInt),
        ActiveAttribType.UnsignedIntVec4 => (4, VertexAttribPointerType.UnsignedInt),

        _ => throw new("Ungnown")
    };

    /// <summary>
    /// Konwertuje tablice kolorów w obraz
    /// </summary>
    public static Image<T> ToImage<T>(this T[,] array) where T : unmanaged, IPixel<T>
    {
        var size = array.GetSize();
        var image = new Image<T>(size.dim0, size.dim1);
        for (int x = 0; x < size.dim0; x++)
            for (int y = 0; y < size.dim1; y++)
                image[x, y] = array[x, y];
        return image;
    }

    /// <summary>
    /// Konwertowanie jednej wartości w inną za pomocą <paramref name="func"/>
    /// </summary>
    public static TOut Cast<TIn, TOut>(this TIn @in, Func<TIn, TOut> func) => func(@in);

    /// <summary>
    /// Konwertuje Enumerator w Enumerowalny (Kolekcje)
    /// </summary>
    public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enume)
    {
        var e = enume;
        while (e.MoveNext())
            yield return e.Current;
    }
}
