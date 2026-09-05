using Create.Assets;

namespace Create.Elements.BlockClasses;

public class Bedrock : Block
{
    private BlockTexture _texture;

    protected override void OnElementRegistered()
    {
        _texture = AssetManager.Find<BlockTexture>("create:bedrock").AsSet;
    }

    public override BlockTexture GetTexture(in GetTextureArgs args) => _texture;
}