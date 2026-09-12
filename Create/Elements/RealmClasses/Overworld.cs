using Create.Storage;
using Create.World;

namespace Create.Elements.RealmClasses;

public class Overworld : Realm
{
    public override IChunk GenerateChunk(in GenerateChunkArgs args)
    {
        var bedrock = new PlacedBlock(Blocks.Bedrock);
        var stone = new PlacedBlock(Blocks.Stone);
        var dirt = new PlacedBlock(Blocks.Dirt);
        var grass = new PlacedBlock(Blocks.GrassyDirt);

        var dirtC = (args.Position.X % 2 == 0 ? args.Position.Z % 2 == 0 : args.Position.Z % 2 != 0) ? 3 : 4;
        
        var chunk = new Chunk256();
        foreach (var x in IChunk.CHUNK_CUBE_SIZE)
        foreach (var z in IChunk.CHUNK_CUBE_SIZE)
        {
            chunk[x, 0, z] = bedrock;
            foreach (var y in 10)
                chunk[x, 1 + y, z] = stone;
            foreach (var y in dirtC)
                chunk[x, 11 + y, z] = dirt;
            chunk[x, 11 + dirtC, z] = grass;
        }
        return chunk;
    }
}