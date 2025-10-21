using Create.Conteiner;
using Create.Elements;
using Create.Linq;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;
using System.Xml.Linq;
using BlockSide = Create.Elements.Block.BlockSide;
using StandardBlockSet = Create.Elements.Block.StandardBlockSet;

namespace Create.Render.ModelCreators.BlockModels;

public sealed class RotatableBlock : IBlockModel
{
    #nullable disable
    IBlockSideModel _sides, _ends;
    #nullable restore

    public void GenerateModel(Block.StandardBlockSet args, ModelConstructor constructor)
    {
        switch (args.block.Meta)
        {
            case "0":
            default:
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    add_side(BlockSide.Top, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    add_side(BlockSide.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    add_side(BlockSide.East, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    add_side(BlockSide.West, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    add_side(BlockSide.North, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    add_side(BlockSide.South, false);
                break;
            case "1":
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    add_side(BlockSide.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    add_side(BlockSide.Bottom, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    add_side(BlockSide.East, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    add_side(BlockSide.West, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    add_side(BlockSide.North, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    add_side(BlockSide.South, true);
                break;
            case "2":
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    add_side(BlockSide.Top, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    add_side(BlockSide.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    add_side(BlockSide.East, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    add_side(BlockSide.West, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    add_side(BlockSide.North, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    add_side(BlockSide.South, false);
                break;
        }
        
        void add_side(BlockSide side, bool rotated)
        {
            Span<Vector2> uvs = stackalloc Vector2[4];
            if(rotated)
            {
                uvs[0] = new Vector2(0, 0);
                uvs[1] = new Vector2(0, 1);
                uvs[2] = new Vector2(1, 0);
                uvs[3] = new Vector2(1, 1);
            }
            else
            {
                uvs[0] = new Vector2(0, 1);
                uvs[1] = new Vector2(1, 1);
                uvs[2] = new Vector2(0, 0);
                uvs[3] = new Vector2(1, 0);
            }

            Span<int> trangles = stackalloc int[6];
            IBlockModel.SetFastDefault(trangles);

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
            var sideT = GetBlockSide(args, side);
            sideT?.RenderSide(constructor, pozitions, uvs, trangles);
        }
    }

    public IBlockSideModel? GetBlockSide(Block.StandardBlockSet sideSet, BlockSide side) =>
        sideSet.block.Meta switch
        {
            "1" => side switch
            {
                BlockSide.East => _ends,
                BlockSide.West => _ends,
                _ => _sides
            },
            "2" => side switch
            {
                BlockSide.North => _ends,
                BlockSide.South => _ends,
                _ => _sides
            },
            _ => side switch
            {
                BlockSide.Top => _ends,
                BlockSide.Bottom => _ends,
                _ => _sides
            }
        };

    internal static IBlockModel Interpreter(XElement element)
    {
        var side = element.Element("side");
        var ends = element.Element("ends");
        if (side is not null && ends is not null)
            return new RotatableBlock() { _sides = interpret(side), _ends = interpret(ends) };

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
