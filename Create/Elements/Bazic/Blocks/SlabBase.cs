using Create.Conteiner;
using Create.Elements.Bazic.Entitys;
using Create.Elements.Bazic.Items;
using Create.Elements.Recipes;
using Create.Linq;
using Create.Space;
using OneOf;
using SlabInfo = OneOf.OneOf<(Create.Elements.Block? Bottom, Create.Elements.Block? Top), 
    (Create.Elements.Block? Column1, Create.Elements.Block? Column2, bool IsAlongTheXAxis)>;

namespace Create.Elements.Bazic.Blocks;

public abstract class SlabBase : Block
{
    /// <summary>
    /// Interpretuje dane bloku aby ułatwić interakcje i modyfikacje tego typu bloków
    /// </summary>
    /// <param name="placedBlock"></param>
    /// <returns></returns>
    public static SlabInfo? InterpretPlacedBlock(PlacedBlock placedBlock)
    {
        // Checks if the PlacedBlock is a slab
        if (placedBlock.Block is not SlabBase slab)
            return null;

        // If there is no meta, then it's a default configuration of a slab (lower horizontal)
        if (string.IsNullOrEmpty(placedBlock.Meta))
            return (placedBlock.Block, null);

        // If meta only consists of '+', it means that it's an upper horizontal slab
        if (placedBlock.Meta[0] == '+')
            return (null, placedBlock.Block);

        // If meta starts with '\' or '|' that means that the slab/s are vertical
        else if (placedBlock.Meta[0] is '|' or '/')
        {
            // If Meta only specifies that the slab is vertical then it will be placed on the: '/' - Soulth, '|' - West
            if (placedBlock.Meta.Length == 1)
                return (placedBlock.Block, null, placedBlock.Meta[0] is '/');

            // The additional character '+' after specifying that slab is vertical means that the slab is on the opposite side. '/' - North, '|' - East
            if (placedBlock.Meta[1] == '+')
                return (null, placedBlock.Block, placedBlock.Meta[0] is '/');

            // If Meta contains only a marker of a vertical slab and a code name that means that there are two slabs in the same block '/' - South>North, '|' - West>East
            else
                return (placedBlock.Block, Register.Blocks.ByName[placedBlock.Meta.Substring(1)], placedBlock.Meta[0] is '/');
        }

        // If Meta only contains a code name, it means that there are two slabs lying on top of each other: the placedBlock.Block specifying one on the bottom and the Meta one on the top
        else
            return (placedBlock.Block, Register.Blocks.ByName[placedBlock.Meta]);
    }
    
    public override sealed bool IsSideVisible(StandardBlockSet sideSet, BlockSide side)
    {
        // Confirm that this block is a slab
        if (!InterpretPlacedBlock(sideSet.block).IsNotNull(out var info))
            return true;
        
        // If the slab is horizontal
        if (info.Index == 0)
        {
            switch (side)
            {
                // Lower slab will hide the bottom
                case BlockSide.Bottom:
                    return info.AsT0.Bottom is null;

                // Upper slab will hide the top
                case BlockSide.Top:
                    return info.AsT0.Top is null;

                // If there is at least one free space in the block, the side is considered to be visible
                default:
                    return info.AsT0.Top is null || info.AsT0.Bottom is null;
            }
        }
        else if (info.Index == 1)
            switch (side)
            {
                // If there is at least one free space in the block, the side is considered to be visible
                case BlockSide.Bottom:
                    return info.AsT1.Column1 is null || info.AsT1.Column2 is null;

                // If there is at least one free space in the block, the side is considered to be visible
                case BlockSide.Top:
                    return info.AsT1.Column1 is null || info.AsT1.Column2 is null;

                    // The rest is the same way, but it also includes a check of the alignment of the vertical slabs
                    // If aligned, then just check if the slab on that side is taken.
                    // Otherwise, check if either side is empty.
                case BlockSide.North:
                    if (info.AsT1.IsAlongTheXAxis)
                        return info.AsT1.Column2 is null;
                    else
                        return info.AsT1.Column1 is null || info.AsT1.Column2 is null;
                case BlockSide.South:
                    if (info.AsT1.IsAlongTheXAxis)
                        return info.AsT1.Column1 is null;
                    else
                        return info.AsT1.Column1 is null || info.AsT1.Column2 is null;
                case BlockSide.East:
                    if (!info.AsT1.IsAlongTheXAxis)
                        return info.AsT1.Column2 is null;
                    else
                        return info.AsT1.Column1 is null || info.AsT1.Column2 is null;
                case BlockSide.West:
                    if (!info.AsT1.IsAlongTheXAxis)
                        return info.AsT1.Column1 is null;
                    else
                        return info.AsT1.Column1 is null || info.AsT1.Column2 is null;
            }
        return true;
    }

