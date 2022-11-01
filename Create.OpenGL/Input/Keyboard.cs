using Create.Virtuals;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Create.Input;

public static partial class Keyboard
{
    static Dictionary<Keys, bool> keys_status = keys_map();
    static List<Keys> keys_down = new();
    static List<Keys> keys_up = new();
    static Dictionary<Keys, Key> keys_prox = keys_prox_map();
    static VirtualDictionaty<Keys, Key> keys_gateway = VirtualDictionaty.Create(keys_prox).Finsh();

    public static VirtualDictionaty<Keys, Key> Keys => keys_gateway;

    static Dictionary<Keys, bool> keys_map()
    {
        Dictionary<Keys, bool> keys = new();
        foreach (var key in (Keys[])Enum.GetValues(typeof(Keys)))
            keys.TryAdd(key, false);
        return keys;
    }
    static Dictionary<Keys, Key> keys_prox_map()
    {
        Dictionary<Keys, Key> keys = new();
        foreach (var key in keys_status)
            keys.Add(key.Key, new(key.Key));
        return keys;
    }
    
    internal static void KeyDown(KeyboardKeyEventArgs args)
    {
        keys_status[args.Key] = true;
        keys_down.Add(args.Key);
    }
    internal static void KeyUp(KeyboardKeyEventArgs args)
    {
        keys_status[args.Key] = false;
        keys_up.Add(args.Key);
    }
    internal static void clear()
    {
        keys_up.Clear();
        keys_down.Clear();
    }

    public sealed class Key
    {
        Keys key;
        internal Key(Keys key)
        {
            this.key = key;
        }

        public Keys KeyCode => key;
        public bool Status => keys_status[key];
        public bool Down => keys_down.Contains(key);
        public bool Up => keys_up.Contains(key);
    }
}