namespace Create.Registry;

public class ElementRegister
{
    private IMod _mod;
    
    internal  ElementRegister(IMod mod)
    {
        _mod = mod;
    }

    internal void Dispose()
    {
        _mod = null!;
    }
}