using Create.Linq;
using Create.Render.ModelCreators.Model;
using OpenTK.Mathematics;
using System.Drawing;
using System.Xml.Linq;

namespace Create.Render.ModelCreators.Side;

public struct MultiTextureColorSide : IBlockSideModel
{
    private int texture_bottom, texture_top;
    private Color4 top_color;

    public bool Equals(IBlockSideModel? other)
    {
        if (!(other is MultiTextureColorSide sid))
            return false;
        return sid.texture_bottom == texture_bottom && sid.texture_top == texture_top && sid.top_color == top_color;
    }

    public void RenderSide(ModelConstructor constructor, IEnumerable<Vector3> pozitions, IEnumerable<Vector2> uvs, IEnumerable<int> trangles)
    {
        var coll = constructor.GetModelMekanizm<MultiTextureColorModel>();
        var poz = pozitions.GetEnumerator();
        var uvs_ = uvs.GetEnumerator();
        var iniCount = coll.pozitions.Count;

        int newPoint = 0;
        bool p = poz.MoveNext(), u = uvs_.MoveNext();

        while (p && u)
        {
            newPoint++;
            coll.pozitions.Add(poz.Current);
            coll.uvs.Add(uvs_.Current);
            coll.tex_top.Add(texture_top);
            coll.tex_bottom.Add(texture_bottom);
            coll.color.Add(top_color);
        }
        if (!p || !u)
            throw new("Amount of pozition's and uv's is not match");
        var newCount = coll.pozitions.Count;
        var diff = newCount - iniCount;

        foreach (var i in trangles)
            coll.trangles.Add(iniCount + i);
    }

    public void RenderSide(ModelConstructor constructor, Span<Vector3> pozitions, Span<Vector2> uvs, Span<int> trangles)
    {
        var conn = constructor.GetModelMekanizm<MultiTextureColorModel>();
        
        if (uvs.Length != pozitions.Length)
            throw new Exception("Data sizes are not match");

        for (int i = 0; i < trangles.Length; ++i)
            conn.trangles.Add(trangles[i] + conn.uvs.Count);

        conn.uvs.AddRange(uvs);
        conn.pozitions.AddRange(pozitions);

        for (int i = pozitions.Length; i > 0; --i)
            conn.tex_top.Add(texture_top);
        for (int i = pozitions.Length; i > 0; --i)
            conn.tex_bottom.Add(texture_bottom);
        for (int i = pozitions.Length; i > 0; --i)
            conn.color.Add(top_color);
    }

    internal static IBlockSideModel Interpreter(XElement element)
    {
        try
        {
            var t1 = element.Element("background");
            int uid_b;
            {
                var h1 = Assets.BlockAtlas.Handles[t1.Element("texture")?.Value?.Trim() ?? string.Empty];
                uid_b = int.TryParse(t1.Element("uid")?.Value?.Trim(), out var u) ? u : h1.Handle;
            }
            var t2 = element.Element("top");
            int uid_t;
            {
                var h2 = Assets.BlockAtlas.Handles[t2.Element("texture")?.Value?.Trim() ?? string.Empty];
                uid_t = int.TryParse(t1.Element("uid")?.Value?.Trim(), out var u) ? u : h2.Handle;
            }
            var color = ColorTranslator.FromHtml(element.Element("color")?.Value?.Trim() ?? "#fff");
            return new MultiTextureColorSide { texture_bottom = uid_b, texture_top = uid_t, top_color = color };
        }
        catch
        { return new MultiTextureColorSide { texture_bottom = BlockTextureHandle.None.Handle, texture_top = BlockTextureHandle.None.Handle, top_color = Color4.White }; }
    }
}
