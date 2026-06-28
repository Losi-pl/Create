using System.Runtime.CompilerServices;
using Silk.NET.Input;

namespace Create.Input;

public class Keyboard
{
    private readonly IKeyboard _context;
    private Keys _keys;

    internal Keyboard(IInputContext context)
    {
        _context = context.Keyboards[0];
        
        foreach (var key in Enum.GetValues<Input.Key>())
        {
            var ind = KeyToIndex(key);
            if(ind is < 0 or >= 120)
                continue;
            
            _keys[ind] = new(key);
            _keys[ind].Combined = (_context.IsKeyPressed((Silk.NET.Input.Key)key), false, false, false);
        }
    }

    // ReSharper disable once UnusedMember.Global
    public ref readonly Key this[Input.Key key]
    {
        get
        {
            var ind = KeyToIndex(key);
            if (ind is < 0 or >= 120)
                throw new KeyNotFoundException();
            return ref _keys[ind];
        }
    }
    
    internal void MarkPressedKey(Input.Key key)
    {
        var ind = KeyToIndex(key);
        if (ind is < 0 or >= 120)
            throw new KeyNotFoundException();
        var status = _keys[ind].Combined;
        status.status = true;
        status.pressed = true;
        _keys[ind].Combined = status;
    }
    
    internal void MarkReleasedKey(Input.Key key)
    {
        var ind = KeyToIndex(key);
        if (ind is < 0 or >= 120)
            throw new KeyNotFoundException();
        var status = _keys[ind].Combined;
        status.status = false;
        status.released = true;
        _keys[ind].Combined = status;
    }
    
    private int KeyToIndex(Input.Key key) => key switch
    {
        Input.Key.Space => 0,
        Input.Key.Apostrophe => 1,
        Input.Key.Comma => 2,
        Input.Key.Minus => 3,
        Input.Key.Period => 4,
        Input.Key.Slash => 5,
        Input.Key.Number0 => 6,
        Input.Key.Number1 => 7,
        Input.Key.Number2 => 8,
        Input.Key.Number3 => 9,
        Input.Key.Number4 => 10,
        Input.Key.Number5 => 11,
        Input.Key.Number6 => 12,
        Input.Key.Number7 => 13,
        Input.Key.Number8 => 14,
        Input.Key.Number9 => 15,
        Input.Key.Semicolon => 16,
        Input.Key.Equal => 17,
        Input.Key.A => 18,
        Input.Key.B => 19,
        Input.Key.C => 20,
        Input.Key.D => 21,
        Input.Key.E => 22,
        Input.Key.F => 23,
        Input.Key.G => 24,
        Input.Key.H => 25,
        Input.Key.I => 26,
        Input.Key.J => 27,
        Input.Key.K => 28,
        Input.Key.L => 29,
        Input.Key.M => 30,
        Input.Key.N => 31,
        Input.Key.O => 32,
        Input.Key.P => 33,
        Input.Key.Q => 34,
        Input.Key.R => 35,
        Input.Key.S => 36,
        Input.Key.T => 37,
        Input.Key.U => 38,
        Input.Key.V => 39,
        Input.Key.W => 40,
        Input.Key.X => 41,
        Input.Key.Y => 42,
        Input.Key.Z => 43,
        Input.Key.LeftBracket => 44,
        Input.Key.BackSlash => 45,
        Input.Key.RightBracket => 46,
        Input.Key.GraveAccent => 47,
        Input.Key.World1 => 48,
        Input.Key.World2 => 49,
        Input.Key.Escape => 50,
        Input.Key.Enter => 51,
        Input.Key.Tab => 52,
        Input.Key.Backspace => 53,
        Input.Key.Insert => 54,
        Input.Key.Delete => 55,
        Input.Key.Right => 56,
        Input.Key.Left => 57,
        Input.Key.Down => 58,
        Input.Key.Up => 59,
        Input.Key.PageUp => 60,
        Input.Key.PageDown => 61,
        Input.Key.Home => 62,
        Input.Key.End => 63,
        Input.Key.CapsLock => 64,
        Input.Key.ScrollLock => 65,
        Input.Key.NumLock => 66,
        Input.Key.PrintScreen => 67,
        Input.Key.Pause => 68,
        Input.Key.F1 => 69,
        Input.Key.F2 => 70,
        Input.Key.F3 => 71,
        Input.Key.F4 => 72,
        Input.Key.F5 => 73,
        Input.Key.F6 => 74,
        Input.Key.F7 => 75,
        Input.Key.F8 => 76,
        Input.Key.F9 => 77,
        Input.Key.F10 => 78,
        Input.Key.F11 => 79,
        Input.Key.F12 => 80,
        Input.Key.F13 => 81,
        Input.Key.F14 => 82,
        Input.Key.F15 => 83,
        Input.Key.F16 => 84,
        Input.Key.F17 => 85,
        Input.Key.F18 => 86,
        Input.Key.F19 => 87,
        Input.Key.F20 => 88,
        Input.Key.F21 => 89,
        Input.Key.F22 => 90,
        Input.Key.F23 => 91,
        Input.Key.F24 => 92,
        Input.Key.F25 => 93,
        Input.Key.Keypad0 => 94,
        Input.Key.Keypad1 => 95,
        Input.Key.Keypad2 => 96,
        Input.Key.Keypad3 => 97,
        Input.Key.Keypad4 => 98,
        Input.Key.Keypad5 => 99,
        Input.Key.Keypad6 => 100,
        Input.Key.Keypad7 => 101,
        Input.Key.Keypad8 => 102,
        Input.Key.Keypad9 => 103,
        Input.Key.KeypadDecimal => 104,
        Input.Key.KeypadDivide => 105,
        Input.Key.KeypadMultiply => 106,
        Input.Key.KeypadSubtract => 107,
        Input.Key.KeypadAdd => 108,
        Input.Key.KeypadEnter => 109,
        Input.Key.KeypadEqual => 110,
        Input.Key.ShiftLeft => 111,
        Input.Key.ControlLeft => 112,
        Input.Key.AltLeft => 113,
        Input.Key.SuperLeft => 114,
        Input.Key.ShiftRight => 115,
        Input.Key.ControlRight => 116,
        Input.Key.AltRight => 117,
        Input.Key.SuperRight => 118,
        Input.Key.Menu => 119,
        _ => -1
    };

