
namespace Create.Conteiner.Items;

public interface ISlotFilter
{
    public bool IsItemStackAllowed(IsAllowedData data);

    public struct IsAllowedData
    {
        public int SlotIndex { get; set; }
        public ItemStack ItemStack { get; set; }
        public Net.Player Player { get; set; }
    }
}
