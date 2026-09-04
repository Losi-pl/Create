namespace Create.Registry;

public class ElementRegister
{
    private IMod _mod;
    
    internal  ElementRegister(IMod mod)
    {
        _mod = mod;
    }

    public ElementType<T> OpenElementType<T>() where T : ElementBase
    {
        if (typeof(T) == typeof(ElementBase))
            throw new InvalidOperationException(
                $"`{nameof(ElementBase)}` type is not supported for element base type.\n" +
                $"Specify a concrete derived type.");
        
        return new(this);
    }
    
    internal void Dispose()
    {
        _mod = null!;
    }

    public readonly ref struct ElementType<T> where T : ElementBase
    {
        private readonly ElementRegister _register;
        private readonly GameElements.TypeLibrary<T> _library;
        
        internal  ElementType(ElementRegister register)
        {
            _register = register;
            _library = GameElements.OpenLibrary<T>();
        }

        public void Register(T element, string name) => _library.RegisterElement((_register._mod, name), element);
    }
}