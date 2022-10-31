using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace Create.OpenGL;

[DebuggerTypeProxy(typeof(Proxy))]
[DebuggerDisplay("Mesh: {((new Proxy(this)).status()),nq}")]
public sealed partial class Mesh : IDisposable, IDrawable
{
    readonly static TextureUnit[] texture_uints = new[]
    {
        TextureUnit.Texture0, TextureUnit.Texture1, TextureUnit.Texture2, 
        TextureUnit.Texture3, TextureUnit.Texture4, TextureUnit.Texture5,
        TextureUnit.Texture6, TextureUnit.Texture7, TextureUnit.Texture8,
        TextureUnit.Texture9, TextureUnit.Texture10, TextureUnit.Texture11,
        TextureUnit.Texture12, TextureUnit.Texture13, TextureUnit.Texture14,
        TextureUnit.Texture15, TextureUnit.Texture16, TextureUnit.Texture17,
        TextureUnit.Texture18, TextureUnit.Texture19, TextureUnit.Texture20,
        TextureUnit.Texture21, TextureUnit.Texture22, TextureUnit.Texture23,
        TextureUnit.Texture24, TextureUnit.Texture25, TextureUnit.Texture26,
        TextureUnit.Texture27, TextureUnit.Texture28, TextureUnit.Texture29,
        TextureUnit.Texture30, TextureUnit.Texture31
    };
    
    Shader shader;

    (int vertex_buffer, int index_buffer, int vertex_array) handlers = new();
    int trangles_count;

    Vector3 position;
    Vector3 rotation;

    bool disposed;

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }
    public Vector3 Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    private Mesh(Shader shader)
    {
        this.shader = shader;
    }

    public Shader Shader => shader;

    public (int VertexBuffer, int IndexBuffer, int VertexArray) Handle => handlers;
    public int TranglesCount => trangles_count / 3;

    public void Draw(Matrix4 projection, Matrix4 model)
    {
        if (disposed)
            return;
        if (shader.IsDisposed) return;

        GL.UseProgram(shader.Handle);

        Matrix4 world_matrix;
        {
            Matrix4 mat = Matrix4.CreateTranslation(Position) *
            ((Rotation != new Vector3()) ?
                Matrix4.CreateFromQuaternion(new(Rotation)) :
                Engine.NeutralMatrix);

            world_matrix = model * mat * projection;
        }

        cull_face();
        simpler_tests();
        blend_system();
        bind_textures();

        if (shader.DefaultMatrixSystem.HasValue)
            GL.UniformMatrix4(shader.DefaultMatrixSystem.Value.handle, false, ref world_matrix);
        if (shader.PozitionVariable.HasValue)
        {
            Vector3 vec = position;
            GL.Uniform3(shader.PozitionVariable.Value.handle, ref vec);
        }
        if (shader.RotationVariable.HasValue)
        {
            Vector3 vec = rotation;
            GL.Uniform3(shader.RotationVariable.Value.handle, ref vec);
        }

        GL.BindVertexArray(handlers.vertex_array);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, handlers.index_buffer);
        GL.DrawElements(PrimitiveType.Triangles, trangles_count, DrawElementsType.UnsignedInt, 0);

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.UseProgram(0);

        //Methods
        void cull_face()
        {
            if (shader.CullFace == CullFaceMode.FrontAndBack)
            {
                if (GL.IsEnabled(EnableCap.CullFace))
                    GL.Disable(EnableCap.CullFace);
            }
            else
            {
                if (!GL.IsEnabled(EnableCap.CullFace))
                    GL.Enable(EnableCap.CullFace);
                GL.CullFace(shader.CullFace);
            }
        }
        void blend_system()
        {
            var blend = Shader.blendfunc;
            Engine.SetMekanizm(EnableCap.Blend, blend.HasValue);
            if(blend.HasValue)
                GL.BlendFunc(blend.Value.s, blend.Value.d);
        }
        void simpler_tests()
        {
            var mekanizms = Shader.simple_mekanizms;
            Engine.SetMekanizm(EnableCap.AlphaTest, mekanizms.alphatest);
            Engine.SetMekanizm(EnableCap.DepthTest, mekanizms.depthtest);
        }
    }

    void bind_textures()
    {
        for(int i = 0; i< shader.Textures.Count;i++)
        {
            var tex = shader.Textures[i];
            GL.ActiveTexture(texture_uints[i]);
            if (tex != null)
                GL.BindTexture(tex.Target, tex.Handle);
            else
                GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }

    ~Mesh() => Dispose();
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;
        GC.SuppressFinalize(this);
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.DeleteVertexArray(handlers.vertex_buffer);
        GL.DeleteBuffer(handlers.index_buffer);
        GL.DeleteBuffer(handlers.vertex_buffer);
        shader = null!;
    }
}
