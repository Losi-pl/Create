using Create.OpenGL;
using Create.Space;
using Create.Virtuals;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Create.Render;


public sealed class ChunkConstructor : ModelConstructor
{
    DimentionSpace dimention;
    ChunkPoz poz;
    FinischedChunkModel? model;
    bool finish;

    public ChunkConstructor(DimentionSpace dimention, ChunkPoz poz)
    {
        this.dimention = dimention;
        this.poz = poz;
    }

    public static FinischedChunkModel GenerateModel(DimentionSpace dimention, ChunkPoz chunk)
    {
        ChunkConstructor hc = new(dimention, chunk);
        hc.CalculateModel();
        return hc.model!;
    }

    public void CalculateModel()
    {
        if (finish)
            return;
        finish = true;
        var need_r = dimention.get_chunk(poz)?.quards_content;
        if (need_r == null)
        {
            model = new(new Dictionary<Type, Mesh>[Chunk.QUARD_STACK], poz);
            return;
        }
        World w = new DimentionSpace.DimentionWorldFaster(dimention, poz);
        Dictionary<Type, Mesh>[] models  = new Dictionary<Type, Mesh>[Chunk.QUARD_STACK];
        for (int q = 0; q < Chunk.QUARD_STACK; q++)
        {
            if (need_r![q] == 0)
            {
                models[q] = new();
                continue;
            }
            int Y = q * Chunk.QUARD_SIZE;
            for (int x = 0; x < Chunk.QUARD_SIZE; x++)
                for (int z = 0; z < Chunk.QUARD_SIZE; z++)
                    for (int y = 0; y < Chunk.QUARD_SIZE; y++)
                    {
                        var poz = ((this.poz.X * Chunk.QUARD_SIZE) + x, Y + y, (this.poz.Z * Chunk.QUARD_SIZE) + z);
                        var bl = w.GetBlock(poz);
                        bl.Block.GenerateModel(new()
                        {
                            pozition = poz,
                            block = bl,
                            world = w
                        }, this);
                    }
            Dictionary<Type, Mesh> quard = new();
            foreach (var elem in Models)
                quard.Add(elem.Key, elem.Value.FinischModel());
            Models.Clear();
            models[q] = quard;
        }
        model = new(models, poz);
    }
    public ChunkPoz CurrentChunk => poz;
    public DimentionSpace Dimention => dimention;

    public class FinischedChunkModel
    {
        Dictionary<Type, Mesh>[] models = new Dictionary<Type, Mesh>[Chunk.QUARD_STACK];
        ChunkPoz pozition;
        internal FinischedChunkModel(Dictionary<Type, Mesh>[] models, ChunkPoz pozition)
        {
            this.models = models;
            this.pozition = pozition;
        }

        public ChunkPoz Pozition => pozition;
        /// <summary>
        /// This element is very unoptimized and it is recommended to use it as rarely as possible
        /// </summary>
        public VirtualList<VirtualDictionaty<Type, Mesh>> ModelParts => VirtualList.Create<VirtualDictionaty<Type, Mesh>>()
            .CountMethod(() => models.Length)
            .EnumerableMethod(() => ((IEnumerable<Dictionary<Type, Mesh>>)models).ConvertAll(d => VirtualDictionaty.Create(d).Finsh()))
            .IsContainMethod(d => false)
            .GetMethod(i => VirtualDictionaty.Create(models[i]).Finsh())
            .Finish();
    }
}
