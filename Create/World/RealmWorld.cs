namespace Create.World;

public sealed class RealmWorld: IWorld
{
    // ReSharper disable MemberCanBePrivate.Global, InconsistentNaming
    public const int CHUNK_CUBE_SIZE = 16;
    public const int CHUNK_CUBE_STACK = 16;
    public const int CHUNK_HEIGHT = CHUNK_CUBE_SIZE * CHUNK_CUBE_STACK;
    // ReSharper restore MemberCanBePrivate.Global, InconsistentNaming

    private readonly bool[,,] _tmpBlocks = new bool[CHUNK_CUBE_SIZE, CHUNK_CUBE_SIZE, CHUNK_CUBE_SIZE];
    
    public bool this[long x, long y, long z]
    {
        get
        {
            if(x is >= CHUNK_CUBE_SIZE or < 0)
                return false;
            if(y is >= CHUNK_CUBE_SIZE or < 0)
                return false;
            if(z is >= CHUNK_CUBE_SIZE or < 0)
                return false;
            return _tmpBlocks[x, y, z];
        }
        set
        {
            if(x is >= CHUNK_CUBE_SIZE or < 0)
                return;
            if(y is >= CHUNK_CUBE_SIZE or < 0)
                return;
            if(z is >= CHUNK_CUBE_SIZE or < 0)
                return;
            _tmpBlocks[x, y, z] = value;
        }
    }

    Silk.NET.Direct3D12.RangeUint64 IWorld.RangeByY => new(0, CHUNK_HEIGHT);
    Silk.NET.Direct3D12.RangeUint64 IWorld.RangeByX => new(uint.MinValue * (long)CHUNK_CUBE_SIZE, uint.MaxValue * (long)CHUNK_CUBE_SIZE);
    Silk.NET.Direct3D12.RangeUint64 IWorld.RangeByZ => new(uint.MinValue * (long)CHUNK_CUBE_SIZE, uint.MaxValue * (long)CHUNK_CUBE_SIZE);
}