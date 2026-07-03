namespace Create.General;

[UnionAliases(aliasT0: "Set", aliasT1: "None")]
public partial struct PossibleResult<T>: IUnion<T, None>
{
    public PossibleResult() { }
}