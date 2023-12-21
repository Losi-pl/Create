namespace Create.Elements.Bazic.Blocks;

internal class OakLog : LogBase
{
    public override void OnRegistered(Mod mod)
    {
        SetModel(Assets.LoadBlockModel("create:oak-log"));
    }
}
