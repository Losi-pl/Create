using Create.Elements;
using Create.Net;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;
using System.Drawing;

namespace Create.Sceans;

partial class GameView
{
    public class Terrain
    {
        Dictionary<ChunkPoz, ChunkConstructor.FinischedChunkModel> chunk_models = new();
        List<ChunkPoz> chunks_to_add = new();
        RenderLayer binded_world_layer, nontransparent_blocks;
        object task_lock = new();
        (Task task, float query, float last) new_chunks = (null!, 2, 0);

        public Terrain(Camera camera)
        {
            binded_world_layer = RenderLayer.Create().Finisch();
            nontransparent_blocks = RenderLayer.Create().Camera(camera).Finisch();
            nontransparent_blocks.Meshes.AddRange(Server.Dimentions[Dimentions.OVERWORLD].AllEntities.ConvertAll(e => e.Model));
            nontransparent_blocks.Meshes.Remove(Client.Me.Entity!.Model);
            binded_world_layer.Meshes.Add(nontransparent_blocks);
            new_chunks.last = new_chunks.query;
        }

        public Color SkyColor
        {
            get => binded_world_layer.Color;
            set => binded_world_layer.Color = value;
        }

        public float NewChunkFrequency
        {
            get => new_chunks.query;
            set => new_chunks.query = value;
        }

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
        public void Remove(ChunkPoz chunk)
        {
            lock(task_lock)
            {
                var meshs = nontransparent_blocks.Meshes;
                if (!chunk_models.Remove(chunk, out var ch_mod))
                    return;
                foreach (var m in ch_mod.AllModelParts())
                    meshs.Remove(m);
            }
        }

        public void Draw()
        {
            nontransparent_blocks.UpdateContent();
            binded_world_layer.UpdateContent();
            binded_world_layer.Draw();
        }
        public RenderLayer Finisched => binded_world_layer;

        public void ChunkUpdate(double time)
        {
            render_new_chunk();
            chunk_rendering_task();

            //Methods
            void render_new_chunk()
            {
                if (chunks_to_add.Count == 0)
                    return;
                var chunk = chunks_to_add[0];
                var done_model = ChunkConstructor.GenerateModel(Server.Dimentions[Dimentions.OVERWORLD], chunk);
                foreach (var m in done_model.AllModelParts())
                    nontransparent_blocks.Meshes.Add(m);
                lock(task_lock)
                {
                    chunk_models.Add(chunk, done_model);
                    chunks_to_add.RemoveAt(0);
                }
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
                            if (!dim.IsChunkLoadet(new ChunkPoz(ch.x, ch.y) + en.Chunk))
                                continue;
                            Add(new ChunkPoz(ch.x, ch.y) + en.Chunk);
                        }
                    });
                }
                else
                    new_chunks.last += (float)time;
            }
        }
        public void Resize(Vector2i size)
        {
            nontransparent_blocks.Resize(size);
            binded_world_layer.Resize(size);
        }
    }
}
