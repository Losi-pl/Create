using Create.Elements;
using Create.Storage;
using static Create.World.IChunk;

namespace Create.World;

public sealed class RealmWorld: IWorld
{
    Dictionary<ChunkPos, IChunk> _chunks = new();
    
    public bool IsChunkLoaded(ChunkPos chunkPos) => _chunks.ContainsKey(chunkPos);

    public void CreateChunk(ChunkPos chunkPos)
    {
        if(IsChunkLoaded(chunkPos))
            return;

        var bedrock = new PlacedBlock(Blocks.Bedrock);
        var stone = new PlacedBlock(Blocks.Stone);
        var dirt = new PlacedBlock(Blocks.Dirt);
        var grass = new PlacedBlock(Blocks.GrassyDirt);
        
        IChunk chunk = new Chunk256();
        foreach (var x in CHUNK_CUBE_SIZE)
            foreach (var z in CHUNK_CUBE_SIZE)
            {
                chunk[x, 0, z] = bedrock;
                foreach (var y in 10)
                    chunk[x, 1 + y, z] = stone;
                foreach (var y in 3)
                    chunk[x, 11 + y, z] = dirt;
                chunk[x, 14, z] = grass;
            }
        _chunks[chunkPos] = chunk;

    }
    
    public PlacedBlock this[long x, long y, long z]
    {
        get
        {
            if(y is < 0 or >= CHUNK_HEIGHT)
                return new(Blocks.Air);
            
            var inChunk = ChunkPos.PerChunkOperation(new(x, y, z), out var chunkPoz);
            return _chunks.TryGetValue(chunkPoz, out var chunk) ? chunk[inChunk.X, inChunk.Y, inChunk.Z] : new(Blocks.Stone);
        }
        set
        {
            if(y is < 0 or >= CHUNK_HEIGHT)
                return;
            
            var inChunk = ChunkPos.PerChunkOperation(new(x, y, z), out var chunkPoz);
            if(!_chunks.TryGetValue(chunkPoz, out var chunk))
                return;
            
            chunk[inChunk.X, inChunk.Y, inChunk.Z] = value;
        }
    }

    LongRange IWorld.RangeByY => new(0, CHUNK_HEIGHT);
    LongRange IWorld.RangeByX => new(uint.MinValue * (long)CHUNK_CUBE_SIZE, uint.MaxValue * (long)CHUNK_CUBE_SIZE);
    LongRange IWorld.RangeByZ => new(uint.MinValue * (long)CHUNK_CUBE_SIZE, uint.MaxValue * (long)CHUNK_CUBE_SIZE);
}