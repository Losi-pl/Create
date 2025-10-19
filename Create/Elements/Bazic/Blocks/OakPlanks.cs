using Create.Render;
using SixLabors.ImageSharp;

namespace Create.Elements.Bazic.Blocks;

internal partial class OakPlanks : Block
{
    public override void OnRegistered(Mod mod)
    {
        SetModel(Assets.LoadBlockModel("create:oak-planks"));
    }
}
