using System.Diagnostics;
using Silk.NET.Maths;
using static Create.World.IChunk;

namespace Create.World;

[DebuggerDisplay("[{X}, {Z}]")]
public record struct ChunkPos(int X, int Z)
{
    public static ChunkPos FromWorldPosition(Vector3D<long> position)
    {
        var x = position.X >= 0 ? position.X / CHUNK_CUBE_SIZE : ((position.X + 1) / CHUNK_CUBE_SIZE) - 1;
        var z = position.Z >= 0 ? position.Z / CHUNK_CUBE_SIZE : ((position.Z + 1) / CHUNK_CUBE_SIZE) - 1;
        return new((int)x, (int)z);
    }

    public static Vector3D<int> PerChunkOperation(Vector3D<long> position, out ChunkPos chunkPos)
    {
        chunkPos = FromWorldPosition(position);
        return new((int)(position.X - chunkPos.X * CHUNK_CUBE_SIZE), (int)position.Y, (int)(position.Z - chunkPos.Z * CHUNK_CUBE_SIZE));
    }
}