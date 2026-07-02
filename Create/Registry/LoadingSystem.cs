using Create.Assets;

namespace Create.Registry;

public class LoadingSystem
{
    private IMod _mod;
    private Dictionary<IMod, List<Action<ElementRegister>>> _elementRegisterProcess;
    
    internal LoadingSystem(IMod mod, Dictionary<IMod, List<Action<ElementRegister>>> elementRegisterProcess) => 
        (_mod, _elementRegisterProcess) = (mod, elementRegisterProcess);

    public void AddElementRegisterProcess(Action<ElementRegister> precess)
    {
        if (_elementRegisterProcess.TryGetValue(_mod, out var list))
            list.Add(precess);
        else
            _elementRegisterProcess[_mod] = [precess];
    }

    public void AddResourceProcessor<T>(IResourceProcessor<T> processor, string assetPath)
    {
        if(_mod is null)
            throw new NullReferenceException("This loader was closed");
        AssetManager.RegisterProcessor(processor, assetPath);
    }
    
    internal void Dispose()
    {
        _mod = null!;
        _elementRegisterProcess = null!;
    }
}