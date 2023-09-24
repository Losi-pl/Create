using OpenTK.Mathematics;
using System.Xml.Linq;

namespace Create.Render;

public interface IBlockSideModel : IEquatable<IBlockSideModel>
{
    public void RenderSide(ModelConstructor constructor, IEnumerable<Vector3> pozitions, IEnumerable<Vector2> uvs, IEnumerable<int> trangles);
    public void RenderSide(ModelConstructor constructor, Span<Vector3> pozitions, Span<Vector2> uvs, Span<int> trangles);

    internal static Dictionary<(Mod mod, string name), Func<XElement, IBlockSideModel>> interpreters = new();

    internal static void Load(Mod mod)
    {
        mod.BlockSideSystem("single", ModelCreators.Side.SingleTextureSide.Interpreter);
        mod.BlockSideSystem("single-colored", ModelCreators.Side.ColoredTextureSide.Interpreter);
        mod.BlockSideSystem("multiple-colored", ModelCreators.Side.MultiTextureColorSide.Interpreter);
    }
}
