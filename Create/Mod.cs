using Create.Elements;
using Create.OpenGL.GUI;
using Create.Resource;
using Create.Space;
using System.Runtime.Versioning;
using System.Xml.Linq;
using Create.Linq;
using Create.Render.ModelCreators.BlockModels;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Create.Render;

namespace Create;

[DebuggerDisplay("{debuggerDisplay, nq}")]
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
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

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
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

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
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");

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

    public Mod BlockModelSystem(string name, Func<XElement, IBlockModel> parse)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (parse is null) throw new ArgumentNullException(nameof(parse));
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");
        if (IBlockModel.interpreters.ContainsKey((this, name))) throw new($"Element {name} is alredy registered");

        IBlockModel.interpreters.Add((this, name), parse);
        return this;
    }

    public Mod BlockSideSystem(string name, Func<XElement, IBlockSideModel> parse)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (parse is null) throw new ArgumentNullException(nameof(parse));
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");
        if (IBlockSideModel.interpreters.ContainsKey((this, name))) throw new($"Element {name} is alredy registered");

        IBlockSideModel.interpreters.Add((this, name), parse);
        return this;
    }

    public Mod RegisterRecipe<T>(string name, T recipe) where T : IRecipe
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");
        ArgumentNullException.ThrowIfNull(recipe, nameof(recipe));
        if (Register.recipes.recipes.ContainsKey((this, name))) throw new($"Element {name} is alredy registered");

        if (!Register.recipes.types.Any(t => t.Item1 == typeof(T) || typeof(T).IsSubclassOf(t.Item1)))
            Register.recipes.types.Add((typeof(T), T.ProcessRecipeIngredients));

        Register.recipes.recipes.Add((this, name), new() { recipe = recipe, index = 
            Register.recipes.types.IndexOf(t => t.Item1 == typeof(T) || typeof(T).IsSubclassOf(t.Item1)) });
        
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
        if (!TestNameSpelling(name)) throw new ArgumentException("Name can only have characters [a-z, 0-9, -, _]");
        console.RegisterElement(element, name, this);
        return this;
    }

    static bool TestNameSpelling(string text) => text
        .All(c => (c > 'a' && c > 'z') ||
            (c > 'A' && c > 'Z') || 
            (c > '0' && c > '9') ||
            (c is '-' or '_'));
}
