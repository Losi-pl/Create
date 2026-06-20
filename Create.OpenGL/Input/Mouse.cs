using Create.Linq;
using Create.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Create.Input;

/// <summary>
/// Kontrolki myszki użytkownika
/// </summary>
public static class Mouse
{
    static bool is_mouse_lock = false, is_visible_cursor = true;
    static (Vector2 delata, Vector2 tmp, bool clear) mouse;
    static bool last_left, last_right, last_scrol, current_left, current_right, current_scrol;

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
                Engine.window.CursorState = value ? CursorState.Normal : CursorState.Hidden;
            is_visible_cursor = value;
        }
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

        (current_left, last_left) = (Engine.window.MouseState[MouseButton.Left], current_left);
        (current_right, last_right) = (Engine.window.MouseState[MouseButton.Right], current_right);
        (current_scrol, last_scrol) = (Engine.window.MouseState[MouseButton.Middle], current_scrol);
    }

    /// <summary>
    /// Czyszczenie ruchu myszki z ostatniej klatki
    /// </summary>
    internal static void clear_data()
    {
        if (!Engine.window.IsFocused)
            mouse.delata = new();
    }

    /// <summary>
    /// Pozycja kursora na ekranie zsględem centrum okna
    /// </summary>
    public static (int x, int y) Pozition => Engine.window.MousePosition.ToTumple().Cast(v => ((int)-((Engine.Size.X / 2f) - v.X), (int)((Engine.Size.Y / 2f) - v.Y)));

    public static (bool Up, bool Down, bool Status) Left => (
        last_left == true && current_left == false,
        last_left == false && current_left == true,
        current_left);

    public static (bool Up, bool Down, bool Status) Right => (
        last_right == true && current_right == false,
        last_right == false && current_right == true,
        current_right);

    public static (bool Up, bool Down, bool Status, int Delta) Scroll => (
        last_scrol == true && current_scrol == false,
        last_scrol == false && current_scrol == true,
        current_scrol,
        (int)(Engine.window.MouseState.ScrollDelta.X + Engine.window.MouseState.ScrollDelta.Y));
}