    // ReSharper disable UnusedMember.Global
    public ref readonly Key Space => ref _keys[0];
    public ref readonly Key Apostrophe => ref _keys[1];
    public ref readonly Key Comma => ref _keys[2];
    public ref readonly Key Minus => ref _keys[3];
    public ref readonly Key Period => ref _keys[4];
    public ref readonly Key Slash => ref _keys[5];
    public ref readonly Key Number0 => ref _keys[6];
    public ref readonly Key Number1 => ref _keys[7];
    public ref readonly Key Number2 => ref _keys[8];
    public ref readonly Key Number3 => ref _keys[9];
    public ref readonly Key Number4 => ref _keys[10];
    public ref readonly Key Number5 => ref _keys[11];
    public ref readonly Key Number6 => ref _keys[12];
    public ref readonly Key Number7 => ref _keys[13];
    public ref readonly Key Number8 => ref _keys[14];
    public ref readonly Key Number9 => ref _keys[15];
    public ref readonly Key Semicolon => ref _keys[16];
    public ref readonly Key Equal => ref _keys[17];
    public ref readonly Key A => ref _keys[18];
    public ref readonly Key B => ref _keys[19];
    public ref readonly Key C => ref _keys[20];
    public ref readonly Key D => ref _keys[21];
    public ref readonly Key E => ref _keys[22];
    public ref readonly Key F => ref _keys[23];
    public ref readonly Key G => ref _keys[24];
    public ref readonly Key H => ref _keys[25];
    public ref readonly Key I => ref _keys[26];
    public ref readonly Key J => ref _keys[27];
    public ref readonly Key K => ref _keys[28];
    public ref readonly Key L => ref _keys[29];
    public ref readonly Key M => ref _keys[30];
    public ref readonly Key N => ref _keys[31];
    public ref readonly Key O => ref _keys[32];
    public ref readonly Key P => ref _keys[33];
    public ref readonly Key Q => ref _keys[34];
    public ref readonly Key R => ref _keys[35];
    public ref readonly Key S => ref _keys[36];
    public ref readonly Key T => ref _keys[37];
    public ref readonly Key U => ref _keys[38];
    public ref readonly Key V => ref _keys[39];
    public ref readonly Key W => ref _keys[40];
    public ref readonly Key X => ref _keys[41];
    public ref readonly Key Y => ref _keys[42];
    public ref readonly Key Z => ref _keys[43];
    public ref readonly Key LeftBracket => ref _keys[44];
    public ref readonly Key BackSlash => ref _keys[45];
    public ref readonly Key RightBracket => ref _keys[46];
    public ref readonly Key GraveAccent => ref _keys[47];
    public ref readonly Key World1 => ref _keys[48];
    public ref readonly Key World2 => ref _keys[49];
    public ref readonly Key Escape => ref _keys[50];
    public ref readonly Key Enter => ref _keys[51];
    public ref readonly Key Tab => ref _keys[52];
    public ref readonly Key Backspace => ref _keys[53];
    public ref readonly Key Insert => ref _keys[54];
    public ref readonly Key Delete => ref _keys[55];
    public ref readonly Key Right => ref _keys[56];
    public ref readonly Key Left => ref _keys[57];
    public ref readonly Key Down => ref _keys[58];
    public ref readonly Key Up => ref _keys[59];
    public ref readonly Key PageUp => ref _keys[60];
    public ref readonly Key PageDown => ref _keys[61];
    public ref readonly Key Home => ref _keys[62];
    public ref readonly Key End => ref _keys[63];
    public ref readonly Key CapsLock => ref _keys[64];
    public ref readonly Key ScrollLock => ref _keys[65];
    public ref readonly Key NumLock => ref _keys[66];
    public ref readonly Key PrintScreen => ref _keys[67];
    public ref readonly Key Pause => ref _keys[68];
    public ref readonly Key F1 => ref _keys[69];
    public ref readonly Key F2 => ref _keys[70];
    public ref readonly Key F3 => ref _keys[71];
    public ref readonly Key F4 => ref _keys[72];
    public ref readonly Key F5 => ref _keys[73];
    public ref readonly Key F6 => ref _keys[74];
    public ref readonly Key F7 => ref _keys[75];
    public ref readonly Key F8 => ref _keys[76];
    public ref readonly Key F9 => ref _keys[77];
    public ref readonly Key F10 => ref _keys[78];
    public ref readonly Key F11 => ref _keys[79];
    public ref readonly Key F12 => ref _keys[80];
    public ref readonly Key F13 => ref _keys[81];
    public ref readonly Key F14 => ref _keys[82];
    public ref readonly Key F15 => ref _keys[83];
    public ref readonly Key F16 => ref _keys[84];
    public ref readonly Key F17 => ref _keys[85];
    public ref readonly Key F18 => ref _keys[86];
    public ref readonly Key F19 => ref _keys[87];
    public ref readonly Key F20 => ref _keys[88];
    public ref readonly Key F21 => ref _keys[89];
    public ref readonly Key F22 => ref _keys[90];
    public ref readonly Key F23 => ref _keys[91];
    public ref readonly Key F24 => ref _keys[92];
    public ref readonly Key F25 => ref _keys[93];
    public ref readonly Key Keypad0 => ref _keys[94];
    public ref readonly Key Keypad1 => ref _keys[95];
    public ref readonly Key Keypad2 => ref _keys[96];
    public ref readonly Key Keypad3 => ref _keys[97];
    public ref readonly Key Keypad4 => ref _keys[98];
    public ref readonly Key Keypad5 => ref _keys[99];
    public ref readonly Key Keypad6 => ref _keys[100];
    public ref readonly Key Keypad7 => ref _keys[101];
    public ref readonly Key Keypad8 => ref _keys[102];
    public ref readonly Key Keypad9 => ref _keys[103];
    public ref readonly Key KeypadDecimal => ref _keys[104];
    public ref readonly Key KeypadDivide => ref _keys[105];
    public ref readonly Key KeypadMultiply => ref _keys[106];
    public ref readonly Key KeypadSubtract => ref _keys[107];
    public ref readonly Key KeypadAdd => ref _keys[108];
    public ref readonly Key KeypadEnter => ref _keys[109];
    public ref readonly Key KeypadEqual => ref _keys[110];
    public ref readonly Key ShiftLeft => ref _keys[111];
    public ref readonly Key ControlLeft => ref _keys[112];
    public ref readonly Key AltLeft => ref _keys[113];
    public ref readonly Key SuperLeft => ref _keys[114];
    public ref readonly Key ShiftRight => ref _keys[115];
    public ref readonly Key ControlRight => ref _keys[116];
    public ref readonly Key AltRight => ref _keys[117];
    public ref readonly Key SuperRight => ref _keys[118];
    public ref readonly Key Menu => ref _keys[119];
    // ReSharper restore UnusedMember.Global
    
    public struct Key
    {
        internal Key(Input.Key key) => KeyCode = key;
        
        // ReSharper disable MemberCanBePrivate.Global
        public bool IsPressed { get; private set; }
        public bool JustPressed { get; private set; }
        public bool JustDoublePressed { get; private set; } //TODO: Finish this part
        public bool JustReleased { get; private set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Input.Key KeyCode { get; }
        // ReSharper restore MemberCanBePrivate.Global
        
        public (bool status, bool pressed, bool released, bool repeated) Combined
        {
            get => (IsPressed, JustPressed, JustReleased, JustDoublePressed);
            internal set => (IsPressed, JustPressed, JustReleased, JustDoublePressed) = value;
        }
    }

    [InlineArray(120)]
    private struct Keys
    {
        public Key _key;
    }
}