using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.Render;
using Create.Virtuals;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Create.Elements.Gui;

public sealed class SimpleText : Element
{
    static Font default_font = Assets.GetFont("create:default");
    static Shader shader = Assets.GetShader("create:interface/simpletext");

    Mesh?[] meshes = Array.Empty<Mesh?>();
    string text = string.Empty;
    float size = 50;
    bool use_static_char_width;
    Font? used_font;
    Color4 color = Color4.White;
    HorizontalAlgin halgin;
    VerticalAlgin valgin;

    public Font? Font { get => used_font; set 
        {
            if (used_font == value)
                return;
            used_font = value;
            recreate_mesh(false);
        }
    }
    public string Text { get => text; set 
        {
            if (text == value)
                return;
            text = value;
            recreate_mesh(false);
        }
    }
    public bool StaticCharWidth { get => use_static_char_width; set {
            if (use_static_char_width == value)
                return;
            use_static_char_width = value;
            recreate_mesh(false);
        }
    }
    public HorizontalAlgin HorizontalAlgin { get => halgin; set
        {
            if (halgin == value)
                return;
            halgin = value;
            recreate_mesh(false);
        }
    }
    public VerticalAlgin VerticalAlgin { get => valgin; set
        {
            if (valgin == value)
                return;
            valgin = value;
            recreate_mesh(false);
        }
    }
    private Font GetFont() => used_font ?? default_font;
    public float Size { get => size; set => size = value; }
    public Color4 Color { get => color; set => color = value; }
    private void recreate_mesh(bool valie)
    {
        if (!valie && Point is null)
            return;
        var f = GetFont();
        if(meshes is null)
            meshes = new Mesh?[f.Textures.Length];
        else if(meshes.Length != f.Textures.Length)
            meshes = new Mesh?[f.Textures.Length];
        float offset = 0;
        int line = 0;
        Dictionary<int, (List<Vector2> poz, List<Vector2> uv, List<Vector2> size, List<Vector2> offset, List<int> triangles, int new_letters)> mesh_parts = new();
        foreach(var c in text.Pattern("\n", "\r\n").Replace(C => C == '\r', '\n'))
        {
            if(c == '\n')
            {
                line++;
                if (halgin == HorizontalAlgin.Rigth)
                {
                    offset -= f.Spaceing.Horizontal;
                    foreach (var kvp in mesh_parts)
                    {
                        var pozs = kvp.Value.poz;
                        for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                        {
                            var p = pozs[pozs.Count - i - 1];
                            p.X -= offset;
                            pozs[pozs.Count - i - 1] = p;
                        }
                    }
                    foreach (var i in mesh_parts.Keys)
                    {
                        var p = mesh_parts[i];
                        p.new_letters = 0;
                        mesh_parts[i] = p;
                    }
                }
                else if(halgin == HorizontalAlgin.Center)
                {
                    offset -= f.Spaceing.Horizontal;
                    offset /= 2;
                    foreach (var kvp in mesh_parts)
                    {
                        var pozs = kvp.Value.poz;
                        for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                        {
                            var p = pozs[pozs.Count - i - 1];
                            p.X -= offset;
                            pozs[pozs.Count - i - 1] = p;
                        }
                    }
                    foreach (var i in mesh_parts.Keys)
                    {
                        var p = mesh_parts[i];
                        p.new_letters = 0;
                        mesh_parts[i] = p;
                    }
                }   
                offset = 0;
                continue;
            }
            if(c == '\t')
            {
                offset = ((int)(offset / f.Tabs) + 1) * f.Tabs;
                continue;
            }
            var cData = f.Chars.TryGetValue(c, out var d) ? d : 
                f.Chars.TryGetValue(Regex.Replace($"{c}".Normalize(), @"[\p{Mn}\p{Mc}]", "")[0], out d) ? d : 
                f.Chars.TryGetValue(f.DefaultChar, out d) ? d : throw new("No letter data found");
            if(cData.size.y == 0)
            {
                offset += (cData.size.x / (float)f.Textures[cData.index].heigth) + f.Spaceing.Horizontal;
                continue;
            }
            var m_data = mesh_parts.TryGetValue(cData.index, out var mD) ? mD : new();
            if(m_data.poz is null)
            {
                m_data = (new(), new(), new(), new(), new(), 0);
                mesh_parts.Add(cData.index, m_data);
            }
            var char_witch = use_static_char_width ? f.StaticCharWidth : cData.width / (float)f.Textures[cData.index].heigth;
            m_data.new_letters++;
            mesh_parts[cData.index] = m_data;
            m_data.poz.AddRange(Enumerable.Repeat(use_static_char_width ?
                new Vector2(offset, (line * -(1 + f.Spaceing.Vertical)) + f.Spaceing.Vertical) + 
                new Vector2((((cData.offs.x + (cData.size.x / 2f)) / cData.width) * f.StaticCharWidth) -
                        (cData.size.x / 2f / f.Textures[cData.index].heigth), 
                    cData.offs.y / (float)f.Textures[cData.index].heigth) : 
                new Vector2(offset, (line * -(1 + f.Spaceing.Vertical)) + f.Spaceing.Vertical) + 
                    (cData.offs.ToVector().ToVector2() / f.Textures[cData.index].heigth), 4));
            m_data.uv.AddRange(get_uv());
            m_data.triangles.AddRange(get_trang());
            m_data.size.AddRange(Enumerable.Repeat(cData.size.ToVector().ToVector2(), 4));
            m_data.offset.AddRange(Enumerable.Repeat(cData.poz.ToVector().ToVector2(), 4));
            offset += char_witch + f.Spaceing.Horizontal;

            IEnumerable<Vector2> get_uv()
            {
                yield return new(0, 0);
                yield return new(1, 0);
                yield return new(0, 1);
                yield return new(1, 1);
            }
            IEnumerable<int> get_trang()
            {
                yield return m_data.poz.Count - 4; // 1
                yield return m_data.poz.Count - 2; // 3
                yield return m_data.poz.Count - 1; // 4
                yield return m_data.poz.Count - 1; // 4
                yield return m_data.poz.Count - 3; // 2
                yield return m_data.poz.Count - 4; // 1
            }
        }
        if (halgin == HorizontalAlgin.Rigth)
        {
            offset -= f.Spaceing.Horizontal;
            foreach (var kvp in mesh_parts)
            {
                var pozs = kvp.Value.poz;
                for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                {
                    var p = pozs[pozs.Count - i - 1];
                    p.X -= offset;
                    pozs[pozs.Count - i - 1] = p;
                }
            }
        }
        else if(halgin == HorizontalAlgin.Center)
        {
            offset -= f.Spaceing.Horizontal;
            offset /= 2;
            foreach (var kvp in mesh_parts)
            {
                var pozs = kvp.Value.poz;
                for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                {
                    var p = pozs[pozs.Count - i - 1];
                    p.X -= offset;
                    pozs[pozs.Count - i - 1] = p;
                }
            }
        }
        if(valgin == VerticalAlgin.Down)
        {
            var ver = (line * -(1 + f.Spaceing.Vertical)) + f.Spaceing.Vertical;
            foreach (var kvp in mesh_parts)
            {
                var pozs = kvp.Value.poz;
                for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                {
                    var p = pozs[pozs.Count - i - 1];
                    p.Y -= ver;
                    pozs[pozs.Count - i - 1] = p;
                }
            }
        }
        else if (valgin == VerticalAlgin.Center)
        {
            var ver = ((line * -(1 + f.Spaceing.Vertical)) + f.Spaceing.Vertical) / 2;
            foreach (var kvp in mesh_parts)
            {
                var pozs = kvp.Value.poz;
                for (var i = 0; i < kvp.Value.new_letters * 4; i++)
                {
                    var p = pozs[pozs.Count - i - 1];
                    p.Y -= ver;
                    pozs[pozs.Count - i - 1] = p;
                }
            }
        }

        for (int i = 0; i < meshes.Length; i++)
        { meshes[i]?.Dispose(); meshes[i] = null; }
            
        foreach(var m in mesh_parts)
        {
            meshes[m.Key] = Mesh.Create(shader)
                .SetVertex("s_offset", m.Value.offset.ToArray())
                .SetVertex("s_size", m.Value.size.ToArray())
                .SetTrangles(m.Value.triangles.ToArray())
                .SetVertex("poz", m.Value.poz.ToArray())
                .SetVertex("uv", m.Value.uv.ToArray())
                .Finish();
        }
    }
    protected internal override void Bind(SpacePoint point) => recreate_mesh(true);
    public override void Draw(Matrix4 projection)
    {
        Matrix4 matrix = Matrix4.CreateScale(size) * Matrix4.CreateTranslation(halgin switch
        {
            HorizontalAlgin.Left => -Point!.Size.Width / 2,
            HorizontalAlgin.Rigth => Point!.Size.Width / 2,
            _ => 0
        }, valgin switch
        {
            VerticalAlgin.Down => -Point!.Size.Width / 2,
            VerticalAlgin.Up => Point!.Size.Width / 2,
            _ => 0
        }, 0) * projection;
        var f = GetFont();
        for (int i = 0; i < meshes.Length; i++)
        {
            var m = meshes[i];
            shader.SetUniform("color", (Vector4)color);
            shader.SetUniform("tex", f.Textures[i].texture);
            shader.SetUniform("static_heigth", (float)f.Textures[i].heigth);
            shader.SetUniform("tex_size", f.Textures[i].texture.Size.ToVector().ToVector2());
            m?.Draw(matrix);
        }
    }
}

public enum HorizontalAlgin
{
    Left, Center, Rigth
}
public enum VerticalAlgin
{
    Up, Center, Down
}