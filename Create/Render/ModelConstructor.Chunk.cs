using Create.Linq;
using Create.OpenGL;
using Create.Space;
using Create.Virtuals;
using OpenTK.Mathematics;

namespace Create.Render;

/// <summary>
/// Generator modelu terenu oparty o mechanike Chunków
/// </summary>
partial class ModelConstructor
{
    /// <summary>
    /// Generuje model sześcianu chunka ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/> i o szescianie numer <paramref name="quard"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <param name="quard">Numer sześcianu od dołu do wygenerowania</param>
    /// <returns></returns>
    public static WorldModel ChunkModel(DimentionSpace dimention, ChunkPoz chunk, int quard)
    {
        var chunk_s = dimention.get_chunk(chunk);
        World world = new DimentionSpace.DimentionWorldFaster(dimention, chunk);
        (int s, int e) xRange = (chunk.X * Chunk.QUARD_SIZE, ((chunk.X + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) zRange = (chunk.Z * Chunk.QUARD_SIZE, ((chunk.Z + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) yRange = (quard * Chunk.QUARD_SIZE, ((quard + 1) * Chunk.QUARD_SIZE) - 1);
        if (chunk_s?.quards_content[quard] == 0)
            return new(new());
        else
            return WorldModel(world, xRange, yRange, zRange);
    }

    /// <summary>
    /// Generuje model całego chunka z ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <returns></returns>
    public static FinischedChunkModel ChunkModel(DimentionSpace dimention, ChunkPoz chunk)
    {
        var need_r = dimention.get_chunk(chunk)?.quards_content;
        if (need_r == null)
            return new(new WorldModel[Chunk.QUARD_STACK], chunk);
        World w = new DimentionSpace.DimentionWorldFaster(dimention, chunk);
        WorldModel[] models = new WorldModel[Chunk.QUARD_STACK];
        (int s, int e) xRange = (chunk.X * Chunk.QUARD_SIZE, ((chunk.X + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) zRange = (chunk.Z * Chunk.QUARD_SIZE, ((chunk.Z + 1) * Chunk.QUARD_SIZE) - 1);
        for (int q = 0; q < Chunk.QUARD_STACK; q++)
        {
            (int s, int e) yRange = (q * Chunk.QUARD_SIZE, ((q + 1) * Chunk.QUARD_SIZE) - 1);
            if (need_r![q] == 0)
                models[q] = new(new Dictionary<Type, Mesh>());
            else
                models[q] = WorldModel(w, xRange, yRange, zRange);
        }
        return new(models, chunk);
    }

    /// <summary>
    /// Generuje model sześcianu chunka ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/> i o szescianie numer <paramref name="quard"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <param name="quard">Numer sześcianu od dołu do wygenerowania</param>
    /// <returns></returns>
    public static async Task<WorldModel> ChunkModelAsync(DimentionSpace dimention, ChunkPoz chunk, int quard)
    {
        var chunk_s = dimention.get_chunk(chunk);
        World world = new DimentionSpace.DimentionWorldFaster(dimention, chunk);
        (int s, int e) xRange = (chunk.X * Chunk.QUARD_SIZE, ((chunk.X + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) zRange = (chunk.Z * Chunk.QUARD_SIZE, ((chunk.Z + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) yRange = (quard * Chunk.QUARD_SIZE, ((quard + 1) * Chunk.QUARD_SIZE) - 1);
        if (chunk_s?.quards_content[quard] == 0)
            return new(new());
        else
            return await WorldModelAsync(world, xRange, yRange, zRange);
    }

    /// <summary>
    /// Generuje model całego chunka z ze świata <paramref name="dimention"/> o pozycji <paramref name="chunk"/>
    /// </summary>
    /// <param name="dimention">Świat źrudłowy</param>
    /// <param name="chunk">Pozycja chunka</param>
    /// <returns></returns>
    public static async Task<FinischedChunkModel> ChunkModelAsync(DimentionSpace dimention, ChunkPoz chunk)
    {
        var need_r = dimention.get_chunk(chunk)?.quards_content;
        if (need_r == null)
            return new(new WorldModel[Chunk.QUARD_STACK], chunk);
        World w = new DimentionSpace.DimentionWorldFaster(dimention, chunk);
        WorldModel[] models = new WorldModel[Chunk.QUARD_STACK];
        (int s, int e) xRange = (chunk.X * Chunk.QUARD_SIZE, ((chunk.X + 1) * Chunk.QUARD_SIZE) - 1);
        (int s, int e) zRange = (chunk.Z * Chunk.QUARD_SIZE, ((chunk.Z + 1) * Chunk.QUARD_SIZE) - 1);
        for (int q = 0; q < Chunk.QUARD_STACK; q++)
        {
            (int s, int e) yRange = (q * Chunk.QUARD_SIZE, ((q + 1) * Chunk.QUARD_SIZE) - 1);
            if (need_r![q] == 0)
                models[q] = new(new Dictionary<Type, Mesh>());
            else
                models[q] = await WorldModelAsync(w, xRange, yRange, zRange);
        }
        return new(models, chunk);
    }
}

/// <summary>
/// Gotowy model całego chunka
/// </summary>
public class FinischedChunkModel : IDrawable, IDisposable
{
    WorldModel[] models;
    ChunkPoz pozition;
    internal FinischedChunkModel(WorldModel[] models, ChunkPoz pozition)
    {
        this.models = models;
        this.pozition = pozition;
    }

    /// <summary>
    /// Pozycja chunka w świecie
    /// </summary>
    public ChunkPoz Pozition => pozition;

    public VirtualList<WorldModel> ModelParts => VirtualList.Create(models).Finish();

    /// <summary>
    /// Ustawia cześcian modelu w chunku
    /// </summary>
    /// <param name="model">Części modelu sześcianu</param>
    /// <param name="poz">Gdzie ten element się znajduje</param>
    internal void set_new_quard(WorldModel model, int poz)
    {
        models[poz]?.Dispose();
        models[poz] = model;
    }

    public void Dispose()
    {
        foreach (var m in models)
            m?.Dispose();
    }

    /// <summary>
    /// Rysuje model
    /// </summary>
    /// <param name="projection"></param>
    /// <param name="model"></param>
    public void Draw(Matrix4 projection, Matrix4 model)
    {
        for (int i = 0; i < models.Length; i++)
        {
            Vector3 point = new Vector3(Chunk.QUARD_SIZE * pozition.X, Chunk.QUARD_SIZE * i, Chunk.QUARD_SIZE * pozition.Z) +
                (new Vector3(Chunk.QUARD_SIZE, Chunk.QUARD_SIZE, Chunk.QUARD_SIZE) / 2);
            point *= new Vector3(1, 1, -1);

            if (MathC.InView(model * projection, (point.ToTumple(), (Chunk.QUARD_SIZE, Chunk.QUARD_SIZE, Chunk.QUARD_SIZE))))
                models[i]?.Draw(projection, model);
        }
    }
}