using Create.Conteiner;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Render.ModelCreators.Model;
using Create.Render.ModelCreators.Side;
using OpenTK.Mathematics;
using System.Drawing;

namespace Create.Elements.Bazic.Blocks;

public abstract class GrassBase : Block
{
    (BlockTextureHandle top, BlockTextureHandle side, Color color)? textures;

    protected void SetTextures(BlockTextureHandle bazic, BlockTextureHandle grassTop, BlockTextureHandle grassSide, Color color)
    {
        SetTexture(bazic);
        textures = (grassTop, grassSide, color);
    }
    public (BlockTextureHandle Base, BlockTextureHandle GrassSide, BlockTextureHandle GrassTop) BlockTextures
    {
        get
        {
            if (textures.HasValue)
                return (base.BlockTexture, textures.Value.side, textures.Value.top);
            return (Render.Textures.NoneHandle, Render.Textures.NoneHandle, Render.Textures.NoneHandle);
        }
    }
    public Color GrassColor => textures.HasValue ? textures.Value.color : Color.White;

    public virtual Color GetGrassColor(StandardBlockSet args) => GrassColor;

    public override void GenerateModel(StandardBlockSet @struct, ModelConstructor constructor)
    {
        SingleTextureModel? single = null;
        MultiTextureColorModel? multy = null;
        ColoredTextureModel? colored = null;

        Color4 color;
        {
            var c = ((GrassBase)@struct.block.Block).GetGrassColor(@struct);
            color = new(c.R, c.G, c.B, c.A);
        }

        if (test_side(BlockSide.Top))
            top_texture();
        if (test_side(BlockSide.Bottom))
            bottom_texture();
        if (test_side(BlockSide.East))
            side_texture(BlockSide.East);
        if (test_side(BlockSide.West))
            side_texture(BlockSide.West);
        if (test_side(BlockSide.North))
            side_texture(BlockSide.North);
        if (test_side(BlockSide.South))
            side_texture(BlockSide.South);

        //Methods
        void side_texture(BlockSide side)
        {
            multy = multy ?? constructor.GetModelMekanizm<MultiTextureColorModel>();
            MultiTextureColorSide side_model = new()
            {
                pozitions = stackalloc Vector3[4],
                uvs = stackalloc Vector2[4],
                trangles = stackalloc int[6],
                texture_bottom = BlockTexture.Handle,
                texture_top = textures.HasValue ? textures.Value.side.Handle : Textures.NoneHandle.Handle,
                top_color = color
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
            Vector3 bl_poz = @struct.pozition.ToVector();
            switch(side)
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
            }

            multy.AddSide(side_model);
        }
        void top_texture()
        {
            colored = colored ?? constructor.GetModelMekanizm<ColoredTextureModel>();
            ColoredTextureSide side_model = new()
            {
                pozitions = stackalloc Vector3[4],
                uvs = stackalloc Vector2[4],
                trangles = stackalloc int[6],
                texture_side = textures.HasValue ? textures.Value.top.Handle : Textures.NoneHandle.Handle,
                color = color
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
            Vector3 bl_poz = @struct.pozition.ToVector();
            side_model.pozitions = stackalloc Vector3[]
            {
                bl_poz + new Vector3(0, 1, 1),
                bl_poz + new Vector3(1, 1, 1),
                bl_poz + new Vector3(0, 1, 0),
                bl_poz + new Vector3(1, 1, 0)
            };

            colored.AddSide(side_model);
        }
        void bottom_texture()
        {
            single = single ?? constructor.GetModelMekanizm<SingleTextureModel>();
            SingleTextureSide side_model = new()
            {
                pozitions = stackalloc Vector3[4],
                uvs = stackalloc Vector2[4],
                trangles = stackalloc int[6],
                texture_side = base.BlockTexture.Handle
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
            Vector3 bl_poz = @struct.pozition.ToVector();
            side_model.pozitions = stackalloc Vector3[]
            {
                bl_poz + new Vector3(0, 0, 0),
                bl_poz + new Vector3(1, 0, 0),
                bl_poz + new Vector3(0, 0, 1),
                bl_poz + new Vector3(1, 0, 1)
            };

            single.AddSide(side_model);
        }
        bool test_side(BlockSide side)
        {
            StandardBlockSet block_set = new()
            {
                pozition = @struct.pozition,
                world = @struct.world
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
            PlacedBlock block = @struct.world.GetBlock(block_set.pozition);
            block_set.block = block;
            return block.Block.IsSideVisible(block_set, side);
        }
    }
}
