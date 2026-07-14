namespace Create.General;

[UnionAliases("Result", "Error")]
public partial struct ExcResult<T>(): IUnion<T, Exception>
{
    
}