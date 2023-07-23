using Create.Elements;
using Create.Elements.Bazic.Entitys;
using Create.Elements.Gui;
using Create.Input;
using Create.Net;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.OpenGL.GUI.Elements;
using Create.OpenGL.Textures;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Drawing;

namespace Create.Sceans;

internal sealed partial class GameView : Scean
{
    Camera camera;
    Terrain terrain;
    Interface _interface;
    int slot_ind;
    List<(string name, UserInterface user, SpacePoint point)> userInterfaces = new();

    internal Camera Camera => camera;
    internal Terrain _Terrain => terrain;
    internal Interface Interface => _interface;
    internal List<(string name, UserInterface user, SpacePoint point)> UserInterfaces => userInterfaces;

    public GameView()
    {
        camera = new();
        terrain = new(camera);
        _interface = new(OpenGL.Engine.Size.X, OpenGL.Engine.Size.Y);
        _interface.CursorGet += () => Mouse.Pozition;
        _interface.MouseLeft += () => Mouse.Lock ? (false, false, false) : Mouse.Left;
        _interface.MouseRight += () => Mouse.Lock ? (false, false, false) : Mouse.Right;
    }

    protected override void SceanLoad()
    {
        camera.Projection = Projection;
        Mouse.Lock = true;
        terrain.SkyColor = Color.FromArgb(255, 100, 171, 236);
        camera.Model = Matrix4.CreateTranslation(-.5f, 0, -.5f);
        camera.RevertAxis.x = true;
        Mouse.Visible = false;

        _interface.MainElements.AddChild(Assets.GetInterface("create:crosshair"));
        _interface.MainElements.AddChild(Assets.GetInterface("create:statusbars"));

        var user_interface = new SpacePoint
        {
            Size = (OpenGL.Engine.Size.X + 1, OpenGL.Engine.Size.Y + 1),
            Pozition = (0, 0),
            Active = false,
            Name = "Active Interface",
            AnkerMode = SpacePoint.Anker.All,
            Element = new Image
            {
                Color = new Color4(0, 0, 0, .8f)
            }
        };
        _interface.MainElements.AddChild(user_interface);
    }

    protected override void RenderFrame(FrameEventArgs args)
    {
        camera.Pozition = Client.Me.Entity!.Pozition + new Vector3(0, ((Mob)Client.Me.Entity.Entity).GetCameraHeight(Client.Me.Entity), 0);
        terrain.Draw();
        _interface.Refrasch();
        _interface.Canvas.Draw();
        OpenGL.Engine.FinishFrame();
    }

    protected override void UpdateFrame(FrameEventArgs args)
    {
        bool inventory = Client.Me.Entity!.Data.Get("inventory_open") as bool? ?? false; ;
        if(Mouse.Visible != inventory)
        {
            Mouse.Visible = inventory;
            Mouse.Lock = !Mouse.Visible;
        }

        if (Mouse.Lock)
            camera_rotation();

        if (Keyboard.Escape.Down)
        {
            if(inventory)
            {
                _interface.MainElements.Find("Active Interface")!.Active = false;
                inventory = false;
                var inte = Client.GetUserInterfaces().FirstOrDefault();
                if (inte is not null)
                    Client.RemoveUserInterface(inte);
            }
            else
            {
                _interface.MainElements.Find("Active Interface")!.Active = true;
                inventory = true;
            }
        }

        _interface.Phizic();
        if(Mouse.Scroll.Delta != 0)
        {
            slot_ind -= Mouse.Scroll.Delta;
            if(slot_ind < 0)
                slot_ind = 8;
            if (slot_ind > 8)
                slot_ind = 0;
            _interface.MainElements.Find("create:statusbars")?.RunEvent(slot_ind.ToString());
        }

        terrain.ChunkUpdate(args.Time);
        Client.Me.Entity!.Data.Set("inventory_open", inventory);
        
        void camera_rotation()
        {
            Vector2 rot;
            {
                var rot_v = Client.Me.Entity!.Data.Get("camera_rot");
                rot = rot_v != null ? (Vector2)rot_v : new();
            }
            
            var rota_d = Mouse.Delta;
            rot.X += (float)(rota_d.x * args.Time) * 3;
            rot.Y -= (float)(rota_d.y * args.Time) * 3;

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
        _interface.Size = args.Size.ToTumple();
        camera.Projection = Projection;
    }
    Matrix4 Projection => Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, OpenGL.Engine.Size.X / (float)OpenGL.Engine.Size.Y, .01f, 10000f);
}
