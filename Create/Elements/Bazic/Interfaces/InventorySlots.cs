using Create.Conteiner;
using Create.Conteiner.Items;
using Create.Elements.Gui;
using Create.OpenGL.GUI;

namespace Create.Elements.Bazic.Interfaces;

public sealed class InventorySlots
{
    public event Func<ToolsBar>? GetToolBar;
    public event Action<ToolsBar>? SetToolBar;

    public event Func<ItemStack?>? GetTransferredItem;
    public event Action<ItemStack?>? SetTransferredItem;

    ToolsBar toolBar { get => GetToolBar?.Invoke() ?? new(); set => SetToolBar?.Invoke(value); }
    ItemStack? transferredItem { get => GetTransferredItem?.Invoke(); set => SetTransferredItem?.Invoke(value); }

    (ItemSlot slot, int id)[] toolBarArray, inventorySlotsArray;

    public InventorySlots(IEnumerable<(ItemSlot slot, int id)> toolBarSlots, IEnumerable<(ItemSlot slot, int id)> inventorySlots)
    {
        if(toolBarSlots == null)
            throw new ArgumentNullException(nameof(toolBarSlots));
        if (inventorySlots == null)
            throw new ArgumentNullException(nameof(inventorySlots));

        toolBarArray = toolBarSlots.ToArray();
        inventorySlotsArray = inventorySlots.ToArray();

        foreach (var s in toolBarArray)
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, false), a);
        foreach (var s in inventorySlotsArray)
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, true), a);
    }

    void SlotInteraction(SpacePoint point, (int id, bool main) id, ClickEventButton args)
    {
        var slot = point.Element as ItemSlot;
        if (slot is null)
            return;

        if (id.main)
        {
            
        }
        else
        {
            var tools = toolBar;
            var tmp = tools[id.id];
            tools[id.id] = transferredItem;
            transferredItem = tmp;
            toolBar = tools;
            UpdateSlotsContent();
        }
    }

    public void UpdateSlotsContent()
    {
        var tool = toolBar;

        foreach (var s in toolBarArray)
            s.slot.ItemStack = tool[s.id];
    }
}
