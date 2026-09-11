using System.Diagnostics;
using Create.Elements;
using Create.Registry;
// ReSharper disable UnusedMember.Global, ConvertToAutoProperty
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Create.Storage;

[DebuggerDisplay("block=[{Block.Identity}] meta={Meta}")]
public readonly struct PlacedBlock : IEquatable<PlacedBlock>
{
    private static GameElements.TypeLibrary<Block> Library => field ??= GameElements.Get<Block>();
    private readonly int _block;
    private readonly byte _meta;
    private readonly bool _full;

    public PlacedBlock(Block block) : this(block, 0) { }
    public PlacedBlock(Block block, byte meta)
    {
        ArgumentNullException.ThrowIfNull(block);
        _block = block.Index;
        _meta = meta;
        _full = true;
    }
    
    public Block Block => _full ? Library.Get(_block)! : Blocks.Air;
    public int BlockIndex => _full ? _block : Blocks.Air.Index;
    
    public int Meta => _meta;

    public bool Equals(PlacedBlock other) => this == other;
    public override bool Equals(object? obj) => obj is PlacedBlock other && this == other;

    public override int GetHashCode() => HashCode.Combine(_full ? _block : Blocks.Air.Index, _meta);
    
    public static bool operator !=(PlacedBlock a, PlacedBlock b) => !(a == b);
    public static bool operator ==(PlacedBlock a, PlacedBlock b) => a.BlockIndex == b.BlockIndex && a.Meta == b.Meta;
}