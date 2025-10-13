using Create.Conteiner;
using Create.Space;
using OneOf;
using SlabInfo = OneOf.OneOf<(Create.Elements.Block? Bottom, Create.Elements.Block? Top), 
    (Create.Elements.Block? Column1, Create.Elements.Block? Column2, bool IsAlongTheXAxis)>;

namespace Create.Elements.Bazic.Blocks;

public abstract class SlabBase : Block
{
    public static SlabInfo? InterpretPlacedBlock(PlacedBlock placedBlock)
    { // TODO - Documentation
        if (placedBlock.Block is not SlabBase slab)
            return null;
        if(string.IsNullOrEmpty(placedBlock.Meta))
            return (placedBlock.Block, null);
        if (placedBlock.Meta[0] == '+')
            return (null, placedBlock.Block);
        else if (placedBlock.Meta[0] is '|' or '/')
        {
            if(placedBlock.Meta.Length == 1)
                return (placedBlock.Block, null, placedBlock.Meta[0] is '/');
            if (placedBlock.Meta[1] == '+')
                return (null, placedBlock.Block, placedBlock.Meta[0] is '/');
            else
                return (placedBlock.Block, Register.Blocks.ByName[placedBlock.Meta.Substring(1)], placedBlock.Meta[0] is '/');
        }
        else
            return (placedBlock.Block, Register.Blocks.ByName[placedBlock.Meta]);
    }
    
    public override sealed bool IsSideVisible(StandardBlockSet sideSet, BlockSide side)
    { // TODO - Documentation
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
        { // Interactions with slabs in the same block
            // Test if the player is aiming at the inside of a placed slab
            var c_block = args.World.GetBlock(args.TargetedBlockPozition);
            if (c_block.Block is SlabBase)
            {
                // Process data of the slabs
                var info = InterpretPlacedBlock(c_block)!.Value;
                if (info.IsT0)
                {
                    // Verify if there is room for a slab in the block
                    if (info.AsT0.Top is null || info.AsT0.Bottom is null)
                    {
                        // Test if player clicks on top of a lover slab block
                        if (args.TargetSide == BlockSide.Top)
                            if (info.AsT0.Bottom is not null)
                            {
                                // Update the slab block
                                args.World.SetBlock(args.TargetedBlockPozition, new(info.AsT0.Bottom, 0, this.CodeName));
                                return true;
                            }
                        // Test if player clicks on the bottom of an upper slab block
                        if (args.TargetSide == BlockSide.Bottom)
                            if (info.AsT0.Top is not null)
                            {
                                // Update the slab block
                                args.World.SetBlock(args.TargetedBlockPozition, new(this, 0, info.AsT0.Top!.CodeName));
                                return true;
                            }
                    }
                }
            }
        } // Interactions with slabs in the same block

        // Placing a new slab on top of a targeted block
        if (args.TargetSide == BlockSide.Top)
        {
            // See if the block above is also a slab
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                // Check if the block above is not vertical
                if (info.IsT1)
                    return false;
                // Check if the lower half of the block is empty
                if (info.AsT0.Bottom is not null)
                    return false;
                // Update the new state of the block to a new one
                args.World.SetBlock(args.TargetBlockPozition, new(this, 0, info.AsT0.Top!.CodeName));
            }
            else
                // Place a new slab block
                args.World.SetBlock(args.TargetBlockPozition, new(this));
            return true;
        }

