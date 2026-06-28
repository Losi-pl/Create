using SilkKey = Silk.NET.Input.Key;

namespace Create.Input;

public enum Key
{
    /// <summary>
    /// An unknown key.
    /// </summary>
    Unknown = SilkKey.Unknown,

    /// <summary>
    /// The spacebar key.
    /// </summary>
    Space = SilkKey.Space,

    /// <summary>
    /// The apostrophe key.
    /// </summary>
    Apostrophe = SilkKey.Apostrophe /* ' */,

    /// <summary>
    /// The comma key.
    /// </summary>
    Comma = SilkKey.Comma /* , */,

    /// <summary>
    /// The minus key.
    /// </summary>
    Minus = SilkKey.Minus /* - */,

    /// <summary>
    /// The period key.
    /// </summary>
    Period = SilkKey.Period /* . */,

    /// <summary>
    /// The slash key.
    /// </summary>
    Slash = SilkKey.Slash /* / */,

    /// <summary>
    /// The 0 key.
    /// </summary>
    Number0 = SilkKey.Number0,

    /// <summary>
    /// The 0 key; alias for <see cref="Number0"/>
    /// </summary>
    D0 = SilkKey.D0,

    /// <summary>
    /// The 1 key.
    /// </summary>
    Number1 = SilkKey.Number1,

    /// <summary>
    /// The 2 key.
    /// </summary>
    Number2 = SilkKey.Number2,

    /// <summary>
    /// The 3 key.
    /// </summary>
    Number3 = SilkKey.Number3,

    /// <summary>
    /// The 4 key.
    /// </summary>
    Number4 = SilkKey.Number4,

    /// <summary>
    /// The 5 key.
    /// </summary>
    Number5 = SilkKey.Number5,

    /// <summary>
    /// The 6 key.
    /// </summary>
    Number6 = SilkKey.Number6,

    /// <summary>
    /// The 7 key.
    /// </summary>
    Number7 = SilkKey.Number7,

    /// <summary>
    /// The 8 key.
    /// </summary>
    Number8 = SilkKey.Number8,

    /// <summary>
    /// The 9 key.
    /// </summary>
    Number9 = SilkKey.Number9,

    /// <summary>
    /// The semicolon key.
    /// </summary>
    Semicolon = SilkKey.Semicolon /* ; */,

    /// <summary>
    /// The equal key.
    /// </summary>
    Equal = SilkKey.Equal /* = */,

    /// <summary>
    /// The A key.
    /// </summary>
    A = SilkKey.A,

    /// <summary>
    /// The B key.
    /// </summary>
    B = SilkKey.B,

    /// <summary>
    /// The C key.
    /// </summary>
    C = SilkKey.C,

    /// <summary>
    /// The D key.
    /// </summary>
    D = SilkKey.D,

    /// <summary>
    /// The E key.
    /// </summary>
    E = SilkKey.E,

    /// <summary>
    /// The F key.
    /// </summary>
    F = SilkKey.F,

    /// <summary>
    /// The G key.
    /// </summary>
    G = SilkKey.G,

    /// <summary>
    /// The H key.
    /// </summary>
    H = SilkKey.H,

    /// <summary>
    /// The I key.
    /// </summary>
    I = SilkKey.I,

    /// <summary>
    /// The J key.
    /// </summary>
    J = SilkKey.J,

    /// <summary>
    /// The K key.
    /// </summary>
    K = SilkKey.K,

    /// <summary>
    /// The L key.
    /// </summary>
    L = SilkKey.L,

    /// <summary>
    /// The M key.
    /// </summary>
    M = SilkKey.M,

    /// <summary>
    /// The N key.
    /// </summary>
    N = SilkKey.N,

    /// <summary>
    /// The O key.
    /// </summary>
    O = SilkKey.O,

    /// <summary>
    /// The P key.
    /// </summary>
    P = SilkKey.P,

    /// <summary>
    /// The Q key.
    /// </summary>
    Q = SilkKey.Q,

    /// <summary>
    /// The R key.
    /// </summary>
    R = SilkKey.R,

    /// <summary>
    /// The S key.
    /// </summary>
    S = SilkKey.S,

    /// <summary>
    /// The T key.
    /// </summary>
    T = SilkKey.T,

    /// <summary>
    /// The U key.
    /// </summary>
    U = SilkKey.U,

    /// <summary>
    /// The V key.
    /// </summary>
    V = SilkKey.V,

    /// <summary>
    /// The W key.
    /// </summary>
    W = SilkKey.W,

