using Create.OpenGL;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;

namespace Create.Render.ModelCreators.Model;

public sealed class SingleTextureModel : ChunkModel
{
    List<Vector2> uvs = new();
    List<Vector3> pozitions = new();
    List<int> ints = new(), trangles = new();

    public static Shader Shader { get; } = Shader.Create()
        .VertexCode(@"#version 440 core
            in int texture_index;
            in vec3 poz;
            in vec2 uv;

            uniform mat4 matrix;

            out vec3 uv_f;
            out vec4 int_poz;

            void main()
            {
                uv_f = vec3(uv, texture_index);
                
                int_poz = vec4(0,0,0,0);

                int_poz.r = texture_index % 255;
                int_poz.g = (texture_index / 255) % 255;
                int_poz.b = (texture_index / 65025)% 255 ;
                int_poz.a = (texture_index/ 16581375 )% 255 ;

                gl_Position = matrix * vec4(poz, 1.0);
            }")
        .FragmentCode(@"#version 440 core
            in vec3 uv_f;
            in vec4 int_poz;

            uniform sampler2DArray block_atlas;
            uniform bool is_debug_mode = true;

            out vec4 color;

            void main()
            {
                if(is_debug_mode)
                    color = vec4(mix(texture(block_atlas, uv_f), int_poz / vec4(255,255,255,255), 0.5).rgb, 1.0);
                else
                    color = vec4(texture(block_atlas, uv_f).rgb, 1.0);
            }")
        .CullFace(OpenTK.Graphics.OpenGL.CullFaceMode.Front)
        .ProjectionMatrixUniform("matrix")
        .Finish(s => s.SetUniform("block_atlas", Assets.BlockAtlas.Attlas).SetUniform("is_debug_mode", false));

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
