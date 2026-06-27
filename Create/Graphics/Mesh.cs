using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.Graphics;

public sealed partial class Mesh
{
    private readonly Shader _shader;
    private readonly uint _vbo;
    private readonly uint? _ebo;
    private readonly PrimitiveType _drawMode;
    private readonly DataLayout _dataLayout;
    private readonly uint _vertexCount;
    private readonly uint _drawCount;
    private (uint vao, GL context)? _binding;

    private Mesh(Shader shader, uint vbo, uint? ebo, PrimitiveType drawMode, DataLayout  dataLayout, uint vertexCount, uint? elementCount)
    {
        ArgumentNullException.ThrowIfNull(shader);

        _shader = shader;
        _vbo = vbo;
        _ebo = ebo;
        _drawMode = drawMode;
        _vertexCount = vertexCount;
        _drawCount = elementCount ?? _vertexCount;
        _dataLayout = dataLayout;
    }
    
    public Shader Shader => _shader;
    
    public static Constructor Create(Shader shader) => new(shader);
    
    public enum DataLayout { Interleaved, NonInterleaved }

    public Mesh ThreadBind()
    {
        if (_binding.HasValue)
        {
            // ReSharper disable once InconsistentNaming
            var currentGL = Window.GL;
            if(_binding.Value.context == currentGL)
                return this;
            throw new InvalidOperationException("This Mesh is already bound to nother Thread");
        }

        var gl = Window.GL;
        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);
        
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        if(_ebo.HasValue)
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo.Value);
        
        gl.UseProgram(_shader.Handle);
        nint offset = 0;
        switch (_dataLayout)
        {
            case DataLayout.Interleaved:
            {
                var stride = 0u;
                foreach (var attribute in _shader.EnumAttrib())
                    stride += (uint)(attribute.Type.SizeOf * attribute.Count);
                foreach (var attribute in _shader.EnumAttrib())
                {
                    gl.VertexAttribPointer(attribute, stride, offset);
                    offset += (nint)(attribute.Type.SizeOf * attribute.Count);
                }

                break;
            }
            case DataLayout.NonInterleaved:
            {
                foreach (var attribute in _shader.EnumAttrib())
                {
                    gl.VertexAttribPointer(attribute, (uint)attribute.Type.SizeOf, offset);
                    offset += (nint)(attribute.Type.SizeOf * attribute.Count * _vertexCount);
                }

                break;
            }
            default:
                throw new ArgumentException("Wait a minute,... who, are you?");
        }
        gl.BindVertexArray(0);
        _binding = (vao, gl);
        return this;
    }

    public void Draw()
    {
        if (!_binding.HasValue)
            throw new InvalidOperationException("This Mesh is not bound to the current thread");
        var gl = Window.GL;
        if(gl != _binding.Value.context)
            throw new InvalidOperationException("This Mesh is not bound to the current thread");
            
        gl.UseProgram(_shader.Handle);
        gl.BindVertexArray(_binding.Value.vao);
        if(_ebo.HasValue)
            unsafe { gl.DrawElements(_drawMode, _drawCount, DrawElementsType.UnsignedInt, (void*)0); }
        else
            gl.DrawArrays(_drawMode, 0, _drawCount);
        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
internal static class TypeTransformations
{
    public const AttributeType HalfFloat = (AttributeType)GLEnum.HalfFloat;
    public const AttributeType HalfFloatVec2 = (AttributeType)0x8FF9;
    public const AttributeType HalfFloatVec3 = (AttributeType)0x8FFA;
    public const AttributeType HalfFloatVec4 = (AttributeType)0x8FFB;
    public const AttributeType Byte = (AttributeType)GLEnum.Byte;
    public const AttributeType UnsignedByte = (AttributeType)GLEnum.UnsignedByte;
    public const AttributeType Short = (AttributeType)GLEnum.Short;
    public const AttributeType UnsignedShort = (AttributeType)GLEnum.UnsignedShort;

    extension(GL gl)
    {
        public void VertexAttribPointer(Shader.Attribute attribute, uint stride, nint offset)
        {
            var primitive = attribute.Type.Primitive;
            var dimensions = attribute.Type.Dimensions;
            var sizeOfPrimitive = primitive.SizeOf;
            for(int a = 0; a < attribute.Count; a++)
                for (int c = 0; c < dimensions.X; c++)
                    gl.EnableVertexAttribArray((uint)(attribute.Location + (dimensions.X * a + c)));
            switch (primitive)
            {
                case VertexAttribPointerType.Double:
                    unsafe
                    {
                        for(int a = 0; a < attribute.Count; a++)
                            for (int c = 0; c < dimensions.X; c++)
                                gl.VertexAttribLPointer(index: (uint)(attribute.Location + (dimensions.X * a + c)), 
                                                         size: dimensions.Y, 
                                                         type: VertexAttribLType.Double, 
                                                       stride: stride, 
                                                      pointer: (void*)(offset + (dimensions.X * a + c) * sizeOfPrimitive));
                        break;
                    }
                case VertexAttribPointerType.HalfFloat:
                case VertexAttribPointerType.Float:
                case VertexAttribPointerType.Fixed:
                    for(var a = 0; a < attribute.Count; a++)
                        for (var c = 0; c < dimensions.X; c++)
                            gl.VertexAttribPointer(index: (uint)(attribute.Location + (dimensions.X * a + c)),
                                                    size: dimensions.Y, 
                                                    type: primitive, false, 
                                                  stride: stride,
                                                 pointer: offset + (dimensions.X * a + c) * sizeOfPrimitive);
                    break;
                default:
                    for(var a = 0; a < attribute.Count; a++)
                        for (var c = 0; c < dimensions.X; c++)
                            gl.VertexAttribIPointer(index: (uint)(attribute.Location + (dimensions.X * a + c)),
                                                     size: dimensions.Y, 
                                                     type: (VertexAttribIType)primitive, 
                                                   stride: stride,
                                                  pointer: offset + (dimensions.X * a + c) * sizeOfPrimitive);
                    break;
            }
        }
    }
    
    extension(AttributeType type)
    {
        public bool IsVector => type switch
        {
            >= HalfFloatVec2 and <= HalfFloatVec4 => true,
            >= AttributeType.IntVec2 and <= AttributeType.IntVec4 => true,
            >= AttributeType.UnsignedIntVec2 and <= AttributeType.UnsignedIntVec4 => true,
            >= AttributeType.Int64Vec2Arb and <= AttributeType.Int64Vec4Arb => true,
            >= AttributeType.UnsignedInt64Vec2Arb and <= AttributeType.UnsignedInt64Vec4Arb => true,
            >= AttributeType.FloatVec2 and <= AttributeType.FloatVec4 => true,
            >= AttributeType.DoubleVec2 and <= AttributeType.DoubleVec4 => true,

            _ => false
        };

        public bool IsMatrix => type switch
        {
            >= AttributeType.FloatMat2 and <= AttributeType.FloatMat4x3 => true,
            >= AttributeType.DoubleMat2 and <= AttributeType.DoubleMat4x3 => true,
            _ => false
        };

        public VertexAttribPointerType Primitive => type switch
        {
            Byte => VertexAttribPointerType.Byte,
            UnsignedByte => VertexAttribPointerType.UnsignedByte,
            Short => VertexAttribPointerType.Short,
            UnsignedShort => VertexAttribPointerType.UnsignedShort,
            AttributeType.Int => VertexAttribPointerType.Int,
            AttributeType.UnsignedInt => VertexAttribPointerType.UnsignedInt,
            AttributeType.Int64Arb => VertexAttribPointerType.Int64Arb,
            AttributeType.UnsignedInt64Arb => VertexAttribPointerType.UnsignedInt64Arb,
            HalfFloat => VertexAttribPointerType.HalfFloat,
            AttributeType.Float => VertexAttribPointerType.Float,
            AttributeType.Double => VertexAttribPointerType.Double,
            
            >= HalfFloatVec2 and <= HalfFloatVec4 => VertexAttribPointerType.HalfFloat,
            >= AttributeType.IntVec2 and <= AttributeType.IntVec4 => VertexAttribPointerType.Int,
            >= AttributeType.UnsignedIntVec2 and <= AttributeType.UnsignedIntVec4 => VertexAttribPointerType.UnsignedInt,
            >= AttributeType.Int64Vec2Arb and <= AttributeType.Int64Vec4Arb => VertexAttribPointerType.Int64Arb,
            >= AttributeType.UnsignedInt64Vec2Arb and <= AttributeType.UnsignedInt64Vec4Arb => VertexAttribPointerType.UnsignedInt64Arb,
            >= AttributeType.FloatVec2 and <= AttributeType.FloatVec4 => VertexAttribPointerType.Float,
            >= AttributeType.DoubleVec2 and <= AttributeType.DoubleVec4 => VertexAttribPointerType.Double,
            
            >= AttributeType.FloatMat2 and <= AttributeType.FloatMat4 => VertexAttribPointerType.Float,
            >= AttributeType.FloatMat2x3 and <= AttributeType.FloatMat4x3 => VertexAttribPointerType.Float,
            
            >= AttributeType.DoubleMat2 and <= AttributeType.DoubleMat4 => VertexAttribPointerType.Double,
            >= AttributeType.DoubleMat2x3 and <= AttributeType.DoubleMat4x3 => VertexAttribPointerType.Double,
            
            _ => VertexAttribPointerType.Int
        };

        public Vector2D<byte> Dimensions => type switch
        {
            AttributeType.IntVec2 => new(1, 2),
            AttributeType.IntVec3 => new(1, 3),
            AttributeType.IntVec4 => new(1, 4),

            AttributeType.UnsignedIntVec2 => new(1, 2),
            AttributeType.UnsignedIntVec3 => new(1, 3),
            AttributeType.UnsignedIntVec4 => new(1, 4),

            AttributeType.Int64Vec2Arb => new(1, 2),
            AttributeType.Int64Vec3Arb => new(1, 3),
            AttributeType.Int64Vec4Arb => new(1, 4),

            AttributeType.UnsignedInt64Vec2Arb => new(1, 2),
            AttributeType.UnsignedInt64Vec3Arb => new(1, 3),
            AttributeType.UnsignedInt64Vec4Arb => new(1, 4),

            AttributeType.FloatVec2 => new(1, 2),
            AttributeType.FloatVec3 => new(1, 3),
            AttributeType.FloatVec4 => new(1, 4),

            AttributeType.DoubleVec2 => new(1, 2),
            AttributeType.DoubleVec3 => new(1, 3),
            AttributeType.DoubleVec4 => new(1, 4),

            HalfFloatVec2 => new(1, 2),
            HalfFloatVec3 => new(1, 3),
            HalfFloatVec4 => new(1, 4),

            AttributeType.FloatMat2 => new(2, 2),
            AttributeType.FloatMat2x3 => new(2, 3),
            AttributeType.FloatMat2x4 => new(2, 4),
            AttributeType.FloatMat3x2 => new(3, 2),
            AttributeType.FloatMat3 => new(3, 3),
            AttributeType.FloatMat3x4 => new(3, 4),
            AttributeType.FloatMat4x2 => new(4, 2),
            AttributeType.FloatMat4x3 => new(4, 3),
            AttributeType.FloatMat4 => new(4, 4),

            AttributeType.DoubleMat2 => new(2, 2),
            AttributeType.DoubleMat2x3 => new(2, 3),
            AttributeType.DoubleMat2x4 => new(2, 4),
            AttributeType.DoubleMat3x2 => new(3, 2),
            AttributeType.DoubleMat3 => new(3, 3),
            AttributeType.DoubleMat3x4 => new(3, 4),
            AttributeType.DoubleMat4x2 => new(4, 2),
            AttributeType.DoubleMat4x3 => new(4, 3),
            AttributeType.DoubleMat4 => new(4, 4),

            _ => new(1, 1)
        };

        public int SizeOf
        {
            get
            {
                var dim = type.Dimensions;
                var primSizeOf = type.Primitive switch
                {
                    VertexAttribPointerType.Byte => sizeof(sbyte),
                    VertexAttribPointerType.UnsignedByte => sizeof(byte),
                    VertexAttribPointerType.Short => sizeof(short),
                    VertexAttribPointerType.UnsignedShort => sizeof(ushort),
                    VertexAttribPointerType.Int => sizeof(int),
                    VertexAttribPointerType.UnsignedInt => sizeof(uint),
                    VertexAttribPointerType.Int64Arb => sizeof(long),
                    VertexAttribPointerType.UnsignedInt64Arb => sizeof(ulong),
                    VertexAttribPointerType.HalfFloat => Unsafe.SizeOf<Half>(),
                    VertexAttribPointerType.Float => sizeof(float),
                    VertexAttribPointerType.Double => sizeof(double),
                    
                    _ => sizeof(int)
                };

                return dim.X * dim.Y * primSizeOf;
            }
        }
    }

    extension(VertexAttribPointerType type)
    {
        public int SizeOf => type switch
        {
            VertexAttribPointerType.Byte => sizeof(sbyte),
            VertexAttribPointerType.UnsignedByte => sizeof(byte),
            VertexAttribPointerType.Short => sizeof(short),
            VertexAttribPointerType.UnsignedShort => sizeof(ushort),
            VertexAttribPointerType.Int => sizeof(int),
            VertexAttribPointerType.UnsignedInt => sizeof(uint),
            VertexAttribPointerType.Int64Arb => sizeof(long),
            VertexAttribPointerType.UnsignedInt64Arb => sizeof(long),
            VertexAttribPointerType.HalfFloat => Unsafe.SizeOf<Half>(),
            VertexAttribPointerType.Float => sizeof(float),
            VertexAttribPointerType.Double => sizeof(double),
            VertexAttribPointerType.Fixed => sizeof(uint),
            VertexAttribPointerType.UnsignedInt2101010Rev => sizeof(uint),
            VertexAttribPointerType.UnsignedInt10f11f11fRev => sizeof(uint),
            VertexAttribPointerType.Int2101010Rev => sizeof(int),
            _ => 4
        };
    }
}