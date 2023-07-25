namespace Create.Elements;

/// <summary>
/// Baza do budowy itemów
/// </summary>
public abstract class Item : Baze
{
    //Ustawienie bazowego typu elementu na Item
    public sealed override Type ElementBazicType => typeof(Item);

}
