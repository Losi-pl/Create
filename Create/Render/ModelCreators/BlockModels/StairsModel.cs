using Create.Conteiner;
using Create.Elements.Bazic.Blocks;
using Create.Linq;
using OpenTK.Mathematics;
using System.Xml.Linq;
using BlockSide = Create.Elements.Block.BlockSide;
using StandardBlockSet = Create.Elements.Block.StandardBlockSet;

namespace Create.Render.ModelCreators.BlockModels;

public class StairsModel : IBlockModel
{
    IBlockSideModel texture;

    public StairsModel(IBlockSideModel Solid) => texture = Solid;
    public void GenerateModel(StandardBlockSet args, ModelConstructor constructor)
    {
        if (!StairsBase.InterpretPlacedBlock(args).IsNotNull(out var info))
            return;

        add_side_b(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper);
        if (IBlockModel.SideVisibilityTest(args, info.isUpper ? BlockSide.Top : BlockSide.Bottom))
            add_side_b(info.isUpper ? BlockSide.Top : BlockSide.Bottom, info.isUpper);
        if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
            add_side_b(BlockSide.East, info.isUpper);
        if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
            add_side_b(BlockSide.West, info.isUpper);
        if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
            add_side_b(BlockSide.North, info.isUpper);
        if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
            add_side_b(BlockSide.South, info.isUpper);

        if (info.StepPrezs.NW)
        {
            if (IBlockModel.SideVisibilityTest(args, info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, false, true);
            if (!info.StepPrezs.NE)
                add_side_s(BlockSide.East, info.isUpper, false, true);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                add_side_s(BlockSide.West, info.isUpper, false, true);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                add_side_s(BlockSide.North, info.isUpper, false, true);
            if (!info.StepPrezs.SW)
                add_side_s(BlockSide.South, info.isUpper, false, true);
        }
        if (info.StepPrezs.NE)
        {
            if (IBlockModel.SideVisibilityTest(args, info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, true, true);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                add_side_s(BlockSide.East, info.isUpper, true, true);
            if (!info.StepPrezs.NW)
                add_side_s(BlockSide.West, info.isUpper, true, true);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                add_side_s(BlockSide.North, info.isUpper, true, true);
            if (!info.StepPrezs.SE)
                add_side_s(BlockSide.South, info.isUpper, true, true);
        }
        if (info.StepPrezs.SW)
        {
            if (IBlockModel.SideVisibilityTest(args, info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, false, false);
            if (!info.StepPrezs.SE)
                add_side_s(BlockSide.East, info.isUpper, false, false);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                add_side_s(BlockSide.West, info.isUpper, false, false);
            if (!info.StepPrezs.NW)
                add_side_s(BlockSide.North, info.isUpper, false, false);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                add_side_s(BlockSide.South, info.isUpper, false, false);
        }
        if (info.StepPrezs.SE)
        {
            if (IBlockModel.SideVisibilityTest(args, info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, true, false);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                add_side_s(BlockSide.East, info.isUpper, true, false);
            if (!info.StepPrezs.SW)
                add_side_s(BlockSide.West, info.isUpper, true, false);
            if (!info.StepPrezs.NE)
                add_side_s(BlockSide.North, info.isUpper, true, false);
            if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                add_side_s(BlockSide.South, info.isUpper, true, false);
        }

        void add_side_s(BlockSide side, bool isUpper, bool move_byX, bool move_byZ)
        {
            Span<Vector2> uvs = stackalloc Vector2[4];
            IBlockModel.SetFastDefault(uvs);
            IBlockModel.ScaleVectors(uvs, .5f);

            if (side is BlockSide.Top)
                IBlockModel.MoveVectors(uvs, new(move_byX ? .5f : 0, move_byZ ? .5f : 0));
            else if (side is BlockSide.Bottom)
                IBlockModel.MoveVectors(uvs, new(move_byX ? .5f : 0, move_byZ ? 0 : .5f));
            else
            {
                IBlockModel.MoveVectors(uvs, new(0, .5f));

                if (side switch { BlockSide.North => !move_byX, BlockSide.South => move_byX, BlockSide.West => !move_byZ,  BlockSide.East => move_byZ, _ => false})
                    IBlockModel.MoveVectors(uvs, new(.5f, 0));
            }

            Span<int> trangles = stackalloc int[4];
            IBlockModel.SetFastDefault(trangles);

            Span<Vector3> pozitions = stackalloc Vector3[4];
            IBlockModel.SetDefault(pozitions, side);
            
            Vector3 bl_poz = args.pozition.ToVector();
            IBlockModel.ScaleAndMoveVector(pozitions, .5f, bl_poz);
            IBlockModel.MoveVectors(pozitions, new Vector3(y: isUpper ? 0 : .5f, x: move_byX ? .5f : 0, z: move_byZ ? .5f : 0));

            texture.RenderSide(constructor, pozitions, uvs, trangles);
        }
        void add_side_b(BlockSide side, bool upper)
        {
            Span<Vector2> uvs = stackalloc Vector2[4];
            IBlockModel.SetFastDefault(uvs);
            if (side is not BlockSide.Top or BlockSide.Bottom)
            {
                IBlockModel.ScaleVectors(uvs, new Vector2(1, 0.5f));
                if (upper)
                    IBlockModel.MoveVectors(uvs, new Vector2(0, 0.5f));
            }
            
            Span<int> trangles = stackalloc int[4];
            IBlockModel.SetFastDefault(trangles);

            Span<Vector3> pozitions = stackalloc Vector3[4];
            IBlockModel.SetDefault(pozitions, side);

            Vector3 bl_poz = args.pozition.ToVector();
            IBlockModel.ScaleAndMoveVector(pozitions, new Vector3(1, 0.5f, 1), bl_poz);

            if (upper)
                IBlockModel.MoveVectors(pozitions, new Vector3(0, 0.5f, 0));

            texture.RenderSide(constructor, pozitions, uvs, trangles);
        }
    }

    public IBlockSideModel? GetBlockSide(StandardBlockSet sideSet, BlockSide side) => texture;

    internal static IBlockModel Interpreter(XElement element)
    {
        var main = element.Element("main");
        if (main is not null)
            return new StairsModel(interpret(main));

        throw new("Not all parameters set");

        IBlockSideModel interpret(XElement el)
        {
            var attr = el.Attribute("type");
            if (attr is null)
                throw new("Attribute \"type\" is not defined");
            var doubleDots = attr.Value.IndexOf(':');
            var modSource = find_mod(doubleDots == -1 ? "create" : element.GetNamespaceOfPrefix(attr.Value.Remove(doubleDots))!.NamespaceName);
            var converter = doubleDots == -1 ? attr.Value : attr.Value.Substring(doubleDots + 1);
            return IBlockSideModel.interpreters[(modSource!, converter!)].Invoke(el);
        }
        Mod? find_mod(string name)
        {
            var l = Mod.All;
            for (int i = 0; i < l.Length; i++)
                if (l[i].Name == name)
                    return l[i];
            return null;
        }
    }
}
