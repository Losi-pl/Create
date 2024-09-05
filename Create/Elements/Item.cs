using Create.Conteiner;
using Create.OpenGL;
using Create.OpenGL.GUI;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements;

/// <summary>
/// Baza do budowy itemów
/// </summary>
public abstract partial class Item : Baze
{
    //Ustawienie bazowego typu elementu na Item
    public sealed override Type ElementBazicType => typeof(Item);

    /// <summary>
    /// Generuje model przedmiotu
    /// </summary>
    /// <param name="itemStack"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual ItemModel GetItemModel(ItemStack itemStack, Net.Player player) =>
        GenerateItemModel(CodeName.Replace('-', '_'));

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

    public virtual string GetItemName(StackData stackData, Net.Player player) => stackData.Item.CodeName;

    public virtual uint CraftingUses(CraftingUsesData data) => data.Stack.Count;

    public virtual ItemStack? UsedInCrafting(UsedInCraftingData data) => data.Stack.Count > 1 ?
        new ItemStack(data.Stack.Count - 1, data.Stack.Item, data.Stack.Type, data.Stack.Meta) : null;

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
        public uint Count;
        public Item Item;
        public string Meta;
        public byte Type;
        public static implicit operator StackData(ItemStack stack) => 
            new() { Count = stack.Count, Item = stack.Item, Meta = stack.Meta, Type = stack.Type };
        public static implicit operator ItemStack(StackData data) =>
            new(data.Count, data.Item, data.Type, data.Meta);
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
    /// <summary>
    /// Używany w metodzie <see cref="CraftingUses(CraftingUsesData)"/>
    /// </summary>
    public struct CraftingUsesData
    {
        public Net.Player Player;
        public World World;
        public ItemStack Stack;
    }
    /// <summary>
    /// Używany w metodzie <see cref="CraftingUses(CraftingUsesData)"/>
    /// </summary>
    public struct UsedInCraftingData
    {
        public Net.Player Player;
        public World World;
        public ItemStack Stack;
    }
}