using Create.Elements;
using Create.Resource;
using Create.Space;

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
