namespace Create.Elements.Bazic.Blocks;

using Create.Conteiner;
using Create.Linq;
using System.Collections.Generic;
using StairsInfo = (StairsBase @base, bool isUpper, (bool NW, bool NE, bool SW, bool SE) StepPrezs, Block.BlockSide MainDirection);

public abstract class StairsBase : Block
{
    public static StairsInfo? InterpretPlacedBlock(PlacedBlock placedBlock)
    {
        // Check that the targeted block is a stair-type
        if (placedBlock.Block is not StairsBase)
            return null;

        // If there is no meta return the default configuration
        if (placedBlock.Meta.Length == 0)
            return ((StairsBase)placedBlock.Block, false, (true, true, false, false), BlockSide.South);

        // The second character if it's present and is set to '^' specifies that this stair base is on top
        bool isUpper = false;
        if (placedBlock.Meta.Length >= 2)
            isUpper = placedBlock.Meta[1] == '^';

        // N - ▀ | E - ▐ | S - ▄ | W - ▌

        // First character if its present specifies in which direction stairs are directed
        BlockSide side = BlockSide.South;
        if (placedBlock.Meta.Length >= 1)
            side = placedBlock.Meta[0] switch
            {
                '▄' => BlockSide.North,
                '▌' => BlockSide.East,
                '▀' => BlockSide.South,
                '▐' => BlockSide.West,
                _ => BlockSide.South,
            };

        // ┌───┬───┐
        // │NW │NE │
        // ├───┼───┤
        // │SW │NE │
        // └───┴───┘
        // See with pair of stairs has to be extended
        (bool NW, bool NE, bool SW, bool SE) steps = new();
        steps = side switch
        {
            BlockSide.North => (false, false, true, true),
            BlockSide.South => (true, true, false, false),
            BlockSide.East => (true, false, true, false),
            BlockSide.West => (false, true, false, true),
            _ => (true, true, false, false)
        };

        return ((StairsBase)placedBlock.Block, isUpper, steps, side);
    }
    public static StairsInfo? InterpretPlacedBlock(StandardBlockSet placedBlock)
    {
        if (!InterpretPlacedBlock(placedBlock.block).IsNotNull(out var info))
            return null;

        if(info.MainDirection is BlockSide.North or BlockSide.South)
        {
            var n = WhereIsFaceing(placedBlock.world.GetBlock(placedBlock.pozition.ToVector() + new OpenTK.Mathematics.Vector3i(0, 0, 1)), info.isUpper);
            var s = WhereIsFaceing(placedBlock.world.GetBlock(placedBlock.pozition.ToVector() + new OpenTK.Mathematics.Vector3i(0, 0, -1)), info.isUpper);

            if (info.MainDirection == BlockSide.North)
            {
                if (n.IsNotNull(out var N) ? N is BlockSide.East or BlockSide.West : false)
                    if (N == BlockSide.East)
                        info.StepPrezs.NW = true;
                    else
                        info.StepPrezs.NE = true;
                else if (s.IsNotNull(out var S) ? S is BlockSide.East or BlockSide.West : false)
                    if (S == BlockSide.East)
                        info.StepPrezs.SE = false;
                    else
                        info.StepPrezs.SW = false;
            }
            else
            {
                if (s.IsNotNull(out var S) ? S is BlockSide.East or BlockSide.West : false)
                    if (S == BlockSide.East)
                        info.StepPrezs.SW = true;
                    else
                        info.StepPrezs.SE = true;
                else if (n.IsNotNull(out var N) ? N is BlockSide.East or BlockSide.West : false)
                    if (N == BlockSide.East)
                        info.StepPrezs.NE = false;
                    else
                        info.StepPrezs.NW = false;
            }
        }
        else if (info.MainDirection is BlockSide.East or BlockSide.West)
        {
            var e = WhereIsFaceing(placedBlock.world.GetBlock(placedBlock.pozition.ToVector() + new OpenTK.Mathematics.Vector3i(1, 0, 0)), info.isUpper);
            var w = WhereIsFaceing(placedBlock.world.GetBlock(placedBlock.pozition.ToVector() + new OpenTK.Mathematics.Vector3i(-1, 0, 0)), info.isUpper);

            if(info.MainDirection == BlockSide.West)
            {
                if (w.IsNotNull(out var W) ? W is BlockSide.North or BlockSide.South : false)
                    if (W == BlockSide.North)
                        info.StepPrezs.SW = true;
                    else
                        info.StepPrezs.NW = true;
                else if (e.IsNotNull(out var E) ? E is BlockSide.North or BlockSide.South : false)
                    if (E == BlockSide.North)
                        info.StepPrezs.NE = false;
                    else
                        info.StepPrezs.SE = false;
            }
            else
            {
                if (e.IsNotNull(out var E) ? E is BlockSide.North or BlockSide.South : false)
                    if (E == BlockSide.North)
                        info.StepPrezs.SE = true;
                    else
                        info.StepPrezs.NE = true;
                else if (w.IsNotNull(out var W) ? W is BlockSide.North or BlockSide.South : false)
                    if (W == BlockSide.North)
                        info.StepPrezs.NW = false;
                    else
                        info.StepPrezs.SW = false;
            }
        }

        return info;
    }

