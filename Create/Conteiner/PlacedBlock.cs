using Create.Elements;

namespace Create.Conteiner;

public readonly struct PlacedBlock
{
    readonly Block block;
    readonly byte editor;
    readonly string meta;

    public PlacedBlock()
    {
        block = Blocks.AIR;
        editor = 0;
        meta = string.Empty;
    }
    public PlacedBlock(Block block)
    {
        test_block(nameof(block), block);
        this.block = block;
        editor = 0;
        meta = string.Empty;
    }
    public PlacedBlock(Block block, byte type)
    {
        test_block(nameof(block), block);
        this.block = block;
        editor = type;
        meta = string.Empty;
    }
    public PlacedBlock(Block block, byte type, string meta)
    {
        test_block(nameof(block), block);
        this.block = block;
        editor = type;
        this.meta = meta;
    }

    public Block Block => block ?? Blocks.AIR;
    public byte Type => editor;
    public string Meta => meta ?? String.Empty;

    static void test_block(string paramName, Block block)
    {
        if (block == null)
            throw new ArgumentNullException(paramName);
        if (!block.IsRegistered)
            throw new Exception($"Block {block.CodeName} is not registered");
    }
}
