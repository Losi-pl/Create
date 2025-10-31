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
                    RenderHoryzontalSlabSide(BlockSide.Bottom, info.AsT0.Bottom, false);
                if(IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderHoryzontalSlabSide(BlockSide.North, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderHoryzontalSlabSide(BlockSide.East, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderHoryzontalSlabSide(BlockSide.South, info.AsT0.Bottom, false);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderHoryzontalSlabSide(BlockSide.West, info.AsT0.Bottom, false);
                if (info.AsT0.Top is null)
                    RenderHoryzontalSlabSide(BlockSide.Top, info.AsT0.Bottom, false);
            }
            if (info.AsT0.Top is not null)
            {
                if (info.AsT0.Bottom is null)
                    RenderHoryzontalSlabSide(BlockSide.Bottom, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderHoryzontalSlabSide(BlockSide.North, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderHoryzontalSlabSide(BlockSide.East, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderHoryzontalSlabSide(BlockSide.South, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderHoryzontalSlabSide(BlockSide.West, info.AsT0.Top, true);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    RenderHoryzontalSlabSide(BlockSide.Top, info.AsT0.Top, true);
            }
        }
        else
        {
            if (info.AsT1.Column1 is not null)
            {
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    RenderVerticalSlabSide(BlockSide.Bottom, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    RenderVerticalSlabSide(BlockSide.Top, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                    RenderVerticalSlabSide(BlockSide.West, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                    RenderVerticalSlabSide(BlockSide.South, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column2 is null)
                        RenderVerticalSlabSide(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                        RenderVerticalSlabSide(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column2 is null)
                        RenderVerticalSlabSide(BlockSide.East, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                        RenderVerticalSlabSide(BlockSide.North, info.AsT1.Column1, false, info.AsT1.IsAlongTheXAxis);
                }
            }
            if (info.AsT1.Column2 is not null)
            {
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Bottom))
                    RenderVerticalSlabSide(BlockSide.Bottom, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.Top))
                    RenderVerticalSlabSide(BlockSide.Top, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.East))
                    RenderVerticalSlabSide(BlockSide.East, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (IBlockModel.SideVisibilityTest(args, BlockSide.North))
                    RenderVerticalSlabSide(BlockSide.North, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                if (info.AsT1.IsAlongTheXAxis)
                {
                    if (info.AsT1.Column1 is null)
                        RenderVerticalSlabSide(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.West))
                        RenderVerticalSlabSide(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
                else
                {
                    if (info.AsT1.Column1 is null)
                        RenderVerticalSlabSide(BlockSide.West, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                    if (IBlockModel.SideVisibilityTest(args, BlockSide.South))
                        RenderVerticalSlabSide(BlockSide.South, info.AsT1.Column2, true, info.AsT1.IsAlongTheXAxis);
                }
            }
        }

        void RenderHoryzontalSlabSide(BlockSide side, Block block, bool upper)
        {
            Span<int> trangles = stackalloc int[6];
            IBlockModel.SetFastDefault(trangles);

            var tex = block.GetSideTexture(new()
            {
                block = args.block,
                pozition = args.pozition,
                world = args.world
            }, side);
            Span<Vector3> pos = stackalloc Vector3[4];
            Span<Vector2> uv = stackalloc Vector2[4];
            HorizontalSlabSidePositions(pos, side, upper);
            if (side == BlockSide.Top || side == BlockSide.Bottom)
                IBlockModel.SetFastDefault(uv);
            else
                HorizontalSlabSideUVs(uv, upper);
            tex?.RenderSide(constructor, pos, uv, trangles);
        }
        void RenderVerticalSlabSide(BlockSide side, Block block, bool upper, bool along_the_x)
        {
            Span<int> trangles = stackalloc int[6];
            IBlockModel.SetFastDefault(trangles);
            
            var tex = block.GetSideTexture(new()
            {
                block = args.block,
                pozition = args.pozition,
                world = args.world
            }, side);
            Span<Vector3> pos = stackalloc Vector3[4];
            Span<Vector2> uv = stackalloc Vector2[4];
            VerticalSlabSidePositions(pos, side, upper, along_the_x);
            if(along_the_x)
            {
                if (side is BlockSide.North or BlockSide.South)
                    IBlockModel.SetFastDefault(uv);
                else if(side is BlockSide.Top or BlockSide.Bottom)
                    HorizontalSlabSideUVs(uv, side is BlockSide.Top ? upper : !upper);
                else
                    VerticalSlabSideUVs(uv, side is BlockSide.East ? upper : !upper);
            }
            else
            {
                if (side is BlockSide.East or BlockSide.West)
                    IBlockModel.SetFastDefault(uv);
                else if (side is BlockSide.Top or BlockSide.Bottom)
                    VerticalSlabSideUVs(uv, side is BlockSide.Top ? upper : !upper);
                else
                    VerticalSlabSideUVs(uv, side is BlockSide.South ? upper : !upper);
            }
            tex?.RenderSide(constructor, pos, uv, trangles);
        }
        void HorizontalSlabSideUVs(Span<Vector2> uvs, bool upper)
        {
            IBlockModel.SetFastDefault(uvs);
            
            if (upper)
                IBlockModel.ScaleAndMoveVector(uvs, new Vector2(1, .5f), new Vector2(0, .5f));
            else
                IBlockModel.ScaleVectors(uvs, new Vector2(1, .5f));
        }
        void VerticalSlabSideUVs(Span<Vector2> uvs, bool upper)
        {
            IBlockModel.SetFastDefault(uvs);

            if (upper)
                IBlockModel.ScaleAndMoveVector(uvs, new Vector2(.5f, 1), new Vector2(.5f, 0));
            else
                IBlockModel.ScaleVectors(uvs, new Vector2(.5f, 1));
        }
        void HorizontalSlabSidePositions(Span<Vector3> positions, BlockSide side, bool upper)
        {
            Vector3 bl_pos = args.pozition.ToVector();
            IBlockModel.SetDefault(positions, side);
            if (upper)
                IBlockModel.ScaleAndMoveVector(positions, new Vector3(1, .5f, 1), bl_pos + new Vector3(0, .5f, 0));
            else
                IBlockModel.ScaleAndMoveVector(positions, new Vector3(1, .5f, 1), bl_pos);
        }
        void VerticalSlabSidePositions(Span<Vector3> positions, BlockSide side, bool upper, bool along_the_x)
        {
            IBlockModel.SetDefault(positions, side);

            Vector3 bl_pos = args.pozition.ToVector();
            if (along_the_x)
                IBlockModel.ScaleAndMoveVector(positions, new Vector3(1, 1, .5f), bl_pos);
            else
                IBlockModel.ScaleAndMoveVector(positions, new Vector3(.5f, 1, 1), bl_pos);

            if (upper)
                if(along_the_x)
                    IBlockModel.MoveVectors(positions, new Vector3(0, 0, .5f));
                else
                    IBlockModel.MoveVectors(positions, new Vector3(.5f, 0, 0));
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
