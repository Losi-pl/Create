using Create.Conteiner;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.Space;
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
    /// Operacja wykonywana po kliknięcu przedmiotem na blok jeżeli blok nie wykona reakcji
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual bool OnClick(OnClickArgs args) => false;
    
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
    /// <summary>
    /// Używany w metodzie <see cref="OnClick(OnClickArgs)"/>
    /// </summary>
    public struct OnClickArgs
    {
        public ClickEventButton Button;
        public Block.OnClickArgs? BlockArgs;
        public Net.Player Player;
        public World World;
        public (int Slot, ItemStack Stack) InHand;
    }
}