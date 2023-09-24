using Create.Elements;
using Create.Linq;
using Create.Render.ModelCreators.Model;
using OpenTK.Mathematics;
using System.Drawing;
using System.Xml.Linq;

namespace Create.Render.ModelCreators.Side;

public struct ColoredTextureSide : IBlockSideModel
{
    private int texture_side;
    private Color4 color;

    public bool Equals(IBlockSideModel? other)
    {
        if (!(other is ColoredTextureSide cts))
            return false;

        if (texture_side != cts.texture_side) return false;
        if (color != cts.color) return false;
        return true;
    }
    public void RenderSide(ModelConstructor constructor, IEnumerable<Vector3> pozitions, IEnumerable<Vector2> uvs, IEnumerable<int> trangles)
    {
        var coll = constructor.GetModelMekanizm<ColoredTextureModel>();
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
            coll.colors.Add(color);
            coll.ints.Add(texture_side);
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
        var coll = constructor.GetModelMekanizm<ColoredTextureModel>();
        if (uvs.Length != pozitions.Length)
            throw new Exception("Data sizes are not match");

        for (int i = 0; i < trangles.Length; i++)
            coll.trangles.Add(trangles[i] + coll.uvs.Count);
        coll.uvs.AddRange(uvs);
        coll.pozitions.AddRange(pozitions);

        for (int i = pozitions.Length; i > 0; --i)
            coll.ints.Add(texture_side);
        for (int i = pozitions.Length; i > 0; --i)
            coll.colors.Add(color);
    }

    internal static IBlockSideModel Interpreter(XElement element)
    {
        try
        {
            var h = Assets.BlockAtlas.Handles[element.Element("texture")?.Value?.Trim() ?? string.Empty];
            var uid = int.TryParse(element.Element("uid")?.Value?.Trim(), out var u) ? u : h.Handle;
            var color = ColorTranslator.FromHtml(element.Element("color")?.Value?.Trim() ?? "#fff");
            return new ColoredTextureSide { texture_side = uid, color = color };
        }
        catch
        { return new ColoredTextureSide { texture_side = BlockTextureHandle.None.Handle, color = Color4.White }; }
    }
}
