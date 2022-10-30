namespace Create.Render;

public sealed class BlockTextureHandle
{
    int handle;
    internal BlockTextureHandle(int handle)
    {
        this.handle = handle;
    }
    public int Handle => handle;

    public static BlockTextureHandle None { get; } = new(0);
}
