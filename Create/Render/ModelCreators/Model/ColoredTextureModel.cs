using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;
using Create.Linq;

namespace Create.Render.ModelCreators.Model;

public sealed class ColoredTextureModel : ChunkModel
{
    internal List<Vector2> uvs = new();
    internal List<Vector3> pozitions = new();
    internal List<Color4> colors = new();
    internal List<int> ints = new(), trangles = new();

    public static Shader Shader { get; } = Assets.GetShader("create:terrain/singlecoloredtexture").InvokeFor(s =>
        s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas));

    /// <summary>
    /// Dodaje dane o sześcianie do modelu
    /// </summary>
    /// <param name="side"></param>
    /// <exception cref="Exception"></exception>
    [Obsolete("Ta metoda nie jest już używana i nie działa", true)]
    public void AddSide(ColoredTextureSide side) { }

    public override Mesh FinischModel() => Mesh.Create(Shader)
        .SetTrangles(trangles.ToArray())
        .SetVertex("uv", uvs.ToArray())
        .SetVertex("poz", pozitions.ToArray())
        .SetVertex("texture_index", ints.ToArray())
        .SetVertex("color", colors.ToArray())
        .Finish();
}
