namespace Create.Elements.Bazic.Blocks;

using Create.Conteiner;
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
    public static StairsInfo? InterpretPlacedBlock(StandardBlockSet placedBlock) => InterpretPlacedBlock(placedBlock.block);

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
}
