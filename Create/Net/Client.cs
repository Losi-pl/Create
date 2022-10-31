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
        var entity_poz = Dimentions.OVERWORLD.GetNewSpawnPoint();
        var entity = Server.Dimentions[Dimentions.OVERWORLD].Spawn(Entitys.PLAYER, entity_poz.ToVector().ToNumeric());
        local_player = new();
        local_player.Entity = entity;
        OpenGL.Engine.Scean = new GameView();
    }

    public static Player Me => local_player ?? throw new("Player is not loadet");
}