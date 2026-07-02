using System.Reflection;
using Create.Assets;
using Create.Registry;

namespace Create;

internal class CreateEntryPoint: IMod
{
    public string Name => "Create";
    public Version Version { get; } = new(1, 0, 0, 0);
    public IResources Resources { get; } = new AssemblyResources(Assembly.GetCallingAssembly(), "main");
    
    public string[] Authors { get; } = ["Losi-pl"];
    public string[]? Urls { get; } = ["https://github.com/Losi-pl/Create/tree/dev/cs/main"];

    public void RegisterLoadingPrecesses(LoadingSystem entry)
    {
        Console.WriteLine($"{Name}, World!");
        
        entry.AddElementRegisterProcess(LoadElements);
    }

    void LoadElements(ElementRegister entry)
    {
        Console.WriteLine("Loading elements");
    }
}