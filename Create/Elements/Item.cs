using Create.Conteiner;
using Create.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements;

/// <summary>
/// Baza do budowy itemów
/// </summary>
public abstract class Item : Baze
{
    //Ustawienie bazowego typu elementu na Item
    public sealed override Type ElementBazicType => typeof(Item);

    public virtual ItemModel GetItemModel(ItemStack itemStack, Net.Player player) => new();

    public virtual bool AreStacksEqual(ItemStack itemStack1, ItemStack itemStack2) =>
        (itemStack1.Item == itemStack2.Item) && (itemStack1.Type == itemStack2.Type) && (itemStack1.Meta == itemStack2.Meta);

    public virtual uint MaxStackCount(StackData stackData) => 64;

    public struct ItemModel
    {
        public IDrawable model;
        public (Color4 color, float progress)? statusBar;
    }

    public struct StackData
    {
        Item item;
        string meta;
        byte type;
        public static implicit operator StackData(ItemStack stack) => 
            new() { item = stack.Item, meta = stack.Meta, type = stack.Type };
    }
}