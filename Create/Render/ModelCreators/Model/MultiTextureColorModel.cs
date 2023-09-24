using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;
using Create.Linq;

namespace Create.Render.ModelCreators.Model;

public sealed class MultiTextureColorModel : ChunkModel
{
    internal List<Vector2> uvs = new();
    internal List<Vector3> pozitions = new();
    internal List<int> tex_top = new(), tex_bottom = new(), trangles = new();
    internal List<Color4> color = new();

    public static Shader Shader { get; } = Assets.GetShader("create:terrain/doublecoloredtexture").InvokeFor(s =>
        s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas));
    
    public override Mesh FinischModel() => Mesh.Create(Shader)
        .SetTrangles(trangles.ToArray())
        .SetVertex("uv", uvs.ToArray())
        .SetVertex("poz", pozitions.ToArray())
        .SetVertex("texture_bottom", tex_bottom.ToArray())
        .SetVertex("texture_top", tex_top.ToArray())
        .SetVertex("tex_color", color.ToArray())
        .Finish();

    /// <summary>
    /// Dodaje dane o sześcianie do modelu
    /// </summary>
    /// <param name="side"></param>
    /// <exception cref="Exception"></exception>
    [Obsolete("Ta metoda nie jest już używana i nie działa", true)]
    public void AddSide(MultiTextureColorSide side) { }
}
