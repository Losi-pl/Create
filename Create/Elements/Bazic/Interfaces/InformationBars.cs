using Create.Elements.Gui;
using Create.Input;
using Create.OpenGL.GUI;

namespace Create.Elements.Interfaces;

[PassiveInterface]
internal sealed class InformationBars : UserInterface, IUserInterface<InformationBars>
{
    int slot_ind;
    #nullable disable
    SpacePoint statusBars;
    (ItemSlot slot, int id)[] slots;
    #nullable restore

    static (InformationBars status, SpacePoint point) IUserInterface<InformationBars>.LoadInterface(InterfaceCreatorArgs args)
    {
        var ib = new InformationBars();
        var sp = new SpacePoint();
        sp.AnkerPoints = (new(.5f, 0), new(.5f, .5f));
        sp.Size = (0, OpenGL.Engine.Size.Y / 2);

        var crosshair = Assets.GetInterface("create:crosshair");
        sp.Childs.AddChild(crosshair);
        crosshair.AnkerMode = SpacePoint.Anker.Up;
        
        ib.statusBars = Assets.GetInterface("create:statusbars");
        sp.Childs.AddChild(ib.statusBars);
        ib.statusBars.AnkerMode = SpacePoint.Anker.Down;
        ib.slots = ItemSlot.GetAllSlots(ib.statusBars)
                           .Select(s => (s, s.ID ?? 0))
                           .Where(s => s.Item2 >= 0 && s.Item2 <= 8)
                           .ToArray();

        return (ib, sp);
    }

    public override void Update(UpdateArgs args)
    {
        if (Mouse.Scroll.Delta != 0 && !args.activeInventory)
        {
            slot_ind -= Mouse.Scroll.Delta;
            if (slot_ind < 0)
                slot_ind = 8;
            if (slot_ind > 8)
                slot_ind = 0;
            statusBars.RunEvent(slot_ind.ToString());
        }

        var tools = Player.Entity?.Data.Get("tool_slots") as Conteiner.Items.ToolsBar? ?? new();
        foreach (var s in slots)
            s.slot.ItemStack = tools[s.id];
    }

    public int UsedSlot => slot_ind;
}
