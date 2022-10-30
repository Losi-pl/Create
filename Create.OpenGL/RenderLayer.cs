using Create.OpenGL.Textures;
using Create.Virtuals;
using OpenTK.Mathematics;
using System.Drawing;
using static Create.OpenGL.RenderLayer.Constructor;

namespace Create.OpenGL;

public sealed class RenderLayer : IDisposable
{
    #region internal static
    internal static Shader default_shader { get; } = Shader.Create()
        .VertexCode(@"#version 440 core

            in vec2 uv;
            out vec2 o_uv;

            void main()
            {
                o_uv = uv;
                gl_Position = vec4((uv.x - 0.5) * 2, (uv.y - 0.5) * 2, 0, 1);
            }")
        .FragmentCode(@"#version 440 core

            uniform sampler2D texture_;
            in vec2 o_uv;
            out vec4 color;

            void main()
            {
                color = texture(texture_, o_uv);
            }")
        .Blend()
        .AlphaTest(true)
        .DepthTest(false)
        .Finish();
    internal static Mesh default_mesh { get; } = Mesh.Create(default_shader)
        .SetVertex("uv", new Vector2[] 
            {
                new (0,1),
                new (1,1),
                new (0,0),
                new (1,0)
            })
        .SetTrangles(new[] { 0, 1, 2, 1, 2, 3 })
        .Finish();
    #endregion

    #region variable
    (int frame_buffer, int render_buffer) handles;
    (RenderTexture texture, FramebufferAttachment chanel)[]? textures;
    bool draw_only;
    creation_data buffer_creation_data;
    (Shader shader, Mesh model, object? sender, Action<RenderLayer, object?> action)? custom_model;
    VirtualDictionaty<FramebufferAttachment, RenderTexture>? frame_buffer_textures;
    Color4 color = new(0, 0, 0, 0);
    bool disposed;
    Camera? camera;
    #endregion

    #region get only
    public VirtualDictionaty<FramebufferAttachment, RenderTexture> Textures => frame_buffer_textures!.Value;
    public (int Width, int Height) Size => (buffer_creation_data.width, buffer_creation_data.height);
    public List<Mesh> Meshes { get; } = new();
    public bool IsDisposed => disposed;
    #endregion

    #region get set
    public Color Color
    {
        get => Color.FromArgb(
            (int)(color.A * 255),
            (int)(color.R * 255),
            (int)(color.G * 255),
            (int)(color.B * 255));
        set => color = new(value.R, value.G, value.B, value.A);
    }
    #endregion

    #region constructor
    public static Constructor Create() => new();
    public sealed class Constructor
    {
        #region variables
        Vector2i buffer_size = Engine.Size;
        bool draw_only = false;
        (Shader shader, Mesh model, object? sender, Action<RenderLayer, object?> action)? custom_model;
        Camera? camera;
        BufferChanel[] chanels = new BufferChanel[] 
        {
            new() { 
                buffer_chanel = FramebufferAttachment.ColorAttachment0,
                in_format = PixelInternalFormat.Rgba,
                out_format = PixelFormat.Rgba,
                type = PixelType.UnsignedByte,
                filter = TextureMagFilter.Nearest
            }
        };
        #endregion

        #region SetSize
        public Constructor SetSize(int width, int height) => SetSize(new(width, height));
        public Constructor SetSize(Vector2i size)
        {
            buffer_size = size;
            return this;
        }
        #endregion

        public Constructor Camera(Camera camera)
        {
            this.camera = camera;
            return this;
        }

        #region CustomShader
        public Constructor CustomShader(Shader shader, (FramebufferAttachment chanel, string uniform)[] uniforms) =>
            CustomShader(shader, calculate_texture_idis(this, shader, uniforms), custom_chanels_apply!);
        public Constructor CustomShader(Mesh custom_model, (FramebufferAttachment chanel, string uniform)[] uniforms) => 
            CustomShader(custom_model, calculate_texture_idis(this, custom_model.Shader, uniforms), custom_chanels_apply!);

        static void custom_chanels_apply(RenderLayer layer, object o)
        {
            var binds = ((int shader, int render)[])o;
            foreach (var b in binds)
                layer.custom_model!.Value.shader.set_texture(b.shader, layer.textures![b.render].texture);
        }
        static (int shader, int render)[] calculate_texture_idis(Constructor cons, Shader shader, (FramebufferAttachment chanel, string uniform)[] uniforms)
        {
            if (cons.chanels == null)
                throw new Exception("First define the channel before binding them");
            foreach (var uniform in uniforms)
                cons.chanels.Find(c => c.buffer_chanel == uniform.chanel, new Exception($"The {uniform.chanel} channel is undefined"));
            foreach (var uniform in uniforms)
                test_uniform(shader, uniform.uniform);

            return uniforms.ConvertAll(uniforms => (
                shader.Uniforms.Find(u => u.Name == uniforms.uniform, null).TextureNumer!.Value,
                cons.chanels.FindAndWhere(c => c.buffer_chanel == uniforms.chanel)!.Value.index));

            void test_uniform(Shader shader, string name)
            {
                var unif = shader.Uniforms.Find(u => u.Name == name, new Exception($"Uniform {name} don't exist"));
                if (unif.Type != ActiveUniformType.Sampler2D && unif.Type != ActiveUniformType.UnsignedIntImage2D)
                    throw new Exception($"Uniform {name} is not Sampler2D");
            }
        }
        static void no_obiect(RenderLayer layer, object o) => ((Action<RenderLayer>)o)(layer);
        #endregion

        #region CustomShader
        public Constructor CustomShader(Shader customShader, object? sender, Action<RenderLayer, object?> action)
        {
            if (customShader == null)
                throw new ArgumentNullException(nameof(customShader));
            var attribut = get_info(customShader);
            Mesh mesh = Mesh.Create(customShader)
                .SetVertex(attribut.Name, new Vector2[]{ new(0,1), new(1,1), new(0,0), new(1,0) })
                .SetTrangles(new[] { 0, 1, 2, 1, 2, 3 })
                .Finish();
            CustomShader(mesh, sender, action);
            return this;
            //Methods
            Shader.AttributInfo get_info(Shader shader)
            {
                if (shader.Attributes.Count != 1)
                    throw new ArgumentOutOfRangeException("Shader must have only on attribute");
                var typ = shader.Attributes[0].GLType;
                if (typ == ActiveAttribType.FloatVec4) { }
                else if (typ == ActiveAttribType.DoubleVec2) { }
                else if (typ == ActiveAttribType.IntVec2) { }
                else if (typ == ActiveAttribType.UnsignedIntVec2) { }
                else
                    throw new ArgumentException("Attribute must be Vector2 of any type");
                return shader.Attributes[0];
            }
        }
        public Constructor CustomShader(Mesh custom_model, object? sender, Action<RenderLayer, object?> action)
        {
            if(custom_model == null)
                throw new ArgumentNullException(nameof(custom_model));
            this.custom_model = (custom_model.Shader, custom_model, sender, action);
            return this;
        }

        public Constructor CustomShader(Shader customShader, Action<RenderLayer> action) => CustomShader(customShader, action ?? throw new ArgumentNullException(nameof(action)), no_obiect!);
        public Constructor CustomShader(Mesh custom_model, Action<RenderLayer> action) => CustomShader(custom_model, action ?? throw new ArgumentNullException(nameof(action)), no_obiect!);
        #endregion

        

        #region DrawOnly
        public Constructor DrawOnly() => DrawOnly(true);
        public Constructor DrawOnly(bool only)
        {
            draw_only = only;
            return this;
        }
        #endregion

        public RenderLayer Finisch()
        {
            RenderLayer render_layer = new();
            creation_data cd = new();
            {
                cd.width = buffer_size.X;
                cd.height = buffer_size.Y;
                cd.draw_only = draw_only;
                cd.chanels = chanels;
            }
            render_layer.buffer_creation_data = cd;
            render_layer.camera = camera;
            render_layer.custom_model = custom_model;
            render_layer.create_buffers();
            return render_layer;
        }
        public struct BufferChanel
        {
            public FramebufferAttachment buffer_chanel;
            public PixelInternalFormat in_format;
            public PixelFormat out_format;
            public PixelType type;
            public TextureMagFilter filter;
        }
    }
    #endregion

    struct creation_data
    {
        public int width, height;
        public bool draw_only;
        public BufferChanel[] chanels;
    }

    #region buffers
    void create_buffers()
    {
        lock (Engine.TaskLock)
        {
            var cd = buffer_creation_data;
            draw_only = cd.draw_only;
            int frame_buffer = gen_frame_buffer(cd.width, cd.height, draw_only);
            (FramebufferAttachment chanel, int handle)[] textures = cd.chanels.ConvertAll(c => (c.buffer_chanel, gen_image_atatchment(cd.width, cd.height, draw_only, c)));
            int render_buffer = gen_render_buffer(cd.width, cd.height, frame_buffer);
            {
                var error = check_status();
                if (error.HasValue)
                {
                    destroy_buffers(frame_buffer, render_buffer, textures.ConvertAll(t => t.handle));
                    throw new Exception($"Frame buffer error:\n{error.Value}");
                }
            }
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            handles = (frame_buffer, render_buffer);
            if (this.textures == null)
            {
                this.textures = textures.ConvertAll(t => (new RenderTexture(t.handle), t.chanel));
                frame_buffer_textures = VirtualDictionaty.Create<FramebufferAttachment, RenderTexture>()
                    .GetMethod(c => this.textures.Find(e => e.chanel == c, new KeyNotFoundException()).texture)
                    .CountMethod(() => this.textures.Length)
                    .IsConteinedMethod(c => this.textures.FindAndWhere(e => e.chanel == c).HasValue)
                    .EnumerableMethod(() => ((IEnumerable<(RenderTexture, FramebufferAttachment)>)this.textures)
                        .ConvertAll(e => new KeyValuePair<FramebufferAttachment, RenderTexture>(e.Item2, e.Item1)))
                    .Finsh();
            }
            else
            {
                for (int i = 0; i < textures.Length; i++)
                    this.textures[i].texture.SetNewHandle(textures[i].handle);
            }
        }

        //Methods
        int gen_frame_buffer(int width, int height, bool draw_only)
        {
            int handle = GL.GenFramebuffer();
            GL.BindFramebuffer(draw_only ? FramebufferTarget.DrawFramebuffer : FramebufferTarget.Framebuffer, handle);
            return handle;
        }
        int gen_render_buffer(int width, int height, int frame_buffer_handle)
        {
            int handle = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, handle);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, width, height);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.FramebufferRenderbuffer(draw_only ? FramebufferTarget.DrawFramebuffer : FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, handle);
            return handle;
        }
        int gen_image_atatchment(int width, int height, bool draw_only, BufferChanel data)
        {
            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, data.in_format, width, height, 0, data.out_format, data.type, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)data.filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)data.filter);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.FramebufferTexture2D(draw_only ? FramebufferTarget.DrawFramebuffer : FramebufferTarget.Framebuffer, data.buffer_chanel, TextureTarget.Texture2D, handle, 0);
            return handle;
        }
        void destroy_buffers(int frame_buffer, int render_buffer, int[] textures)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(frame_buffer);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.DeleteRenderbuffer(render_buffer);
            foreach(var texture in textures)
                GL.DeleteTexture(texture);
        }
        FramebufferErrorCode? check_status()
        {
            var status = GL.CheckFramebufferStatus(draw_only ? FramebufferTarget.DrawFramebuffer : FramebufferTarget.Framebuffer);
            if (status == FramebufferErrorCode.FramebufferComplete || status == FramebufferErrorCode.FramebufferCompleteExt)
                return null;
            return status;
        }
    }
    void releise_buffers()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        GL.DeleteFramebuffer(handles.frame_buffer);
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        GL.DeleteRenderbuffer(handles.render_buffer);
        if (textures != null)
            foreach (var texture in textures)
                GL.DeleteTexture(texture.texture.Handle);
    }
    #endregion

    #region Resize
    public void Resize(Vector2i size) => Resize(size.X, size.Y);
    public void Resize(int width, int height)
    {
        releise_buffers();
        buffer_creation_data.width = width;
        buffer_creation_data.height = height;
        create_buffers();
    }
    #endregion

    #region drawing
    internal void Bind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, handles.frame_buffer);
    internal void Unbind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    public void Clear()
    {
        Bind();
        GL.ClearColor(color);
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        Unbind();
    }
    public void UpdateContent()
    {
        Bind();
        Matrix4 projection = projection_matrix();
        GL.ClearColor(color);
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        foreach (var mesh in Meshes)
            mesh.Draw(model_matrix(mesh) * projection);
        Unbind();
    }
    internal void Draw()
    {
        if (custom_model.HasValue)
        {
            custom_model.Value.action(this, custom_model.Value.sender);
            custom_model.Value.model.Draw();
        }
        else
        {
            default_shader.set_texture(0, textures![0].texture);
            default_mesh.Draw();
        }
    }
    Matrix4 projection_matrix()
    {
        if(camera != null)
        {
            var reve = new Vector3(camera.RevertAxis.x ? -1 : 1, camera.RevertAxis.y ? -1 : 1, camera.RevertAxis.z ? -1 : 1);
            Matrix4 sca = Matrix4.CreateScale(reve);
            Matrix4 poz = Matrix4.CreateTranslation(-camera.Pozition * reve);
            Matrix4 rot = Matrix4.CreateRotationY(MathF.PI) * (camera.Rotation != new Vector3() ? Matrix4.CreateFromQuaternion(camera.RotationQuaternion) : Engine.NeutralMatrix);
            Matrix4 cam = camera.Projection;
            return sca * poz * rot * cam;
        }
        return Engine.NeutralMatrix;
    }
    Matrix4 model_matrix(Mesh mesh)
    {
        Matrix4 mat = Matrix4.CreateTranslation(mesh.Position) *
            ((mesh.Rotation != new Vector3()) ?
                Matrix4.CreateFromQuaternion(new(mesh.Rotation)) :
                Engine.NeutralMatrix);
        return (camera != null ? camera.Model : Engine.NeutralMatrix) * mat;
    }
    public void ExecuteIn(Action action)
    {
        Bind();
        action();
        Unbind();
    }
    #endregion

    #region destructor
    ~RenderLayer() => Dispose();
    public void Dispose()
    {
        if(disposed)
            return;
        disposed = true;
        GC.SuppressFinalize(this);
        lock (Engine.TaskLock)
        {
            Unbind();
            releise_buffers();
        }
        textures = null!;
        if(custom_model.HasValue)
            custom_model = null;
        frame_buffer_textures = null;
    }
    #endregion
}
