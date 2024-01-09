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
        object? ingredientsData;
        readonly bool editable;

        internal RecipeIngredients(CraftingTableInterface cti, bool editable) => (craftingTable, this.editable) = (cti, editable);
        CraftingTableInterface craftingTable;
        internal void SetIngridients(object? obj) => ingredientsData = obj;
        public object IngredientsData => ingredientsData!;
        public Player Player => craftingTable.Player;
        public World World => craftingTable.Player.Entity?.Dimention?.World ?? throw new("Player outside world?");
        public (uint Width, uint Height) TableSize => (3, 3);
        /// <summary>
        /// <see langword="set"/> działa tylko w <see cref="IRecipeBaze.UseRecipe"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public ItemStack? this[int x, int y]
        {
            get => (x >= 3 || x < 0) || (y >= 3 || y < 0) ?
                    throw new IndexOutOfRangeException() :
                    craftingTable.slots.GetItem((y * 3) + x);
            set => craftingTable.slots.SetItem((
                ((y >= 3 || y < 0) ? throw new IndexOutOfRangeException() : y) * 3) +
                ((x >= 3 || x < 0) ? throw new IndexOutOfRangeException() : x), 
                editable ? value : throw new("Recepi in spectator mode"));
        }
    }
}