using Create.Render;

namespace Create.Space;

public abstract class Dimention : Elements.Baze
{
    public sealed override Type ElementBazicType => typeof(Dimention);
    public abstract void GenerateChunk(GenerateChunkStruct args);
    public virtual (int x, int y, int z) GetNewSpawnPoint()
    {
        return (0, 0, 0);
    }

    public struct GenerateChunkStruct
    {
        public ChunkPoz pozition;
        public Chunk chunk;
    }
}
