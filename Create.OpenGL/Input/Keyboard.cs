using Create.Virtuals;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Create.Input;

public static class Keyboard
{
    static Dictionary<Keys, bool> keys = keys_map();
    static VirtualDictionaty<Keys, bool> keys_gateway = VirtualDictionaty.Create(keys).Finsh();

    public static VirtualDictionaty<Keys, bool> Keys => keys_gateway;

    internal static Dictionary<Keys, bool> keys_map()
    {
        Dictionary<Keys, bool> keys = new();
        foreach (var key in (Keys[])Enum.GetValues(typeof(Keys)))
            keys.TryAdd(key, false);
        return keys;
    }

    public static bool Space => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space];
    public static bool Apostrophe => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Apostrophe];
    public static bool Comma => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Comma];
    public static bool Minus => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Minus];
    public static bool Period => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Period];
    public static bool Slash => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Slash];
    public static bool D0 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D0];
    public static bool D1 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D1];
    public static bool D2 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D2];
    public static bool D3 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D3];
    public static bool D4 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D4];
    public static bool D5 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D5];
    public static bool D6 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D6];
    public static bool D7 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D7];
    public static bool D8 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D8];
    public static bool D9 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D9];
    public static bool Semicolon => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Semicolon];
    public static bool Equal => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Equal];
    public static bool A => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.A];
    public static bool B => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.B];
    public static bool C => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.C];
    public static bool D => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.D];
    public static bool E => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.E];
    public static bool F => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F];
    public static bool G => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.G];
    public static bool H => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.H];
    public static bool I => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.I];
    public static bool J => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.J];
    public static bool K => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.K];
    public static bool L => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.L];
    public static bool M => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.M];
    public static bool N => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.N];
    public static bool O => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.O];
    public static bool P => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.P];
    public static bool Q => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Q];
    public static bool R => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.R];
    public static bool S => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.S];
    public static bool T => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.T];
    public static bool U => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.U];
    public static bool V => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.V];
    public static bool W => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.W];
    public static bool X => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.X];
    public static bool Y => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Y];
    public static bool Z => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Z];
    public static bool LeftBracket => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftBracket];
    public static bool Backslash => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Backslash];
    public static bool RightBracket => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightBracket];
    public static bool GraveAccent => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.GraveAccent];
    public static bool Escape => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape];
    public static bool Enter => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Enter];
    public static bool Tab => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Tab];
    public static bool Backspace => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Backspace];
    public static bool Insert => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Insert];
    public static bool Delete => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Delete];
    public static bool Right => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right];
    public static bool Left => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left];
    public static bool Down => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down];
    public static bool Up => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up];
    public static bool PageUp => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.PageUp];
    public static bool PageDown => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.PageDown];
    public static bool Home => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Home];
    public static bool End => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.End];
    public static bool CapsLock => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.CapsLock];
    public static bool ScrollLock => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.ScrollLock];
    public static bool NumLock => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.NumLock];
    public static bool PrintScreen => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.PrintScreen];
    public static bool Pause => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Pause];
    public static bool F1 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F1];
    public static bool F2 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F2];
    public static bool F3 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F3];
    public static bool F4 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F4];
    public static bool F5 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F5];
    public static bool F6 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F6];
    public static bool F7 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F7];
    public static bool F8 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F8];
    public static bool F9 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F9];
    public static bool F10 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F10];
    public static bool F11 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F11];
    public static bool F12 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F12];
    public static bool F13 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F13];
    public static bool F14 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F14];
    public static bool F15 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F15];
    public static bool F16 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F16];
    public static bool F17 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F17];
    public static bool F18 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F18];
    public static bool F19 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F19];
    public static bool F20 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F20];
    public static bool F21 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F21];
    public static bool F22 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F22];
    public static bool F23 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F23];
    public static bool F24 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F24];
    public static bool F25 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.F25];
    public static bool KeyPad0 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad0];
    public static bool KeyPad1 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad1];
    public static bool KeyPad2 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad2];
    public static bool KeyPad3 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad3];
    public static bool KeyPad4 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad4];
    public static bool KeyPad5 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad5];
    public static bool KeyPad6 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad6];
    public static bool KeyPad7 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad7];
    public static bool KeyPad8 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad8];
    public static bool KeyPad9 => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad9];
    public static bool KeyPadDecimal => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadDecimal];
    public static bool KeyPadDivide => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadDivide];
    public static bool KeyPadMultiply => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadMultiply];
    public static bool KeyPadSubtract => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadSubtract];
    public static bool KeyPadAdd => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadAdd];
    public static bool KeyPadEnter => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadEnter];
    public static bool KeyPadEqual => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPadEqual];
    public static bool LeftShift => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift];
    public static bool LeftControl => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftControl];
    public static bool LeftAlt => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftAlt];
    public static bool LeftSuper => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftSuper];
    public static bool RightShift => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightShift];
    public static bool RightControl => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightControl];
    public static bool RightAlt => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightAlt];
    public static bool RightSuper => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightSuper];
    public static bool Menu => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.Menu];
    public static bool LastKey => keys[OpenTK.Windowing.GraphicsLibraryFramework.Keys.LastKey];

    internal static void KeyDown(KeyboardKeyEventArgs args)
    {
        keys[args.Key] = true;
    }
    internal static void KeyUp(KeyboardKeyEventArgs args)
    {
        keys[args.Key] = false;
    }
}