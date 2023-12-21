using Create.Conteiner;

namespace Create.Elements;

partial class CreativeTabs
{
    public static readonly CreativeTab BLOCKS = new()
    {
        RegisterElements = () =>
            (new Block[]
            {
                Blocks.STONE,
                Blocks.DIRT,
                Blocks.GRASS_BLOCK,
                Blocks.BEDROCK,
                Blocks.OAK_PLANKS
            }).Select(b => new ItemStack(b)),
        TabName = "create.creative-tabs.blocks.name"
    };
}
