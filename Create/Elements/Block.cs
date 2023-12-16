using Create.Conteiner;
using Create.Linq;
using Create.Net;
using Create.OpenGL.GUI;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Render.ModelCreators.BlockModels;
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

    static IBlockModel noModel = Assets.LoadBlockModel("create:no-model");
    IBlockModel? model;

    /// <summary>
    /// Tekstura bloku
    /// </summary>
    [Obsolete("Ta metoda jest już nie aktualna i zwraca tylko pustą teksture", true)]
    public BlockTextureHandle BlockTexture => Assets.BlockAtlas.NoneHandle;

    /// <summary>
    /// Ustawia teksture bloku
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [Obsolete("Ta metoda jest już nie aktualna i nie będzie aktualizować tekstury", true)]
    protected Block SetTexture(BlockTextureHandle texture) => this;

    /// <summary>
    /// Ustawia model używany do renderowanie bloku na scenie
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected Block SetModel(IBlockModel model)
    {
        this.model = model;
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
    public virtual void GenerateModel(StandardBlockSet args, ModelConstructor constructor) => (model ?? noModel).GenerateModel(args, constructor);

    /// <summary>
    /// Zwraca teksture danej ściany
    /// </summary>
    /// <param name="sideSet">Parametry bloku</param>
    /// <param name="side">Śtrona bloku</param>
    /// <returns></returns>
    public virtual IBlockSideModel? GetSideTexture(StandardBlockSet sideSet, BlockSide side) => (model ?? noModel).GetBlockSide(sideSet, side);
    
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

    public virtual string GetItemName(ItemName args) => CodeName;

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
    public struct ItemName
    {
        public Player Player;
        public ItemStack Item;
        PlacedBlock? block;
        public PlacedBlock Block { get { block = block ?? Item.AsPlacedBlock(); return block.Value; } }
    }
}
