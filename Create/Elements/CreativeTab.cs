using Create.Conteiner;

namespace Create.Elements;

public class CreativeTab : Baze
{
    public Func<IEnumerable<ItemStack>>? RegisterElements { private get; init; }
    ItemStack[]? items;

    internal void load_stacks()
    {
        items = (RegisterElements?.Invoke() ?? Enumerable.Empty<ItemStack>()).ToArray();
    }
}
