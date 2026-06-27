using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.Graphics;

partial class Mesh
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static void CompileVertexData(Span<byte> content, Shader shader, DataLayout mode, Dictionary<string, Array> attributes, uint vertexCount)
    { //TODO: Figure out what to do with the Matrix's Row major order 
        int attribIndex = 0;

        foreach (ref var attribute in shader.EnumAttrib())
        {
            var array = attributes[attribute.Name];
            if(array.Length != vertexCount * attribute.Count)
                throw new ArgumentException("Not some vertexes dont have specified data");
        }
        
        foreach (ref var attribute in shader.EnumAttrib())
        {
            var str = GetStride(ref attribute);
            var off = GetOffset(ref attribute);
            var array = attributes[attribute.Name];
            switch (attribute.Type)
            {
                case TypeTransformations.Byte:          InsertData((sbyte[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.UnsignedByte:  InsertData( (byte[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.Short:         InsertData( (short[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.UnsignedShort: InsertData((ushort[])array, attribute.Count, off, str, content); break;
                case AttributeType.Int:                 InsertData( (int[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedInt:         InsertData((uint[])array, attribute.Count, off, str, content); break;
                case AttributeType.Int64Arb:            InsertData( (long[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedInt64Arb:    InsertData((ulong[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.HalfFloat:     InsertData((Half[])array, attribute.Count, off, str, content); break;
                case AttributeType.Float:               InsertData((float[])array, attribute.Count, off, str, content); break;
                case AttributeType.Double:              InsertData((double[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.IntVec2: InsertData((Vector2D<int>[])array, attribute.Count, off, str, content); break;
                case AttributeType.IntVec3: InsertData((Vector3D<int>[])array, attribute.Count, off, str, content); break;
                case AttributeType.IntVec4: InsertData((Vector4D<int>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.UnsignedIntVec2: InsertData((Vector2D<uint>[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedIntVec3: InsertData((Vector3D<uint>[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedIntVec4: InsertData((Vector4D<uint>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.Int64Vec2Arb: InsertData((Vector2D<long>[])array, attribute.Count, off, str, content); break;
                case AttributeType.Int64Vec3Arb: InsertData((Vector3D<long>[])array, attribute.Count, off, str, content); break;
                case AttributeType.Int64Vec4Arb: InsertData((Vector4D<long>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.UnsignedInt64Vec2Arb: InsertData((Vector2D<ulong>[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedInt64Vec3Arb: InsertData((Vector3D<ulong>[])array, attribute.Count, off, str, content); break;
                case AttributeType.UnsignedInt64Vec4Arb: InsertData((Vector4D<ulong>[])array, attribute.Count, off, str, content); break;
                
                case TypeTransformations.HalfFloatVec2: InsertData((Vector2D<Half>[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.HalfFloatVec3: InsertData((Vector3D<Half>[])array, attribute.Count, off, str, content); break;
                case TypeTransformations.HalfFloatVec4: InsertData((Vector4D<Half>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.FloatVec2: InsertData((Vector2D<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatVec3: InsertData((Vector3D<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatVec4: InsertData((Vector4D<float>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.DoubleVec2: InsertData((Vector2D<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleVec3: InsertData((Vector3D<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleVec4: InsertData((Vector4D<double>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.FloatMat2:   InsertData((Matrix2X2<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat2x3: InsertData((Matrix2X3<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat2x4: InsertData((Matrix2X4<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat3x2: InsertData((Matrix3X2<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat3:   InsertData((Matrix3X3<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat3x4: InsertData((Matrix3X4<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat4x2: InsertData((Matrix4X2<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat4x3: InsertData((Matrix4X3<float>[])array, attribute.Count, off, str, content); break;
                case AttributeType.FloatMat4:   InsertData((Matrix4X4<float>[])array, attribute.Count, off, str, content); break;
                
                case AttributeType.DoubleMat2:   InsertData((Matrix2X2<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat2x3: InsertData((Matrix2X3<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat2x4: InsertData((Matrix2X4<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat3x2: InsertData((Matrix3X2<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat3:   InsertData((Matrix3X3<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat3x4: InsertData((Matrix3X4<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat4x2: InsertData((Matrix4X2<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat4x3: InsertData((Matrix4X3<double>[])array, attribute.Count, off, str, content); break;
                case AttributeType.DoubleMat4:   InsertData((Matrix4X4<double>[])array, attribute.Count, off, str, content); break;
            }
        }
        
        //Methods
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        int GetOffset(ref Shader.Attribute attrib)
        {
            switch (mode)
            {
                default:
                case DataLayout.Interleaved:
                {
                    var c = attribIndex;
                    attribIndex += (int)(attrib.Type.SizeOf * attrib.Count);
                    return c;
                }
                case DataLayout.NonInterleaved:
                {
                    var c = attribIndex;
                    attribIndex += (int)(attrib.Type.SizeOf * attrib.Count * vertexCount);
                    return c;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        int GetStride(ref Shader.Attribute attrib)
        {
            switch (mode)
            {
                default:
                case DataLayout.Interleaved:
                    int vertexSize = 0;
                    foreach (ref var attribute in shader.EnumAttrib())
                        vertexSize += (int)(attribute.Type.SizeOf * attribute.Count);
                    return vertexSize;
                
                case DataLayout.NonInterleaved:
                    return (int)(attrib.Type.SizeOf * attrib.Count);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        void InsertData<T>(T[] data, uint count, int offset, int stride, Span<byte> dest) where T : struct
        {
            //data - the set of data to be translated to the destination
            //count - the size of stets to be put together
            //offset - self-explanatory
            //stride - the stride oh how much a set of data have to be offset from another
            //dest - destination for the data
            
            if (mode == DataLayout.NonInterleaved)
            {
                var span = MemoryMarshal.AsBytes(data);
                span.CopyTo(dest[offset..(offset + span.Length)]);
            }
            else if(mode == DataLayout.Interleaved)
            {
                var dataSpan = new Span<T>(data);
                for (var i = 0; i < data.Length / count; i++)
                {
                    var myOff = offset + stride * i;
                    var elem = MemoryMarshal.AsBytes(dataSpan[(i * (int)count)..((i + 1) * (int)count)]);
                    elem.CopyTo(dest[myOff..(myOff + elem.Length)]);
                }
            }
            else throw new Exception("What is happening right now!?");
        }
    }
}