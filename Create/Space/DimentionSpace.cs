using Create.Conteiner;
using Create.Elements;
using Create.Linq;
using System.Numerics;

namespace Create.Space;

public sealed class DimentionSpace
{
    Dimention dimention;
    Dictionary<ChunkPoz, Chunk> chunks = new();
    DimentionWorld world;
    List<LivingEntity> entities = new();
    List<ChunkPoz> changet_chunks = new();
    List<ChunkPoz> loadet_chunks = new();

    internal DimentionSpace(Dimention dimention)
    {
        this.dimention = dimention;
        world = new(this);
    }

    /// <summary>
    /// Mechanizm do modyfikowania terenu w danym wymiarze
    /// </summary>
    public World World => world;
    
    /// <summary>
    /// Parametry i statystyki danego świata
    /// </summary>
    public Dimention Dimention => dimention;
    
    /// <summary>
    /// Dodaj przez wygenerowanie albo załadowanie <see cref="Chunk"/>a z plików do świata
    /// </summary>
    /// <param name="position"></param>
    internal void add_chunk(ChunkPoz position)
    {
        if(is_chunk_saved(position))
        {
            //Load from files
        }
        else
        {
            lock (loadet_chunks)
            {
                if (loadet_chunks.Contains(position))
                    return;
                CancellationTokenSource token = new(TimeSpan.FromMinutes(2));
                var t = Task.Run(() =>
                {
                    Chunk ch = new();
                    dimention.GenerateChunk(new() { chunk = ch, pozition = position });
                    return ch;
                }, token.Token).ContinueWith((Task<Chunk> task) =>
                {
                    lock (loadet_chunks)
                        loadet_chunks.Remove(position);
                    lock (chunks)
                        chunks.Add(position, task.Result);
                });
            }
        }
    }
    
    /// <summary>
    /// Zwróć chunk jeżeli znajduje on się w pamięci
    /// </summary>
    /// <param name="poz"></param>
    /// <returns>Jeżali zwraca <see langword="null"/>, <see cref="Chunk"/> nie znajduje się w pamięci</returns>
    internal Chunk? get_chunk(ChunkPoz poz)
    {
        lock (chunks)
        {
            if (chunks.TryGetValue(poz, out var chunk))
                return chunk;
        }
        return null;
    }

    /// <summary>
    /// Sprawdza czy <see cref="Chunk"/> jest załadowany do pamięci
    /// </summary>
    /// <param name="chunk"></param>
    /// <returns></returns>
    public bool IsChunkLoadet(ChunkPoz chunk)
    {
        lock(chunks)
            return chunks.ContainsKey(chunk);
    }

    /// <summary>
    /// Sprawdza czy <see cref="Chunk"/> jest w procesie ładowania
    /// </summary>
    public bool IsChunkLoading(ChunkPoz chunk)
    {
        lock (loadet_chunks)
            return loadet_chunks.Contains(chunk);
    }

    /// <summary>
    /// Sprawdza czy <see cref="Chunk"/> jest załadowany do pamięci albo się ładuje
    /// </summary>
    public bool IsChunkLoadetOrLoading(ChunkPoz chunk) => IsChunkLoadet(chunk) || IsChunkLoading(chunk);

    /// <summary>
    /// Ma sprawdzać czy <see cref="Chunk"/> został zapisany w plikach
    /// </summary>
    /// <param name="poz"></param>
    /// <returns></returns>
    bool is_chunk_saved(ChunkPoz poz) => false; // Testing if chunk is alredy saved
    
    /// <summary>
    /// Umieszcza instancje na płaszczyźnie świata
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="pozition">Pozycja w świecie gdze byt zostanie umieszczony</param>
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
    
    /// <summary>
    /// Usunie instancje z płaszczyzny świata bez niszczenia go
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveFromPlatform(LivingEntity entity)
    {
        if (entity.Dimention == null)
            return;
        entities.Remove(entity);
        entity.set_diment(null);
        entity.remove_chunk();
    }
    
    /// <summary>
    /// Gdy <see cref="Chunk"/> na kturym stoji instancja się zmieni
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="new_chunk"></param>
    internal void change_entity_chunk(LivingEntity entity, ChunkPoz new_chunk)
    {
        if(chunks.TryGetValue(entity.Chunk, out var chunk))
            chunk.local_entitys.Remove(entity);
        if (chunks.TryGetValue(new_chunk, out chunk))
            chunk.local_entitys.Add(entity);
    }
    
    /// <summary>
    /// Oblicza w którym <see cref="Chunk"/>u blok o współrzędnych (<paramref name="x"/>, y, <paramref name="z"/>) się znajduje
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    internal static ChunkPoz calculate_chunk_pozition(int x, int z) => new()
    {
        X = MathC.Section(x, Chunk.QUARD_SIZE),
        Z = MathC.Section(z, Chunk.QUARD_SIZE)
    };
    
    /// <summary>
    /// Kolekcja wrzystkich instancji bytów na tym świecie
    /// </summary>
    public IEnumerable<LivingEntity> AllEntities => entities;
    
