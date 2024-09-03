using Create.Conteiner;
using Create.Elements;
using Create.Elements.Gui;
using Create.Linq;
using Create.Sceans;
using System.Reflection;

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
        var rez = func.Invoke(new() { Player = Me, AditionalParameters = sender });
        if (rez.status is null || rez.point is null)
            throw new ArgumentNullException("One of results of interface creation is not specified");

        rez.status.bind_player(Me);
        gam.Interface.MainElements.Find(rez.status.GetType().GetCustomAttribute<PassiveInterfaceAttribute>() is not null ?
            "Passive Interface" : 
            (rez.status.GetType().GetCustomAttribute<OnTopInterfaceAttribute>() is not null ?
                "Top Passive Interface" : "Active Interface"), false)?.Childs.AddChild(rez.point);
        gam.UserInterfaces.Add((name, rez.status, rez.point, rez.status.GetType().GetCustomAttribute<OnTopInterfaceAttribute>() is not null));
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
        var rez = func.Invoke(new() { Player = Me, AditionalParameters = sender });
        if (rez.status is null || rez.point is null)
            throw new ArgumentNullException("One of results of interface creation is not specified");

        rez.status.bind_player(Me);
        gam.Interface.MainElements.Find(typeof(T).GetCustomAttribute<PassiveInterfaceAttribute>() is not null ?
            "Passive Interface" : 
            (typeof(T).GetCustomAttribute<OnTopInterfaceAttribute>() is not null ? 
                "Top Passive Interface" : "Active Interface"), false)?.Childs.AddChild(rez.point);
        gam.UserInterfaces.Add((key.name, rez.status, rez.point, typeof(T).GetCustomAttribute<OnTopInterfaceAttribute>() is not null));
        return (T)rez.status;
    }

    /// <summary>
    /// Pobiera wrzystkie interfejsy otwarte dla lokalnego użytkownika o nazwie <paramref name="name"/>
    /// </summary>
    /// <param name="name">Nazwa interfejsu</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<UserInterface> GetUserInterfaces(string name)
    {
        if (local_player is null)
            throw new("Player is not loadet");
        if (!Register.userinterfaces.ContainsKey(k => k.name == name))
            throw new ArgumentException("Interface shemat with that type doesn't exist");
        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        return gam.UserInterfaces.Where(i => i.name == name).Select(i => i.user);
    }

    /// <summary>
    /// Pobiera wrzystkie interfejsy otwarte dla lokalnego użytkownika o typie <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Typ interfejsu</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<T> GetUserInterfaces<T>() where T : UserInterface, IUserInterface<T>
    {
        if (local_player is null)
            throw new("Player is not loadet");
        if (!Register.userinterfaces.TryGetValue(k => k.type == typeof(T), out _, out var key))
            throw new ArgumentException("Interface shemat with that type doesn't exist");
        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        return gam.UserInterfaces.Where(i => i.name == key.name).Select(i => (T)i.user);
    }

    /// <summary>
    /// Pobiera pierwszy z rzędu interfjs dla lokalnego urzytkownika dla lokalnego użytkownika o nazwie <paramref name="name"/>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static UserInterface? GetUserInterface(string name) => GetUserInterfaces(name).FirstOrDefault();

    /// <summary>
    /// Pobiera pierwszy z rzędu interfjs dla lokalnego urzytkownika dla lokalnego użytkownika o typie <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetUserInterface<T>() where T : UserInterface, IUserInterface<T> => 
        GetUserInterfaces<T>().FirstOrDefault();

    public static bool RemoveUserInterface(UserInterface userInterface)
    {
        if (local_player is null)
            throw new("Player is not loadet");
        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        var user = gam.UserInterfaces.FindAndWhere(ui => ui.user == userInterface);
        if (!user.HasValue)
            return false;
        gam.UserInterfaces.RemoveAt(user.Value.index);
        user.Value.element.point.Parent!.Childs.RemoveChild(user.Value.element.point);
        return true;
    }

    public static IEnumerable<UserInterface> GetUserInterfaces()
    {
        if (local_player is null)
            throw new("Player is not loadet");
        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        return gam.UserInterfaces.ConvertAll(ui => ui.user);
    }

    public static ItemSlot TransferedItemSlot => (OpenGL.Engine.Scean as GameView)?
                                                 .Interface.MainElements.Find("Transferred Item")?.Element as ItemSlot ??
                                              throw new("Game isn't active");
}