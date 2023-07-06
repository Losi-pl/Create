namespace Create.Render;

/// <summary>
/// Odnośnik do tekstury zapisanej w <see cref="Assets.BlockAtlas.Attlas"/>
/// </summary>
public sealed class BlockTextureHandle
{
    int handle;
    internal BlockTextureHandle(int handle)
    {
        this.handle = handle;
    }
    public int Handle => handle;

    /// <summary>
    /// Odnośnik do zerowej tekstury urzywanej gdy jakaś tekstura nie została znaleziona w <see cref="Assets.BlockAtlas.Handles"/>
    /// </summary>
    public static BlockTextureHandle None { get; } = new(0);
}
