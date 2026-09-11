using Create.Elements;
using Create.Storage;
using static Create.World.IChunk;

namespace Create.World;

public sealed class RealmWorld: IWorld
{
    Dictionary<ChunkPos, IChunk> _chunks = new();
    
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