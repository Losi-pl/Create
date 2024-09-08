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
                Blocks.CRAFTING_TABLE,
                Blocks.STONE_SLAB,
            }).Select(b => new ItemStack(b)),
        CreateIcon = () => new ItemStack(Blocks.STONE),
        TabName = "create.creative-tabs.blocks.name"
    };
}
