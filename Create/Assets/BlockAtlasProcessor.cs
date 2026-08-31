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
    
    private FrozenElementDictionary<(int ind, string name)>? _textures = null;
    
    public void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest)
    {
        var used = IResourceProcessor.FlattenSources(fileManifest, [IMod.Mods["create"].Resources]);
        Dictionary<IMod, List<(string name, int index)>> assetIndexes = new();

        var atlas = Texture2DAtlas.Create();
        var lasIndex = 0;
        atlas.SuppressExceptions((atlas[0] = NullImage()).Value);
        atlas.SpecifyImageSize(BlockTexture.PIXEL_SIZE, BlockTexture.PIXEL_SIZE);
        
        foreach (var file in used)
        {
            if(!assetIndexes.TryGetValue(file.Key.mod, out var modFiles))
                modFiles = assetIndexes[file.Key.mod] = [];

            var stream = file.Value.GetStream($"{file.Key.mod.Identity}/{ASSET_PATH}{file.Key.path}")!;

            var name = file.Key.path[..file.Key.path.LastIndexOf('.')];
            
            atlas[++lasIndex] = (stream, true);
            modFiles.Add((name, lasIndex));
        }

        var done = atlas.Finish();
        foreach (var (index, error) in done.TakeOutErrors(true))
        {
            var ident = assetIndexes.SelectMany(mod => mod.Value.Select(p => (p.index, mod: mod.Key, p.name)))
                .First(texture => texture.index == index);
            Console.WriteLine($"Texture {ident.mod.Identity}:{ident.name} failed to load properly:\n{error}");
        }

        BlockTexture.Atlas = done;
    }

    public void ClearResources()
    {
        
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
    
    public PossibleResult<BlockTexture> Find(string identity)
    {
        if (_textures is null)
            return new None();

        if (!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
            throw new ArgumentException("Mod not found");
        
        if(_textures.TryGetValue(identity, out var index))
            return new BlockTexture(index.ind, mod, index.name);
        
        return BlockTexture.NULL;
    }
    public PossibleResult<BlockTexture> Find(IMod source, string identity)
    {
        if (_textures is null)
            return new None();

        if(_textures.TryGetValue((source, identity), out var index))
            return new BlockTexture(index.ind, source, index.name);
        
        return BlockTexture.NULL;
    }
}