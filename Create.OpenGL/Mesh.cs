using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Create.OpenGL;

public sealed partial class Mesh : IDisposable
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
    
    public static Constructor Create(Shader shader) => new(shader);
    
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
    public int TranglesCount => trangles_count;

    internal void Draw(Matrix4 world_matrix)
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
    public void Draw() => Draw(Engine.NeutralMatrix);

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

    public class Constructor
    {
        Shader shader;

        int[]? trangles;
        object[] attributes;


        public Constructor(Shader shader)
        {
            this.shader = shader;
            attributes = new object[shader.Attributes.Count];
        }

        public Constructor SetTrangles(int[] trangles) 
        {
            this.trangles = trangles;
            return this;
        }

        public Constructor SetVertex(string name, int[] array) => SetValue(name, array, ActiveAttribType.Int);
        public Constructor SetVertex(string name, Vector2i[] array) => SetValue(name, array, ActiveAttribType.IntVec2);
        public Constructor SetVertex(string name, Vector3i[] array) => SetValue(name, array, ActiveAttribType.IntVec3);
        public Constructor SetVertex(string name, Vector4i[] array) => SetValue(name, array, ActiveAttribType.IntVec4);
        public Constructor SetVertex(string name, float[] array) => SetValue(name, array, ActiveAttribType.Float);
        public Constructor SetVertex(string name, Vector2[] array) => SetValue(name, array, ActiveAttribType.FloatVec2);
        public Constructor SetVertex(string name, Vector3[] array) => SetValue(name, array, ActiveAttribType.FloatVec3);
        public Constructor SetVertex(string name, Vector4[] array) => SetValue(name, array, ActiveAttribType.FloatVec4);
        public Constructor SetVertex(string name, Color4[] array) => SetValue(name, array, ActiveAttribType.FloatVec4);
        public Constructor SetVertex(string name, Matrix2[] array) => SetValue(name, array, ActiveAttribType.FloatMat2);
        public Constructor SetVertex(string name, Matrix2x3[] array) => SetValue(name, array, ActiveAttribType.FloatMat2x3);
        public Constructor SetVertex(string name, Matrix2x4[] array) => SetValue(name, array, ActiveAttribType.FloatMat2x4);
        public Constructor SetVertex(string name, Matrix3x2[] array) => SetValue(name, array, ActiveAttribType.FloatMat3x2);
        public Constructor SetVertex(string name, Matrix3[] array) => SetValue(name, array, ActiveAttribType.FloatMat3);
        public Constructor SetVertex(string name, Matrix3x4[] array) => SetValue(name, array, ActiveAttribType.FloatMat3x4);
        public Constructor SetVertex(string name, Matrix4x2[] array) => SetValue(name, array, ActiveAttribType.FloatMat4x2);
        public Constructor SetVertex(string name, Matrix4x3[] array) => SetValue(name, array, ActiveAttribType.FloatMat4x3);
        public Constructor SetVertex(string name, Matrix4[] array) => SetValue(name, array, ActiveAttribType.FloatMat4);
        public Constructor SetVertex(string name, double[] array) => SetValue(name, array, ActiveAttribType.Double);
        public Constructor SetVertex(string name, Vector2d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec2);
        public Constructor SetVertex(string name, Vector3d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec3);
        public Constructor SetVertex(string name, Vector4d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec4);
        public Constructor SetVertex(string name, Matrix2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2);
        public Constructor SetVertex(string name, Matrix2x3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2x3);
        public Constructor SetVertex(string name, Matrix2x4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2x4);
        public Constructor SetVertex(string name, Matrix3x2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3x2);
        public Constructor SetVertex(string name, Matrix3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3);
        public Constructor SetVertex(string name, Matrix3x4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3x4);
        public Constructor SetVertex(string name, Matrix4x2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4x2);
        public Constructor SetVertex(string name, Matrix4x3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4x3);
        public Constructor SetVertex(string name, Matrix4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4);

        Constructor SetValue<T>(string name, T[] values, ActiveAttribType type1, ActiveAttribType? type2 = null)
        {
            var info = shader.Attributes.FindAndWhere(attr => attr.Name == name);
            if (!info.HasValue)
                throw new ArgumentException("Invalid name");
            var elem = info.Value;
            if (elem.element.GLType != type1 && elem.element.GLType != type2)
                throw new ArgumentException("Invalid type");
            attributes[elem.index] = values;
            return this;
        }

        public Mesh Finish()
        {
            Mesh mesh = new(shader);
            if (trangles == null)
                throw new Exception("No trangles specified");
            foreach (var att in attributes)
                if (att is not Array)
                    throw new Exception("Not every attrybute was specyfied");
            int? lenght = null;
            if (attributes.ConditionsMeet(attr =>
            {
                if (!lenght.HasValue)
                {
                    lenght = ((Array)attr).Length;
                    return false;
                }
                else
                    return lenght != ((Array)attr).Length;
            }))
                throw new Exception("Attributes lists lenghts are not math");
            if (trangles.ConditionsMeet(vert => vert >= lenght))
                throw new Exception("Trangle out of range");
            lenght = lenght ?? 0;
            var buffer = map_vertices(lenght.Value);
            mesh.shader = shader;
            int vertex_buff = vertex_buffer((int)lenght, buffer);
            int index_buff = index_byffer(trangles);
            int vertex_bin = vertex_bind(vertex_buff);
            mesh.handlers = (vertex_buff, index_buff, vertex_bin);
            mesh.trangles_count = trangles.Length;
            return mesh;

            //Methods
            byte[] map_vertices(int lenght)
            {
                byte[] bytes = new byte[lenght * shader.ShaderBind.ByteSize];
                foreach (var bind in shader.ShaderBind.Binds)
                    InprintValue(bytes, (Array)attributes[bind.index], bind.offset, shader.ShaderBind.ByteSize);
                return bytes;
            }
            int vertex_buffer(int lenght, byte[] array)
            {
                int handle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
                GL.BufferData(BufferTarget.ArrayBuffer, array.Length, array, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                return handle;
            }
            int index_byffer(int[] buffer)
            {
                int handle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
                GL.BufferData(BufferTarget.ElementArrayBuffer, buffer.Length * sizeof(int), buffer, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                return handle;
            }
            int vertex_bind(int vertex_buffer)
            {
                int handle = GL.GenVertexArray();
                GL.BindVertexArray(handle);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer);
                foreach (var bind_data in shader.ShaderBind.Binds)
                {
                    var info = shader.Attributes[bind_data.index];
                    var binds = info.GLType.ValueBindData();
                    GL.VertexAttribPointer(info.Handle, binds.values, binds.type, false, shader.ShaderBind.ByteSize, bind_data.offset);
                    GL.EnableVertexAttribArray(info.Handle);
                }
                GL.BindVertexArray(0);
                return handle;
            }
        }
    }
}
