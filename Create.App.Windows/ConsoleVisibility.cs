using System.Runtime.InteropServices;

namespace Create.App.Windows;

static class ConsoleVisibility
{
    [DllImport("Kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("User32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

    public static void Show() => ShowWindow(GetConsoleWindow(), 1);
    public static void Hide() => ShowWindow(GetConsoleWindow(), 0);
}
