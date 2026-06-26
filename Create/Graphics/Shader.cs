using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Create.General;
using Silk.NET.OpenGL;
using OneOf.Types;
using OneOf;

namespace Create.Graphics;
[DebuggerDisplay("Shader: {Name, nq}")]
public sealed partial class Shader
{
    /// The pointer to the Shader Program in OpenGL memory
    internal uint Handle { get; }
    /// The name of this Shader
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string Name { get; }
    public override string ToString() => Name;

    /// Array of <see cref="Uniform"/>'s of this Shader
    private readonly Uniform[] _uniforms;
    /// Array of <see cref="Attribute"/>'s of this Shader
    private readonly Attribute[] _attributes;
    
    /// <summary>
    /// Connects an already compiled Shader Program with a C# wrapper
    /// </summary>
    /// <param name="handle">Pointer to the Shader in OpenGL</param>
    /// <param name="name">The name of this Shader, specyfied in <see cref="Constructor"/></param>
    /// <param name="gl">OpenGL context passed from the <see cref="Constructor"/> to continued processing without a need to acquire context again</param>
    private Shader(uint handle, string? name, GL? gl)
    {
        gl ??= Window.GL;
        Handle = handle;
        Name = name ?? $"#{handle}";

        _uniforms = new Uniform[gl.GetProgram(handle,ProgramPropertyARB.ActiveUniforms)];
        _attributes = new Attribute[gl.GetProgram(handle, ProgramPropertyARB.ActiveAttributes)];

        foreach (var i in (uint)_uniforms.Length)
        {
            var uName = gl.GetActiveUniform(handle, i, out var size, out var type);
            var location = gl.GetUniformLocation(Handle, uName);
            _uniforms[i] = new Uniform(uName, location, type, new None(), (uint)size);
        }

        foreach (var i in (uint)_attributes.Length)
        {
            var uName = gl.GetActiveAttrib(handle, i, out var size, out var type);
            var location = gl.GetAttribLocation(Handle, uName);
            _attributes[i] = new Attribute(uName, (uint)location, type, (uint)size);
        }
    }

    /// <summary>
    /// Returns a pointer to the <see cref="Attribute"/> under the name <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the searched <see cref="Attribute"/></param>
    /// <exception cref="KeyNotFoundException">If the attribute is not found</exception>
    internal ref Attribute FindAttribute(string name)
    {
        for (int i = 0; i < _attributes.Length; i++)
        {
            if(_attributes[i].Name == name)
                return ref _attributes[i];
        }

        throw new KeyNotFoundException($"There is no Attribute by name \"{name}\"");
    }

    /// The count of used attributes in this Shader
    internal int AttributeCount => _attributes.Length;
    /// <summary>
    /// Meant for use int <see langword="foreach"/> to enumerate over all Attributes in this Shader
    /// </summary>
    internal EnumerateAttributes EnumAttrib() => new(this);
    /// Used in <see cref="EnumAttrib"/>()
    internal readonly ref struct EnumerateAttributes(Shader shader)
    {
        public Enumerator GetEnumerator() => new(shader);
        
        public struct Enumerator(Shader shader)
        {
            private int index = -1;
            public bool MoveNext() => ++index < shader._attributes.Length;
            public ref Attribute Current => ref shader._attributes[index];
        }
    }
    
    /// <summary>
    /// All data of a Uniform actively used in this Shader
    /// </summary>
    internal readonly struct Uniform(string name, int location, UniformType type, OneOf<uint, None> objectIndex, uint count)
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
        /// The count of values in this uniform (array[count])
        /// </summary>
        public readonly uint Count = count;
        
        public readonly bool IsArray => Count > 1;
    }
    
    /// <summary>
    /// All data of an Attribute actively used in this Shader
    /// </summary>
    internal readonly struct Attribute(string name, uint location, AttributeType type, uint count)
    {
        /// <summary>
        /// Human name for this attribute
        /// </summary>
        public readonly string Name = name;
        /// <summary>
        /// The address by which the driver recognizes that attribute
        /// </summary>
        public readonly uint Location = location;
        /// <summary>
        /// Type of the attribute content
        /// </summary>
        public readonly AttributeType Type = type;
        /// <summary>
        /// The count of values in this attribute (array[count])
        /// </summary>
        public readonly uint Count = count;
    }
}