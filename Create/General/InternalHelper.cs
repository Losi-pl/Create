using Silk.NET.OpenGL;
using Shader = Create.Graphics.Shader;

namespace Create.General;

internal static class InternalHelper
{
    extension(Shader.Uniform[] uniforms)
    {
        public ref Shader.Uniform Find(string name)
        {
            foreach (var i in uniforms.Length)
            {
                if (uniforms[i].Name == name)
                    return ref uniforms[i];
            }
            throw new KeyNotFoundException($"Uniform \"{name}\" not found.");
        }
    }

    extension(UniformType type)
    {
        public bool IsObject => type switch
        {
            UniformType.Sampler1D => true,
            UniformType.Sampler2D => true,
            UniformType.Sampler3D => true,
            UniformType.SamplerCube => true,
            UniformType.Sampler1DShadow => true,
            UniformType.Sampler2DShadow => true,
            UniformType.Sampler2DRect => true,
            UniformType.Sampler2DRectShadow => true,
            UniformType.Sampler1DArray => true,
            UniformType.Sampler2DArray => true,
            UniformType.SamplerBuffer => true,
            UniformType.Sampler1DArrayShadow => true,
            UniformType.Sampler2DArrayShadow => true,
            UniformType.SamplerCubeShadow => true,
            UniformType.IntSampler1D => true,
            UniformType.IntSampler2D => true,
            UniformType.IntSampler3D => true,
            UniformType.IntSamplerCube => true,
            UniformType.IntSampler2DRect => true,
            UniformType.IntSampler1DArray => true,
            UniformType.IntSampler2DArray => true,
            UniformType.IntSamplerBuffer => true,
            UniformType.UnsignedIntSampler1D => true,
            UniformType.UnsignedIntSampler2D => true,
            UniformType.UnsignedIntSampler3D => true,
            UniformType.UnsignedIntSamplerCube => true,
            UniformType.UnsignedIntSampler2DRect => true,
            UniformType.UnsignedIntSampler1DArray => true,
            UniformType.UnsignedIntSampler2DArray => true,
            UniformType.UnsignedIntSamplerBuffer => true,
            UniformType.SamplerCubeMapArray => true,
            UniformType.SamplerCubeMapArrayShadow => true,
            UniformType.IntSamplerCubeMapArray => true,
            UniformType.UnsignedIntSamplerCubeMapArray => true,
            UniformType.Sampler2DMultisample => true,
            UniformType.IntSampler2DMultisample => true,
            UniformType.UnsignedIntSampler2DMultisample => true,
            UniformType.Sampler2DMultisampleArray => true,
            UniformType.IntSampler2DMultisampleArray => true,
            UniformType.UnsignedIntSampler2DMultisampleArray => true,
            
            
            UniformType.Int => false,
            UniformType.UnsignedInt => false,
            UniformType.Float => false,
            UniformType.Double => false,
            UniformType.FloatVec2 => false,
            UniformType.FloatVec3 => false,
            UniformType.FloatVec4 => false,
            UniformType.IntVec2 => false,
            UniformType.IntVec3 => false,
            UniformType.IntVec4 => false,
            UniformType.Bool => false,
            UniformType.BoolVec2 => false,
            UniformType.BoolVec3 => false,
            UniformType.BoolVec4 => false,
            UniformType.FloatMat2 => false,
            UniformType.FloatMat3 => false,
            UniformType.FloatMat4 => false,
            UniformType.DoubleMat2 => false,
            UniformType.DoubleMat3 => false,
            UniformType.DoubleMat4 => false,
            UniformType.DoubleMat2x3 => false,
            UniformType.DoubleMat2x4 => false,
            UniformType.DoubleMat3x2 => false,
            UniformType.DoubleMat3x4 => false,
            UniformType.DoubleMat4x2 => false,
            UniformType.DoubleMat4x3 => false,
            UniformType.DoubleVec2 => false,
            UniformType.DoubleVec3 => false,
            UniformType.DoubleVec4 => false,
            UniformType.FloatMat2x3 => false,
            UniformType.FloatMat2x4 => false,
            UniformType.FloatMat3x2 => false,
            UniformType.FloatMat3x4 => false,
            UniformType.FloatMat4x2 => false,
            UniformType.FloatMat4x3 => false,
            UniformType.UnsignedIntVec2 => false,
            UniformType.UnsignedIntVec3 => false,
            UniformType.UnsignedIntVec4 => false,

            
            _ => throw new ArgumentOutOfRangeException(null, type, null)
        };
        
        public TextureTarget TextureType => type switch
        {
            UniformType.Sampler1D => TextureTarget.Texture1D,
            UniformType.Sampler2D => TextureTarget.Texture2D,
            UniformType.Sampler3D => TextureTarget.Texture3D,
            UniformType.SamplerCube => TextureTarget.TextureCubeMap,
            UniformType.Sampler1DArray => TextureTarget.Texture1DArray,
            UniformType.Sampler2DArray => TextureTarget.Texture2DArray,
            UniformType.SamplerBuffer => TextureTarget.TextureBuffer,
            UniformType.Sampler1DShadow => TextureTarget.Texture1D,
            UniformType.Sampler2DShadow => TextureTarget.Texture2D,
            UniformType.SamplerCubeShadow => TextureTarget.TextureCubeMap,
            UniformType.Sampler1DArrayShadow => TextureTarget.Texture1DArray,
            UniformType.Sampler2DArrayShadow => TextureTarget.Texture2DArray,
            UniformType.IntSampler1D => TextureTarget.Texture1D,
            UniformType.IntSampler2D => TextureTarget.Texture2D,
            UniformType.IntSampler3D => TextureTarget.Texture3D,
            UniformType.IntSamplerCube => TextureTarget.TextureCubeMap,
            UniformType.IntSampler1DArray => TextureTarget.Texture1DArray,
            UniformType.IntSampler2DArray => TextureTarget.Texture2DArray,
            UniformType.IntSamplerBuffer => TextureTarget.TextureBuffer,
            UniformType.UnsignedIntSampler1D => TextureTarget.Texture1D,
            UniformType.UnsignedIntSampler2D => TextureTarget.Texture2D,
            UniformType.UnsignedIntSampler3D => TextureTarget.Texture3D,
            UniformType.UnsignedIntSamplerCube => TextureTarget.TextureCubeMap,
            UniformType.UnsignedIntSampler1DArray => TextureTarget.Texture1DArray,
            UniformType.UnsignedIntSampler2DArray => TextureTarget.Texture2DArray,
            UniformType.UnsignedIntSamplerBuffer => TextureTarget.TextureBuffer,
            
            _ => throw new ArgumentException($"Unsupported UniformType: {type}")
        };
    }
}