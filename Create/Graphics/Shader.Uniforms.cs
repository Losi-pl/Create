using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL.Extensions.ARB;
using Silk.NET.OpenGL;
using Create.General;
using Silk.NET.Maths;

namespace Create.Graphics;

partial class Shader
{
    private delegate void UniformSetter<in T>(GL gl, ref Uniform uniform, T value, uint handle);
    
    private void SetUniform<T>(string name, UniformType type, T value, UniformSetter<T> setting)
    {
        var gl = Window.GL;

        ref var target = ref _uniforms.Find(name);
        
        if(target.Type != type)
            throw new ArgumentException($"The uniform {name} does expects data of {type}.");
        
        setting(gl, ref target, value, Handle);
    }

    /// <summary>
    /// Used to set a single value uniform accepting <see langword="sbyte"/>, <see langword="byte"/>, <see langword="short"/>, <see langword="ushort"/>
    /// <see langword="int"/>, <see langword="uint"/>, <see langword="long"/>, <see langword="ulong"/>, <see langword="float"/>, <see langword="double"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, T value) where T : unmanaged, INumberBase<T>
    {
        ref var uniform = ref _uniforms.Find(name);//This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");
        
        if(uniform.Type is (UniformType)ARB.Int64Arb or (UniformType)ARB.UnsignedInt64Arb)
            switch (uniform.Type)
            {
                case (UniformType)ARB.Int64Arb:
                    Window.GLong.ProgramUniform1(Handle, uniform.Location, long.CreateTruncating(value));
                    break;
                case (UniformType)ARB.UnsignedInt64Arb:
                    Window.GLong.ProgramUniform1(Handle, uniform.Location, ulong.CreateTruncating(value));
                    break;
            }
        else
            switch (uniform.Type)
            {
                case (UniformType)GLEnum.Byte:
                case (UniformType)GLEnum.Short:
                case UniformType.Int:
                    Window.GL.ProgramUniform1(Handle, uniform.Location, int.CreateTruncating(value));
                    break;
                case (UniformType)GLEnum.UnsignedByte:
                case (UniformType)GLEnum.UnsignedShort:
                case UniformType.UnsignedInt:
                    Window.GL.ProgramUniform1(Handle, uniform.Location, uint.CreateTruncating(value));
                    break;
                case UniformType.Float:
                case (UniformType)0x8FF8: //Half Float
                    Window.GL.ProgramUniform1(Handle, uniform.Location, float.CreateTruncating(value));
                    break;
                case UniformType.Double:
                    Window.GL.ProgramUniform1(Handle, uniform.Location, double.CreateTruncating(value));
                    break;
                default:
                    throw new ArgumentException($"The uniform \"{name}\" is not a Binary Integer");
            }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Vector2D{int}"/>, <see cref="Vector2D{uint}"/>, <see cref="Vector2D{long}"/>, <see cref="Vector2D{ulong}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Vector2D<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name);//This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");
        
