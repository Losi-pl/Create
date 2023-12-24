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
            return true;
        return false;
    }
}
