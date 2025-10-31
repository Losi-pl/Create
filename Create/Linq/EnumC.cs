using Create.Elements;
using OpenTK.Mathematics;

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

    public static Vector3i ToVectorI(this Block.BlockSide side) => side switch
    {
        Block.BlockSide.Top => new Vector3i(0, 1, 0),
        Block.BlockSide.Bottom => new Vector3i(0, -1, 0),
        Block.BlockSide.North => new Vector3i(0, 0, 1),
        Block.BlockSide.South => new Vector3i(0, 0, -1),
        Block.BlockSide.East => new Vector3i(1, 0, 0),
        Block.BlockSide.West => new Vector3i(-1, 0, 0),
        _ => Vector3i.Zero
    };
}
