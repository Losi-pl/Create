using Create.Assets;

namespace Create.Elements.BlockClasses;

public class Dirt : Block
{
    private BlockTexture _texture;

    protected override void OnElementRegistered()
    {
        _texture = AssetManager.Find<BlockTexture>("create:dirt").AsSet;
    }

    public override BlockTexture GetTexture(ref readonly GetTextureArgs args) => _texture;
}