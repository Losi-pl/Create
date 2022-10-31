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

    public static Shader Shader { get; } = Shader.Create()
        .VertexCode(@"#version 440 core
            in int texture_top;
            in int texture_bottom;
            in vec4 tex_color;
            in vec3 poz;
            in vec2 uv;

            uniform mat4 matrix;

            out vec2 uv_f;
            out vec2 tex_f;
            out vec4 color_f;

            void main()
            {
                uv_f = uv;
                tex_f = vec2(texture_bottom, texture_top);
                color_f = tex_color;
                gl_Position = matrix * vec4(poz, 1.0);
            }")
        .FragmentCode(@"#version 440 core
            in vec2 uv_f;
            in vec2 tex_f;
            in vec4 color_f;

            uniform sampler2DArray block_atlas;

            out vec4 color;

            void main()
            {
                vec4 color_bot = texture(block_atlas, vec3(uv_f, tex_f.x));
                vec4 color_top = texture(block_atlas, vec3(uv_f, tex_f.y)) * vec4(color_f.rgb, 1.0);
                vec3 color_mix = mix(color_bot.rgb, color_top.rgb, color_top.a);
                color = vec4(color_mix, 1.0);
            }")
        .CullFace(OpenTK.Graphics.OpenGL.CullFaceMode.Front)
        .ProjectionMatrixUniform("matrix")
        .Finish(s => s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas));

    public override Mesh FinischModel() => Mesh.Create(Shader)
        .SetTrangles(trangles.ToArray())
        .SetVertex("uv", uvs.ToArray())
        .SetVertex("poz", pozitions.ToArray())
        .SetVertex("texture_bottom", tex_bottom.ToArray())
        .SetVertex("texture_top", tex_top.ToArray())
        .SetVertex("tex_color", color.ToArray())
        .Finish();

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
