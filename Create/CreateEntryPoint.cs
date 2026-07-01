using System.Reflection;
using Create.Assets;
using Create.Registry;

namespace Create;

internal class CreateEntryPoint: IMod
{
    public string Name => "Create";
    public Version Version { get; } = new(1, 0, 0, 0);
    public IResources Resources { get; } = new AssemblyResources(Assembly.GetCallingAssembly(), "main");
    
    public void RegisterLoadingPrecesses(LoadingRegister entry)
    {
        Console.WriteLine("Create, World!");
        foreach (var resource in Resources.GetManifest())
        {
            Console.WriteLine("Resource: " + resource);
        }
    }
}