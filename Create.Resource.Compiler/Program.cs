using Create.Resource;
using System.Reflection;

try
{
    var newArgs = new List<String>();
    {
        var rawArgs = args[0];
        while (!string.IsNullOrEmpty(rawArgs))
        {
            newArgs.Add(rawArgs.Substring(0,rawArgs.IndexOf(';')));
            rawArgs = rawArgs.Substring(rawArgs.IndexOf(';') + 1);
        }
    }

    var sourcePath = newArgs[0];
    var destinationPath = newArgs[1];

    Stream s = File.Exists(destinationPath) ? File.OpenWrite(destinationPath) : File.Create(destinationPath);
    
    Resources.CreateFromDirectory().AddFolder(sourcePath, "assets/create/", path =>
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
}
catch (Exception ex)
{
    Console.WriteLine(ex.StackTrace);
    Environment.Exit(-13);
}