    /// <summary>
    /// The X key.
    /// </summary>
    X = SilkKey.X,

    /// <summary>
    /// The Y key.
    /// </summary>
    Y = SilkKey.Y,

    /// <summary>
    /// The Z key.
    /// </summary>
    Z = SilkKey.Z,

    /// <summary>
    /// The left bracket(opening bracket) key.
    /// </summary>
    LeftBracket = SilkKey.LeftBracket /* [ */,

    /// <summary>
    /// The backslash.
    /// </summary>
    BackSlash = SilkKey.BackSlash /* \ */,

    /// <summary>
    /// The right bracket(closing bracket) key.
    /// </summary>
    RightBracket = SilkKey.RightBracket /* ] */,

    /// <summary>
    /// The grave accent key.
    /// </summary>
    GraveAccent = SilkKey.GraveAccent /* ` */,

    /// <summary>
    /// Non US keyboard layout key 1.
    /// </summary>
    World1 = SilkKey.World1 /* non-US #1 */,

    /// <summary>
    /// Non US keyboard layout key 2.
    /// </summary>
    World2 = SilkKey.World2 /* non-US #2 */,

    /// <summary>
    /// The escape key.
    /// </summary>
    Escape = SilkKey.Escape,

    /// <summary>
    /// The enter key.
    /// </summary>
    Enter = SilkKey.Enter,

    /// <summary>
    /// The tab key.
    /// </summary>
    Tab = SilkKey.Tab,

    /// <summary>
    /// The backspace key.
    /// </summary>
    Backspace = SilkKey.Backspace,

    /// <summary>
    /// The insert key.
    /// </summary>
    Insert = SilkKey.Insert,

    /// <summary>
    /// The delete key.
    /// </summary>
    Delete = SilkKey.Delete,

    /// <summary>
    /// The right arrow key.
    /// </summary>
    Right = SilkKey.Right,

    /// <summary>
    /// The left arrow key.
    /// </summary>
    Left = SilkKey.Left,

    /// <summary>
    /// The down arrow key.
    /// </summary>
    Down = SilkKey.Down,

    /// <summary>
    /// The up arrow key.
    /// </summary>
    Up = SilkKey.Up,

    /// <summary>
    /// The page up key.
    /// </summary>
    PageUp = SilkKey.PageUp,

    /// <summary>
    /// The page down key.
    /// </summary>
    PageDown = SilkKey.PageDown,

    /// <summary>
    /// The home key.
    /// </summary>
    Home = SilkKey.Home,

    /// <summary>
    /// The end key.
    /// </summary>
    End = SilkKey.End,

    /// <summary>
    /// The caps lock key.
    /// </summary>
    CapsLock = SilkKey.CapsLock,

    /// <summary>
    /// The scroll lock key.
    /// </summary>
    ScrollLock = SilkKey.ScrollLock,

    /// <summary>
    /// The num lock key.
    /// </summary>
    NumLock = SilkKey.NumLock,

    /// <summary>
    /// The print screen key.
    /// </summary>
    PrintScreen = SilkKey.PrintScreen,

    /// <summary>
    /// The pause key.
    /// </summary>
    Pause = SilkKey.Pause,

    /// <summary>
    /// The F1 key.
    /// </summary>
    F1 = SilkKey.F1,

    /// <summary>
    /// The F2 key.
    /// </summary>
    F2 = SilkKey.F2,

    /// <summary>
    /// The F3 key.
    /// </summary>
    F3 = SilkKey.F3,

    /// <summary>
    /// The F4 key.
    /// </summary>
    F4 = SilkKey.F4,

    /// <summary>
    /// The F5 key.
    /// </summary>
    F5 = SilkKey.F5,

    /// <summary>
    /// The F6 key.
    /// </summary>
    F6 = SilkKey.F6,

    /// <summary>
    /// The F7 key.
    /// </summary>
    F7 = SilkKey.F7,

    /// <summary>
    /// The F8 key.
    /// </summary>
    F8 = SilkKey.F8,

    /// <summary>
    /// The F9 key.
    /// </summary>
    F9 = SilkKey.F9,

    /// <summary>
    /// The F10 key.
    /// </summary>
    F10 = SilkKey.F10,

    /// <summary>
    /// The F11 key.
    /// </summary>
    F11 = SilkKey.F11,

    /// <summary>
    /// The F12 key.
    /// </summary>
    F12 = SilkKey.F12,

    /// <summary>
    /// The F13 key.
    /// </summary>
    F13 = SilkKey.F13,

