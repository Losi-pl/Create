using System.Diagnostics;

namespace Create.OpenGL;

partial class Mesh
{
    class Proxy
    {
        Mesh mesh;
        public Proxy(Mesh mesh)
        {
            this.mesh = mesh;
        }

        public handles Handles => new handles(mesh);
        public int TranglesCount => mesh.trangles_count / 3;

        public string status() => mesh.disposed ? "Disposed" : $"Handles: {mesh.handlers}";

        [DebuggerDisplay("")]
        public class handles
        {
            public handles(Mesh mesh)
            {
                VertexBuffer = mesh.handlers.vertex_buffer;
                IndexBuffer = mesh.handlers.index_buffer;
                VertexArray = mesh.handlers.vertex_array;
            }
            public readonly int VertexBuffer, IndexBuffer, VertexArray;
        }
    }
}
