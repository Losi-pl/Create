using Create.Conteiner;
using Create.Conteiner.Items;
using Create.Elements.Gui;
using Create.Input;
using Create.Linq;
using Create.Net;
using Create.OpenGL.GUI;
using OneOf;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Linq;

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
    OneOf<QuickWithdrawal, MassTransfer, OneOf.Types.None, ItemPlaceing, LongWithrawl> unique_events = new OneOf.Types.None();
    const float DOUBLE_CLICK_TIME = 0.17f;

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
        var inv = playerInventory;
        var tools = toolBar;
        var container = containerItems;
        var transfered = transferredItem;
        
        switch (args)
        {
            case ClickEventButton.Left:
                if(Keyboard.LeftShift.Status || Keyboard.RightShift.Status)
                {
                    if (unique_events.IsT1)
                        MovingAllContainer();
                    else
                        MovingASlingleStack();
                }
                else
                {
                    if (unique_events.IsT0)
                        FillItemsInHand();
                    else
                        SingleSlotClick();
                }
                break;
            case ClickEventButton.Scroll:
                GetFullStackOfItem();
                break;
            case ClickEventButton.Right:
                SplitItemStack();
                break;
        }
        playerInventory = inv;
        toolBar = tools;
        transferredItem = transfered;
        containerItems = container;

        // Left Click
        void FillItemsInHand()
        {
            if (!unique_events.IsT0)
                return;
            if (unique_events.AsT0.slot_id != id)
                return;
            if (!transfered.HasValue)
                return;

            var maxCount = transfered.Value.Item.MaxStackCount(transfered.Value);
            var pozesed = transfered.Value.Count;
            foreach(var slot_id in slots_in_order())
            { //Take items from slots in order
                var @is = get_specyfic_item(slot_id);
                if (@is != transfered)
                    continue;
                if (pozesed + @is.Value.Count < maxCount)
                {
                    pozesed += @is.Value.Count;
                    set_specyfic_item(slot_id, null);
                }
                else if (pozesed + @is.Value.Count == maxCount)
                {
                    set_specyfic_item(slot_id, null);
                    pozesed = maxCount;
                }
                else
                {
                    set_specyfic_item(slot_id, new((@is.Value.Count + pozesed) - maxCount, @is.Value));
                    pozesed = maxCount;
                }
                if (pozesed >= maxCount)
                    break;
            } //Take items from slots in order
            transfered = new(pozesed, transfered.Value);
            unique_events = new OneOf.Types.None();

            IEnumerable<(int index, byte cont)> slots_in_order()
            {
                if (id.cont == 0)
                {
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                }
                if (id.cont == 1)
                {
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                }
                if (id.cont == 2)
                {
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                }
            }
        }
        void SingleSlotClick()
        {
            var slot_cont = get_item();
            if (!transfered.HasValue && slot_cont.HasValue)
            { //Move item from slot to hand
                transfered = slot_cont;
                set_item(null);
                unique_events = new QuickWithdrawal() { slot_id = id, maxItemCount = slot_cont!.Value.Item.MaxStackCount(slot_cont!.Value) };
            } //Move item from slot to hand
            else if (transfered.HasValue && !slot_cont.HasValue)
            { //Mode item from hand to a slot
                if (!is_stack_allowed(transfered.Value))
                    return;
                unique_events = new ItemPlaceing() { in_hand = transfered.Value, in_slot = null, 
                    slot_id = id, SplitMode = ItemPlaceing.SplitModeEnum.Equally };
                set_item(transfered);
                transfered = null;
            } //Mode item from hand to a slot
            else if (transfered.HasValue && slot_cont.HasValue)
            {
                if (transfered.Value == slot_cont.Value)
                { //Adding items to the slot
                    var maxCount = transfered.Value.Item.MaxStackCount(transfered.Value);
                    if (transfered.Value.Count + slot_cont.Value.Count > maxCount)
                    {

                        unique_events = new ItemPlaceing() { in_hand = transfered.Value, in_slot = slot_cont.Value, 
                            slot_id = id, SplitMode = ItemPlaceing.SplitModeEnum.Equally };
                        set_item(new(maxCount, slot_cont.Value));
                        transfered = new((transfered.Value.Count + slot_cont.Value.Count) - maxCount, slot_cont.Value);
                    }
                    else if (transfered.Value.Count + slot_cont.Value.Count <= maxCount)
                    {

                        unique_events = new ItemPlaceing() { in_hand = transfered.Value, in_slot = slot_cont.Value, 
                            slot_id = id, SplitMode = ItemPlaceing.SplitModeEnum.Equally };
                        set_item(new(slot_cont.Value.Count + transfered.Value.Count, slot_cont.Value));
                        transfered = null;
                    }
                    else
                        unique_events = new OneOf.Types.None();
                } //Adding items to the slot
                else
                { //Switching items between slot and hand
                    if (!is_stack_allowed(transfered.Value))
                        return;
                    set_item(transfered);
                    transfered = slot_cont;
                    unique_events = new OneOf.Types.None();
                } //Switching items between slot and hand
            }
            
        }

        // Left Click - Shift
        void MovingASlingleStack()
        {
            var sour_item = get_item();
            if (!sour_item.HasValue)
                return;
            unique_events = new MassTransfer() { focus = sour_item.Value, slot_id = id};
            var items_count = sour_item.Value.Count;
            var maxCount = sour_item.Value.Item.MaxStackCount(sour_item.Value);
            foreach (var slot_id in EnumeOfSlotsInOrder())
            { //Adding items to alredy existing stacks
                var dest_slot = get_specyfic_item(slot_id);
                if (dest_slot != sour_item)
                    continue;
                if (dest_slot.Value.Count >= maxCount)
                    continue;
                if (dest_slot.Value.Count + items_count > maxCount)
                {
                    set_specyfic_item(slot_id, new(maxCount, dest_slot.Value));
                    items_count -= maxCount - dest_slot.Value.Count;
                }
                else
                {
                    set_specyfic_item(slot_id, new(dest_slot.Value.Count + items_count, dest_slot.Value));
                    items_count = 0;
                    break;
                }
            } //Adding items to alredy existing stacks
            if (items_count > 0)
                foreach (var slot_id in EnumeOfSlotsInOrder())
                { //Adding new stacks in empty slots
                    if (get_specyfic_item(slot_id).HasValue)
                        continue;
                    if (!is_stack_allowed(sour_item.Value, slot_id))
                        continue;
                    if(items_count > maxCount)
                    {
                        set_specyfic_item(slot_id, new(maxCount, sour_item.Value));
                        items_count -= maxCount;
                    }
                    else
                    {
                        set_specyfic_item(slot_id, new(items_count, sour_item.Value));
                        items_count = 0;
                        break;
                    }
                } //Adding new stacks in empty slots

            if (items_count > 0)
                set_item(new(items_count, sour_item.Value));
            else
                set_item(null);

            IEnumerable<(int index, byte cont)> EnumeOfSlotsInOrder()
            {
                if (id.cont == 0)
                {
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                }
                if (id.cont == 1)
                {
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                }
                if (id.cont == 2)
                {
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                }
            }
        }
        void MovingAllContainer()
        {
            if (!unique_events.IsT1)
                return;
            if (unique_events.AsT1.slot_id != id)
                return;

            var filter = unique_events.AsT1.focus;
            uint items_count = 0;
            var maxCount = filter.Item.MaxStackCount(filter);

            foreach (var slot_id in slots_in_order_source())
            { //Counting all items in set container
                var slot_cont = get_specyfic_item(slot_id);
                if (!slot_cont.HasValue)
                    continue;
                if (filter != slot_cont)
                    continue;
                items_count += slot_cont.Value.Count;
            } //Counting all items in set container
            
            foreach (var slot_id in slots_in_order_dest())
            { //Adding items to alredy existing stacks
                var dest_slot = get_specyfic_item(slot_id);
                if (!dest_slot.HasValue)
                    continue;
                if (filter != dest_slot)
                    continue;
                if (dest_slot.Value.Count >= maxCount)
                    continue;
                if (dest_slot.Value.Count + items_count > maxCount)
                {
                    set_specyfic_item(slot_id, new(maxCount, dest_slot.Value));
                    items_count -= maxCount - dest_slot.Value.Count;
                }
                else
                {
                    set_specyfic_item(slot_id, new(dest_slot.Value.Count + items_count, dest_slot.Value));
                    items_count = 0;
                    break;
                }
            } //Adding items to alredy existing stacks
            if (items_count > 0)
                foreach (var slot_id in slots_in_order_dest())
                { //Adding new stacks to empty slots
                    if (get_specyfic_item(slot_id).HasValue)
                        continue;
                    if (!is_stack_allowed(filter, slot_id))
                        continue;
                    if (items_count > maxCount)
                    {
                        set_specyfic_item(slot_id, new(maxCount, filter));
                        items_count -= maxCount;
                    }
                    else
                    {
                        set_specyfic_item(slot_id, new(items_count, filter));
                        items_count = 0;
                        break;
                    }
                } //Adding new stacks to empty slots

            foreach (var slot_id in slots_in_order_source(true))
            { //Setting the source to only remaneing items
                var slot_cont = get_specyfic_item(slot_id);
                if (!slot_cont.HasValue)
                    continue;
                if (filter != slot_cont)
                    continue;
                if (items_count > 0)
                {
                    if (items_count > slot_cont.Value.Count)
                        items_count -= slot_cont.Value.Count;
                    else
                    {
                        set_specyfic_item(slot_id, new(items_count, slot_cont.Value));
                        items_count = 0;
                    }
                }
                else
                    set_specyfic_item(slot_id, null);
            } //Setting the source to only remaneing items
            unique_events = new OneOf.Types.None();

            IEnumerable<(int index, byte cont)> slots_in_order_source(bool reversed = false)
            {
                if(reversed)
                {
                    if (id.cont == 0)
                        for (int i = tools.Length - 1; i >= 0; i--)
                            yield return (i, 0);
                    if (id.cont == 1)
                        for (int i = inv.Length - 1; i >= 0; i--)
                            yield return (i, 1);
                    if (id.cont == 2)
                        for (int i = (container?.Length ?? -1); i >= 0; i--)
                            yield return (i, 2);
                }
                else
                {
                    if (id.cont == 0)
                        for (int i = 0; i < tools.Length; i++)
                            yield return (i, 0);
                    if (id.cont == 1)
                        for (int i = 0; i < inv.Length; i++)
                            yield return (i, 1);
                    if (id.cont == 2)
                        for (int i = 0; i < (container?.Length ?? 0); i++)
                            yield return (i, 2);
                } 
            }
            IEnumerable<(int index, byte cont)> slots_in_order_dest()
            {
                if (id.cont == 0)
                {
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                }
                if (id.cont == 1)
                {
                    for (int i = 0; i < (container?.Length ?? 0); i++)
                        yield return (i, 2);
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                }
                if (id.cont == 2)
                {
                    for (int i = 0; i < tools.Length; i++)
                        yield return (i, 0);
                    for (int i = 0; i < inv.Length; i++)
                        yield return (i, 1);
                }
            }
        }

        //Creative - Scroll
        void GetFullStackOfItem()
        {
            var slot_cont = get_item();
            if (transfered.HasValue)
                return;
            if (!slot_cont.HasValue)
                return;
            slot_cont = new(slot_cont.Value.Item.MaxStackCount(slot_cont.Value), slot_cont!.Value);
            transfered = slot_cont;
        }

        // Right Click
        void SplitItemStack()
        {
            if (unique_events.IsT3)
                return;
            ItemStack? slot_cont = get_item();
            if (!slot_cont.HasValue && !transfered.HasValue)
                return; // Do nothing obviously
            if (!slot_cont.HasValue && transfered.HasValue)
            { // Put item into empty slot
                unique_events = new ItemPlaceing() { in_hand = transfered.Value, in_slot = null,
                    slot_id = is_stack_allowed(transfered.Value) ? id : null, SplitMode = ItemPlaceing.SplitModeEnum.OnePerSlot };
            } // Put item into empty slot
            else if (slot_cont.HasValue && transfered.HasValue && slot_cont == transfered)
            { // Put into alredy partle filles slot
                unique_events = new ItemPlaceing() { in_hand = transfered.Value, in_slot = slot_cont.Value,
                    slot_id = id, SplitMode = ItemPlaceing.SplitModeEnum.OnePerSlot };
            } // Put into alredy partle filles slot
            else if (slot_cont.HasValue && !transfered.HasValue)
            { // Take half of the slot content
                var half = slot_cont.Value.Count % 2 == 0 ? slot_cont.Value.Count / 2 : (slot_cont.Value.Count / 2) + 1;
                transfered = half > 0 ? new(half, slot_cont.Value) : null;
                set_item(slot_cont!.Value.Count - half > 0 ? new(slot_cont!.Value.Count - half, slot_cont.Value) : null);
            } // Take half of the slot content
        }

        ItemStack? get_specyfic_item((int id, byte cont) id) => id.cont switch
        {
            0 => tools.Length > id.id ? tools.GetItem(id.id) : null,
            1 => inv.Length > id.id ? inv.GetItem(id.id) : null,
            2 => (container?.Length ?? 0) > id.id ? container!.GetItem(id.id) : null,
            _ => throw new()
        };
        ItemStack? get_item() => id.cont switch
        {
            0 => tools.Length > id.id ? tools.GetItem(id.id) : null,
            1 => inv.Length > id.id ? inv.GetItem(id.id) : null,
            2 => container.Length > id.id ? container.GetItem(id.id) : null,
            _ => throw new()
        };
        void set_specyfic_item((int id, byte cont) id, ItemStack? item)
        {
            switch (id.cont)
            {
                case 0: tools.SetItem(id.id, item); break;
                case 1: inv.SetItem(id.id, item); break;
                case 2: container?.SetItem(id.id, item); break;
            }
        }
        void set_item(ItemStack? item)
        {
            switch (id.cont)
            {
                case 0: tools.SetItem(id.id, item); break;
                case 1: inv.SetItem(id.id, item); break;
                case 2: container.SetItem(id.id, item); break;
            } 
        }
        bool is_stack_allowed(ItemStack itemStack,(int id, byte cont)? id_ = null)
        {
            var d = id_ ?? id;
            switch(d.cont)
            {
                case 0: return true;
                case 1: return true;
                case 2:
                    var filter = container as ISlotFilter;
                    if (filter is null)
                        return true;
                    if ((container?.Length ?? 0) <= d.id)
                        return true;
                    return filter.IsItemStackAllowed(new() { ItemStack = itemStack, Player = Client.Me, SlotIndex = d.id });
            }
            return true;
        }
    }

    void SlotEnter(SpacePoint point, (int id, byte cont) id)
    {
        AddSlotToTheUniqueEvent(id, get_item());

        ItemStack? get_item() => id.cont switch
        {
            0 => toolBar.Cast(tb => tb.Length > id.id ? tb.GetItem(id.id) : null),
            1 => playerInventory.Cast(pi => pi.Length > id.id ? pi.GetItem(id.id) : null),
            2 => containerItems.Cast(ct => ct.Length > id.id ? ct.GetItem(id.id) : null),
            _ => throw new()
        };
    }

    void AddSlotToTheUniqueEvent((int id, byte cont) id, ItemStack? slot_tem)
    {
        if (unique_events.IsT3)
            if ((unique_events.AsT3.SplitMode == ItemPlaceing.SplitModeEnum.Equally && Mouse.Left.Status) ||
                (unique_events.AsT3.SplitMode == ItemPlaceing.SplitModeEnum.OnePerSlot && Mouse.Right.Status))
            {
                var d = unique_events.AsT3;
                if (d.slot_id == id)
                    return;
                if(d.used_slots is null)
                {
                    d.used_slots = new();
                    unique_events = d;
                }
                else if (d.used_slots.Contains(id))
                    return;
                if(d.in_hand == slot_tem || !slot_tem.HasValue)
                    if(is_stack_allowed(d.in_hand))
                        d.used_slots.Add(id);
            }
        if(unique_events.IsT0 || unique_events.IsT4)
        {
            var max_count = unique_events.IsT0 ? unique_events.AsT0.maxItemCount : unique_events.AsT4.maxItemCount;
            var in_hand = transferredItem;
            if(in_hand == slot_tem)
            {
                if((in_hand?.Count ?? 0) + (slot_tem?.Count ?? 0) <= max_count)
                {
                    transferredItem = new((in_hand?.Count ?? 0) + (slot_tem?.Count ?? 0), in_hand!.Value);
                    set_item(null);
                }
                else
                {
                    transferredItem = new(max_count, in_hand!.Value);
                    set_item(new(slot_tem!.Value.Count - (max_count - in_hand!.Value.Count), in_hand!.Value));
                }
            }
        }

        void set_item(ItemStack? stack)
        {
            switch (id.cont)
            {
                case 0: { var tb = toolBar; tb.SetItem(id.id, stack); toolBar = tb; } break;
                case 1: { var pi = playerInventory; pi.SetItem(id.id, stack); playerInventory = pi; } break;
                case 2: { var ct = containerItems; ct.SetItem(id.id, stack); containerItems = ct; } break;
            }
        }
        bool is_stack_allowed(ItemStack itemStack)
        {
            switch (id.cont)
            {
                case 0: return true;
                case 1: return true;
                case 2:
                    var c = containerItems;
                    var filter = c as ISlotFilter;
                    if (filter is null)
                        return true;
                    if ((c?.Length ?? 0) <= id.id)
                        return true;
                    return filter.IsItemStackAllowed(new() { ItemStack = itemStack, Player = Client.Me, SlotIndex = id.id });
            }
            return true;
        }
    }

    void SlotExit(SpacePoint point, (int id, byte cont) id)
    {

    }

    public void UpdateSlotsContent(double time)
    {
        ToolsBar tool = toolBar;
        PlayerInventory inv = playerInventory;
        IItemContainer container = containerItems;
        bool changet = false;
        bool forced_hand_item = false;

        UniqueEventCountdown();
        ClearHardSelects();

        if (unique_events.IsT3)
            SpreadAllItemsOverTheSlots();

        if (changet)
            (toolBar, playerInventory, containerItems) = (tool as ToolsBar? ?? new(), inv as PlayerInventory? ?? new(), container);

        foreach (var s in toolBarArray.Where(s => !s.slot.HardSelected))
            s.slot.ItemStack = tool.Length > s.id ? tool.GetItem(s.id) : null;

        foreach (var s in inventorySlotsArray.Where(s => !s.slot.HardSelected))
            s.slot.ItemStack = inv.Length > s.id ? inv.GetItem(s.id) : null;

        foreach (var s in conteinerSlotsArray.Where(s => !s.slot.HardSelected))
            s.slot.ItemStack = container.Length > s.id ? container.GetItem(s.id) : null;
        
        if (!forced_hand_item)
            transferSlot.ItemStack = transferredItem;

        void ClearHardSelects()
        {
            for (int i = 0; i < toolBarArray.Length; i++)
                toolBarArray[i].slot.HardSelected = false;
            for (int i = 0; i < inventorySlotsArray.Length; i++)
                inventorySlotsArray[i].slot.HardSelected = false;
            for (int i = 0; i < conteinerSlotsArray.Length; i++)
                conteinerSlotsArray[i].slot.HardSelected = false;
            transferSlot.HardSelected = false;
        }
        void SpreadAllItemsOverTheSlots()
        {
            var eve = unique_events.AsT3;
            var maxCount = eve.in_hand.Item.MaxStackCount(eve.in_hand);
            if (eve.SplitMode == ItemPlaceing.SplitModeEnum.Equally)
            {
                if (eve.SlotCount > 1)
                {
                    Span<CalculatingData> values;
                    Span<int> incluted = stackalloc int[eve.SlotCount];
                    Span<int> overflowed = stackalloc int[eve.SlotCount];
                    int overflow_count = 0;

                    #pragma warning disable CS9081
                    unsafe { values = stackalloc CalculatingData[eve.SlotCount]; }
                    #pragma warning restore CS9081

                    {
                        var i = 0;
                        foreach (var slot in eve)
                        {
                            incluted[i] = i;
                            var itemStack = slot == eve.slot_id ? eve.in_slot : get_item(slot);
                            values[i].used_space = (itemStack?.Count ?? 0);
                            values[i].slot_id = slot;
                            values[i].free_space = maxCount - values[i].used_space;
                            ++i;
                        }
                    }

                    // Calculate amounts of items per slot;
                    var remaneing = eve.in_hand.Count;
                    var per_slot = (uint)(remaneing / incluted.Length);
                    var left_overs = (uint)(remaneing % incluted.Length);
                    bool there_are_problems = true;

                    while (there_are_problems)
                    {
                        for (int i = 0; i < incluted.Length; i++)
                        {
                            // Checking if this slot will overflow with extra items
                            if (values[incluted[i]].free_space < per_slot)
                            {
                                // Recalculate item spread
                                remaneing -= values[incluted[i]].free_space;
                                per_slot = (uint)(remaneing / (incluted.Length - 1));
                                left_overs = (uint)(remaneing % (incluted.Length - 1));

                                // Specifi this slot as full
                                overflowed[overflow_count++] = incluted[i];
                                cut_of_index(i, ref incluted);

                                break;
                            }
                        }

                        there_are_problems = false;
                    }

                    if (Mouse.Left.Up)
                    {
                        for (int i = 0; i < overflow_count; i++)
                        {
                            var slot_id = values[overflowed[i]].slot_id;
                            set_item(slot_id, new(maxCount, eve.in_hand));
                        }
                        for (int i = 0; i < incluted.Length; i++)
                        {
                            var slot_id = values[incluted[i]].slot_id;
                            var stack_count = values[incluted[i]].used_space + per_slot;
                            set_item(slot_id, stack_count > 0 ? new(stack_count, eve.in_hand) : null);
                        }
                        transferredItem = left_overs > 0 ? new(left_overs, eve.in_hand) : null;
                        unique_events = new OneOf.Types.None();
                    }
                    else
                    {
                        for (int i = 0; i < overflow_count; i++)
                        {
                            var slot_id = values[overflowed[i]].slot_id;
                            set_item_slot(slot_id, new(maxCount, eve.in_hand), true);
                        }
                        for (int i = 0; i < incluted.Length; i++)
                        {
                            var slot_id = values[incluted[i]].slot_id;
                            var stack_count = values[incluted[i]].used_space + per_slot;
                            set_item_slot(slot_id, stack_count > 0 ? new(stack_count, eve.in_hand) : null, true);
                        }
                        transferSlot.ItemStack = left_overs > 0 ? new(left_overs, eve.in_hand) : null;
                        forced_hand_item = true;
                    }
                    void cut_of_index(int index, ref Span<int> span)
                    {
                        for (int i = index; i < span.Length - 1; i++)
                            span[i] = span[i + 1];
                        span = span.Slice(0, span.Length - 1);
                    }
                }
                else if (eve.SlotCount == 1)
                {
                    if (Mouse.Left.Up)
                    {
                        var slot = eve.GetSlotAddress(0);
                        if (eve.in_hand.Count + (eve.in_slot?.Count ?? 0) > maxCount)
                        {
                        }
                        else
                        {
                            set_item(slot, new(eve.in_hand.Count + (eve.in_slot?.Count ?? 0), eve.in_hand));
                            transferredItem = null;
                        }
                        unique_events = new OneOf.Types.None();
                    }
                    else
                    {
                        var slot = eve.GetSlotAddress(0);
                        if (eve.in_hand.Count + (eve.in_slot?.Count ?? 0) > maxCount)
                        {
                            set_item_slot(slot, new(maxCount, eve.in_hand), true);
                        }
                        else
                        {
                            var item = new ItemStack(eve.in_hand.Count + (eve.in_slot?.Count ?? 0), eve.in_hand);
                            set_item_slot(slot, item, true);
                            transferSlot.ItemStack = null;
                        }
                    }
                }
            }
            else
            {
                if (eve.SlotCount > 1)
                {
                    var count = eve.in_hand.Count;
                    if(Mouse.Right.Up)
                    {
                        foreach (var slot_id in eve)
                        {
                            var slot_item = slot_id == eve.slot_id ? eve.in_slot : get_item(slot_id);
                            if (count > 0)
                            {
                                if (!slot_item.HasValue)
                                {
                                    set_item(slot_id, new(1, eve.in_hand));
                                    count--;
                                }
                                else if (slot_item.Value.Count + 1 <= maxCount)
                                {
                                    set_item(slot_id, new(slot_item.Value.Count + 1, eve.in_hand));
                                    count--;
                                }
                            }
                        }
                        if (count > 0)
                            transferredItem = new(count, eve.in_hand);
                        else
                            transferredItem = null;
                        unique_events = new OneOf.Types.None();
                    }
                    else
                    {
                        foreach (var slot_id in eve)
                        {
                            var slot_item = slot_id == eve.slot_id ? eve.in_slot : get_item(slot_id);
                            if (count > 0)
                            {
                                if (!slot_item.HasValue)
                                {
                                    set_item_slot(slot_id, new(1, eve.in_hand), true);
                                    count--;
                                }
                                else if (slot_item.Value.Count + 1 <= maxCount)
                                {
                                    set_item_slot(slot_id, new(slot_item.Value.Count + 1, eve.in_hand), true);
                                    count--;
                                }
                                else
                                    set_item_slot(slot_id, slot_item, true);
                            }
                            else
                                set_item_slot(slot_id, slot_item, true);
                        }
                        if (count > 0)
                            transferSlot.ItemStack = new(count, eve.in_hand);
                        else
                            transferSlot.ItemStack = null;
                    }
                    forced_hand_item = true;
                }
                else if (eve.SlotCount == 1)
                {
                    if (Mouse.Right.Up)
                    {
                        var slot = eve.GetSlotAddress(0);
                        if ((eve.in_slot?.Count ?? 0) + 1 <= maxCount)
                        {
                            set_item(slot, new((eve.in_slot?.Count ?? 0) + 1, eve.in_hand));
                            transferredItem = eve.in_hand.Count - 1 > 0 ? new(eve.in_hand.Count - 1, eve.in_hand) : null;
                        }
                        unique_events = new OneOf.Types.None();
                    }
                    else
                    {
                        var slot = eve.GetSlotAddress(0);
                        if ((eve.in_slot?.Count ?? 0) + 1 > maxCount)
                        {
                            set_item_slot(slot, eve.in_slot, true);
                            forced_hand_item = true;
                        }
                        else
                        {
                            set_item_slot(slot, new((eve.in_slot?.Count ?? 0) + 1, eve.in_hand), true);
                            transferSlot.ItemStack = eve.in_hand.Count - 1 > 0 ? new(eve.in_hand.Count - 1, eve.in_hand) : null;
                            forced_hand_item = true;
                        }
                    }
                }
            }
        }
        void UniqueEventCountdown()
        {
            if (unique_events.IsT0)
            {
                var eve = unique_events.AsT0;
                eve.timeSinceClick += time;
                if (eve.timeSinceClick > DOUBLE_CLICK_TIME)
                    unique_events = new LongWithrawl() { maxItemCount = eve.maxItemCount };
                else
                    unique_events = eve;
            }
            else if (unique_events.IsT1)
            {
                var eve = unique_events.AsT1;
                eve.timeSinceClick += time;
                if (eve.timeSinceClick > DOUBLE_CLICK_TIME)
                    unique_events = new OneOf.Types.None();
                else
                    unique_events = eve;
            }
        }
        void set_item_slot((int id, byte cont) id, ItemStack? stack, bool forced = false)
        {
            switch(id.cont)
            {
                case 0:
                    for (int i = 0; i < toolBarArray.Length; i++)
                        if (toolBarArray[i].id == id.id)
                            (toolBarArray[i].slot.ItemStack, toolBarArray[i].slot.HardSelected) = (stack, forced);
                    break;
                case 1:
                    for (int i = 0; i < inventorySlotsArray.Length; i++)
                        if (inventorySlotsArray[i].id == id.id)
                            (inventorySlotsArray[i].slot.ItemStack, inventorySlotsArray[i].slot.HardSelected) = (stack, forced);
                    break;
                case 2:
                    for (int i = 0; i < conteinerSlotsArray.Length; i++)
                        if (conteinerSlotsArray[i].id == id.id)
                            (conteinerSlotsArray[i].slot.ItemStack, conteinerSlotsArray[i].slot.HardSelected) = (stack, forced);
                    break;
            }
        }
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

    public void DefaultBinds(Player player, string inventoryKey = "inventory", string toolBarKey = "tool_slots", string handItemKey = "transferred_item")
    {
        DefaultBindPlayerInventory(player, inventoryKey);
        DefaultBindToolBar(player, toolBarKey);
        DefaultBindTransferredItem(player, handItemKey);
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
    public void DefaultBindContainer<T>(Player player, string key) where T : struct, IItemContainer
    {
        GetContainer += () => player.Entity?.Data.Get(key) as T? ?? default;
        SetContainer += t => player.Entity?.Data.Set(key, t as T? ?? default);
    }

    struct QuickWithdrawal
    {
        public (int id, byte cont) slot_id;
        public double timeSinceClick;
        public uint maxItemCount;
    }
    struct MassTransfer
    {
        public (int id, byte cont) slot_id;
        public ItemStack focus;
        public double timeSinceClick;
    }
    struct LongWithrawl
    {
        public uint maxItemCount;
    }
    struct ItemPlaceing
    {
        public (int id, byte cont)? slot_id;
        public ItemStack? in_slot;
        public ItemStack in_hand;
        public List<(int id, byte cont)> used_slots;
        public SplitModeEnum SplitMode;

        public IEnumerator<(int id, byte cont)> GetEnumerator()
        {
            if (slot_id.HasValue)
                yield return slot_id.Value;
            for (int i = 0; i < used_slots.Count; i++)
                yield return used_slots[i];
        }

        public int SlotCount => (slot_id.HasValue ? 1 : 0) + (used_slots?.Count ?? 0);
        public (int id, byte cont) GetSlotAddress(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            if (index > 0)
            {
                if (slot_id.HasValue)
                    index--;
                if ((used_slots?.Count ?? 0) >= index)
                    throw new IndexOutOfRangeException();
                return used_slots![index];
            }
            else
                return slot_id ?? (used_slots ?? throw new IndexOutOfRangeException())[0];
        }

        public enum SplitModeEnum { OnePerSlot, Equally }
    }
}

file struct CalculatingData
{
    public (int id, byte cont) slot_id;
    public uint free_space, used_space;
}