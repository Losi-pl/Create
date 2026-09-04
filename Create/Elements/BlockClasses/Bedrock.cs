using Create.Assets;

namespace Create.Elements.BlockClasses;

public class Bedrock : Block
{
    private BlockTexture _texture;

    protected override void OnElementRegistered()
    {
        _texture = AssetManager.Find<BlockTexture>("create:bedrock").AsSet;
    }

    public override BlockTexture GetTexture(ref readonly GetTextureArgs args) => _texture;
}