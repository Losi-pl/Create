using System.Diagnostics.CodeAnalysis;
using OneOf;
using OneOf.Types;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using ShaderParam = Silk.NET.OpenGL.ShaderParameterName;

namespace Create.Graphics;

partial class Shader
{
    public static Constructor Create() => new();
    
    public struct Constructor
    {
        private OneOf<String, Stream>? _vertex;
        private OneOf<String, Stream>? _fragment;
        private string? _name;

        public Constructor Vertex(string content) { _vertex = content; return this; }
        public Constructor Vertex(Stream input)   { _vertex = input; return this; }

        public Constructor Fragment(string content) { _fragment = content; return this; }
        public Constructor Fragment(Stream input)   { _fragment = input;  return this; }
        
        public Constructor Name(string name) { _name = name; return this; }
        
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
            string GetCodeFrom(OneOf<String, Stream>? content, Func<string> name)
            {
                if(!content.HasValue)
                    throw new InvalidOperationException($"The {name()} shader was not specified.");
                
                return content.Value.Match(s => s, stream =>
                {
                    using var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
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