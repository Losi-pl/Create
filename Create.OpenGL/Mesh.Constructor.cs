using Create.Linq;
using OpenTK.Mathematics;

namespace Create.OpenGL;

partial class Mesh
{
    public static Constructor Create(Shader shader) => new(shader);
    
    /// <summary>
    /// Konstruktor <see cref="Mesh"/>ów
    /// </summary>
    public class Constructor
    {
        Shader shader;
        MechDrawingMode mode = MechDrawingMode.Triangle;
        int[]? trangles;
        object[] attributes;
        int line_thicknes = 1;

        public Constructor(Shader shader)
        {
            this.shader = shader;
            attributes = new object[shader.Attributes.Count];
        }

        /// <summary>
        /// Łączy wrzystkie wertexy w trujkąty
        /// </summary>
        /// <param name="trangles"></param>
        /// <returns></returns>
        public Constructor SetTrangles(int[] trangles)
        {
            this.trangles = trangles;
            return this;
        }

        /// <summary>
        /// Ustawia jedną wartość dla wrzystkich werteksów
        /// </summary>
        public Constructor SetVertex(string name, int[] array) => SetValue(name, array, ActiveAttribType.Int);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector2i[] array) => SetValue(name, array, ActiveAttribType.IntVec2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector3i[] array) => SetValue(name, array, ActiveAttribType.IntVec3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector4i[] array) => SetValue(name, array, ActiveAttribType.IntVec4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, float[] array) => SetValue(name, array, ActiveAttribType.Float);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector2[] array) => SetValue(name, array, ActiveAttribType.FloatVec2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector3[] array) => SetValue(name, array, ActiveAttribType.FloatVec3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector4[] array) => SetValue(name, array, ActiveAttribType.FloatVec4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Color4[] array) => SetValue(name, array, ActiveAttribType.FloatVec4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2[] array) => SetValue(name, array, ActiveAttribType.FloatMat2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2x3[] array) => SetValue(name, array, ActiveAttribType.FloatMat2x3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2x4[] array) => SetValue(name, array, ActiveAttribType.FloatMat2x4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3x2[] array) => SetValue(name, array, ActiveAttribType.FloatMat3x2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3[] array) => SetValue(name, array, ActiveAttribType.FloatMat3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3x4[] array) => SetValue(name, array, ActiveAttribType.FloatMat3x4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4x2[] array) => SetValue(name, array, ActiveAttribType.FloatMat4x2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4x3[] array) => SetValue(name, array, ActiveAttribType.FloatMat4x3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4[] array) => SetValue(name, array, ActiveAttribType.FloatMat4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, double[] array) => SetValue(name, array, ActiveAttribType.Double);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector2d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector3d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Vector4d[] array) => SetValue(name, array, ActiveAttribType.DoubleVec4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2x3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2x3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix2x4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat2x4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3x2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3x2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix3x4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat3x4);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4x2d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4x2);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4x3d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4x3);

        /// <summary>
        /// <inheritdoc cref=" SetVertex(string, int[])"/>
        /// </summary>
        public Constructor SetVertex(string name, Matrix4d[] array) => SetValue(name, array, ActiveAttribType.DoubleMat4);

        /// <summary>
        /// Sprawdza czy werteksy modelu mają parametr <paramref name="name"/> i czy jest on zgodny z typem <paramref name="type1"/> albo <paramref name="type2"/>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Mode in witch model will be drawn
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Constructor DrawingMode(MechDrawingMode mode)
        {
            this.mode = mode;
            return this;
        }

        public Constructor LineThickness(int thicknes)
        {
            line_thicknes = thicknes;
            return this;
        }

        /// <summary>
        /// Łączy wrzystkie dane w cały model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Mesh Finish()
        {
            if (trangles == null)
                throw new Exception("No trangles specified");
            if (mode == MechDrawingMode.Triangle)
                if (trangles.Length % 3 != 0)
                    throw new Exception("Not every set of triangles is fully set");
            if (mode == MechDrawingMode.Line)
                if (trangles.Length % 2 != 0)
                    throw new Exception("Not every set of lines is fully set");
            foreach (var att in attributes)
                if (att is not Array)
                    throw new Exception("Not every attrybute was specyfied");
            int? lenght = null;
            if (attributes.Any(attr =>
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
            if (trangles.Any(vert => vert >= lenght))
                throw new Exception("Trangle out of range");
            lenght = lenght ?? 0;
            var buffer = map_vertices(lenght.Value);
            Mesh mesh = new(shader);
            mesh.mode = mode;
            mesh.line_thicknes = line_thicknes;
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

        /// <summary>
        ///  Łączy wrzystkie dane w cały model i wykonuje na min akcje <paramref name="action"/>
        /// </summary>
        /// <param name="action">Akcja do wykonania na gotowym modelu</param>
        /// <returns></returns>
        public Mesh Finish(Action<Mesh> action)
        {
            var m = Finish();
            action(m);
            return m;
        }
    }
}
