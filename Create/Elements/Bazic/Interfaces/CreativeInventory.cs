using Create.OpenGL.GUI;
using Create.Elements.Gui;
using Create.Conteiner.Items;
using Create.Conteiner;
using Create.Net;
using OpenTK.Graphics.OpenGL;
using Create.Input;
using Create.Linq;

namespace Create.Elements.Interfaces;

internal class CreativeInventory : UserInterface, IUserInterface<CreativeInventory>
{
    readonly static OpenTab[] tabTypes = Enum.GetValues<OpenTab>();
    (bool active, SpacePoint? point, ItemSlot? slot) tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9, tab10, tabInventory, tabKompas;
    int tabs_set_index;

    #nullable disable
    SpacePoint root;
    InventorySlots inventory;
    (ItemSlot slot, (int x, int y) index)[] creativeSlots;
    int scroll_offset = 0, lines_of_items = 5;
    CreativeTab usedTab;
    #nullable restore

    static (CreativeInventory status, SpacePoint point) IUserInterface<CreativeInventory>.LoadInterface(InterfaceCreatorArgs args)
    {
        var ci = new CreativeInventory();
        ci.root = Assets.GetInterface("create:creativeinventory");
        
        var points = (new[] {
            ("tab1", OpenTab.Tab1),
            ("tab2", OpenTab.Tab2),
            ("tab3", OpenTab.Tab3),
            ("tab4", OpenTab.Tab4),
            ("tab5", OpenTab.Tab5),
            ("tab6", OpenTab.Tab6),
            ("tab7", OpenTab.Tab7),
            ("tab8", OpenTab.Tab8),
            ("tab9", OpenTab.Tab9),
            ("tab10", OpenTab.Tab10),
            ("tabSearch", OpenTab.SearchTab),
            ("tabInventory", OpenTab.InventoryTab)})
            .Convert(n =>
            {
                var point = ci.root.Childs.Find(n.Item1, true);
                point!.OnClick += (p, a) => ci.TabOpenClose(p, n.Item2, a);
                var slot = ItemSlot.GetAllSlots(point).FirstOrDefault();
                return (point, slot);
            });

        ci.tab1 = (false, points[0].point, points[0].slot);
        ci.tab2 = (true, points[1].point, points[1].slot);
        ci.tab3 = (true, points[2].point, points[2].slot);
        ci.tab4 = (true, points[3].point, points[3].slot);
        ci.tab5 = (true, points[4].point, points[4].slot);
        ci.tab6 = (true, points[5].point, points[5].slot);
        ci.tab7 = (true, points[6].point, points[6].slot);
        ci.tab8 = (true, points[7].point, points[7].slot);
        ci.tab9 = (true, points[8].point, points[8].slot);
        ci.tab10 = (true, points[9].point, points[9].slot);
        ci.tabKompas = (true, points[10].point, points[10].slot);
        ci.tabInventory = (true, points[11].point, points[11].slot);
        ci.SetOpenTabs(OpenTab.Tab1);

        for (int i = 0; i < points.Length; i++)
            points[i].slot!.Enable = false;

        ci.inventory = new(Client.TransferedItemSlot,
            ItemSlot.GetAllSlots(ci.root.Childs.Find("Slots bar", true) ?? new())
                .Select(s => (s, s.ID ?? 0)),
            ItemSlot.GetAllSlots(ci.root.Childs.Find("Inventory", true) ?? new())
                .Select(s => (s, s.ID ?? 0))
                .Where(s => s.Item2 < 27));

        ci.inventory.GetToolBar += () => ci.Player.Entity?.Data.Get("tool_slots") as ToolsBar? ?? new();
        ci.inventory.SetToolBar += t  => ci.Player.Entity?.Data.Set("tool_slots", t);

        ci.inventory.GetTransferredItem += () => ci.Player.Entity?.Data.Get("transferred_item") as ItemStack?;
        ci.inventory.SetTransferredItem += i  => ci.Player.Entity?.Data.Set("transferred_item", i);

        ci.inventory.GetPlayerInventory += () => ci.Player.Entity?.Data.Get("inventory") as PlayerInventory? ?? new();
        ci.inventory.SetPlayerInventory += t => ci.Player.Entity?.Data.Set("inventory", t);

        ci.creativeSlots = ItemSlot.GetAllSlots(ci.root.Childs.Find("Creative", true)!)
            .Select(s => (s, ((s.ID ?? 0) % 9, (s.ID ?? 0) / 9))).ToArray();

        foreach (var s in ci.creativeSlots)
            s.slot.Point!.OnClick += ci.CreativeSlot;

        ci.SetOfTabs(0);
        return (ci, ci.root);
    }

