using Create.Conteiner;
using Create.Elements;
using Create.Sceans;

namespace Create.Net;

public static class Client
{
    static Player? local_player;

    internal static void load_save()
    {
        Server.init_server(false);
        foreach (var p in MathC.GetElementsFromCenter(10))
            Server.Dimentions[Dimentions.OVERWORLD].add_chunk(new(p.x, p.y));
        local_player = Server.LoadPlayer(new());
        OpenGL.Engine.Scean = new GameView();
    }

    public static Player Me => local_player ?? throw new("Player is not loadet");
}