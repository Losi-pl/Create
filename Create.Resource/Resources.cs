using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.Enumeration;

namespace Create.Resource;

public abstract class Resources
{
    ResourceDirectory root;

    public Resources(PathDirectory paths)
    {
        root = paths.get_main_directory();
        foreach (var path in root.AllSubPaths)
            path.set_mather_resources(this);
    }

    public ResourceDirectory GetPath(string path) => root.GetSubPath(path);
    public ResourceFile GetFile(string path) => root.GetFile(path);

    internal ResourceDirectory main_dir() => root;

    public IEnumerable<ResourceDirectory> MainDirectories => root.SubPaths;
    public IEnumerable<ResourceDirectory> AllDirectories => root.AllSubPaths;
    public IEnumerable<ResourceFile> AllFiles => root.AllSubFiles;

    protected internal abstract Stream GetStream(GetStreamStruct args);

    public static DirectoryResources.Creator CreateFromDirectory() => new();
    public static SingleFileResources.Constructor CreateFromFile() => new();
    public static MargedResources.Constructor FromOthers() => new();
    
}