namespace Create.Assets;

public interface IResources
{
    public Stream? GetStream(string name);

    public string[] GetManifest();
}