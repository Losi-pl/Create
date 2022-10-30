using Create.OpenGL.Textures;
using Create.Virtuals;
using OpenTK.Mathematics;
using Create.OpenGL.Mathematic;
using System.Diagnostics;

namespace Create.OpenGL;

[DebuggerTypeProxy(typeof(Proxy))]
[DebuggerDisplay("Shader: {(new Proxy(this)).get_status(),nq}")]
public sealed class Shader : IDisposable
{
    #region variables
    int handle;
    AttributInfo[] attributInfos;
    UniformInfo[] uniformInfos;
    Texture[] textures;
    VertexAttributesBind bind;
    bool disposed;

    (string name, int handle)? pozition_variable = null;
    (string name, int handle)? rotation_variable = null;
    (string name, int handle)? use_default_matrix_mehanic = null;
    (BlendingFactor, BlendingFactor)? blend = null;

    VirtualList<AttributInfo> virtual_attribut;
    VirtualList<UniformInfo> virtual_uniform;
    VirtualList<Texture> virtual_textures;
    CullFaceMode cull_face;
    bool alphatest, depthtest;
    #endregion

    #region disable constructor
#pragma warning disable CS8618
    private Shader() { }
#pragma warning restore CS8618
    #endregion

    #region get only
    public VirtualList<AttributInfo> Attributes => virtual_attribut;
    public VirtualList<UniformInfo> Uniforms => virtual_uniform;
    public VirtualList<Texture> Textures => virtual_textures;
    public VertexAttributesBind ShaderBind => bind;
    public CullFaceMode CullFace => cull_face;
    public int Handle => handle;
    public bool IsDisposed => disposed;
    internal (string name, int handle)? PozitionVariable => pozition_variable;
    internal (string name, int handle)? RotationVariable => rotation_variable;
    internal (string name, int handle)? DefaultMatrixSystem => use_default_matrix_mehanic;
    internal (BlendingFactor s, BlendingFactor d)? blendfunc => blend;
    internal (bool alphatest, bool depthtest) simple_mekanizms => (alphatest, depthtest);
    #endregion

    internal void set_texture(int index, Texture texture) => textures[index] = texture;

    #region SetUniform
    public Shader SetUniform(string name, int value) => set_parametr(name, ActiveUniformType.Int, u => GL.Uniform1(u.Handle, value));
    public Shader SetUniform(string name, Vector2i value) => set_parametr(name, ActiveUniformType.IntVec2, u => GL.Uniform2(u.Handle, value), ActiveUniformType.UnsignedIntVec2);
    public Shader SetUniform(string name, Vector3i value) => set_parametr(name, ActiveUniformType.IntVec3, u => GL.Uniform3(u.Handle, value), ActiveUniformType.UnsignedIntVec3);
    public Shader SetUniform(string name, Vector4i value) => set_parametr(name, ActiveUniformType.IntVec4, u => GL.Uniform4(u.Handle, value), ActiveUniformType.UnsignedIntVec4);
    
    public Shader SetUniform(string name, bool value) => set_parametr(name, ActiveUniformType.Bool, u => GL.Uniform1(u.Handle, value ? 1 : 0));
    public Shader SetUniform(string name, Vector2b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform2(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0));
    public Shader SetUniform(string name, Vector3b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform3(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0, value.Z ? 1 : 0));
    public Shader SetUniform(string name, Vector4b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform4(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0, value.Z ? 1 : 0, value.W ? 1 : 0));
    
    public Shader SetUniform(string name, float value) => set_parametr(name, ActiveUniformType.Float, u => GL.Uniform1(u.Handle, value));
    public Shader SetUniform(string name, Vector2 value) => set_parametr(name, ActiveUniformType.FloatVec2, u => GL.Uniform2(u.Handle, value));
    public Shader SetUniform(string name, Vector3 value) => set_parametr(name, ActiveUniformType.FloatVec3, u => GL.Uniform3(u.Handle, value));
    public Shader SetUniform(string name, Vector4 value) => set_parametr(name, ActiveUniformType.FloatVec4, u => GL.Uniform4(u.Handle, value));
    
