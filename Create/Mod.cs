using Create.Elements;
using Create.Resource;
using Create.Space;
using System.Numerics;

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

    internal static void add_to_mod_list(Mod m) => mods.Add(m);

    public string Name => mod_name;
    public Version Version => version;
    public Resources Resources => resources;
    public static Mod[] All => mods.ToArray();

    public Mod RegisterElement(string name, Block block) => register_element(block, name, Register.blocks_console);
    public Mod RegisterElement(string name, Dimention dimention) => register_element(dimention, name, Register.dimentions_console);
    public Mod RegisterElement(string name, Entity entity) => register_element(entity, name, Register.entitys_console);

    Mod register_element<T>(T element, string name, Register.ElementRegister<T>.Console console) where T : Baze
    {
        console.RegisterElement(element, name, this);
        return this;
    }
}
