using Create.Conteiner;
using Create.Elements.Interfaces;
using Create.Elements.Recipes;
using Create.Net;
using Create.Space;

namespace Create.Elements
{
    public partial interface IRecipe : IRecipeBaze
    {
        public static abstract object? ProcessRecipeIngredients(RecipeIngredients ingredients);
    }

    public interface IRecipeBaze
    {
        public (ItemStack rezult, uint uses)? CheckRecipe(RecipeIngredients ingredients);
        public void UseRecipe(RecipeIngredients ingredients);
    }
}
namespace Create.Elements.Recipes
{
    public class RecipeIngredients
    {
        internal RecipeIngredients(CraftingTableInterface cti) => craftingTable = cti;
        CraftingTableInterface craftingTable;
        object? ingredientsData;
        internal void SetIngridients(object? obj) => ingredientsData = obj;
        public object IngredientsData => ingredientsData!;
        public Player Player => craftingTable.Player;
        public World World => craftingTable.Player.Entity?.Dimention?.World ?? throw new("Player outside world?");
        public (uint Width, uint Height) TableSize => (3, 3);
        public ItemStack? this[int x, int y] => (x >= 3 || x < 0) || (y >= 3 || y < 0) ? 
            throw new IndexOutOfRangeException() :
            craftingTable.slots.GetItem((y * 3) + x);
    }
}