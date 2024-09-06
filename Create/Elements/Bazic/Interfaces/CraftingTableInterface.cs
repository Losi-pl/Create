using Create.OpenGL.GUI;
using Create.Elements.Gui;
using Create.Conteiner.Items;
using Create.Conteiner;
using Create.Net;
using OpenTK.Graphics.OpenGL;
using Create.Input;
using Create.Linq;
using Create.Elements.Recipes;

namespace Create.Elements.Interfaces;

internal class CraftingTableInterface : UserInterface, IUserInterface<CraftingTableInterface>
{
    #nullable disable
    SpacePoint root;
    InventorySlots inventory;
    internal CraftingSlots slots;
    (IRecipeBaze recipe, ItemStack rezult, uint uses, object? details)? usedRecipe;
    #nullable restore

    static (CraftingTableInterface status, SpacePoint point) IUserInterface<CraftingTableInterface>.LoadInterface(InterfaceCreatorArgs args)
    {
        CraftingTableInterface cti = new();
        cti.root = Assets.GetInterface("create:crafting-table");
        cti.slots = new();
        cti.inventory = new(Client.TransferedItemSlot,
            ItemSlot.GetAllSlots(cti.root.Childs.Find("Slots bar", true) ?? new())
                .Select(s => (s, s.ID ?? 0)),
            new[] { ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 1", true) ?? new()),
                    ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 2", true) ?? new()),
                    ItemSlot.GetAllSlots(cti.root.Childs.Find("Row 3", true) ?? new()) }
                .SelectMany(r => r)
                .Select(s => (s, s.ID ?? 0)),
            ItemSlot.GetAllSlots(cti.root.Childs.Find("Crafting", true) ?? new())
                .Select(s => (s, s.ID ?? 0)).Where(s => s.Item2 < 9));

        {
            var p = ItemSlot.GetAllSlots(cti.root.Childs.Find("Crafting", true) ?? new())
                .Find(s => s.ID == 9)?.Point;
            if (p is not null)
                p.OnClick += cti.CraftItem;
        }

        cti.inventory.DefaultBindPlayerInventory(args.Player);
        cti.inventory.DefaultBindTransferredItem(args.Player);
        cti.inventory.DefaultBindToolBar(args.Player);

        cti.inventory.GetContainer += () => cti.slots;
        cti.inventory.SetContainer += c => { cti.slots = c as CraftingSlots ?? new(); cti.MathRecipe(); };

        return (cti, cti.root)!;
    }

    public override void Update(UpdateArgs args)
    {
        if (!args.activeInventory)
            return;
        inventory.UpdateSlotsContent(args.time);
    }

    void MathRecipe()
    {
        if (!slots.AnyChanges())
            return;

        var ingridiens = new object?[Register.recipes.types.Count];
        var ri = new RecipeIngredients(this, false);

        for (int i = 0; i < Register.recipes.types.Count; i++)
            ingridiens[i] = Register.recipes.types[i].Item2.Invoke(ri);
        (IRecipeBaze recipe, ItemStack rezult, uint uses, object? details)? recipe = null;
        foreach (var rec in Register.recipes.recipes)
        {
            ri.SetIngridients(ingridiens[rec.Value.index]);
            var r = rec.Value.recipe.CheckRecipe(ri);
            if (!r.HasValue)
                continue;
            recipe = (rec.Value.recipe, r.Value.rezult, r.Value.uses, ingridiens[rec.Value.index]);
            break;
        }
        if (recipe.HasValue)
        {
            ItemSlot.GetAllSlots(root.Childs.Find("Crafting", true) ?? new()).Find(s => s.ID == 9)
                .ItemStack = recipe.Value.rezult;
            usedRecipe = recipe;
            return;
        }
        else
        {
            ItemSlot.GetAllSlots(root.Childs.Find("Crafting", true) ?? new()).Find(s => s.ID == 9)
                .ItemStack = null;
            usedRecipe = recipe;
            return;
        }
    }

    void CraftItem(SpacePoint sp, ClickEventButton ceb)
    {
        if (!usedRecipe.HasValue)
            return;
        var inhandItem = Player.Entity?.Data.Get("transferred_item") as ItemStack?;
        if(inhandItem.HasValue)
        {
            if (usedRecipe.Value.rezult.Item == inhandItem.Value.Item &&
                usedRecipe.Value.rezult.Type == inhandItem.Value.Type &&
                usedRecipe.Value.rezult.Meta == inhandItem.Value.Meta)
            {
                if (usedRecipe.Value.rezult.Count + inhandItem.Value.Count > inhandItem.Value.Item.MaxStackCount(inhandItem.Value))
                    return;
                Player.Entity?.Data.Set("transferred_item", new ItemStack(
                    usedRecipe.Value.rezult.Count + inhandItem.Value.Count,
                    usedRecipe.Value.rezult.Item,
                    usedRecipe.Value.rezult.Type,
                    usedRecipe.Value.rezult.Meta));
            }
        }
        else
            Player.Entity?.Data.Set("transferred_item", usedRecipe.Value.rezult);

        var ri = new RecipeIngredients(this, true);
        ri.SetIngridients(usedRecipe.Value.details);
        usedRecipe.Value.recipe.UseRecipe(ri);
        MathRecipe();
    }

    internal class CraftingSlots : IItemContainer
    {
        StructArray.Count9<ItemStack?> @new, old;

        public bool AnyChanges()
        {
            bool ch = false;
            for (int i = 0; i < 9 && !ch; i++)
                if (@new[i] != old[i])
                    ch = true;
            old = @new;
            return ch;
        }
        public ItemStack? GetItem(int index) => @new[index];
        public void SetItem(int index, ItemStack? item) => @new[index] = item;
        public int Length => 9;
    }
}