using System.Diagnostics.CodeAnalysis;
using Silk.NET.GLFW;
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
    public struct Constructor
    {
        /// Stores the vertex code for this shader. Eather as a stream to be loaded during compilation or as a preloaded string
        private OneOf<String, (Stream, bool close)>? _vertex;
        /// Stores the fragment code for this shader. Eather as a stream to be loaded during compilation or as a preloaded string
        private OneOf<String, (Stream, bool close)>? _fragment;
        /// Optional name for this shader if none is set, will default to #programId
        private string? _name;

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
        
        /// <summary>
        /// Compiles and returns the <see cref="Shader"/> from all data specified
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the current thread is not connected to OpenGL</exception>
        /// <exception cref="GlfwException">If the Shader Compilation fails</exception>
        public Shader Finish()
        {
            if(!Window.HasGL)
                throw new InvalidOperationException("The current thread is not connected with OpenGL.");
            var gl = Window.GL;

            var vertexCode = GetCodeFrom(_vertex, () => "vertex");
            var fragmentCode = GetCodeFrom(_fragment, () => "fragment");

            var vertex = CompileShader(vertexCode, ShaderType.VertexShader);
            var fragment = CompileShader(fragmentCode, ShaderType.FragmentShader);

            if(vertex.IsT1 ||  fragment.IsT1)
            {
                var error = vertex.IsT1 ? "\nVertex error:\n" + vertex.AsT1.Value : "";
                error += fragment.IsT1 ? "\nFragment error:\n" + fragment.AsT1.Value : "";
                throw new GlfwException(error);
            }
            
            var combined = CombineShaders(vertex.AsT0, fragment.AsT0);
            
            return new(combined, _name, gl);
            
            // Helper methods
            string GetCodeFrom(OneOf<String, (Stream, bool close)>? content, Func<string> name)
            {
                if(!content.HasValue)
                    throw new InvalidOperationException($"The {name()} shader was not specified.");
                
                return content.Value.Match(s => s, stream =>
                {
                    using var reader = new StreamReader(stream.Item1);
                    var code = reader.ReadToEnd();
                    if(stream.close)
                        stream.Item1.Close();
                    return code;
                });
            }

            OneOf<Result<uint>, Error<string>> CompileShader(string code, ShaderType type)
            {
                var handle = gl.CreateShader(type);
                gl.ShaderSource(handle, code);
                gl.CompileShader(handle);
                if (gl.GetShader(handle, ShaderParam.CompileStatus) == 0)
                {
                    Error<string> error = new(gl.GetShaderInfoLog(handle));
                    gl.DeleteShader(handle);
                    return error;
                }
                
                return new Result<uint>(handle);
            }

            [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
            uint CombineShaders(Result<uint> vertex, Result<uint> fragment)
            {
                var program = gl.CreateProgram();
                gl.AttachShader(program, vertex.Value);
                gl.AttachShader(program, fragment.Value);

                gl.LinkProgram(program);

                if (gl.GetProgram(program, ProgramPropertyARB.LinkStatus) == 0)
                {
                    var error = gl.GetProgramInfoLog(program);
                    throw new GlfwException("Shader linking error:\n" + error);
                }
            
                gl.DetachShader(program, vertex.Value);
                gl.DeleteProgram(vertex.Value);
            
                gl.DetachShader(program, fragment.Value);
                gl.DeleteProgram(fragment.Value);
                
                return program;
            }
        }
    }
}