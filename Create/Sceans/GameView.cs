using Create.Conteiner;
using Create.Conteiner.Items;
using Create.Elements;
using Create.Elements.Bazic.Entitys;
using Create.Elements.Interfaces;
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
using Create.Linq;
using static Create.Elements.Bazic.Entitys.Mob;

namespace Create.Sceans;

internal sealed partial class GameView : Scean
{
    Camera camera;
    Terrain terrain;
    Interface _interface;
    List<(string name, UserInterface user, SpacePoint point, bool onTop)> userInterfaces = new();
    static Shader shader = Assets.GetShader("create:selector");
    
    internal Camera Camera => camera;
    internal Terrain _Terrain => terrain;
    internal Interface Interface => _interface;
    internal List<(string name, UserInterface user, SpacePoint point, bool onTop)> UserInterfaces => userInterfaces;

    public GameView()
    {
        camera = new();
        terrain = new(camera);
        _interface = new(OpenGL.Engine.Size.X, OpenGL.Engine.Size.Y);
        _interface.CursorGet += () => Mouse.Pozition;
        _interface.MouseLeft += () => Mouse.Lock ? (false, false, false) : Mouse.Left;
        _interface.MouseRight += () => Mouse.Lock ? (false, false, false) : Mouse.Right;
        _interface.MouseScroll += () => Mouse.Lock ? (false, false, false, 0) : Mouse.Scroll;
    }

