using Create.OpenGL;
using Create.Space;
using Create.Virtuals;
using OpenTK.Mathematics;

namespace Create.Render;

/// <summary>
/// Generator modelu terenu oparty o mechanike Chunków
/// </summary>
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

    /// <summary>
    /// Generuje model całego chunka z ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <returns></returns>
    public static FinischedChunkModel GenerateModel(DimentionSpace dimention, ChunkPoz chunk)
    {
        ChunkConstructor hc = new(dimention, chunk);
        hc.CalculateModel();
        return hc.model!;
    }

    /// <summary>
    /// Generuje model sześcianu chunka ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/> i o szescianie numer <paramref name="quard"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <param name="quard">Numer sześcianu od dołu do wygenerowania</param>
    /// <returns></returns>
    public static Dictionary<Type, Mesh> QuardModel(DimentionSpace dimention, ChunkPoz chunk, int quard)
    {
        var chunk_s = dimention.get_chunk(chunk);
        World world = new DimentionSpace.DimentionWorldFaster(dimention, chunk);
        ChunkConstructor constructor = new(dimention, chunk);
        int Y = quard * Chunk.QUARD_SIZE;
        for (int x = 0; x < Chunk.QUARD_SIZE; x++)
            for (int z = 0; z < Chunk.QUARD_SIZE; z++)
                for (int y = 0; y < Chunk.QUARD_SIZE; y++)
                {
                    var poz = ((chunk.X * Chunk.QUARD_SIZE) + x, Y + y, (chunk.Z * Chunk.QUARD_SIZE) + z);
                    var bl = world.GetBlock(poz);
                    bl.Block.GenerateModel(new()
                    {
                        pozition = poz,
                        block = bl,
                        world = world
                    }, constructor);
                }
        Dictionary<Type, Mesh> quard_m = new();
        foreach (var elem in constructor.Models)
            quard_m.Add(elem.Key, elem.Value.FinischModel());
        return quard_m;
    }

    /// <summary>
    /// Przetworzenie gotowych zebranych danych terenu w modele
    /// </summary>
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
    
    /// <summary>
    /// Pozycja generowanego chunka
    /// </summary>
    public ChunkPoz CurrentChunk => poz;

    /// <summary>
    /// Świat źrudłowy dla modelu
    /// </summary>
    public DimentionSpace Dimention => dimention;

    /// <summary>
    /// Gotowy model całego chunka
    /// </summary>
    public class FinischedChunkModel : IDrawable, IDisposable
    {
        Dictionary<Type, Mesh>[] models = new Dictionary<Type, Mesh>[Chunk.QUARD_STACK];
        ChunkPoz pozition;
        internal FinischedChunkModel(Dictionary<Type, Mesh>[] models, ChunkPoz pozition)
        {
            this.models = models;
            this.pozition = pozition;
        }

        /// <summary>
        /// Pozycja chunka w świecie
        /// </summary>
        public ChunkPoz Pozition => pozition;
        
        /// <summary>
        /// Części gotowego modelu całego chunka
        /// <para>This element is very unoptimized and it is recommended to use it as rarely as possible</para>
        /// </summary>
        public VirtualList<VirtualDictionaty<Type, Mesh>> ModelParts => VirtualList.Create<VirtualDictionaty<Type, Mesh>>()
            .CountMethod(() => models.Length)
            .EnumerableMethod(() => ((IEnumerable<Dictionary<Type, Mesh>>)models).ConvertAll(d => VirtualDictionaty.Create(d).Finsh()))
            .IsContainMethod(d => false)
            .GetMethod(i => VirtualDictionaty.Create(models[i]).Finsh())
            .Finish();

        /// <summary>
        /// Ustawia cześcian modelu w chunku
        /// </summary>
        /// <param name="model">Części modelu sześcianu</param>
        /// <param name="poz">Gdzie ten element się znajduje</param>
        internal void set_new_quard(Dictionary<Type, Mesh> model, int poz)
        {
            foreach (var mod in models[poz].Values)
                mod.Dispose();
            models[poz] = model;
        }
        
        public void Dispose()
        {
            for (int i = 0; i < models.Length; i++)
                foreach (var mod in models[i].Values)
                    mod.Dispose();
        }

        /// <summary>
        /// Rysuje model
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="model"></param>
        public void Draw(Matrix4 projection, Matrix4 model)
        {
            for(int i = 0; i < models.Length; i++)
            {
                Vector3 point = new Vector3(Chunk.QUARD_SIZE * pozition.X, Chunk.QUARD_SIZE * i, Chunk.QUARD_SIZE * pozition.Z) + 
                    (new Vector3(Chunk.QUARD_SIZE, Chunk.QUARD_SIZE, Chunk.QUARD_SIZE) / 2);
                point *= new Vector3(1, 1, -1);

                if(MathC.InView(model * projection, (point.ToTumple(), (Chunk.QUARD_SIZE, Chunk.QUARD_SIZE, Chunk.QUARD_SIZE))))
                {
                    foreach (var mod in models[i].Values)
                        mod.Draw(projection, model);
                }
            }
        }
    }
}
