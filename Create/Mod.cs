using Create.Elements;
using Create.OpenGL.GUI;
using Create.Resource;
using Create.Space;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace Create;

public sealed class Mod
{
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
    public static Mod[] All => mods.ToArray();

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
    [RequiresPreviewFeatures]
    public Mod RegisterInterfaceLoadingMethod<T>(string name) where T : Element, IElementLoading<T>
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        var code_name = $"{Name}:{name}";
        if (Assets.interfaceElementTypes.ContainsKey(code_name))
            throw new ArgumentException($"This element is alredy registered");
        Assets.interfaceElementTypes.Add(code_name, (this, T.Parse, (e, o) => T.ChangeEvent((T)e, o), T.ChangeEventParameter));
        return this;
    }

    [RequiresPreviewFeatures]
    public Mod RegisterInterface<T>(string name) where T : UserInterface, IUserInterface<T>
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        var code_name = $"{Name}:{name}";
        if (Register.userinterfaces.ContainsKey(code_name))
            throw new ArgumentException($"This interface is alredy registered");
        Register.userinterfaces.Add(code_name, o => T.LoadInterface(o));
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
        console.RegisterElement(element, name, this);
        return this;
    }
}
