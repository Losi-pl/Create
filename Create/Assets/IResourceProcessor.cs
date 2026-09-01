using Create.Registry;

namespace Create.Assets;

public interface IResourceProcessor
{
    internal void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest);
    internal void ClearResources();

    protected static sealed Dictionary<(IMod mod, string path), IResources> FlattenSources(
        Dictionary<IResources, Dictionary<IMod, List<string>>> sources, IResources[] order)
    {
        Dictionary<(IMod mod, string path), IResources > flat = new();
        foreach (var source in order)
        {
            var sourceCont = sources[source];
            foreach (var perMod in sourceCont)
            {
                foreach(var path in perMod.Value)
                    flat.TryAdd((perMod.Key, path), source);
            }
        }
        
        return flat;
    }
}

public interface IResourceProcessor<T>: IResourceProcessor
{
    public PossibleResult<T> Find(RefElementIdent identity);
}