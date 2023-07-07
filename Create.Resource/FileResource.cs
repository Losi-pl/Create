namespace Create.Resource;

/// <summary>
/// Obecnie nie urzywane do niczego
/// </summary>
public class FilesResources : Resources
{
    private FilesResources(PathDirectory paths) : base(paths)
    {

    }

    protected internal override Stream GetStream(GetStreamStruct args)
    {
        throw new NotImplementedException();
    }
}
