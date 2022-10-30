using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Create.Conteiner;

public sealed class DataContainer
{
    Dictionary<string, object> data = new();


    public void Set<T>(string name, T value) where T : unmanaged
    {
        if (data.ContainsKey(name))
            data[name] = value;
        else
            data.Add(name, value);
    }
    public void Set<T>(string name, T? value) where T : unmanaged
    {
        if(value.HasValue)
        {
            if (data.ContainsKey(name))
                data[name] = value.Value;
            else
                data.Add(name, value.Value);
        }
        else
        {
            if(data.ContainsKey(name))
                data.Remove(name);
        }
    }
    public void Set<T>(string name, T[]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }
    public void Set<T>(string name, T[,]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }
    public void Set<T>(string name, T[,,]? value) where T : unmanaged
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }
    public void Set(string name, string? value)
    {
        if (value != null)
        {
            if (data.ContainsKey(name))
                data[name] = value;
            else
                data.Add(name, value);
        }
        else
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
    }
    public void Set(string name, string[]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }
    public void Set(string name, string[,]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }
    public void Set(string name, string[,,]? value)
    {
        if (value == null)
        {
            if (data.ContainsKey(name))
                data.Remove(name);
        }
        else
        {
            if (data.ContainsKey(name))
                data[name] = clone_array(value);
            else
                data.Add(name, clone_array(value));
        }
    }

    public object? Get(string name)
    {
        if (!data.ContainsKey(name))
            return null;
        var d = data[name];
        if (d is Array)
        {
            var a = (Array)d;
            switch (a.Rank)
            {
                case 1:
                    return clone_array((object[])a);
                case 2:
                    return clone_array((object[,])a);
                case 3:
                    return clone_array((object[,,])a);
            }
            return null;
        }
        return d;
    }

    static T[] clone_array<T>(T[] array)
    {
        if (array == null)
            return null!;
        T[] coppy = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
            coppy[i] = array[i];
        return coppy;
    }
    static T[,] clone_array<T>(T[,] array)
    {
        if (array == null)
            return null!;
        var size = array.GetSize();
        T[,] coppy = new T[size.dim0, size.dim1];
        for (int d0 = 0; d0 < size.dim0; d0++)
            for (int d1 = 0; d1 < size.dim1; d1++)
                coppy[d0, d1] = array[d0, d1];
        return coppy;
    }
    static T[,,] clone_array<T>(T[,,] array)
    {
        if (array == null)
            return null!;
        var size = array.GetSize();
        T[,,] coppy = new T[size.dim0, size.dim1, size.dim2];
        for (int d0 = 0; d0 < size.dim0; d0++)
            for (int d1 = 0; d1 < size.dim1; d1++)
                for (int d2 = 0; d2 < size.dim2; d2++)
                    coppy[d0, d1, d2] = array[d0, d1, d2];
        return coppy;
    }

    /*public struct Content
    {
        object? cont;

        #region In 0D
        public static implicit operator Content(int? value) => new() { cont = value.HasValue ? value.Value : null };

        public static implicit operator Content(uint value) => new() { cont = value };
        public static implicit operator Content(float value) => new() { cont = value };
        public static implicit operator Content(bool value) => new() { cont = value };
        public static implicit operator Content(byte value) => new() { cont = value };
        public static implicit operator Content(string value) => new() { cont = value };

        public static implicit operator Content(Vector2 value) => new() { cont = value };
        public static implicit operator Content(Vector3 value) => new() { cont = value };
        public static implicit operator Content(Vector4 value) => new() { cont = value };
        public static implicit operator Content(Matrix2 value) => new() { cont = value };
        public static implicit operator Content(Matrix2x3 value) => new() { cont = value };
        public static implicit operator Content(Matrix2x4 value) => new() { cont = value };
        public static implicit operator Content(Matrix3x2 value) => new() { cont = value };
        public static implicit operator Content(Matrix3 value) => new() { cont = value };
        public static implicit operator Content(Matrix3x4 value) => new() { cont = value };
        public static implicit operator Content(Matrix4x2 value) => new() { cont = value };
        public static implicit operator Content(Matrix4x3 value) => new() { cont = value };
        public static implicit operator Content(Matrix4 value) => new() { cont = value };
        #endregion

        #region In 1D
        public static implicit operator Content(int[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(uint[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(float[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(bool[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(byte[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(string[] value) => new() { cont = clone_array(value) };

        public static implicit operator Content(Vector2[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector3[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector4[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x3[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x4[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x2[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x4[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x2[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x3[] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4[] value) => new() { cont = clone_array(value) };
        #endregion

        #region In 2D
        public static implicit operator Content(int[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(uint[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(float[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(bool[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(byte[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(string[,] value) => new() { cont = clone_array(value) };

        public static implicit operator Content(Vector2[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector3[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector4[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x3[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x4[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x2[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x4[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x2[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x3[,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4[,] value) => new() { cont = clone_array(value) };
        #endregion

        #region In 3D
        public static implicit operator Content(int[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(uint[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(float[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(bool[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(byte[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(string[,,] value) => new() { cont = clone_array(value) };

        public static implicit operator Content(Vector2[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector3[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Vector4[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x3[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix2x4[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x2[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix3x4[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x2[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4x3[,,] value) => new() { cont = clone_array(value) };
        public static implicit operator Content(Matrix4[,,] value) => new() { cont = clone_array(value) };
        #endregion

        #region Out 0D
        public static explicit operator int(Content c) => cast_struct_value<int>(c);
        public static explicit operator bool(Content c) => cast_struct_value<bool>(c);
        public static explicit operator byte(Content c) => cast_struct_value<byte>(c);
        public static explicit operator uint(Content c) => cast_struct_value<uint>(c);
        public static explicit operator float(Content c) => cast_struct_value<float>(c);

        public static explicit operator int?(Content c) => null_cast_struct_value<int>(c);
        public static explicit operator bool?(Content c) => null_cast_struct_value<bool>(c);
        public static explicit operator byte?(Content c) => null_cast_struct_value<byte>(c);
        public static explicit operator uint?(Content c) => null_cast_struct_value<uint>(c);
        public static explicit operator float?(Content c) => null_cast_struct_value<float>(c);
        public static explicit operator string?(Content c)
        {
            if (c.cont is null)
                return null;
            return (string)c.cont;
        }

        public static explicit operator Vector2(Content c) => cast_struct_value<Vector2>(c);
        public static explicit operator Vector3(Content c) => cast_struct_value<Vector3>(c);
        public static explicit operator Vector4(Content c) => cast_struct_value<Vector4>(c);
        public static explicit operator Matrix2(Content c) => cast_struct_value<Matrix2>(c);
        public static explicit operator Matrix2x3(Content c) => cast_struct_value<Matrix2x3>(c);
        public static explicit operator Matrix2x4(Content c) => cast_struct_value<Matrix2x4>(c);
        public static explicit operator Matrix3x2(Content c) => cast_struct_value<Matrix3x2>(c);
        public static explicit operator Matrix3(Content c) => cast_struct_value<Matrix3>(c);
        public static explicit operator Matrix3x4(Content c) => cast_struct_value<Matrix3x4>(c);
        public static explicit operator Matrix4x2(Content c) => cast_struct_value<Matrix4x2>(c);
        public static explicit operator Matrix4x3(Content c) => cast_struct_value<Matrix4x3>(c);
        public static explicit operator Matrix4(Content c) => cast_struct_value<Matrix4>(c);

        public static explicit operator Vector2?(Content c) => null_cast_struct_value<Vector2>(c);
        public static explicit operator Vector3?(Content c) => null_cast_struct_value<Vector3>(c);
        public static explicit operator Vector4?(Content c) => null_cast_struct_value<Vector4>(c);
        public static explicit operator Matrix2?(Content c) => null_cast_struct_value<Matrix2>(c);
        public static explicit operator Matrix2x3?(Content c) => null_cast_struct_value<Matrix2x3>(c);
        public static explicit operator Matrix2x4?(Content c) => null_cast_struct_value<Matrix2x4>(c);
        public static explicit operator Matrix3x2?(Content c) => null_cast_struct_value<Matrix3x2>(c);
        public static explicit operator Matrix3?(Content c) => null_cast_struct_value<Matrix3>(c);
        public static explicit operator Matrix3x4?(Content c) => null_cast_struct_value<Matrix3x4>(c);
        public static explicit operator Matrix4x2?(Content c) => null_cast_struct_value<Matrix4x2>(c);
        public static explicit operator Matrix4x3?(Content c) => null_cast_struct_value<Matrix4x3>(c);
        public static explicit operator Matrix4?(Content c) => null_cast_struct_value<Matrix4>(c);
        #endregion

        #region Out 1D
        public static explicit operator int[]?(Content c) => cast_array_1<int>(c);
        public static explicit operator bool[]?(Content c) => cast_array_1<bool>(c);
        public static explicit operator byte[]?(Content c) => cast_array_1<byte>(c);
        public static explicit operator uint[]?(Content c) => cast_array_1<uint>(c);
        public static explicit operator float[]?(Content c) => cast_array_1<float>(c);
        public static explicit operator string[]?(Content c)
        {
            if (c.cont is null)
                return null;
            return clone_array((string[])c.cont);
        }

        public static explicit operator Vector2[]?(Content c) => cast_array_1<Vector2>(c);
        public static explicit operator Vector3[]?(Content c) => cast_array_1<Vector3>(c);
        public static explicit operator Vector4[]?(Content c) => cast_array_1<Vector4>(c);
        public static explicit operator Matrix2[]?(Content c) => cast_array_1<Matrix2>(c);
        public static explicit operator Matrix2x3[]?(Content c) => cast_array_1<Matrix2x3>(c);
        public static explicit operator Matrix2x4[]?(Content c) => cast_array_1<Matrix2x4>(c);
        public static explicit operator Matrix3x2[]?(Content c) => cast_array_1<Matrix3x2>(c);
        public static explicit operator Matrix3[]?(Content c) => cast_array_1<Matrix3>(c);
        public static explicit operator Matrix3x4[]?(Content c) => cast_array_1<Matrix3x4>(c);
        public static explicit operator Matrix4x2[]?(Content c) => cast_array_1<Matrix4x2>(c);
        public static explicit operator Matrix4x3[]?(Content c) => cast_array_1<Matrix4x3>(c);
        public static explicit operator Matrix4[]?(Content c) => cast_array_1<Matrix4>(c);
        #endregion

        #region Out 2D
        public static explicit operator int[,]?(Content c) => cast_array_2<int>(c);
        public static explicit operator bool[,]?(Content c) => cast_array_2<bool>(c);
        public static explicit operator byte[,]?(Content c) => cast_array_2<byte>(c);
        public static explicit operator uint[,]?(Content c) => cast_array_2<uint>(c);
        public static explicit operator float[,]?(Content c) => cast_array_2<float>(c);
        public static explicit operator string[,]?(Content c)
        {
            if (c.cont is null)
                return null;
            return clone_array((string[,])c.cont);
        }

        public static explicit operator Vector2[,]?(Content c) => cast_array_2<Vector2>(c);
        public static explicit operator Vector3[,]?(Content c) => cast_array_2<Vector3>(c);
        public static explicit operator Vector4[,]?(Content c) => cast_array_2<Vector4>(c);
        public static explicit operator Matrix2[,]?(Content c) => cast_array_2<Matrix2>(c);
        public static explicit operator Matrix2x3[,]?(Content c) => cast_array_2<Matrix2x3>(c);
        public static explicit operator Matrix2x4[,]?(Content c) => cast_array_2<Matrix2x4>(c);
        public static explicit operator Matrix3x2[,]?(Content c) => cast_array_2<Matrix3x2>(c);
        public static explicit operator Matrix3[,]?(Content c) => cast_array_2<Matrix3>(c);
        public static explicit operator Matrix3x4[,]?(Content c) => cast_array_2<Matrix3x4>(c);
        public static explicit operator Matrix4x2[,]?(Content c) => cast_array_2<Matrix4x2>(c);
        public static explicit operator Matrix4x3[,]?(Content c) => cast_array_2<Matrix4x3>(c);
        public static explicit operator Matrix4[,]?(Content c) => cast_array_2<Matrix4>(c);
        #endregion

        #region Out 3D
        public static explicit operator int[,,]?(Content c) => cast_array_3<int>(c);
        public static explicit operator bool[,,]?(Content c) => cast_array_3<bool>(c);
        public static explicit operator byte[,,]?(Content c) => cast_array_3<byte>(c);
        public static explicit operator uint[,,]?(Content c) => cast_array_3<uint>(c);
        public static explicit operator float[,,]?(Content c) => cast_array_3<float>(c);
        public static explicit operator string[,,]?(Content c)
        {
            if (c.cont is null)
                return null;
            return clone_array((string[,,])c.cont);
        }

        public static explicit operator Vector2[,,]?(Content c) => cast_array_3<Vector2>(c);
        public static explicit operator Vector3[,,]?(Content c) => cast_array_3<Vector3>(c);
        public static explicit operator Vector4[,,]?(Content c) => cast_array_3<Vector4>(c);
        public static explicit operator Matrix2[,,]?(Content c) => cast_array_3<Matrix2>(c);
        public static explicit operator Matrix2x3[,,]?(Content c) => cast_array_3<Matrix2x3>(c);
        public static explicit operator Matrix2x4[,,]?(Content c) => cast_array_3<Matrix2x4>(c);
        public static explicit operator Matrix3x2[,,]?(Content c) => cast_array_3<Matrix3x2>(c);
        public static explicit operator Matrix3[,,]?(Content c) => cast_array_3<Matrix3>(c);
        public static explicit operator Matrix3x4[,,]?(Content c) => cast_array_3<Matrix3x4>(c);
        public static explicit operator Matrix4x2[,,]?(Content c) => cast_array_3<Matrix4x2>(c);
        public static explicit operator Matrix4x3[,,]?(Content c) => cast_array_3<Matrix4x3>(c);
        public static explicit operator Matrix4[,,]?(Content c) => cast_array_3<Matrix4>(c);
        #endregion

        #region Cast data
        static T cast_struct_value<T>(Content c)
        {
            if (c.cont is null)
                throw new NullReferenceException();
            if (c.cont is T)
                return (T)c.cont;
            throw new InvalidCastException();
        }
        static T? null_cast_struct_value<T>(Content c) where T : unmanaged
        {
            if (c.cont is null)
                return null;
            if (c.cont is not T)
                throw new NullReferenceException();
            return (T)c.cont;
        }
        static T[] clone_array<T>(T[] array)
        {
            if (array == null)
                return null!;
            T[] coppy = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                coppy[i] = array[i];
            return coppy;
        }
        static T[,] clone_array<T>(T[,] array)
        {
            if (array == null)
                return null!;
            var size = array.GetSize();
            T[,] coppy = new T[size.dim0, size.dim1];
            for (int d0 = 0; d0 < size.dim0; d0++)
                for (int d1 = 0; d1 < size.dim1; d1++)
                    coppy[d0, d1] = array[d0, d1];
            return coppy;
        }
        static T[,,] clone_array<T>(T[,,] array)
        {
            if (array == null)
                return null!;
            var size = array.GetSize();
            T[,,] coppy = new T[size.dim0, size.dim1, size.dim2];
            for (int d0 = 0; d0 < size.dim0; d0++)
                for (int d1 = 0; d1 < size.dim1; d1++)
                    for (int d2 = 0; d2 < size.dim2; d2++)
                        coppy[d0, d1, d2] = array[d0, d1, d2];
            return coppy;
        }
        static T[]? cast_array_1<T>(Content o)
        {
            if (o.cont is null)
                return null;
            if (o.cont is T[])
                return clone_array((T[])o.cont);
            throw new NullReferenceException();
        }
        static T[,]? cast_array_2<T>(Content o)
        {
            if (o.cont is null)
                return null;
            if (o.cont is T[,])
                return clone_array((T[,])o.cont);
            throw new NullReferenceException();
        }
        static T[,,]? cast_array_3<T>(Content o)
        {
            if (o.cont is null)
                return null;
            if (o.cont is T[,,])
                return clone_array((T[,,])o.cont);
            throw new NullReferenceException();
        }
        #endregion

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }
    }*/
}