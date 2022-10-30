namespace Create.OpenGL.Textures;

public abstract class Texture
{
    public static bool TextureSupported(ActiveUniformType type) => type switch
    {
        ActiveUniformType.Sampler2D => true,
        ActiveUniformType.Sampler2DArray => true,




        _ => false
    };

    public abstract int Handle
    {
        get;
    }
    public abstract TextureTarget Target
    {
        get;
    }
}
