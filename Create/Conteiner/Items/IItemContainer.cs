using System.Collections;

namespace Create.Conteiner.Items;

public interface IItemContainer
{
    static readonly IItemContainer empty = new Empty();
    
    public ItemStack? GetItem(int index);
    public void SetItem(int index, ItemStack? item);
    public int Length { get; }
    public static IItemContainer Empty => empty;
}

file struct Empty : IItemContainer, IList<ItemStack?>
{
    // IList<ItemStack?>
    ItemStack? IList<ItemStack?>.this[int index] { get => null; set { } }
    int ICollection<ItemStack?>.Count => 0;
    bool ICollection<ItemStack?>.IsReadOnly => true;
    void ICollection<ItemStack?>.Add(ItemStack? item) => throw new NotImplementedException();
    void ICollection<ItemStack?>.Clear() => throw new NotImplementedException();
    bool ICollection<ItemStack?>.Contains(ItemStack? item) => throw new NotImplementedException();
    void ICollection<ItemStack?>.CopyTo(ItemStack?[] array, int arrayIndex) { }
    IEnumerator<ItemStack?> IEnumerable<ItemStack?>.GetEnumerator() => Enumerable.Empty<ItemStack?>().GetEnumerator();
    int IList<ItemStack?>.IndexOf(ItemStack? item) => -1;
    void IList<ItemStack?>.Insert(int index, ItemStack? item) => throw new NotImplementedException();
    bool ICollection<ItemStack?>.Remove(ItemStack? item) => throw new NotImplementedException();
    void IList<ItemStack?>.RemoveAt(int index) => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => Enumerable.Empty<ItemStack?>().GetEnumerator();

    // IItemContainer
    public ItemStack? GetItem(int index) => null;
    public void SetItem(int index, ItemStack? item) { }
    public int Length { get => 0; }
}