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
    public static Version Version { get; } = Assembly.GetEntryAssembly()?.GetName().Version ?? new(1, 0, 0);

    /// <summary>
    /// <inheritdoc cref="GatPathToGameFolder"/>
    /// </summary>
    public static string FilesPath { get; } = GatPathToGameFolder();

    /// <summary>
    /// Ścieżka z której gra jest aktywowana
    /// </summary>
    /// <returns>Ścieżka</returns>
    static string GatPathToGameFolder()
    {
        var files = Assembly.GetExecutingAssembly().Location;
        files = files?.Remove(files.IndexOf('\\') + 1);
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