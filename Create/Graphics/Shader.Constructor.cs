using System.Diagnostics.CodeAnalysis;
using Silk.NET.OpenGL;
using ShaderParam = Silk.NET.OpenGL.ShaderParameterName;

namespace Create.Graphics;

partial class Shader
{
    /// <summary>
    /// Begins the creation of the shader
    /// </summary>
    /// <returns>A new struct <see cref="Constructor"/> for <see cref="Shader"/> creation.</returns>
    public static Constructor Create() => new();
    
    /// <summary>
    /// The constructor for a <see cref="Shader"/>
    /// </summary>
    public class Constructor
    {
        /// Stores the vertex code for this shader. Eather as a stream to be loaded during compilation or as a preloaded string
        private Union<string, (Stream, bool close)>? _vertex;
        /// Stores the fragment code for this shader. Eather as a stream to be loaded during compilation or as a preloaded string
        private Union<string, (Stream, bool close)>? _fragment;
        /// Optional name for this shader if none is set, will default to #programId
        private string? _name;
        /// If for some reason you need to set specific locations on fragment shader outputs
        private readonly List<(string name, uint index)> _fragOutputs = [];
        private string? _model, _view, _projection;

        /// <summary>
        /// Sets a code for the vertex stage of the <see cref="Shader"/>
        /// </summary>
        /// <param name="content">Vertex Shader Code</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> is null</exception>
        public Constructor Vertex(string content) { _vertex = content ?? throw new ArgumentNullException(nameof(content)); return this; }
        /// <summary>
        /// Sets a Stream to load code of Vertex Shader, will be only loaded when the <see cref="Shader"/> is compiled.
        /// </summary>
        /// <param name="input">The source of Vertex Shader Code</param>
        /// <param name="shouldClose">A flag telling if the <paramref name="input"/> should be automatically closed</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="input"/> is null</exception>
        public Constructor Vertex(Stream input, bool shouldClose = false) { _vertex = (input ?? throw new ArgumentNullException(nameof(input)), shouldClose); return this; }

        /// <summary>
        /// Sets a code for the fragment stage of the <see cref="Shader"/>
        /// </summary>
        /// <param name="content">Fragment Shader Code</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="content"/> is null</exception>
        public Constructor Fragment(string content) { _fragment = content ?? throw new ArgumentNullException(nameof(content)); return this; }
        /// <summary>
        /// Sets a Stream to load code of Fragment Shader, will be only loaded when the <see cref="Shader"/> is compiled.
        /// </summary>
        /// <param name="input">The source of Fragment Shader Code</param>
        /// <param name="shouldClose">A flag telling if the <paramref name="input"/> should be automatically closed</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="input"/> is null</exception>
        public Constructor Fragment(Stream input, bool shouldClose = false) { _fragment = (input ?? throw new ArgumentNullException(nameof(input)), shouldClose);  return this; }
        
        /// <summary>
        /// Specifies the name of this shader is none is set will default to <c>#programID</c>
        /// <br/><br/>
        /// Example: <c>#3</c>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor Name(string name) { _name = name ?? throw new ArgumentNullException(); return this; }

        public Constructor BindFragmentOutput(string name, uint index)
        {
            var ind = _fragOutputs.IndexOf(b => b.name == name);
            if(ind == -1)
                _fragOutputs.Add((name, index));
            else
                _fragOutputs[ind] = (name, index);
            
            return this;
        }

        /// <summary>
        /// Specifies which uniform contains the Model Matrix.<br/>
        /// If none is set, the compiler will automatically look for valid uniform named <c>model</c>.<br/>
        /// To specify that there is no uniform for that purpose, pass <see langword="null"/>.
        /// </summary>
        /// <param name="name">Name of the Uniform, or <c>null</c> if there is none.</param>
        public Constructor SpecifyModelMatrix(string? name)
        {
            _model = name ?? "";
            return this;
        }

