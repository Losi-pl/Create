namespace Create.Conteiner.Items;

public interface IItemContainer
{
    public ItemStack? GetItem(int index);
    public void SetItem(int index, ItemStack? item);
    public int Length { get; }
}
