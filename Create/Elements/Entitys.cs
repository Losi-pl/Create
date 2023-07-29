using Create.Elements.Bazic.Entitys;
using Create.SourceGenerators;

namespace Create.Elements;

/// <summary>
/// Wrzystkie byty
/// </summary>
[Register(typeof(Entity))]
public static class Entitys
{
    public static readonly Entity PLAYER = new Player();
}
