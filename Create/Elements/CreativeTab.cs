using Create.Conteiner;
using System.Collections.ObjectModel;

namespace Create.Elements;

public class CreativeTab : Baze
{
    public Func<IEnumerable<ItemStack>>? RegisterElements { private get; init; }
    public Func<ItemStack>? CreateIcon { private get; init; }
    ReadOnlyCollection<ItemStack>? items;
    ItemStack? icon;

    internal void load_stacks()
    {
        items = new((RegisterElements?.Invoke() ?? Enumerable.Empty<ItemStack>()).ToArray());
        icon = (CreateIcon?.Invoke() ?? (items.Count > 0 ? items[0] : null));
    }

    public ReadOnlyCollection<ItemStack> Items => items!;
    public ItemStack? Icon => icon;
    public string TabName { get; init; } = "create.creative-tabs.name";
}
