namespace Create.Initialization;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ModAttribute : Attribute
{
    string version, code_name;

    public ModAttribute(string version, string codeName)
    {
        this.version = version;
        code_name = codeName;
    }

    public string Version => version;
    public string CodeName => code_name;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class ModIniterAttribute : Attribute { }