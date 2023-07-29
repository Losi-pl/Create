using Create.Elements.Bazic.Dimentions;
using Create.SourceGenerators;
using Create.Space;

namespace Create.Elements;

/// <summary>
/// Wrzystkie wymiary
/// </summary>
[Register(typeof(Dimention))]
public static class Dimentions
{
    public static readonly Dimention OVERWORLD = new Overworld();

}