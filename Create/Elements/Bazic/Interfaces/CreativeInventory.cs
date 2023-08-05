using Create.OpenGL.GUI;
using Create.Elements.Gui;
using Create.Conteiner.Items;
using Create.Conteiner;
using Create.Net;

namespace Create.Elements.Interfaces;

internal class CreativeInventory : UserInterface, IUserInterface<CreativeInventory>
{
    readonly static OpenTab[] tabTypes = Enum.GetValues<OpenTab>();
    (bool active, SpacePoint? point, ItemSlot? slot) tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9, tab10, tabInventory, tabKompas;
    #nullable disable
    SpacePoint root;
    Net.Player player;
    InventorySlots inventory;
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
            .ConvertAll(n =>
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
            points[i].slot!.ItemStack = new((i % 3) switch
            {
                0 => Blocks.STONE,
                1 => Blocks.DIRT,
                _ => Blocks.GRASS_BLOCK
            });

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

        return (ci, ci.root);
    }

    public override void Update(UpdateArgs args)
    {
        inventory.UpdateSlotsContent(args.time);
    }

    private void TabOpenClose(SpacePoint obj, OpenTab tab, ClickEventButton args) => Tab = tab;

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
        Tab2 = 2,
        Tab3 = 4,
        Tab4 = 8,
        Tab5 = 16,
        Tab6 = 32,
        Tab7 = 64,
        Tab8 = 128,
        Tab9 = 256,
        Tab10 = 512,
        InventoryTab = 1024,
        SearchTab = 2048
    }
}
