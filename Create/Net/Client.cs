using Create.Conteiner;
using Create.Elements;
using Create.Sceans;

namespace Create.Net;

public static class Client
{
    static Player? local_player;

    /// <summary>
    /// Ładuje świat z plików w trybie lokalnym
    /// </summary>
    internal static void load_save()
    {
        Server.init_server(false);
        local_player = Server.LoadPlayer(new());
        OpenGL.Engine.Scean = new GameView();
    }

    /// <summary>
    /// Gracz połączony z serwerem albo lokalnym światem
    /// </summary>
    public static Player Me => local_player ?? throw new("Player is not loadet");
}