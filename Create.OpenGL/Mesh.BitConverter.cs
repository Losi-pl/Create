using Create.OpenGL.Mathematic;
using OpenTK.Mathematics;

namespace Create.OpenGL;

partial class Mesh
{
    /// <summary>
    /// konwertuje dane z <paramref name="values"/> i wkleje je do <paramref name="bytes"/> z przesunięciem <paramref name="offser"/> i w odstępach <paramref name="sequence"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Array values, int offser, int sequence)
    {
        var typ = values.GetType().GetElementType();

        if (typ == typeof(int))
            InprintValue(bytes, (int[])values, offser, sequence);
        else if (typ == typeof(Vector2i))
            InprintValue(bytes, (Vector2i[])values, offser, sequence);
        else if (typ == typeof(Vector3i))
            InprintValue(bytes, (Vector3i[])values, offser, sequence);
        else if (typ == typeof(Vector4i))
            InprintValue(bytes, (Vector4i[])values, offser, sequence);

        else if (typ == typeof(bool))
            InprintValue(bytes, (bool[])values, offser, sequence);
        else if (typ == typeof(Vector2b))
            InprintValue(bytes, (Vector2b[])values, offser, sequence);
        else if (typ == typeof(Vector3b))
            InprintValue(bytes, (Vector3b[])values, offser, sequence);
        else if (typ == typeof(Vector4b))
            InprintValue(bytes, (Vector4b[])values, offser, sequence);

        else if (typ == typeof(float))
            InprintValue(bytes, (float[])values, offser, sequence);
        else if (typ == typeof(Vector2))
            InprintValue(bytes, (Vector2[])values, offser, sequence);
        else if (typ == typeof(Vector3))
            InprintValue(bytes, (Vector3[])values, offser, sequence);
        else if (typ == typeof(Vector4))
            InprintValue(bytes, (Vector4[])values, offser, sequence);

        else if (typ == typeof(Matrix2))
            InprintValue(bytes, (Matrix2[])values, offser, sequence);
        else if (typ == typeof(Matrix2x3))
            InprintValue(bytes, (Matrix2x3[])values, offser, sequence);
        else if (typ == typeof(Matrix2x4))
            InprintValue(bytes, (Matrix2x4[])values, offser, sequence);

        else if (typ == typeof(Matrix3x2))
            InprintValue(bytes, (Matrix3x2[])values, offser, sequence);
        else if (typ == typeof(Matrix3))
            InprintValue(bytes, (Matrix3[])values, offser, sequence);
        else if (typ == typeof(Matrix3x4))
            InprintValue(bytes, (Matrix3x4[])values, offser, sequence);

        else if (typ == typeof(Matrix4x2))
            InprintValue(bytes, (Matrix4x2[])values, offser, sequence);
        else if (typ == typeof(Matrix4x3))
            InprintValue(bytes, (Matrix4x3[])values, offser, sequence);
        else if (typ == typeof(Matrix4))
            InprintValue(bytes, (Matrix4[])values, offser, sequence);

        else if (typ == typeof(Color4))
            InprintValue(bytes, (Color4[])values, offser, sequence);

        else
            throw new NotSupportedException("Array content is not supported");
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, int[] values, int offser, int sequence)
    {
        for(int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector2i[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector3i[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector4i[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, bool[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector2b[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector3b[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector4b[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, float[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector2[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector3[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Vector4[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix2[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix2x3[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix2x4[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix3x2[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix3[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix3x4[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix4x2[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix4x3[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Matrix4[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            InprintBytes(bytes, values[i], s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintValue(byte[] bytes, Color4[] values, int offser, int sequence)
    {
        for (int i = 0; i < values.Length; i++)
        {
            int s = (sequence * i) + offser;
            var v = values[i];
            InprintBytes(bytes, new Vector4(v.R, v.G, v.B, v.A), s);
        }
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintArray<T>(T[] print_to, T[] print_from, int offset, int lenght)
    {
        for(int i = 0; i < lenght; i++)
            print_to[i + offset] = print_from[i];
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, int value, int offset)
    {
        var byts = BitConverter.GetBytes(value);
        InprintArray(bytes, byts, offset, 4);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector2i value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector3i value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
        InprintBytes(bytes, value.Z, offset + 8);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector4i value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
        InprintBytes(bytes, value.Z, offset + 8);
        InprintBytes(bytes, value.W, offset + 12);
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, bool value, int offset)
    {
        var byts = BitConverter.GetBytes(value);
        InprintArray(bytes, byts, offset, 1);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector2b value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 1);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector3b value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 1);
        InprintBytes(bytes, value.Z, offset + 2);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector4b value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 1);
        InprintBytes(bytes, value.Z, offset + 2);
        InprintBytes(bytes, value.W, offset + 3);
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, float value, int offset)
    {
        var byts = BitConverter.GetBytes(value);
        InprintArray(bytes, byts, offset, 4);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector2 value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector3 value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
        InprintBytes(bytes, value.Z, offset + 8);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Vector4 value, int offset)
    {
        InprintBytes(bytes, value.X, offset);
        InprintBytes(bytes, value.Y, offset + 4);
        InprintBytes(bytes, value.Z, offset + 8);
        InprintBytes(bytes, value.W, offset + 12);
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix2 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 8);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix2x3 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 8);
        InprintBytes(bytes, value.Column2, offset + 16);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix2x4 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 8);
        InprintBytes(bytes, value.Column2, offset + 16);
        InprintBytes(bytes, value.Column3, offset + 24);
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix3x2 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 12);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix3 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 12);
        InprintBytes(bytes, value.Column2, offset + 24);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix3x4 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 12);
        InprintBytes(bytes, value.Column2, offset + 24);
        InprintBytes(bytes, value.Column3, offset + 36);
    }


    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix4x2 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 16);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix4x3 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 16);
        InprintBytes(bytes, value.Column2, offset + 32);
    }

    /// <summary>
    /// <inheritdoc cref="InprintValue(byte[], Array, int, int)"/>
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    static void InprintBytes(byte[] bytes, Matrix4 value, int offset)
    {
        InprintBytes(bytes, value.Column0, offset);
        InprintBytes(bytes, value.Column1, offset + 16);
        InprintBytes(bytes, value.Column2, offset + 32);
        InprintBytes(bytes, value.Column3, offset + 48);
    }
}