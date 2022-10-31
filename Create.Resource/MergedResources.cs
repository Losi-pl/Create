namespace Create.Resource;

public sealed class MargedResources : Resources
{
    private MargedResources(PathDirectory paths) : base(paths) { }

    protected internal override Stream GetStream(GetStreamStruct args) => ((ResourceFile)args.Sender!).GetStream();

    public class Constructor
    {
        Dictionary<string, ResourceFile> files = new();

        public Constructor MargeFiles(Resources resources, string srcPath, string desPath)
        {
            valid_paths();

            var start_dir = resources.GetPath(srcPath);
            foreach(var file in start_dir.AllSubFiles)
            {
                var path = file.Path.Substring(srcPath.Length);
                path = desPath + path;
                if (files.ContainsKey(path))
                    files[path] = file;
                else
                    files.Add(path, file);
            }

            return this;
            //Methods
            void valid_paths()
            {
                if (srcPath == "\\" || srcPath == "/")
                    srcPath = string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(srcPath))
                        srcPath = "";
                    else
                    {
                        if (srcPath[0] is '\\' or '/')
                            srcPath = srcPath.Substring(1);
                        if (!(srcPath[^1] is '\\' or '/'))
                            srcPath = srcPath + "\\";
                    }
                }

                if (desPath == "\\" || desPath == "/")
                    desPath = string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(desPath))
                        desPath = "";
                    else
                    {
                        if (desPath[0] is '\\' or '/')
                            desPath = desPath.Substring(1);
                        if (!(desPath[^1] is '\\' or '/'))
                            desPath = desPath + "\\";
                    }
                }
            }
        }

        public MargedResources Finish()
        {
            PathDirectory paths = new();
            foreach(var file in files)
                paths.AddFile(file.Key, file.Value);
            return new(paths);
        }
    }
}
