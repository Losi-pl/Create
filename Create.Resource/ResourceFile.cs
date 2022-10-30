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
    public string Name { get => name; internal init => name = value; }
    public string Path => directory.Parent != null ? directory.Path + name : name;
    public override string ToString() => Path;
    public Stream GetStream() => directory.Resources.GetStream(new(me, this));
}
