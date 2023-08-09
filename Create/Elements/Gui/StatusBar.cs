using Create.Linq;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements.Gui;

public class StatusBar : Element
{
    static Shader shader = Assets.GetShader("create:interface/statusbar");
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

    Vector2i back_offs, back_size;
    Vector2i? half_offs, half_size;
    Vector2i full_offs, full_size;
    int points = 10, filled, offset = -1;

    public Texture2D Texture { get => texture; set => texture = value; }

    public ((int x, int y) offset, (int Width, int Height) size) Background
    {
        get => ((back_offs.X, back_offs.Y), (back_size.X, back_size.Y));
        set => ((back_offs.X, back_offs.Y), (back_size.X, back_size.Y)) = value;
    }

    public ((int x, int y) offset, (int Width, int Height) size)? HalfPoint
    {
        get => half_offs.HasValue ? ((half_offs.Value.X, half_offs.Value.Y), (half_size!.Value.X, half_size.Value.Y)) : null;
        set
        {
            if (value.HasValue)
                (half_offs, half_size) = (value.Value.offset.ToVector(), value.Value.size.ToVector());
            else
                (half_offs, half_size) = (null, null);
        }
    }

    public ((int x, int y) offset, (int Width, int Height) size) FullPoint
    {
        get => ((full_offs.X, full_offs.Y), (full_size.X, full_size.Y));
        set => ((full_offs.X, full_offs.Y), (full_size.X, full_size.Y)) = value;
    }

    public int Points { get => points; set => points = value > 0 ? value : throw new ArgumentOutOfRangeException("Must be above 0"); }
    public int Filled { get => filled; set => filled = value; }
    public int Offset { get => offset; set => offset = value; }

    public override void Draw(Matrix4 projection)
    {
        shader.SetUniform("points_poz", new Matrix4x2(half_offs.GetValueOrDefault(), half_size.GetValueOrDefault(), full_offs, full_size));
        shader.SetUniform("background_poz", new Matrix2(back_offs, back_size));
        shader.SetUniform("texture_size", texture.Size.ToVector());
        shader.SetUniform("half_points", half_offs.HasValue);
        shader.SetUniform("points", points);
        shader.SetUniform("offset", offset);
        shader.SetUniform("filled", filled);
        shader.SetUniform("text", texture);

        model.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
    }
}