        if(uniform.Type is (UniformType)ARB.Int64Vec2Arb or (UniformType)ARB.UnsignedInt64Vec2Arb)
            switch (uniform.Type)
            {
                case (UniformType)ARB.Int64Vec2Arb:
                    var vec = value as Vector2D<long>? ?? value.As<long>();
                    Window.GLong.ProgramUniform2(Handle, uniform.Location, vec.X, vec.Y);
                    break;
                case (UniformType)ARB.UnsignedInt64Vec2Arb:
                    var uVec = value as Vector2D<ulong>? ?? value.As<ulong>();
                    Window.GLong.ProgramUniform2(Handle, uniform.Location, uVec.X, uVec.Y);
                    break;
            }
        else
            switch (uniform.Type)
            {
                case UniformType.IntVec2:
                    var vec = value as Vector2D<int>? ?? value.As<int>();
                    Window.GL.ProgramUniform2(Handle, uniform.Location, vec.X, vec.Y);
                    break;
                case UniformType.UnsignedIntVec2:
                    var uVec = value as Vector2D<uint>? ?? value.As<uint>();
                    Window.GL.ProgramUniform2(Handle, uniform.Location, uVec.X, uVec.Y);
                    break;
                case UniformType.FloatVec2:
                case (UniformType)0x8FF9: //Half Float Vector 2
                    var fVec = value as Vector2D<float>? ?? value.As<float>();
                    Window.GL.ProgramUniform2(Handle, uniform.Location, fVec.X, fVec.Y);
                    break;
                case UniformType.DoubleVec2:
                    var dVec = value as Vector2D<double>? ?? value.As<double>();
                    Window.GL.ProgramUniform2(Handle, uniform.Location, dVec.X, dVec.Y);
                    break;
                default:
                    throw new ArgumentException($"The uniform \"{name}\" is not a Vector2");
            }
    }

    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Vector3D{int}"/>, <see cref="Vector3D{uint}"/>, <see cref="Vector3D{long}"/>, <see cref="Vector3D{ulong}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Vector3D<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        if (uniform.Type is (UniformType)ARB.Int64Vec3Arb or (UniformType)ARB.UnsignedInt64Vec3Arb)
            switch (uniform.Type)
            {
                case (UniformType)ARB.Int64Vec3Arb:
                    var vec = value as Vector3D<long>? ?? value.As<long>();
                    Window.GLong.ProgramUniform3(Handle, uniform.Location, vec.X, vec.Y, vec.Z);
                    break;
                case (UniformType)ARB.UnsignedInt64Vec3Arb:
                    var uVec = value as Vector3D<ulong>? ?? value.As<ulong>();
                    Window.GLong.ProgramUniform3(Handle, uniform.Location, uVec.X, uVec.Y, uVec.Z);
                    break;
            }
        else
            switch (uniform.Type)
            {
                case UniformType.IntVec3:
                    var vec = value as Vector3D<int>? ?? value.As<int>();
                    Window.GL.ProgramUniform3(Handle, uniform.Location, vec.X, vec.Y, vec.Z);
                    break;
                case UniformType.UnsignedIntVec3:
                    var uVec = value as Vector3D<uint>? ?? value.As<uint>();
                    Window.GL.ProgramUniform3(Handle, uniform.Location, uVec.X, uVec.Y, uVec.Z);
                    break;
                case UniformType.FloatVec3:
                case (UniformType)0x8FFA: //Half Float Vector 3
                    var fVec = value as Vector3D<float>? ?? value.As<float>();
                    Window.GL.ProgramUniform3(Handle, uniform.Location, fVec.X, fVec.Y, fVec.Z);
                    break;
                case UniformType.DoubleVec3:
                    var dVec = value as Vector3D<double>? ?? value.As<double>();
                    Window.GL.ProgramUniform3(Handle, uniform.Location, dVec.X, dVec.Y, dVec.Z);
                    break;
                default:
                    throw new ArgumentException($"The uniform \"{name}\" is not a Vector3");
            }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Vector4D{int}"/>, <see cref="Vector4D{uint}"/>, <see cref="Vector4D{long}"/>, <see cref="Vector4D{ulong}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Vector4D<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        if (uniform.Type is (UniformType)ARB.Int64Vec4Arb or (UniformType)ARB.UnsignedInt64Vec4Arb)
            switch (uniform.Type)
            {
                case (UniformType)ARB.Int64Vec4Arb:
                    var vec = value as Vector4D<long>? ?? value.As<long>();
                    Window.GLong.ProgramUniform4(Handle, uniform.Location, vec.X, vec.Y, vec.Z, vec.W);
                    break;
                case (UniformType)ARB.UnsignedInt64Vec4Arb:
                    var uVec = value as Vector4D<ulong>? ?? value.As<ulong>();
                    Window.GLong.ProgramUniform4(Handle, uniform.Location, uVec.X, uVec.Y, uVec.Z, uVec.W);
                    break;
            }
        else
            switch (uniform.Type)
            {
                case UniformType.IntVec4:
                    var vec = value as Vector4D<int>? ?? value.As<int>();
                    Window.GL.ProgramUniform4(Handle, uniform.Location, vec.X, vec.Y, vec.Z, vec.W);
                    break;
                case UniformType.UnsignedIntVec4:
                    var uVec = value as Vector4D<uint>? ?? value.As<uint>();
                    Window.GL.ProgramUniform4(Handle, uniform.Location, uVec.X, uVec.Y, uVec.Z, uVec.W);
                    break;
                case UniformType.FloatVec4:
                case (UniformType)0x8FFB: //Half Float Vector 4
                    var fVec = value as Vector4D<float>? ?? value.As<float>();
                    Window.GL.ProgramUniform4(Handle, uniform.Location, fVec.X, fVec.Y, fVec.Z, fVec.W);
                    break;
                case UniformType.DoubleVec4:
                    var dVec = value as Vector4D<double>? ?? value.As<double>();
                    Window.GL.ProgramUniform4(Handle, uniform.Location, dVec.X, dVec.Y, dVec.Z, dVec.W);
                    break;
                default:
                    throw new ArgumentException($"The uniform \"{name}\" is not a Vector4");
            }
    }

    /// <summary>
    /// Used to set a single value uniform accepting <see langword="bool"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform(string name, bool value) => SetUniform(name, UniformType.Bool, value, (gl, ref uni, v, handle) =>
        gl.ProgramUniform1(handle, uni.Location, v ? 1 : 0));
    //TODO => Make Bool Vectors
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix2X2{float}"/>, <see cref="Matrix2X2{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix2X2<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat2:
                var mat = value as Matrix2X2<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X2<float>, float>(ref mat),
                    2 * 2));
                break;
            case UniformType.DoubleMat2:
                var dMat = value as Matrix2X2<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X2<double>, double>(ref dMat),
                    2 * 2));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 2x2");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix2X3{float}"/>, <see cref="Matrix2X3{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix2X3<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat2x3:
                var mat = value as Matrix2X3<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix2x3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X3<float>, float>(ref mat),
                    2 * 3));
                break;
            case UniformType.DoubleMat2x3:
                var dMat = value as Matrix2X3<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix2x3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X3<double>, double>(ref dMat),
                    2 * 3));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 2x3");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix2X4{float}"/>, <see cref="Matrix2X4{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix2X4<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat2x4:
                var mat = value as Matrix2X4<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix2x4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X4<float>, float>(ref mat),
                    2 * 4));
                break;
            case UniformType.DoubleMat2x4:
                var dMat = value as Matrix2X4<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix2x4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix2X4<double>, double>(ref dMat),
                    2 * 4));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 2x4");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix3X2{float}"/>, <see cref="Matrix3X2{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix3X2<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat3x2:
                var mat = value as Matrix3X2<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix3x2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X2<float>, float>(ref mat),
                    3 * 2));
                break;
            case UniformType.DoubleMat3x2:
                var dMat = value as Matrix3X2<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix3x2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X2<double>, double>(ref dMat),
                    3 * 2));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 3x2");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix3X3{float}"/>, <see cref="Matrix3X3{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix3X3<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat3:
                var mat = value as Matrix3X3<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X3<float>, float>(ref mat),
                    3 * 3));
                break;
            case UniformType.DoubleMat3:
                var dMat = value as Matrix3X3<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X3<double>, double>(ref dMat),
                    3 * 3));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 3x3");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix3X4{float}"/>, <see cref="Matrix3X4{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix3X4<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat3x4:
                var mat = value as Matrix3X4<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix3x4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X4<float>, float>(ref mat),
                    3 * 4));
                break;
            case UniformType.DoubleMat3x4:
                var dMat = value as Matrix3X4<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix3x4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix3X4<double>, double>(ref dMat),
                    3 * 4));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 3x4");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix4X2{float}"/>, <see cref="Matrix4X2{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix4X2<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat4x2:
                var mat = value as Matrix4X2<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix4x2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X2<float>, float>(ref mat),
                    4 * 2));
                break;
            case UniformType.DoubleMat4x2:
                var dMat = value as Matrix4X2<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix4x2(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X2<double>, double>(ref dMat),
                    4 * 2));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 4x2");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix4X3{float}"/>, <see cref="Matrix4X3{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix4X3<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat4x3:
                var mat = value as Matrix4X3<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix4x3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X3<float>, float>(ref mat),
                    4 * 3));
                break;
            case UniformType.DoubleMat4x3:
                var dMat = value as Matrix4X3<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix4x3(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X3<double>, double>(ref dMat),
                    4 * 3));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 4x3");
        }
    }
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix4X4{float}"/>, <see cref="Matrix4X4{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The <paramref name="value"/> to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform<T>(string name, Matrix4X4<T> value) where T : unmanaged, IBinaryNumber<T>
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat4:
                var mat = value as Matrix4X4<float>? ?? value.As<float>();
                gl.ProgramUniformMatrix4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X4<float>, float>(ref mat),
                    4 * 4));
                break;
            case UniformType.DoubleMat4:
                var dMat = value as Matrix4X4<double>? ?? value.As<double>();
                gl.ProgramUniformMatrix4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X4<double>, double>(ref dMat),
                    4 * 4));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 4x4");
        }
    }
    
    
    
    /// <summary>
    /// Used to set a single value uniform accepting <see cref="Matrix4X4{float}"/>, <see cref="Matrix4X4{uint}"/>
    /// </summary>
    /// <param name="name">Name of the uniform to be set</param>
    /// <param name="value">The value to set in this uniform</param>
    /// <exception cref="ArgumentException">If the type expected by this uniform is none of the aforementioned and/or expected an array of data</exception>
    /// <exception cref="KeyNotFoundException">If there is no uniform by <see cref="name"/></exception>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public void SetUniform(string name, Matrix4x4 value)
    {
        ref var uniform = ref _uniforms.Find(name); //This is an extension method to make the look-up simpler
        if(uniform.IsArray)
            throw new ArgumentException($"The uniform \"{name}\" has expected an array of size {uniform.Count}.");

        var gl = Window.GL;
        
        switch (uniform.Type)
        {
            case UniformType.FloatMat4:
                gl.ProgramUniformMatrix4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4x4, float>(ref value),
                    4 * 4));
                break;
            case UniformType.DoubleMat4:
                var mat = new Matrix4X4<double>(value.M11, value.M12, value.M13, value.M14, 
                                                value.M21, value.M22, value.M23, value.M24, 
                                                value.M31, value.M32, value.M33, value.M34, 
                                                value.M41, value.M42, value.M43, value.M44);
                gl.ProgramUniformMatrix4(Handle, uniform.Location, true, MemoryMarshal.CreateSpan(
                    ref Unsafe.As<Matrix4X4<double>, double>(ref mat),
                    4 * 4));
                break;
            default:
                throw new ArgumentException($"The uniform \"{name}\" is not a Matrix 4x4");
        }
    }
}