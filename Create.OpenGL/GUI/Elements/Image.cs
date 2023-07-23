using Create.OpenGL.Textures;
using OpenTK.Mathematics;

namespace Create.OpenGL.GUI.Elements;

public class Image : Element
{
    static Shader shader = Shader.Create()
        .VertexCode(@"#version 440 core
            in vec2 poz;

            uniform mat4 matrix;

            out vec2 uv;

            void main()
            {
                uv = poz;
                gl_Position = matrix * vec4(poz - vec2(.5, .5), 0.0, 1.0);
            }")
        .FragmentCode(@"#version 440 core
            in vec2 uv;

            uniform vec4 color;
            uniform sampler2D tex;
            uniform bool use_tex;

            out vec4 color_o;

            void main()
            {
                if (use_tex)
                    color_o = texture(tex, uv) * color;
                else
                    color_o = color;
            }")
        .ProjectionMatrixUniform("matrix")
        .AlphaTest()
        .Blend(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha)
        .DepthTest(false)
        .Finish()
        .SetUniform("color", new Vector4(255, 255, 255, 255));

    static Mesh mesh = Mesh.Create(shader)
        .SetVertex("poz", new Vector2[]
        {
            new (0, 0),
            new (1, 0),
            new (0, 1),
            new (1, 1)
        })
        .SetTrangles(new int[]
        {
            0,2,3,
            0,3,1
        })
        .Finish();

    Color4 color = Color4.White;
    Texture2D? texture;

    public Texture2D? Texture { get => texture; set => texture = value; }
    public Color4 Color { get => color; set => color = value; }

    public override void Draw(Matrix4 projection)
    {
        if(texture != null)
            shader.SetUniform("tex", texture);
        shader.SetUniform("use_tex", texture != null);
        shader.SetUniform("color", new Vector4(color.R, color.G, color.B, color.A));
        mesh.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
    }
}
