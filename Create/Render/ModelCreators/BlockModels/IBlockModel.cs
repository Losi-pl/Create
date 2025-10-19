using System.Xml.Linq;
using static Create.Elements.Block;

namespace Create.Render.ModelCreators.BlockModels;

public interface IBlockModel
{
    public void GenerateModel(StandardBlockSet args, ModelConstructor constructor);

    public IBlockSideModel? GetBlockSide(StandardBlockSet sideSet, BlockSide side);

    internal static Dictionary<(Mod mod, string name), Func<XElement, IBlockModel>> interpreters = new();

    internal static void Load(Mod mod)
    {
        mod.BlockModelSystem("bazic", SolidBlock.Interpreter);
        mod.BlockModelSystem("rotatable", RotatableBlock.Interpreter);
        mod.BlockModelSystem("slab", SlabModel.Interpreter);
        mod.BlockModelSystem("stairs", StairsModel.Interpreter);
    }
}
