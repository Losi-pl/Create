namespace Create.Elements.BlockClasses;

public class Air : Block
{
    public override bool IsSideSolid(in IsSideSolidArgs args) => false;
}