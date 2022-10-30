namespace Create.OpenGL.Textures;

public sealed class RenderTexture : Texture
{
    int handle;

    internal RenderTexture(int handle) => this.handle = handle;
    internal void SetNewHandle(int handle) => this.handle = handle;
    public override int Handle => handle;
    public override TextureTarget Target => TextureTarget.Texture2D;
}
