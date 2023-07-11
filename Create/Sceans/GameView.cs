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

    public GameView()
    {
        camera = new();
        terrain = new(camera);
        _interface = new(OpenGL.Engine.Size.X, OpenGL.Engine.Size.Y);

        var status = new SpacePoint
        {
            Size = (0, 0),
            Pozition = (0, 0),
            AnkerMode = SpacePoint.Anker.Down
        };
        _interface.MainElements.AddChild(status);

        _interface.MainElements.AddChild(new SpacePoint
        {
            Size = (28, 28),
            Pozition = (0, 0),
            Element = new Crosshair
            {
                Interface = Assets.GetTexture("create:gui/play_screen"),
                Terrain = terrain.Finisched.Textures[OpenTK.Graphics.OpenGL.FramebufferAttachment.ColorAttachment0],
                Offset = (4, 147),
                Size = (7, 7)
            }
        }); // Crosshair
        { // Item bar
            var scrol = new SpacePoint
            {
                Size = (728, 88),
                Pozition = (0, 48),
                AnkerMode = SpacePoint.Anker.Down,
                Element = new InterfaceImage
                {
                    Offset = (0, 24),
                    Size = (182, 22),
                    Texture = Assets.GetTexture("create:gui/play_screen")
                }
            };

            foreach (var slo in new[] { 11, 31, 51, 71, 91, 111, 131, 151, 171 })
                scrol.Childs.AddChild(new()
                {
                    Size = (64, 64),
                    Pozition = (slo * 4, 0),
                    AnkerMode = SpacePoint.Anker.Left,
                    Element = new Image
                    {
                        Color = Color4.LightGreen
                    }
                });

            var right_hand = new SpacePoint()
            {
                Size = (88, 88),
                Pozition = (52, 0),
                AnkerMode = SpacePoint.Anker.Right,
                Element = new InterfaceImage
                {
                    Offset = (60, 1),
                    Size = (22, 22),
                    Texture = Assets.GetTexture("create:gui/play_screen")
                }
            };

            right_hand.Childs.AddChild(new()
            {
                Size = (64, 64),
                Element = new Image
                {
                    Color = Color4.LightGreen
                }
            });

            scrol.Childs.AddChild(right_hand);
            scrol.Childs.AddChild(new()
            {
                Size = (96, 96),
                Pozition = (48, 0),
                AnkerMode = SpacePoint.Anker.Left,
                Element = new InterfaceImage
                {
                    Offset = (0, 0),
                    Size = (24, 24),
                    Texture = Assets.GetTexture("create:gui/play_screen")
                }
            });

            status.Childs.AddChild(scrol);
        } // Item bar
        { // Exp bar
            var bar_baze = new SpacePoint
            {
                Size = (728, 20),
                Pozition = (0, 110),
                AnkerMode = SpacePoint.Anker.Down,
                Element = new InterfaceImage
                {
                    Offset = (0, 89),
                    Size = (182, 5),
                    Texture = Assets.GetTexture("create:gui/play_screen")
                }
            };

            bar_baze.Childs.AddChild(new SpacePoint
            {
                Size = (728, 20),
                Pozition = (0, 0),
                Element = new InterfaceImage
                {
                    Offset = (0, 84),
                    Size = (182, 5),
                    Texture = Assets.GetTexture("create:gui/play_screen")
                }
            });

            status.Childs.AddChild(bar_baze);
        } // Exp bar
    }

    protected override void SceanLoad()
    {
        camera.Projection = Projection;
        Mouse.Lock = true;
        terrain.SkyColor = Color.FromArgb(255, 100, 171, 236);
        camera.Model = Matrix4.CreateTranslation(-.5f, 0, -.5f);
        camera.RevertAxis.x = true;
        Mouse.Visible = false;
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
        if (Keyboard.Escape.Down)
        {
            Mouse.Visible = Mouse.Lock;
            Mouse.Lock = !Mouse.Lock;
        }

        if (!Mouse.Lock)
            return;

        terrain.ChunkUpdate(args.Time);
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
