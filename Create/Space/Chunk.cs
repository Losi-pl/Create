using Create.Conteiner;
using Create.Elements;
using System.Runtime.CompilerServices;

namespace Create.Space;

public sealed class Chunk
{
    public const int QUARD_SIZE = 16, QUARD_STACK = 16, CHUNK_HEIGHT = QUARD_SIZE * QUARD_STACK;

    PlacedBlock[][,,] blocks = new PlacedBlock[QUARD_STACK][,,];
    uint[] block_content = new uint[QUARD_STACK];
    List<int> modified_quard = new();
    object task_lock = new();
    internal List<LivingEntity> local_entitys = new();

    public PlacedBlock this[int x, int y, int z]
    {
        get
        {
            lock (task_lock)
            {
                var y_ = y / QUARD_SIZE;
                var qu = blocks[y_];
                if (qu != null)
                    return qu[x, y % QUARD_SIZE, z];
                else
                    return new(Blocks.AIR);
            }
        }
        set
        {
            lock (task_lock)
            {
                var y_ = y / QUARD_SIZE;
                var Y = y % QUARD_SIZE;
                var qu = blocks[y_];
                if (value.Block == Blocks.AIR)
                {
                    if (qu == null)
                        return;
                    var old = qu[x, Y, z];
                    qu[x,Y,z] = value;
                    if (old.Block != Blocks.AIR)
                        block_content[y_]--;
                    if (block_content[y_] == 0)
                        blocks[y_] = null!;
                }
                else
                {
                    if(qu == null)
                    {
                        PlacedBlock[,,] bls = new PlacedBlock[QUARD_SIZE, QUARD_SIZE, QUARD_SIZE];
                        blocks[y_] = bls;
                        qu = bls;
                    }
                    var old = qu[x, Y, z];
                    qu[x, Y, z] = value;
                    if (old.Block == Blocks.AIR)
                        block_content[y_]++;
                }
            }
        }
    }

    internal uint[] quards_content => block_content;
    internal IEnumerable<int> las_modified_quards()
    {
        lock(task_lock)
        {
            if(modified_quard.Count == 0)
                return Enumerable.Empty<int>();
            var list = modified_quard;
            list.Sort();
            modified_quard = new();
            return list;
        }
    }
}
