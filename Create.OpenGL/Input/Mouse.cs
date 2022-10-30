using Create.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace Create.Input;

public static class Mouse
{
    static bool is_mouse_lock = false, is_visible_cursor = true;
    static (Vector2 delata, Vector2 tmp, bool clear) mouse;
    static MouseCursor cursor = Engine.window.Cursor;
    static MouseCursor no_cur = new(0, 0, 1, 1, new byte[] { 0, 0, 0, 0 });
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

    static void hide_cursor()
    {
        cursor = Engine.window.Cursor;
        Engine.window.Cursor = no_cur;
    }
    static void show_cursor()
    {
        Engine.window.Cursor = cursor;
    }

    public static (float x, float y) Delta => mouse.delata.ToTumple();

    internal static void mouse_move(MouseMoveEventArgs args)
    {
        //args.Delta
        if(!is_mouse_lock)
            if (Engine.window.IsFocused)
                mouse.delata = args.Delta;
    }

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
    internal static void clear_data()
    {
        if (!Engine.window.IsFocused)
            mouse.delata = new();
    }
}
