using Create.Elements.Recipes;
using Create.Conteiner;

namespace Create.Elements;

partial interface IRecipe
{
    internal static void Load(Mod mod)
    {
        mod.RegisterRecipe("oak-planks", new Fluid(new(4, Blocks.OAK_PLANKS), new ItemStack(Blocks.OAK_LOG)));
        mod.RegisterRecipe("crafting-table", new Standard(new(Blocks.CRAFTING_TABLE),
            "pp",
            "pp",
            ('p', new(Blocks.OAK_PLANKS))
          ));
        mod.RegisterRecipe("stone-slab", new Standard(new(6, Blocks.STONE_SLAB),
            "sss", ('s', new(Blocks.STONE)) ));
        mod.RegisterRecipe("oak-planks-slab", new Standard(new(6, Blocks.OAK_PLANKS_SLAB),
            "ooo", ('o', new(Blocks.OAK_PLANKS)) ));
    }
}
