using Create.Elements;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Side;

public ref struct ColoredTextureSide
{
    public Span<Vector2> uvs;
    public Span<Vector3> pozitions;
    public Span<int> trangles;
    public int texture_side;
    public Color4 color;
}
