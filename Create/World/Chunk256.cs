using System.Runtime.CompilerServices;
using Create.Storage;
using static Create.World.IChunk;

namespace Create.World;

public class Chunk256 : IChunk
{
    // ReSharper disable FieldCanBeMadeReadOnly.Local
    private BlockIndexes _indexes;
    private PlacedBlocks _blocks;
    private BlockCounts _counts;
    // ReSharper restore FieldCanBeMadeReadOnly.Local

    private ushort _uniqueBlocks;

    public Chunk256()
    {
        _counts[0] = CHUNK_CUBE_SIZE * CHUNK_HEIGHT * CHUNK_CUBE_SIZE;
        _uniqueBlocks = 1;
    }
    
    public PlacedBlock this[int x, int y, int z]
    {
        get
        {
            var ind = _indexes[ToIndex(x, y, z)];
            return _blocks[ind];
        }
        set
        {
            var blInd = ToIndex(x, y, z);
            if(_blocks[_indexes[blInd]] == value)
                return;
            var ind = _indexes[blInd];
            _counts[ind] -= 1;
            if (_counts[ind] == 0)
                _uniqueBlocks--;
            foreach (var val in 256)
                if (_blocks[val] == value)
                {
                    if(_counts[val] == 0)
                        continue;
                    
                    _indexes[blInd] = (byte)val;
                    _counts[val]++;
                    return;
                }

            foreach (var val in 256)
            {
                if(_counts[val] > 0)
                    continue;
                _blocks[val] = value;
                _counts[val] = 1;
                _indexes[blInd] = (byte)val;
                _uniqueBlocks++;
                return;
            }
        }
    }

    private int ToIndex(int x, int y, int z) => 
        x is >= 0 and < CHUNK_CUBE_SIZE && 
        y is >= 0 and < CHUNK_HEIGHT && 
        z is >= 0 and < CHUNK_CUBE_SIZE ? 
            x + (z * CHUNK_CUBE_SIZE) + (y * CHUNK_CUBE_SIZE * CHUNK_CUBE_SIZE) : 
            throw new IndexOutOfRangeException();
    
    [InlineArray(256)]
    private struct PlacedBlocks { private PlacedBlock block; }
    
    [InlineArray(256)]
    private struct BlockCounts { private uint count; }
    
    [InlineArray(CHUNK_CUBE_SIZE * CHUNK_HEIGHT * CHUNK_CUBE_SIZE)]
    private struct BlockIndexes { private byte position; }
}

