using System.Xml.Linq;
using static Create.Elements.Block;
using Create.Linq;
using OpenTK.Mathematics;

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
    /// Wypełnia tablice z pozycjami tekstury pierwszej ściany
    /// </summary>
    protected internal static void SetFastDefault(Span<Vector2> uvs)
    {
        uvs[0] = new Vector2(0, 1);
        uvs[1] = new Vector2(1, 1);
        uvs[2] = new Vector2(0, 0);
        uvs[3] = new Vector2(1, 0);
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

    /// <summary>
    /// Wypełnia tablice <paramref name="positions"/> z pozycjami punktów ściany skierowanej w strone <paramref name="side"/>
    /// </summary>
    protected internal static void SetDefault(Span<Vector3> positions, BlockSide side)
    {
        switch (side)
        {
            case BlockSide.North:
                positions[0] = new Vector3(1, 1, 1);
                positions[1] = new Vector3(0, 1, 1);
                positions[2] = new Vector3(1, 0, 1);
                positions[3] = new Vector3(0, 0, 1);
                break;
            case BlockSide.South:
                positions[0] = new Vector3(0, 1, 0);
                positions[1] = new Vector3(1, 1, 0);
                positions[2] = new Vector3(0, 0, 0);
                positions[3] = new Vector3(1, 0, 0);
                break;
            case BlockSide.East:
                positions[0] = new Vector3(1, 1, 0);
                positions[1] = new Vector3(1, 1, 1);
                positions[2] = new Vector3(1, 0, 0);
                positions[3] = new Vector3(1, 0, 1);
                break;
            case BlockSide.West:
                positions[0] = new Vector3(0, 1, 1);
                positions[1] = new Vector3(0, 1, 0);
                positions[2] = new Vector3(0, 0, 1);
                positions[3] = new Vector3(0, 0, 0);
                break;
            case BlockSide.Top:
                positions[0] = new Vector3(0, 1, 1);
                positions[1] = new Vector3(1, 1, 1);
                positions[2] = new Vector3(0, 1, 0);
                positions[3] = new Vector3(1, 1, 0);
                break;
            case BlockSide.Bottom:
                positions[0] = new Vector3(0, 0, 0);
                positions[1] = new Vector3(1, 0, 0);
                positions[2] = new Vector3(0, 0, 1);
                positions[3] = new Vector3(1, 0, 1);
                break;
        }
    }

    /// <summary>
    /// Przeprowadza operacje przesuwania dla <see cref="Vector3"/>ów w tablicy o podany o <paramref name="offset"/>
    /// </summary>
    protected internal static void MoveVectors(Span<Vector3> positions, Vector3 offset)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += offset;
    }

    /// <summary><inheritdoc cref="MoveVectors(Span{Vector3} positions, Vector3 offset)"/></summary>
    protected internal static void MoveVectors(Span<Vector3> positions, float offset)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += new Vector3(offset);
    }

    /// <summary>
    /// Przeprowadza operacje przesuwania dla <see cref="Vector2"/>ów w tablicy o podany <paramref name="offset"/>
    /// </summary>
    protected internal static void MoveVectors(Span<Vector2> positions, Vector2 offset)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += offset;
    }

    /// <summary>
    /// Przeprowadza operacje mnorzenia dla <see cref="Vector3"/>ów w tablicy o podaną <paramref name="scale"/>
    /// </summary>
    protected internal static void ScaleVectors(Span<Vector3> positions, Vector3 scale)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] *= scale;
    }

    /// <summary><inheritdoc cref="ScaleVectors(Span{Vector3}, Vector3)"/></summary>
    protected internal static void ScaleVectors(Span<Vector3> positions, float scale)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] *= scale;
    }

    /// <summary>
    /// Przeprowadza operacje mnorzenia dla <see cref="Vector3"/>ów w tablicy o podaną <paramref name="scale"/>
    /// </summary>
    protected internal static void ScaleVectors(Span<Vector2> positions, Vector2 scale)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] *= scale;
    }

    /// <summary><inheritdoc cref="ScaleVectors(Span{Vector2}, Vector2)"/></summary>
    protected internal static void ScaleVectors(Span<Vector2> positions, float scale)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] *= scale;
    }

    /// <summary>
    /// Najpierw skaluje o <paramref name="scale"/>, potem przesuwa o <paramref name="offset"/> wszystkie elementy tablicy <paramref name="positions"/>
    /// </summary>
    protected internal static void ScaleAndMoveVector(Span<Vector3> positions, Vector3 scale, Vector3 offset)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] = (positions[i] * scale) + offset;
    }

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector3> positions, float scale, Vector3 offset) =>
        ScaleAndMoveVector(positions, new Vector3(scale), offset);

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector3> positions, Vector3 scale, float offset) =>
        ScaleAndMoveVector(positions, scale, new Vector3(offset));

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector3> positions, float scale, float offset) =>
        ScaleAndMoveVector(positions, new Vector3(scale), new Vector3(offset));

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector2> positions, Vector2 scale, Vector2 offset)
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] = (positions[i] * scale) + offset;
    }

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector2> positions, float scale, Vector2 offset) =>
        ScaleAndMoveVector(positions, new Vector2(scale), offset);

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector2> positions, Vector2 scale, float offset) =>
        ScaleAndMoveVector(positions, scale, new Vector2(offset));

    /// <summary><inheritdoc cref="ScaleAndMoveVector(Span{Vector3}, Vector3, Vector3)"/></summary>
    protected internal static void ScaleAndMoveVector(Span<Vector2> positions, float scale, float offset) =>
        ScaleAndMoveVector(positions, new Vector2(scale), new Vector2(offset));
}
