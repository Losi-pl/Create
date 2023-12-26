using Create.OpenGL.GUI;
using Create.Elements.Gui;
using Create.Conteiner.Items;
using Create.Conteiner;
using Create.Net;
using OpenTK.Graphics.OpenGL;
using Create.Input;
using Create.Linq;

namespace Create.Elements.Interfaces;

internal class CraftingTableInterface : UserInterface, IUserInterface<CraftingTableInterface>
{
    #nullable disable
    SpacePoint root;
    #nullable restore

    static (CraftingTableInterface status, SpacePoint point) IUserInterface<CraftingTableInterface>.LoadInterface(InterfaceCreatorArgs args)
    {
        CraftingTableInterface cti = new();
        cti.root = Assets.GetInterface("create:crafting-table");

        return (cti, cti.root)!;
    }
}
