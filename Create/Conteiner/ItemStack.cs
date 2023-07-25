using Create.Elements;

namespace Create.Conteiner;

public struct ItemStack
{
    readonly byte edition;
    readonly string meta;
    readonly Item item;
    readonly int count;

    public ItemStack()
    {
        throw new Exception("ItemStack can't be empty");
    }
    public ItemStack(int count, Item item)
    {
        if(count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = 0;
        meta = string.Empty;
    }
    public ItemStack(int count, Item item, byte type)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = type;
        meta = string.Empty;
    }
    public ItemStack(int count, Item item, byte type, string meta)
    {
        if (count <= 0)
            throw new Exception("ItemStack can't be empty");
        test_item(nameof(item), item);
        this.item = item;
        edition = type;
        this.meta = meta;
    }

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
}