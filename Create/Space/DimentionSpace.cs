using Create.Conteiner;
using Create.Elements;
using System.Numerics;

namespace Create.Space;

public sealed class DimentionSpace
{
    Dimention dimention;
    Dictionary<ChunkPoz, Chunk> chunks = new();
    DimentionWorld world;
    List<LivingEntity> entities = new();
    List<ChunkPoz> changet_chunks = new();

    internal DimentionSpace(Dimention dimention)
    {
        this.dimention = dimention;
        world = new(this);
    }

    public World World => new DimentionWorld(this);
    public Dimention Dimention => dimention;
    internal void add_chunk(ChunkPoz position)
    {
        if(is_chunk_saved(position))
        {
            //Load from files
        }
        else
        {
            Chunk ch = new();
            dimention.GenerateChunk(new() { chunk = ch, pozition = position });
            chunks.Add(position, ch);
        }
    }
    internal Chunk? get_chunk(ChunkPoz poz)
    {
        if (chunks.TryGetValue(poz, out var chunk))
            return chunk;
        return null;
    }

    public bool IsChunkLoadet(ChunkPoz chunk) => chunks.ContainsKey(chunk);
    bool is_chunk_saved(ChunkPoz poz) => false; // Testing if chunk is alredy saved
    public void PlaceOnPlatform(LivingEntity entity, Vector3 pozition)
    {
        if (entity.Dimention == this)
            entity.Pozition = pozition.ToOpenGL();
        if (entity.Dimention != null)
            entity.Dimention.RemoveFromPlatform(entity);
        entities.Add(entity);
        entity.set_diment(this);
        entity.Pozition = pozition.ToOpenGL();
    }
    public void RemoveFromPlatform(LivingEntity entity)
    {
        if (entity.Dimention == null)
            return;
        entities.Remove(entity);
        entity.set_diment(null);
        entity.remove_chunk();
    }
    internal void change_entity_chunk(LivingEntity entity, ChunkPoz new_chunk)
    {
        if(chunks.TryGetValue(entity.Chunk, out var chunk))
            chunk.local_entitys.Remove(entity);
        if (chunks.TryGetValue(new_chunk, out chunk))
            chunk.local_entitys.Add(entity);
    }
    internal static ChunkPoz calculate_chunk_pozition(int x, int z) => new()
    {
        X = MathC.Section(x, Chunk.QUARD_SIZE),
        Z = MathC.Section(z, Chunk.QUARD_SIZE)
    };
    public IEnumerable<LivingEntity> AllEntities => entities;
    internal IEnumerable<(ChunkPoz chunk, uint quard)> get_changet_chunks()
    {
        lock (changet_chunks)
        {
            if(changet_chunks.Count == 0)
                return Enumerable.Empty<(ChunkPoz chunk, uint quard)>();
            return result().ToArray();
        }
        IEnumerable<(ChunkPoz chunk, uint quard)> result()
        {
            for (int i = 0; i < changet_chunks.Count; i++)
            {
                var ch = changet_chunks[i];
                foreach (var q in chunks[ch].las_modified_quards())
                    yield return (ch, (uint)q);
            }
            changet_chunks.Clear();
        }
    }
    public IEnumerable<ChunkPoz> LoadetChunks => chunks.Keys;
    public LivingEntity Spawn(Entity entity, Vector3 pozition, object? specialArgs = null)
    {
        var ent = new LivingEntity(entity);
        PlaceOnPlatform(ent, pozition);
        ent.Entity.OnSpawn(ent, specialArgs);
        return ent;
    }
    public static explicit operator World(DimentionSpace ds) => ds.world;

    class DimentionWorld : World
    {
        DimentionSpace dimentionSpace;
        public DimentionWorld(DimentionSpace space) => dimentionSpace = space;
        public sealed override object? Owner => dimentionSpace;
        public override PlacedBlock GetBlock(int x, int y, int z)
        {
            if (y < 0)
                return new();
            if (y >= Chunk.CHUNK_HEIGHT)
                return new();
            var chunk_poz = calculate_chunk_pozition(x, z);
            var ib = (x - (chunk_poz.X * Chunk.QUARD_SIZE), z - (chunk_poz.Z * Chunk.QUARD_SIZE));
            if (dimentionSpace.chunks.TryGetValue(chunk_poz, out var chunk))
                return chunk[ib.Item1, y, ib.Item2];
            return new(Blocks.STONE);
        }
        public override void SetBlock(int x, int y, int z, PlacedBlock block)
        {
            if (y < 0)
                return;
            if (y > Chunk.CHUNK_HEIGHT)
                return;
            var chunk_poz = calculate_chunk_pozition(x, z);
            var ib = (x - (chunk_poz.X * Chunk.QUARD_SIZE), z - (chunk_poz.Z * Chunk.QUARD_SIZE));
            if (dimentionSpace.chunks.TryGetValue(chunk_poz, out var chunk))
            {
                chunk[ib.Item1, y, ib.Item2] = block;
                if (!dimentionSpace.changet_chunks.Contains(chunk_poz))
                    dimentionSpace.changet_chunks.Add(chunk_poz);
            }
        }
    }
    internal class DimentionWorldFaster : World
    {
        DimentionSpace dimentionSpace;
        Chunk chunk;
        ChunkPoz chunk_pozition;
        public DimentionWorldFaster(DimentionSpace space, ChunkPoz chunk) => (dimentionSpace, this.chunk, chunk_pozition) = (space, space.chunks[chunk], chunk);
        public sealed override object? Owner => dimentionSpace;
        public override PlacedBlock GetBlock(int x, int y, int z)
        {
            if (y < 0)
                return new();
            if (y >= Chunk.CHUNK_HEIGHT)
                return new();
            var chunk_poz = calculate_chunk_pozition(x, z);
            var ib = (x - (chunk_poz.X * Chunk.QUARD_SIZE), z - (chunk_poz.Z * Chunk.QUARD_SIZE));
            if (get_chunk(chunk_poz, out var chunk))
                return chunk[ib.Item1, y, ib.Item2];
            return new(Blocks.STONE);
        }

        bool get_chunk(ChunkPoz poz, out Chunk chunk)
        {
            if(poz == chunk_pozition)
            {
                chunk = this.chunk;
                return true;
            }
            return dimentionSpace.chunks.TryGetValue(poz, out chunk!);
        }

        public override void SetBlock(int x, int y, int z, PlacedBlock block)
        {
            if (y < 0)
                return;
            if (y > Chunk.CHUNK_HEIGHT)
                return;
            var chunk_poz = calculate_chunk_pozition(x, z);
            var ib = (x - (chunk_poz.X * Chunk.QUARD_SIZE), z - (chunk_poz.Z * Chunk.QUARD_SIZE));
            if (get_chunk(chunk_poz, out var chunk))
            {
                chunk[ib.Item1, y, ib.Item2] = block;
                if (!dimentionSpace.changet_chunks.Contains(chunk_poz))
                    dimentionSpace.changet_chunks.Add(chunk_poz);
            }
        }
    }
}
