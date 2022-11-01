using Create.Conteiner;
using Create.Elements;
using Create.Space;
using Create.Virtuals;
using OpenTK.Windowing.Common;

namespace Create.Net;

public static class Server
{
    static List<DimentionSpace> dimentions = new();
    static List<Player> players = new();

    public static VirtualDictionaty<Dimention, DimentionSpace> Dimentions { get; } = VirtualDictionaty.Create<Dimention, DimentionSpace>()
        .EnumerableMethod(() => ((IEnumerable<DimentionSpace>)dimentions).ConvertAll(d => new KeyValuePair<Dimention, DimentionSpace>(d.Dimention, d)))
        .GetMethod(n => dimentions.Find(d => d.Dimention == n, new KeyNotFoundException()))
        .IsConteinedMethod(n => dimentions.Find(d => d.Dimention == n) != null)
        .CountMethod(() => dimentions.Count)
        .Finsh();
    public static VirtualDictionaty<Account, Player> Players { get; } = VirtualDictionaty.Create<Account, Player>()
        .EnumerableMethod(() => ((IEnumerable<Player>)players).ConvertAll(p => new KeyValuePair<Account, Player>(p.Account, p)))
        .GetMethod(a => players.Find(p => p.Account.Equals(a), new KeyNotFoundException()))
        .IsConteinedMethod(a => players.Find(p => p.Account.Equals(a)) != null)
        .CountMethod(() => players.Count)
        .Finsh();

    internal static void init_server(bool global_server)
    {
        load_save();
        OpenGL.Engine.OnUpdateFrame += phisic_update;
    }
    
    static void load_save()
    {
        foreach (var d in Register.Dimentions)
            dimentions.Add(new(d));
    }
    internal static void phisic_update(FrameEventArgs args)
    {
        var delta_time = (float)args.Time;
        foreach (var d in dimentions)
            world_phisic(d, delta_time);
    }
    static void world_phisic(DimentionSpace dimention, float args)
    {
        mob_physic(dimention.AllEntities);

        //Methods
        void mob_physic(IEnumerable<LivingEntity> entities)
        {
            foreach (var entity in entities)
                entity.Entity.OnPhisicUpdate(entity, args);
        }
    }
    internal static Player LoadPlayer(Account account)
    {
        var entity_poz = Elements.Dimentions.OVERWORLD.GetNewSpawnPoint();
        var entity = Dimentions[Elements.Dimentions.OVERWORLD].Spawn(Entitys.PLAYER, entity_poz.ToVector().ToNumeric());
        var player = new Player(account);
        player.Entity = entity;
        return player;
    }
}
