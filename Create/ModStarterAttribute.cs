namespace Create.Initialization;

/// <summary>
/// Podstawowe informacje modyfikacji
/// <para>Tylko jeden atrybut w projekcjie</para>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ModAttribute : Attribute
{
    string version, code_name;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="version">Wersja modyfikacji</param>
    /// <param name="codeName">Nazwa kodowa modyfikacji</param>
    public ModAttribute(string version, string codeName)
    {
        this.version = version;
        code_name = codeName;
    }

    /// <summary>
    /// Wersja modyfikacji
    /// </summary>
    public string Version => version;

    /// <summary>
    /// Nazwa kodowa modyfikacji
    /// </summary>
    public string CodeName => code_name;
}

/// <summary>
/// Przypisywany do funkcji inicjalizacyjnej modyfikacji w klasie zawierającej <see cref="ModAttribute"/>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class ModIniterAttribute : Attribute 
{
    public InitjalizationStage Stage { get; }
    public ModIniterAttribute(InitjalizationStage stage = InitjalizationStage.Main) { Stage = stage; }
}

public enum InitjalizationStage
{
    Initial = 1,
    Main = 0,
    Finishing = 2
}