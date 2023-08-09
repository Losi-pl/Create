using Create.Elements;
using Create.Linq;
using Create.Net;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Conteiner;

/// <summary>
/// Instancja bytu
/// </summary>
public sealed class LivingEntity
{
    DataContainer _container = new();
    ChunkPoz? chunk;
    Vector3 in_chunk;
    DimentionSpace? dimention;
    Player? player;
    Entity entity;
    EntityModel mesh;

    public LivingEntity(Entity entity)
    {
        this.entity = entity;
        in_chunk = new();
        mesh = entity.GetModel(this);
        mesh.set_entity(this);
    }

    /// <summary>
    /// Łączy gracza z instancją bytu
    /// </summary>
    /// <param name="player"></param>
    internal void set_player(Player? player) => this.player = player;
    
    /// <summary>
    /// Ustawia wymiar w którym byt się znajduje
    /// </summary>
    /// <param name="dimention"></param>
    internal void set_diment(DimentionSpace? dimention) => this.dimention = dimention;
    
    /// <summary>
    /// Usuwa Chunk na któym byt się znajduje
    /// </summary>
    internal void remove_chunk() => chunk = null;

    /// <summary>
    /// Pozycja bytu względem centrum bloku
    /// </summary>
    public Vector3 Pozition
    {
        get => PozitionByCenter - new Vector3(.5f, 0, .5f);
        set => PozitionByCenter = value + new Vector3(.5f, 0, .5f);
    }
    
    /// <summary>
    /// Pozycja bytu względem centrum świata
    /// </summary>
    public Vector3 PozitionByCenter
    {
        get
        {
            if (!chunk.HasValue)
                return new();
            if (dimention == null)
                throw new Exception("This entity is not present in any dimension");
            Vector2 ch = new(chunk.Value.X * Space.Chunk.QUARD_SIZE, chunk.Value.Z * Space.Chunk.QUARD_SIZE);
            return new(ch.X + in_chunk.X, in_chunk.Y, ch.Y + in_chunk.Z);
        }
        set
        {
            if (dimention == null)
                throw new Exception("This entity is not present in any dimension");
            
            var in_c = (MathC.InSection(value.X, Space.Chunk.QUARD_SIZE), value.Y, MathC.InSection(value.Z, Space.Chunk.QUARD_SIZE)).ToVector();
            var ch = new ChunkPoz(MathC.Section(value.X, Space.Chunk.QUARD_SIZE), MathC.Section(value.Z, Space.Chunk.QUARD_SIZE));
            if(!chunk.HasValue)
            {
                Dimention?.change_entity_chunk(this, ch);
                chunk = ch;
            }
            else if (chunk.Value != ch)
            { 
                Dimention?.change_entity_chunk(this, ch);
                chunk = ch;
            }
            in_chunk = in_c;
        }
    }

    /// <summary>
    /// Chunk na którym byt sięznajduje
    /// </summary>
    public ChunkPoz Chunk => chunk ?? new();

    /// <summary>
    /// Pojemnik danych bytu
    /// </summary>
    public DataContainer Data => _container;

    /// <summary>
    /// Wymiar w którym byt się znajduje
    /// </summary>
    public DimentionSpace? Dimention => dimention;

    /// <summary>
    /// Gracz z którym byt jest połączony
    /// </summary>
    public Player? Player => player;

    /// <summary>
    /// Model bytu
    /// </summary>
    public EntityModel Model => mesh;

    /// <summary>
    /// Typ bytu
    /// </summary>
    public Entity Entity => entity;
}
