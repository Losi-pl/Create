using Create.Render;
using SixLabors.ImageSharp;

namespace Create.Elements.Bazic.Blocks;

internal class Dirt : Block
{
    public override void OnRegistered(Mod mod)
    {
        SetModel(Assets.LoadBlockModel("create:dirt"));
    }
}