    protected override void SceanLoad()
    {
        camera.Projection = Projection;
        Mouse.Lock = true;
        terrain.SkyColor = Color.FromArgb(255, 100, 171, 236);
        camera.Model = Matrix4.CreateTranslation(-.5f, 0, -.5f);
        camera.RevertAxis.x = true;
        Mouse.Visible = false;

        _interface.MainElements.AddChild(new SpacePoint
        {
            Size = OpenGL.Engine.Size.ToTumple(),
            Pozition = (0, 0),
            Interactable = false,
            Name = "Passive Interface",
            AnkerMode = SpacePoint.Anker.All,
        });
        _interface.MainElements.AddChild(new SpacePoint
        {
            Size = (OpenGL.Engine.Size.X + 1, OpenGL.Engine.Size.Y + 1),
            Pozition = (0, 0),
            Active = false,
            Name = "Active Interface",
            AnkerMode = SpacePoint.Anker.All,
            Element = new Image
            {
                Color = new Color4(0, 0, 0, .75f)
            }
        });
        _interface.MainElements.AddChild(new SpacePoint
        {
            Size = (OpenGL.Engine.Size.X + 1, OpenGL.Engine.Size.Y + 1),
            Pozition = (0, 0),
            Active = false,
            Name = "Top Passive Interface",
            AnkerMode = SpacePoint.Anker.All,
            Interactable = false
        });
        _interface.MainElements.AddChild(new()
        {
            Active = false,
            Pozition = (0, 0),
            Interactable = false,
            Size = (16 * 4, 16 * 4),
            Name = "Transferred Item",
            Element = new ItemSlot() { Enable = false }
        });
        Client.CreateUserInterface<InformationBars>();
        Client.CreateUserInterface<ItemDescription>().Padding = 4;
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
        bool inventory = Client.Me.Entity!.Data.Get("inventory_open") as bool? ?? false;
        var interaction = Mob.ImLookingAt(Client.Me.Entity!, 6);
        UpdateInteractionPointer(interaction);

        if (Mouse.Visible != inventory)
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
                _interface.MainElements.Find("Top Passive Interface")!.Active = false;
                inventory = false;
            }
            else
            {
                _interface.MainElements.Find("Active Interface")!.Active = true;
                _interface.MainElements.Find("Top Passive Interface")!.Active = true;
                inventory = true;
                Client.GetUserInterfaces().Where(i => !(i.IsPassive || i.IsOnTop))
                    .ForEvery(i => Client.RemoveUserInterface(i));
            }
            Client.Me.Entity!.Data.Set("inventory_open", inventory);
        }

        if (Keyboard.E.Down)
        {
            inventory = !inventory;
            if (inventory)
            {
                foreach (var i in Client.GetUserInterfaces().Where(ui => !(ui.IsPassive || ui.IsOnTop)).ToArray())
                    Client.RemoveUserInterface(i);
                (Client.GetUserInterface<CreativeInventory>() ?? Client.CreateUserInterface<CreativeInventory>()).Tab = 
                    CreativeInventory.OpenTab.InventoryTab;
            }
            Client.Me.Entity!.Data.Set("inventory_open", inventory);

        }

        _interface.MainElements.Find("Active Interface")!.Active = inventory;
        _interface.MainElements.Find("Top Passive Interface")!.Active = inventory;

        if ((Mouse.Left.Down || Mouse.Right.Down || Mouse.Scroll.Down) && !inventory)
        {
            var button = Mouse.Left.Down ? ClickEventButton.Left :
                         Mouse.Right.Down ? ClickEventButton.Right :
                         Mouse.Scroll.Down ? ClickEventButton.Scroll :
                         ClickEventButton.Unknown;

            var world = Client.Me.Entity!.Dimention!.World;
            (int, ItemStack?) inHand = (Client.GetUserInterface<InformationBars>(), Client.Me).Cast(t =>
                (t.Item1?.UsedSlot ?? 0, (t.Me.Entity?.Data.Get("tool_slots") as ToolsBar? ?? new())[t.Item1?.UsedSlot ?? 0]));
            if(interaction.HasValue)
            {
                var block = world.GetBlock(interaction!.Value.BlockPozition);
                var blockArgs = new Block.OnClickArgs()
                {
                    HitBoxIndex = interaction.Value.HitBoxIndex,
                    BlockPozition = interaction.Value.BlockPozition,
                    TargetSide = interaction.Value.BlockSide,
                    Player = Client.Me,
                    Button = button,
                    Block = block,
                    World = world,
                    InHand = inHand
                };
                var itemArgs = new Item.OnClickArgs()
                {
                    BlockArgs = blockArgs,
                    Player = Client.Me,
                    Button = button,
                    World = world,
                    InHand = (inHand.Item1, inHand.Item2 ?? new(1, Items.BLOCK_ITEM))
                };

                if (Keyboard.LeftShift.Status || Keyboard.RightShift.Status)
                {
                    if ((!inHand.Item2?.Item.OnClick(itemArgs)) ?? true)
                        block.Block.OnClick(blockArgs);
                }
                else
                {
                    if (!block.Block.OnClick(blockArgs))
                        inHand.Item2?.Item.OnClick(itemArgs);
                }
            }
            else
                inHand.Item2?.Item.OnClick(new() { 
                    BlockArgs = null,
                    Button = button,
                    Player = Client.Me,
                    World = world,
                    InHand = (inHand.Item1, inHand.Item2 ?? new(1, Items.BLOCK_ITEM))});
            inventory = Client.Me.Entity!.Data.Get("inventory_open") as bool? ?? false;
        }
        _interface.Phizic();
        foreach (var i in userInterfaces)
            i.user.Update(new() { time = args.Time, activeInventory = inventory });

        if (inventory)
        {
            var trans_slot = _interface.MainElements.Find("Transferred Item")?.Element as ItemSlot;
            if (trans_slot != null)
            {
                trans_slot.Point!.Active = true;
                trans_slot.Point!.GlobalPozition = Mouse.Pozition;
            }
        }
        else
        {
            var trans_slot = _interface.MainElements.Find("Transferred Item")?.Element as ItemSlot;
            if (trans_slot != null)
                trans_slot.Point!.Active = false;
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

    (Mesh model, ((float x, float y, float z) start, (float x, float y, float z) end)[] values, (PlacedBlock block, (int x, int y, int z) poz) block)? current;
    void UpdateInteractionPointer(ImLookingAtRezult? collizion)
    {
        if (!current.HasValue && !collizion.HasValue)
            return;
        if(current.HasValue && !collizion.HasValue)
        {
            terrain.RemoveModel(current.Value.model);
            current.Value.model.Dispose();
            current = null;
        }
        else if (!current.HasValue && collizion.HasValue)
        {
            var world = Client.Me.Entity?.Dimention?.World;
            if (world is null)
                return;
            var b = world.GetBlock(collizion.Value.BlockPozition);
            var int_mod = b.Block.GetInteractionModel(new() { block = b, pozition = collizion.Value.BlockPozition, world = world, HitBoxIndex = collizion.Value.HitBoxIndex });
            var mesh = generateSelectionModel(int_mod);
            mesh.Position = collizion.Value.BlockPozition.ToVector().ToVector3();
            current = (mesh, int_mod.ToArray(), (b, collizion.Value.BlockPozition));
            terrain.AddModel(mesh);
        }
        else
        {
            var world = Client.Me.Entity?.Dimention?.World;
            if (world is null)
                return;
            var b = world.GetBlock(collizion!.Value.BlockPozition);
            var block_com = current!.Value.block.block == b;
            var poz_com = current.Value.block.poz == collizion!.Value.BlockPozition;
            if(!block_com || !poz_com)
            {
                var int_mod = b.Block.GetInteractionModel(new() { block = b, pozition = collizion.Value.BlockPozition, world = world });
                var int_mod_com = compare(current!.Value.values, int_mod);
                if (!int_mod_com)
                {
                    var new_model = generateSelectionModel(int_mod);
                    terrain.RemoveModel(current.Value.model);
                    current.Value.model.Dispose();
                    current = (new_model, int_mod.ToArray(), (b, collizion.Value.BlockPozition));
                    new_model.Position = collizion.Value.BlockPozition.ToVector().ToVector3();
                    terrain.AddModel(new_model);
                }
                else
                {
                    if(!poz_com)
                    {
                        current = (current.Value.model, current.Value.values, (b, collizion.Value.BlockPozition));
                        current.Value.model.Position = collizion.Value.BlockPozition.ToVector().ToVector3();
                    }
                }
            }
        }

        bool compare(((float x, float y, float z) start, (float x, float y, float z) end)[] array, 
         IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> enumerable)
        {
            if (array == null || enumerable == null)
                return false;
            var arr_ = ((IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)>)array).GetEnumerator();
            var enu_ = enumerable.GetEnumerator();
            bool p1, p2;
            for ((p1, p2) = (enu_.MoveNext(), arr_.MoveNext()); p1 && p2; (p1, p2) = (enu_.MoveNext(), arr_.MoveNext()))
            {
                if (arr_.Current != enu_.Current)
                    return false;
            }
            return (!p1) && (!p2);
        }
        Mesh generateSelectionModel(IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> rawModel)
        {
            var cre = Mesh.Create(shader)
                .DrawingMode(MechDrawingMode.Line)
                .LineThickness(2);
            var points = rawModel.Deconstruct().Select(v => ((v.ToVector() - new Vector3(.5f)) * 1.003f) + new Vector3(.5f)).ToArray();
            var index = (new Range(0, points.Length - 1)).GetEnumerable().ToArray();

            cre.SetVertex("poz", points);
            cre.SetTrangles(index);
            return cre.Finish();
        }
    }

}