    public override void Update(UpdateArgs args)
    {
        if (!args.activeInventory)
            return;
        inventory.UpdateSlotsContent(args.time);
        if(Mouse.Scroll.Delta != 0)
        {
            scroll_offset -= Mouse.Scroll.Delta;
            if (scroll_offset < 0)
                scroll_offset = 0;
            if (scroll_offset > lines_of_items)
                scroll_offset = lines_of_items;
            var items = usedTab.Items;
            foreach (var s in creativeSlots.Select(s => (s.slot, ((s.index.y + scroll_offset) * 9) + s.index.x)))
                if (s.Item2 < items.Count)
                    s.slot.ItemStack = items[s.Item2];
                else
                    s.slot.ItemStack = null;
        }
    }

    private void TabOpenClose(SpacePoint obj, OpenTab tab, ClickEventButton args)
    {
        Tab = tab;
        if(tab == OpenTab.InventoryTab)
        {

        }
        else if (tab == OpenTab.SearchTab)
        {

        }
        else
        {
            scroll_offset = 0;
            usedTab = Register.CreativeTabs.List[(tabs_set_index * 10) + tab switch
            {
                OpenTab.Tab1 => 0,
                OpenTab.Tab2 => 1,
                OpenTab.Tab3 => 2,
                OpenTab.Tab4 => 3,
                OpenTab.Tab5 => 4,
                OpenTab.Tab6 => 5,
                OpenTab.Tab7 => 6,
                OpenTab.Tab8 => 7,
                OpenTab.Tab9 => 8,
                OpenTab.Tab10 => 9,
                _ => 0
            }];
            var items = usedTab.Items;
            lines_of_items = ((items.Count % 9 == 0) ? (items.Count / 9) : (items.Count / 9) + 1) - (creativeSlots.Length / 9);
            lines_of_items = lines_of_items < 0 ? 0 : lines_of_items;
            foreach (var s in creativeSlots.Select(s => (s.slot, (s.index.y * 9) + s.index.x)))
                if (s.Item2 < items.Count)
                    s.slot.ItemStack = items[s.Item2];
                else
                    s.slot.ItemStack = null;
        }
    }

    public void CreativeSlot(SpacePoint point, ClickEventButton args)
    {
        var slot_id = creativeSlots.Find(s => s.slot.Point == point, null).index;
        slot_id = (slot_id.x, slot_id.y + scroll_offset);
        var i = (slot_id.y * 9) + slot_id.x;
        ItemStack? itemStack = i < usedTab.Items.Count ? usedTab.Items[i] : null;
        if (itemStack.HasValue)
        {
            if(args == ClickEventButton.Left)
            {
                var old = Player.Entity?.Data.Get("transferred_item") as ItemStack?;
                if(old.HasValue)
                {
                    if (old == itemStack)
                        old = new(old.Value.Count + 1 > old.Value.Item.MaxStackCount(old.Value) ? old.Value.Count : old.Value.Count + 1, old.Value);
                    else
                        old = null;
                    Player.Entity?.Data.Set("transferred_item", old);
                    return;
                }
                else
                    Player.Entity?.Data.Set("transferred_item", new ItemStack(1, itemStack.Value));
            }
            else if(args == ClickEventButton.Scroll)
            {
                var old = Player.Entity?.Data.Get("transferred_item") as ItemStack?;
                if (old.HasValue)
                    if (old != itemStack)
                        return;
                Player.Entity?.Data.Set("transferred_item", new ItemStack(itemStack.Value.Item.MaxStackCount(itemStack.Value), itemStack.Value));
            }
            else if(args == ClickEventButton.Right)
            {
                var old = Player.Entity?.Data.Get("transferred_item") as ItemStack?;
                if (!old.HasValue)
                    return;
                if (old.Value.Count > 1)
                    old = new(old.Value.Count - 1, old.Value);
                else
                    old = null;
                Player.Entity?.Data.Set("transferred_item", old);
            }
        }
        else
        {
            if (args == ClickEventButton.Left)
            {
                Player.Entity?.Data.Set("transferred_item", itemStack);
                return;
            }
            else if (args == ClickEventButton.Right)
            {
                var old = Player.Entity?.Data.Get("transferred_item") as ItemStack?;
                if (!old.HasValue)
                    return;
                old = old.Value.Count - 1 > 0 ? new(old.Value.Count - 1, old.Value) : null;
                Player.Entity?.Data.Set("transferred_item", old);
                return;
            }
        }
    }

