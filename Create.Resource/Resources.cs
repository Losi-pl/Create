namespace Create.Resource;

/// <summary>
/// Podstawa do tworzenia rurznych rodzaji repozytori z danymi
/// </summary>
public abstract class Resources
{
    ResourceDirectory root;

    public Resources(PathDirectory paths)
    {
        root = paths.get_main_directory();
        foreach (var path in root.AllSubPaths)
            path.set_mather_resources(this);
    }

    /// <summary>
    /// Pobierz folder o ścierzce <paramref name="path"/>
    /// </summary>
    public ResourceDirectory GetPath(string path) => root.GetSubPath(path);

    /// <summary>
    /// Pobierz plik o ścierzce <paramref name="path"/>
    /// </summary>
    public ResourceFile GetFile(string path) => root.GetFile(path);

    /// <summary>
    /// Główna ścierzka
    /// </summary>
    /// <returns></returns>
    internal ResourceDirectory main_dir() => root;

    /// <summary>
    /// Foldery w głównym katalogu
    /// </summary>
    public IEnumerable<ResourceDirectory> MainDirectories => root.SubPaths;

    /// <summary>
    /// Wrzystkie katalogi
    /// </summary>
    public IEnumerable<ResourceDirectory> AllDirectories => root.AllSubPaths;

    /// <summary>
    /// Wrzystkie pliki
    /// </summary>
    public IEnumerable<ResourceFile> AllFiles => root.AllSubFiles;

    /// <summary>
    /// Pobieranie <see cref="Stream"/>a z <see cref="ResourceFile"/>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected internal abstract Stream GetStream(GetStreamStruct args);

    /// <summary>
    /// Stworzenie repozytorium z folderów
    /// </summary>
    public static DirectoryResources.Creator CreateFromDirectory() => new();

    /// <summary>
    /// Załadowanie z pliku
    /// </summary>
    /// <returns></returns>
    public static SingleFileResources.Constructor CreateFromFile() => new();

    /// <summary>
    /// Połączenie kilku repozutoriów w jedno
    /// </summary>
    /// <returns></returns>
    public static MargedResources.Constructor FromOthers() => new();
    
}