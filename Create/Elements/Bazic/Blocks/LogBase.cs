namespace Create.Elements.Bazic.Blocks;

public abstract class LogBase : Block
{
    public override bool OnPlaceBlock(PlaceBlock args)
    {
        var block = args.BlockStack.AsPlacedBlock();
        block = new(block.Block, block.Type, args.TargetSide switch
        {
            BlockSide.Top => "0",
            BlockSide.Bottom => "0",
            BlockSide.East => "1",
            BlockSide.West => "1",
            BlockSide.North => "2",
            BlockSide.South => "2",
            _ => string.Empty
        });
        args.World.SetBlock(args.TargetBlockPozition, block);
        return true;
    }
}
