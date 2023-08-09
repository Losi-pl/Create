using Create.Conteiner;
using Create.Elements;
using Create.Linq;
using Create.Space;
using Create.Virtuals;
using OpenTK.Windowing.Common;

namespace Create.Net;

public static class Server
{
    static List<DimentionSpace> dimentions = new();
    static List<Player> players = new();

    /// <summary>
    /// Załadowane światy
    /// </summary>
    public static VirtualDictionaty<Dimention, DimentionSpace> Dimentions { get; } = VirtualDictionaty.Create<Dimention, DimentionSpace>()
        .EnumerableMethod(() => dimentions.Select(d => new KeyValuePair<Dimention, DimentionSpace>(d.Dimention, d)))
        .GetMethod(n => dimentions.Find(d => d.Dimention == n, new KeyNotFoundException()))
        .IsConteinedMethod(n => dimentions.Find(d => d.Dimention == n) != null)
        .CountMethod(() => dimentions.Count)
        .Finsh();
    
    /// <summary>
    /// Połączeni gracze
    /// </summary>
    public static VirtualDictionaty<Account, Player> Players { get; } = VirtualDictionaty.Create<Account, Player>()
        .EnumerableMethod(() => players.Select(p => new KeyValuePair<Account, Player>(p.Account, p)))
        .GetMethod(a => players.Find(p => p.Account.Equals(a), new KeyNotFoundException()))
        .IsConteinedMethod(a => players.Find(p => p.Account.Equals(a)) != null)
        .CountMethod(() => players.Count)
        .Finsh();

    /// <summary>
    /// Aktywuje serwer w trybie lokalnym albo globalnym weterminowanym przez <paramref name="global_server"/>
    /// </summary>
    /// <param name="global_server">Czy serwer ma być w trybie globalnym</param>
    internal static void init_server(bool global_server)
    {
        load_save();
        OpenGL.Engine.OnUpdateFrame += phisic_update;
    }
    
    /// <summary>
    /// Załaduj świat z plików
    /// </summary>
    static void load_save()
    {
        foreach (var d in Register.Dimentions)
            dimentions.Add(new(d));
    }
    
    /// <summary>
    /// Obliczanie fizyki w całości
    /// </summary>
    /// <param name="args"></param>
    internal static void phisic_update(FrameEventArgs args)
    {
        var delta_time = (float)args.Time;
        chunk_loading.update(args.Time);
        foreach (var d in dimentions)
            world_phisic(d, delta_time);
    }
    
    /// <summary>
    /// Obliczanie fizyki pojedyńczych światów
    /// </summary>
    /// <param name="dimention"></param>
    /// <param name="args">od ostatniego obliczania</param>
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
    
    /// <summary>
    /// Ładuje gracza za pomocą o identyikatorze <paramref name="account"/>
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    internal static Player LoadPlayer(Account account)
    {
        var entity_poz = Elements.Dimentions.OVERWORLD.GetNewSpawnPoint();
        var dimention = Dimentions[Elements.Dimentions.OVERWORLD];
        var ch_poz = DimentionSpace.calculate_chunk_pozition(entity_poz.x, entity_poz.z);
        if (!dimention.IsChunkLoadetOrLoading(ch_poz))
            dimention.add_chunk(ch_poz);
        while (dimention.IsChunkLoadet(ch_poz)) { Task.Delay(1); }
        var entity = dimention.Spawn(Entitys.PLAYER, entity_poz.ToVector().ToNumeric());
        var player = new Player(account);
        player.Entity = entity;
        players.Add(player);
        return player;
    }

    /// <summary>
    /// Mechanizm ładowania chunków w wontku pobocznym
    /// </summary>
    static class chunk_loading
    {
        static Task? task;
        static float query = 2;
        static float last = query;
        /// <summary>
        /// W odstępach czasowych określonych w <see cref="query"/> ładuje chunki o odległości 10 chunków od graczy jeżeli te nie są jeszcze załadowane
        /// </summary>
        /// <param name="time">Czas od ostatniego obliczenia</param>
        public static void update(double time)
        {
            if (last >= query)
            {
                last -= query;
                if(task != null)
                {
                    if (task.IsCanceled)
                        if (task.IsFaulted)
                            throw task.Exception!.InnerException!;
                }
                task = Task.Run(() =>
                {
                    foreach(var player in players.Where(p => p.Entity?.Dimention != null))
                    {
                        var dimen = player.Entity!.Dimention!;
                        foreach(var ch in MathC.GetElementsFromCenter(10)
                            .Select(ch => player.Entity!.Chunk + new ChunkPoz(ch.x, ch.y)))
                        {
                            if (dimen.IsChunkLoadetOrLoading(ch))
                                continue;
                            dimen.add_chunk(ch);
                        }
                    }
                });
            }
            else
                last += (float)time;
        }
    }
}
