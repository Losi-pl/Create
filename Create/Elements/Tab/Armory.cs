using Create.Conteiner;

namespace Create.Elements;

partial class CreativeTabs
{
    public static readonly CreativeTab WEAPONS_AND_ARMOR = new()
    {
        RegisterElements = () =>
            (new[]
            {
                Items.IRON_HELMET,
                Items.IRON_CHESTPLATE,
                Items.IRON_LEGGINGS,
                Items.IRON_BOOTS,
            }).Select(b => new ItemStack(b)),
        CreateIcon = () => new ItemStack(Items.IRON_CHESTPLATE),
        TabName = "create.creative-tabs.weapons-and-armor.name"
    };
}
