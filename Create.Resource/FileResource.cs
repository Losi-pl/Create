namespace Create.Resource;

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
