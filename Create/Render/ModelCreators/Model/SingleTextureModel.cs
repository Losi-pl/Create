using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Model;

public sealed class SingleTextureModel : ChunkModel
{
    List<Vector2> uvs = new();
    List<Vector3> pozitions = new();
    List<int> ints = new(), trangles = new();

    public static Shader Shader { get; } = Assets.GetShader("create:terrain/singletexture").InvokeFor(s =>
        s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas).SetUniform("is_debug_mode", false)); 
    
    /// <summary>
    /// Dodaje dane o sześcianie do modelu
    /// </summary>
    /// <param name="side"></param>
    /// <exception cref="Exception"></exception>
    public void AddSide(SingleTextureSide side)
    {
        if (side.uvs.Length != side.pozitions.Length)
            throw new Exception("Data sizes are not match");

        for (int i = 0; i < side.trangles.Length; i++)
            trangles.Add(side.trangles[i] + uvs.Count);

        uvs.AddRange(side.uvs);
        pozitions.AddRange(side.pozitions);

        for (int i = side.pozitions.Length; i > 0; --i)
            ints.Add(side.texture_side);
    }

    public override Mesh FinischModel() => Mesh.Create(Shader)
        .SetTrangles(trangles.ToArray())
        .SetVertex("uv", uvs.ToArray())
        .SetVertex("poz", pozitions.ToArray())
        .SetVertex("texture_index", ints.ToArray())
        .Finish();
}
