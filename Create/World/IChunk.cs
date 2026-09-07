using Create.Storage;

namespace Create.World;

public interface IChunk
{
    // ReSharper disable MemberCanBePrivate.Global, InconsistentNaming
    public const int CHUNK_CUBE_SIZE = 16;
    public const int CHUNK_CUBE_STACK = 16;
    public const int CHUNK_HEIGHT = CHUNK_CUBE_SIZE * CHUNK_CUBE_STACK;
    // ReSharper restore MemberCanBePrivate.Global, InconsistentNaming
    
    public PlacedBlock this[int x, int y, int z] { get; set; }
}