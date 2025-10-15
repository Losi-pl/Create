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
        if (test_side(info.isUpper ? BlockSide.Top : BlockSide.Bottom))
            add_side_b(info.isUpper ? BlockSide.Top : BlockSide.Bottom, info.isUpper);
        if (test_side(BlockSide.East))
            add_side_b(BlockSide.East, info.isUpper);
        if (test_side(BlockSide.West))
            add_side_b(BlockSide.West, info.isUpper);
        if (test_side(BlockSide.North))
            add_side_b(BlockSide.North, info.isUpper);
        if (test_side(BlockSide.South))
            add_side_b(BlockSide.South, info.isUpper);

        if(info.StepPrezs.NW)
        {
            if (test_side(info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, false, true);
            if (!info.StepPrezs.NE)
                add_side_s(BlockSide.East, info.isUpper, false, true);
            if (test_side(BlockSide.West))
                add_side_s(BlockSide.West, info.isUpper, false, true);
            if (test_side(BlockSide.North))
                add_side_s(BlockSide.North, info.isUpper, false, true);
            if (!info.StepPrezs.SW)
                add_side_s(BlockSide.South, info.isUpper, false, true);
        }
        if (info.StepPrezs.NE)
        {
            if (test_side(info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, true, true);
            if (test_side(BlockSide.East))
                add_side_s(BlockSide.East, info.isUpper, true, true);
            if (!info.StepPrezs.NW)
                add_side_s(BlockSide.West, info.isUpper, true, true);
            if (test_side(BlockSide.North))
                add_side_s(BlockSide.North, info.isUpper, true, true);
            if (!info.StepPrezs.SW)
                add_side_s(BlockSide.South, info.isUpper, true, true);
        }
        if (info.StepPrezs.SW)
        {
            if (test_side(info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, false, false);
            if (!info.StepPrezs.SE)
                add_side_s(BlockSide.East, info.isUpper, false, false);
            if (test_side(BlockSide.West))
                add_side_s(BlockSide.West, info.isUpper, false, false);
            if (!info.StepPrezs.NW)
                add_side_s(BlockSide.North, info.isUpper, false, false);
            if (test_side(BlockSide.South))
                add_side_s(BlockSide.South, info.isUpper, false, false);
        }
        if (info.StepPrezs.SE)
        {
            if (test_side(info.isUpper ? BlockSide.Bottom : BlockSide.Top))
                add_side_s(info.isUpper ? BlockSide.Bottom : BlockSide.Top, info.isUpper, true, false);
            if (test_side(BlockSide.East))
                add_side_s(BlockSide.East, info.isUpper, true, false);
            if (!info.StepPrezs.SW)
                add_side_s(BlockSide.West, info.isUpper, true, false);
            if (!info.StepPrezs.NE)
                add_side_s(BlockSide.North, info.isUpper, true, false);
            if (test_side(BlockSide.South))
                add_side_s(BlockSide.South, info.isUpper, true, false);
        }

        void add_side_s(BlockSide side, bool isUpper, bool move_byX, bool move_byZ)
        {
            Span<Vector2> uvs = stackalloc[]
            {
                new Vector2(0, .5f),
                new Vector2(.5f, .5f),
                new Vector2(0, 0),
                new Vector2(.5f, 0)
            };
            if(side is BlockSide.Top)
            {
                for (int i = 0; i < 4; i++)
                    uvs[i] = new(
                        x: move_byX ? uvs[i].X + .5f : uvs[i].X,
                        y: move_byZ ? uvs[i].Y + .5f : uvs[i].Y);
            }
            else if (side is BlockSide.Bottom)
            {
                for (int i = 0; i < 4; i++)
                    uvs[i] = new(
                        x: move_byX ? uvs[i].X + .5f : uvs[i].X,
                        y: move_byZ ? uvs[i].Y : uvs[i].Y + .5f);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    uvs[i] = uvs[i] with { Y = uvs[i].Y + .5f };

                if ((side == BlockSide.South && move_byX) || 
                    (side == BlockSide.North && !move_byX) || 
                    (side == BlockSide.West && !move_byZ) || 
                    (side == BlockSide.East && move_byZ))
                    for (int i = 0; i < 4; i++)
                        uvs[i] = uvs[i] with { X = uvs[i].X + .5f };
            }

            Span<int> trangles = stackalloc[]
            {
                0, 1, 2,
                3, 2, 1
            };
            Span<Vector3> pozitions = stackalloc Vector3[4];
            Vector3 bl_poz = args.pozition.ToVector();
            switch (side)
            {
                case BlockSide.North:
                    pozitions[0] = bl_poz + new Vector3(.5f, .5f, .5f);
                    pozitions[1] = bl_poz + new Vector3(0, .5f, .5f);
                    pozitions[2] = bl_poz + new Vector3(.5f, 0, .5f);
                    pozitions[3] = bl_poz + new Vector3(0, 0, .5f);
                    break;
                case BlockSide.South:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[1] = bl_poz + new Vector3(.5f, .5f, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, 0);
                    pozitions[3] = bl_poz + new Vector3(.5f, 0, 0);
                    break;
                case BlockSide.East:
                    pozitions[0] = bl_poz + new Vector3(.5f, .5f, 0);
                    pozitions[1] = bl_poz + new Vector3(.5f, .5f, .5f);
                    pozitions[2] = bl_poz + new Vector3(.5f, 0, 0);
                    pozitions[3] = bl_poz + new Vector3(.5f, 0, .5f);
                    break;
                case BlockSide.West:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, .5f);
                    pozitions[1] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, .5f);
                    pozitions[3] = bl_poz + new Vector3(0, 0, 0);
                    break;
                case BlockSide.Top:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, .5f);
                    pozitions[1] = bl_poz + new Vector3(.5f, .5f, .5f);
                    pozitions[2] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[3] = bl_poz + new Vector3(.5f, .5f, 0);
                    break;
                case BlockSide.Bottom:
                    pozitions[0] = bl_poz + new Vector3(0, 0, 0);
                    pozitions[1] = bl_poz + new Vector3(.5f, 0, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, .5f);
                    pozitions[3] = bl_poz + new Vector3(.5f, 0, .5f);
                    break;
            }

            for (int i = 0; i < 4; i++)
                pozitions[i] = new(
                    y: isUpper ? pozitions[i].Y : pozitions[i].Y + .5f,
                    x: move_byX ? pozitions[i].X + .5f : pozitions[i].X,
                    z: move_byZ ? pozitions[i].Z + .5f : pozitions[i].Z);

            texture.RenderSide(constructor, pozitions, uvs, trangles);
        }
        void add_side_b(BlockSide side, bool upper)
        {
            Span<Vector2> uvs = side is BlockSide.Top or BlockSide.Bottom ? stackalloc Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            } :  (upper ? stackalloc Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, .5f),
                new Vector2(1, .5f)
            } : stackalloc Vector2[]
            {
                new Vector2(0, .5f),
                new Vector2(1, .5f),
                new Vector2(0, 0),
                new Vector2(1, 0)
            });
            Span<int> trangles = stackalloc int[]
            {
                0, 1, 2,
                3, 2, 1
            };
            Span<Vector3> pozitions = stackalloc Vector3[4];
            Vector3 bl_poz = args.pozition.ToVector();
            switch (side)
            {
                case BlockSide.North:
                    pozitions[0] = bl_poz + new Vector3(1, .5f, 1);
                    pozitions[1] = bl_poz + new Vector3(0, .5f, 1);
                    pozitions[2] = bl_poz + new Vector3(1, 0, 1);
                    pozitions[3] = bl_poz + new Vector3(0, 0, 1);
                    break;
                case BlockSide.South:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[1] = bl_poz + new Vector3(1, .5f, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, 0);
                    pozitions[3] = bl_poz + new Vector3(1, 0, 0);
                    break;
                case BlockSide.East:
                    pozitions[0] = bl_poz + new Vector3(1, .5f, 0);
                    pozitions[1] = bl_poz + new Vector3(1, .5f, 1);
                    pozitions[2] = bl_poz + new Vector3(1, 0, 0);
                    pozitions[3] = bl_poz + new Vector3(1, 0, 1);
                    break;
                case BlockSide.West:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, 1);
                    pozitions[1] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, 1);
                    pozitions[3] = bl_poz + new Vector3(0, 0, 0);
                    break;
                case BlockSide.Top:
                    pozitions[0] = bl_poz + new Vector3(0, .5f, 1);
                    pozitions[1] = bl_poz + new Vector3(1, .5f, 1);
                    pozitions[2] = bl_poz + new Vector3(0, .5f, 0);
                    pozitions[3] = bl_poz + new Vector3(1, .5f, 0);
                    break;
                case BlockSide.Bottom:
                    pozitions[0] = bl_poz + new Vector3(0, 0, 0);
                    pozitions[1] = bl_poz + new Vector3(1, 0, 0);
                    pozitions[2] = bl_poz + new Vector3(0, 0, 1);
                    pozitions[3] = bl_poz + new Vector3(1, 0, 1);
                    break;
            }

            if (upper)
                for (int i = 0; i < 4; i++)
                    pozitions[i] = pozitions[i] with { Y = pozitions[i].Y + .5f };

            texture.RenderSide(constructor, pozitions, uvs, trangles);
        }
        bool test_side(BlockSide side)
        {
            StandardBlockSet block_set = new()
            {
                pozition = args.pozition,
                world = args.world
            };

            switch (side)
            {
                case BlockSide.Top:
                    block_set.pozition.y++;
                    break;
                case BlockSide.Bottom:
                    block_set.pozition.y--;
                    break;
                case BlockSide.West:
                    block_set.pozition.x--;
                    break;
                case BlockSide.East:
                    block_set.pozition.x++;
                    break;
                case BlockSide.North:
                    block_set.pozition.z++;
                    break;
                case BlockSide.South:
                    block_set.pozition.z--;
                    break;
            }
            PlacedBlock block = args.world.GetBlock(block_set.pozition);
            block_set.block = block;
            return block.Block.IsSideVisible(block_set, side.Invert());
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
