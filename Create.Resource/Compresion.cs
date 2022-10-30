using System.Text;

namespace Create.Resource;

public static class Compresion
{
    public static SingleFile CompresToOneFile(this Resources resources) => new(resources);

    public sealed class SingleFile
    {
        Resources resources;

        public SingleFile(Resources resources)
        {
            this.resources = resources;
        }
        public void SaveTo(string path)
        {
            path = Path.GetFullPath(path);
            var dir_end = 0;
            for (int i = 0; i < path.Length; i++)
                if (path[i] is '/' or '\\')
                    dir_end = i;
            var directory = path.Remove(dir_end + 1);
            if (!Directory.Exists(directory))
                throw new IOException($"Directory \"{directory}\" doesn't exist");
            if(File.Exists(path))
                throw new IOException($"File \"{path}\" alredy exist");
            var s = File.OpenWrite(path);
            SaveTo(s);
            s.Close();
            s.Dispose();
        }
        public void SaveTo(Stream stream)
        {
            long offs = 0;
            var all_file = get_all_files().Cast(file =>
            {
                (long offset, long lenght) pozition = (offs, file.stream.Length);
                offs += pozition.lenght;
                return (file, pozition);
            }).ToArray();
            long all_data_lengh = 0;
            for (long i = 0; i < all_file.LongLength; i++)
                all_data_lengh += all_file[i].pozition.lenght;
            var root_dir = resources.main_dir();
            var manifest = generate_manifest(all_file, root_dir).ToArray();
            stream.Position = 0;
            all_data_lengh += manifest.LongLength;
            stream.SetLength(all_data_lengh);
            stream.WriteLong(manifest);
            foreach (var file in all_file)
                stream.WriteStream(file.file.stream);
        }

        IEnumerable<(Stream stream, ResourceFile file)> get_all_files() => resources.AllFiles.Cast(f => (f.GetStream(), f));
        IEnumerable<byte> generate_manifest(((Stream stream, ResourceFile file) file, (long offset, long lenght))[] file_pozitions, ResourceDirectory directory)
        {
            var files_count = (uint)directory.Files.Count();
            var file_manifest = new[]
            {
                BitConverter.GetBytes(files_count),
                directory.Files.Cast(f => file_data(f)).MargEnumerables()
            }.MargEnumerables();

            var directory_manifest =
                directory.SubPaths
                .Cast(d => generate_manifest(file_pozitions, d))
                .MargEnumerables();

            return new[]
            {
                calculate_name(directory.Name),
                file_manifest,
                BitConverter.GetBytes((uint)directory.SubPaths.Count()),
                directory_manifest,
            }.MargEnumerables();

            //Methods
            byte[] file_data(ResourceFile file)
            {
                byte[] bytes = new byte[file.Name.Length + 17];
                Span<byte> bytes_span = new(bytes);
                bytes[0] = (byte)file.Name.Length;
                var s = file.Name;
                Encoding.UTF8.GetBytes(s, 0, s.Length, bytes, 1);
                var file_data = file_pozition(file);
                BitConverter.TryWriteBytes(bytes_span.Slice(s.Length + 1, 8), file_data.offset);
                BitConverter.TryWriteBytes(bytes_span.Slice(s.Length + 9, 8), file_data.lenght);

                return bytes;
            }
            byte[] calculate_name(string name)
            {
                if (string.IsNullOrEmpty(name))
                    return new byte[] { 0 };
                Span<byte> bytes = stackalloc byte[name.Length + 1];
                bytes[0] = (byte)name.Length;
                Encoding.UTF8.GetBytes(name.AsSpan(), bytes.Slice(1, name.Length));
                return bytes.ToArray();
            }
            (long offset, long lenght) file_pozition(ResourceFile file) => file_pozitions.First(f => f.file.file == file).Item2;
        }
    }
}