    public OpenTab Tab
    {
        get => GetOpenTabs();
        set
        {
            if (!tabTypes.Contains(value))
                throw new ArgumentException("Values overlap", (string)null!);
            var t = Tab;
            if (t == value)
                return;
            SetOpenTabs(value);
            root.RunEvent(value switch
            {
                OpenTab.InventoryTab => "inventoryTab",
                OpenTab.SearchTab => "searchTab",
                _ => "standardTab"
            });
        }
    }

    bool Tab1 { get => tab1.active; set => chane_mode(value, ref tab1); }
    bool Tab2 { get => tab2.active; set => chane_mode(value, ref tab2); }
    bool Tab3 { get => tab3.active; set => chane_mode(value, ref tab3); }
    bool Tab4 { get => tab4.active; set => chane_mode(value, ref tab4); }
    bool Tab5 { get => tab5.active; set => chane_mode(value, ref tab5); }
    bool Tab6 { get => tab6.active; set => chane_mode(value, ref tab6); }
    bool Tab7 { get => tab7.active; set => chane_mode(value, ref tab7); }
    bool Tab8 { get => tab8.active; set => chane_mode(value, ref tab8); }
    bool Tab9 { get => tab9.active; set => chane_mode(value, ref tab9); }
    bool Tab10 { get => tab10.active; set => chane_mode(value, ref tab10); }
    bool TabKompas { get => tabKompas.active; set => chane_mode(value, ref tabKompas); }
    bool TabInventory { get => tabInventory.active; set => chane_mode(value, ref tabInventory); }
    void chane_mode(bool value, ref (bool active, SpacePoint? point, ItemSlot? slot) data)
    {
        if (data.point is null)
            return;
        if (data.active == value)
            return;
        data.active = value;
        data.point.RunEvent(value ? "enable" : "disable");
    }

    /// <summary>
    /// Ustawia zakładki i ich ikony z danego zestawu
    /// </summary>
    void SetOfTabs(int index)
    {
        int tabs_count = Register.CreativeTabs.List.Count;
        int sets_count = tabs_count % 10 == 0 ? tabs_count / 10 : (tabs_count / 10) + 1;
        int in_set = sets_count - 1 < index ? 10 : (tabs_count % 10 == 0 ? 10 : tabs_count % 10);
        tabs_set_index = index;
        SetVisible(OpenTab.Tab1, in_set > 0);
        SetVisible(OpenTab.Tab2, in_set > 1);
        SetVisible(OpenTab.Tab3, in_set > 2);
        SetVisible(OpenTab.Tab4, in_set > 3);
        SetVisible(OpenTab.Tab5, in_set > 4);
        SetVisible(OpenTab.Tab6, in_set > 5);
        SetVisible(OpenTab.Tab7, in_set > 6);
        SetVisible(OpenTab.Tab8, in_set > 7);
        SetVisible(OpenTab.Tab9, in_set > 8);
        SetVisible(OpenTab.Tab10, in_set > 9);

        if (in_set > 0)
            SetIcon(OpenTab.Tab1, Register.CreativeTabs.List[(index * 10)]?.Icon);
        if (in_set > 1)
            SetIcon(OpenTab.Tab2, Register.CreativeTabs.List[(index * 10) + 1]?.Icon);
        if (in_set > 2)
            SetIcon(OpenTab.Tab3, Register.CreativeTabs.List[(index * 10) + 2]?.Icon);
        if (in_set > 3)
            SetIcon(OpenTab.Tab4, Register.CreativeTabs.List[(index * 10) + 3]?.Icon);
        if (in_set > 4)
            SetIcon(OpenTab.Tab5, Register.CreativeTabs.List[(index * 10) + 4]?.Icon);
        if (in_set > 5)
            SetIcon(OpenTab.Tab6, Register.CreativeTabs.List[(index * 10) + 5]?.Icon);
        if (in_set > 6)
            SetIcon(OpenTab.Tab7, Register.CreativeTabs.List[(index * 10) + 6]?.Icon);
        if (in_set > 7)
            SetIcon(OpenTab.Tab8, Register.CreativeTabs.List[(index * 10) + 7]?.Icon);
        if (in_set > 8)
            SetIcon(OpenTab.Tab9, Register.CreativeTabs.List[(index * 10) + 8]?.Icon);
        if (in_set > 9)
            SetIcon(OpenTab.Tab10, Register.CreativeTabs.List[(index * 10) + 9]?.Icon);
    }

