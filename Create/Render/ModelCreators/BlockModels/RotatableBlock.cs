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
                    RenderBlockSide(BlockSide.Top, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    RenderBlockSide(BlockSide.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderBlockSide(BlockSide.East, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderBlockSide(BlockSide.West, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderBlockSide(BlockSide.North, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderBlockSide(BlockSide.South, false);
                break;
            case "1":
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    RenderBlockSide(BlockSide.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    RenderBlockSide(BlockSide.Bottom, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderBlockSide(BlockSide.East, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderBlockSide(BlockSide.West, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderBlockSide(BlockSide.North, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderBlockSide(BlockSide.South, true);
                break;
            case "2":
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    RenderBlockSide(BlockSide.Top, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    RenderBlockSide(BlockSide.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderBlockSide(BlockSide.East, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderBlockSide(BlockSide.West, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderBlockSide(BlockSide.North, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderBlockSide(BlockSide.South, false);
                break;
        }
        
        void RenderBlockSide(BlockSide side, bool rotated)
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

            Span<Vector3> positions = stackalloc Vector3[4];
            IBlockModel.SetDefault(positions, side);
            IBlockModel.MoveVectors(positions, args.pozition.ToVector());
            
            var sideT = GetBlockSide(args, side);
            sideT?.RenderSide(constructor, positions, uvs, trangles);
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
