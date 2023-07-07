namespace Create.Resource;

/// <summary>
/// Repozytorium zbudowane z folderu w komputerze
/// </summary>
public class DirectoryResources : Resources
{
    private DirectoryResources(PathDirectory rootDirectory) : base(rootDirectory) { }

    protected internal override Stream GetStream(GetStreamStruct args)
    {
        var path = (string)args.Sender!;
        var stream = File.OpenRead(path);
        return stream;
    }

    /// <summary>
    /// Konstruktor dla <see cref="DirectoryResources"/>
    /// </summary>
    public class Creator
    {
        List<(string drive, string resource)> paths = new();

        public Creator() { }

        /// <summary>
        /// Wygeneruj gotowe <see cref="DirectoryResources"/>
        /// </summary>
        /// <returns></returns>
        public DirectoryResources Finish()
        {
            PathDirectory paths = new();
            foreach (var path in this.paths)
                paths.AddFile(path.resource, path.drive);

            return new(paths);
        }

        /// <summary>
        /// Połącz folder z repozytorium
        /// </summary>
        /// <param name="folderPath">Folder na komputerze</param>
        /// <param name="resourcePath">Ścieżka w repozutorium</param>
        /// <param name="modyfication">Modyfikacje no pliku</param>
        /// <returns></returns>
        public Creator AddFolder(string folderPath, string resourcePath, Action<PathModyfication>? modyfication = null)
        {
            if (folderPath[^1] is not '\\' or '/')
                folderPath = Path.GetFullPath(folderPath + '/');
            else
                folderPath = Path.GetFullPath(folderPath);

            if (resourcePath.Length > 0)
                if (resourcePath[^1] != '\\' && resourcePath[^1] != '/')
                    resourcePath = resourcePath + '/';

            var all_files_ = all_files(all_subpaths(folderPath));

            var all_files_2 = all_files_.Cast(f =>
            {
                PathModyfication data = new(f, f.RemoveFirstSubString(folderPath));
                modyfication?.Invoke(data);
                return (f, data);
            }).Where(d => !d.data.ignore)
            .Cast(f =>
            {
                (string path, string stream) data = (string.Empty, f.f);
                if (f.data.alter_sub_path != null)
                    data.path = f.data.alter_sub_path;
                else
                {
                    if (f.data.alter_name != null)
                    {
                        string pat = f.data.RegisterPath;
                        int last_el = 0;
                        for (int i = 0; i < pat.Length; i++)
                            if (pat[i] is '\\' or '/')
                                last_el = i;
                        data.path = pat.Remove(last_el) + f.data.alter_name;
                    }
                    else
                        data.path = f.data.RegisterPath;
                }

                return data;
            }).Cast(v => (v.stream, resourcePath + v.path));

            paths.AddRange(all_files_2);

            return this;

            //Method
            IEnumerable<string> all_subpaths(string path)
            {
                var paths = Directory.GetDirectories(path);
                return new[]
                    {
                        new string[] { path }, paths.Cast(p => all_subpaths(p)).MargEnumerables()
                    }.MargEnumerables();
            }
            IEnumerable<string> all_files(IEnumerable<string> folders) => folders.Cast(Directory.GetFiles).MargEnumerables();
        }

        /// <summary>
        /// Test kolizji nazw plików
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void test_file_colizions()
        {
            for (int i = 0; i < paths.Count; i++)
            {
                var my_pat = paths[i];
                if (paths.IsConteinedEx(path => path.resource == my_pat.resource, i))
                    throw new Exception($"Path {{{my_pat.resource}}} repeats");
            }
        }

        /// <summary>
        /// Modyfikacja parametrów plików w repozutorium
        /// </summary>
        public class PathModyfication
        {
            internal bool ignore;
            internal string? alter_sub_path, alter_name;

            string path, file_path;

            internal PathModyfication(string file_path ,string internal_path)
            {
                path = internal_path;
                this.file_path = file_path;
            }

            /// <summary>
            /// Zignoruj ten plik w repozytorium
            /// </summary>
            public void Ignore() => ignore = true;

            /// <summary>
            /// Ścierzka w repozutorium
            /// </summary>
            public string RegisterPath => path;

            /// <summary>
            /// Ścierzka na komputerze
            /// </summary>
            public string FilePath => file_path;

            /// <summary>
            /// Nazwa pliku na komputerze
            /// </summary>
            public string FileName
            {
                get
                {
                    int end = path.FindFromEnd(new[] { '/', '\\' }, 0);
                    return path.Substring(end, path.Length - end);
                }
            }

            /// <summary>
            /// Przeniesienie do kolejnych pod-folderów
            /// </summary>
            public PathModyfication SubPath(string path)
            {
                alter_sub_path = path;
                return this;
            }

            /// <summary>
            /// Zmiana nazwy w repozytorium
            /// </summary>
            public PathModyfication Rename(string name)
            {
                alter_name = name;
                return this;
            }
        }
    }
}
