using Create.Elements;
using System.Diagnostics.CodeAnalysis;

namespace Create.Conteiner;

/// <summary>
/// Postawiony blok
/// </summary>
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

    /// <summary>
    /// Typ bloku
    /// </summary>
    public Block Block => block ?? Blocks.AIR;
    
    /// <summary>
    /// Pod typ bloku
    /// </summary>
    public byte Type => editor;

    /// <summary>
    /// Dodatkowe parametry bloku
    /// </summary>
    public string Meta => meta ?? string.Empty;

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

    public static bool operator ==(PlacedBlock a, PlacedBlock b) =>
        (a.block == b.block) && (a.meta == b.meta) && (a.editor == b.editor);
    public static bool operator !=(PlacedBlock a, PlacedBlock b) => !(a == b);

    public override int GetHashCode() => base.GetHashCode();
    public override bool Equals([NotNullWhen(true)] object? obj) => 
        obj is PlacedBlock pb ? pb == this : false;

    public ItemStack AsItemStack(uint itemCount = 1)
    {
        if (itemCount < 1)
            throw new ArgumentException("ItemStack can't be empty");
        return new(itemCount, block, editor, meta);
    }
}
