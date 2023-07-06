namespace Create.Elements;

/// <summary>
/// Baza do budowania elementów w grze
/// </summary>
public abstract class Baze
{
    string? by_name;
    ushort? id;
    Mod? mod;
    bool registered;

    internal static bool are_ids_binded = false;

    /// <summary>
    /// Nazwa kodowa elementu
    /// </summary>
    public string CodeElementName => by_name ?? throw new Exception("Element not registered");

    /// <summary>
    /// Nazwa kodowa elementu i mod pochodenia
    /// </summary>
    public string CodeName => $"{(mod ?? throw new Exception("Element not registered")).Name}:{by_name}";

    /// <summary>
    /// Opcjonalny numer elementu
    /// </summary>
    public ushort Id => id.HasValue ? id.Value : throw new Exception("Number id is not set");

    /// <summary>
    /// Mod pochadzenia elementu
    /// </summary>
    public Mod Mod => mod ?? throw new Exception("Element not registered");

    /// <summary>
    /// Bazowy typ elementu
    /// </summary>
    public virtual Type ElementBazicType => typeof(Baze);

    /// <summary>
    /// Czy został zarejestrowany
    /// </summary>
    public bool IsRegistered => registered;

    /// <summary>
    /// Czy ma przypisane id
    /// </summary>
    public bool IdBinded => id.HasValue;

    /// <summary>
    /// Podglądowa nazwa elementu
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (!IsRegistered)
            return "{Not registered}";
        string id = this.id.HasValue ? this.id.Value.ToString() : "null";
        Baze b = this;
        return $"name=\"{CodeName}\", id={id}, type=\"{b.ElementBazicType.Name}\"";
    }

    /// <summary>
    /// Wywoływana gdy element został zarejestrowany
    /// </summary>
    public virtual void OnRegistered() { }

    /// <summary>
    /// Ustawia podstawowe informacje elementu jak nazwa albo mod pochodzenia
    /// </summary>
    /// <param name="mod"></param>
    /// <param name="name"></param>
    internal void set_bazic_informations(Mod mod, string name)
    {
        by_name = name;
        this.mod = mod;

        var b = this;
        b.OnRegistered();

        registered = true;
    }
}
