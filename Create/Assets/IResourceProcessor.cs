using Create.Registry;

namespace Create.Assets;

public interface IResourceProcessor
{
    internal void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest);
    internal void ClearResources();
}

public interface IResourceProcessor<T>: IResourceProcessor
{
    public PossibleResult<T> Find(IMod source, string identity);

    public PossibleResult<T> Find(string identity)
    {
        var mods = IMod.Mods.GetAlternateLookup<ReadOnlySpan<char>>();
        if (!mods.TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod))
            throw new KeyNotFoundException($"Mod with identity {identity[..identity.IndexOf(':')]}");
        return Find(mod, identity[(identity.IndexOf(':') + 1)..]);
    }
}