    /// <summary>
    /// The F14 key.
    /// </summary>
    F14 = SilkKey.F14,

    /// <summary>
    /// The F15 key.
    /// </summary>
    F15 = SilkKey.F15,

    /// <summary>
    /// The F16 key.
    /// </summary>
    F16 = SilkKey.F16,

    /// <summary>
    /// The F17 key.
    /// </summary>
    F17 = SilkKey.F17,

    /// <summary>
    /// The F18 key.
    /// </summary>
    F18 = SilkKey.F18,

    /// <summary>
    /// The F19 key.
    /// </summary>
    F19 = SilkKey.F19,

    /// <summary>
    /// The F20 key.
    /// </summary>
    F20 = SilkKey.F20,

    /// <summary>
    /// The F21 key.
    /// </summary>
    F21 = SilkKey.F21,

    /// <summary>
    /// The F22 key.
    /// </summary>
    F22 = SilkKey.F22,

    /// <summary>
    /// The F23 key.
    /// </summary>
    F23 = SilkKey.F23,

    /// <summary>
    /// The F24 key.
    /// </summary>
    F24 = SilkKey.F24,

    /// <summary>
    /// The F25 key.
    /// </summary>
    F25 = SilkKey.F25,

    /// <summary>
    /// The 0 key on the key pad.
    /// </summary>
    Keypad0 = SilkKey.Keypad0,

    /// <summary>
    /// The 1 key on the key pad.
    /// </summary>
    Keypad1 = SilkKey.Keypad1,

    /// <summary>
    /// The 2 key on the key pad.
    /// </summary>
    Keypad2 = SilkKey.Keypad2,

    /// <summary>
    /// The 3 key on the key pad.
    /// </summary>
    Keypad3 = SilkKey.Keypad3,

    /// <summary>
    /// The 4 key on the key pad.
    /// </summary>
    Keypad4 = SilkKey.Keypad4,

    /// <summary>
    /// The 5 key on the key pad.
    /// </summary>
    Keypad5 = SilkKey.Keypad5,

    /// <summary>
    /// The 6 key on the key pad.
    /// </summary>
    Keypad6 = SilkKey.Keypad6,

    /// <summary>
    /// The 7 key on the key pad.
    /// </summary>
    Keypad7 = SilkKey.Keypad7,

    /// <summary>
    /// The 8 key on the key pad.
    /// </summary>
    Keypad8 = SilkKey.Keypad8,

    /// <summary>
    /// The 9 key on the key pad.
    /// </summary>
    Keypad9 = SilkKey.Keypad9,

    /// <summary>
    /// The decimal key on the key pad.
    /// </summary>
    KeypadDecimal = SilkKey.KeypadDecimal,

    /// <summary>
    /// The divide key on the key pad.
    /// </summary>
    KeypadDivide = SilkKey.KeypadDivide,

    /// <summary>
    /// The multiply key on the key pad.
    /// </summary>
    KeypadMultiply = SilkKey.KeypadMultiply,

    /// <summary>
    /// The subtract key on the key pad.
    /// </summary>
    KeypadSubtract = SilkKey.KeypadSubtract,

    /// <summary>
    /// The add key on the key pad.
    /// </summary>
    KeypadAdd = SilkKey.KeypadAdd,

    /// <summary>
    /// The enter key on the key pad.
    /// </summary>
    KeypadEnter = SilkKey.KeypadEnter,

    /// <summary>
    /// The equal key on the key pad.
    /// </summary>
    KeypadEqual = SilkKey.KeypadEqual,

    /// <summary>
    /// The left shift key.
    /// </summary>
    ShiftLeft = SilkKey.ShiftLeft,

    /// <summary>
    /// The left control key.
    /// </summary>
    ControlLeft = SilkKey.ControlLeft,

    /// <summary>
    /// The left alt key.
    /// </summary>
    AltLeft = SilkKey.AltLeft,

    /// <summary>
    /// The left super key.
    /// </summary>
    SuperLeft = SilkKey.SuperLeft,

    /// <summary>
    /// The right shift key.
    /// </summary>
    ShiftRight = SilkKey.ShiftRight,

    /// <summary>
    /// The right control key.
    /// </summary>
    ControlRight = SilkKey.ControlRight,

    /// <summary>
    /// The right alt key.
    /// </summary>
    AltRight = SilkKey.AltRight,

    /// <summary>
    /// The right super key.
    /// </summary>
    SuperRight = SilkKey.SuperRight,

    /// <summary>
    /// The menu key.
    /// </summary>
    Menu = SilkKey.Menu
}