using System.Diagnostics.CodeAnalysis;
using Create.General;
using Silk.NET.OpenGL;
using OneOf.Types;
using OneOf;

namespace Create.Graphics;

public sealed partial class Shader
{
    internal uint Handle { get; }
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string Name { get; }
    public override string ToString() => Name;

    private Uniform[] uniforms;
    private Attribute[] attributes;
    
    private Shader(uint handle, string? name, GL gl)
    {
        Handle = handle;
        Name = name ?? $"Shader #{handle}";

        uniforms = new Uniform[gl.GetProgram(handle,ProgramPropertyARB.ActiveUniforms)];
        attributes = new Attribute[gl.GetProgram(handle, ProgramPropertyARB.ActiveAttributes)];

        foreach (var i in (uint)uniforms.Length)
        {
            var uName = gl.GetActiveUniform(handle, i, out var size, out var type);
            var location = gl.GetUniformLocation(Handle, uName);
            uniforms[i] = new Uniform(uName, location, type, new None(), (uint)size);
        }

        foreach (var i in (uint)attributes.Length)
        {
            var uName = gl.GetActiveAttrib(handle, i, out var size, out var type);
            var location = gl.GetAttribLocation(Handle, uName);
            attributes[i] = new Attribute(uName, location, type, (uint)size);
        }
    }
    
    internal struct Uniform(string name, int location, UniformType type, OneOf<uint, None> objectIndex, uint count)
    {
        /// <summary>
        /// Human name for this uniform
        /// </summary>
        public readonly string Name = name;
        /// <summary>
        /// The address by which the driver recognizes that uniform
        /// </summary>
        public readonly int Location = location;
        /// <summary>
        /// Type of the uniform content
        /// </summary>
        public readonly UniformType Type = type;
        /// <summary>
        /// Whether this uniform is meant for storing another OpenGL object like a Sampler or Image
        /// </summary>
        public readonly OneOf<uint, None> ObjectIndex = objectIndex;
        /// <summary>
        /// The count of values in this uniform
        /// </summary>
        public readonly uint Count = count;
        
        public readonly bool IsArray => Count > 1;
    }
    
    internal struct Attribute(string name, int location, AttributeType type, uint count)
    {
        /// <summary>
        /// Human name for this attribute
        /// </summary>
        public readonly string Name = name;
        /// <summary>
        /// The address by which the driver recognizes that attribute
        /// </summary>
        public readonly int Location = location;
        /// <summary>
        /// Type of the attribute content
        /// </summary>
        public readonly AttributeType Type = type;
        /// <summary>
        /// The count of values in this uniform
        /// </summary>
        public readonly uint Count = count;
    }
}