using Create.OpenGL.Mathematic;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using System.Collections;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;

namespace Create;

internal static partial class Special
{
    /// <summary>
    /// Przekłada <see cref="ActiveAttribType"/> z OpenGL na <see cref="Type"/> z C#
    /// </summary>
    public static Type? GetCSharpType(this ActiveAttribType type) => type switch
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
    public static Type GetCSharpType(this ActiveUniformType type) => type switch
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
    public static int ElementByteSize(this ActiveAttribType type) => type switch
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
    /// Tworzy kolekcje z kopią zawartości <see cref="System.ReadOnlySpan{T}"/>
    /// </summary>
    public static IEnumerable<T> ToEnumerable<T>(this ReadOnlySpan<T> span)
    {
        T[] array = new T[span.Length];
        for(int i = 0; i < span.Length; i++)
            array[i] = span[i];
        return array;
    }
    
    /// <summary>
    /// Zwraca obiekt i gdzie w kolekcji się on znajduje który spełnia warunki <paramref name="condition"/>
    /// </summary>
    public static (T element, int index)? FindAndWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> condition)
    {
        int i = 0;
        foreach(var element in enumerable)
        {
            if (condition(element))
                return (element, i);
            ++i;
        }
        return null;
    }

    /// <summary>
    /// Zwraca element z kolekcji spełnaijący warunki <paramref name="condition"/>
    /// </summary>
    [DebuggerHidden]
    public static T Find<T>(this IEnumerable<T> enume, Func<T, bool> condition, Exception? ifNotFound)
    {
        foreach(var element in enume)
            if(condition(element))
                return element;
        if (ifNotFound != null)
            throw ifNotFound;
        return default!;
    }

    /// <summary>
    /// Przekłada z jakiego typu informacji zbudowane są wyrzsze typy zmiennych i ile zajmują one w pamięci karty graficznej
    /// </summary>
    public static (int values, VertexAttribPointerType type) ValueBindData(this ActiveAttribType value) => value switch
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
    /// Konwertowanie jednej wartości w inną za pomocą <paramref name="func"/>
    /// </summary>
    public static TOut Cast<TIn, TOut>(this TIn @in, Func<TIn, TOut> func) => func(@in);
    
    /// <summary>
    /// Konwertuje wartości w kolekcji za pomocą <paramref name="func"/>
    /// </summary>
    public static IEnumerable<TOut> ConvertAll<TIn, TOut>(this IEnumerable<TIn> enume, Func<TIn, TOut> func) => new CastEnumerable<TIn, TOut>(enume, func);

    /// <summary>
    /// Konwertuje wartości w tablicy za pomocą <paramref name="func"/>
    /// </summary>
    public static TOut[] ConvertAll<TIn, TOut>(this TIn[] array, Func<TIn, TOut> func)
    {
        var _array = new TOut[array.Length];
        for(long l = 0; l < array.LongLength; l++)
            _array[l] = func(array[l]);
        return _array;
    }
    
    /// <summary>
    /// Konwertuje przediał wartości w kolekcje numerów
    /// </summary>
    public static IEnumerator<int> GetEnumerator(this Range range) => new foreach_range(range);

    /// <summary>
    /// <inheritdoc cref="GetEnumerator(Range)"/>
    /// </summary>
    public static IEnumerable<int> GetEnumerable(this Range range) => new foreach_range(range);

    /// <summary>
    /// <inheritdoc cref="GetEnumerator(Range)"/>
    /// </summary>
    public struct foreach_range : IEnumerator<int>, IEnumerable<int>
    {
        int index, end;
        public foreach_range(Range range)
        {
            if (range.Start.IsFromEnd)
                throw new NotSupportedException();
            index = range.Start.Value - 1;
            if(range.End.IsFromEnd)
                throw new NotSupportedException();
            end = range.End.Value;
        }
        public bool MoveNext()
        {
            index++;
            return index <= end;
        }
        public IEnumerator<int> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;
        public void Reset() { }
        public void Dispose() { }
        public int Current => index;
        object IEnumerator.Current => Current;
    }
    
    /// <summary>
    /// Konwertuje tablice kolorów w obraz
    /// </summary>
    public static Image<T> ToImage<T>(this T[,] array) where T : unmanaged, IPixel<T>
    {
        var size = array.GetSize();
        var image = new Image<T>(size.dim0, size.dim1);
        for (int x = 0; x < size.dim0; x++)
            for (int y = 0; y < size.dim1; y++)
                //foreach (var x in 0..(size.dim0 - 1))
                //    foreach (var y in 0..(size.dim1 - 1))
                image[x, y] = array[x, y];
        return image;
    }
    
    /// <summary>
    /// Konwertuje Enumerator w Enumerowalny (Kolekcje)
    /// </summary>
    public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enume)
    {
        var e = enume;
        while (e.MoveNext())
            yield return e.Current;
    }

    #region Vector - Tumple Conversion
    /// <summary>
    /// Przekłada <see cref="Vector2"/> na krotke
    /// </summary>
    public static (float X, float Y) ToTumple(this Vector2 vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3"/> na krotke
    /// </summary>
    public static (float X, float Y, float Z) ToTumple(this Vector3 vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4"/> na krotke
    /// </summary>
    public static (float X, float Y, float Z, float W) ToTumple(this Vector4 vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2"/>
    /// </summary>
    public static Vector2 ToVector(this (float X, float Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3"/>
    /// </summary>
    public static Vector3 ToVector(this (float X, float Y, float Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4"/>
    /// </summary>
    public static Vector4 ToVector(this (float X, float Y, float Z, float W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada <see cref="Vector2i"/> na krotke
    /// </summary>
    public static (int X, int Y) ToTumple(this Vector2i vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3i"/> na krotke
    /// </summary>
    public static (int X, int Y, int Z) ToTumple(this Vector3i vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4i"/> na krotke
    /// </summary>
    public static (int X, int Y, int Z, int W) ToTumple(this Vector4i vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2i"/>
    /// </summary>
    public static Vector2i ToVector(this (int X, int Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3i"/>
    /// </summary>
    public static Vector3i ToVector(this (int X, int Y, int Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4i"/>
    /// </summary>
    public static Vector4i ToVector(this (int X, int Y, int Z, int W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);


    /// <summary>
    /// Przekłada <see cref="Vector2d"/> na krotke
    /// </summary>
    public static (double X, double Y) ToTumple(this Vector2d vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3d"/> na krotke
    /// </summary>
    public static (double X, double Y, double Z) ToTumple(this Vector3d vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4d"/> na krotke
    /// </summary>
    public static (double X, double Y, double Z, double W) ToTumple(this Vector4d vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2d"/>
    /// </summary>
    public static Vector2d ToVector(this (double X, double Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3d"/>
    /// </summary>
    public static Vector3d ToVector(this (double X, double Y, double Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4d"/>
    /// </summary>
    public static Vector4d ToVector(this (double X, double Y, double Z, double W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);


    /// <summary>
    /// Przekłada <see cref="Vector2b"/> na krotke
    /// </summary>
    public static (bool X, bool Y) ToTumple(this Vector2b vector) => (vector.X, vector.Y);

    /// <summary>
    /// Przekłada <see cref="Vector3b"/> na krotke
    /// </summary>
    public static (bool X, bool Y, bool Z) ToTumple(this Vector3b vector) => (vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada <see cref="Vector4b"/> na krotke
    /// </summary>
    public static (bool X, bool Y, bool Z, bool W) ToTumple(this Vector4b vector) => (vector.X, vector.Y, vector.Z, vector.W);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector2b"/>
    /// </summary>
    public static Vector2b ToVector(this (bool X, bool Y) vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector3b"/>
    /// </summary>
    public static Vector3b ToVector(this (bool X, bool Y, bool Z) vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary>
    /// Przekłada krotke na <see cref="Vector4b"/>
    /// </summary>
    public static Vector4b ToVector(this (bool X, bool Y, bool Z, bool W) vector) => new(vector.X, vector.Y, vector.Z, vector.W);
    #endregion

    #region Array Sizes
    /// <summary>
    /// Pobiera rozmiar tabeli w 2 osich
    /// </summary>
    public static (int dim0, int dim1) GetSize<T>(this T[,] array) => (array.GetLength(0), array.GetLength(1));

    /// <summary>
    /// Pobiera rozmiar tabeli w 3 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2) GetSize<T>(this T[,,] array) => (array.GetLength(0), array.GetLength(1), array.GetLength(2));

    /// <summary>
    /// Pobiera rozmiar tabeli w 4 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3) GetSize<T>(this T[,,,] array) => (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3));

    /// <summary>
    /// Pobiera rozmiar tabeli w 5 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4) GetSize<T>(this T[,,,,] array) => 
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4));

    /// <summary>
    /// Pobiera rozmiar tabeli w 6 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5) GetSize<T>(this T[,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5));

    /// <summary>
    /// Pobiera rozmiar tabeli w 7 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6) GetSize<T>(this T[,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6));

    /// <summary>
    /// Pobiera rozmiar tabeli w 8 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7) GetSize<T>(this T[,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7));

    /// <summary>
    /// Pobiera rozmiar tabeli w 9 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7, int dim8) GetSize<T>(this T[,,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7), array.GetLength(8));

    /// <summary>
    /// Pobiera rozmiar tabeli w 10 osich
    /// </summary>
    public static (int dim0, int dim1, int dim2, int dim3, int dim4, int dim5, int dim6, int dim7, int dim8, int dim9) GetSize<T>(this T[,,,,,,,,,] array) =>
        (array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), array.GetLength(4), array.GetLength(5), array.GetLength(6), array.GetLength(7), array.GetLength(8), array.GetLength(9));
    #endregion

    /// <summary>
    /// <inheritdoc cref="Cast{TIn, TOut}(TIn, Func{TIn, TOut})"/>
    /// </summary>
    private struct CastEnumerable<TIn, TOut> : IEnumerable<TOut>
    {
        Func<TIn, TOut> _func;
        IEnumerable<TIn> enumerable;

        public CastEnumerable(IEnumerable<TIn> en, Func<TIn,TOut> func)
        {
            enumerable = en;
            _func = func;
        }

        public IEnumerator<TOut> GetEnumerator() => new Enumerator(enumerable.GetEnumerator(), _func);
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(enumerable.GetEnumerator(), _func);

        public struct Enumerator : IEnumerator<TOut>
        {
            TOut value;
            IEnumerator<TIn> enumerator;
            Func<TIn, TOut> func;

            public Enumerator(IEnumerator<TIn> en, Func<TIn, TOut> func)
            {
                value = default!;
                enumerator = en;
                this.func = func;
            }

            public TOut Current => value;
            object IEnumerator.Current => value!;

            public void Dispose() => enumerator.Dispose();
            public void Reset() => enumerator.Reset();

            public bool MoveNext()
            {
                if (!enumerator.MoveNext())
                    return false;
                value = func(enumerator.Current);
                return true;
            }
        }
    }
}
