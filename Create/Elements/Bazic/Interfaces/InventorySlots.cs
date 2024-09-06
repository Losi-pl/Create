using Create.Conteiner;
using Create.Conteiner.Items;
using Create.Elements.Gui;
using Create.Input;
using Create.Linq;
using Create.Net;
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

    public event Func<IItemContainer>? GetContainer;
    public event Action<IItemContainer>? SetContainer;

    (ItemSlot slot, int id)[] toolBarArray, inventorySlotsArray, conteinerSlotsArray; ItemSlot transferSlot;
    ClickEventButton button = ClickEventButton.Unknown;
    List<(int id, byte cont)> slots = new();
    ItemStack transfered;
    double timeSinceClick = 0;

    ToolsBar toolBar { get => GetToolBar?.Invoke() ?? new(); set => SetToolBar?.Invoke(value); }
    PlayerInventory playerInventory { get => GetPlayerInventory?.Invoke() ?? new(); set => SetPlayerInventory?.Invoke(value); }
    ItemStack? transferredItem { get => GetTransferredItem?.Invoke(); set => SetTransferredItem?.Invoke(value); }
    IItemContainer containerItems { get => GetContainer?.Invoke() ?? IItemContainer.Empty; 
                                    set => SetContainer?.Invoke(value == IItemContainer.Empty ? null! : value); }

    public InventorySlots(ItemSlot transfered, IEnumerable<(ItemSlot slot, int id)> toolBarSlots, IEnumerable<(ItemSlot slot, int id)> inventorySlots)
    {
        ArgumentNullException.ThrowIfNull(inventorySlots, nameof(inventorySlots));
        ArgumentNullException.ThrowIfNull(toolBarSlots, nameof(toolBarSlots));
        ArgumentNullException.ThrowIfNull(transfered, nameof(transfered));

        transferSlot = transfered;
        toolBarArray = toolBarSlots.ToArray();
        inventorySlotsArray = inventorySlots.ToArray();
        conteinerSlotsArray = new (ItemSlot slot, int id)[0];

        foreach (var s in toolBarArray)
        {
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, 0), a);
            s.slot.Point!.OnEnter += p => SlotEnter(p, (s.id, 0));
            s.slot.Point!.OnExit += p => SlotExit(p, (s.id, 0));
        }
        foreach (var s in inventorySlotsArray)
        {
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, 1), a);
            s.slot.Point!.OnEnter += p => SlotEnter(p, (s.id, 1));
            s.slot.Point!.OnExit += p => SlotExit(p, (s.id, 1));
        }
    }

    public InventorySlots(ItemSlot transfered, IEnumerable<(ItemSlot slot, int id)> toolBarSlots, IEnumerable<(ItemSlot slot, int id)> inventorySlots, IEnumerable<(ItemSlot slot, int id)> containerSlots)
    {
        ArgumentNullException.ThrowIfNull(inventorySlots, nameof(inventorySlots));
        ArgumentNullException.ThrowIfNull(toolBarSlots, nameof(toolBarSlots));
        ArgumentNullException.ThrowIfNull(transfered, nameof(transfered));

        transferSlot = transfered;
        toolBarArray = toolBarSlots.ToArray();
        inventorySlotsArray = inventorySlots.ToArray();
        conteinerSlotsArray = containerSlots.ToArray();

        foreach (var s in toolBarArray)
        {
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, 0), a);
            s.slot.Point!.OnEnter += p => SlotEnter(p, (s.id, 0));
            s.slot.Point!.OnExit += p => SlotExit(p, (s.id, 0));
        }
        foreach (var s in inventorySlotsArray)
        {
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, 1), a);
            s.slot.Point!.OnEnter += p => SlotEnter(p, (s.id, 1));
            s.slot.Point!.OnExit += p => SlotExit(p, (s.id, 1));
        }
        foreach (var s in conteinerSlotsArray)
        {
            s.slot.Point!.OnClick += (p, a) => SlotInteraction(p, (s.id, 2), a);
            s.slot.Point!.OnEnter += p => SlotEnter(p, (s.id, 2));
            s.slot.Point!.OnExit += p => SlotExit(p, (s.id, 2));
        }
    }

    void SlotInteraction(SpacePoint point, (int id, byte cont) id, ClickEventButton args)
    {
        var slot = point.Element as ItemSlot;
        if (slot is null)
            return;
        IItemContainer inv = playerInventory;
        IItemContainer tools = toolBar;
        IItemContainer container = containerItems;
        var transfered = transferredItem;
        switch (args)
        {
            case ClickEventButton.Left:
                if (button != ClickEventButton.Unknown && button != ClickEventButton.Scroll)
                    return;
                ItemStack? sItem;
                if(Keyboard.LeftShift.Status || Keyboard.RightShift.Status)
                {
                    if (timeSinceClick > 0.17)
                    {
                        button = ClickEventButton.Scroll;
                        var con = id.cont switch { 0 => tools, 1 => inv, 2 => container, _ => inv };
                        sItem = get_item();
                        if (!sItem.HasValue)
                            return;
                        uint count = sItem.Value.Count, maxCount = sItem.Value.Item.MaxStackCount(sItem.Value);
                        for (int i = 0; i < con.Length && count > 0; i++)
                        {
                            var @is = con.Length > i ? con.GetItem(i) : null;
                            if (!@is.HasValue)
                                continue;
                            if (sItem != @is)
                                continue;
                            var free = maxCount - @is.Value.Count;
                            if (free == 0)
                                continue;
                            if (count > free)
                                (@is, count) = (new(maxCount, @is.Value), count - free);
                            else
                                (@is, count) = (new(@is.Value.Count + count, @is.Value), 0);
                            con.SetItem(i, @is);
                        }
                        for (int i = 0; i < con.Length && count > 0; i++)
                        {
                            if (con.Length > i ? con.GetItem(i).HasValue : false)
                                continue;
                            con.SetItem(i, new(count, sItem.Value));
                            count = 0;
                            break;
                        }
                        set_item(count > 0 ? new(count, sItem.Value) : null);
                        this.transfered = sItem.Value;
                    }
                    else if(button == ClickEventButton.Scroll)
                    {
                        IItemContainer con = id.cont switch { 0 => tools, 1 => inv, 2 => container, _ => inv };
                        IItemContainer src = id.cont switch { 0 => inv, 1 => tools, 2 => container, _ => tools };
                        uint count = 0;
                        for(int i = 0; i < src.Length; i++)
                        {
                            var @is = src.Length > i ? src.GetItem(i) : null;
                            if (!@is.HasValue)
                                continue;
                            if (@is != this.transfered)
                                continue;
                            count += @is.Value.Count;
                        }
                        var old_count = count;
                        var maxCount = this.transfered.Item.MaxStackCount(this.transfered);
                        for (int i = 0; i < con.Length && count > 0; i++)
                        {
                            var @is = con.Length > i ? con.GetItem(i) : null;
                            if (!@is.HasValue)
                                continue;
                            if (this.transfered != @is)
                                continue;
                            var free = maxCount - @is.Value.Count;
                            if (free == 0)
                                continue;
                            if (count > free)
                                (@is, count) = (new(maxCount, @is.Value), count - free);
                            else
                                (@is, count) = (new(@is.Value.Count + count, @is.Value), 0);
                            con.SetItem(i, @is);
                        }
                        for (int i = 0; i < con.Length && count > 0; i++)
                        {
                            if (con.Length > i ? con.GetItem(i).HasValue : false)
                                continue;
                            if (count > maxCount)
                            {
                                con.SetItem(i, new(maxCount, this.transfered));
                                count -= maxCount;
                            }
                            else
                            {
                                con.SetItem(i, new(count, this.transfered));
                                count = 0;
                            }
                        }
                        if(old_count > count)
                        {
                            count = old_count - count;
                            for(int i = src.Length - 1; i > -1 && count > 0; i--)
                            {
                                var @is = src.Length > i ? src.GetItem(i) : null;
                                if (@is != this.transfered)
                                    continue;
                                if (count < @is.Value.Count)
                                    (@is, count) = (new(@is.Value.Count - count, @is.Value), 0);
                                else
                                    (@is, count) = (null, count - @is!.Value.Count);
                                src.SetItem(i, @is);
                            }
                        }
                        button = ClickEventButton.Unknown;
                        return;
                    }
                }
                else
                {
                    if (timeSinceClick > 0.17)
                    {
                        sItem = get_item();
                        if (!sItem.HasValue && !transfered.HasValue)
                            return;
                        if (!sItem.HasValue && transfered.HasValue)
                        {
                            slots.Add(id);
                            this.transfered = transferredItem!.Value;
                            button = ClickEventButton.Left;
                        }
                        else if (sItem.HasValue && transfered.HasValue && sItem == transfered)
                        {
                            button = ClickEventButton.Left;
                            slots.Add(id);
                            this.transfered = transferredItem!.Value;
                            button = ClickEventButton.Left;
                        }
                        else
                        {
                            var tmp = sItem;
                            set_item(transfered);
                            transfered = tmp;
                        }
                    }
                    else
                    {
                        if (!transfered.HasValue)
                            return;
                        var maxCount = transfered.Value.Item.MaxStackCount(transfered.Value) - transfered.Value.Count;
                        var count = transfered.Value.Count;
                        for (int i = 0; i < inv.Length; i++)
                        {
                            var @is = inv.Length > i ? inv.GetItem(i) : null;
                            if (@is != transfered)
                                continue;
                            if (maxCount > @is.Value.Count)
                            {
                                count += @is.Value.Count;
                                maxCount -= @is.Value.Count;
                                inv.SetItem(i, null);
                            }
                            else if (maxCount > 0)
                            {
                                inv.SetItem(i, new(@is.Value.Count - maxCount, @is.Value));
                                count += maxCount;
                                maxCount -= maxCount;
                            }
                            else if (maxCount == 0)
                                break;
                        }
                        transfered = new(count, transfered.Value);
                    }
                }
                timeSinceClick = 0;
                break;
            case ClickEventButton.Scroll:
                if (button != ClickEventButton.Unknown)
                    return;
                sItem = get_item();
                if (transfered.HasValue)
                    return;
                if (!sItem.HasValue)
                    return;
                sItem = new(sItem.Value.Item.MaxStackCount(sItem.Value), sItem!.Value);
                transfered = sItem;
                break;
            case ClickEventButton.Right:
                if (button != ClickEventButton.Unknown)
                    return;
                sItem = get_item();
                if (!sItem.HasValue && !transfered.HasValue)
                    return;
                if (!sItem.HasValue && transfered.HasValue)
                {
                    slots.Add(id);
                    button = ClickEventButton.Right;
                    this.transfered = transferredItem!.Value;
                }
                else if (sItem.HasValue && transfered.HasValue && sItem == transfered)
                {
                    slots.Add(id);
                    button = ClickEventButton.Right;
                    this.transfered = transferredItem!.Value;
                }
                else if(sItem.HasValue && !transfered.HasValue)
                {
                    var half = sItem.Value.Count % 2 == 0 ? sItem.Value.Count / 2 : (sItem.Value.Count / 2) + 1;
                    transfered = new(half, sItem.Value);
                    set_item(new(sItem!.Value.Count - half, sItem.Value));
                }
                break;
        }
        playerInventory = inv as PlayerInventory? ?? new();
        toolBar = tools as ToolsBar? ?? new();
        transferredItem = transfered;
        containerItems = container;

        ItemStack? get_item() => id.cont switch
        {
            0 => tools.Length > id.id ? tools.GetItem(id.id) : null,
            1 => inv.Length > id.id ? inv.GetItem(id.id) : null,
            2 => container.Length > id.id ? container.GetItem(id.id) : null,
            _ => throw new()
        };
        void set_item(ItemStack? item)
        {
            switch (id.cont)
            {
                case 0: tools.SetItem(id.id, item); break;
                case 1: inv.SetItem(id.id, item); break;
                case 2: container.SetItem(id.id, item); break;
            } 
        }
    }

    void SlotEnter(SpacePoint point, (int id, byte cont) id)
    {
        if (!(button == ClickEventButton.Left || button == ClickEventButton.Right))
            return;
        if (slots.Contains(id))
            return;
        if(button == ClickEventButton.Left)
        {
            var s = get_item();
            if (s == transfered || !s.HasValue)
                 slots.Add(id);
        }
        if(button == ClickEventButton.Right)
        {
            var s = get_item();
            if (s == transfered || !s.HasValue)
                slots.Add(id);
        }

        ItemStack? get_item() => id.cont switch
        {
            0 => toolBar.Cast(tb => tb.Length > id.id ? tb.GetItem(id.id) : null),
            1 => playerInventory.Cast(pi => pi.Length > id.id ? pi.GetItem(id.id) : null),
            2 => containerItems.Cast(ct => ct.Length > id.id ? ct.GetItem(id.id) : null),
            _ => throw new()
        };
    }

    void SlotExit(SpacePoint point, (int id, byte cont) id)
    {

    }

    public void UpdateSlotsContent(double time)
    {
        IItemContainer tool = toolBar;
        IItemContainer inv = playerInventory;
        IItemContainer container = containerItems;
        bool changet = false;
        timeSinceClick += time;

        if (button == ClickEventButton.Left)
        {
            if (Mouse.Left.Up)
            {
                button = ClickEventButton.Unknown;
                if (slots.Count == 1)
                {
                    var sItem = get_item(slots[0]);
                    if (sItem.HasValue)
                    {
                        var maxStack = transfered.Item.MaxStackCount(transfered);
                        var sum = (sItem?.Count ?? 0) + transfered.Count;
                        uint ci, ct;
                        if (sum > maxStack)
                            (ci, ct) = (maxStack, sum - maxStack);
                        else
                            (ci, ct) = (sum, 0);
                        set_item(slots[0], new(ci, sItem!.Value));
                        transferredItem = ct > 0 ? new(ct, transfered) : null;
                    }
                    else
                    {
                        set_item(slots[0], transfered);
                        transferredItem = null;
                    }
                }
                else
                {
                    var slots_space = slots.Select(d => (get_slot(d), get_item(d), d))
                        .Select(s => (s.Item1, s.Item2, s.Item2.HasValue ?
                            s.Item2.Value.Item.MaxStackCount(s.Item2.Value) - s.Item2.Value.Count :
                            transfered.Item.MaxStackCount(transfered), s.d))
                        .Where(s => s.Item3 > 0)
                        .Numerate();

                    if (slots_space.Count() > transfered.Count)
                    {
                        foreach (var s in slots_space.Where(s => s.index < transfered.Count))
                            set_item(s.item.d, s.item.Item2.HasValue ?
                                    new ItemStack(s.item.Item2.Value.Count + 1, s.item.Item2.Value) :
                                    new ItemStack(1, transfered));
                        transferredItem = null;
                    }
                    else
                    {
                        Span<uint> forEvery = stackalloc uint[slots.Count];
                        uint rest = 0;
                        uint split = 0;

                        uint occupied = 0, occupied_count = 0, new_occupied = 0, new_occupied_count = 0;
                        do
                        {
                            rest = (uint)(transfered.Count % (slots.Count - occupied_count));
                            split = (uint)(transfered.Count / (slots.Count - occupied_count));

                            foreach (var s in slots_space)
                            {
                                if (forEvery[s.index] == 0)
                                    if (s.item.Item3 < split)
                                        forEvery[s.index] = s.item.Item3;
                            }
                            for (int i = 0; i < forEvery.Length; i++)
                            {
                                if (forEvery[i] > 0)
                                {
                                    new_occupied_count++;
                                    new_occupied += forEvery[i];
                                }
                            }
                        }
                        while (test_continue());
                        bool test_continue()
                        {
                            if (occupied_count != new_occupied_count)
                            {
                                occupied = new_occupied;
                                occupied_count = new_occupied_count;
                                return true;
                            }
                            return false;
                        }
                        foreach (var s in slots_space)
                        {
                            if (forEvery[s.index] == 0)
                                foreach (var s_ in s.item.Item1)
                                    set_item(s.item.d, s.item.Item2.HasValue ?
                                        new(s.item.Item2!.Value.Count + split, transfered) :
                                        new(split, transfered));
                            else
                                foreach (var s_ in s.item.Item1)
                                    set_item(s.item.d, s.item.Item2.HasValue ?
                                        new(s.item.Item2!.Value.Count + split, transfered) :
                                        new(split, transfered));
                        }
                        transferredItem = rest > 0 ? new(rest, transfered) : null;
                    }
                }
                foreach (var s in slots)
                    foreach (var s_ in get_slot(s))
                        s_.HardSelected = false;
                slots.Clear();
            }
            else if (slots.Count == 1)
            {
                var sItem = get_item(slots[0]);
                if(sItem.HasValue)
                {
                    var maxStack = transfered.Item.MaxStackCount(transfered);
                    var sum = (sItem?.Count ?? 0) + transfered.Count;
                    uint ci, ct;
                    if (sum > maxStack)
                        (ci, ct) = (maxStack, sum - maxStack);
                    else
                        (ci, ct) = (sum, 0);
                    foreach (var s in get_slot(slots[0]))
                        (s.ItemStack, s.HardSelected) = (new(ci, sItem!.Value), true);
                    transferSlot.ItemStack = ct > 0 ? new(ct, transfered) : null;
                }
                else
                {
                    foreach (var s in get_slot(slots[0]))
                        (s.ItemStack, s.HardSelected) = (transfered, true);
                    transferSlot.ItemStack = null;
                }
            }
            else
            {
                var slots_space = slots.Select(d => (get_slot(d), get_item(d)))
                    .Select(s => (s.Item1, s.Item2, s.Item2.HasValue ?
                        s.Item2.Value.Item.MaxStackCount(s.Item2.Value) - s.Item2.Value.Count :
                        transfered.Item.MaxStackCount(transfered)))
                    .Where(s => s.Item3 > 0)
                    .Numerate();

                if (slots_space.Count() > transfered.Count)
                {
                    foreach(var s in slots_space.Where(s => s.index < transfered.Count))
                        foreach(var s_ in s.item.Item1)
                        {
                            s_.HardSelected = true;
                            s_.ItemStack = s.item.Item2.HasValue ? 
                                new ItemStack(s.item.Item2.Value.Count + 1, s.item.Item2.Value) :
                                new ItemStack(1, transfered);
                        }
                    transferSlot.ItemStack = null;
                }
                else
                {
                    Span<uint> forEvery = stackalloc uint[slots.Count];
                    uint rest = 0;
                    uint split = 0;

                    uint occupied = 0, occupied_count = 0, new_occupied = 0, new_occupied_count = 0;
                    do
                    {
                        rest = (uint)(transfered.Count % (slots.Count - occupied_count));
                        split = (uint)(transfered.Count / (slots.Count - occupied_count));

                        foreach (var s in slots_space)
                        {
                            if (forEvery[s.index] == 0)
                                if (s.item.Item3 < split)
                                    forEvery[s.index] = s.item.Item3;
                        }
                        for (int i = 0; i < forEvery.Length; i++)
                        {
                            if (forEvery[i] > 0)
                            {
                                new_occupied_count++;
                                new_occupied += forEvery[i];
                            }
                        }
                    }
                    while (test_continue());
                    bool test_continue()
                    {
                        if (occupied_count != new_occupied_count)
                        {
                            occupied = new_occupied;
                            occupied_count = new_occupied_count;
                            return true;
                        }
                        return false;
                    }
                    foreach (var s in slots_space)
                    {
                        var new_item = forEvery[s.index] > 0 ?
                            (s.item.Item2.HasValue ?
                                new(s.item.Item2!.Value.Count + forEvery[s.index], transfered.Item, transfered.Type, transfered.Meta) :
                                new ItemStack(split, transfered)) :
                            (s.item.Item2.HasValue ?
                                new(s.item.Item2!.Value.Count + split, transfered) :
                                new(split, transfered));

                        foreach (var s_ in s.item.Item1)
                            (s_.ItemStack, s_.HardSelected) = (new_item, true);
                    }
                    transferSlot.ItemStack = rest > 0 ? new(rest, transfered) : null;
                }
            }
        }
        if (button == ClickEventButton.Right)
        {
            if (Mouse.Right.Up)
            {
                var enume = slots.Where(s => get_item(s) is ItemStack @is ? @is.Item.MaxStackCount(@is) > @is.Count : true);
                if (enume.Count() > transfered.Count)
                {
                    foreach (var s in enume.Numerate().Where(i => i.index < transfered.Count))
                    {
                        var @is = get_item(s.item);
                        @is = @is.HasValue ? new(@is.Value.Count + 1, @is.Value) :
                            new(1, transfered);
                        set_item(s.item, @is);
                    }
                    transferredItem = null;
                }
                else
                {
                    uint c = transfered.Count;
                    foreach (var s in enume)
                    {
                        var @is = get_item(s);
                        @is = @is.HasValue ? new(@is.Value.Count + 1, @is.Value) :
                            new(1, transfered);
                        set_item(s, @is);
                        c--;
                    }
                    transferredItem = c > 0 ? new(c, transfered) : null;
                }
                button = ClickEventButton.Unknown;
                foreach (var s in slots)
                    foreach (var s_ in get_slot(s))
                        s_.HardSelected = false;
                slots.Clear();
            }
            else
            {
                var enume = slots.Where(s => get_item(s) is ItemStack @is ? @is.Item.MaxStackCount(@is) > @is.Count : true);
                if (enume.Count() > transfered.Count)
                {
                    foreach(var s in enume.Numerate().Where(i => i.index < transfered.Count))
                    {
                        var @is = get_item(s.item);
                        @is = @is.HasValue ? new(@is.Value.Count + 1, @is.Value) :
                            new(1, transfered);
                        foreach (var si in get_slot(s.item))
                            (si.ItemStack, si.HardSelected) = (@is, true);
                    }
                    transferSlot.ItemStack = null;
                }
                else
                {
                    uint c = transfered.Count;
                    foreach (var s in enume)
                    {
                        var @is = get_item(s);
                        @is = @is.HasValue ? new(@is.Value.Count + 1, @is.Value) :
                            new(1, transfered);
                        foreach (var si in get_slot(s))
                            (si.ItemStack, si.HardSelected) = (@is, true);
                        c--;
                    }
                    transferSlot.ItemStack = c > 0 ? new(c, transfered) : null;
                }
            }
        }

        if (changet)
            (toolBar, playerInventory, containerItems) = (tool as ToolsBar? ?? new(), inv as PlayerInventory? ?? new(), container);

        foreach (var s in toolBarArray.Where(s => !slots.Contains((s.id, 0))))
            s.slot.ItemStack = tool.Length > s.id ? tool.GetItem(s.id) : null;

        foreach (var s in inventorySlotsArray.Where(s => !slots.Contains((s.id, 1))))
            s.slot.ItemStack = inv.Length > s.id ? inv.GetItem(s.id) : null;

        foreach (var s in conteinerSlotsArray.Where(s => !slots.Contains((s.id, 2))))
            s.slot.ItemStack = container.Length > s.id ? container.GetItem(s.id) : null;
        if (button == ClickEventButton.Unknown)
            transferSlot.ItemStack = transferredItem;

        IEnumerable<ItemSlot> get_slot((int id, byte cont) id) => id.cont switch
        {
            0 => toolBarArray.Where(s => s.id == id.id).Select(s => s.slot),
            1 => inventorySlotsArray.Where(s => s.id == id.id).Select(s => s.slot),
            2 => conteinerSlotsArray.Where(s => s.id == id.id).Select(s => s.slot),
            _ => throw new()
        };
        ItemStack? get_item((int id, byte cont) id) => id.cont switch
        {
            0 => tool.Length > id.id ? tool.GetItem(id.id) : null,
            1 => inv.Length > id.id ? inv.GetItem(id.id) : null,
            2 => container.Length > id.id ? container.GetItem(id.id) : null,
            _ => throw new()
        };
        void set_item((int id, byte cont) id, ItemStack? stack)
        {
            switch (id.cont)
            {
                case 0: tool.SetItem(id.id, stack); break;
                case 1: inv.SetItem(id.id, stack); break;
                case 2: container.SetItem(id.id, stack); break;
            } 
            changet = true;
        }
    }


    public void DefaultBindPlayerInventory(Player player, string key = "inventory")
    {
        GetPlayerInventory += () => player.Entity?.Data.Get(key) as PlayerInventory? ?? default;
        SetPlayerInventory += t => player.Entity?.Data.Set(key, t);
    }
    public void DefaultBindToolBar(Player player, string key = "tool_slots")
    {
        GetToolBar += () => player.Entity?.Data.Get(key) as ToolsBar? ?? default;
        SetToolBar += t => player.Entity?.Data.Set(key, t);
    }
    public void DefaultBindTransferredItem(Player player, string key = "transferred_item")
    {
        GetTransferredItem += () => player.Entity?.Data.Get(key) as ItemStack?;
        SetTransferredItem += t => player.Entity?.Data.Set(key, t);
    }
}
