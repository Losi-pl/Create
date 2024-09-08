using Create.Conteiner;
using Create.Space;
using OneOf;
using SlabInfo = OneOf.OneOf<(Create.Elements.Block? Bottom, Create.Elements.Block? Top), 
    (Create.Elements.Block? Column1, Create.Elements.Block? Column2, Create.Elements.Block.BlockSide Direction)>;

namespace Create.Elements.Bazic.Blocks;

public abstract class SlabBase : Block
{
    public static SlabInfo? InterpretPlacedBlock(PlacedBlock placedBlock)
    {
        if (placedBlock.Block is not SlabBase slab)
            return null;
        if(string.IsNullOrEmpty(placedBlock.Meta))
            return (placedBlock.Block, null);
        if (placedBlock.Meta[0] == '+')
            return (null, placedBlock.Block);
        else if (placedBlock.Meta[0] == '|')
            return (placedBlock.Block, null, BlockSide.North);
        else
            return (placedBlock.Block, Register.Blocks.ByName[placedBlock.Meta]);
    }
    
    public override sealed bool IsSideVisible(StandardBlockSet sideSet, BlockSide side)
    {
        var info = InterpretPlacedBlock(sideSet.block);
        if (!info.HasValue)
            return true;
        if (info.Value.Index == 0)
        {
            switch (side)
            {
                case BlockSide.Bottom:
                    return info.Value.AsT0.Bottom is null;
                case BlockSide.Top:
                    return info.Value.AsT0.Top is null;
                default:
                    return !(info.Value.AsT0.Top is not null && info.Value.AsT0.Bottom is not null);
            }
        }
        else if (info.Value.Index == 1)
            //TODO - Cheacking for visibility in vertical slabs
            return true;
        
        return base.IsSideVisible(sideSet, side);
    }

    public override sealed bool OnPlaceBlock(PlaceBlock args)
    {
        {
            var c_block = args.World.GetBlock(args.TargetedBlockPozition);
            if(c_block.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(c_block)!.Value;
                if(info.IsT0)
                {
                    if(info.AsT0.Top is null || info.AsT0.Bottom is null)
                    {
                        if (args.TargetSide == BlockSide.Top)
                            if (info.AsT0.Bottom is not null)
                            {
                                args.World.SetBlock(args.TargetedBlockPozition, new(info.AsT0.Bottom, 0, this.CodeName));
                                return true;
                            }
                        if (args.TargetSide == BlockSide.Bottom)
                            if (info.AsT0.Top is not null)
                            {
                                args.World.SetBlock(args.TargetedBlockPozition, new(this, 0, info.AsT0.Top!.CodeName));
                                return true;
                            }
                    }
                }
            }
        }
        if(args.TargetSide == BlockSide.Top)
        {
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                if (info.IsT1)
                    return false;
                if (info.AsT0.Bottom is not null)
                    return false;
                args.World.SetBlock(args.TargetBlockPozition, new(this, 0, info.AsT0.Top!.CodeName));
            }
            else
                args.World.SetBlock(args.TargetBlockPozition, new(this));
            return true;
        }
        if(args.TargetSide == BlockSide.Bottom)
        {
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                if (info.IsT1)
                    return false;
                if (info.AsT0.Top is not null)
                    return false;
                args.World.SetBlock(args.TargetBlockPozition, new(info.AsT0.Bottom!, 0, this.CodeName));
            }
            else
                args.World.SetBlock(args.TargetBlockPozition, new(this, 0, "+"));
            return true;
        }
        else
        {
            var is_upper = args.InWorldPoint.Y % 1 > .5f;
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if(target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                if (info.IsT1)
                    return false;
                if((is_upper ? info.AsT0.Top : info.AsT0.Bottom) is not null)
                    return false;
                if(is_upper)
                    args.World.SetBlock(args.TargetBlockPozition, new(info.AsT0.Bottom!, 0, this.CodeName));
                else
                    args.World.SetBlock(args.TargetBlockPozition, new(this, 0, info.AsT0.Bottom!.CodeName));
                return true;
            }
            args.World.SetBlock(args.TargetBlockPozition, new(this, 0, is_upper ? "+" : string.Empty));
            return true;
        }
    }

    public override sealed IEnumerable<BlockCollider> GetInteractionCollision(StandardBlockSet set)
    {
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta.Length > 1)
        {
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        }
    }
    public override sealed IEnumerable<BlockCollider> GetPhisicCollision(StandardBlockSet set)
    {
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta.Length > 1)
            yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
    }
    public override sealed IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> GetInteractionModel(StandardBlockSet set)
    {
        if(string.IsNullOrEmpty(set.block.Meta))
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, @base.start.y / 2, @base.start.z), (@base.end.x, @base.end.y / 2, @base.end.z));
        else if (set.block.Meta[0] == '+')
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, (@base.start.y / 2) + .5f, @base.start.z), (@base.end.x, (@base.end.y / 2) + .5f, @base.end.z));
        else if(set.block.Meta[0] == '|')
        {
            foreach (var @base in base.GetInteractionModel(set))
                yield return @base;
        }
        else
        {
            if(set.HitBoxIndex == 0)
                foreach (var @base in base.GetInteractionModel(set))
                    yield return ((@base.start.x, @base.start.y / 2, @base.start.z), (@base.end.x, @base.end.y / 2, @base.end.z));
            else
                foreach (var @base in base.GetInteractionModel(set))
                    yield return ((@base.start.x, (@base.start.y / 2) + .5f, @base.start.z), (@base.end.x, (@base.end.y / 2) + .5f, @base.end.z));
        }
    }
}
