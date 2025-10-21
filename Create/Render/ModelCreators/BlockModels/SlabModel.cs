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
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    render_side(BlockSide.Bottom, info.AsT0.Bottom, false);
                if(IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    render_side(BlockSide.North, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    render_side(BlockSide.East, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    render_side(BlockSide.South, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    render_side(BlockSide.West, info.AsT0.Bottom, false);
                if (info.AsT0.Top is null)
                    render_side(BlockSide.Top, info.AsT0.Bottom, false);
            }
            if (info.AsT0.Top is not null)
            {
                if (info.AsT0.Bottom is null)
                    render_side(BlockSide.Bottom, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    render_side(BlockSide.North, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    render_side(BlockSide.East, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    render_side(BlockSide.South, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    render_side(BlockSide.West, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    render_side(BlockSide.Top, info.AsT0.Top, true);
            }
        }
        else
        {
            if (info.AsT1.Column1 is not null)
            {
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    render_side_v(BlockSide.Bottom, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    render_side_v(BlockSide.Top, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    render_side_v(BlockSide.West, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    render_side_v(BlockSide.South, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column2 is null)
                        render_side_v(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                        render_side_v(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column2 is null)
                        render_side_v(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                        render_side_v(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
            }
            if (info.AsT1.Column2 is not null)
            {
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    render_side_v(BlockSide.Bottom, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    render_side_v(BlockSide.Top, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    render_side_v(BlockSide.East, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    render_side_v(BlockSide.North, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column1 is null)
                        render_side_v(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                        render_side_v(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column1 is null)
                        render_side_v(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                        render_side_v(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
            }
        }

        void render_side(BlockSide side, Block block, bool upper)
        {
            Span<int> trangles = stackalloc int[6];
            IBlockModel.SetFastDefault(trangles);

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
                IBlockModel.SetFastDefault(uv);
            else
                get_face_uvs_s(uv, upper);
            tex?.RenderSide(constructor, poz, uv, trangles);
        }
        void render_side_v(BlockSide side, Block block, bool upper, bool along_the_x)
        {
            Span<int> trangles = stackalloc int[6];
            IBlockModel.SetFastDefault(trangles);
            
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
                    IBlockModel.SetFastDefault(uv);
                else if(side is BlockSide.Top or BlockSide.Bottom)
                    get_face_uvs_s(uv, side is BlockSide.Top ? upper : !upper);
                else
                    get_face_uvs_s_v(uv, side is BlockSide.East ? upper : !upper);
            }
            else
            {
                if (side is BlockSide.East or BlockSide.West)
                    IBlockModel.SetFastDefault(uv);
                else if (side is BlockSide.Top or BlockSide.Bottom)
                    get_face_uvs_s_v(uv, side is BlockSide.Top ? upper : !upper);
                else
                    get_face_uvs_s_v(uv, side is BlockSide.South ? upper : !upper);
            }
            tex?.RenderSide(constructor, poz, uv, trangles);
        }
        void get_face_uvs_s(Span<Vector2> uvs, bool upper)
        {
            IBlockModel.SetFastDefault(uvs);
            
            if (upper)
                IBlockModel.ScaleAndMoveVector(uvs, new Vector2(1, .5f), new Vector2(0, .5f));
            else
                IBlockModel.ScaleVectors(uvs, new Vector2(1, .5f));
        }
        void get_face_uvs_s_v(Span<Vector2> uvs, bool upper)
        {
            IBlockModel.SetFastDefault(uvs);

            if (upper)
                IBlockModel.ScaleAndMoveVector(uvs, new Vector2(.5f, 1), new Vector2(.5f, 0));
            else
                IBlockModel.ScaleVectors(uvs, new Vector2(.5f, 1));
        }
        void get_face_pozitions(Span<Vector3> pozitions, BlockSide side, bool upper)
        {
            Vector3 bl_poz = args.pozition.ToVector();
            IBlockModel.SetDefault(pozitions, side);
            if (upper)
                IBlockModel.ScaleAndMoveVector(pozitions, new Vector3(1, .5f, 1), bl_poz + new Vector3(0, .5f, 0));
            else
                IBlockModel.ScaleAndMoveVector(pozitions, new Vector3(1, .5f, 1), bl_poz);
        }
        void get_face_pozitions_v(Span<Vector3> pozitions, BlockSide side, bool upper, bool along_the_x)
        {
            Span<Vector3> poz = stackalloc Vector3[4];
            IBlockModel.SetDefault(poz, side);

            Vector3 bl_poz = args.pozition.ToVector();
            if (along_the_x)
                IBlockModel.ScaleAndMoveVector(poz, new Vector3(1, 1, .5f), bl_poz);
            else
                IBlockModel.ScaleAndMoveVector(poz, new Vector3(.5f, 1, 1), bl_poz);

            if (upper)
                if(along_the_x)
                    IBlockModel.MoveVectors(poz, new Vector3(0, 0, .5f));
                else
                    IBlockModel.MoveVectors(poz, new Vector3(.5f, 0, 0));
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
