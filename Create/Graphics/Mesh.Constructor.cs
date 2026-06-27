using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.Graphics;

partial class Mesh
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public readonly struct Constructor(Shader shader)
    {
        public PiceMeal ManualFillOut() => new PiceMeal(shader);
        
        public class PiceMeal(Shader shader)
        {
            private readonly Dictionary<string, Array> _attributes = new();
            private DataLayout _dataMode = DataLayout.NonInterleaved;
            private PrimitiveType _drawMode = PrimitiveType.Triangles;
            private uint[]? _elements;

            public PiceMeal SetDataLayout(DataLayout layout) { _dataMode = layout; return this; }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, T[] values) where T : INumberBase<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(sbyte))
                    deliveredType = TypeTransformations.Byte;
                else if (typeof(T) == typeof(byte))
                    deliveredType = TypeTransformations.UnsignedByte;
                else if (typeof(T) == typeof(short))
                    deliveredType = TypeTransformations.Short;
                else if (typeof(T) == typeof(ushort))
                    deliveredType = TypeTransformations.UnsignedShort;
                else if (typeof(T) == typeof(int))
                    deliveredType = AttributeType.Int;
                else if (typeof(T) == typeof(uint))
                    deliveredType = AttributeType.UnsignedInt;
                else if (typeof(T) == typeof(long))
                    deliveredType = AttributeType.Int64Arb;
                else if (typeof(T) == typeof(ulong))
                    deliveredType = AttributeType.UnsignedInt64Arb;
                else if (typeof(T) == typeof(Half))
                    deliveredType = TypeTransformations.HalfFloat;
                else if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.Float;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.Double;
                else deliveredType = null;

                if (uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == TypeTransformations.Byte)
                    {
                        var copy = new sbyte[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, sbyte>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.UnsignedByte)
                    {
                        var copy = new byte[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, byte>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.Short)
                    {
                        var copy = new short[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, short>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.UnsignedShort)
                    {
                        var copy = new ushort[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, ushort>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Int)
                    {
                        var copy = new int[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, int>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedInt)
                    {
                        var copy = new uint[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, uint>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Int64Arb)
                    {
                        var copy = new long[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, long>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedInt64Arb)
                    {
                        var copy = new ulong[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, ulong>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.HalfFloat)
                    {
                        var copy = new Half[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, Half>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Float)
                    {
                        var copy = new float[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, float>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Double)
                    {
                        var copy = new double[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = Scalar.As<T, double>(values[i]);
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a single Scalar");
                }

                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Vector2D<T>[] values) where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(int))
                    deliveredType = AttributeType.IntVec2;
                else if (typeof(T) == typeof(uint))
                    deliveredType = AttributeType.UnsignedIntVec2;
                else if (typeof(T) == typeof(long))
                    deliveredType = AttributeType.Int64Vec2Arb;
                else if (typeof(T) == typeof(ulong))
                    deliveredType = AttributeType.UnsignedInt64Vec2Arb;
                else if (typeof(T) == typeof(Half))
                    deliveredType = TypeTransformations.HalfFloatVec2;
                else if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatVec2;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleVec2;
                else deliveredType = null;
                
                if (uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.IntVec2)
                    {
                        var copy = new Vector2D<int>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<int>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedIntVec2)
                    {
                        var copy = new Vector2D<uint>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<uint>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Int64Vec2Arb)
                    {
                        var copy = new Vector2D<long>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<long>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedInt64Vec2Arb)
                    {
                        var copy = new Vector2D<ulong>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<ulong>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.HalfFloatVec2)
                    {
                        var copy = new Vector2D<Half>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<Half>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.FloatVec2)
                    {
                        var copy = new Vector2D<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleVec2)
                    {
                        var copy = new Vector2D<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain an Vector2");
                }

                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Vector3D<T>[] values) where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if (values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values),
                        "You can't pass an empty array as a value in Mesh constructor");

                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));

                AttributeType? deliveredType;
                if (typeof(T) == typeof(int))
                    deliveredType = AttributeType.IntVec3;
                else if (typeof(T) == typeof(uint))
                    deliveredType = AttributeType.UnsignedIntVec3;
                else if (typeof(T) == typeof(long))
                    deliveredType = AttributeType.Int64Vec3Arb;
                else if (typeof(T) == typeof(ulong))
                    deliveredType = AttributeType.UnsignedInt64Vec3Arb;
                else if (typeof(T) == typeof(Half))
                    deliveredType = TypeTransformations.HalfFloatVec3;
                else if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatVec3;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleVec3;
                else deliveredType = null;

                if (uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.IntVec3)
                    {
                        var copy = new Vector3D<int>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<int>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedIntVec3)
                    {
                        var copy = new Vector3D<uint>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<uint>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Int64Vec3Arb)
                    {
                        var copy = new Vector3D<long>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<long>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedInt64Vec3Arb)
                    {
                        var copy = new Vector3D<ulong>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<ulong>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.HalfFloatVec3)
                    {
                        var copy = new Vector3D<Half>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<Half>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.FloatVec3)
                    {
                        var copy = new Vector3D<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleVec3)
                    {
                        var copy = new Vector3D<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain an Vector3");
                }

                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Vector4D<T>[] values) where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(int))
                    deliveredType = AttributeType.IntVec4;
                else if (typeof(T) == typeof(uint))
                    deliveredType = AttributeType.UnsignedIntVec4;
                else if (typeof(T) == typeof(long))
                    deliveredType = AttributeType.Int64Vec4Arb;
                else if (typeof(T) == typeof(ulong))
                    deliveredType = AttributeType.UnsignedInt64Vec4Arb;
                else if (typeof(T) == typeof(Half))
                    deliveredType = TypeTransformations.HalfFloatVec4;
                else if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatVec4;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleVec4;
                else deliveredType = null;
                
                if (uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.IntVec4)
                    {
                        var copy = new Vector4D<int>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<int>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedIntVec4)
                    {
                        var copy = new Vector4D<uint>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<uint>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.Int64Vec4Arb)
                    {
                        var copy = new Vector4D<long>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<long>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.UnsignedInt64Vec4Arb)
                    {
                        var copy = new Vector4D<ulong>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<ulong>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == TypeTransformations.HalfFloatVec4)
                    {
                        var copy = new Vector4D<Half>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<Half>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.FloatVec4)
                    {
                        var copy = new Vector4D<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleVec4)
                    {
                        var copy = new Vector4D<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain an Vector4");
                }

                return this;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix2X2<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat2;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat2;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat2)
                    {
                        var copy = new Matrix2X2<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat2)
                    {
                        var copy = new Matrix2X2<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix2x2");
                }
                
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix2X3<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat2x3;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat2x3;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat2x3)
                    {
                        var copy = new Matrix2X3<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat2x3)
                    {
                        var copy = new Matrix2X3<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix2x3");
                }
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix2X4<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat2x4;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat2x4;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat2x4)
                    {
                        var copy = new Matrix2X4<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat2x4)
                    {
                        var copy = new Matrix2X4<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix2x4");
                }
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix3X2<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat3x2;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat3x2;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat3x2)
                    {
                        var copy = new Matrix3X2<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat3x2)
                    {
                        var copy = new Matrix3X2<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix3x2");
                }
                
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix3X3<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat3;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat3;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat3)
                    {
                        var copy = new Matrix3X3<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat3)
                    {
                        var copy = new Matrix3X3<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix3x3");
                }
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix3X4<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat3x4;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat3x4;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat3x4)
                    {
                        var copy = new Matrix3X4<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat3x4)
                    {
                        var copy = new Matrix3X4<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix3x4");
                }
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix4X2<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat4x2;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat4x2;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat4x2)
                    {
                        var copy = new Matrix4X2<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat4x2)
                    {
                        var copy = new Matrix4X2<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix4x2");
                }
                
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix4X3<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat4x3;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat4x3;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat4x3)
                    {
                        var copy = new Matrix4X3<float>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat4x3)
                    {
                        var copy = new Matrix4X3<double>[values.Length];
                        for (int i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix4x3");
                }
                return this;
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal SetAttribute<T>(string name, Matrix4X4<T>[] values)  where T : unmanaged, INumberBase<T>, IComparable<T>
            {
                if(values.Length == 0)
                    throw new ArgumentOutOfRangeException(nameof(values), "You can't pass an empty array as a value in Mesh constructor");
                
                ref var uniform = ref shader.FindAttribute(name);
                
                if(values.Length % uniform.Count > 0)
                    throw new ArgumentException("Not all vertexes have all data specified", nameof(values));
                
                AttributeType? deliveredType;
                if (typeof(T) == typeof(float))
                    deliveredType = AttributeType.FloatMat4;
                else if (typeof(T) == typeof(double))
                    deliveredType = AttributeType.DoubleMat4;
                else deliveredType = null;
                
                if(uniform.Type == deliveredType)
                    _attributes[uniform.Name] = values;
                else
                {
                    if (uniform.Type == AttributeType.FloatMat4)
                    {
                        var copy = new Matrix4X4<float>[values.Length];
                        for (var i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<float>();
                        _attributes[uniform.Name] = copy;
                    }
                    else if (uniform.Type == AttributeType.DoubleMat4)
                    {
                        var copy = new Matrix4X4<double>[values.Length];
                        for (var i = 0; i < values.Length; i++)
                            copy[i] = values[i].As<double>();
                        _attributes[uniform.Name] = copy;
                    }
                    else
                        throw new InvalidCastException("This attribute doesn't contain a Matrix4x4");
                }
                return this;
            }

            public PiceMeal Triangles()
            {
                _drawMode = PrimitiveType.Triangles;
                return this;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public PiceMeal Triangles<T>(T[] triangles) where T: IBinaryInteger<T>
            {
                _drawMode = PrimitiveType.Triangles;
                if (triangles is uint[] uTriangles)
                    _elements = uTriangles;
                else
                {
                    var copy = new uint[triangles.Length];
                    for (int i = 0; i < triangles.Length; i++)
                        copy[i] = Scalar.As<T, uint>(triangles[i]);
                    _elements = copy;
                }

                return this;
            }
            
            public Mesh Finish()
            {
                int VertexCount()
                {
                    var kAttr = _attributes.First();
                    return (int)(kAttr.Value.Length / shader.FindAttribute(kAttr.Key).Count);
                }
                
                var gl = Window.GL;
                if(_attributes.Count != shader.AttributeCount)
                    throw  new ArgumentException("Not all vertexes have all data specified");
                var vertexCount = VertexCount();
                var vertSize = 0;
                foreach (ref var attribute in shader.EnumAttrib())
                    vertSize += (int)(attribute.Type.SizeOf * attribute.Count);
                Span<byte> buffer = stackalloc byte[vertSize * vertexCount];
                CompileVertexData(buffer, shader, _dataMode, _attributes, (uint)vertexCount);
                var vbo = gl.CreateBuffer();
                gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
                gl.BufferData(BufferTargetARB.ArrayBuffer, buffer, BufferUsageARB.StaticDraw);
                gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

                if (_elements == null)
                    return new(shader, vbo, null, _drawMode, _dataMode, (uint)vertexCount, null);
                
                var ebo = gl.CreateBuffer();
                gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, _elements, BufferUsageARB.StaticDraw);
                gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

                return new(shader, vbo, ebo, _drawMode, _dataMode, (uint)vertexCount, (uint)_elements.Length);
            }
        }
    }
}