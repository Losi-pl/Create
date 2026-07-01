namespace Create.Registry;

public class LoadingRegister
{
    private IMod _mod;
    private Dictionary<IMod, List<Action<ElementRegister>>> _elementRegisterProcess;
    
    internal LoadingRegister(IMod mod, Dictionary<IMod, List<Action<ElementRegister>>> elementRegisterProcess) => 
        (_mod, _elementRegisterProcess) = (mod, elementRegisterProcess);

    public void AddElementRegisterProcess(Action<ElementRegister> precess)
    {
        if (_elementRegisterProcess.TryGetValue(_mod, out var list))
            list.Add(precess);
        else
            _elementRegisterProcess[_mod] = [precess];
    }
    
    internal void Dispose()
    {
        _mod = null!;
        _elementRegisterProcess = null!;
    }
}