    public Shader SetUniform(string name, Matrix2 value) => set_parametr(name, ActiveUniformType.FloatMat2, u => GL.UniformMatrix2(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix2x3 value) => set_parametr(name, ActiveUniformType.FloatMat2x3, u => GL.UniformMatrix2x3(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix2x4 value) => set_parametr(name, ActiveUniformType.FloatMat2x4, u => GL.UniformMatrix2x4(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix3x2 value) => set_parametr(name, ActiveUniformType.FloatMat3x2, u => GL.UniformMatrix3x2(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix3 value) => set_parametr(name, ActiveUniformType.FloatMat3, u => GL.UniformMatrix3(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix3x4 value) => set_parametr(name, ActiveUniformType.FloatMat3x4, u => GL.UniformMatrix3x4(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix4x2 value) => set_parametr(name, ActiveUniformType.FloatMat4x2, u => GL.UniformMatrix4x2(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix4x3 value) => set_parametr(name, ActiveUniformType.FloatMat4x3, u => GL.UniformMatrix4x3(u.Handle, false, ref value));
    public Shader SetUniform(string name, Matrix4 value) => set_parametr(name, ActiveUniformType.FloatMat4, u => GL.UniformMatrix4(u.Handle, false, ref value));
    public Shader SetUniform(string name, Texture2D texture) => set_parametr_test(name, ActiveUniformType.Sampler2D, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2D);
    public Shader SetUniform(string name, Texture2DArray texture) => set_parametr_test(name, ActiveUniformType.Sampler2DArray, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2DArray);
    public Shader SetUniform(string name, RenderTexture texture) => set_parametr_test(name, ActiveUniformType.Sampler2D, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2D);

    Shader set_parametr(string name, ActiveUniformType type, Action<UniformInfo> func, ActiveUniformType? secondary_type = null)
    {
        var var_ = uniformInfos.FindAndWhere(u => u.Name == name);
        if (!var_.HasValue)
            throw new ArgumentException($"Parament with name \"{name}\" don't exist");
        if (var_.Value.element.Type != type && var_.Value.element.Type != secondary_type)
        {
            string? secound_type = (secondary_type != null ?(type.GetCSharpType() != secondary_type.Value.GetCSharpType() ? $" or {secondary_type.Value.GetCSharpType()}" : null) : null);
            throw new ArgumentException($"Types are not match\nexpected {type.GetCSharpType()}{secound_type}");
        }
        //MainTask.Run(() => 
        {
            GL.UseProgram(handle);
            func(var_.Value.element);
            GL.UseProgram(0);
        }//);
        return this;
    }
    Shader set_parametr_test(string name, ActiveUniformType type, Action<UniformInfo> func, ActiveUniformType? secondary_type = null)
    {
        var var_ = uniformInfos.FindAndWhere(u => u.Name == name);
        if (!var_.HasValue)
            throw new ArgumentException($"Parament with name \"{name}\" don't exist");
        if (var_.Value.element.Type != type && var_.Value.element.Type != secondary_type)
        {
            string? secound_type = (secondary_type != null ?(type.GetCSharpType() != secondary_type.Value.GetCSharpType() ? $" or {secondary_type.Value.GetCSharpType()}" : null) : null);
            throw new ArgumentException($"Types are not match\nexpected {type.GetCSharpType()}{secound_type}");
        }
        func(var_.Value.element);
        return this;
    }
    #endregion

    #region destructor
    ~Shader() => Dispose();
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;
        GC.SuppressFinalize(this);

        GL.UseProgram(0);
        GL.DeleteProgram(handle);
        attributInfos = null!;
        uniformInfos = null!;
        textures = null!;
        virtual_attribut = VirtualList.Create<AttributInfo>().Finish();
        virtual_uniform = VirtualList.Create<UniformInfo>().Finish();
        virtual_textures = VirtualList.Create<Texture>().Finish();
    }
    #endregion

    #region constructor
    public static Constructor.IVertexShader Create() => new Constructor();
    public class Constructor : Constructor.IFragmentShader, Constructor.IVertexShader
    {
        #region variable
        (string? vertex, string? fragment) shaderscodes;
        (int vertex, int fragment) shadershandlers;
        (bool vertex, bool fragment) shaderdelete = (true, true);
        string? pozition_variable = null;
        string? rotation_variable = null;
        string? default_matrix_sys;
        bool alphatest = false, depthtest = true;
        CullFaceMode cull_face = CullFaceMode.FrontAndBack;
        (BlendingFactor, BlendingFactor)? blend = null;
        #endregion

        #region vertex fragment
        IFragmentShader IVertexShader.VertexCode(string vertex)
        {
            if(vertex == null) throw new ArgumentNullException(nameof(vertex));
            shaderscodes.vertex = vertex;
            return this;
        }
        IFragmentShader IVertexShader.VertexHandle(int handle, bool deletAfter)
        {
            shadershandlers.vertex = handle;
            shaderdelete.vertex = deletAfter;
            return this;
        }

        Constructor IFragmentShader.FragmentCode(string fragment)
        {
            if(fragment == null) throw new ArgumentNullException(nameof(fragment));
            shaderscodes.fragment = fragment;
            return this;
        }
        Constructor IFragmentShader.FragmentHandle(int handle, bool deletAfter)
        {
            shadershandlers.fragment = handle;
            shaderdelete.fragment = deletAfter;
            return this;
        }
        #endregion

        #region system uniforms
        public Constructor PozitionUniform(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            pozition_variable = name;
            return this;
        }
        public Constructor RotationUniform(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            rotation_variable = name;
            return this;
        }
        public Constructor ProjectionMatrixUniform(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            default_matrix_sys = name;
            return this;
        }
        #endregion

        #region gl systems
        public Constructor CullFace(CullFaceMode mode)
        {
            cull_face = mode;
            return this;
        }
        public Constructor AlphaTest() => AlphaTest(true);
        public Constructor AlphaTest(bool active)
        {
            alphatest = active;
            return this;
        }
        public Constructor DepthTest(bool active)
        {
            alphatest = active;
            return this;
        }
        public Constructor Blend() => Blend(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        public Constructor Blend(BlendingFactor sfactor, BlendingFactor dfactor)
        {
            blend = (sfactor, dfactor);
            return this;
        }
        #endregion

        #region Finish
        public Shader Finish()
        {
            Shader shader = new();
            shader.cull_face = cull_face;
            compile_shaders();
            shader.handle = marge_shaders();
            delete_shaders();
            shader.uniformInfos = get_uniforms(shader.handle, out shader.textures);
            shader.attributInfos = get_attributes(shader.handle);
            shader.bind = new(shader.attributInfos);

            if(pozition_variable != null)
                if(!pozition_variable_test(pozition_variable, out var exception))
                {
                    lock (Engine.TaskLock)
                    {
                        GL.UseProgram(0);
                        GL.DeleteProgram(shader.handle);
                    }
                    throw exception!;
                }
                else
                    shader.pozition_variable = (pozition_variable, 
                        shader.uniformInfos.FindAndWhere(t => t.Name == pozition_variable)
                        .Cast(t => shader.uniformInfos[t!.Value.index].Handle));

            if (rotation_variable != null)
                if (!rotation_variable_test(rotation_variable, out var exception))
                {
                    lock (Engine.TaskLock)
                    {
                        GL.UseProgram(0);
                        GL.DeleteProgram(shader.handle);
                    }
                    throw exception!;
                }
                else
                    shader.rotation_variable = (rotation_variable, 
                        shader.uniformInfos.FindAndWhere(t => t.Name == rotation_variable)
                        .Cast(t => shader.uniformInfos[t!.Value.index].Handle));

            if (default_matrix_sys != null)
                if (!matrix_variable_test(default_matrix_sys, out var exception))
                {
                    lock (Engine.TaskLock)
                    {
                        GL.UseProgram(0);
                        GL.DeleteProgram(shader.handle);
                    }
                    throw exception!;
                }
                else
                    shader.use_default_matrix_mehanic = (default_matrix_sys,
                        shader.uniformInfos.FindAndWhere(t => t.Name == default_matrix_sys)
                        .Cast(t => shader.uniformInfos[t!.Value.index].Handle));

            shader.virtual_attribut = VirtualList.Create(shader.attributInfos).Finish();
            shader.virtual_uniform = VirtualList.Create(shader.uniformInfos).Finish();
            shader.virtual_textures = VirtualList.Create(shader.textures).Finish();

            shader.alphatest = alphatest;
            shader.depthtest = depthtest;
            shader.blend = blend;

            return shader;
            
            //Methods
            int marge_shaders()
            {
                int handle = GL.CreateProgram();

                GL.AttachShader(handle, shadershandlers.vertex);
                GL.AttachShader(handle, shadershandlers.fragment);

                GL.LinkProgram(handle);

                GL.DetachShader(handle, shadershandlers.vertex);
                GL.DetachShader(handle, shadershandlers.fragment);

                return handle;
            }
            void delete_shaders()
            {
                if (shaderdelete.vertex)
                    GL.DeleteShader(shadershandlers.vertex);
                if (shaderdelete.fragment)
                    GL.DeleteShader(shadershandlers.fragment);
            }
            void compile_shaders()
            {
                (string? vertex, string? fragment) error = new();
                if (shaderscodes.vertex != null)
                {
                    var rezult = shader_compiler(shaderscodes.vertex, ShaderType.VertexShader);
                    if (rezult.error != null)
                        error.vertex = rezult.error;
                    else
                        shadershandlers.vertex = rezult.handle;
                }
                if (shaderscodes.fragment != null)
                {
                    var rezult = shader_compiler(shaderscodes.fragment, ShaderType.FragmentShader);
                    if (rezult.error != null)
                        error.fragment = rezult.error;
                    else
                        shadershandlers.fragment = rezult.handle;
                }
                if(error.vertex != null || error.fragment != null)
                    throw new ShaderCompilationException(error.vertex, error.fragment);

                (int handle, string? error) shader_compiler(string code, ShaderType type)
                {
                    int shader_handle = GL.CreateShader(type);
                    GL.ShaderSource(shader_handle, code);
                    GL.CompileShader(shader_handle);
                    string vertex_shader_info = GL.GetShaderInfoLog(shader_handle);
                    if (!string.IsNullOrEmpty(vertex_shader_info))
                    {
                        GL.DeleteShader(shader_handle);
                        return (0, vertex_shader_info);
                    }
                    return (shader_handle, null);
                }
            }
            
            bool pozition_variable_test(string name, out Exception? ex)
            {
                var poz = shader.uniformInfos.FindAndWhere(t => t.Name == name);
                if(!poz.HasValue)
                {
                    ex = new Exception($"The uniform {name} doesynt exist");
                    return false;
                }
                var unif_t = poz.Value.element.Type;
                if(!(unif_t == ActiveUniformType.FloatVec3 || unif_t == ActiveUniformType.DoubleVec3 || unif_t == ActiveUniformType.UnsignedIntVec3))
                {
                    ex = new Exception($"Uniform {name} is not eny of a Vector3");
                    return false;
                }
                ex = null;
                return true;
            }
            bool rotation_variable_test(string name, out Exception? ex)
            {
                var poz = shader.uniformInfos.FindAndWhere(t => t.Name == name);
                if (!poz.HasValue)
                {
                    ex = new Exception($"The uniform {name} doesynt exist");
                    return false;
                }
                var unif_t = poz.Value.element.Type;
                if (!(unif_t == ActiveUniformType.FloatVec3 || unif_t == ActiveUniformType.DoubleVec3 || unif_t == ActiveUniformType.UnsignedIntVec3))
                {
                    ex = new Exception($"Uniform {name} is not eny of a Vector3");
                    return false;
                }
                ex = null;
                return true;
            }
            bool matrix_variable_test(string name, out Exception? ex)
            {
                var poz = shader.uniformInfos.FindAndWhere(t => t.Name == name);
                if (!poz.HasValue)
                {
                    ex = new Exception($"The uniform {name} doesynt exist");
                    return false;
                }
                var unif_t = poz.Value.element.Type;
                if (unif_t != ActiveUniformType.FloatMat4)
                {
                    ex = new Exception($"Uniform {name} is not Matrix4");
                    return false;
                }
                ex = null;
                return true;
            }

            AttributInfo[] get_attributes(int handle)
            {
                AttributInfo[] properties;
                {
                    GL.GetProgram(handle, GetProgramParameterName.ActiveAttributes, out int Lenght);
                    properties = new AttributInfo[Lenght];
                }
                for (int i = 0; i < properties.Length; i++)
                {
                    GL.GetActiveAttrib(handle, i, 256, out _, out _, out var type, out var name);
                    int location = GL.GetAttribLocation(handle, name);
                    properties[i] = new(location, type, name);
                }
                return properties;
            }
            UniformInfo[] get_uniforms(int handle, out Texture[] textures)
            {
                UniformInfo[] properties;
                int textes = 0;
                {
                    GL.GetProgram(handle, GetProgramParameterName.ActiveUniforms, out int Lenght);
                    properties = new UniformInfo[Lenght];
                }
                for (int i = 0; i < properties.Length; i++)
                {
                    GL.GetActiveUniform(handle, i, 256, out _, out _, out var type, out var name);
                    int location = GL.GetUniformLocation(handle, name);
                    bool tex_supp = Texture.TextureSupported(type);
                    properties[i] = tex_supp ? new(location, type, name, textes++) : new(location, type, name);
                    if (tex_supp)
                        lock (Engine.TaskLock)
                        {
                            GL.UseProgram(handle);
                            GL.Uniform1(location, textes - 1);
                        }
                }
                if (textes > 0)
                    textures = new Texture[textes];
                else
                    textures = Array.Empty<Texture>();
                return properties;
            }
        }
        public Shader Finish(Action<Shader> action)
        {
            var s = Finish();
            action(s);
            return s;
        }
        #endregion

        #region interface
        public interface IFragmentShader
        {
            public Constructor FragmentCode(string code);
            public Constructor FragmentHandle(int handle, bool deletAfter = false);
        }
        public interface IVertexShader
        {
            public IFragmentShader VertexCode(string code);
            public IFragmentShader VertexHandle(int handle, bool deletAfter = false);
        }
        #endregion
    }
    #endregion

    #region shader variable info
    public struct AttributInfo
    {
        int handle;
        ActiveAttribType type;
        string name;
        public AttributInfo(int handle, ActiveAttribType type, string name)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
        }

        public int Handle => handle;
        public string Name => name;
        public ActiveAttribType GLType => type;
        public Type? Type => type.GetCSharpType();
    }
    public struct UniformInfo
    {
        int handle;
        ActiveUniformType type;
        string name;
        int? tex_id;

        public UniformInfo(int handle, ActiveUniformType type, string name)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
            tex_id = null;
        }
        public UniformInfo(int handle, ActiveUniformType type, string name, int tex_num)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
            tex_id = tex_num;
        }

        public int Handle => handle;
        public string Name => name;
        public ActiveUniformType Type => type;
        public int? TextureNumer => tex_id;
    }
    public struct VertexAttributesBind
    {
        int byte_lenght;
        (int index, int offset)[] attributes;
        internal VertexAttributesBind(AttributInfo[] attributs)
        {
            byte_lenght = 0;
            attributes = new (int index, int offset)[attributs.Length];
            for (int i = 0; i < attributs.Length; i++)
            {
                attributes[i] = new(i, byte_lenght);
                byte_lenght += attributs[i].GLType.ElementByteSize();
            }
        }
        public int ByteSize => byte_lenght;
        public ReadOnlySpan<(int index, int offset)> Binds => new(attributes);
    }
    #endregion

    #region proxy
    class Proxy
    {
        Shader shader;
        public Proxy(Shader shader)
        {
            this.shader = shader;
        }

        public ReadOnlyDictionaryView<string, AttributInfo> Attributes =>
           new(shader.attributInfos.ToDictionary(attr => attr.Name));
        public ReadOnlyDictionaryView<string, UniformInfo> Uniforms =>
            new(shader.uniformInfos.ToDictionary(uni => uni.Name));
        public VertexAttributesBind VerticesBind => shader.bind;
        public int Handle => shader.handle;
        public StandardMechanisc_ CreateMechanisc => new()
        {
            Pozition = shader.pozition_variable,
            Rotation = shader.rotation_variable,
            Matrix = shader.use_default_matrix_mehanic
        };
        public OpenGLMechanisc_ OpenGLMechanisc => new(shader);

        public object get_status() => shader.disposed ? "Dispose" : $"Handle: {shader.handle}";

        [DebuggerDisplay("")]
        public class StandardMechanisc_
        {
            public (string Name, int Handle)? Pozition;
            public (string Name, int Handle)? Rotation;
            public (string Name, int Handle)? Matrix;
            public override string ToString() => string.Empty;
        }
        [DebuggerDisplay("")]
        public class OpenGLMechanisc_
        {
            public OpenGLMechanisc_(Shader shader)
            {
                AlphaTest = new() { active = shader.simple_mekanizms.alphatest };
                DepthTest = new() { active = shader.simple_mekanizms.depthtest };
                CullFace = shader.cull_face != CullFaceMode.FrontAndBack ? shader.cull_face : new system() { active = false };
                Blend = shader.blend.HasValue ? shader.blend : new system() { active = false };
            }

            public system AlphaTest;
            public system DepthTest;
            public object CullFace;
            public object Blend;
        }
        [DebuggerDisplay("{(active ? \"Enable\" : \"Disable\"),nq}")]
        public struct system
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public bool active;
        }

    }
    #endregion
}
