using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.Render;
using Create.Virtuals;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using Create.Render.Text;

namespace Create.Elements.Gui;

public sealed class SimpleText : Element
{
    SimpleTextMesh mesh = new();
    public Font? Font { get => mesh.Font; set => mesh.Font = value; }
    public string Text { get => mesh.Text; set => mesh.Text = value; }
    public bool StaticCharWidth { get => mesh.StaticCharWidth; set => mesh.StaticCharWidth = value; }
    public HorizontalAlgin HorizontalAlgin { get => mesh.HorizontalDirection switch {
        HorizontalDirection.Left => HorizontalAlgin.Rigth,
        HorizontalDirection.Rigth => HorizontalAlgin.Left,
        _ => HorizontalAlgin.Center
    }; set => mesh.HorizontalDirection = value switch {
        HorizontalAlgin.Left => HorizontalDirection.Rigth,
        HorizontalAlgin.Rigth => HorizontalDirection.Left,
        _ => HorizontalDirection.Center
    }; }
    public VerticalAlgin VerticalAlgin { get => mesh.VerticalDirection switch
    {
        VerticalDirection.Up => VerticalAlgin.Down,
        VerticalDirection.Down => VerticalAlgin.Up,
        _ => VerticalAlgin.Center
    }; set => mesh.VerticalDirection = value switch
    {
        VerticalAlgin.Up => VerticalDirection.Down,
        VerticalAlgin.Down => VerticalDirection.Up,
        _ => VerticalDirection.Center
    }; }
    public float Size { get => mesh.Size; set => mesh.Size = value; }
    public (float Width, float Height) Dimentions => (mesh.Dimensions.width * Size, mesh.Dimensions.height * Size);
    public Color4 Color { get => mesh.Color; set => mesh.Color = value; }
    public override void Draw(Matrix4 projection)
    {
        Matrix4 matrix = Matrix4.CreateTranslation(mesh.HorizontalDirection switch
        {
            HorizontalDirection.Rigth => -Point!.Size.Width / 2,
            HorizontalDirection.Left => Point!.Size.Width / 2,
            _ => 0
        }, mesh.VerticalDirection switch
        {
            VerticalDirection.Up => -Point!.Size.Height / 2,
            VerticalDirection.Down => Point!.Size.Height / 2,
            _ => 0
        }, 0) * projection;

        mesh.Draw(matrix);
    }
}

public enum HorizontalAlgin
{
    Left, Center, Rigth
}
public enum VerticalAlgin
{
    Up, Center, Down
}