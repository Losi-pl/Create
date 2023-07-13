namespace Create.Resource;

/// <summary>
/// Wrzystkie ścierzki w trakcie tworzenia <see cref="Resources"/>
/// </summary>
public sealed class PathDirectory
{
    directory main = new();
    Dictionary<string, directory> all_registered = new();

    public PathDirectory() => all_registered.Add(string.Empty, main);

    /// <summary>
    /// Dodaj plik o ścierzce <paramref name="path"/>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="file">Obiekt urzywany do wyciągania <see cref="Stream"/>a z pliku</param>
    /// <exception cref="Exception"></exception>
    public void AddFile(string path, object? file)
    {
        var path_data = validate_path(path);
        directory dir;
        if(!all_registered.TryGetValue(path_data.path, out dir!))
        {
            dir = main;
            for(int i = 0; i < path_data.segments.Length - 1; i++)
            {
                var new_dir = dir.folders.Find(f => f.name == path_data.segments[i]);
                if (new_dir == null)
                {
                    directory new_d = new();
                    new_d.name = path_data.segments[i];
                    dir.folders.Add(new_d);
                    dir = new_d;
                    string new_path = "";
                    for (int l = 0; l < i + 1; l++)
                        new_path += $"{path_data.segments[l]}\\";
                    all_registered.Add(new_path, new_d);
                }
                else
                    dir = new_dir;
            }
        }
        if (dir.files.Find(fi => fi.name == path_data.file) != null)
            throw new Exception("That file is allredy added");
        dir.files.Add(new() { name = path_data.file, sender = file });
    }

    /// <summary>
    /// Konwertuje ścierzke i sprawdza czy jej składnia jest poprawna
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    static (string path, string file, string[] segments) validate_path(string path)
    {
        path = path.Replace("/", "\\");

        string[] sects = path.Split('\\');
        for (int i = 0; i < sects.Length; i++)
            if (string.IsNullOrWhiteSpace(sects[i]))
                throw new ArgumentException("Path is not valid");
        for (int i = 0; i < sects.Length; i++)
        {
            for (int s = 0; s < sects[i].Length; s++)
                if (sects[i][s] is '<' or '>' or ':' or '\"' or '\\' or '/' or '|' or '?' or '*')
                    throw new ArgumentException("Path is not valid");
        }

        int last_set = path.FindFromEnd('\\');

        return (path.Remove(last_set + 1), path.Substring(last_set + 1), sects);
    }

    /// <summary>
    /// Konwertuje strukture na strukture w <see cref="ResourceDirectory"/>
    /// </summary>
    internal ResourceDirectory get_main_directory() => get_directory(main);

    /// <summary>
    /// <inheritdoc cref="get_main_directory"/>
    /// </summary>
    ResourceDirectory get_directory(directory dir)
    {
        var files = dir.files.Cast(f => new ResourceFile(f.name, f.sender)).ToArray();
        var folders = dir.folders.Cast(f => get_directory(f)).ToArray();
        return new(dir.name, folders, files);
    }

    /// <summary>
    /// Parametry i zawartość pod folderu
    /// </summary>
    class directory
    {
        #pragma warning disable CS8618
        public string name;
        #pragma warning restore CS8618
        public List<directory> folders = new();
        public List<file> files = new();
    }

    /// <summary>
    /// Parametry i zawartość pliku
    /// </summary>
    class @file
    {
        #pragma warning disable CS8618
        public string name;
        public object? sender;
        #pragma warning restore CS8618
    }
}
