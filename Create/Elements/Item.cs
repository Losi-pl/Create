using Create.Conteiner;
using Create.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements;

/// <summary>
/// Baza do budowy itemów
/// </summary>
public abstract class Item : Baze
{
    //Ustawienie bazowego typu elementu na Item
    public sealed override Type ElementBazicType => typeof(Item);

    public virtual ItemModel GetItemModel(ItemStack itemStack, Net.Player player) => new();

    public struct ItemModel
    {
        public IDrawable model;
        public (Color4 color, float progress)? statusBar;
    }
}
