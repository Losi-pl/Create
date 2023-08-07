using Create.Noise;
using Create.Space;

namespace Create.Elements.Bazic.Dimentions;

internal sealed class Overworld : Dimention
{
    PerlinNoise noise = new((new Random()).Next(0, int.MaxValue), 10);
    PerlinNoise aptitude = new((new Random()).Next(0, int.MaxValue), 120);
    PerlinNoise temperature = new((new Random()).Next(0, int.MaxValue), 120);

    public override void GenerateChunk(GenerateChunkStruct args)
    {
        (int x, int z) hunk_abs = (Chunk.QUARD_SIZE * args.pozition.X, Chunk.QUARD_SIZE * args.pozition.Z);
        for (int x = 0; x < Chunk.QUARD_SIZE; x++)
            for (int z = 0; z < Chunk.QUARD_SIZE; z++)
                args.chunk[x, 0, z] = new(Elements.Blocks.BEDROCK);
        for (int x = 0; x < Chunk.QUARD_SIZE; x++)
            for (int z = 0; z < Chunk.QUARD_SIZE; z++)
            {
                var _h = terrain_heigth(hunk_abs.x + x, hunk_abs.z + z);
                for(int i = 0; i<_h;i++)
                    args.chunk[x, 1 + i, z] = new(Elements.Blocks.STONE);
                for(int i = 0; i<3;i++)
                    args.chunk[x, 1 + _h + i, z] = new(Elements.Blocks.DIRT);
                args.chunk[x, 4 + _h, z] = new(Elements.Blocks.GRASS_BLOCK);
            }
    }
    public override (int x, int y, int z) GetNewSpawnPoint()
    {
        (int x, int z) map_point = (Chunk.QUARD_SIZE / 2, Chunk.QUARD_SIZE / 2);
        return (map_point.x, terrain_heigth(map_point.x, map_point.z) + 5, map_point.z);
    }

    int terrain_heigth(int x, int z)
    {
        var h = point(x - 1, z + 1) + point(x, z + 1) + point(x + 1, z + 1) +
                point(x - 1, z) +     point(x, z) +     point(x + 1, z) +
                point(x - 1, z - 1) + point(x, z - 1) + point(x + 1, z - 1);
        return (int)(h / 9f);
        int point(int x, int z)
        {
            var avr = 75 + ((aptitude.Sample2D(x / 102400f, z / 102400f) + 1) / 2 * 50);
            int _h = (int)(avr + ((noise.Sample2D(x * .002f, z * .002f) + noise.Sample2D(x * .005f, z * .005f)) / 2 * 5));
            return _h;
        }
    }
}
