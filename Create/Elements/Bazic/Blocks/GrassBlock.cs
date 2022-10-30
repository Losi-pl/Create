using System.Drawing;

namespace Create.Elements.Bazic.Blocks;

internal sealed class GrassBlock : GrassBase
{
    public override void OnRegistered()
    {
        this.SetTextures(
            Render.Textures.Handles["create:dirt"],
            Render.Textures.Handles["create:grass_block_top"],
            Render.Textures.Handles["create:grass_block_side_overlay"],
            Color.FromArgb(255, 65, 149, 39));
    }
}
