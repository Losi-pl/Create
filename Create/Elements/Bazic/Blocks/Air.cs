using Create.Render;

namespace Create.Elements.Bazic.Blocks;

internal sealed class Air : Block
{
    public override void GenerateModel(StandardBlockSet @struct, ModelConstructor constructor) { }
    public override bool IsSideVisible(StandardBlockSet @struct, BlockSide side) => true;
    public override BlockCollider[] GetPhisicCollision(StandardBlockSet set) => Array.Empty<BlockCollider>();
    public override BlockCollider[] GetInteractionCollision(StandardBlockSet set) => Array.Empty<BlockCollider>();
}