    /// <summary>
    /// Kolekcja wrzystkich sześcianów w <see cref="Chunk"/>ach zostały zmienione od ostatniego sprawdzenia
    /// </summary>
    /// <returns></returns>
    internal IEnumerable<(ChunkPoz chunk, uint quard)> get_changet_chunks()
    {
        lock (changet_chunks)
        {
            if(changet_chunks.Count == 0)
                return Enumerable.Empty<(ChunkPoz chunk, uint quard)>();
            return result();
        }
        IEnumerable<(ChunkPoz chunk, uint quard)> result()
        {
            lock(chunks)
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
    }
    
    /// <summary>
    /// Kolekcja <see cref="ChunkPoz"/> wrzystkich załadowanych <see cref="Chunk"/>ów w tym świecie
    /// </summary>
    public IEnumerable<ChunkPoz> LoadetChunks => chunks.Keys.Secure();

    /// <summary>
    /// Kolekcja <see cref="ChunkPoz"/> wrzystkich <see cref="Chunk"/>ów w procesie ładowania
    /// </summary>
    public IEnumerable<ChunkPoz> ChunksDurringLoading => loadet_chunks.Secure();
    
    /// <summary>
    /// Kolekcja <see cref="ChunkPoz"/> wrzystkich <see cref="Chunk"/>ów załadowanych lub w procesie ładowania
    /// </summary>
    public IEnumerable<ChunkPoz> ProcessedChunks => chunks.Keys.Concat(loadet_chunks);
    
    /// <summary>
    /// Tworzy i umieszcza instancje bytu w tym świecie
    /// </summary>
    /// <param name="entity">Typ bytu</param>
    /// <param name="pozition">Pozycja w świecie</param>
    /// <param name="specialArgs">Dodatkowy parametr tworzenia bytu</param>
    /// <returns></returns>
    public LivingEntity Spawn(Entity entity, Vector3 pozition, object? specialArgs = null)
    {
        var ent = new LivingEntity(entity);
        PlaceOnPlatform(ent, pozition);
        ent.Entity.OnSpawn(ent, specialArgs);
        return ent;
    }
    public static explicit operator World(DimentionSpace ds) => ds.world;

    /// <summary>
    /// Teren połączony z danym światem
    /// </summary>
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
            set(chunk_poz, (ib.Item1, y, ib.Item2), block);
            if(ib.Item1 == 0)
            {
                forced_update(chunk_poz + new ChunkPoz(-1, 0));
                if(ib.Item2 == 0)
                    forced_update(chunk_poz + new ChunkPoz(-1, -1));
                if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                    forced_update(chunk_poz + new ChunkPoz(-1, 1));
            }
            if (ib.Item1 == Chunk.QUARD_SIZE - 1)
            {
                forced_update(chunk_poz + new ChunkPoz(1, 0));
                if (ib.Item2 == 0)
                    forced_update(chunk_poz + new ChunkPoz(1, -1));
                if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                    forced_update(chunk_poz + new ChunkPoz(1, 1));
            }
            if (ib.Item2 == 0)
                forced_update(chunk_poz + new ChunkPoz(0, -1));
            if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                forced_update(chunk_poz + new ChunkPoz(0, 1));

            void set(ChunkPoz chunk, (int x, int y, int z) block_poz, PlacedBlock block)
            {
                lock (dimentionSpace.chunks)
                    if (dimentionSpace.chunks.TryGetValue(chunk, out var chunk_))
                    {
                        chunk_[block_poz.x, block_poz.y, block_poz.z] = block;
                        if (!dimentionSpace.changet_chunks.Contains(chunk))
                            dimentionSpace.changet_chunks.Add(chunk);
                    }
            }
            void forced_update(ChunkPoz chunk)
            {
                lock (dimentionSpace.chunks)
                {
                    if (dimentionSpace.chunks.TryGetValue(chunk, out var chunk_))
                        chunk_.modyfication(y, true);
                    if (!dimentionSpace.changet_chunks.Contains(chunk))
                    dimentionSpace.changet_chunks.Add(chunk);
                }
            }
        }
    }
    
    /// <summary>
    /// Teren połączony z danym światem ale zoptymalizowany na urzywanie kąkretnego chunka
    /// </summary>
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
            set(chunk_poz, (ib.Item1, y, ib.Item2), block);
            if (ib.Item1 == 0)
            {
                forced_update(chunk_poz + new ChunkPoz(-1, 0));
                if (ib.Item2 == 0)
                    forced_update(chunk_poz + new ChunkPoz(-1, -1));
                if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                    forced_update(chunk_poz + new ChunkPoz(-1, 1));
            }
            if (ib.Item1 == Chunk.QUARD_SIZE - 1)
            {
                forced_update(chunk_poz + new ChunkPoz(1, 0));
                if (ib.Item2 == 0)
                    forced_update(chunk_poz + new ChunkPoz(1, -1));
                if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                    forced_update(chunk_poz + new ChunkPoz(1, 1));
            }
            if (ib.Item2 == 0)
                forced_update(chunk_poz + new ChunkPoz(0, -1));
            if (ib.Item2 == Chunk.QUARD_SIZE - 1)
                forced_update(chunk_poz + new ChunkPoz(0, 1));

            void set(ChunkPoz chunk, (int x, int y, int z) block_poz, PlacedBlock block)
            {
                lock (dimentionSpace.chunks)
                    if (get_chunk(chunk, out var chunk_))
                    {
                        chunk_[block_poz.x, block_poz.y, block_poz.z] = block;
                        if (!dimentionSpace.changet_chunks.Contains(chunk))
                            dimentionSpace.changet_chunks.Add(chunk);
                    }
            }
            void forced_update(ChunkPoz chunk)
            {
                lock (dimentionSpace.chunks)
                {
                    if (get_chunk(chunk, out var chunk_))
                        chunk_.modyfication(y, true);
                    if (!dimentionSpace.changet_chunks.Contains(chunk))
                        dimentionSpace.changet_chunks.Add(chunk);
                }
            }
        }
    }
}
