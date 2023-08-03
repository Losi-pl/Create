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

    /// <summary>
    /// Generuje model przedmiotu
    /// </summary>
    /// <param name="itemStack"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual ItemModel GetItemModel(ItemStack itemStack, Net.Player player) => new();

    /// <summary>
    /// Sprawdza czy dane są takie same nie licząc ilości
    /// </summary>
    public virtual bool AreStacksEqual(ItemStack itemStack1, ItemStack itemStack2) =>
        (itemStack1.Item == itemStack2.Item) && (itemStack1.Type == itemStack2.Type) && (itemStack1.Meta == itemStack2.Meta);
    
    /// <summary>
    /// Maksymalna ilość przedmiotów w pojedyńczym staku
    /// </summary>
    public virtual uint MaxStackCount(StackData stackData) => 64;

    /// <summary>
    /// Używany w metodzie <see cref="GetItemModel(ItemStack, Net.Player)"/>
    /// </summary>
    public struct ItemModel
    {
        public IDrawable model;
        public (Color4 color, float progress)? statusBar;
    }
    /// <summary>
    /// Używany w metodzie <see cref="MaxStackCount(StackData)"/>
    /// </summary>
    public struct StackData
    {
        Item item;
        string meta;
        byte type;
        public static implicit operator StackData(ItemStack stack) => 
            new() { item = stack.Item, meta = stack.Meta, type = stack.Type };
    }
}