using Create.OpenGL.GUI;
using Create.Elements.Gui;
using Create.Conteiner.Items;
using Create.Conteiner;
using Create.Net;
using OpenTK.Graphics.OpenGL;
using Create.Input;
using Create.Linq;

namespace Create.Elements.Interfaces;

internal class CraftingTableInterface : UserInterface, IUserInterface<CraftingTableInterface>
{
    #nullable disable
    SpacePoint root;
    InventorySlots inventory;
    CraftingSlots slots;
    #nullable restore

    static (CraftingTableInterface status, SpacePoint point) IUserInterface<CraftingTableInterface>.LoadInterface(InterfaceCreatorArgs args)
    {
        CraftingTableInterface cti = new();
        cti.root = Assets.GetInterface("create:crafting-table");
        cti.slots = new();
        cti.inventory = new(Client.TransferedItemSlot,
            ItemSlot.GetAllSlots(cti.root.Childs.Find("Slots bar", true) ?? new())
                .Select(s => (s, s.ID ?? 0)),
            new[] { ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 1", true) ?? new()),
                    ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 2", true) ?? new()),
                    ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 3", true) ?? new()) }
                .SelectMany(r => r)
                .Select(s => (s, s.ID ?? 0)),
            ItemSlot.GetAllSlots(cti.root.Childs.Find("Crafting", true) ?? new())
                .Select(s => (s, s.ID ?? 0)).Where(s => s.Item2 < 9));

        cti.inventory.GetToolBar += () => cti.Player.Entity?.Data.Get("tool_slots") as ToolsBar? ?? new();
        cti.inventory.SetToolBar += t => cti.Player.Entity?.Data.Set("tool_slots", t);

        cti.inventory.GetTransferredItem += () => cti.Player.Entity?.Data.Get("transferred_item") as ItemStack?;
        cti.inventory.SetTransferredItem += i => cti.Player.Entity?.Data.Set("transferred_item", i);

        cti.inventory.GetPlayerInventory += () => cti.Player.Entity?.Data.Get("inventory") as PlayerInventory? ?? new();
        cti.inventory.SetPlayerInventory += t => cti.Player.Entity?.Data.Set("inventory", t);

        cti.inventory.GetContainer += () => cti.slots;
        cti.inventory.SetContainer += c => cti.slots = c as CraftingSlots ?? new();

        return (cti, cti.root)!;
    }

    public override void Update(UpdateArgs args)
    {
        if (!args.activeInventory)
            return;
        inventory.UpdateSlotsContent(args.time);
    }

    class CraftingSlots : IItemContainer
    {

        public bool AnyChanges()
        {
            bool ch = false;
            for (int i = 0; i < 9 && !ch; i++)
                if (@new[i] != old[i])
                    ch = true;
            old = @new;
            return ch;
        }
        public ItemStack? GetItem(int index) => @new[index];
        public void SetItem(int index, ItemStack? item) => @new[index] = item;
        public int Length => 9;
    }
}