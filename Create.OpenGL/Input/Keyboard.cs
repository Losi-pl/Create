using Create.Virtuals;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Create.Input;

/// <summary>
/// Kontrolki klawiatury
/// </summary>
public static partial class Keyboard
{
    static Dictionary<Keys, bool> keys_status = keys_map();
    static List<Keys> keys_down = new();
    static List<Keys> keys_up = new();
    static Dictionary<Keys, Key> keys_prox = keys_prox_map();
    static VirtualDictionaty<Keys, Key> keys_gateway = VirtualDictionaty.Create(keys_prox).Finsh();

    /// <summary>
    /// Mapa wrzystkich przycisków
    /// </summary>
    public static VirtualDictionaty<Keys, Key> Keys => keys_gateway;

    /// <summary>
    /// Generuje biblioteke statusów czy jakiś przycisk jest wciśnięty
    /// </summary>
    static Dictionary<Keys, bool> keys_map()
    {
        Dictionary<Keys, bool> keys = new();
        foreach (var key in (Keys[])Enum.GetValues(typeof(Keys)))
            keys.TryAdd(key, false);
        return keys;
    }

    /// <summary>
    /// Generuje biblioteke statusów z informacjami o przycisku
    /// </summary>
    /// <returns></returns>
    static Dictionary<Keys, Key> keys_prox_map()
    {
        Dictionary<Keys, Key> keys = new();
        foreach (var key in keys_status)
            keys.Add(key.Key, new(key.Key));
        return keys;
    }
    
    /// <summary>
    /// Gdy przycisk jest wciśnięty
    /// </summary>
    internal static void KeyDown(KeyboardKeyEventArgs args)
    {
        keys_status[args.Key] = true;
        keys_down.Add(args.Key);
    }

    /// <summary>
    /// Gdy przycisk jest puszczony
    /// </summary>
    internal static void KeyUp(KeyboardKeyEventArgs args)
    {
        keys_status[args.Key] = false;
        keys_up.Add(args.Key);
    }
    
    /// <summary>
    /// Szyści wciśniętych i puszczonych przycisków
    /// </summary>
    internal static void clear()
    {
        keys_up.Clear();
        keys_down.Clear();
    }

    /// <summary>
    /// Statusy pojedyńczego przycisku
    /// </summary>
    public sealed class Key
    {
        Keys key;
        internal Key(Keys key)
        {
            this.key = key;
        }

        /// <summary>
        /// Nazwa kodowa przycisku w standardze US
        /// </summary>
        public Keys KeyCode => key;

        /// <summary>
        /// Czy w tej klatce przycisk jest wciśnięty
        /// </summary>
        public bool Status => keys_status[key];

        /// <summary>
        /// Czy w tej klatce przycisk został wciśnięty
        /// </summary>
        public bool Down => keys_down.Contains(key);

        /// <summary>
        /// Czy w tej klatce przycisk został puszczony
        /// </summary>
        public bool Up => keys_up.Contains(key);
    }
}