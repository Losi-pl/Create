using Create.Elements;
using Create.Net;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;
using System.Drawing;
using System.Reflection;

namespace Create.Sceans;

partial class GameView
{
    /// <summary>
    /// Mechanizm renderowania terenu
    /// </summary>
    public class Terrain
    {
        Dictionary<ChunkPoz, FinischedChunkModel> chunk_models = new();
        List<ChunkPoz> chunks_to_add = new();
        List<ChunkPoz> chunks_to_rem = new();
        List<ChunkPoz> chunks_to_ren = new();
        RenderLayer binded_world_layer, nontransparent_blocks;
        Camera camera;
        object task_lock = new();
        (Task task, float query, float last) new_chunks = (null!, 2, 0);

        public Terrain(Camera camera)
        {
            this.camera = camera;
            binded_world_layer = RenderLayer.Create().Finisch();
            nontransparent_blocks = RenderLayer.Create().Camera(camera).Finisch();
            nontransparent_blocks.Meshes.AddRange(Client.Me.Entity!.Dimention!.AllEntities.ConvertAll(e => e.Model));
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
            lock(task_lock)
            {
                if (chunk_models.ContainsKey(chunk))
                    return;
                if (chunks_to_add.Contains(chunk))
                    return;
                chunks_to_add.Add(chunk);
            }
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
                var chunk_m = chunk_models[chunk];
                var new_quard = ModelConstructor.ChunkModel(Client.Me.Entity!.Dimention!, chunk, quard);
                chunk_m.set_new_quard(new_quard, quard);
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
            lock (task_lock)
            {
                if (!chunk_models.ContainsKey(chunk))
                    return;
                if (chunks_to_ren.Contains(chunk))
                    return;
                chunks_to_ren.Add(chunk);
            }
        }

        /// <summary>
        /// Wyrenderuj obraz terenu na ekranie
        /// </summary>
        public void Draw()
        {
            nontransparent_blocks.UpdateContent();
            binded_world_layer.UpdateContent();
            binded_world_layer.Draw();
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
            render_new_chunk();
            remove_old_chunk();
            renew_old_chunk();
            emidiet_renew();
            chunk_rendering_task();

            //Methods
            void render_new_chunk()
            {
                if (chunks_to_add.Count == 0)
                    return;
                var chunk = chunks_to_add[0];
                var done_model = ModelConstructor.ChunkModel(Server.Dimentions[Dimentions.OVERWORLD], chunk);
                lock(task_lock)
                {
                    chunk_models.Add(chunk, done_model);
                    nontransparent_blocks.Meshes.Add(done_model);
                    chunks_to_add.RemoveAt(0);
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
            void renew_old_chunk()
            {
                if (chunks_to_ren.Count == 0)
                    return;
                var chunk = chunks_to_ren[0];
                var m = nontransparent_blocks.Meshes;
                var new_model = ModelConstructor.ChunkModel(Server.Dimentions[Dimentions.OVERWORLD], chunk);
                lock(task_lock)
                {
                    var old_model = chunk_models[chunk];
                    chunk_models[chunk] = new_model;
                    m.Add(new_model);
                    m.Remove(old_model);
                    chunks_to_ren.RemoveAt(0);
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
                            if (chunk_models.ContainsKey(chunk_poz))
                                continue;
                            if (chunks_to_add.ContainsKey(chunk_poz))
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
}
