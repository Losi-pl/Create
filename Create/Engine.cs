using System.Reflection;

namespace Create;

/// <summary>
/// Bazowa klasa inicjalizacji gry
/// </summary>
public static class Engine
{
    /// <summary>
    /// Twoży instancje aktywatora gry
    /// </summary>
    /// <returns></returns>
    public static Activator CreateActivator() => new();

    /// <summary>
    /// Wersja gry
    /// </summary>
    public static Version Version { get; } =
        Version.Parse(Assembly
        .GetCallingAssembly()!
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
        .InformationalVersion);

    /// <summary>
    /// <inheritdoc cref="get_files_path"/>
    /// </summary>
    public static string FilesPath { get; } = get_files_path();

    /// <summary>
    /// Ścieżka z której gra jest aktywowana
    /// </summary>
    /// <returns>Ścieżka</returns>
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

    /// <summary>
    /// Scena ładowania gry
    /// </summary>
    static void main()
    {
        OpenGL.Engine.Scean = new Sceans.Loading();
        OpenGL.Engine.window.Run();
    }

    /// <summary>
    /// Aktywator
    /// </summary>
    public class Activator
    {
        public void Finish() => main();
    }
}