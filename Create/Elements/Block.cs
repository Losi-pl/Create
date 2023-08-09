using Create.Conteiner;
using Create.Linq;
using Create.Net;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Render.ModelCreators.Model;
using Create.Render.ModelCreators.Side;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements;

/// <summary>
/// Baza do budowy bloków
/// </summary>
public abstract class Block : Baze
{
    //Ustawienie bazowego typu elementu na Block
    public sealed override Type ElementBazicType => typeof(Block);

    #region parametry
    BlockTextureHandle? default_textre;
    #endregion

    #region
#pragma warning disable CS8618
    static BlockTextureHandle null_texture = Assets.BlockAtlas.NoneHandle;
#pragma warning restore CS8618
    #endregion

    /// <summary>
    /// Tekstura bloku
    /// </summary>
    public BlockTextureHandle BlockTexture => default_textre ?? null_texture;

    /// <summary>
    /// Ustawia teksture bloku
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected Block SetTexture(BlockTextureHandle texture)
    {
        if (IsRegistered)
            throw new Exception("Element was registered and default method was blocked");
        default_textre = texture;
        return this;
    }

    /// <summary>
    /// Czy ściana <paramref name="side"/> jest widoczna?
    /// </summary>
    /// <param name="sideSet">Parametry bloku</param>
    /// <param name="side">Śtrona bloku</param>
    /// <returns></returns>
    public virtual bool IsSideVisible(StandardBlockSet sideSet, BlockSide side) => false;
    
