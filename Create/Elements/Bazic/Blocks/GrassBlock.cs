using System.Drawing;

namespace Create.Elements.Bazic.Blocks;

internal sealed class GrassBlock : GrassBase
{
    public override void OnRegistered(Mod mod)
    {
        this.SetTextures(
            Assets.BlockAtlas.Handles["create:dirt"],
            Assets.BlockAtlas.Handles["create:grass_block_top"],
            Assets.BlockAtlas.Handles["create:grass_block_side_overlay"],
            Color.FromArgb(255, 65, 149, 39));
    }
}
