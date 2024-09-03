using Create.Conteiner;

namespace Create.Elements.Recipes;

public sealed class Standard : IRecipe
{
    ItemStack?[,] ingridients = new ItemStack?[0, 0];
    (byte top, byte bottom, byte left, byte right) margin;
    List<ItemStack> itemsList = new List<ItemStack>();
    ItemStack rezult;

    public Standard(ItemStack rezult, string row1, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, string row6, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5, row6 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, string row6, string row7, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5, row6, row7 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, string row6, string row7, string row8, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5, row6, row7, row8 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, string row6, string row7, string row8, string row9, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5, row6, row7, row8, row9 }, items, rezult);
    public Standard(ItemStack rezult, string row1, string row2, string row3, string row4, string row5, string row6, string row7, string row8, string row9, string row10, params (char key, ItemStack item)[] items) =>
        ConvertMultiple(new string[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10 }, items, rezult);
    public Standard(ItemStack rezult, string[] rows, (char key, ItemStack item)[] items) =>
        ConvertMultiple(rows, items, rezult);
    static Dictionary<char, ItemStack> ConvertToDictionary((char key, ItemStack item)[] items) =>
        new(items.Select(i => new KeyValuePair<char, ItemStack>(i.key, i.item)));
    static (byte left, byte right)? Margins(string row, Dictionary<char, ItemStack> itemsLiblary)
    {
        uint? left = null, right = null;
        for (int i = 0; i < row.Length && !left.HasValue; i++)
            if (itemsLiblary.ContainsKey(row[i]))
                left = (uint)i;
        if (!left.HasValue) return null;
        for (int i = 0; i < row.Length && !right.HasValue; i++)
            if (itemsLiblary.ContainsKey(row[row.Length - i - 1]))
                right = (uint)i;
        return ((byte)left.Value, (byte)right!.Value);
    }
    void ConvertMultiple(string[] rows, (char key, ItemStack item)[] items, ItemStack rezult)
    {
        this.rezult = rezult;
        for (int i = 0; i < rows.Length; i++)
            if (string.IsNullOrEmpty(rows[i]))
                throw new ArgumentException("Rows aren't equal size or defined");
        for ((int i, int? l) = (0, null); i < rows.Length; i++)
            if (l.HasValue)
            {
                if (rows[i].Length != l)
                    throw new ArgumentException("Rows aren't equal size or defined");
            }
            else
                l = rows[i].Length;

        var items_ = ConvertToDictionary(items);
        Span<(byte left, byte right)?> margins = stackalloc (byte left, byte right)?[rows.Length];
        for (int i = 0; i < rows.Length; i++)
            margins[i] = Margins(rows[i], items_);
        {
            bool any = false;
            for (int i = 0; i < margins.Length; i++)
                if (margins[i].HasValue)
                    any = true;
            if (!any)
                throw new("The recipe is empty");
        }
        (byte left, byte right, byte top, byte bottom) margin = ((byte)rows[0].Length, (byte)rows[0].Length, 0, 0);
        for (int i = 0; i < margins.Length; i++)
            if (margins[i].HasValue)
            { margin.top = (byte)i; break; }
        for (int i = 0; i < margins.Length; i++)
            if (margins[margins.Length - i - 1].HasValue)
            { margin.bottom = (byte)i; break; }
        for (int i = 0; i < margins.Length; i++)
            if (margins[i].HasValue)
                (margin.left, margin.right) =
                    (margin.left < margins[i]!.Value.left ? margin.left : margins[i]!.Value.left,
                     margin.right < margins[i]!.Value.right ? margin.right : margins[i]!.Value.right);

        ingridients = new ItemStack?[rows[0].Length - margin.left - margin.right, margins.Length - margin.top - margin.bottom];
        for (int y = 0; y < ingridients.GetLength(1); y++)
            for (int x = 0; x < ingridients.GetLength(0); x++)
                ingridients[x, y] = items_.TryGetValue(rows[y][x], out var i) ? i : null;

        List<(ItemStack item, int count)> list = new();
        foreach (var item in ingridients)
        {
            if (!item.HasValue)
                continue;
            bool sucess_neg = true;
            for (int i = 0; i < list.Count && sucess_neg; i++)
                if (item.Value.Item == list[i].item.Item &&
                   item.Value.Type == list[i].item.Type &&
                   item.Value.Meta == list[i].item.Meta)
                {
                    var d = list[i];
                    d.count++;
                    list[i] = d;
                    sucess_neg = false;
                }
            if (sucess_neg)
                list.Add((item.Value, 1));
        }
        list.Sort((x, y) =>
        {
            if (x.count > y.count)
                return -1;
            if (x.count < y.count)
                return 1;

            var it_ord = x.item.Item.CodeName.CompareTo(y.item.Item.CodeName);
            if (it_ord != 0)
                return it_ord;

            if (x.item.Type > y.item.Type)
                return -1;
            if (x.item.Type < y.item.Type)
                return 1;

            it_ord = x.item.Item.CodeName.CompareTo(y.item.Item.CodeName);
            if (it_ord != 0)
                return it_ord;

            return 0;
        });
        for (int i = 0; i < list.Count; i++)
            itemsList.Add(new((uint)list[i].count, list[i].item.Item, list[i].item.Type, list[i].item.Meta));
    }

    public static object? ProcessRecipeIngredients(RecipeIngredients ingredients)
    {
        var ing = new IngridientsData();
        Span<(byte left, byte right)?> marginesmesurement = stackalloc (byte left, byte right)?[(int)ingredients.TableSize.Height];
        for (int y = 0; y < ingredients.TableSize.Height; y++)
            marginesmesurement[y] = mesure_margin(y);
        (int? Left, int? Right, int? Top, int? Bottom) margines = (null, null, null, null);
        for (int y = 0; y < ingredients.TableSize.Height && !margines.Top.HasValue; y++)
            if (marginesmesurement[y].HasValue)
                margines.Top = y;
        if (!margines.Top.HasValue)
            return null;
        for (int y = 0; y < ingredients.TableSize.Height && !margines.Bottom.HasValue; y++)
            if (marginesmesurement[((int)ingredients.TableSize.Height) - y - 1].HasValue)
                margines.Bottom = y;
        for (int y = 0; y < ingredients.TableSize.Height; y++)
            if (marginesmesurement[y].HasValue)
                if (margines.Left.HasValue)
                    margines.Left = margines.Left < marginesmesurement[y]!.Value.left ? 
                                    margines.Left : marginesmesurement[y]!.Value.left;
                else
                    margines.Left = marginesmesurement[y]!.Value.left;
        for (int y = 0; y < ingredients.TableSize.Height; y++)
            if (marginesmesurement[y].HasValue)
                if (margines.Right.HasValue)
                    margines.Right = margines.Right < marginesmesurement[y]!.Value.right ? 
                                     margines.Right : marginesmesurement[y]!.Value.right;
                else
                    margines.Right = marginesmesurement[y]!.Value.right;
        ing.Padding = ((byte)margines.Left!.Value, (byte)margines.Right!.Value, 
                       (byte)margines.Top.Value, (byte)margines.Bottom!.Value);
        ing.Items = new ItemStack?[ingredients.TableSize.Width - ing.Padding.Left - ing.Padding.Right,
                                   ingredients.TableSize.Height - ing.Padding.Top - ing.Padding.Bottom];
        for (int x = 0; x < ing.Items.GetLength(0); x++)
            for (int y = 0; y < ing.Items.GetLength(1); y++)
                ing.Items[x, y] = ingredients[x + ing.Padding.Left, y + ing.Padding.Top];

        List<(ItemStack item, int count)> list = new();

        foreach(var item in ing.Items)
        {
            if (!item.HasValue)
                continue;
            bool sucess_neg = true;
            for(int i = 0; i < list.Count && sucess_neg; i++)
                if(item.Value.Item == list[i].item.Item &&
                   item.Value.Type == list[i].item.Type &&
                   item.Value.Meta == list[i].item.Meta)
                {
                    var d = list[i];
                    d.count++;
                    list[i] = d;
                    sucess_neg = false;
                }
            if(sucess_neg)
                list.Add((item.Value, 1));
        }
        list.Sort((x, y) =>
        {
            if (x.count > y.count)
                return -1;
            if (x.count < y.count)
                return 1;

            var it_ord = x.item.Item.CodeName.CompareTo(y.item.Item.CodeName);
            if (it_ord != 0)
                return it_ord;

            if (x.item.Type > y.item.Type)
                return -1;
            if (x.item.Type < y.item.Type)
                return 1;

            it_ord = x.item.Meta.CompareTo(y.item.Meta);
            if (it_ord != 0)
                return it_ord;

            return 0;
        });
        for (int i = 0; i < list.Count; i++)
            ing.ItemsList.Add(new((uint)list[i].count, list[i].item.Item, list[i].item.Type, list[i].item.Meta));

        return ing;

        (byte left, byte right)? mesure_margin(int row)
        {
            int? left = null, right = null;
            for (int i = 0; i < ingredients.TableSize.Width && !left.HasValue; i++)
                if (ingredients[i, row].HasValue)
                    left = i;
            if (!left.HasValue) return null;
            for (int i = 0; i < ingredients.TableSize.Width && !right.HasValue; i++)
                if (ingredients[((int)ingredients.TableSize.Width) - i - 1, row].HasValue)
                    right = i;
            return ((byte)left.Value, (byte)right!.Value);
        }
    }

    public (ItemStack rezult, uint uses)? CheckRecipe(RecipeIngredients ingredients)
    {
        if (!(ingredients.IngredientsData is IngridientsData id))
            return null;
        if (id.ItemsList.Count != itemsList.Count)
            return null;
        for (int i = 0; i < id.ItemsList.Count; i++)
            if (id.ItemsList[i] != itemsList[i])
                return null;
        if (id.Padding.Top < margin.top   || id.Padding.Bottom < margin.bottom ||
            id.Padding.Left < margin.left || id.Padding.Right < margin.right)
            return null;
        if (ingridients.GetLength(0) != id.Items.GetLength(0) || ingridients.GetLength(1) != id.Items.GetLength(1))
            return null;
        for (int x = 0; x < ingridients.GetLength(0); x++)
            for (int y = 0; y < ingridients.GetLength(1); y++)
                if (ingridients[x, y] != id.Items[x, y])
                    return null;
        uint? uses = null;
        foreach(var item in id.Items)
        {
            if (!item.HasValue)
                continue;
            var u = item.Value.Item.CraftingUses(new() { Player = ingredients.Player, 
                Stack = item.Value, World = ingredients.World });
            if (uses.HasValue)
                uses = Math.Min(uses.Value, u);
            else
                uses = u;
        }

        return (rezult, uses!.Value);
    }

    public void UseRecipe(RecipeIngredients ingredients)
    {
        for(int y = 0; y < ingredients.TableSize.Height; y++)
            for(int x = 0; x < ingredients.TableSize.Width; x++)
            {
                var i = ingredients[x, y];
                if (!i.HasValue)
                    continue;
                i = i.Value.Item.UsedInCrafting(new() { Player = ingredients.Player, Stack = i.Value, World = ingredients.World });
                ingredients[x, y] = i;
            }
    }
}

file class IngridientsData
{
    #nullable disable
    public (byte Left, byte Right, byte Top, byte Bottom) Padding;
    public ItemStack?[,] Items;
    public List<ItemStack> ItemsList = new List<ItemStack>();
    #nullable restore
}