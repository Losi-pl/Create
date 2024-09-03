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
    }
}
