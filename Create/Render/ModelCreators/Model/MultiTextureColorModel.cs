using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Model;

public sealed class MultiTextureColorModel : ChunkModel
{
    List<Vector2> uvs = new();
    List<Vector3> pozitions = new();
    List<int> tex_top = new(), tex_bottom = new(), trangles = new();
    List<Color4> color = new();

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
    public void AddSide(MultiTextureColorSide side)
    {
        if (side.uvs.Length != side.pozitions.Length)
            throw new Exception("Data sizes are not match");

        for (int i = 0; i < side.trangles.Length; ++i)
            trangles.Add(side.trangles[i] + uvs.Count);

        uvs.AddRange(side.uvs);
        pozitions.AddRange(side.pozitions);

        for (int i = side.pozitions.Length; i > 0; --i)
            tex_top.Add(side.texture_top);
        for (int i = side.pozitions.Length; i > 0; --i)
            tex_bottom.Add(side.texture_bottom);
        for (int i = side.pozitions.Length; i > 0; --i)
            color.Add(side.top_color);
    }
}
