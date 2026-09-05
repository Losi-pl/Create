using Create.Graphics;
using Create.Registry;

namespace Create.Assets;

public readonly struct BlockTexture(uint index, IMod mod, string name)
{
    // ReSharper disable once InconsistentNaming
    public const int PIXEL_SIZE = 16;
    
    // ReSharper disable MemberCanBePrivate.Global
    public string Name => name;
    public IMod Mod => mod;
    public uint Index => index;
    // ReSharper restore MemberCanBePrivate.Global

    public string Identity => $"{Mod.Identity}:{Name}";

    // ReSharper disable once InconsistentNaming
    public static BlockTexture NULL { get; } = new(0, IMod.Mods["create"], string.Empty);

    public static Texture2DAtlas Atlas { get => field ?? throw new InvalidOperationException("Texture atlas has not yet bean finished"); internal set; } = null!;
}