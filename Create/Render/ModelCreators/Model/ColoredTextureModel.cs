using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Model;

public sealed class ColoredTextureModel : ChunkModel
{
    List<Vector2> uvs = new();
    List<Vector3> pozitions = new();
    List<Color4> colors = new();
    List<int> ints = new(), trangles = new();

    public static Shader Shader { get; } = Shader.Create()
        .VertexCode(@"#version 440 core
            in int texture_index;
            in vec4 color;
            in vec3 poz;
            in vec2 uv;

            uniform mat4 matrix;

            out vec3 uv_f;
            out vec4 color_f;

            void main()
            {
                uv_f = vec3(uv, texture_index);
                color_f = color;
                gl_Position = matrix * vec4(poz, 1.0);
            }")
        .FragmentCode(@"#version 440 core
            in vec3 uv_f;
            in vec4 color_f;

            uniform sampler2DArray block_atlas;

            out vec4 color;

            void main()
            {
                color = vec4(texture(block_atlas, uv_f).rgb * color_f.rgb, 1.0);
            }")
        .CullFace(OpenTK.Graphics.OpenGL.CullFaceMode.Front)
        .ProjectionMatrixUniform("matrix")
        .Finish(s => s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas));

    /// <summary>
    /// Dodaje dane o sześcianie do modelu
    /// </summary>
    /// <param name="side"></param>
    /// <exception cref="Exception"></exception>
    public void AddSide(ColoredTextureSide side)
    {
        if (side.uvs.Length != side.pozitions.Length)
            throw new Exception("Data sizes are not match");

        for (int i = 0; i < side.trangles.Length; i++)
            trangles.Add(side.trangles[i] + uvs.Count);

        uvs.AddRange(side.uvs);
        pozitions.AddRange(side.pozitions);

        for (int i = side.pozitions.Length; i > 0; --i)
            ints.Add(side.texture_side);
        for (int i = side.pozitions.Length; i > 0; --i)
            colors.Add(side.color);
    }

    public override Mesh FinischModel() => Mesh.Create(Shader)
        .SetTrangles(trangles.ToArray())
        .SetVertex("uv", uvs.ToArray())
        .SetVertex("poz", pozitions.ToArray())
        .SetVertex("texture_index", ints.ToArray())
        .SetVertex("color", colors.ToArray())
        .Finish();
}