    public override sealed bool OnPlaceBlock(PlaceBlock args)
    {
        { // Interactions with slabs in the same block

            // Test if the player is aiming at the inside of a placed slab
            var c_block = args.World.GetBlock(args.TargetedBlockPozition);
            if (InterpretPlacedBlock(c_block).IsNotNull(out var info))
            {
                if (info.IsT0)
                {
                    // Verify if there is room for a slab in the block
                    if (info.AsT0.Top is null || info.AsT0.Bottom is null)
                    {
                        // Test if player clicks on top of a lower slab block
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

        // See if slab used by the player is vertical
        if(string.IsNullOrEmpty(args.BlockStack.AsPlacedBlock().Meta) ? false : args.BlockStack.AsPlacedBlock().Meta[0] is '|' or '/')
        {
            if(args.TargetSide is BlockSide.Top or BlockSide.Bottom)
            {
                // Calculate potential slab orientation
                var along_the_X = Mob.RouthDirection(args.Player.Entity!, true) is BlockSide.North or BlockSide.South;
                var slide = along_the_X ? args.InWorldPoint.Z % 1 : args.InWorldPoint.X % 1;
                var is_upper = (slide >= 0 ? slide : 1 + slide) > .5f;

                // Check if the targeted location alredy has a slab present
                if (InterpretPlacedBlock(args.World.GetBlock(args.TargetBlockPozition)).IsNotNull(out var info))
                {
                    // Check if the slab/s in that place is vertical
                    if (!info.IsT1)
                        return false;

                    // Check if the slab/s in that place are in the same orientation as intended
                    if (along_the_X != info.AsT1.IsAlongTheXAxis)
                        return false;

                    // Check if the place if free
                    if ((is_upper ? info.AsT1.Column2 : info.AsT1.Column1) is not null)
                        return false;

                    // Update the block placed in the world
                    if(is_upper)
                        args.World.SetBlock(args.TargetBlockPozition, new(info.AsT1.Column1!, 0, (along_the_X ? "/" : "|") + this.CodeName));
                    else
                        args.World.SetBlock(args.TargetBlockPozition, new(this, 0, (along_the_X ? "/" : "|") + info.AsT1.Column2!.CodeName));
                }
                
                // If the location is empty then just set the block
                else if (args.World.GetBlock(args.TargetBlockPozition).Block == Elements.Blocks.AIR)
                    args.World.SetBlock(args.TargetBlockPozition, new(this, 0, (along_the_X ? "/" : "|") + (is_upper ? "+" : string.Empty)));
                
                // The location is alredy taken by something else
                else
                    return false;
            }
            else
            {
                var is_upper = args.TargetSide is BlockSide.South or BlockSide.West;
                var along_the_X = args.TargetSide is BlockSide.North or BlockSide.South;

                // Check if the targeted location alredy has a slab present
                if (InterpretPlacedBlock(args.World.GetBlock(args.TargetBlockPozition)).IsNotNull(out var info))
                {
                    // Check if the slab/s in that place is vertical
                    if (!info.IsT1)
                        return false;

                    // Check if the slab/s in that place are in the same orientation as intended
                    if (along_the_X != info.AsT1.IsAlongTheXAxis)
                        return false;

                    // Check if the place if free
                    if ((is_upper ? info.AsT1.Column2 : info.AsT1.Column1) is not null)
                        return false;

                    // Update the block placed in the world
                    if (is_upper)
                        args.World.SetBlock(args.TargetBlockPozition, new(info.AsT1.Column1!, 0, (along_the_X ? "/" : "|") + this.CodeName));
                    else
                        args.World.SetBlock(args.TargetBlockPozition, new(this, 0, (along_the_X ? "/" : "|") + info.AsT1.Column2!.CodeName));
                }

                // If the location is empty then just set the block
                else if (args.World.GetBlock(args.TargetBlockPozition).Block == Elements.Blocks.AIR)
                    args.World.SetBlock(args.TargetBlockPozition, new(this, 0, (along_the_X ? "/" : "|") + (is_upper ? "+" : string.Empty)));

                // The location is alredy taken by something else
                else
                    return false;
            }

            return true;
        }

        // Placing a new slab on top of a targeted block
        if (args.TargetSide == BlockSide.Top)
        {
            // See if the block above is also a slab
            var target_bl = args.World.GetBlock(args.TargetBlockPozition);
            if (InterpretPlacedBlock(target_bl).IsNotNull(out var info))
            {
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
            if (InterpretPlacedBlock(target_bl).IsNotNull(out var info))
            {
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
            if (InterpretPlacedBlock(target_bl).IsNotNull(out var info))
            {
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
    {
        // Check if the block interacted with is a slab
        if (!InterpretPlacedBlock(args.Block).IsNotNull(out var info))
            return false;

        // If the slab/s are horizontal
        if(info.IsT0)
        {
            // If there is only one slab just remove it
            if (info.AsT0.Top is null || info.AsT0.Bottom is null)
                args.World.SetBlock(args.BlockPozition, new());
            else
            {
                // Replace a slab block but with the specified part removed
                if (args.HitBoxIndex == 0)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT0.Top, 0, "+"));
                else if (args.HitBoxIndex == 1)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT0.Bottom));

                // Something is wrong
                else
                    return false;
            }
        }

        // If the slab/s are vertical
        else
        {
            // If there is only one slab just remove it
            if (info.AsT1.Column1 is null || info.AsT1.Column2 is null)
                args.World.SetBlock(args.BlockPozition, new());
            else
            {
                // Replace a slab block but with the specified part removed
                if (args.HitBoxIndex == 0)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT1.Column2, 0,  (info.AsT1.IsAlongTheXAxis ? "/" : "|") + "+"));
                else if (args.HitBoxIndex == 1)
                    args.World.SetBlock(args.BlockPozition, new(info.AsT1.Column1, 0, info.AsT1.IsAlongTheXAxis ? "/" : "|"));

                // Something is wrong
                else
                    return false;
            }
        }

        return true;
    }

    public override string GetItemName(ItemName args)
    {
        // If there is no meta then it's I don't even know
        if (string.IsNullOrEmpty(args.Item.Meta))
            return base.GetItemName(args);

        // Directly get the meta of the slab from the item
        var nMeta = BlockItem.GetBlockMeta(args.Item);
        
        // If the block has no meta then its just a normal default slab
        if(nMeta.Length == 0)
            return base.GetItemName(args);

        // Check if it's a vertical slab and if so, add a vertical prefix
        if (nMeta[0] is '/' or '|')
            return string.Format(Assets.Language.GetFromKey("create.blocks.format.slab.vertical.name"), base.GetItemName(args));

        // Whatever else is happening
        return base.GetItemName(args);
    }

    public override sealed IEnumerable<BlockCollider> GetInteractionCollision(StandardBlockSet set)
    {
        // No extra meta so a default slab (lower horizontal)
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };

        // If it's a vertical slab/s
        else if (set.block.Meta[0] is '|' or '/')
        {
            // If it's a single slab in the first column
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .25f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.25f, .5f, .5f), size = (.5f, 1, 1) };

            // If it's a single slab in the secound column
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .75f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.75f, .5f, .5f), size = (.5f, 1, 1) };

