using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;

namespace Create.Sceans;

partial class GameView
{
    public sealed class Crosshair : Element
    {
        static Shader shader = Shader.Create()
            .VertexCode(@"#version 440 core
                in vec2 poz;

                uniform mat4 matrix;

                out vec2 tex_uv;
                out vec2 pan_uv;

                void main()
                {
                    vec4 fin = matrix * vec4(poz - vec2(.5, .5), 0.0, 1.0);

                    tex_uv = poz;
                    pan_uv = vec2((fin.x + 1) / 2, (fin.y + 1) / 2);

                    gl_Position = fin;
                }")
            .FragmentCode(@"#version 440 core
                in vec2 tex_uv;
                in vec2 pan_uv;

                uniform sampler2D pointer;
                uniform sampler2D panorama;
                uniform mat3x2 pointer_poz;
                uniform vec2 color_range;

                out vec4 color_o;

                void main()
                {
                    vec2 uv_poz = (pointer_poz[1] / pointer_poz[0]) + ((pointer_poz[2] / pointer_poz[0]) * tex_uv);
                    float vis = texture(pointer, uv_poz).w;

                    if(vis == 0)
                        discard;

                    color_o = vec4(1, 1, 1, 2) - texture(panorama, pan_uv);
                    color_o = vec4(vec3(color_range.x), 0) + (color_o * (color_range.y - color_range.x));
                    color_o.w = 1;
                }")
            .ProjectionMatrixUniform("matrix")
            .DepthTest(false)
            .Finish();

        static Mesh model = Mesh.Create(shader)
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

        #pragma warning disable CS8618
        RenderTexture terain;
        Texture2D _interface;
        #pragma warning restore CS8618

        Vector2i offset, size;
        Vector2 col_range = new(.55f, 1);

        public RenderTexture Terrain { get => terain; set => terain = value; }
        public Texture2D Interface { get => _interface; set => _interface = value; }

        public (int x, int z) Offset { get => (offset.X, offset.Y); set => (offset.X, offset.Y) = value; }
        public (int Width, int Height) Size { get => (size.X, size.Y); set => (size.X, size.Y) = value; }
        public (float Min, float Max) Range { get => (col_range.X, col_range.Y); set => (col_range.X, col_range.Y) = value; }

        public override void Draw(Matrix4 projection)
        {
            shader.SetUniform("panorama", terain);
            shader.SetUniform("pointer", _interface);
            shader.SetUniform("color_range", col_range);
            shader.SetUniform("pointer_poz", new Matrix3x2(_interface.Size.ToVector(), offset, size));
            model.Draw(Matrix4.CreateScale(Point.Size.Width, Point.Size.Height, 1) * projection);
        }
    }
}
