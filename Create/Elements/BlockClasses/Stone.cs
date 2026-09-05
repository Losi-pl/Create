using Create.Assets;

namespace Create.Elements.BlockClasses;

public class Stone : Block
{
    private BlockTexture _texture;

    protected override void OnElementRegistered()
    {
        _texture = AssetManager.Find<BlockTexture>("create:stone").AsSet;
    }

    public override BlockTexture GetTexture(in GetTextureArgs args) => _texture;
}