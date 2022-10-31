using Create.Conteiner;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Render.ModelCreators.Model;
using Create.Render.ModelCreators.Side;
using Create.Space;
using OpenTK.Graphics.ES11;
using OpenTK.Mathematics;

namespace Create.Elements;

public abstract class Block : Baze
{
    public sealed override Type ElementBazicType => typeof(Block);

    #region parametry
    BlockTextureHandle? default_textre;
    #endregion

    #region
#pragma warning disable CS8618
    static BlockTextureHandle null_texture = Assets.BlockAtlas.NoneHandle;
#pragma warning restore CS8618
    #endregion

    public BlockTextureHandle BlockTexture => default_textre ?? null_texture;

    protected Block SetTexture(BlockTextureHandle texture)
    {
        if (IsRegistered)
            throw new Exception("Element was registered and default method was blocked");
        default_textre = texture;
        return this;
    }

    public virtual bool IsSideVisible(StandardBlockSet sideSet, BlockSide side) => false;
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
    public virtual BlockTextureHandle GetSideTexture(StandardBlockSet sideSet, BlockSide side) => default_textre ?? null_texture;
    public virtual BlockCollider[] GetPhisicCollision(StandardBlockSet set)
    {
        return new BlockCollider[]
        {
            new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) }
        };
    }
    public virtual BlockCollider[] GetInteractionCollision(StandardBlockSet set)
    {
        return new BlockCollider[]
        {
            new() { pozition = (.5f, .5f, .5f), size = (1, 1, 1) }
        };
    }

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
    public struct StandardBlockSet
    {
        public (int x, int y, int z) pozition;
        //public ModelConstructor constructor;
        public PlacedBlock block;
        public World world;
    }

    public struct BlockCollider
    {
        public (float x, float y, float z) pozition;
        public (float x, float y, float z) size;
    }
}
