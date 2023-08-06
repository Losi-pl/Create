using System.Collections;

namespace Create.Conteiner.Items;

public struct PlayerInventory : IItemContainer, IList<ItemStack?>
{
    StructArray.Count27<ItemStack?> items;

    public int Length => items.Count;
    public ItemStack? GetItem(int index) => items[index];
    public void SetItem(int index, ItemStack? item) => items[index] = item;
    public ItemStack? this[int x, int y] { get => items[(y * 9) + x]; set => items[(y * 9) + x] = value; }

    ItemStack? IList<ItemStack?>.this[int index] { get => items[index]; set => items[index] = value; }
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
}
