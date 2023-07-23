using System.Text;

namespace Create.Resource;

/// <summary>
/// Repozytorium załadowane z jednego pliku
/// </summary>
public class SingleFileResources : Resources
{
    Stream baze_stream;
    object task_lock = new();

    private SingleFileResources(Stream stream, PathDirectory pd) : base(pd)
    {
        baze_stream = stream;
    }

    /// <summary>
    /// Do zadbania że tylko jeden wątek urzywa pliku żrudłowego na raz
    /// </summary>
    internal object TaskLock => task_lock;

    /// <summary>
    /// Plik żrudłowy
    /// </summary>
    internal Stream Stream => baze_stream;

    protected internal override Stream GetStream(GetStreamStruct args)
    {
        (long offset, long lenght) data = ((long, long))args.Sender!;
        return new ResourceStream(this, args.File, data.lenght, data.offset);
    }
    
    /// <summary>
    /// Konstruktor dla <see cref="SingleFileResources"/>
    /// </summary>
    public class Constructor
    {
#pragma warning disable CS8618
        Stream stream;
#pragma warning restore CS8618

        /// <summary>
        /// Załaduj repozytorium z pliku
        /// </summary>
        /// <exception cref="Exception"></exception>
        public Constructor FromFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception("File doesynt exist");
            stream = File.OpenRead(path);
            return this;
        }

        /// <summary>
        /// Załaduj repozytorium z <see cref="System.IO.Stream"/>a
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Constructor FromFile(Stream stream)
        {
            this.stream = stream;
            return this;
        }

        /// <summary>
        /// Zakończ konstrukcje <see cref="SingleFileResources"/>
        /// </summary>
        public SingleFileResources Finish()
        {
            stream.Position = 0;
            var dir = read_directory();
            var data_offset = stream.Position;
            var files_list = gen_paths(dir, null!);
            PathDirectory paths = new();
            foreach (var file in files_list)
                paths.AddFile(file.path, file.pozition);
            return new SingleFileResources(stream, paths);
            //Methods
            IEnumerable<(string path, (long offsetm, long lenght) pozition)> gen_paths(Directory directory, string root_path)
            {
                var files = directory.files.Cast(f => (string.IsNullOrEmpty(root_path) ? f.name : $"{root_path}{f.name}", (data_offset + f.offset, f.lengh)));
                return new[]
                {
                    files,
                    directory.directories.Cast(d => gen_paths(d, $"{root_path}{d.name}/")).MargEnumerables()
                }.MargEnumerables();
            }
            Directory read_directory()
            {
                string name = read_name();
                var files = read_all_files_data();
                var directories = new Directory[read_uint()];
                for (int i = 0; i < directories.Length; i++)
                    directories[i] = read_directory();
                return new() { name = name, files = files, directories = directories };
            }
            (string name, long offset, long lengh)[] read_all_files_data()
            {
                var files = new (string name, long offset, long lengh)[read_uint()];
                for (int i = 0; i < files.Length; i++)
                    files[i] = read_file_data();
                return files;
            }
            (string name, long offset, long lengh) read_file_data()
            {
                string n = read_name();
                long of = read_long();
                long le = read_long();
                return (n, of, le);
            }
            string read_name()
            {
                byte l = (byte)stream.ReadByte();
                Span<byte> bytes = stackalloc byte[l];
                stream.Read(bytes);
                return Encoding.UTF8.GetString(bytes);
            }
            long read_long()
            {
                Span<byte> bytes = stackalloc byte[8];
                stream.Read(bytes);
                return BitConverter.ToInt64(bytes);
            }
            uint read_uint()
            {
                Span<byte> bytes = stackalloc byte[4];
                stream.Read(bytes);
                return BitConverter.ToUInt32(bytes);
            }
        }

        /// <summary>
        /// Pozycje plików w fliku źrudłowym
        /// </summary>
        class Directory
        {
            public string name = null!;
            public (string name, long offset, long lengh)[] files = null!;
            public Directory[] directories = null!;
        }
    }
}
