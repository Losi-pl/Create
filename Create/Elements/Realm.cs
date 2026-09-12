using Create.Registry;
using Create.World;

namespace Create.Elements;

public abstract partial class Realm : ElementBase
{
    protected Realm()
    {
        World = new(this);
    }
    
    public RealmWorld World { get; }
    
    public struct GenerateChunkArgs
    {
        public ChunkPos Position;
    }
    
    public abstract IChunk GenerateChunk(in GenerateChunkArgs args);
}