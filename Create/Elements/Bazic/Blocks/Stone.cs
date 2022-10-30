using Create.Render;
using SixLabors.ImageSharp;

namespace Create.Elements.Bazic.Blocks;

internal class Stone : Block
{
    public override void OnRegistered()
    {
        SetTexture(Textures.Handles["create:stone"]);
    }
}
