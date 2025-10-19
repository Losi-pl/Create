using Create.Conteiner;

namespace Create.Elements;

partial class CreativeTabs
{
    public static readonly CreativeTab BLOCKS = new()
    {
        RegisterElements = () =>
            (new[]
            {
                Blocks.STONE,
                Blocks.DIRT,
                Blocks.GRASS_BLOCK,
                Blocks.BEDROCK,
                Blocks.OAK_LOG,
                Blocks.OAK_PLANKS,
                Blocks.OAK_PLANKS_STAIRS,
                Blocks.OAK_PLANKS_SLAB,
                Blocks.CRAFTING_TABLE,
                Blocks.STONE_STAIRS,
                Blocks.STONE_SLAB,

                #if DEBUG
                Blocks.DEBUG_BLOCK,
                Blocks.DEBUG_STAIRS,
                Blocks.DEBUG_SLAB,
                #endif
            }).Select(b => new ItemStack(b)),
        CreateIcon = () => new ItemStack(Blocks.STONE),
        TabName = "create.creative-tabs.blocks.name"
    };
}
