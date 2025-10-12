using OpenTK.Mathematics;
using System.Diagnostics;

namespace Create.OpenGL;

/// <summary>
/// Odwołanie do modelu zawartego w pamięci karty graficznej
/// </summary>
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
    int trangles_count, line_thicknes;
    MechDrawingMode mode;

    Vector3 position;
    Vector3 rotation;

    bool disposed;

    /// <summary>
    /// Pozycja modelu w przestrzeni
    /// </summary>
    public Vector3 Position
    {
        get => position;
        set => position = value;
    }
    
    /// <summary>
    /// Orjentacja modelu w przestrzeni
    /// </summary>
    public Vector3 Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    private Mesh(Shader shader)
    {
        this.shader = shader;
    }

    /// <summary>
    /// <see cref="OpenGL.Shader"/> używany do renderowania danego modelu
    /// </summary>
    public Shader Shader => shader;

    /// <summary>
    /// Odwołania do obiektów zapisanych w pamięci karty graficznej
    /// </summary>
    public (int VertexBuffer, int IndexBuffer, int VertexArray) Handle => handlers;

    /// <summary>
    /// Liczba trójkątów z których model jest złorzony
    /// </summary>
    public int TrianglesCount => mode == MechDrawingMode.Triangle ? trangles_count / 3 : throw new("Mesh mode isn't set on triangles");

    /// <summary>
    /// Liczba linji z których model jest złorzony
    /// </summary>
    public int LinesCount => mode == MechDrawingMode.Line ? trangles_count / 2 : throw new("Mesh mode isn't set on lines");

    public void Draw(Matrix4 projection, Matrix4 model)
    {
        if (disposed)
            return;
        if (shader.IsDisposed) return;

        GL.UseProgram(shader.Handle);

        cull_face();
        simpler_tests();
        blend_system();
        bind_textures();

        if (shader.DefaultMatrixSystem.HasValue)
        {
            Matrix4 mat = Matrix4.CreateTranslation(Position) *
            ((Rotation != new Vector3()) ?
                Matrix4.CreateFromQuaternion(new(Rotation)) :
                Engine.NeutralMatrix);
            Matrix4 world_matrix = model * mat * projection;
            GL.UniformMatrix4(shader.DefaultMatrixSystem.Value.handle, false, ref world_matrix);
        }
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

        if(mode == MechDrawingMode.Line)
            GL.LineWidth(line_thicknes);
        GL.BindVertexArray(handlers.vertex_array);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, handlers.index_buffer);
        GL.DrawElements(mode switch
        {
            MechDrawingMode.Triangle => PrimitiveType.Triangles,
            MechDrawingMode.Line => PrimitiveType.Lines,
            _ => PrimitiveType.Triangles,
        }, trangles_count, DrawElementsType.UnsignedInt, 0);

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.UseProgram(0);

        //Methods
        void cull_face()
        {
            if (shader.CullFace == CullFaceMode.FrontAndBack)
            {
                GL.Disable(EnableCap.CullFace);
            }
            else
            {
                GL.Enable(EnableCap.CullFace);
                GL.CullFace((TriangleFace)shader.CullFace);
            }
        }
        void blend_system()
        {
            var blend = Shader.blendfunc;

            if (blend.HasValue)
                GL.Enable(EnableCap.Blend);
            else
                GL.Disable(EnableCap.Blend);

            if (blend.HasValue)
                GL.BlendFunc(blend.Value.s, blend.Value.d);
        }
        void simpler_tests()
        {
            var mekanizms = Shader.simple_mekanizms;

            if (mekanizms.depthtest)
                GL.Enable(EnableCap.DepthTest);
            else
                GL.Disable(EnableCap.DepthTest);

            if (mekanizms.alphatest)
                GL.Enable(EnableCap.AlphaTest);
            else
                GL.Disable(EnableCap.AlphaTest);
        }
    }

    /// <summary>
    /// Aktywuje w pamięci tekstury używane przez ten model
    /// </summary>
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
        OpenGL.Disposing.add(new Disposing() { 
            va = handlers.vertex_array, 
            ib = handlers.index_buffer, 
            vb = handlers.vertex_buffer });
        shader = null!;
    }

    /// <summary>
    /// Niszczenie modelu gdy nie jest już potrzebny
    /// </summary>
    struct Disposing : OpenGL.Disposing.gl_element
    {
        public int va, ib, vb;

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteVertexArray(va);
            GL.DeleteBuffer(ib);
            GL.DeleteBuffer(vb);
        }
    }
}

public enum MechDrawingMode
{
    Triangle,
    Line
}