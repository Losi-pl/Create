using Create.Elements.Interfaces;
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
    public virtual void Update(UpdateArgs args) { }

    internal static void LoadInterfaces(Mod mod)
    {
        mod.RegisterInterface<CreativeInventory>("creativeinventory");
    }

    public struct UpdateArgs
    {
        public double time;
        public bool activeInventory;
    }
}