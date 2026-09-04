// ReSharper disable UnusedMember.Global
namespace Create.Registry;

public abstract class ElementBase
{
    private ElementIdent? _identity;
    private ushort? _id;
    private int? _index;
    
    internal void SetIdentity(ElementIdent identity, int index) => (_identity, _index) = (identity, index);
    internal void SetId(ushort id) => _id = id;
    
    public bool IsRegistered => _identity.HasValue;
    public ElementIdent Identity => _identity ?? throw new InvalidOperationException("Element is not registered");
    public int Index => _index ?? throw new InvalidOperationException("Element is not registered");
    
    // ReSharper disable InconsistentNaming
    public ushort ID => _id ?? throw new InvalidOperationException("Element ID's have not yet been assigned");
    public bool HasID => _id.HasValue;
    // ReSharper restore InconsistentNaming
}