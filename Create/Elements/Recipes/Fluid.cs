using Create.Conteiner;
using System.Collections.Generic;
using System.Linq;

namespace Create.Elements.Recipes;

public class Fluid : IRecipe
{
    ItemStack[] items;
    ItemStack rezult;

    public Fluid(ItemStack rezult, params ItemStack[] ingridients)
    {
        ArgumentNullException.ThrowIfNull(rezult.Item, nameof(rezult));
        ArgumentNullException.ThrowIfNull(ingridients, nameof(ingridients));
        if (ingridients.Length == 0)
            throw new ArgumentException("There are no specified ingridients");

        items = CombineItems(ingridients);
        this.rezult = rezult;
    }

    static object? IRecipe.ProcessRecipeIngredients(RecipeIngredients ingredients)
    {
        ProcesedContents content = new();
        List<ItemStack> raw_list = new();
        for (int x = 0; x < ingredients.TableSize.Width; x++)
            for (int y = 0; y < ingredients.TableSize.Height; y++)
                if (ingredients[x, y].HasValue)
                    raw_list.Add(ingredients[x, y]!.Value);
        content.items = CountItems(raw_list);
        return content;
    }

    public (ItemStack rezult, uint uses)? CheckRecipe(RecipeIngredients ingredients)
    {
        var data = ingredients.IngredientsData as ProcesedContents;
        if (data == null)
            return null;
        if(data.items.Length != items.Length)
            return null;
        for (int i = 0; i < items.Length; i++)
            if (data.items[i] != items[i] || data.items[i].Count != items[i].Count)
                return null;

        uint? uzes = null;
        for(int x = 0; x < ingredients.TableSize.Width; x++)
            for(int y = 0; y < ingredients.TableSize.Height; y++)
            {
                if (!ingredients[x, y].HasValue)
                    continue;
                var u = ingredients[x, y]!.Value.Item.CraftingUses(new()
                { Player = ingredients.Player, Stack = ingredients[x, y]!.Value, World = ingredients.World });
                if (uzes is null)
                    uzes = u;
                else
                    uzes = Math.Min(uzes.Value, u);
            }
        return (rezult, uzes ?? 1);

    }

    public void UseRecipe(RecipeIngredients ingredients)
    {
        for (int y = 0; y < ingredients.TableSize.Height; y++)
            for (int x = 0; x < ingredients.TableSize.Width; x++)
            {
                var i = ingredients[x, y];
                if (!i.HasValue)
                    continue;
                i = i.Value.Item.UsedInCrafting(new() { Player = ingredients.Player, Stack = i.Value, World = ingredients.World });
                ingredients[x, y] = i;
            }
    }

    static ItemStack[] CombineItems(ItemStack[] items)
    {
        List<ItemStack> rezult = new();
        foreach (var item in items)
        {
            bool sucess_neg = true;
            for (int i = 0; i < rezult.Count && sucess_neg; i++)
                if (item.Item == rezult[i].Item &&
                    item.Type == rezult[i].Type &&
                    item.Meta == rezult[i].Meta)
                {
                    var d = rezult[i];
                    d = new(d.Count + item.Count, d.Item, d.Type, d.Meta);
                    rezult[i] = d;
                    sucess_neg = false;
                }
            if (sucess_neg)
                rezult.Add(item);
        }
        rezult.Sort(ItemsOrder);
        return rezult.ToArray();
    }
    static ItemStack[] CountItems(IEnumerable<ItemStack> enumerable)
    {
        List<ItemStack> list = new();
        foreach (var item in enumerable)
        {
            bool sucess_neg = true;
            for (int i = 0; i < list.Count && sucess_neg; i++)
                if (item.Item == list[i].Item &&
                    item.Type == list[i].Type &&
                    item.Meta == list[i].Meta)
                {
                    var d = list[i];
                    d = new(d.Count + 1, d.Item, d.Type, d.Meta);
                    list[i] = d;
                    sucess_neg = false;
                }
            if (sucess_neg)
                list.Add(new(1, item.Item, item.Type, item.Meta));
        }
        list.Sort(ItemsOrder);
        return list.ToArray();
    }

    static int ItemsOrder(ItemStack x, ItemStack y)
    {
        if (x.Count > y.Count)
            return -1;
        if (x.Count < y.Count)
            return 1;

        var it_ord = x.Item.CodeName.CompareTo(y.Item.CodeName);
        if (it_ord != 0)
            return it_ord;

        if (x.Type > y.Type)
            return -1;
        if (x.Type < y.Type)
            return 1;

        it_ord = x.Meta.CompareTo(y.Meta);
        if (it_ord != 0)
            return it_ord;

        return 0;
    }
}
file class ProcesedContents
{
    public ItemStack[] items = Array.Empty<ItemStack>();
}