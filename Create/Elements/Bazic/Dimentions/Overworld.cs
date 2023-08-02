using Create.Noise;
using Create.Space;

namespace Create.Elements.Bazic.Dimentions;

internal sealed class Overworld : Dimention
{
    ValueNoise noise = new((new Random()).Next(0, int.MaxValue), 10);

    public override void GenerateChunk(GenerateChunkStruct args)
    {
        (int x, int z) hunk_abs = (Chunk.QUARD_SIZE * args.pozition.X, Chunk.QUARD_SIZE * args.pozition.Z);
        for (int x = 0; x < Chunk.QUARD_SIZE; x++)
            for (int z = 0; z < Chunk.QUARD_SIZE; z++)
                args.chunk[x, 0, z] = new(Elements.Blocks.BEDROCK);
        for (int x = 0; x < Chunk.QUARD_SIZE; x++)
            for (int z = 0; z < Chunk.QUARD_SIZE; z++)
            {
                var h = noise.Sample2D((hunk_abs.x + x) * .006f, (hunk_abs.z + z) * .006f);
                int _h = (int)(MathF.Abs(h * 5)) + 3;
                for(int i = 0; i<_h;i++)
                    args.chunk[x, 1 + i, z] = new(Elements.Blocks.STONE);
                for(int i = 0; i<3;i++)
                    args.chunk[x, 1 + _h + i, z] = new(Elements.Blocks.DIRT);
                args.chunk[x, 4 + _h, z] = new(Elements.Blocks.GRASS_BLOCK);
            }

        if (args.pozition == new ChunkPoz())
        {
            for (int i = 1; i < 30; i++)
                args.chunk[5, i, 5] = new(Elements.Blocks.AIR);
            var poi = GetNewSpawnPoint();
            poi.y += 20;
            args.chunk[poi.x, poi.y, poi.z] = new(Elements.Blocks.STONE);
        }
    }
    public override (int x, int y, int z) GetNewSpawnPoint()
    {
        (int x, int z) map_point = (Chunk.QUARD_SIZE / 2, Chunk.QUARD_SIZE / 2);
        var h = (int)MathF.Abs(noise.Sample2D(map_point.x * .006f, map_point.z * .006f) * 5) + 8;
        return (map_point.x, h + 2, map_point.z);
    }
}
