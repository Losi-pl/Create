using Create.Conteiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create.Elements.Recipes
{
    public class ItemAlteration : IRecipe
    {
        Func<ItemStack, bool> m_is_alterable;
        Func<ItemStack, (ItemStack rezult, uint uses)> m_alteration;

        public ItemAlteration(Func<ItemStack, bool> isAlterable, Func<ItemStack, (ItemStack rezult, uint uses)> alteration) =>
            (m_is_alterable, m_alteration) = (isAlterable, alteration);

        public static object? ProcessRecipeIngredients(RecipeIngredients ingredients) => null;

        public (ItemStack rezult, uint uses)? CheckRecipe(RecipeIngredients ingredients)
        {
            ItemStack? itemStack = null;
            for (int y = 0; y < ingredients.TableSize.Height; y++)
                for (int x = 0; x < ingredients.TableSize.Width; x++)
                {
                    var @is = ingredients[x, y];
                    if (!@is.HasValue)
                        continue;
                    if (itemStack.HasValue)
                        return null;
                    itemStack = @is;
                }

            if (!itemStack.HasValue)
                return null;

            if (!m_is_alterable(itemStack.Value))
                return null;

            return m_alteration(itemStack.Value);
        }

        public void UseRecipe(RecipeIngredients ingredients)
        {
            for (int y = 0; y < ingredients.TableSize.Height; y++)
                for (int x = 0; x < ingredients.TableSize.Width; x++)
                {
                    var @is = ingredients[x, y];
                    if (!@is.HasValue)
                        continue;
                    ingredients[x, y] = @is.Value.Count > 1 ? new(@is.Value.Count - 1, @is.Value.Item, @is.Value.Type, @is.Value.Meta) : null;
                }
        }
    }
}