    public static BlockSide? WhereIsFaceing(PlacedBlock block) => block.Block is not StairsBase ? null :
        (string.IsNullOrEmpty(block.Meta) ? '▀' : block.Meta[0]) switch
            {
                '▄' => BlockSide.North,
                '▌' => BlockSide.East,
                '▀' => BlockSide.South,
                '▐' => BlockSide.West,
                _ => BlockSide.South,
            };

    public static BlockSide? WhereIsFaceing(PlacedBlock block, bool asumeIsUpper)
    {
        if (GetOrientation(block).IsNotNull(out var info))
            if (info.isUpper == asumeIsUpper)
                return info.side;
        return null;
    }
    public static bool? IsUpper(PlacedBlock block, BlockSide asumeSide)
    {
        if (GetOrientation(block).IsNotNull(out var info))
            if (info.side == asumeSide)
                return info.isUpper;
        return null;
    }
    public static bool? IsUpper(PlacedBlock block) => block.Block is not StairsBase ? null : (block.Meta.Length >= 2 ? block.Meta[1] == '^' : false);
    public static (BlockSide side, bool isUpper)? GetOrientation(PlacedBlock block)
    {
        if (block.Block is not StairsBase)
            return null;

        var side = (string.IsNullOrEmpty(block.Meta) ? '▀' : block.Meta[0]) switch
        {
            '▄' => BlockSide.North,
            '▌' => BlockSide.East,
            '▀' => BlockSide.South,
            '▐' => BlockSide.West,
            _ => BlockSide.South,
        };

        var isUp = (block.Meta.Length >= 2 ? block.Meta[1] == '^' : false);

        return (side, isUp);
    }

    public override bool OnPlaceBlock(PlaceBlock args)
    {
        var rough = Entitys.Mob.RouthDirection(args.Player.Entity!, true);

        if (args.World.GetBlock(args.TargetBlockPozition).Block == Elements.Blocks.AIR)
        {
            bool is_upper = false;
            if (args.TargetSide == BlockSide.Bottom)
                is_upper = true;
            else if (args.TargetSide != BlockSide.Top)
                is_upper = args.InWorldPoint.Y % 1 > .5f;

            args.World.SetBlock(args.TargetBlockPozition, new(this, 0, rough switch
            {
                BlockSide.North => "▀",
                BlockSide.East => "▐",
                BlockSide.South => "▄",
                BlockSide.West => "▌",
                _ => "▀"
            } + (is_upper ? "^" : string.Empty)));
        }
        else
            return false;
        return true;
    }

