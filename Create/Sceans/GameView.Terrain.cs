using Create.Elements;
using Create.Linq;
using Create.Net;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OneOf;
using OpenTK.Mathematics;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using static OneOf.Types.TrueFalseOrNull;

namespace Create.Sceans;

partial class GameView
{
    /// <summary>
    /// Mechanizm renderowania terenu
    /// </summary>
    public class Terrain
    {
        Dictionary<ChunkPoz, FinischedChunkModel> chunk_models = new();
        List<ChunkPoz> chunks_to_rem = new();
        ChunksConstructor to_add = new(), to_renew = new();
        RenderLayer binded_world_layer, nontransparent_blocks;
        Camera camera;
        object task_lock = new();
        (Task task, float query, float last) new_chunks = (null!, 2, 0);

        public Terrain(Camera camera)
        {
            this.camera = camera;
            binded_world_layer = RenderLayer.Create().Finisch();
            nontransparent_blocks = RenderLayer.Create().Camera(camera).Finisch();
            nontransparent_blocks.Meshes.AddRange(Client.Me.Entity!.Dimention!.AllEntities.Select(e => e.Model));
            nontransparent_blocks.Meshes.Remove(Client.Me.Entity!.Model);
            binded_world_layer.Meshes.Add(nontransparent_blocks);
            new_chunks.last = new_chunks.query;
        }

        public void AddModel(IDrawable drawable) => nontransparent_blocks.Meshes.Add(drawable);
        public bool RemoveModel(IDrawable drawable) => nontransparent_blocks.Meshes.Remove(drawable);

        /// <summary>
        /// Kolor nieba
        /// </summary>
        public Color SkyColor
        {
            get => binded_world_layer.Color;
            set => binded_world_layer.Color = value;
        }
        
        /// <summary>
        /// Częstotliwość generowania modeli nowych <see cref="Chunk"/>ów
        /// </summary>
        public float NewChunkFrequency
        {
            get => new_chunks.query;
            set => new_chunks.query = value;
        }

        /// <summary>
        /// Dodaj nowy <see cref="Chunk"/> do listy do wyrenderowania
        /// </summary>
        public void Add(ChunkPoz chunk)
        {
            lock (chunk_models)
                if (chunk_models.ContainsKey(chunk))
                    return;
            to_add.Add(chunk);
        }
        
        /// <summary>
        /// Wygeneruj model <see cref="Chunk"/>a natychmiastowo
        /// </summary>
        public void EmidietRenew(ChunkPoz chunk)
        {
            lock (task_lock)
            {
                nontransparent_blocks.Meshes.Remove(chunk_models[chunk]);
                var new_chunk = ModelConstructor.ChunkModel(Client.Me.Entity!.Dimention!, chunk);
                nontransparent_blocks.Meshes.Add(new_chunk);
                chunk_models[chunk].Dispose();
                chunk_models[chunk] = new_chunk;
            }
        }

        /// <summary>
        /// Wygeneruj sześcian z modelu <see cref="Chunk"/>a natychmiastowo
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="quard"></param>
        public void EmidietRenew(ChunkPoz chunk, int quard)
        {
            lock (task_lock)
            {
                if (chunk_models.TryGetValue(chunk, out var chunk_m))
                {
                    var new_quard = ModelConstructor.ChunkModel(Client.Me.Entity!.Dimention!, chunk, quard);
                    chunk_m.set_new_quard(new_quard, quard);
                }
            }
        }
        
        /// <summary>
        /// Usuwa model z wygenerowanych albo z listy do wygenerowania modelu
        /// </summary>
        public void Remove(ChunkPoz chunk)
        {
            lock(task_lock)
            {
                if (!chunk_models.ContainsKey(chunk))
                    return;
                if (chunks_to_rem.Contains(chunk))
                    return;
                chunks_to_rem.Add(chunk);
            }
        }
        
        /// <summary>
        /// Wygeneruj model <see cref="Chunk"/>a ponownie
        /// </summary>
        public void Renew(ChunkPoz chunk)
        {
            lock (chunk_models)
                if (!chunk_models.ContainsKey(chunk))
                    return;
            to_renew.Add(chunk);
        }

        /// <summary>
        /// Wyrenderuj obraz terenu na ekranie
        /// </summary>
        public void Draw()
        {
            lock (nontransparent_blocks)
                nontransparent_blocks.UpdateContent();
            lock (binded_world_layer)
            {
                binded_world_layer.UpdateContent();
                binded_world_layer.Draw();
            }
        }
        
        /// <summary>
        /// Gotowy obraz terenu
        /// </summary>
        public RenderLayer Finisched => binded_world_layer;

