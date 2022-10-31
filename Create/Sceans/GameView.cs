using Create.Elements;
using Create.Elements.Bazic.Entitys;
using Create.Input;
using Create.Net;
using Create.OpenGL;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Create.Sceans;

internal sealed class GameView : Scean
{
    Camera camera;
    Dictionary<ChunkPoz, ChunkConstructor.FinischedChunkModel> chunk_models = new();
    List<ChunkPoz> chunks_to_add = new(), chunks_to_remove = new();
    RenderLayer binded_world_layer, nontransparent_blocks;
    Matrix4 matrix;

    public GameView()
    {
        camera = new();
        binded_world_layer = RenderLayer.Create().Finisch();
        nontransparent_blocks = RenderLayer.Create().Camera(camera).Finisch();
        foreach (var v in MathC.GetElementsFromCenter(10))
            chunks_to_add.Add(new(v.x, v.y));
        matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, OpenGL.Engine.Size.X / (float)OpenGL.Engine.Size.Y, .01f, 10000f);
        nontransparent_blocks.Meshes.AddRange(Server.Dimentions[Dimentions.OVERWORLD].AllEntities.ConvertAll(e => e.Mesh));
        nontransparent_blocks.Meshes.Remove(Client.Me.Entity!.Mesh);
    }

    protected override void SceanLoad()
    {
        camera.Projection = matrix;
        Mouse.Lock = true;
        camera.Pozition = Server.Dimentions[Dimentions.OVERWORLD].Dimention.GetNewSpawnPoint().ToVector() + new Vector3(0, 1.7f, 0);
        binded_world_layer.Color = System.Drawing.Color.FromArgb(255, 100, 171, 236);
        camera.Model = Matrix4.CreateTranslation(-.5f, 0, -.5f);
        camera.RevertAxis.x = true;
        Conteiner.DataContainer c = new();
    }

    protected override void RenderFrame(FrameEventArgs args)
    {
        camera.Pozition = Client.Me.Entity!.Pozition + new Vector3(0, ((Mob)Client.Me.Entity.Entity).GetCameraHeight(Client.Me.Entity), 0);
        nontransparent_blocks.UpdateContent();
        binded_world_layer.Clear();
        binded_world_layer.ExecuteIn(nontransparent_blocks.Draw);
        binded_world_layer.Draw();
        OpenGL.Engine.FinishFrame();
    }

    protected override void UpdateFrame(FrameEventArgs args)
    {
        Mouse.Visible = !Mouse.Lock;
        if (!Mouse.Lock)
            return;

        pozition_entitys();
        camera_rotation();
        render_new_chunk();
        

        void render_new_chunk()
        {
            if (chunks_to_add.Count == 0)
                return;
            var chunk = chunks_to_add[0];
            chunks_to_add.RemoveAt(0);
            var done_model = ChunkConstructor.GenerateModel(Server.Dimentions[Dimentions.OVERWORLD], chunk);
            chunk_models.Add(chunk, done_model);
            foreach(var m in done_model.AllModelParts())
                nontransparent_blocks.Meshes.Add(m);
        }
        void pozition_entitys()
        {
            foreach (var entity in Server.Dimentions[Dimentions.OVERWORLD].AllEntities)
                ((Mesh)entity.Mesh).Position = entity.PozitionByCenter;
        }
        void camera_rotation()
        {
            Vector2 rot;
            {
                var rot_v = Client.Me.Entity!.Data.Get("camera_rot");
                rot = rot_v != null ? (Vector2)rot_v : new();
            }
            
            var rota_d = Mouse.Delta;
            rot.X += (float)(rota_d.x * args.Time) * 3;
            rot.Y += (float)(rota_d.y * args.Time) * 3;

            if (rot.Y < -90f)
                rot.Y = -90f;
            if (rot.Y > 90f)
                rot.Y = 90f;

            camera.Rotation = new(rot.Y, rot.X, 0);
            Client.Me.Entity!.Data.Set("camera_rot", rot);
        }
    }

    protected override void Resize(ResizeEventArgs args)
    {
        if (args.Size == new Vector2i())
            return;
        binded_world_layer.Resize(args.Size);
        nontransparent_blocks.Resize(args.Size);
        matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, OpenGL.Engine.Size.X / (float)OpenGL.Engine.Size.Y, .01f, 10000f);
        camera.Projection = matrix;
    }

    protected override void KeyDown(KeyboardKeyEventArgs args)
    {
        if(args.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)
            Input.Mouse.Lock = !Input.Mouse.Lock;
    }
}
