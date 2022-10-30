using System.Reflection;

namespace Create;

public static class Engine
{
    public static Activator CreateActivator() => new();

    public static Version Version { get; } =
        Version.Parse(Assembly
        .GetCallingAssembly()!
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
        .InformationalVersion);

    public static string FilesPath { get; } = get_files_path();

    static string get_files_path()
    {
        var files = Assembly.GetExecutingAssembly().Location;
        int rem_from = 0;
        for (int i = 0; i < files.Length; i++)
            if (files[i] is '\\')
                rem_from = i;
        files = files?.Remove(rem_from + 1);
        return files!;
    }

    static void main()
    {
        OpenGL.Engine.Scean = new Sceans.Loading();
        OpenGL.Engine.window.Run();
    }

    public class Activator
    {
        public void Finish() => main();
    }
}