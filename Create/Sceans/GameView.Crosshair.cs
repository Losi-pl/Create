using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;

namespace Create.Sceans;

partial class GameView
{
    public sealed class Crosshair : Element
    {
        static Shader shader = Assets.GetShader("create:interface/crosshair");
        static Mesh model = Mesh.Create(shader)
            .SetVertex("poz", new Vector2[]
            {
                new (0, 0),
                new (1, 0),
                new (0, 1),
                new (1, 1)
            })
            .SetTrangles(new int[]
            {
                0,2,3,
                0,3,1
            })
            .Finish();

        #pragma warning disable CS8618
        RenderTexture terain;
        Texture2D _interface;
        #pragma warning restore CS8618

        Vector2i offset, size;
        Vector2 col_range = new(.55f, 1);

        public RenderTexture Terrain { get => terain; set => terain = value; }
        public Texture2D Interface { get => _interface; set => _interface = value; }

        public (int x, int z) Offset { get => (offset.X, offset.Y); set => (offset.X, offset.Y) = value; }
        public (int Width, int Height) Size { get => (size.X, size.Y); set => (size.X, size.Y) = value; }
        public (float Min, float Max) Range { get => (col_range.X, col_range.Y); set => (col_range.X, col_range.Y) = value; }

        public override void Draw(Matrix4 projection)
        {
            shader.SetUniform("panorama", terain);
            shader.SetUniform("pointer", _interface);
            shader.SetUniform("color_range", col_range);
            shader.SetUniform("pointer_poz", new Matrix3x2(_interface.Size.ToVector(), offset, size));
            model.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
        }
    }
}
