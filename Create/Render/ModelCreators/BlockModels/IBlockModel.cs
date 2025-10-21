using System.Xml.Linq;
using static Create.Elements.Block;
using Create.Linq;

namespace Create.Render.ModelCreators.BlockModels;

public interface IBlockModel
{
    public void GenerateModel(StandardBlockSet args, ModelConstructor constructor);

    public IBlockSideModel? GetBlockSide(StandardBlockSet sideSet, BlockSide side);

    internal static Dictionary<(Mod mod, string name), Func<XElement, IBlockModel>> interpreters = new();

    internal static void Load(Mod mod)
    {
        mod.BlockModelSystem("bazic", SolidBlock.Interpreter);
        mod.BlockModelSystem("rotatable", RotatableBlock.Interpreter);
        mod.BlockModelSystem("slab", SlabModel.Interpreter);
        mod.BlockModelSystem("stairs", StairsModel.Interpreter);
    }

    /// <summary>
    /// Metoda standardowa do weryfikacji czy dana strona bloku jest widzoczna
    /// </summary>
    protected internal static bool SideVisibilityTest(StandardBlockSet args, BlockSide side)
    {
        StandardBlockSet block_set = new()
        {
            pozition = args.pozition,
            world = args.world
        };

        switch (side)
        {
            case BlockSide.Top:
                block_set.pozition = block_set.pozition with { y = block_set.pozition.y + 1 };
                break;
            case BlockSide.Bottom:
                block_set.pozition = block_set.pozition with { y = block_set.pozition.y - 1 };
                break;
            case BlockSide.West:
                block_set.pozition = block_set.pozition with { x = block_set.pozition.x - 1 };
                break;
            case BlockSide.East:
                block_set.pozition = block_set.pozition with { x = block_set.pozition.x + 1 };
                break;
            case BlockSide.North:
                block_set.pozition = block_set.pozition with { z = block_set.pozition.z + 1 };
                break;
            case BlockSide.South:
                block_set.pozition = block_set.pozition with { z = block_set.pozition.z - 1 };
                break;
        }

        var block = args.world.GetBlock(block_set.pozition);
        block_set.block = block;
        return block.Block.IsSideVisible(block_set, side.Invert());
    }


    /// <summary>
    /// Wypełnia tablice z indeksami punktów pierwszej ściany
    /// </summary>
    protected internal static void SetFastDefault(Span<int> vertexIndexes)
    {
        vertexIndexes[0] = 0;
        vertexIndexes[1] = 1;
        vertexIndexes[2] = 2;
        vertexIndexes[3] = 3;
        vertexIndexes[4] = 2;
        vertexIndexes[5] = 1;
    }

    /// <summary>
    /// Wypełnia tablice z indeksami punktów ścian
    /// </summary>
    protected internal static void SetDefault(Span<int> vertexIndexes, OpenGL.MechDrawingMode mode = OpenGL.MechDrawingMode.Triangle, uint offset = 0)
    {
        if (mode == OpenGL.MechDrawingMode.Triangle)
        {
            for ((int i, uint face_count) = (0, ((uint)vertexIndexes.Length - offset) / 6); i < face_count; i++)
            {
                vertexIndexes[(i * 6) + 0] = (i * 4) + 0;
                vertexIndexes[(i * 6) + 1] = (i * 4) + 1;
                vertexIndexes[(i * 6) + 2] = (i * 4) + 2;
                vertexIndexes[(i * 6) + 3] = (i * 4) + 3;
                vertexIndexes[(i * 6) + 4] = (i * 4) + 2;
                vertexIndexes[(i * 6) + 5] = (i * 4) + 1;
            }
            if (((uint)vertexIndexes.Length - offset) % 6 > 0)
            {
                var vert_ind_start = vertexIndexes.Length - ((vertexIndexes.Length - (int)offset) % 6);
                var face_ind = (vertexIndexes.Length - (int)offset) / 6;

                if (vert_ind_start + 1 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 0] = (face_ind * 4) + 0;
                if (vert_ind_start + 2 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 1] = (face_ind * 4) + 1;
                if (vert_ind_start + 3 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 2] = (face_ind * 4) + 2;
                if (vert_ind_start + 4 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 3] = (face_ind * 4) + 3;
                if (vert_ind_start + 5 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 4] = (face_ind * 4) + 2;
                if (vert_ind_start + 6 >= vertexIndexes.Length)
                    vertexIndexes[vert_ind_start + 5] = (face_ind * 4) + 1;
            }
            return;
        }
        else if (mode == OpenGL.MechDrawingMode.Line)
        {
            // TODO - For lines
        }
    }
}
