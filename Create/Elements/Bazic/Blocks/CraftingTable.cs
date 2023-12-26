using Create.Elements.Interfaces;
using Create.Net;

namespace Create.Elements.Bazic.Blocks;

internal class CraftingTable : Block
{
    public override void OnRegistered(Mod mod)
    {
        SetModel(Assets.LoadBlockModel("create:crafting-table"));
    }

    public override bool OnClick(OnClickArgs args)
    {
        if (base.OnClick(args))
            return true;
        if (args.Button == OpenGL.GUI.ClickEventButton.Right)
        {
            foreach (var i in Client.GetUserInterfaces().Where(ui => !(ui.IsPassive || ui.IsOnTop)).ToArray())
                Client.RemoveUserInterface(i);
            Client.CreateUserInterface<CraftingTableInterface>((args.BlockPozition, args.World, args.Player));
            args.Player.Entity!.Data.Set("inventory_open", true);
            return true;
        }
        return false;
    }
}
