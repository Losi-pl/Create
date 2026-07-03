using System.Diagnostics;
using CommunityToolkit.HighPerformance;
using Silk.NET.OpenGL;

namespace Create.Graphics;
[DebuggerDisplay("Shader: {Name, nq}")]
public sealed partial class Shader: IDisposable
{
    // ReSharper disable InconsistentNaming
    public const string MODEL_UNIFORM = "model";
    public const string VIEW_UNIFORM = "view";
    public const string PROJECTION_UNIFORM = "projection";

    public const string FRAGMENT_SHADER = "fragment";
    public const string VERTEX_SHADER = "vertex";
    // ReSharper restore InconsistentNaming
    
    /// The pointer to the Shader Program in OpenGL memory
    internal uint Handle => Disposed ? throw new InvalidOperationException("This Shader was disposed of") : field;

    /// The name of this Shader
    public string Name { get; }

    // ReSharper disable once MemberCanBePrivate.Global
    public bool Disposed { get; private set; }
    public override string ToString() => Name;

    /// Array of <see cref="Uniform"/>'s of this Shader
    private readonly Uniform[] _uniforms;
    /// Array of <see cref="Attribute"/>'s of this Shader
    private readonly Attribute[] _attributes;

    private readonly uint? _modelMat, _viewMat, _projectionMat;

    public bool HasModelMatrix => _modelMat.HasValue;
    public bool HasViewMatrix => _viewMat.HasValue;
    public bool HasProjectionMatrix => _projectionMat.HasValue;
    
    /// <summary>
    /// Connects an already compiled Shader Program with a C# wrapper
    /// </summary>
    /// <param name="handle">Pointer to the Shader in OpenGL</param>
    /// <param name="name">The name of this Shader, specified in <see cref="Constructor"/></param>
    /// <param name="gl">OpenGL context passed from the <see cref="Constructor"/> to continued processing without a need to acquire context again</param>
    /// <param name="modelMat">Name of possible Model Matrix if none is sett will see if there is valid <c>model</c> uniform</param>
    /// <param name="viewMat">Name of possible Model Matrix if none is sett will see if there is valid <c>view</c> uniform</param>
    /// <param name="projMat">Name of possible Model Matrix if none is sett will see if there is valid <c>projection</c> uniform</param>
    private Shader(uint handle, string? name, GL? gl, string? modelMat, string? viewMat, string? projMat)
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

        _modelMat = CheckMatrixUniform(modelMat,!string.IsNullOrEmpty(modelMat), MODEL_UNIFORM);
        _viewMat = CheckMatrixUniform(viewMat,!string.IsNullOrEmpty(viewMat), VIEW_UNIFORM);
        _projectionMat = CheckMatrixUniform(projMat,!string.IsNullOrEmpty(projMat), PROJECTION_UNIFORM);
    }

    private uint? CheckMatrixUniform(string? name, bool @throw, string goal)
    {
        name ??= goal;
        
        var index = _uniforms.IndexOf(u => u.Name == name);

        if(index == -1)
            return @throw ? throw new ArgumentException($"Uniform {name} not found") : null;
        
        ref var uniform = ref _uniforms[index];
        
        if (uniform.IsArray)
            return @throw ? throw new ArgumentException($"Uniform for {goal} Matrix must be a single value") : null;
        
        if(uniform.Type is not (UniformType.FloatMat4 or UniformType.DoubleMat4))
            return @throw ? throw new ArgumentException($"Uniform {name} is not a Matrix 4x4") : null;

        return (uint)_uniforms.IndexOf(in uniform);
    }

    /// <summary>
    /// Returns a pointer to the <see cref="Attribute"/> under the name <paramref name="name"/>
    /// </summary>
    /// <param name="name">Name of the searched <see cref="Attribute"/></param>
    /// <exception cref="KeyNotFoundException">If the attribute is not found</exception>
    internal ref Attribute FindAttribute(string name)
    {
        for (var i = 0; i < _attributes.Length; i++)
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
            // ReSharper disable once InconsistentNaming
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
        
        public bool IsArray => Count > 1;
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

    public void Dispose()
    {
        if (Disposed)
            return;
        Disposed = true;
        
        GC.SuppressFinalize(this);

        var handle = Handle;
        if (Window.HasGL)
            Window.GL.DeleteProgram(handle);
        else
            Window.Queue(() => Window.GL.DeleteProgram(handle));
    }
    
    ~Shader() => Dispose();
}