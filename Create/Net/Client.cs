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

    /// <summary>
    /// Tworzy instancje interfejsu dla lokalnego użytkownika
    /// </summary>
    /// <param name="name">Nazwa interfejsu w rejestrze</param>
    /// <param name="sender">Dodatkowe parametry do tworzenia interfejsu</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static UserInterface CreateUserInterface(string name, object? sender = null)
    {
        if (local_player is null)
            throw new("Player is not loadet");
        if (!Register.userinterfaces.TryGetValue(k => k.name == name, out var func))
            throw new ArgumentException("Interface shemat not found", nameof(name));

        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        var rez = func.Invoke(sender);

        gam.Interface.MainElements.Find("Active Interface", false)?.Childs.AddChild(rez.point);
        return rez.status;
    }

    /// <summary>
    /// <inheritdoc cref="CreateUserInterface(string, object?)"/>
    /// </summary>
    /// <typeparam name="T">Typ interfejsu</typeparam>
    /// <param name="sender">Dodatkowe parametry do tworzenia interfejsu</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static T CreateUserInterface<T>(object? sender = null) where T : UserInterface, IUserInterface<T>
    {
        if(local_player is null)
            throw new("Player is not loadet");
        if (!Register.userinterfaces.TryGetValue(k => k.type == typeof(T), out var func, out var key))
            throw new ArgumentException("Interface shemat with that type doesn't exist");

        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        var rez = func.Invoke(sender);

        gam.Interface.MainElements.Find("Active Interface", false)?.Childs.AddChild(rez.point);
        return (T)rez.status;
    }
}