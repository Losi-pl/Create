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
        else
        {
            if (info.AsT1.Column1 is not null)
            {
                if (test_side(BlockSide.Bottom))
                    render_side_v(BlockSide.Bottom, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.Top))
                    render_side_v(BlockSide.Top, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.West))
                    render_side_v(BlockSide.West, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.South))
                    render_side_v(BlockSide.South, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column2 is null)
                        render_side_v(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (test_side(BlockSide.East))
                        render_side_v(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column2 is null)
                        render_side_v(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (test_side(BlockSide.North))
                        render_side_v(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
            }
            if (info.AsT1.Column2 is not null)
            {
                if (test_side(BlockSide.Bottom))
                    render_side_v(BlockSide.Bottom, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.Top))
                    render_side_v(BlockSide.Top, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.East))
                    render_side_v(BlockSide.East, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (test_side(BlockSide.North))
                    render_side_v(BlockSide.North, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column1 is null)
                        render_side_v(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (test_side(BlockSide.West))
                        render_side_v(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column1 is null)
                        render_side_v(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (test_side(BlockSide.South))
                        render_side_v(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
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
        void render_side_v(BlockSide side, Block block, bool upper, bool along_the_x)
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
            get_face_pozitions_v(poz, side, upper, along_the_x);
            if(along_the_x)
            {
                if (side is BlockSide.North or BlockSide.South)
                    get_face_uvs(uv);
                else if(side is BlockSide.Top or BlockSide.Bottom)
                    get_face_uvs_s(uv, side is BlockSide.Top ? upper : !upper);
                else
                    get_face_uvs_s_v(uv, side is BlockSide.East ? upper : !upper);
            }
            else
            {
                if (side is BlockSide.East or BlockSide.West)
                    get_face_uvs(uv);
                else if (side is BlockSide.Top or BlockSide.Bottom)
                    get_face_uvs_s_v(uv, side is BlockSide.Top ? upper : !upper);
                else
                    get_face_uvs_s_v(uv, side is BlockSide.South ? upper : !upper);
            }
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
        void get_face_uvs_s_v(Span<Vector2> uvs, bool upper)
        {
            uvs[0] = new Vector2(0, 1);
            uvs[1] = new Vector2(.5f, 1);
            uvs[2] = new Vector2(0, 0);
            uvs[3] = new Vector2(.5f, 0);
            if (upper)
                for (int i = 0; i < 4; i++)
                    uvs[i] += new Vector2(.5f, 0);
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
        void get_face_pozitions_v(Span<Vector3> pozitions, BlockSide side, bool upper, bool along_the_x)
        {
            Span<Vector3> poz = stackalloc Vector3[4];
            Vector3 bl_poz = args.pozition.ToVector();
            switch (side)
            {
                case BlockSide.North:
                    poz[0] = new Vector3(1, 1, 1);
                    poz[1] = new Vector3(0, 1, 1);
                    poz[2] = new Vector3(1, 0, 1);
                    poz[3] = new Vector3(0, 0, 1);
                    break;
                case BlockSide.South:
                    poz[0] = new Vector3(0, 1, 0);
                    poz[1] = new Vector3(1, 1, 0);
                    poz[2] = new Vector3(0, 0, 0);
                    poz[3] = new Vector3(1, 0, 0);
                    break;
                case BlockSide.East:
                    poz[0] = new Vector3(1, 1, 0);
                    poz[1] = new Vector3(1, 1, 1);
                    poz[2] = new Vector3(1, 0, 0);
                    poz[3] = new Vector3(1, 0, 1);
                    break;
                case BlockSide.West:
                    poz[0] = new Vector3(0, 1, 1);
                    poz[1] = new Vector3(0, 1, 0);
                    poz[2] = new Vector3(0, 0, 1);
                    poz[3] = new Vector3(0, 0, 0);
                    break;
                case BlockSide.Top:
                    poz[0] = new Vector3(0, 1, 1);
                    poz[1] = new Vector3(1, 1, 1);
                    poz[2] = new Vector3(0, 1, 0);
                    poz[3] = new Vector3(1, 1, 0);
                    break;
                case BlockSide.Bottom:
                    poz[0] = new Vector3(0, 0, 0);
                    poz[1] = new Vector3(1, 0, 0);
                    poz[2] = new Vector3(0, 0, 1);
                    poz[3] = new Vector3(1, 0, 1);
                    break;
            }

            if (along_the_x)
                for (int i = 0; i < 4; i++)
                    pozitions[i] = bl_poz + (poz[i] * new Vector3(1, 1, .5f));
            else
                for (int i = 0; i < 4; i++)
                    pozitions[i] = bl_poz + (poz[i] * new Vector3(.5f, 1, 1));

            if (upper)
                if(along_the_x)
                    for (int i = 0; i < 4; i++)
                        pozitions[i] = pozitions[i] + new Vector3(0, 0, .5f);
                else
                    for (int i = 0; i < 4; i++)
                        pozitions[i] = pozitions[i] + new Vector3(.5f, 0, 0);
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