            // If there are two slabs
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

        // If it's an upper slab
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };

        // Two horizontal slabs
        else if (set.block.Meta.Length > 1)
        {
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };
        }
    }
    public override sealed IEnumerable<BlockCollider> GetPhisicCollision(StandardBlockSet set)
    {
        // No extra meta so a default slab (lower horizontal)
        if (string.IsNullOrEmpty(set.block.Meta))
            yield return new() { pozition = (.5f, .25f, .5f), size = (1, .5f, 1) };

        // If it's a vertical slab/s
        else if (set.block.Meta[0] is '|' or '/')
        {
            // If it's a single slab in the first column
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .25f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.25f, .5f, .5f), size = (.5f, 1, 1) };

            // If it's a single slab in the secound column
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    yield return new() { pozition = (.5f, .5f, .75f), size = (1, 1, .5f) };
                else
                    yield return new() { pozition = (.75f, .5f, .5f), size = (.5f, 1, 1) };
            else
                yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
        }

        // If it's an upper slab
        else if (set.block.Meta[0] == '+')
            yield return new() { pozition = (.5f, .75f, .5f), size = (1, .5f, 1) };

        // Two horizontal slabs
        else if (set.block.Meta.Length > 1)
            yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
    }
    public override sealed IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> GetInteractionModel(StandardBlockSet set)
    {
        // No extra meta so a default slab (single lower horizontal)
        if (string.IsNullOrEmpty(set.block.Meta))
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, @base.start.y / 2, @base.start.z), (@base.end.x, @base.end.y / 2, @base.end.z));

        // If it's an upper slab
        else if (set.block.Meta[0] == '+')
            foreach (var @base in base.GetInteractionModel(set))
                yield return ((@base.start.x, (@base.start.y / 2) + .5f, @base.start.z), (@base.end.x, (@base.end.y / 2) + .5f, @base.end.z));

        // If it's a vertical slab/s
        else if (set.block.Meta[0] is '|' or '/')
        {
            // If it's a single slab in the first column
            if (set.block.Meta.Length == 1)
                if (set.block.Meta[0] is '/')
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x, @base.start.y, @base.start.z / 2), (@base.end.x, @base.end.y, @base.end.z / 2));
                else
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x / 2, @base.start.y, @base.start.z), (@base.end.x / 2, @base.end.y, @base.end.z));

            // If it's a single slab in the secound column
            else if (set.block.Meta[1] == '+')
                if (set.block.Meta[0] is '/')
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return ((@base.start.x, @base.start.y, (@base.start.z / 2) + .5f), (@base.end.x, @base.end.y, (@base.end.z / 2) + .5f));
                else
                    foreach (var @base in base.GetInteractionModel(set))
                        yield return (((@base.start.x / 2) + .5f, @base.start.y, @base.start.z), ((@base.end.x / 2) + .5f, @base.end.y, @base.end.z));

            // There are two vertical slabs to choose from
            else
            {
                // A slab in the first column
                if(set.HitBoxIndex == 0)
                    if (set.block.Meta[0] is '/')
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x, @base.start.y, @base.start.z / 2), (@base.end.x, @base.end.y, @base.end.z / 2));
                    else
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x / 2, @base.start.y, @base.start.z), (@base.end.x / 2, @base.end.y, @base.end.z));

                // A slab in the secound column
                else
                    if (set.block.Meta[0] is '/')
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return ((@base.start.x, @base.start.y, (@base.start.z / 2) + .5f), (@base.end.x, @base.end.y, (@base.end.z / 2) + .5f));
                    else
                        foreach (var @base in base.GetInteractionModel(set))
                            yield return (((@base.start.x / 2) + .5f, @base.start.y, @base.start.z), ((@base.end.x / 2) + .5f, @base.end.y, @base.end.z));
            }
        }

        // There are two horizontal slabs to choose from
        else
        {
            // A lower slab
            if (set.HitBoxIndex == 0)
                foreach (var @base in base.GetInteractionModel(set))
                    yield return ((@base.start.x, @base.start.y / 2, @base.start.z), (@base.end.x, @base.end.y / 2, @base.end.z));

            // An upper slab
            else
                foreach (var @base in base.GetInteractionModel(set))
                    yield return ((@base.start.x, (@base.start.y / 2) + .5f, @base.start.z), (@base.end.x, (@base.end.y / 2) + .5f, @base.end.z));
        }
    }

    internal static void BazicSlabRecipes(Mod mod)
    {
        mod.RegisterRecipe("to-vertical-slabs", new ItemAlteration(
            s => BlockItem.GetBlock(s) is SlabBase && BlockItem.GetBlockMeta(s).Length == 0,
            s => (new(BlockItem.GetBlock(s), 0, "/+"), s.Count)));

        mod.RegisterRecipe("to-horizontal-slabs", new ItemAlteration(
            s => BlockItem.GetBlock(s) is SlabBase && BlockItem.GetBlockMeta(s).Equals("/+".AsSpan(), StringComparison.Ordinal),
            s => (new(BlockItem.GetBlock(s)), s.Count)));
    }
}
