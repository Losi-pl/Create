using Create.OpenGL.Textures;
using Create.Virtuals;

namespace Create.OpenGL;

partial class Shader
{
    public static Constructor.IVertexShader Create() => new Constructor();
    
    /// <summary>
    /// Konstruktor do <see cref="Shader"/>a
    /// </summary>
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
            if (vertex == null) throw new ArgumentNullException(nameof(vertex));
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
            if (fragment == null) throw new ArgumentNullException(nameof(fragment));
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
        /// <summary>
        /// Ustawia która statuczna zmienna przechowuje pozycje modelu
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor PozitionUniform(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            pozition_variable = name;
            return this;
        }

        /// <summary>
        /// Ustawia która statuczna zmienna przechowuje orjentacje modelu
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor RotationUniform(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            rotation_variable = name;
            return this;
        }

        /// <summary>
        /// Ustawia która statuczna zmienna przechowuje podstawowy <see cref="OpenTK.Mathematics.Matrix4"/> do transformacji modelu
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor ProjectionMatrixUniform(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            default_matrix_sys = name;
            return this;
        }
        #endregion

        #region gl systems
        /// <summary>
        /// Pod jakim kątem trujkąty <see cref="Shader"/>a mają być widoczne
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Constructor CullFace(CullFaceMode mode)
        {
            cull_face = mode;
            return this;
        }

        /// <summary>
        /// Włącz wsparcie przezroczystości w <see cref="Shader"/>ze
        /// </summary>
        /// <returns></returns>
        public Constructor AlphaTest() => AlphaTest(true);

        /// <summary>
        /// Czy <see cref="Shader"/> wspiera mechanizm przezroczystości
        /// </summary>
        /// <returns></returns>
        public Constructor AlphaTest(bool active)
        {
            alphatest = active;
            return this;
        }

        /// <summary>
        /// Czy <see cref="Shader"/> wspiera głębokość obrazu
        /// </summary>
        public Constructor DepthTest() => DepthTest(true);

        /// <summary>
        /// Czy <see cref="Shader"/> wspiera głębokość obrazu
        /// </summary>
        public Constructor DepthTest(bool active)
        {
            depthtest = active;
            return this;
        }

        /// <summary>
        /// Ustawia domyślne ustawienia przezroczystości
        /// </summary>
        public Constructor Blend() => Blend(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        /// <summary>
        /// Ustawienia mechanizmu przezroczystości
        /// </summary>
        public Constructor Blend(BlendingFactor sfactor, BlendingFactor dfactor)
        {
            blend = (sfactor, dfactor);
            return this;
        }
        #endregion

        #region Finish
        /// <summary>
        /// Zakończ tworzenie <see cref="Shader"/>a
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ShaderCompilationException"></exception>
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

            if (pozition_variable != null)
                if (!pozition_variable_test(pozition_variable, out var exception))
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
                if (error.vertex != null || error.fragment != null)
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
        
        /// <summary>
        /// Zakończ tworzenie <see cref="Shader"/>a i wykonaj na nim operacje <paramref name="action"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Shader Finish(Action<Shader> action)
        {
            var s = Finish();
            action(s);
            return s;
        }
        #endregion

        #region interface
        /// <summary>
        /// Wprowadź program Fragment Shader
        /// </summary>
        public interface IFragmentShader
        {
            /// <summary>
            /// Kod do fragment <see cref="Shader"/>a
            /// </summary>
            public Constructor FragmentCode(string code);

            /// <summary>
            /// Podłącz już istniejący fragment <see cref="Shader"/>
            /// </summary>
            /// <param name="deletAfter">Czy urzyty fragment <see cref="Shader"/> ma zostać usunięty po użyciu</param>
            public Constructor FragmentHandle(int handle, bool deletAfter = false);
        }

        /// <summary>
        /// Wprowadź program Vertex Shader
        /// </summary>
        public interface IVertexShader
        {
            /// <summary>
            /// Kod do vertex <see cref="Shader"/>a
            /// </summary>
            public IFragmentShader VertexCode(string code);

            /// <summary>
            /// Podłącz już istniejący vertex <see cref="Shader"/>
            /// </summary>
            /// <param name="deletAfter">Czy urzyty vertex <see cref="Shader"/> ma zostać usunięty po użyciu</param>
            public IFragmentShader VertexHandle(int handle, bool deletAfter = false);
        }
        #endregion
    }
}
