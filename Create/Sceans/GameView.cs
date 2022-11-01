using Create.Elements;
using Create.Elements.Bazic.Entitys;
using Create.Input;
using Create.Net;
using Create.OpenGL;
using Create.Render;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Drawing;

namespace Create.Sceans;

internal sealed partial class GameView : Scean
{
    Camera camera;
    Terrain terrain;
    Matrix4 matrix;

    public GameView()
    {
        camera = new();
        terrain = new(camera);
        foreach (var v in MathC.GetElementsFromCenter(10))
            terrain.Add(new(v.x, v.y));
    }

    protected override void SceanLoad()
    {
        camera.Projection = Projection;
        Mouse.Lock = true;
        terrain.SkyColor = Color.FromArgb(255, 100, 171, 236);
        camera.Model = Matrix4.CreateTranslation(-.5f, 0, -.5f);
        camera.RevertAxis.x = true;
    }

    protected override void RenderFrame(FrameEventArgs args)
    {
        camera.Pozition = Client.Me.Entity!.Pozition + new Vector3(0, ((Mob)Client.Me.Entity.Entity).GetCameraHeight(Client.Me.Entity), 0);
        terrain.Draw();
        OpenGL.Engine.FinishFrame();
    }
    protected override void UpdateFrame(FrameEventArgs args)
    {
        Mouse.Visible = !Mouse.Lock;
        if (!Mouse.Lock)
            return;

        terrain.ChunkUpdate();
        camera_rotation();
        
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
        terrain.Resize(args.Size);
        camera.Projection = Projection;
    }

    protected override void KeyDown(KeyboardKeyEventArgs args)
    {
        if(args.Key == OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)
            Mouse.Lock = !Mouse.Lock;
    }

    Matrix4 Projection => Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, OpenGL.Engine.Size.X / (float)OpenGL.Engine.Size.Y, .01f, 10000f);
}
