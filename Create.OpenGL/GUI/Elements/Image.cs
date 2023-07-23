using Create.OpenGL.Textures;
using OpenTK.Mathematics;

namespace Create.OpenGL.GUI.Elements;

public class Image : Element
{
    static Shader shader = null!;
    static Mesh mesh = null!;

    Color4 color = Color4.White;
    Texture2D? texture;

    public Texture2D? Texture { get => texture; set => texture = value; }
    public Color4 Color { get => color; set => color = value; }

    internal static void set_shader(Shader shader)
    {
        if(Image.shader is not null)
        {
            Image.mesh.Dispose();
            Image.shader.Dispose();
        }
        
        Image.shader = shader.SetUniform("color", (Vector4)Color4.White);
        Image.mesh = Mesh.Create(shader)
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
    }

    public override void Draw(Matrix4 projection)
    {
        if (shader is null)
            return;
        if(texture != null)
            shader.SetUniform("tex", texture);
        shader.SetUniform("use_tex", texture != null);
        shader.SetUniform("color", new Vector4(color.R, color.G, color.B, color.A));
        mesh.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
    }
}
