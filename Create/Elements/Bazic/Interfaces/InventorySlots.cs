using Create.Conteiner;
using Create.Conteiner.Items;
using Create.Elements.Gui;
using Create.OpenGL.GUI;

namespace Create.Elements.Interfaces;

public sealed class InventorySlots
{
    public event Func<ToolsBar>? GetToolBar;
    public event Action<ToolsBar>? SetToolBar;

    public event Func<ItemStack?>? GetTransferredItem;
    public event Action<ItemStack?>? SetTransferredItem;

    public event Func<PlayerInventory>? GetPlayerInventory;
    public event Action<PlayerInventory>? SetPlayerInventory;

    ToolsBar toolBar { get => GetToolBar?.Invoke() ?? new(); set => SetToolBar?.Invoke(value); }
    PlayerInventory playerInventory { get => GetPlayerInventory?.Invoke() ?? new(); set => SetPlayerInventory?.Invoke(value); }
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
        var inv = playerInventory;
        var tools = toolBar;
        var transfered = transferredItem;
        switch (args)
        {
            case ClickEventButton.Left:
                var sItem = get_item();
                if (sItem.HasValue && transfered.HasValue && (sItem == transfered))
                {
                    var maxStack = sItem.Value.Item.MaxStackCount(sItem.Value);

                    var sum = sItem!.Value.Count + transfered!.Value.Count;
                    uint ci, ct;
                    if (sum > maxStack)
                        (ci, ct) = (maxStack, sum - maxStack);
                    else
                        (ci, ct) = (sum, 0);
                    set_item(new(ci, sItem.Value.Item, sItem.Value.Type, sItem.Value.Meta));
                    transfered = ct > 0 ? new(ct, sItem.Value.Item, sItem.Value.Type, sItem.Value.Meta) : null;
                }
                else
                {
                    var tmp = sItem;
                    set_item(transfered);
                    transfered = tmp;
                }
                break;
            case ClickEventButton.Scroll:
                sItem = get_item();
                if (transfered.HasValue)
                    return;
                if (!sItem.HasValue)
                    return;
                sItem = new(sItem.Value.Item.MaxStackCount(sItem.Value), 
                    sItem!.Value.Item, sItem.Value.Type, sItem.Value.Meta);
                transfered = sItem;
                break;
            case ClickEventButton.Right:
                sItem = get_item();
                if (!sItem.HasValue && !transfered.HasValue)
                    return;
                if(!sItem.HasValue && transfered.HasValue)
                {
                    sItem = new(transfered.Value.Item, transfered.Value.Type, transfered.Value.Meta);
                    set_item(sItem);
                    transfered = transfered.Value.Count == 1 ? null :
                        new(transfered.Value.Count - 1, 
                            transfered.Value.Item, 
                            transfered.Value.Type, 
                            transfered.Value.Meta);
                }
                else if (sItem.HasValue && transfered.HasValue)
                {
                    if (sItem != transfered)
                        return;
                    if (sItem?.Count >= sItem?.Item.MaxStackCount(sItem.Value))
                        return;

                    transfered = transfered!.Value.Count == 1 ? null :
                        new(transfered.Value.Count - 1, transfered.Value.Item, transfered.Value.Type, transfered.Value.Meta);
                    set_item(new(sItem!.Value.Count + 1, sItem.Value.Item, sItem.Value.Type, sItem.Value.Meta));
                }
                else if(sItem.HasValue && !transfered.HasValue)
                {
                    var half = sItem.Value.Count % 2 == 0 ? sItem.Value.Count / 2 : (sItem.Value.Count / 2) + 1;
                    transfered = new(half, sItem.Value.Item, sItem.Value.Type, sItem.Value.Meta);
                    set_item(new(sItem!.Value.Count - half, sItem.Value.Item, sItem.Value.Type, sItem.Value.Meta));
                }
                break;
        }
        transferredItem = transfered;
        playerInventory = inv;
        toolBar = tools;

        ItemStack? get_item()
        {
            if (id.main)
                return inv.GetItem(id.id);
            else
                return tools[id.id];
        }
        void set_item(ItemStack? item)
        {
            if (id.main)
                inv.SetItem(id.id, item);
            else
                tools[id.id] = item;
        }
    }

    public void UpdateSlotsContent()
    {
        var tool = toolBar;
        var inv = playerInventory;

        foreach (var s in toolBarArray)
            s.slot.ItemStack = tool[s.id];

        foreach (var s in inventorySlotsArray)
            s.slot.ItemStack = inv.GetItem(s.id);
    }
}
