using Create.OpenGL.Textures;
using Create.Virtuals;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Drawing;
using static Create.OpenGL.RenderLayer.Constructor;

namespace Create.OpenGL;

/// <summary>
/// Płutno na którym obraz jest renderowany
/// </summary>
[DebuggerDisplay("{((new Proxy(this)).get_name()),nq}")]
[DebuggerTypeProxy(typeof(Proxy))]
public sealed partial class RenderLayer : IDisposable, IDrawable
{
    #region rendering
    static Shader shader = null!;
    static Mesh mesh = null!;

    internal static Shader default_shader => shader;
    internal static Mesh default_mesh => mesh;
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

    #region internal static
    internal static void set_shader(Shader shader)
    {
        if(RenderLayer.shader != null)
        {
            mesh.Dispose();
            RenderLayer.shader.Dispose();
        }
        
        RenderLayer.shader = shader;
        mesh = Mesh.Create(shader)
        .SetVertex("uv", new Vector2[] {
            new (0,1),
            new (1,1),
            new (0,0),
            new (1,0)
        })
        .SetTrangles(new[] { 0, 1, 2, 1, 2, 3 })
        .Finish();
    }
    #endregion

    #region get only
    /// <summary>
    /// Kanały renderowania podłączone do tego płutna
    /// </summary>
    public VirtualDictionaty<FramebufferAttachment, RenderTexture> Textures => frame_buffer_textures!.Value;

    /// <summary>
    /// Rozmiar płotna
    /// </summary>
    public (int Width, int Height) Size => (buffer_creation_data.width, buffer_creation_data.height);

    /// <summary>
    /// Modele renderowane na tym płutnie
    /// </summary>
    public List<IDrawable> Meshes { get; } = new();
    public bool IsDisposed => disposed;
    #endregion

    #region get set
    /// <summary>
    /// Kolor płutna po jego wyczyszczeniu
    /// </summary>
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

    /// <summary>
    /// Parametry tworzenia płutna
    /// </summary>
    struct creation_data
    {
        public int width, height;
        public bool draw_only;
        public BufferChanel[] chanels;
    }

    #region buffers
    /// <summary>
    /// tworzenie kanałów i płutna
    /// </summary>
    /// <exception cref="Exception"></exception>
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
    
    /// <summary>
    /// Rozłącza elementy płutna i je usuwa
    /// </summary>
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
    /// <summary>
    /// Aktywuje to płutno jako obecnie aktywne
    /// </summary>
    void Bind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, handles.frame_buffer);
    
    /// <summary>
    /// Przełącza obecnie używane płutno na domyślne
    /// </summary>
    void Unbind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    
    /// <summary>
    /// Wyczyść to płutno
    /// </summary>
    public void Clear()
    {
        Bind();
        GL.ClearColor(color);
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        Unbind();
    }
    
    /// <summary>
    /// Odświerza zawartość wyrenderowaną na płutnie
    /// </summary>
    public void UpdateContent()
    {
        Bind();
        Matrix4 projection = camera != null ? camera.CombinedMatrix : Engine.NeutralMatrix;
        Matrix4 model = model_matrix();
        GL.ClearColor(color);
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        foreach (var mesh in Meshes)
            mesh.Draw(projection, model);
        Unbind();

        //Methods
        Matrix4 model_matrix() => camera != null ? camera.Model : Engine.NeutralMatrix;
    }
    
    /// <summary>
    /// Renderuje to płutno jako obiekt na innym płutnie
    /// </summary>
    public void Draw(Matrix4 proj, Matrix4 mat)
    {
        if (custom_model.HasValue)
        {
            custom_model.Value.action(this, custom_model.Value.sender);
            custom_model.Value.model.Draw(proj, mat);
        }
        else
        {
            if (default_shader is null)
                return;
            default_shader.set_texture(0, textures![0].texture);
            default_mesh.Draw(proj, mat);
        }
    }
    
    /// <summary>
    /// Wykonuje inne operacje w OpenGL na tym płutnie
    /// </summary>
    /// <param name="action"></param>
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
        int[] handles;
        {
            if (textures != null)
            {
                handles = new int[textures.Length];
                for (int i = 0; i < textures.Length; i++)
                    handles[i] = textures[i].texture.Handle;
            }
            else
                handles = new int[0];
        }
        OpenGL.disposing.add(new disposing() {
            textures = handles,
            handel = (this.handles.frame_buffer, this.handles.render_buffer) });
        textures = null!;
        if(custom_model.HasValue)
            custom_model = null;
        frame_buffer_textures = null;
    }

    /// <summary>
    /// Niszczenie tego płutna i jego kanałów
    /// </summary>
    struct disposing : OpenGL.disposing.gl_element
    {
        public (int frame, int render) handel;
        public int[] textures;

        public void Dispose()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(handel.frame);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.DeleteRenderbuffer(handel.render);
            for (int i = 0; i < textures.Length; i++)
                GL.DeleteTexture(textures[i]);
        }
    }
    #endregion
}
