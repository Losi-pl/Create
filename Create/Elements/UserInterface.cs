using Create.Elements.Bazic.Interfaces;
using Create.OpenGL.GUI;
using System.Runtime.Versioning;

namespace Create.Elements;

public interface IUserInterface<T> where T : UserInterface
{
    [RequiresPreviewFeatures]
    internal abstract static (T status, SpacePoint point) LoadInterface(object? aditionalParameters);
}

public abstract class UserInterface
{
    public virtual void Update(double time) { }

    internal static void LoadInterfaces(Mod mod)
    {
        mod.RegisterInterface<CreativeInventory>("creativeinventory");
    }
}