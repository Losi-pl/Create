using Create.Graphics;

namespace Create.Registry;

internal sealed class LoadingScene: Scene
{
    protected override void OnConnect()
    {
        Title = "Create: Loading";
    }
}