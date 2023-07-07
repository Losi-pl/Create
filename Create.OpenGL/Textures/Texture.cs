namespace Create.OpenGL.Textures;

/// <summary>
/// Podstawa do przechowywania tekstór w karcie graficznej
/// </summary>
public abstract class Texture
{
    /// <summary>
    /// Czy dany typ jest wspierany przez <c>Create.OpenGL"</c>
    /// </summary>
    public static bool TextureSupported(ActiveUniformType type) => type switch
    {
        ActiveUniformType.Sampler2D => true,
        ActiveUniformType.Sampler2DArray => true,




        _ => false
    };

    /// <summary>
    /// Odnośnik do tego obiektu w pamięci karty graficznej
    /// </summary>
    public abstract int Handle { get; }

    /// <summary>
    /// Typ do jakiego ta tekstura używa w <see cref="Shader"/>ach
    /// </summary>
    public abstract TextureTarget Target { get; }
}
