using Create.Conteiner;
using Create.OpenGL.Textures;
using Create.Render;
using Create.Render.ModelCreators.Model;
using Create.Render.ModelCreators.Side;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements;

/// <summary>
/// Baza do budowy bloków
/// </summary>
public abstract class Item : Baze
{
    //Ustawienie bazowego typu elementu na Item
    public sealed override Type ElementBazicType => typeof(Item);

}
