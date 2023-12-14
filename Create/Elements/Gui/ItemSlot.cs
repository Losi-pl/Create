using Create.Conteiner;
using Create.Elements.Interfaces;
using Create.Linq;
using Create.Net;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.Render;
using OpenTK.Mathematics;

namespace Create.Elements.Gui;

public sealed class ItemSlot : Element
{
    static Dictionary<(int w, int h), (RenderLayer layer, int count)> renderLayers = new();
    
    ItemStack? itemStack;
    Item.ItemModel? itemModel;
    SimpleTextMesh text = new() { 
        HorizontalDirection = Render.Text.HorizontalDirection.Left, 
        VerticalDirection = Render.Text.VerticalDirection.Up, 
        Size = 8 * 4};
    bool enable = true, hoverd, status, hardSelect;
    int? id;
    static Shader shader = Assets.GetShader("create:interface/itemslot")
        .SetUniform("hoverd_color", (Vector4)new Color4(1f, 1f, 1f, .4f));

    static Mesh mesh = Mesh.Create(shader)
        .SetTrangles(new[] { 0, 3, 1, 2, 3, 0 })
        .SetVertex("poz", new Vector2[]
        {
            new(0, 0),
            new(1, 0),
            new(0, 1),
            new(1, 1)
        }).Finish();

    public ItemSlot(int? id = null) => this.id = id;
    public int? ID => id;
    public bool Enable { get => enable; 
        set
        {
            enable = value;
            if(Point != null)
            {
                if (value)
                {
                    Point.OnEnter += OnEnter;
                    Point.OnExit += OnExit;
                }
                else
                {
                    Point.OnEnter -= OnEnter;
                    Point.OnExit -= OnExit;
                }
            }
        }
    }
    public ItemStack? ItemStack
    {
        get => itemStack;
        set
        {
            if (value == itemStack && value?.Count == itemStack?.Count)
                return;
            if(itemModel.HasValue)
            {
                var dis = itemModel.Value.model as IDisposable;
                if(dis is not null)
                    dis.Dispose();
            }
            itemStack = value?.Item == null || value?.Count == 0 ? null : value;
            itemModel = itemStack?.Item.GetItemModel(itemStack.Value, Net.Client.Me);
            text.Text = (itemStack?.Count > 1 ? itemStack?.Count.ToString() : text.Text) ?? text.Text;
            if(hoverd)
            {
                if (itemStack.HasValue)
                {
                    var id = Client.GetUserInterface<ItemDescription>()!;
                    if (id is null) return;
                    id.Visible = true;
                    id.Text = itemStack.Value.Item.CodeName;
                }
                else
                {
                    var id = Client.GetUserInterface<ItemDescription>()!;
                    if (id is null) return;
                    id.Visible = false;
                }
            }
        }
    }
    public bool DisplayStatus
    {
        get => status;
        set => status = value;
    }
    public bool HardSelected { get => hardSelect; set => hardSelect = value; }

    void OnEnter(SpacePoint point)
    {
        hoverd = true;
        if (itemStack.HasValue)
        {
            var id = Client.GetUserInterface<ItemDescription>()!;
            if (id is null) return;
            id.Visible = true;
            id.Text = itemStack.Value.Item.CodeName;
        }
    }
    void OnExit(SpacePoint point)
    {
        hoverd = false;
        var id = Client.GetUserInterface<ItemDescription>()!;
        if (id is null) return;
        id.Visible = false;
    }

    protected internal override void Bind(SpacePoint point)
    {
        var size = ((int)point.Size.Width, (int)point.Size.Height);
        add_canvas(size);
        if(Enable)
        {
            point.OnEnter += OnEnter;
            point.OnExit += OnExit;
        }
    }
    protected internal override void Unbind(SpacePoint point)
    {
        var size = ((int)point.Size.Width, (int)point.Size.Height);
        sub_canvas(size);
        if (Enable)
        {
            point.OnEnter -= OnEnter;
            point.OnExit -= OnExit;
        }
    }
    protected internal override void OnSizeChanget((float Width, float Height) old, (float Width, float Height) @new)
    {
        var o = ((int)old.Width, (int)old.Height);
        var n = ((int)@new.Width, (int)@new.Height);
        sub_canvas(o);
        add_canvas(n);
    }

    static void add_canvas((int w, int h) size)
    {
        lock(renderLayers)
        {
            if (renderLayers.TryGetValue(size, out var rl))
                renderLayers[size] = (rl.layer, rl.count + 1);
            else
                renderLayers.Add(size, (RenderLayer.Create().SetSize(size.ToVector()).Finisch(), 1));
        }
    }
    static void sub_canvas((int w, int h) size)
    {
        lock (renderLayers)
            if (renderLayers.TryGetValue(size, out var rl))
            {
                var i = rl.count - 1;
                if (i < 1)
                {
                    renderLayers.Remove(size);
                    rl.layer.Dispose();
                }
                else
                    renderLayers[size] = (rl.layer, i);
            }
    }

    public override void Draw(Matrix4 projection)
    {
        if (itemModel.HasValue)
        {
            var render_layer = renderLayers.TryGetValue(((int)Point!.Size.Width, (int)Point!.Size.Height), out var rl) ? rl.layer : null;
            if (render_layer is null)
                return;
            render_layer.Clear();
            render_layer.ExecuteIn(itemModel.Value.model.Draw);
            shader.SetUniform("text", render_layer.Textures[OpenTK.Graphics.OpenGL.FramebufferAttachment.ColorAttachment0]);
            shader.SetUniform("contains", true);
            shader.SetUniform("hoverd", hoverd);
            shader.SetUniform("hard_select", hardSelect);
            mesh.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
            if(itemStack?.Count > 1)
            {
                text.Color = new(62, 62, 62, 255);
                text.Draw(Matrix4.CreateTranslation((Point.Size.Width / 2) + 4, (-Point.Size.Height / 2) - 4, 0) * projection);
                text.Color = Color4.White;
                text.Draw(Matrix4.CreateTranslation(Point.Size.Width / 2, -Point.Size.Height / 2, 0) * projection);
            }
        }
        else
        {
            if (!(hoverd || hardSelect))
                return;
            shader.SetUniform("contains", false);
            mesh.Draw(Matrix4.CreateScale(Point!.Size.Width, Point.Size.Height, 1) * projection);
        }
    }

    public static IEnumerable<ItemSlot> GetAllSlots(SpacePoint point)
    {
        return get_point(point).Select(p => p.Element as ItemSlot).Where(s => s is not null).Cast<ItemSlot>();

        IEnumerable<SpacePoint> get_point(SpacePoint point)
        {
            yield return point;
            foreach(var p in point.Childs)
                foreach(var c in get_point(p))
                    yield return c;
        }
    }
}
