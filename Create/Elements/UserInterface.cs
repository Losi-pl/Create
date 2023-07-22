using Create.Elements.Bazic.Interfaces;
using Create.OpenGL.GUI;
using System.Runtime.Versioning;

namespace Create.Elements;

[RequiresPreviewFeatures]
public interface IUserInterface<T> where T : UserInterface
{
    internal abstract static (T status, SpacePoint point) LoadInterface(object? aditionalParameters);
}

public abstract class UserInterface
{
    public virtual void Update(decimal time) { }

    [RequiresPreviewFeatures]
    internal static void LoadInterfaces(Mod mod)
    {
        mod.RegisterInterface<CreativeInventory>("creativeinventory");
    }
}