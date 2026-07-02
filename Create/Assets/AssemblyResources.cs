using System.Reflection;

namespace Create.Assets;

public class AssemblyResources: IResources
{
    private readonly Assembly _assembly;
    private readonly string? _pathPrefix;
        
    /// <summary></summary>
    /// <param name="assembly">The source of resources</param>
    /// <param name="pathPrefix">If there are more resources in the manifest than desired this can be used to specify which files are to be included in the manifest</param>
    public AssemblyResources(Assembly assembly, string? pathPrefix = null)
    {
        _assembly = assembly;
        if(pathPrefix != null)
            _pathPrefix = pathPrefix[^1] is '/' or '\\' ? pathPrefix.Replace('\\', '/') : pathPrefix.Replace('\\', '/') + '/';
    }

    public Stream? GetStream(string path) => _assembly.GetManifestResourceStream(_pathPrefix != null ? $"{_pathPrefix}{path}" : path);

    public string[] GetManifest()
    {
        return _pathPrefix == null ? _assembly.GetManifestResourceNames() : _assembly.GetManifestResourceNames().Where(StartsWith).Select(CutPrefix).ToArray();

        bool StartsWith(string x) => x.StartsWith(_pathPrefix);
        string CutPrefix(string x) => x[_pathPrefix!.Length..];
    }
}