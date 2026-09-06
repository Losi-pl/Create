using Create.Assets;
using Create.Graphics.Block;

namespace Create.Elements.BlockClasses;

public class Stone : Block
{
    private SingleTextureFace _texture = null!;

    protected override void OnElementRegistered()
    {
        _texture = new(AssetManager.Find<BlockTexture>("create:stone").AsSet);
    }

    public override IBlockModelFace GetTexture(in GetTextureArgs args) => _texture;
}