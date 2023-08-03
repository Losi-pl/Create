using Create.Elements;
using System.Diagnostics.CodeAnalysis;

namespace Create.Conteiner;

public struct ItemStack
{
    readonly byte edition;
    readonly string meta;
    readonly Item item;
    readonly uint count;

    public ItemStack()
    {
        throw new Exception("ItemStack can't be empty");
    }
    public ItemStack(uint count, Item item)
    {
        if(count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = 0;
        meta = string.Empty;
        this.count = count;
    }
    public ItemStack(uint count, Item item, byte type)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = type;
        meta = string.Empty;
        this.count = count;
    }
    public ItemStack(uint count, Item item, byte type, string meta)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = type;
        this.meta = meta;
        this.count = count;
    }

    public ItemStack(Item item) : this(1, item) { }
    public ItemStack(Item item, byte type) : this(1, item, type) { }
    public ItemStack(Item item, byte type, string meta) : this(1, item, type, meta) { }

    public ItemStack(uint count, Block block)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_block(nameof(block), block);
        this.count = count;
        meta = block.CodeName;
        edition = 0;
        item = Elements.Items.BLOCK_ITEM;
    }
    public ItemStack(uint count, Block block, byte type)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_block(nameof(block), block);
        this.count = count;
        meta = block.CodeName;
        edition = type;
        item = Elements.Items.BLOCK_ITEM;
    }
    public ItemStack(uint count, Block block, byte type, string meta)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_block(nameof(block), block);
        this.count = count;
        this.meta = string.IsNullOrEmpty(meta) ? block.CodeName : $"{block.CodeName};{meta}";
        edition = type;
        item = Elements.Items.BLOCK_ITEM;
    }

    public ItemStack(Block block) : this(1, block) { }
    public ItemStack(Block block, byte type) : this(1, block, type) { }
    public ItemStack(Block block, byte type, string meta) : this(1, block, type, meta) { }

    /// <summary>
    /// Test czy Typ itemu jest zarejestrowany w rejestrze
    /// </summary>
    /// <param name="item">Typ itemu</param>
    static void test_item(string paramName, Item item)
    {
        if (item == null)
            throw new ArgumentNullException(paramName);
        if (!item.IsRegistered)
            throw new Exception($"Item {item.CodeName} is not registered");
    }

    /// <summary>
    /// Test czy Typ bloku jest zarejestrowany w rejestrze
    /// </summary>
    /// <param name="paramName"></param>
    /// <param name="block">Typ bloku</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    static void test_block(string paramName, Block block)
    {
        if (block == null)
            throw new ArgumentNullException(paramName);
        if (!block.IsRegistered)
            throw new Exception($"Block {block.CodeName} is not registered");
    }

    public Item Item => item;
    public byte Type => edition;
    public string Meta => meta;
    public uint Count => count;

    public static bool operator ==(ItemStack a, ItemStack b) =>
        (a.item == b.item) ? a.Item.AreStacksEqual(a, b) : false;
    public static bool operator !=(ItemStack a, ItemStack b) => !(a == b);

    public override int GetHashCode() => base.GetHashCode();
    public override bool Equals([NotNullWhen(true)] object? obj) => 
        obj is ItemStack @is ? this == @is : false;

    public PlacedBlock AsPlacedBlock()
    {
        if (item != Elements.Items.BLOCK_ITEM)
            throw new Exception("Item doesn't contain block");

        var meta = this.meta;
        var bl_id = meta.AsSpan();
        {
            var i = meta.IndexOf(';');
            if (i != -1)
                bl_id = bl_id[0..i];
        }
        var nMeta = meta.IndexOf(';').Cast(c => c == -1 || c == meta.Length - 1 ? string.Empty : meta.Substring(c + 1, meta.Length - c - 1));
        Block block = null!;
        foreach (var b in Register.Blocks)
        {
            if (b.CodeName.AsSpan().Equals(bl_id, StringComparison.Ordinal))
            {
                block = b;
                break;
            }
        }
        if (block == null)
            throw new Exception("Block not recognized");

        return new(block, edition, nMeta);
    }
}