        /// <summary>
        /// Specifies which uniform contains the View Matrix.<br/>
        /// If none is set, the compiler will automatically look for valid uniform named <c>view</c>.<br/>
        /// To specify that there is no uniform for that purpose, pass <see langword="null"/>.
        /// </summary>
        /// <param name="name">Name of the Uniform, or <c>null</c> if there is none.</param>
        public Constructor SpecifyViewMatrix(string? name)
        {
            _view = name ?? "";
            return this;
        }
        
        /// <summary>
        /// Specifies which uniform contains the Projection Matrix.<br/>
        /// If none is set, the compiler will automatically look for valid uniform named <c>projection</c>.<br/>
        /// To specify that there is no uniform for that purpose, pass <see langword="null"/>.
        /// </summary>
        /// <param name="name">Name of the Uniform, or <c>null</c> if there is none.</param>
        public Constructor SpecifyProjectionMatrix(string? name)
        {
            _projection = name;
            return this;
        }
        
        /// <summary>
        /// Compiles and returns the <see cref="Shader"/> from all data specified
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the current thread is not connected to OpenGL</exception>
        /// <exception cref="ShaderCompilationException">If the Shader Compilation fails</exception>
        public Shader Finish()
        {
            if(!Window.HasGL)
                throw new InvalidOperationException("The current thread is not connected with OpenGL.");
            var gl = Window.GL;

            var vertexCode = GetCodeFrom(_vertex, VERTEX_SHADER);
            var fragmentCode = GetCodeFrom(_fragment, FRAGMENT_SHADER);

            var vertex = CompileShader(vertexCode, ShaderType.VertexShader);
            var fragment = CompileShader(fragmentCode, ShaderType.FragmentShader);

            if(vertex.IsError ||  fragment.IsError)
            {
                var error = vertex.IsError ? "\nVertex error:\n" + vertex.AsError.Value : "";
                error += fragment.IsError ? "\nFragment error:\n" + fragment.AsError.Value : "";
                throw new ShaderCompilationException(error);
            }
            
            var combined = CombineShaders(vertex.AsSuccess, fragment.AsSuccess);
            
            return new(combined, _name, gl, _model, _view, _projection);
            
            // Helper methods
            string GetCodeFrom(Union<string, (Stream, bool close)>? content, string name)
            {
                if(!content.HasValue)
                    throw new InvalidOperationException($"The {name} shader was not specified.");
                
                return content.Value.Match(s => s, stream =>
                {
                    using var reader = new StreamReader(stream.Item1);
                    var code = reader.ReadToEnd();
                    if(stream.close)
                        stream.Item1.Close();
                    return code;
                });
            }

            Result<uint> CompileShader(string code, ShaderType type)
            {
                var handle = gl.CreateShader(type);
                gl.ShaderSource(handle, code);
                gl.CompileShader(handle);
                
                if (gl.GetShader(handle, ShaderParam.CompileStatus) != 0) return handle;
                
                var error = gl.GetShaderInfoLog(handle);
                gl.DeleteShader(handle);
                return new Error<string>(error);
            }

            [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
            uint CombineShaders(uint vertex, uint fragment)
            {
                var program = gl.CreateProgram();
                gl.AttachShader(program, vertex);
                gl.AttachShader(program, fragment);

                BindFragmentOutputs(program);
                
                gl.LinkProgram(program);

                if (gl.GetProgram(program, ProgramPropertyARB.LinkStatus) == 0)
                {
                    var error = gl.GetProgramInfoLog(program);
                    throw new ShaderCompilationException("Shader linking error:\n" + error);
                }
            
                gl.DetachShader(program, vertex);
                gl.DeleteShader(vertex);
            
                gl.DetachShader(program, fragment);
                gl.DeleteShader(fragment);
                
                return program;
            }

            void BindFragmentOutputs(uint program)
            {
                foreach(var output in _fragOutputs)
                    gl.BindFragDataLocation(program, output.index,  output.name);
            }
        }
    }
}