using Create.Elements;

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

    public Item Item => item;
    public byte Type => edition;
    public string Meta => meta;
    public uint Count => count;
}