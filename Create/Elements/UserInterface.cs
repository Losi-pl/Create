using Create.Elements.Interfaces;
using Create.OpenGL.GUI;
using System.Reflection;
using System.Runtime.Versioning;

namespace Create.Elements;

public interface IUserInterface<T> where T : UserInterface
{
    [RequiresPreviewFeatures]
    internal abstract static (T status, SpacePoint point) LoadInterface(UserInterface.InterfaceCreatorArgs args);
}

public abstract class UserInterface
{
#nullable disable
    Net.Player player;
#nullable restore

    public virtual void Update(UpdateArgs args) { }

    public bool IsPassive => GetType().GetCustomAttribute<PassiveInterface>() is not null;

    public struct UpdateArgs
    {
        public double time;
        public bool activeInventory;
    }

    internal void bind_player(Net.Player player) => this.player = player;

    public Net.Player Player => player;
    internal static void LoadInterfaces(Mod mod)
    {
        mod.RegisterInterface<CreativeInventory>("creativeinventory");
        mod.RegisterInterface<InformationBars>("informationbars");
    }

    public struct InterfaceCreatorArgs
    {
        public object? AditionalParameters;
        public Net.Player Player;
    }
}