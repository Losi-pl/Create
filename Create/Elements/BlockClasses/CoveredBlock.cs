using System.Drawing;
using Create.Assets;
using Create.Graphics.Block;
using Create.Registry;

namespace Create.Elements.BlockClasses;

public class CoveredBlock : Block
{
    private IBlockModelFace? _side, _top;

    public (IBlockModelFace Bottom, IBlockModelFace Side, IBlockModelFace Top)? TextureSet
    {
        get => MainTexture is { } bottom ? (bottom, _side!, _top!) : null;
        set => (MainTexture, _side, _top) = value ?? (null, null, null)!;
    }

    public static (IBlockModelFace Bottom, IBlockModelFace Side, IBlockModelFace Top) StandardTextureSet(RefElementIdent @base, RefElementIdent coveringSide, RefElementIdent coveringTop, Color coveringColor, Color? baseColor = null)
    {
        var baseTex = AssetManager.Find<BlockTexture>(@base) is { IsSet: true, AsSet: var set1 } ? set1 : BlockTexture.NULL;
        var sideTex = AssetManager.Find<BlockTexture>(coveringSide) is { IsSet: true, AsSet: var set2 } ? set2 : BlockTexture.NULL;
        var topTex = AssetManager.Find<BlockTexture>(coveringTop) is { IsSet: true, AsSet: var set3 } ? set3 : BlockTexture.NULL;

        return (baseColor.HasValue ? new ColoredTextureFace(baseTex, baseColor.Value) : new SingleTextureFace(baseTex),
            new DoubleColoredTextureFace(sideTex, baseTex, coveringColor, baseColor ?? Color.White),
            new ColoredTextureFace(topTex, coveringColor));
    }

    public override IBlockModelFace GetTexture(in GetTextureArgs args)
    {
        if (MainTexture is null)
            return NO_TEXTURE;
        if(args.Direction is GeneralDirection.Top)
            return _top!;
        if(args.Direction is GeneralDirection.Bottom)
            return MainTexture;
        return _side!;
    }
}