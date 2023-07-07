namespace Create.OpenGL.Textures;

/// <summary>
/// Kanał kolorowy połonczony z <see cref="RenderLayer"/>
/// </summary>
public sealed class RenderTexture : Texture
{
    int handle;

    internal RenderTexture(int handle) => this.handle = handle;

    /// <summary>
    /// Połączenie z nowym kanałem w pamięci ram pełniącym to samo zadanie
    /// </summary>
    internal void SetNewHandle(int handle) => this.handle = handle;
    public override int Handle => handle;
    public override TextureTarget Target => TextureTarget.Texture2D;
}
