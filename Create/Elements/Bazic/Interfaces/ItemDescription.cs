using Create.Elements.Gui;
using Create.OpenGL.GUI;

namespace Create.Elements.Interfaces;

[OnTopInterface]
public class ItemDescription : UserInterface, IUserInterface<ItemDescription>
{
    SpacePoint root = null!;
    SimpleText text = null!;
    static (ItemDescription status, SpacePoint point) IUserInterface<ItemDescription>.LoadInterface(InterfaceCreatorArgs args)
    {
        var des = new ItemDescription();
        des.root = Assets.GetInterface("create:item_description");
        des.text = (des.root.Childs.Find("center")?.Childs[0].Element as SimpleText) ?? throw new("Item Description is invalid");
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

    public string Text
    {
        get => text.Text;
        set
        {
            text.Text = value;
            var size = text.Dimentions;

            var p_l = root.Childs.Find("left")!;
            var p_r = root.Childs.Find("right")!;
            var p_d = root.Childs.Find("down")!;
            var p_u = root.Childs.Find("up")!;
            var p_c = root.Childs.Find("center")!;
            var p_ul = root.Childs.Find("left-up")!;
            var p_ur = root.Childs.Find("right-up")!;
            var p_dl = root.Childs.Find("left-down")!;
            var p_dr = root.Childs.Find("right-down")!;

            p_c.Pozition = (p_ul.Size.Width + (size.Width / 2), -p_ul.Size.Height - (size.Height / 2));
            p_c.Size = size;

            p_l.Size = (p_ul.Size.Width, size.Height);
            p_l.Pozition = (p_ul.Pozition.x, -p_ul.Size.Height - (size.Height / 2));

            p_u.Size = (size.Width, p_ul.Size.Height);
            p_u.Pozition = (p_ul.Size.Width + (size.Width / 2), p_ul.Pozition.y);

            p_ur.Pozition = (p_ul.Size.Width + size.Width + (p_ur.Size.Width / 2), p_ul.Pozition.y);
            p_dl.Pozition = (p_dl.Pozition.x, -p_ul.Size.Height - size.Height - (p_dl.Size.Height / 2));
            p_dr.Pozition = (p_ur.Pozition.x, p_dl.Pozition.y);

            p_r.Pozition = (p_ur.Pozition.x, -p_ur.Size.Height - (size.Height / 2));
            p_r.Size = (p_ur.Size.Width, size.Height);

            p_d.Pozition = (p_dl.Size.Width + (size.Width / 2), p_dl.Pozition.y);
            p_d.Size = (size.Width, p_dl.Size.Height);
        }
    }
}
