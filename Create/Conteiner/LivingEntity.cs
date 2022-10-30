using Create.Elements;
using Create.Net;
using Create.OpenGL;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Conteiner;

public sealed class LivingEntity
{
    DataContainer _container = new();
    ChunkPoz? chunk;
    Vector3 in_chunk;
    DimentionSpace? dimention;
    Player? player;
    Entity entity;
    Mesh mesh;

    public LivingEntity(Entity entity)
    {
        this.entity = entity;
        in_chunk = new();
        mesh = entity.GetModel(this);
    }

    internal void set_player(Player? player) => this.player = player;
    internal void set_diment(DimentionSpace? dimention) => this.dimention = dimention;
    internal void remove_chunk() => chunk = null;

    public Vector3 Pozition
    {
        get => PozitionByCenter - new Vector3(.5f, 0, .5f);
        set => PozitionByCenter = value + new Vector3(.5f, 0, .5f);
    }
    public Vector3 PozitionByCenter
    {
        get
        {
            if (!chunk.HasValue)
                return new();
            if (dimention == null)
                throw new Exception("This being is not present in any dimension.");
            Vector2 ch = new(chunk.Value.X * Space.Chunk.QUARD_SIZE, chunk.Value.Z * Space.Chunk.QUARD_SIZE);
            return new(ch.X + in_chunk.X, in_chunk.Y, ch.Y + in_chunk.Z);
        }
        set
        {
            if (dimention == null)
                throw new Exception("This being is not present in any dimension.");
            if (value.Y > Space.Chunk.CHUNK_HEIGHT)
                value.Y = Space.Chunk.CHUNK_HEIGHT;
            
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
    public ChunkPoz Chunk => chunk ?? new();
    public DataContainer Data => _container;
    public DimentionSpace? Dimention => dimention;
    public Player? Player => player;
    public Mesh Mesh => mesh;
    public Entity Entity => entity;
}
