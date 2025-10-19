using Create.Elements.Bazic.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create.Conteiner.Items;

public struct ArmorSet : IItemContainer, IList<ItemStack?>, ISlotFilter
{
    StructArray.Count4<ItemStack?> items;
    public int Length => items.Count;

    public ItemStack? GetItem(int index) => items[index];
    public void SetItem(int index, ItemStack? item) => items[index] = item;
    public ItemStack? this[int index] { get => items[index]; set => items[index] = value; }

    public IEnumerator<ItemStack?> GetEnumerator() => ((IEnumerable<ItemStack?>)items).GetEnumerator();
    int ICollection<ItemStack?>.Count => Length;
    bool ICollection<ItemStack?>.IsReadOnly => false;
    void IList<ItemStack?>.RemoveAt(int index) => throw new NotImplementedException();
    void ICollection<ItemStack?>.Add(ItemStack? item) => throw new NotImplementedException();
    bool ICollection<ItemStack?>.Remove(ItemStack? item) => throw new NotImplementedException();
    void IList<ItemStack?>.Insert(int index, ItemStack? item) => throw new NotImplementedException();
    void ICollection<ItemStack?>.Clear() => items.Clear();
    bool ICollection<ItemStack?>.Contains(ItemStack? item) => items.Contains(item);
    void ICollection<ItemStack?>.CopyTo(ItemStack?[] array, int arrayIndex) => ((IList<ItemStack?>)items).CopyTo(array, arrayIndex);
    int IList<ItemStack?>.IndexOf(ItemStack? item) => ((IList<ItemStack?>)items).IndexOf(item);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool IsItemStackAllowed(ISlotFilter.IsAllowedData data)
    {
        var acces_data = (data.ItemStack.Item as IClothing)?.GetArmorPlacement(new() 
        {
            Entity = data.Player.Entity!,
            ItemStack = data.ItemStack
        });
        return acces_data?.HasFlag(data.SlotIndex switch
        {
            0 => IClothing.Placement.Head,
            1 => IClothing.Placement.Torso,
            2 => IClothing.Placement.Legs,
            3 => IClothing.Placement.Feet,
            _ => IClothing.Placement.None
        }) ?? false;
    }
}
