namespace Create.Resource;

public class ResourceFile
{
    string name;
    internal ResourceDirectory directory;
    internal object? me;

    internal ResourceFile(string name, object? sender)
    { 
        directory = null!;
        this.name = name;
        me = sender;
    }
    
    /// <summary>
    /// Nazwa folderu
    /// </summary>
    public string Name { get => name; internal init => name = value; }
    
    /// <summary>
    /// Ścierzka pliku
    /// </summary>
    public string Path => directory.Parent != null ? directory.Path + name : name;
    public override string ToString() => Path;

    /// <summary>
    /// Pobierz <see cref="Stream"/> do pliku
    /// </summary>
    /// <returns></returns>
    public Stream GetStream() => directory.Resources.GetStream(new(me, this));
}
