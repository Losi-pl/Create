using Create.Conteiner;
using Create.Space;

namespace Create.Elements.Bazic.Items;

public interface IClothing
{

    public virtual Placement GetArmorPlacement(GetArmorPlacementData data) =>
        Placement.Head | Placement.Torso | Placement.Legs | Placement.Feet;

    public struct GetArmorPlacementData
    {
        public ItemStack ItemStack;
        public LivingEntity Entity;
        public World World => Entity.Dimention!.World;
    }

    [Flags]
    public enum Placement
    {
        Head = 1, Torso = 2, Legs = 4, Feet = 8
    }
}
