using Create.Elements;
using Create.OpenGL.GUI;
using Create.Resource;
using Create.Space;
using System.Runtime.Versioning;
using System.Xml.Linq;
using Create.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Create;

[DebuggerDisplay("{debuggerDisplay, nq}")]
public sealed class Mod
{
    private static char[] allowedChars = 
        (new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm',
                      '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', '-'})
        .Select(c => char.IsLetter(c) ? (new char[] { c, char.ToUpper(c) }) : (new char[] { c })).SelectMany(l => l).ToArray();
    
    string mod_name;
    Version version;
    Resources resources;
    static List<Mod> mods = new();

    internal Mod(string mod_name, Version version, Resource.Resources resources)
    {
        this.mod_name = mod_name;
        this.version = version;
        this.resources = resources;
    }

    /// <summary>
    /// Dodaje moda do listy modyfikacji
    /// </summary>
    /// <param name="m"></param>
    internal static void add_to_mod_list(Mod m) => mods.Add(m);

    private string debuggerDisplay => $"{{Name: {mod_name}, Version: {version}}}";

    /// <summary>
    /// Nazwa modyfikacji
    /// </summary>
    public string Name => mod_name;

    /// <summary>
    /// Wersja modyfikacji
    /// </summary>
    public Version Version => version;
    
    /// <summary>
    /// Pakiet zasobów modyfikacji
    /// </summary>
    public Resources Resources => resources;

    /// <summary>
    /// Wszystkie modyfikacje w grze
    /// </summary>
    public static ReadOnlySpan<Mod> All => CollectionsMarshal.AsSpan(mods);

    /// <summary>
    /// Metoda dodawanie <paramref name="block"/> do rejestru
    /// </summary>
    /// <param name="name">Nazwa bloku</param>
    /// <param name="block">Klasa bloku</param>
    /// <returns></returns>
    public Mod RegisterElement(string name, Block block) => register_element(block, name, Register.blocks_console);

    /// <summary>
    /// Metoda dodawanie <paramref name="dimention"/> do rejestru
    /// </summary>
    /// <param name="name">Nazwa wymiaru</param>
    /// <param name="block">Klasa wymiaru</param>
    /// <returns></returns>
    public Mod RegisterElement(string name, Dimention dimention) => register_element(dimention, name, Register.dimentions_console);

    /// <summary>
    /// Metoda dodawanie <paramref name="entity"/> do rejestru
    /// </summary>
    /// <param name="name">Nazwa bytu</param>
    /// <param name="entity">Klasa bytu</param>
    /// <returns></returns>
    public Mod RegisterElement(string name, Entity entity) => register_element(entity, name, Register.entitys_console);

    /// <summary>
    /// Metoda dodawanie <paramref name="item"/> do rejestru
    /// </summary>
    /// <param name="name">Nazwa itemu</param>
    /// <param name="item">Klasa itemu</param>
    /// <returns></returns>
    public Mod RegisterElement(string name, Item item) => register_element(item, name, Register.items_console);

    /// <summary>
    /// Metoda dodawanie <paramref name="creativeTab"/> do rejestru
    /// </summary>
    /// <param name="name">Nazwa zakładki</param>
    /// <param name="creativeTab">Klasa zakładki</param>
    /// <returns></returns>
    public Mod RegisterElement(string name, CreativeTab creativeTab) => register_element(creativeTab, name, Register.creativetab_console);

    /// <summary>
    /// Dodawanie interpretera który konwertuje element zapisany w zasobach w kawałek interfejsu
    /// </summary>
    /// <param name="name">Nazwa interpretera</param>
    /// <param name="parse">Metoda interpretacji</param>
    /// <param name="changeEvent">Metoda urzywana gdy w modelu jest zapisana operacja zmiany parametrów</param>
    /// <param name="changeEventParameter">Operacja urzywana do konwertowania danych dla <paramref name="changeEvent"/> aby oszczędzać dane</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Mod RegisterInterfaceLoadingMethod(string name, Func<XElement ,Element> parse, Action<Element, object> changeEvent, Func<XElement, object> changeEventParameter)
    {
        if(parse is null) throw new ArgumentNullException(nameof(parse));
        if(changeEvent is null) throw new ArgumentNullException(nameof(changeEvent));
        if(changeEventParameter is null) throw new ArgumentNullException(nameof(changeEventParameter));
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!allowed_chars(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

        var code_name = $"{Name}:{name}";
        if(Assets.interfaceElementTypes.ContainsKey(code_name))
            throw new ArgumentException($"This element is alredy registered");
        Assets.interfaceElementTypes.Add(code_name, (this, parse, changeEvent, changeEventParameter));
        return this;
    }

    /// <summary>
    /// Dodawanie interpretera który konwertuje element zapisany w zasobach w kawałek interfejsu
    /// </summary>
    /// <param name="name">Nazwa interpretera</param>
    /// <param name="parse">Metoda interpretacji</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Mod RegisterInterfaceLoadingMethod(string name, Func<XElement, Element> parse)
    {
        if (parse is null) throw new ArgumentNullException(nameof(parse));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!allowed_chars(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

        var code_name = $"{Name}:{name}";
        if (Assets.interfaceElementTypes.ContainsKey(code_name))
            throw new ArgumentException($"This element is alredy registered");
        Assets.interfaceElementTypes.Add(code_name, (this, parse, null, null));
        return this;
    }

    /// <summary>
    /// Dodawanie interpretera który konwertuje element zapisany w zasobach w kawałek interfejsu
    /// </summary>
    /// <param name="name">Nazwa interpretera</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Mod RegisterInterfaceLoadingMethod<T>(string name) where T : Element, IElementLoading<T>
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!allowed_chars(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

        var code_name = $"{Name}:{name}";
        if (Assets.interfaceElementTypes.ContainsKey(code_name))
            throw new ArgumentException($"This element is alredy registered");
        Assets.interfaceElementTypes.Add(code_name, (this, T.Parse, (e, o) => T.ChangeEvent((T)e, o), T.ChangeEventParameter));
        return this;
    }

    public Mod RegisterInterface<T>(string name) where T : UserInterface, IUserInterface<T>
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        var code_name = $"{Name}:{name}";
        if (Register.userinterfaces.ContainsKey(k => k.name == code_name))
            throw new ArgumentException($"Interface {{{code_name}}} is alredy registered");
        if (Register.userinterfaces.ContainsKey(k => k.type == typeof(T)))
            throw new ArgumentException($"Interface type {{{typeof(T)}}} is alredy registered");
        Register.userinterfaces.Add((code_name, typeof(T)), o => T.LoadInterface(o));
        return this;
    }

    /// <summary>
    /// Uniwersalna metoda dodawania elementów wo rejestru
    /// </summary>
    /// <typeparam name="T">Typ elementu</typeparam>
    /// <param name="element">Klasa elementu</param>
    /// <param name="name">Nazwa elementu</param>
    /// <param name="console">Konsola do rejestru</param>
    /// <returns></returns>
    Mod register_element<T>(T element, string name, Register.ElementRegister<T>.Console console) where T : Baze
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        if (console is null) throw new ArgumentNullException(nameof(console));
        if (!allowed_chars(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");
        console.RegisterElement(element, name, this);
        return this;
    }

    static bool allowed_chars(string text) => text.All(c => allowedChars.Contains(c));
}
