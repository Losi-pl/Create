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
    /// Gdy gracz naciśnie na blok
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual bool OnClick(OnClickArgs args)
    {
        if(args.Button == ClickEventButton.Left)
        {
            DestroyBlock des_args = new();
            des_args.BlockPozition = args.BlockPozition;
            des_args.TargetSide = args.TargetSide;
            des_args.Block = args.Block;
            des_args.Player = args.Player;
            des_args.World = args.World;
            des_args.HitBoxIndex = args.HitBoxIndex;
            des_args.InHand = args.InHand;
            des_args.InWorldPoint = args.InWorldPoint;
            return args.Block.Block.OnDestroyBlock(des_args);
        }
        return false;
    }

    public virtual bool OnDestroyBlock(DestroyBlock args)
    {
        args.World.SetBlock(args.BlockPozition, new(Blocks.AIR));
        return true;
    }

    public virtual bool OnPlaceBlock(PlaceBlock args)
    {
        if (args.World.GetBlock(args.TargetBlockPozition).Block == Blocks.AIR)
            args.World.SetBlock(args.TargetBlockPozition, args.BlockStack.AsPlacedBlock());
        else
            return false;
        return true;
    }

    public virtual string GetItemName(ItemName args) => Assets.Language.GetFromKey($"{Mod.Name}.blocks.{CodeElementName}.name");

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
        public (int x, int y, int z) BlockPozition { get; set; }
        public BlockSide TargetSide { get; set; }
        public ClickEventButton Button { get; set; }
        public PlacedBlock Block { get; set; }
        public Player Player { get; set; }
        public World World { get; set; }
        public int HitBoxIndex { get; set; }
        public (int Slot, ItemStack? Stack) InHand { get; set; }
        public Vector3 InWorldPoint { get; set; }
        public (int x, int y, int z) BlockOnSide =>
            (BlockPozition.ToVector() + TargetSide.ToVectorI()).ToTumple();

        public OnClickArgs(Player player, ClickEventButton button,
            (int Slot, ItemStack? Stack) inHand,
            Bazic.Entitys.Mob.ImLookingAtRezult lookingAt)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            if (player?.Entity?.Dimention?.World is null)
                throw new ArgumentNullException(nameof(player), "Player needs to be bound to a entity in a world.");

            Player = player;
            Button = button;
            InHand = inHand;
            World = player.Entity.Dimention.World;
            TargetSide = lookingAt.BlockSide;
            BlockPozition = lookingAt.BlockPozition;
            HitBoxIndex = lookingAt.HitBoxIndex;
            InWorldPoint = lookingAt.HitPoint;
            Block = World.GetBlock(BlockPozition);
        }
    }
    /// <summary>
    /// Standardowe parametry bloku
    /// </summary>
    public struct StandardBlockSet
    {
        public int HitBoxIndex { get; set; }
        public (int x, int y, int z) pozition { get; set; }
        public PlacedBlock block { get; set; }
        public World world { get; set; }
    }
    /// <summary>
    /// Parametry granic bloku
    /// </summary>
    public struct BlockCollider
    {
        public (float x, float y, float z) pozition { get; set; }
        public (float x, float y, float z) size { get; set; }
    }
    public struct ItemName
    {
        public Player Player { get; set; }
        public ItemStack Item { get; set; }
        PlacedBlock? block;
        public PlacedBlock Block { get { block = block ?? Item.AsPlacedBlock(); return block.Value; } }
    }

    public struct PlaceBlock
    {
        public (int x, int y, int z) TargetedBlockPozition { get; set; }
        public BlockSide TargetSide { get; set; }
        public ItemStack BlockStack { get; set; }
        public Player Player { get; set; }
        public World World { get; set; }
        public int HitBoxIndex { get; set; }
        public Vector3 InWorldPoint { get; set; }
        public (int x, int y, int z) TargetBlockPozition => (TargetSide switch
        {
            BlockSide.Top => new(0, 1, 0),
            BlockSide.Bottom => new(0, -1, 0),
            BlockSide.North => new(0, 0, 1),
            BlockSide.South => new(0, 0, -1),
            BlockSide.West => new(-1, 0, 0),
            BlockSide.East => new(1, 0, 0),
            _ => new Vector3i(0)
        } + TargetedBlockPozition.ToVector()).ToTumple();
    }

    public struct DestroyBlock
    {
        public (int x, int y, int z) BlockPozition { get; set; }
        public BlockSide TargetSide { get; set; }
        public PlacedBlock Block { get; set; }
        public Player Player { get; set; }
        public World World { get; set; }
        public int HitBoxIndex { get; set; }
        public (int Slot, ItemStack? Stack) InHand { get; set; }
        public Vector3 InWorldPoint { get; set; }
    }
}
