using Create.Elements.Gui;
using Create.OpenGL.GUI;
using OneOf;

namespace Create.Elements.Interfaces;

[OnTopInterface]
public class ItemDescription : UserInterface, IUserInterface<ItemDescription>
{
    SpacePoint root = null!;
    SimpleText text = null!;
    OneOf<float, (float horizontal, float vertical), (float top, float bottom, float left, float right)> padding = 0f;
    static (ItemDescription status, SpacePoint point) IUserInterface<ItemDescription>.LoadInterface(InterfaceCreatorArgs args)
    {
        var des = new ItemDescription();
        des.root = Assets.GetInterface("create:item_description");
        des.text = (des.root.Childs.Find("center")?.Childs[0].Element as SimpleText) ?? throw new("Item Description model is invalid");
        des.Visible = false;
        return (des, des.root);
    }

    public override void Update(UpdateArgs args)
    {
        if (!args.activeInventory) return;
        root.Pozition = Input.Mouse.Pozition;
    }

    public bool Visible
    {
        set => root.Active = value;
        get => root.Active;
    }

    public OneOf<float, (float horizontal, float vertical), (float top, float bottom, float left, float right)> Padding
    {
        get => padding;
        set { padding = value; refresh_model(); }
    }

    public string Text
    {
        get => text.Text;
        set { text.Text = value; refresh_model(); }
    }

    void refresh_model()
    {
        var size = text.Dimentions;
        (float Width, float Height) after_size = padding.Match(
            f => (size.Width + (f * 2), size.Height + (f * 2)),
            d => (size.Width + (d.horizontal * 2), size.Height + (d.vertical * 2)),
            q => (size.Width + q.left + q.right, size.Height + q.top + q.bottom));
        var p_l = root.Childs.Find("left")!;
        var p_r = root.Childs.Find("right")!;
        var p_d = root.Childs.Find("down")!;
        var p_u = root.Childs.Find("up")!;
        var p_c = root.Childs.Find("center")!;
        var p_ul = root.Childs.Find("left-up")!;
        var p_ur = root.Childs.Find("right-up")!;
        var p_dl = root.Childs.Find("left-down")!;
        var p_dr = root.Childs.Find("right-down")!;

        p_c.Pozition = (p_ul.Size.Width + (after_size.Width / 2), -p_ul.Size.Height - (after_size.Height / 2));
        p_c.Size = after_size;

        p_l.Size = (p_ul.Size.Width, after_size.Height);
        p_l.Pozition = (p_ul.Pozition.x, -p_ul.Size.Height - (after_size.Height / 2));

        p_u.Size = (after_size.Width, p_ul.Size.Height);
        p_u.Pozition = (p_ul.Size.Width + (after_size.Width / 2), p_ul.Pozition.y);

        p_ur.Pozition = (p_ul.Size.Width + after_size.Width + (p_ur.Size.Width / 2), p_ul.Pozition.y);
        p_dl.Pozition = (p_dl.Pozition.x, -p_ul.Size.Height - after_size.Height - (p_dl.Size.Height / 2));
        p_dr.Pozition = (p_ur.Pozition.x, p_dl.Pozition.y);

        p_r.Pozition = (p_ur.Pozition.x, -p_ur.Size.Height - (after_size.Height / 2));
        p_r.Size = (p_ur.Size.Width, after_size.Height);

        p_d.Pozition = (p_dl.Size.Width + (after_size.Width / 2), p_dl.Pozition.y);
        p_d.Size = (after_size.Width, p_dl.Size.Height);
        p_c.Childs[0].Pozition = padding.Match(f => (0, 0), d => (0, 0), q => (q.left + (size.Width / 2) - (after_size.Width / 2),
                                                                               q.bottom + (size.Height / 2) - (after_size.Height / 2)));
        p_c.Childs[0].Size = size;
    }
}
