using Create.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;

namespace Create.Input;

/// <summary>
/// Kontrolki myszki użytkownika
/// </summary>
public static class Mouse
{
    static bool is_mouse_lock = false, is_visible_cursor = true;
    static (Vector2 delata, Vector2 tmp, bool clear) mouse;
    static MouseCursor cursor = Engine.window.Cursor;
    static MouseCursor no_cur = new(0, 0, 1, 1, new byte[] { 0, 0, 0, 0 });

    /// <summary>
    /// Czy kursoj jest zablokowany w centrum okna
    /// </summary>
    public static bool Lock
    {
        get => is_mouse_lock;
        set
        {
            if (value && !is_mouse_lock)
                if (Engine.window.IsFocused)
                    Engine.window.MousePosition = new(Engine.Size.X / 2f, Engine.Size.Y / 2f);
            is_mouse_lock = value;
            mouse.clear = true;
        }
    }
    
    /// <summary>
    /// Czy kursor jest widoczny
    /// </summary>
    public static bool Visible
    {
        get => is_visible_cursor;
        set
        {
            if (is_visible_cursor != value)
                if (value)
                    show_cursor();
                else
                    hide_cursor();
            is_visible_cursor = value;
        }
    }

    /// <summary>
    /// Zmienia kursor na niewidoczny
    /// </summary>
    static void hide_cursor()
    {
        cursor = Engine.window.Cursor;
        Engine.window.Cursor = no_cur;
    }

    /// <summary>
    /// Zmienia kursor na ten przed ukryciem w <see cref="hide_cursor"/>
    /// </summary>
    static void show_cursor()
    {
        Engine.window.Cursor = cursor;
    }

    /// <summary>
    /// O ile myszka została przesumięta w ostatniej klatce
    /// </summary>
    public static (float x, float y) Delta => mouse.delata.ToTumple();

    /// <summary>
    /// Rejestrowanie ruchu myszki w trybie <c><see cref="Lock"/> == <see langword="false"/></c>
    /// </summary>
    internal static void mouse_move(MouseMoveEventArgs args)
    {
        if(!is_mouse_lock)
            if (Engine.window.IsFocused)
                mouse.delata = args.Delta;
    }

    /// <summary>
    /// Rejestrowanie ruchu myszki w trybie <c><see cref="Lock"/> == <see langword="true"/></c>
    /// </summary>
    internal static void standard_mode(FrameEventArgs args)
    {
        if (is_mouse_lock)
            if (Engine.window.IsFocused)
            {
                Vector2 center = new(Engine.Size.X / 2f, Engine.Size.Y / 2f);
                Vector2 delata = Engine.window.MousePosition - center;
                Engine.window.MousePosition = center;
                delata = new((int)delata.X, (int)delata.Y);
                mouse.delata = (mouse.clear ? new() : delata);
                mouse.clear = false;
            }
    }

    /// <summary>
    /// Czyszczenie ruchu myszki z ostatniej klatki
    /// </summary>
    internal static void clear_data()
    {
        if (!Engine.window.IsFocused)
            mouse.delata = new();
    }
}
