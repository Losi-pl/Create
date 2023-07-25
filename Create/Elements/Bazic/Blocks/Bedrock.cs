using Create.Render;
using SixLabors.ImageSharp;

namespace Create.Elements.Bazic.Blocks;

internal class Bedrock : Block
{
    public override void OnRegistered(Mod mod)
    {
        SetTexture(Assets.BlockAtlas.Handles["create:bedrock"]);
    }
}
