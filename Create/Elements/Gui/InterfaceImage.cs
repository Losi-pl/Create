using Create.Linq;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements.Gui;

public class InterfaceImage : Element
{
    static Shader shader = Assets.GetShader("create:interface/interfaceimage");
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
    Texture2D texture;
    #pragma warning restore CS8618

    Vector2i offset, size;

    public Texture2D Texture { get => texture; set => texture = value; }

    public (int x, int z) Offset { get => (offset.X, offset.Y); set => (offset.X, offset.Y) = value; }
    public (int Width, int Height) Size { get => (size.X, size.Y); set => (size.X, size.Y) = value; }

    public override void Draw(Matrix4 projection)
    {
        shader.SetUniform("test", texture);
        shader.SetUniform("pointer_poz", new Matrix3x2(texture.Size.ToVector(), offset, size));
        model.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
    }
}