    public override bool IsSideVisible(StandardBlockSet sideSet, BlockSide side)
    {
        if (!InterpretPlacedBlock(sideSet).IsNotNull(out var info))
            return base.IsSideVisible(sideSet, side);
        if (side == BlockSide.Bottom)
            return info.isUpper;
        else if (side == BlockSide.Top)
            return !info.isUpper;

        else if (side == BlockSide.North)
            return !(info.StepPrezs.NW && info.StepPrezs.NE);
        else if (side == BlockSide.South)
            return !(info.StepPrezs.SW && info.StepPrezs.SE);
        else if (side == BlockSide.East)
            return !(info.StepPrezs.SE && info.StepPrezs.NE);
        else if (side == BlockSide.West)
            return !(info.StepPrezs.SW && info.StepPrezs.NW);
        
        return base.IsSideVisible(sideSet, side);
    }

    public override IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> GetInteractionModel(StandardBlockSet set)
    {
        if(!InterpretPlacedBlock(set).IsNotNull(out var info))
        {
            foreach (var line in base.GetInteractionModel(set))
                yield return line;
            yield break;
        }

        if(info.isUpper)
        {
            yield return ((0, 1, 0), (1, 1, 0));
            yield return ((0, 1, 1), (1, 1, 1));
            yield return ((1, 1, 1), (1, 1, 0));
            yield return ((0, 1, 1), (0, 1, 0));

            yield return ((0, 1, 0), (0, info.StepPrezs.SW ? 0 : .5f, 0));
            yield return ((0, 1, 1), (0, info.StepPrezs.NW ? 0 : .5f, 1));
            yield return ((1, 1, 0), (1, info.StepPrezs.SE ? 0 : .5f, 0));
            yield return ((1, 1, 1), (1, info.StepPrezs.NE ? 0 : .5f, 1));
        }
        else
        {
            yield return ((0, 0, 0), (1, 0, 0));
            yield return ((0, 0, 1), (1, 0, 1));
            yield return ((1, 0, 1), (1, 0, 0));
            yield return ((0, 0, 1), (0, 0, 0));

            yield return ((0, 0, 0), (0, info.StepPrezs.SW ? 1 : .5f, 0));
            yield return ((0, 0, 1), (0, info.StepPrezs.NW ? 1 : .5f, 1));
            yield return ((1, 0, 0), (1, info.StepPrezs.SE ? 1 : .5f, 0));
            yield return ((1, 0, 1), (1, info.StepPrezs.NE ? 1 : .5f, 1));
        }

        if(!info.StepPrezs.NE || !info.StepPrezs.NW)
            yield return (((info.StepPrezs.NW ? .5f : 0), .5f, 1), ((info.StepPrezs.NE ? .5f : 1), .5f, 1));
        if (!info.StepPrezs.SE || !info.StepPrezs.SW)
            yield return (((info.StepPrezs.SW ? .5f : 0), .5f, 0), ((info.StepPrezs.SE ? .5f : 1), .5f, 0));
        if (!info.StepPrezs.NE || !info.StepPrezs.SE)
            yield return ((1, .5f, (info.StepPrezs.SE ? .5f : 0)), (1, .5f, (info.StepPrezs.NE ? .5f : 1)));
        if (!info.StepPrezs.SW || !info.StepPrezs.NW)
            yield return ((0, .5f, (info.StepPrezs.SW ? .5f : 0)), (0, .5f, (info.StepPrezs.NW ? .5f : 1)));

        var Y = (info.isUpper ? 0 : 1);
        if (info.StepPrezs.NE || info.StepPrezs.NW)
            yield return (((info.StepPrezs.NW ? 0 : .5f), Y, 1), ((info.StepPrezs.NE ? 1 : .5f), Y, 1));
        if (info.StepPrezs.SE || info.StepPrezs.SW)
            yield return (((info.StepPrezs.SW ? 0 : .5f), Y, 0), ((info.StepPrezs.SE ? 1 : .5f), Y, 0));
        if (info.StepPrezs.NE || info.StepPrezs.SE)
            yield return ((1, Y, (info.StepPrezs.SE ? 0 : .5f)), (1, Y, (info.StepPrezs.NE ? 1 : .5f)));
        if (info.StepPrezs.SW || info.StepPrezs.NW)
            yield return ((0, Y, (info.StepPrezs.SW ? 0 : .5f)), (0, Y, (info.StepPrezs.NW ? 1 : .5f)));

        if (info.StepPrezs.NE ^ info.StepPrezs.NW)
            yield return ((.5f, .5f, 1), (.5f, Y, 1));
        if (info.StepPrezs.SE ^ info.StepPrezs.SW)
            yield return ((.5f, .5f, 0), (.5f, Y, 0));
        if (info.StepPrezs.NE ^ info.StepPrezs.SE)
            yield return ((1, .5f, .5f), (1, Y, .5f));
        if (info.StepPrezs.NW ^ info.StepPrezs.SW)
            yield return ((0, .5f, .5f), (0, Y, .5f));

        if((info.StepPrezs.NE ^ info.StepPrezs.NW) || (info.StepPrezs.SE ^ info.StepPrezs.SW)) {
            bool n = info.StepPrezs.NE ^ info.StepPrezs.NW, s = info.StepPrezs.SE ^ info.StepPrezs.SW;
            yield return ((.5f, .5f, (s ? 0 : .5f)), (.5f, .5f, (n ? 1 : .5f)));
            yield return ((.5f, Y, (s ? 0 : .5f)), (.5f, Y, (n ? 1 : .5f))); }
        if ((info.StepPrezs.NE ^ info.StepPrezs.SE) || (info.StepPrezs.SE ^ info.StepPrezs.SW)) {
            bool e = info.StepPrezs.NE ^ info.StepPrezs.SE, w = info.StepPrezs.NW ^ info.StepPrezs.SW;
            yield return (((w ? 0 : .5f), .5f, .5f), ((e ? 1 : .5f), .5f, .5f));
            yield return (((w ? 0 : .5f), Y, .5f), ((e ? 1 : .5f), Y, .5f)); }

        yield break;
        //return base.GetInteractionModel(set);
    }

