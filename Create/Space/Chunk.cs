using Create.Conteiner;
using Create.Elements;
using System.Runtime.CompilerServices;

namespace Create.Space;

public sealed class Chunk
{
    public const int QUARD_SIZE = 16, QUARD_STACK = 16, CHUNK_HEIGHT = QUARD_SIZE * QUARD_STACK;

    PlacedBlock[,,] blocks = new PlacedBlock[QUARD_SIZE, QUARD_SIZE * QUARD_STACK, QUARD_SIZE];
    uint[] block_content = new uint[QUARD_STACK];
    List<int> modified_quard = new();
    object task_lock = new();
    internal List<LivingEntity> local_entitys = new();

    public PlacedBlock this[int x, int y, int z]
    {
        get => blocks[x, y, z]; 
        set
        {
            int d_index = y / QUARD_SIZE;
            lock(task_lock) 
            {
                var old = blocks[x, y, z];
                blocks[x, y, z] = value;
                int q_m = y / QUARD_SIZE;
                if(!modified_quard.Contains(q_m))
                    modified_quard.Add(q_m);
                if (old.Block == Blocks.AIR && value.Block != Blocks.AIR)
                    block_content[d_index]++;
                else if (old.Block != Blocks.AIR && value.Block == Blocks.AIR)
                    block_content[d_index]--;
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
