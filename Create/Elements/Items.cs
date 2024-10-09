using Create.Elements.Bazic.Items;
using Create.SourceGenerators;

namespace Create.Elements;

[Register(typeof(Item))]
public static class Items
{
    public static readonly Item BLOCK_ITEM = new BlockItem();
    public static readonly Item IRON_LEGGINGS = new IronLeggings();
    public static readonly Item IRON_BOOTS = new IronBoots();
}