    /// <summary>
    /// Generowanie modelu bloku
    /// </summary>
    /// <param name="args">Parametry bloku</param>
    /// <param name="constructor">Konstruktor modelu terenu</param>
    public virtual void GenerateModel(StandardBlockSet args, ModelConstructor constructor)
    {
        SingleTextureModel? con = null;
        if (test_side(BlockSide.Top))
            add_side(BlockSide.Top);
        if (test_side(BlockSide.Bottom))
            add_side(BlockSide.Bottom);
        if (test_side(BlockSide.East))
            add_side(BlockSide.East);
        if (test_side(BlockSide.West))
            add_side(BlockSide.West);
        if (test_side(BlockSide.North))
            add_side(BlockSide.North);
        if (test_side(BlockSide.South))
            add_side(BlockSide.South);

        void add_side(BlockSide side)
        {
            con = con ?? constructor.GetModelMekanizm<SingleTextureModel>();
            SingleTextureSide side_model = new()
            {
                pozitions = stackalloc Vector3[4],
                uvs = stackalloc Vector2[4],
                trangles = stackalloc int[6],
                texture_side = args.block.Block.GetSideTexture(args, BlockSide.Top).Handle
            };
            side_model.uvs = stackalloc Vector2[]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0)
            };
            side_model.trangles = stackalloc int[]
            {
                0, 1, 2,
                3, 2, 1
            };
            Vector3 bl_poz = args.pozition.ToVector();
            switch (side)
            {
                case BlockSide.North:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(1, 0, 1),
                        bl_poz + new Vector3(0, 0, 1)
                    };
                    break;
                case BlockSide.South:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(1, 1, 0),
                        bl_poz + new Vector3(0, 0, 0),
                        bl_poz + new Vector3(1, 0, 0)
                    };
                    break;
                case BlockSide.East:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(1, 1, 0),
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(1, 0, 0),
                        bl_poz + new Vector3(1, 0, 1)
                    };
                    break;
                case BlockSide.West:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(0, 0, 1),
                        bl_poz + new Vector3(0, 0, 0)
                    };
                    break;
                case BlockSide.Top:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 1, 1),
                        bl_poz + new Vector3(1, 1, 1),
                        bl_poz + new Vector3(0, 1, 0),
                        bl_poz + new Vector3(1, 1, 0)
                    };
                    break;
                case BlockSide.Bottom:
                    side_model.pozitions = stackalloc Vector3[]
                    {
                        bl_poz + new Vector3(0, 0, 0),
                        bl_poz + new Vector3(1, 0, 0),
                        bl_poz + new Vector3(0, 0, 1),
                        bl_poz + new Vector3(1, 0, 1)
                    };
                    break;
            }
            con.AddSide(side_model);
        }
        bool test_side(BlockSide side)
        {
            StandardBlockSet block_set = new()
            {
                pozition = args.pozition,
                world = args.world
            };
            
            switch (side)
            {
                case BlockSide.Top:
                    block_set.pozition.y++;
                    break;
                case BlockSide.Bottom:
                    block_set.pozition.y--;
                    break;
                case BlockSide.West:
                    block_set.pozition.x--;
                    break;
                case BlockSide.East:
                    block_set.pozition.x++;
                    break;
                case BlockSide.North:
                    block_set.pozition.z++;
                    break;
                case BlockSide.South:
                    block_set.pozition.z--;
                    break;
            }
            PlacedBlock block = args.world.GetBlock(block_set.pozition);
            block_set.block = block;
            return block.Block.IsSideVisible(block_set, side);
        }
    }

    /// <summary>
    /// Zwraca teksture danej ściany
    /// </summary>
    /// <param name="sideSet">Parametry bloku</param>
    /// <param name="side">Śtrona bloku</param>
    /// <returns></returns>
    public virtual BlockTextureHandle GetSideTexture(StandardBlockSet sideSet, BlockSide side) => default_textre ?? null_texture;
    
    /// <summary>
    /// Fizyczne bariery bloku
    /// </summary>
    /// <param name="set">Parametry bloku</param>
    /// <returns></returns>
    public virtual IEnumerable<BlockCollider> GetPhisicCollision(StandardBlockSet set)
    {
        yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
    }

    /// <summary>
    /// Interakcyjne bariery bloku
    /// </summary>
    /// <param name="set">Parametry bloku</param>
    /// <returns></returns>
    public virtual IEnumerable<BlockCollider> GetInteractionCollision(StandardBlockSet set)
    {
        yield return new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) };
    }

    /// <summary>
    /// Generuje model wyświetlający granice interakcji z blokiem
    /// </summary>
    /// <param name="set">Parametry bloku</param>
    /// <returns></returns>
    public virtual IEnumerable<((float x, float y, float z) start, (float x, float y, float z) end)> GetInteractionModel(StandardBlockSet set)
    {
        yield return ((0, 0, 0), (1, 0, 0));
        yield return ((0, 0, 1), (1, 0, 1));
        yield return ((1, 0, 1), (1, 0, 0));
        yield return ((0, 0, 1), (0, 0, 0));

        yield return ((0, 1, 0), (1, 1, 0));
        yield return ((0, 1, 1), (1, 1, 1));
        yield return ((1, 1, 1), (1, 1, 0));
        yield return ((0, 1, 1), (0, 1, 0));

        yield return ((0, 0, 0), (0, 1, 0));
        yield return ((0, 0, 1), (0, 1, 1));
        yield return ((1, 0, 0), (1, 1, 0));
        yield return ((1, 0, 1), (1, 1, 1));
    }

    /// <summary>
    /// Gdy gracz naciśnie prawym przycieskiem na
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual bool OnClick(OnClickArgs args)
    {
        if(args.Button == ClickEventButton.Left)
        {
            args.World.SetBlock(args.BlockPozition, new(Blocks.AIR));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Używany do definiowania stron bloku
    /// </summary>
    [Flags]
    public enum BlockSide
    {
        Top = 1,
        Bottom = 2,
        North = 4,
        South = 8,
        East = 16,
        West = 32
    }
    /// <summary>
    /// Parametry interakcji z blokami
    /// </summary>
    public struct OnClickArgs
    {
        public (int x, int y, int z) BlockPozition;
        public BlockSide TargetSide;
        public ClickEventButton Button;
        public PlacedBlock Block;
        public Player Player;
        public World World;
        public int HitBoxIndex;
        public (int Slot, ItemStack? Stack) InHand;
        public (int x, int y, int z) BlockOnSide => (TargetSide switch
        {
            BlockSide.Top => new(0, 1, 0),
            BlockSide.Bottom => new(0, -1, 0),
            BlockSide.North => new(0, 0, 1),
            BlockSide.South => new(0, 0, -1),
            BlockSide.West => new(-1, 0, 0),
            BlockSide.East => new(1, 0, 0),
            _ => new Vector3i(0)
        } + BlockPozition.ToVector()).ToTumple();
    }
    /// <summary>
    /// Standardowe parametry bloku
    /// </summary>
    public struct StandardBlockSet
    {
        public (int x, int y, int z) pozition;
        public PlacedBlock block;
        public World world;
    }
    /// <summary>
    /// Parametry granic bloku
    /// </summary>
    public struct BlockCollider
    {
        public (float x, float y, float z) pozition;
        public (float x, float y, float z) size;
    }
}
