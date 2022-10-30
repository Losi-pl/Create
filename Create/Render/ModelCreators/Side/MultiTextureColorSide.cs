using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Side;

public ref struct MultiTextureColorSide
{
    public Span<Vector2> uvs;
    public Span<Vector3> pozitions;
    public Span<int> trangles;
    public int texture_bottom, texture_top;
    public Color4 top_color;
}
