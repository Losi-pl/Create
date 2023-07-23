using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements.Gui;

public class StatusBar : Element
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

            uniform sampler2D text;
            uniform ivec2 texture_size;
            uniform mat2x2 background_poz;
            uniform mat4x2 points_poz;
            uniform int filled;
            uniform int points;
            uniform int offset;
            uniform bool half_points;

            out vec4 color_o;

            int width()
            {
                return int(background_poz[1].x) + ((int(background_poz[1].x) + offset) * (points - 1));
            }
            int sec_start(int poi)
            {
                return (offset + int(background_poz[1].x)) * poi;
            }
            ivec2 pix_point(ivec2 siz)
            {
                return ivec2(int(uv.x * siz.x), int(uv.y * siz.y));
            }
            int point_index(ivec2 pix)
            {
                if(pix.x < background_poz[1].x)
                    return 0;

                return int((pix.x - background_poz[1].x) / (background_poz[1].x + offset)) + 1;
            }
            vec4 full_icon(int index, vec2 in_uv)
            {
                vec2 back_uv = (background_poz[0] / texture_size) + ((background_poz[1] / vec2(texture_size)) * in_uv);
                vec2 full_uv = (points_poz[2] / texture_size) + ((points_poz[3] / texture_size) * in_uv);

                if(filled > index)
                {
                    vec4 b = texture(text, full_uv);
                    if(b.w == 0)
                        return texture(text, back_uv);
                    else
                        return b;
                }
                else
                    return texture(text, back_uv);
            }
            vec4 half_icon(int index, vec2 in_uv)
            {
                vec2 back_uv = (background_poz[0] / texture_size) + ((background_poz[1] / vec2(texture_size)) * in_uv);
                vec2 full_uv = (points_poz[2] / texture_size) + ((points_poz[3] / texture_size) * in_uv);
                vec2 half_uv = (points_poz[0] / texture_size) + ((points_poz[1] / texture_size) * in_uv);

                bool m = false;
                    if(filled % 2 == 0)
                        m = filled / 2 > index;
                    else
                        m = filled / 2 + 1 > index;

                    if(m)
                    {
                        vec4 b;

                        if (filled / 2 == index)
                            b = texture(text, half_uv);
                        else
                            b = texture(text, full_uv);

                        if(b.w == 0)
                            return texture(text, back_uv);
                        else
                            return b;
                    }
                    else
                        return texture(text, back_uv);
            }

            void main()
            {
                int w = width();
                ivec2 pix_poz = pix_point(ivec2(w, background_poz[1].y));
                int index = point_index(pix_poz);
                vec2 in_uv = vec2(((w * uv.x) - sec_start(index)) / background_poz[1].x, uv.y);

                if(in_uv.x < 0 || in_uv.x > 1 || in_uv.y < 0 || in_uv.y > 1)
                    discard;

                if(half_points)
                    color_o = half_icon(index, in_uv);
                else
                    color_o = full_icon(index, in_uv);

                if(offset < 0 && index < points - 1)
                {
                    in_uv = vec2(((w * uv.x) - sec_start(index + 1)) / background_poz[1].x, uv.y);
                    if(in_uv.x >= 0)
                    {
                        vec4 n_color;
                        if(half_points)
                            n_color = half_icon(index + 1, in_uv);
                        else
                            n_color = full_icon(index + 1, in_uv);

                        if(n_color.a != 0)
                            color_o = n_color;
                    }
                }
            }")
        .ProjectionMatrixUniform("matrix")
        .DepthTest(false)
        .AlphaTest(true)
        .Blend(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha)
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
    Texture2D texture;
    #pragma warning restore CS8618

    Vector2i back_offs, back_size;
    Vector2i? half_offs, half_size;
    Vector2i full_offs, full_size;
    int points = 10, filled, offset = -1;

    public Texture2D Texture { get => texture; set => texture = value; }

    public ((int x, int y) offset, (int Width, int Height) size) Background
    {
        get => ((back_offs.X, back_offs.Y), (back_size.X, back_size.Y));
        set => ((back_offs.X, back_offs.Y), (back_size.X, back_size.Y)) = value;
    }

    public ((int x, int y) offset, (int Width, int Height) size)? HalfPoint
    {
        get => half_offs.HasValue ? ((half_offs.Value.X, half_offs.Value.Y), (half_size!.Value.X, half_size.Value.Y)) : null;
        set
        {
            if (value.HasValue)
                (half_offs, half_size) = (value.Value.offset.ToVector(), value.Value.size.ToVector());
            else
                (half_offs, half_size) = (null, null);
        }
    }

    public ((int x, int y) offset, (int Width, int Height) size) FullPoint
    {
        get => ((full_offs.X, full_offs.Y), (full_size.X, full_size.Y));
        set => ((full_offs.X, full_offs.Y), (full_size.X, full_size.Y)) = value;
    }

    public int Points { get => points; set => points = value > 0 ? value : throw new ArgumentOutOfRangeException("Must be above 0"); }
    public int Filled { get => filled; set => filled = value; }
    public int Offset { get => offset; set => offset = value; }

    public override void Draw(Matrix4 projection)
    {
        shader.SetUniform("points_poz", new Matrix4x2(half_offs.GetValueOrDefault(), half_size.GetValueOrDefault(), full_offs, full_size));
        shader.SetUniform("background_poz", new Matrix2(back_offs, back_size));
        shader.SetUniform("texture_size", texture.Size.ToVector());
        shader.SetUniform("half_points", half_offs.HasValue);
        shader.SetUniform("points", points);
        shader.SetUniform("offset", offset);
        shader.SetUniform("filled", filled);
        shader.SetUniform("text", texture);

        model.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
    }
}
