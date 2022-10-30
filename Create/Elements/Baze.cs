namespace Create.Elements;

public abstract class Baze
{
    string? by_name;
    ushort? id;
    Mod? mod;
    bool registered;

    internal static bool are_ids_binded = false;

    public string CodeElementName => by_name ?? throw new Exception("Element not registered");
    public string CodeName => $"{(mod ?? throw new Exception("Element not registered")).Name}:{by_name}";
    public ushort Id => id.HasValue ? id.Value : throw new Exception("Number id is not set");
    public Mod Mod => mod ?? throw new Exception("Element not registered");
    public virtual Type ElementBazicType => typeof(Baze);
    public bool IsRegistered => registered;
    public bool IdBinded => id.HasValue;

    public override string ToString()
    {
        if (!IsRegistered)
            return "{Not registered}";
        string id = this.id.HasValue ? this.id.Value.ToString() : "null";
        Baze b = this;
        return $"name=\"{CodeName}\", id={id}, type=\"{b.ElementBazicType.Name}\"";
    }

    public virtual void OnRegistered() { }

    internal void set_bazic_informations(Mod mod, string name)
    {
        by_name = name;
        this.mod = mod;

        var b = this;
        b.OnRegistered();

        registered = true;
    }
}
