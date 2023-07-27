using System.Collections;
using System.ComponentModel;

namespace Create.Conteiner.Items;

public struct ToolsBar : IItemContainer, IList<ItemStack?>
{
    private ItemStack? i0, i1, i2, i3, i4, i5, i6, i7, i8;
    public int Length => 9;

    public ItemStack? GetItem(int index) => get(index);
    public void SetItem(int index, ItemStack? item) => set(index, item);
    public ItemStack? this[int index] { get => get(index); set => set(index, value); }

    private ItemStack? get(int index)
    {
        switch (index)
        {
            case 0: return i0;
            case 1: return i1;
            case 2: return i2;
            case 3: return i3;
            case 4: return i4;
            case 5: return i5;
            case 6: return i6;
            case 7: return i7;
            case 8: return i8;
            default: throw new IndexOutOfRangeException("Index must be in range [0..8]");
        }
    }
    private void set(int index, ItemStack? item)
    {
        switch (index)
        {
            case 0: i0 = item; break;
            case 1: i1 = item; break;
            case 2: i2 = item; break;
            case 3: i3 = item; break;
            case 4: i4 = item; break;
            case 5: i5 = item; break;
            case 6: i6 = item; break;
            case 7: i7 = item; break;
            case 8: i8 = item; break;
            default: throw new IndexOutOfRangeException("Index must be in range [0..8]");
        }
    }
    public IEnumerator<ItemStack?> GetEnumerator()
    {
        yield return i0;
        yield return i1;
        yield return i2;
        yield return i3;
        yield return i4;
        yield return i5;
        yield return i6;
        yield return i7;
        yield return i8;
    }

    int ICollection<ItemStack?>.Count => Length;
    bool ICollection<ItemStack?>.IsReadOnly => false;
    void IList<ItemStack?>.RemoveAt(int index) => throw new NotImplementedException();
    void ICollection<ItemStack?>.Add(ItemStack? item) => throw new NotImplementedException();
    bool ICollection<ItemStack?>.Remove(ItemStack? item) => throw new NotImplementedException();
    void IList<ItemStack?>.Insert(int index, ItemStack? item) => throw new NotImplementedException();
    void ICollection<ItemStack?>.Clear()
    {
        i0 = null; i1 = null; i2 = null; i3 = null; i4 = null; i5 = null; i6 = null; i7 = null; i8 = null;
    }
    bool ICollection<ItemStack?>.Contains(ItemStack? item)
    {
        if (i0 == item) return true;
        if (i1 == item) return true;
        if (i2 == item) return true;
        if (i3 == item) return true;
        if (i4 == item) return true;
        if (i5 == item) return true;
        if (i6 == item) return true;
        if (i7 == item) return true;
        if (i8 == item) return true;

        return false;
    }
    void ICollection<ItemStack?>.CopyTo(ItemStack?[] array, int arrayIndex)
    {
        if (array.Length > arrayIndex) array[arrayIndex] = i0;
        if (array.Length > arrayIndex + 1) array[arrayIndex + 1] = i1;
        if (array.Length > arrayIndex + 2) array[arrayIndex + 2] = i2;
        if (array.Length > arrayIndex + 3) array[arrayIndex + 3] = i3;
        if (array.Length > arrayIndex + 4) array[arrayIndex + 4] = i4;
        if (array.Length > arrayIndex + 5) array[arrayIndex + 5] = i5;
        if (array.Length > arrayIndex + 6) array[arrayIndex + 6] = i6;
        if (array.Length > arrayIndex + 7) array[arrayIndex + 7] = i7;
        if (array.Length > arrayIndex + 8) array[arrayIndex + 8] = i8;
    }
    int IList<ItemStack?>.IndexOf(ItemStack? item)
    {
        if (i0 == item) return 0;
        if (i1 == item) return 1;
        if (i2 == item) return 2;
        if (i3 == item) return 3;
        if (i4 == item) return 4;
        if (i5 == item) return 5;
        if (i6 == item) return 6;
        if (i7 == item) return 7;
        if (i8 == item) return 8;
        return -1;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
