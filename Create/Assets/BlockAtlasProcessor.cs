using System.Drawing;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;
using Create.Graphics;
using Create.Registry;
using Silk.NET.Core;
using Silk.NET.Maths;

namespace Create.Assets;

internal class BlockAtlasProcessor: IResourceProcessor<BlockTexture>
{
    // ReSharper disable InconsistentNaming
    public const string ASSET_PATH = "textures/blocks/";
    // ReSharper restore InconsistentNaming
    
    private FrozenElementDictionary<(int Index, string Name)>? _textures;
    
    public void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest)
    {
        var used = IResourceProcessor.FlattenSources(fileManifest, [IMod.Mods["create"].Resources]);
        ElementDictionary<(int Index, string name)> textureIndexes = new();

        var atlas = Texture2DAtlas.Create();
        var lasIndex = 0;
        atlas.SuppressExceptions((atlas[0] = NullImage()).Value);
        atlas.SpecifyImageSize(BlockTexture.PIXEL_SIZE, BlockTexture.PIXEL_SIZE);
        
        foreach (var file in used)
        {
            var stream = file.Value.GetStream($"{file.Key.mod.Identity}/{ASSET_PATH}{file.Key.path}")!;

            var name = file.Key.path[..file.Key.path.LastIndexOf('.')];
            
            atlas[++lasIndex] = (stream, true);
            textureIndexes[(file.Key.mod, name)] = (lasIndex, name);
        }

        var done = atlas.Finish();
        foreach (var (index, error) in done.TakeOutErrors(true))
        {
            var ident = textureIndexes.First(kvp => kvp.Value.Index == index);
            Console.WriteLine($"Texture {{{ident.Key}}} failed to load properly:\n{error}");
        }

        _textures = textureIndexes.ToFrozenDictionary();
        BlockTexture.Atlas = done;
    }
    
    public void ClearResources() { }
    
    public PossibleResult<BlockTexture> Find(RefElementIdent identity)
    {
        if (_textures is null)
            return new None();

        if (_textures.TryGetValue(identity, out var texture))
            return new BlockTexture(texture.Index, identity.Mod, texture.Name);

        return BlockTexture.NULL;
    }
    
    private static RawImage NullImage()
    {
        var black = Color.Black.AsVector();
        var magenta = Color.Magenta.AsVector();
        
        var data = new byte[BlockTexture.PIXEL_SIZE * BlockTexture.PIXEL_SIZE * 4];
        var image = MemoryMarshal.Cast<byte, Vector4D<byte>>(data.AsSpan()).AsSpan2D(BlockTexture.PIXEL_SIZE, BlockTexture.PIXEL_SIZE);

        for (var y = 0; y < BlockTexture.PIXEL_SIZE; y++)
        for(var x = 0; x < BlockTexture.PIXEL_SIZE; x++)
            image[y, x] = black;
        
        for (var y = BlockTexture.PIXEL_SIZE / 2; y < BlockTexture.PIXEL_SIZE; y++)
        for(var x = 0; x < BlockTexture.PIXEL_SIZE / 2; x++)
            image[x, y] = image[y, x] = magenta;
        
        return new(BlockTexture.PIXEL_SIZE, BlockTexture.PIXEL_SIZE, data);
    }
}