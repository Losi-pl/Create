using System.Drawing;

namespace Create.Elements.Bazic.Blocks;

internal sealed class GrassBlock : GrassBase
{
    public override void OnRegistered(Mod mod)
    {
        SetModel(Assets.LoadBlockModel("create:grass-block"));
    }
}
