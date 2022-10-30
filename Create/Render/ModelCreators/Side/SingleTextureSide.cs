using Create.Elements;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Side;

public ref struct SingleTextureSide
{
    public Span<Vector2> uvs;
    public Span<Vector3> pozitions;
    public Span<int> trangles;
    public int texture_side;
}