        /// <summary>
        /// Zarządzanie i tworzenie nowych modeli chunków co określony czas w <see cref="NewChunkFrequency"/>
        /// </summary>
        /// <param name="time"></param>
        public void ChunkUpdate(double time)
        {
            to_add.Dimention = to_renew.Dimention = Client.Me.Entity!.Dimention!;
            
            add_new_chunks();
            renew_old_chunks();
            remove_old_chunk();
            emidiet_renew();
            chunk_rendering_task();

            //Methods
            void add_new_chunks()
            {
                foreach(var ch in to_add.Finished())
                {
                    chunk_models.TryAdd(ch.Key, ch.Value);
                    nontransparent_blocks.Meshes.Add(ch.Value);
                }
            }
            void renew_old_chunks()
            {
                foreach (var ch in to_renew.Finished())
                {
                    var old = chunk_models[ch.Key];
                    nontransparent_blocks.Meshes.Remove(old);
                    old?.Dispose();
                    chunk_models[ch.Key] = ch.Value;
                    nontransparent_blocks.Meshes.Add(ch.Value);
                }
            }
            void remove_old_chunk()
            {
                if (chunks_to_rem.Count == 0)
                    return;
                var chunk = chunks_to_rem[0];
                var m = nontransparent_blocks.Meshes;
                lock (task_lock)
                {
                    chunk_models.Remove(chunk, out var model);
                    m.Remove(model!);
                    model!.Dispose();
                    chunks_to_rem.RemoveAt(0);
                }
            }
            void emidiet_renew()
            {
                foreach (var ch_q in Client.Me.Entity!.Dimention!.get_changet_chunks())
                    EmidietRenew(ch_q.chunk, (int)ch_q.quard);
            }
            void chunk_rendering_task()
            {
                if (new_chunks.last > new_chunks.query)
                {
                    new_chunks.last -= new_chunks.query;
                    new_chunks.task = Task.Run(() =>
                    {
                        if (Client.Me.Entity == null)
                            return;
                        var en = Client.Me.Entity;
                        var dim = en.Dimention;
                        if (dim == null)
                            return;
                        foreach (var ch in MathC.GetElementsFromCenter(10))
                        {
                            var chunk_poz = new ChunkPoz(ch.x, ch.y) + en.Chunk;
                            if (!dim.IsChunkLoadetOrLoading(chunk_poz))
                                continue;
                            if (to_add.IsProcessed(chunk_poz))
                                continue;
                            if (chunk_models.ContainsKey(chunk_poz))
                                continue;
                            Add(chunk_poz);
                            Renew(chunk_poz + new ChunkPoz(-1, 0));
                            Renew(chunk_poz + new ChunkPoz(1, 0));
                            Renew(chunk_poz + new ChunkPoz(0, -1));
                            Renew(chunk_poz + new ChunkPoz(0, 1));
                        }
                        FinischedChunkModel[] models;
                        lock (task_lock)
                            models = chunk_models.Values.ToArray();
                        foreach(var chm in models)
                            if (en.Chunk.Distance(chm.Pozition) >= 15)
                                Remove(chm.Pozition);
                    });
                }
                else
                    new_chunks.last += (float)time;
            }
        }
        
        /// <summary>
        /// Odświerzenie rozmiaru płuten na których elementy są renderowane
        /// </summary>
        /// <param name="size"></param>
        public void Resize(Vector2i size)
        {
            nontransparent_blocks.Resize(size);
            binded_world_layer.Resize(size);
        }
    }

    class ChunksConstructor
    {
        Dictionary<ChunkPoz, OneOf<Null, Task, FinischedChunkModel, Exception>> chunks_to_add = new();
        DimentionSpace dimention = Server.Dimentions[Dimentions.OVERWORLD];
        int in_working = 0, max_working = 20;

        public DimentionSpace Dimention { get => dimention; set => dimention = value; }
        public int MaxInWorking { get => max_working; set => max_working = value; }
        public void Add(ChunkPoz chunk)
        {
            lock(chunks_to_add)
            {
                if (chunks_to_add.ContainsKey(chunk))
                    return;
                if(max_working > in_working)
                {
                    in_working++;
                    var task = generate(chunk);
                    chunks_to_add.Add(chunk, task);
                }
                else
                    chunks_to_add.Add(chunk, new Null());
            }
        }
        public bool IsProcessed(ChunkPoz chunk)
        {
            lock (chunks_to_add)
                return chunks_to_add.ContainsKey(chunk);
        }

        async Task generate(ChunkPoz poz)
        {
            try
            {
                var rez = await ModelConstructor.ChunkModelAsync(dimention, poz);
                lock (chunks_to_add)
                {
                    chunks_to_add[poz] = rez;
                    if (chunks_to_add.Count(e => e.Value.IsT0) > 0)
                    {
                        var new_poz = chunks_to_add.Where(e => e.Value.IsT0).First().Key;
                        var t = generate(new_poz);
                        chunks_to_add[new_poz] = t;
                    }
                    else
                        in_working--;
                }
            }
            catch (Exception ex)
            {
                lock (chunks_to_add)
                {
                    chunks_to_add[poz] = ex;
                    if (chunks_to_add.Count(e => e.Value.IsT0) > 0)
                    {
                        var new_poz = chunks_to_add.Where(e => e.Value.IsT0).First().Key;
                        var t = generate(new_poz);
                        chunks_to_add[new_poz] = t;
                    }
                    else
                        in_working--;
                }
            }
        }
        public IEnumerable<(ChunkPoz Key, FinischedChunkModel Value)> Finished() => new Enumerable(this);
        struct Enumerable : IEnumerable<(ChunkPoz Key, FinischedChunkModel Value)>, IEnumerator<(ChunkPoz Key, FinischedChunkModel Value)>
        {
            OneOf<(ChunkPoz Key, FinischedChunkModel Valie), Exception> current;
            ChunksConstructor source;

            public Enumerable(ChunksConstructor source) => this.source = source;
            public (ChunkPoz Key, FinischedChunkModel Value) Current => current.IsT1 ? throw new(current.AsT1.Message, current.AsT1) : current.AsT0;
            object IEnumerator.Current => Current;
            public void Dispose() => (source, current) = (null!, new());
            public IEnumerator<(ChunkPoz Key, FinischedChunkModel Value)> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => this;
            public void Reset() { }

            public bool MoveNext()
            {
                lock (source.chunks_to_add)
                {
                    var res = source.chunks_to_add.Where(t => t.Value.IsT2 || t.Value.IsT3).GetEnumerator();
                    if (!res.MoveNext())
                        return false;
                    source.chunks_to_add.Remove(res.Current.Key);
                    current = res.Current.Value.IsT2 ? (res.Current.Key, res.Current.Value.AsT2) : res.Current.Value.AsT3;
                    return true;
                }
            }
        }
    }
}
