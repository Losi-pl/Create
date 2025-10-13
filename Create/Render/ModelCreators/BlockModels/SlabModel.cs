using Create.Conteiner;
using Create.Elements;
using Create.Elements.Bazic.Blocks;
using Create.Linq;
using OpenTK.Mathematics;
using System.Xml.Linq;
using static Create.Elements.Block;

namespace Create.Render.ModelCreators.BlockModels;

public sealed class SlabModel : IBlockModel
{
    IBlockSideModel texture;

    public SlabModel(IBlockSideModel Solid) => texture = Solid;

    public void GenerateModel(StandardBlockSet args, ModelConstructor constructor)
    {
        var info = SlabBase.InterpretPlacedBlock(args.block)!.Value;
        if(info.IsT0)
        {
            if (info.AsT0.Bottom is not null)
            {
                if (test_side(BlockSide.Bottom))
                    render_side(BlockSide.Bottom, info.AsT0.Bottom, false);
                if(test_side(BlockSide.North))
                    render_side(BlockSide.North, info.AsT0.Bottom, false);
                if (test_side(BlockSide.East))
                    render_side(BlockSide.East, info.AsT0.Bottom, false);
                if (test_side(BlockSide.South))
                    render_side(BlockSide.South, info.AsT0.Bottom, false);
                if (test_side(BlockSide.West))
                    render_side(BlockSide.West, info.AsT0.Bottom, false);
                if (info.AsT0.Top is null)
                    render_side(BlockSide.Top, info.AsT0.Bottom, false);
            }
            if (info.AsT0.Top is not null)
            {
                if (info.AsT0.Bottom is null)
                    render_side(BlockSide.Bottom, info.AsT0.Top, true);
                if (test_side(BlockSide.North))
                    render_side(BlockSide.North, info.AsT0.Top, true);
                if (test_side(BlockSide.East))
                    render_side(BlockSide.East, info.AsT0.Top, true);
                if (test_side(BlockSide.South))
                    render_side(BlockSide.South, info.AsT0.Top, true);
                if (test_side(BlockSide.West))
                    render_side(BlockSide.West, info.AsT0.Top, true);
                if (test_side(BlockSide.Top))
                    render_side(BlockSide.Top, info.AsT0.Top, true);
            }
        }

        void render_side(BlockSide side, Block block, bool upper)
        {
            Span<int> trangles = stackalloc int[]
            {
                0, 1, 2,
                3, 2, 1
            };
            var tex = block.GetSideTexture(new()
            {
                block = args.block,
                pozition = args.pozition,
                world = args.world
            }, side);
            Span<Vector3> poz = stackalloc Vector3[4];
            Span<Vector2> uv = stackalloc Vector2[4];
            get_face_pozitions(poz, side, upper);
            if (side == BlockSide.Top || side == BlockSide.Bottom)
                get_face_uvs(uv);
            else
                get_face_uvs_s(uv, upper);
            tex?.RenderSide(constructor, poz, uv, trangles);
        }
        void get_face_uvs(Span<Vector2> uvs)
        {
            uvs[0] = new Vector2(0, 1);
            uvs[1] = new Vector2(1, 1);
            uvs[2] = new Vector2(0, 0);
            uvs[3] = new Vector2(1, 0);
        }
        void get_face_uvs_s(Span<Vector2> uvs, bool upper)
        {
            uvs[0] = new Vector2(0, .5f);
            uvs[1] = new Vector2(1, .5f);
            uvs[2] = new Vector2(0, 0);
            uvs[3] = new Vector2(1, 0);
            if (upper)
                for (int i = 0; i < 4; i++)
                    uvs[i] = uvs[i] + new Vector2(0, .5f);
        }
        void get_face_pozitions(Span<Vector3> pozitions, BlockSide side, bool upper)
        {
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
                    pozitions[i] = pozitions[i] + new Vector3(0, .5f, 0);
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
            return new SlabModel(interpret(main));

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
