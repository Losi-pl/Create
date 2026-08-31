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
        Console.WriteLine($"World of {Name}!");
        
        entry.AddResourceProcessor(new BlockAtlasProcessor(), BlockAtlasProcessor.ASSET_PATH);
        entry.AddResourceProcessor(new ShaderProcessor(), ShaderProcessor.ASSET_PATH);
        
        entry.AddElementRegisterProcess(LoadElements);
    }

    private void LoadElements(ElementRegister entry)
    {
        Console.WriteLine("Loading elements");
    }
}