    public override IEnumerable<BlockCollider> GetPhisicCollision(StandardBlockSet set)
    {
        if (!InterpretPlacedBlock(set).IsNotNull(out var info))
        {
            yield return new() { pozition = new(.5f, .5f, .5f), size = new(1, 1, 1) };
            yield break;
        }

        yield return new() { pozition = new(.5f, info.isUpper ? .75f : .25f, .5f), size = new(1, .5f, 1) };

        float Y = info.isUpper ? .25f : .75f;

        if(!(info.StepPrezs.SE ^ info.StepPrezs.NE) && !(info.StepPrezs.SW ^ info.StepPrezs.NW) && info.StepPrezs.NW != info.StepPrezs.NE)
            yield return new() { pozition = new(info.StepPrezs.NW ? .25f : .75f, Y, .5f), size = new(.5f, .5f, 1) };
        else
        {
            if (info.StepPrezs.NW && info.StepPrezs.NE)
                yield return new() { pozition = new(.5f, Y, .75f), size = new(1, .5f, .5f) };
            else if (info.StepPrezs.NW ^ info.StepPrezs.NE)
                yield return new() { pozition = new(info.StepPrezs.NE ? .75f : .25f, Y, .75f), size = new(.5f, .5f, .5f) };

            if (info.StepPrezs.SW && info.StepPrezs.SE)
                yield return new() { pozition = new(.5f, Y, .25f), size = new(1, .5f, .5f) };
            else if (info.StepPrezs.SW ^ info.StepPrezs.SE)
                yield return new() { pozition = new(info.StepPrezs.SW ? .25f : .75f, Y, .25f), size = new(.5f, .5f, .5f) };
        }
    }

    public override IEnumerable<BlockCollider> GetInteractionCollision(StandardBlockSet set) => GetPhisicCollision(set);
}
