using Create.Resource;
using System.Reflection;

try
{
    if (args[1] != "Release")
        return;

    var compilation_path = Path.GetFullPath($"{Assembly.GetExecutingAssembly().Location}../../../../../../{args[0]}/bin/Release/net8.0/{args[2]}/create.resources");
    var resource_path = Path.GetFullPath($"{Assembly.GetExecutingAssembly().Location}../../../../../../Create/Resources/");

    Stream s;
    if (File.Exists(compilation_path))
        s = File.OpenWrite(compilation_path);
    else
        s = File.Create(compilation_path);
    Resources.CreateFromDirectory().AddFolder(resource_path, "assets/create/", path =>
    {
        string pat = path.RegisterPath.ToLower();

        string root, file;

        {
            int last = 0;
            for (int i = 0; i < pat.Length; i++)
                if (pat[i] is '\\' or '/')
                    last = i;
            root = pat.Remove(last + 1);
            file = pat.Substring(last + 1);
        }
        {
            if (file[0] != '.')
            {
                int last = 0;
                for (int i = 0; i < file.Length; i++)
                    if (file[i] is '.')
                        last = i;
                file = file.Remove(last);
            }
        }
        path.SubPath($"{root}{file}");
    }).Finish().CompresToOneFile().SaveTo(s);
    s.Close();
    s.Dispose();
    Console.WriteLine($"Resources compiled - \"{compilation_path}\"");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Environment.Exit(-1);
}