using Create.Elements;

namespace Create.Linq;

public static class EnumC
{
    public static Block.BlockSide Invert(this Block.BlockSide side) => side switch
    {
        Block.BlockSide.Top => Block.BlockSide.Bottom,
        Block.BlockSide.Bottom => Block.BlockSide.Top,
        Block.BlockSide.North => Block.BlockSide.South,
        Block.BlockSide.South => Block.BlockSide.North,
        Block.BlockSide.East => Block.BlockSide.West,
        Block.BlockSide.West => Block.BlockSide.East,
        _ => Block.BlockSide.Top
    };
}