    /// <summary>
    /// Ustawia przedmiot w ikonie zakładki
    /// </summary>
    void SetIcon(OpenTab tab, ItemStack? stack)
    {
        var slot = tab switch
        {
            OpenTab.Tab1 => tab1.slot,
            OpenTab.Tab2 => tab2.slot,
            OpenTab.Tab3 => tab3.slot,
            OpenTab.Tab4 => tab4.slot,
            OpenTab.Tab5 => tab5.slot,
            OpenTab.Tab6 => tab6.slot,
            OpenTab.Tab7 => tab7.slot,
            OpenTab.Tab8 => tab8.slot,
            OpenTab.Tab9 => tab9.slot,
            OpenTab.Tab10 => tab10.slot,
            OpenTab.InventoryTab => tabInventory.slot,
            OpenTab.SearchTab => tabKompas.slot,
            _ => null
        };
        if (slot is not null)
            slot.ItemStack = stack;
    }

    /// <summary>
    /// Ustawia czy dana zakładka jest widoczna
    /// </summary>
    void SetVisible(OpenTab tab, bool status)
    {
        var point = tab switch
        {
            OpenTab.Tab1 => tab1.point,
            OpenTab.Tab2 => tab2.point,
            OpenTab.Tab3 => tab3.point,
            OpenTab.Tab4 => tab4.point,
            OpenTab.Tab5 => tab5.point,
            OpenTab.Tab6 => tab6.point,
            OpenTab.Tab7 => tab7.point,
            OpenTab.Tab8 => tab8.point,
            OpenTab.Tab9 => tab9.point,
            OpenTab.Tab10 => tab10.point,
            OpenTab.InventoryTab => tabInventory.point,
            OpenTab.SearchTab => tabKompas.point,
            _ => null
        };
        if (point is not null)
            point.Active = status;
    }

    /// <summary>
    /// Bierze zestaw wrzystkich zakładek ustawionych jako aktywne
    /// </summary>
    /// <returns></returns>
    public OpenTab GetOpenTabs()
    {
        var s = OpenTab.None;

        if (Tab1)
            s |= OpenTab.Tab1;
        if (Tab2)
            s |= OpenTab.Tab2;
        if (Tab3)
            s |= OpenTab.Tab3;
        if (Tab4)
            s |= OpenTab.Tab4;
        if (Tab5)
            s |= OpenTab.Tab5;
        if (Tab6)
            s |= OpenTab.Tab6;
        if (Tab7)
            s |= OpenTab.Tab7;
        if (Tab8)
            s |= OpenTab.Tab8;
        if (Tab9)
            s |= OpenTab.Tab9;
        if (Tab10)
            s |= OpenTab.Tab10;
        if (TabKompas)
            s |= OpenTab.SearchTab;
        if (TabInventory)
            s |= OpenTab.InventoryTab;
        return s;
    }
    
    /// <summary>
    /// Ustawia które zakładki są ustawione jako aktywne
    /// </summary>
    /// <param name="tab"></param>
    public void SetOpenTabs(OpenTab tab)
    {
        Tab1 = tab.HasFlag(OpenTab.Tab1);
        Tab2 = tab.HasFlag(OpenTab.Tab2);
        Tab3 = tab.HasFlag(OpenTab.Tab3);
        Tab4 = tab.HasFlag(OpenTab.Tab4);
        Tab5 = tab.HasFlag(OpenTab.Tab5);
        Tab6 = tab.HasFlag(OpenTab.Tab6);
        Tab7 = tab.HasFlag(OpenTab.Tab7);
        Tab8 = tab.HasFlag(OpenTab.Tab8);
        Tab9 = tab.HasFlag(OpenTab.Tab9);
        Tab10 = tab.HasFlag(OpenTab.Tab10);
        TabKompas = tab.HasFlag(OpenTab.SearchTab);
        TabInventory = tab.HasFlag(OpenTab.InventoryTab);
    }

    [Flags]
    public enum OpenTab
    {
        None = 0,
        Tab1 = 1,
        Tab2 = 1 << 1,
        Tab3 = 1 << 2,
        Tab4 = 1 << 3,
        Tab5 = 1 << 4,
        Tab6 = 1 << 5,
        Tab7 = 1 << 6,
        Tab8 = 1 << 7,
        Tab9 = 1 << 8,
        Tab10 = 1 << 9,
        InventoryTab = 1 << 10,
        SearchTab = 1 << 11
    }
}