        // Placing a new slab below the targeted block
        else if (args.TargetSide == BlockSide.Bottom)
        {
            // See if the block below is also a slab
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                // Check if the block below is not vertical
                if (info.IsT1)
                    return false;
                // Check if the upper half of the block is empty
                if (info.AsT0.Top is not null)
                    return false;
                // Update the new state of the block to a new one
                args.World.SetBlock(args.TargetBlockPozition, new(info.AsT0.Bottom!, 0, this.CodeName));
            }
            else
                // Place a new slab block
                args.World.SetBlock(args.TargetBlockPozition, new(this, 0, "+"));
            return true;
        }

        // Placing a new slab to the side of the targeted block
        else
        {
            // See which half of the block the player is aiming for (false - lower, true - upper)
            var is_upper = args.InWorldPoint.Y % 1 > .5f;

            // See if the block to the side is also a slab
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (target_bl.Block is SlabBase)
            {
                var info = InterpretPlacedBlock(target_bl)!.Value;
                // Check if the block below is not vertical
                if (info.IsT1)
                    return false;
                // Check that the targeted half of the block is empty
                if ((is_upper ? info.AsT0.Top : info.AsT0.Bottom) is not null)
                    return false;

                if (is_upper) // Update the block with the new one at the bottom
                    args.World.SetBlock(args.TargetBlockPozition, new(info.AsT0.Bottom!, 0, this.CodeName));
                else // Update the block with the new one at the top
                    args.World.SetBlock(args.TargetBlockPozition, new(this, 0, info.AsT0.Top!.CodeName));
            }
            else
                // Place a new slab block
                args.World.SetBlock(args.TargetBlockPozition, new(this, 0, is_upper ? "+" : string.Empty));
            return true;
        }
    }
    public override bool OnDestroyBlock(DestroyBlock args)
    { // TODO - Documentation
        if (args.Block.Block is not SlabBase)
            return false;
        var info = InterpretPlacedBlock(args.Block)!.Value;
        if(info.IsT0)
        {
            if (info.AsT0.Top is null || info.AsT0.Bottom is null)
                args.World.SetBlock(args.BlockPozition, new());
            else
            {
                if (args.HitBoxIndex == 0)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT0.Top, 0, "+"));
                else if (args.HitBoxIndex == 1)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT0.Bottom));
                else
                    return false;
            }
        }
        else
        {
            // TODO - Interaction with a vertical slab
        }

        return true;
    }

    public override sealed IEnumerable<BlockCollider> GetInteractionCollision(StandardBlockSet set)
    { // TODO - Documentation
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta[0] is '|' or '/')
        {
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .25f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.25f, .5f, .5f), size = (.5f, 1, 1) };
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .75f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.75f, .5f, .5f), size = (.5f, 1, 1) };
            else
                yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
        }
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta.Length > 1)
        {
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        }
    }
    public override sealed IEnumerable<BlockCollider> GetPhisicCollision(StandardBlockSet set)
    { // TODO - Documentation
        
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta[0] is '|' or '/')
        {
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .25f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.25f, .5f, .5f), size = (.5f, 1, 1) };
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .75f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.75f, .5f, .5f), size = (.5f, 1, 1) };
            else
            {
                if (set.block.Meta[0] is '/')
                {
                    yield return new() { pozition = (.5f, .5f, .25f), size = (1, 1, .5f) };
                    yield return new() { pozition = (.5f, .5f, .75f), size = (1, 1, .5f) };
                }
                else
                {
                    yield return new() { pozition = (.25f, .5f, .5f), size = (.5f, 1, 1) };
                    yield return new() { pozition = (.75f, .5f, .5f), size = (.5f, 1, 1) };
                }
            }
        }
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        else if (set.block.Meta.Length > 1)
            yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
    }
    public override sealed IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> GetInteractionModel(StandardBlockSet set)
    { // TODO - Documentation
        if (string.IsNullOrEmpty(set.block.Meta))
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, @base.start.y / 2, @base.start.z), (@base.end.x, @base.end.y / 2, @base.end.z));
        else if (set.block.Meta[0] == '+')
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, (@base.start.y / 2) + .5f, @base.start.z), (@base.end.x, (@base.end.y / 2) + .5f, @base.end.z));
        else if(set.block.Meta[0] is '|' or '/')
        {
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x, @base.start.y, @base.start.z / 2), (@base.end.x, @base.end.y, @base.end.z / 2));
                else
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x / 2, @base.start.y, @base.start.z), (@base.end.x / 2, @base.end.y, @base.end.z));
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x, @base.start.y, (@base.start.z / 2) + .5f), (@base.end.x, @base.end.y, (@base.end.z / 2) + .5f));
                else
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return (((@base.start.x / 2) + .5f, @base.start.y, @base.start.z), ((@base.end.x / 2) + .5f, @base.end.y, @base.end.z));
            else
            {
                if(set.HitBoxIndex == 0)
                    if (set.block.Meta[0] is '/')
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x, @base.start.y, @base.start.z / 2), (@base.end.x, @base.end.y, @base.end.z / 2));
                    else
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x / 2, @base.start.y, @base.start.z), (@base.end.x / 2, @base.end.y, @base.end.z));
                else
                    if (set.block.Meta[0] is '/')
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x, @base.start.y, (@base.start.z / 2) + .5f), (@base.end.x, @base.end.y, (@base.end.z / 2) + .5f));
                    else
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return (((@base.start.x / 2) + .5f, @base.start.y, @base.start.z), ((@base.end.x / 2) + .5f, @base.end.y, @base.end.z));
            }
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
