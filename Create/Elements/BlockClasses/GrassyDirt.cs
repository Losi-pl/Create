using System.Drawing;

namespace Create.Elements.BlockClasses;

public class GrassyDirt : CoveredBlock
{
    protected override void OnElementRegistered()
    {
        TextureSet = StandardTextureSet("create:dirt", "create:grass-block-side", "create:grass-block-top", Color.ForestGreen);
    }
}