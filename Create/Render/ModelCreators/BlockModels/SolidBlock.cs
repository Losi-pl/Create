using Create.Conteiner;
using Create.Elements;
using Create.Linq;
using OneOf;
using OpenTK.Mathematics;
using System.Xml.Linq;
using BlockSide = Create.Elements.Block.BlockSide;
using StandardBlockSet = Create.Elements.Block.StandardBlockSet;

namespace Create.Render.ModelCreators.BlockModels;

public class SolidBlock : IBlockModel
{
    OneOf<(IBlockSideModel t, bool u), (IBlockSideModel Side, IBlockSideModel Ends), 
        (IBlockSideModel Top, IBlockSideModel Bottom, IBlockSideModel East, IBlockSideModel West, IBlockSideModel North, IBlockSideModel South)> textures;

    public SolidBlock(IBlockSideModel Solid) => textures = (Solid, false);
    public SolidBlock(IBlockSideModel Sides, IBlockSideModel Ends) => textures = (Sides, Ends);
    public SolidBlock(IBlockSideModel Top, IBlockSideModel Bottom, IBlockSideModel East, IBlockSideModel West, IBlockSideModel North, IBlockSideModel South) =>
        textures = (Top, Bottom, East, West, North, South);

    public void GenerateModel(StandardBlockSet args, ModelConstructor constructor)
    {
        if (test_side(BlockSide.Top))
            add_side(BlockSide.Top);
        if (test_side(BlockSide.Bottom))
            add_side(BlockSide.Bottom);
        if (test_side(BlockSide.East))
            add_side(BlockSide.East);
        if (test_side(BlockSide.West))
            add_side(BlockSide.West);
        if (test_side(BlockSide.North))
            add_side(BlockSide.North);
        if (test_side(BlockSide.South))
            add_side(BlockSide.South);

        void add_side(BlockSide side)
        {
            Span<Vector2> uvs = stackalloc Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
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
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(1, 0, 1),
                        bl_poz + new Vector3(0, 0, 1)
                    };
                    break;
                case BlockSide.South:
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(1, 1, 0),
                        bl_poz + new Vector3(0, 0, 0),
                        bl_poz + new Vector3(1, 0, 0)
                    };
                    break;
                case BlockSide.East:
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(1, 1, 0),
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(1, 0, 0),
                        bl_poz + new Vector3(1, 0, 1)
                    };
                    break;
                case BlockSide.West:
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(0, 0, 1),
                        bl_poz + new Vector3(0, 0, 0)
                    };
                    break;
                case BlockSide.Top:
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(1, 1, 0)
                    };
                    break;
                case BlockSide.Bottom:
                    pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 0, 0),
                        bl_poz + new Vector3(1, 0, 0),
                        bl_poz + new Vector3(0, 0, 1),
                        bl_poz + new Vector3(1, 0, 1)
                    };
                    break;
            }
            var sideT = textures.Match(s => s.t,
                k => side switch
                {
                    BlockSide.Top => k.Ends,
                    BlockSide.Bottom => k.Ends,
                    _ => k.Side
                },
                a => side switch
                {
                    BlockSide.Top => a.Top,
                    BlockSide.Bottom => a.Bottom,
                    BlockSide.East => a.East,
                    BlockSide.West => a.West,
                    BlockSide.North => a.North,
                    _ => a.South
                });
            sideT.RenderSide(constructor, pozitions, uvs, trangles);
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
                    block_set.pozition = (block_set.pozition.x, block_set.pozition.y + 1, block_set.pozition.z);
                    break;
                case BlockSide.Bottom:
                    block_set.pozition = (block_set.pozition.x, block_set.pozition.y - 1, block_set.pozition.z);
                    break;
                case BlockSide.West:
                    block_set.pozition = (block_set.pozition.x - 1, block_set.pozition.y, block_set.pozition.z);
                    break;
                case BlockSide.East:
                    block_set.pozition = (block_set.pozition.x + 1, block_set.pozition.y, block_set.pozition.z);
                    break;
                case BlockSide.North:
                    block_set.pozition = (block_set.pozition.x, block_set.pozition.y, block_set.pozition.z + 1);
                    break;
                case BlockSide.South:
                    block_set.pozition = (block_set.pozition.x, block_set.pozition.y, block_set.pozition.z - 1);
                    break;
            }
            PlacedBlock block = args.world.GetBlock(block_set.pozition);
            block_set.block = block;
            return block.Block.IsSideVisible(block_set, side.Invert());
        }
    }

    public IBlockSideModel? GetBlockSide(StandardBlockSet sideSet, BlockSide side) =>
        textures.Match(s => s.t,
            k => side switch
            {
                BlockSide.Top => k.Ends,
                BlockSide.Bottom => k.Ends,
                _ => k.Side
            },
            a => side switch
            {
                BlockSide.Top => a.Top,
                BlockSide.Bottom => a.Bottom,
                BlockSide.East => a.East,
                BlockSide.West => a.West,
                BlockSide.North => a.North,
                _ => a.South
            });

    internal static IBlockModel Interpreter(XElement element)
    {
        {
            var solid = element.Element("solid");
            if (solid is not null)
                return new SolidBlock(interpret(solid));

            var side = element.Element("side");
            var ends = element.Element("ends");
            if(side is not null && ends is not null)
                return new SolidBlock(interpret(side), interpret(ends));

            var top = element.Element("top");
            var bottom = element.Element("bottom");
            var east = element.Element("east");
            var west = element.Element("west");
            var north = element.Element("north");
            var south = element.Element("south");
            if ((top is not null && bottom is not null) &&
                (east is not null && west is not null) &&
                (north is not null && south is not null))
                return new SolidBlock(interpret(top), interpret(bottom), interpret(east), interpret(west), interpret(north), interpret(south));

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
        }

        return null!;

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
