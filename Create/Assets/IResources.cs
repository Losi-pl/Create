using Create.Registry;

namespace Create.Assets;

public interface IResources
{
    public Stream? GetStream(string name);

    public string[] GetManifest();

    public static IResources[] GetActiveResources() => IMod.Mods.Values.Select(m => m.Resources).ToArray();
    public static IResources Empty { get; } = new Empty();
}

file class Empty: IResources
{
    public Stream? GetStream(string name) => null;

    public string[] GetManifest() => Array.Empty<string>();
}