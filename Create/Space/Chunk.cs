using Create.Conteiner;
using Create.Elements;

namespace Create.Space;

/// <summary>
/// Jednostka zawierająca kolumne terenu podzieloną na mniejsze sekcje terenu aby ułatwić organizacje kawałków terenu
/// </summary>
public sealed class Chunk
{
    public const int QUARD_SIZE = 16, QUARD_STACK = 16, CHUNK_HEIGHT = QUARD_SIZE * QUARD_STACK;

    PlacedBlock[][,,] blocks = new PlacedBlock[QUARD_STACK][,,];
    uint[] block_content = new uint[QUARD_STACK];
    List<int> modified_quard = new();
    internal List<LivingEntity> local_entitys = new();

    public PlacedBlock this[int x, int y, int z]
    {
        get
        {
            lock (blocks)
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
            lock (blocks)
            {
                var y_ = y / QUARD_SIZE;
                var Y = y % QUARD_SIZE;
                var qu = blocks[y_];
                if (qu?[x, Y, z] == value)
                    return;
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
                        var bls = new PlacedBlock[QUARD_SIZE, QUARD_SIZE, QUARD_SIZE];
                        blocks[y_] = bls;
                        qu = bls;
                    }
                    var old = qu[x, Y, z];
                    qu[x, Y, z] = value;
                    if (old.Block == Blocks.AIR)
                        block_content[y_]++;
                }
                modyfication(y_, false);
                if (Y == 0)
                    if (y_ > 0)
                        modyfication(y_ - 1, false);
                if(Y == QUARD_SIZE - 1)
                    if(y_ < QUARD_STACK - 1)
                        modyfication(y_ + 1, false);
            }
        }
    }

    /// <summary>
    /// Ile bloków w karzdym sześcianie jest innych niż powietrze
    /// </summary>
    internal uint[] quards_content => block_content;

    /// <summary>
    /// Lista sześcianów chunka których zawartość została zmieniona od ostatniego sprawdzenia
    /// </summary>
    /// <returns></returns>
    internal IEnumerable<int> las_modified_quards()
    {
        lock (blocks)
        {
            if(modified_quard.Count == 0)
                return Enumerable.Empty<int>();
            var list = modified_quard;
            list.Sort();
            modified_quard = new();
            return list;
        }
    }

    /// <summary>
    /// Oznacza dany sześcian jako ostatnio modyfikowany
    /// </summary>
    internal void modyfication(int quard, bool absolute)
    {
        lock(blocks)
        {
            quard = absolute ? quard / QUARD_SIZE : quard;
            if (!modified_quard.Contains(quard))
                modified_quard.Add(quard);
        }
    }
}
