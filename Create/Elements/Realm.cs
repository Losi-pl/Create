using Create.Registry;
using Create.World;

namespace Create.Elements;

public abstract partial class Realm : ElementBase
{
    protected Realm()
    {
        World = new(this);
    }
    
    public RealmWorld World { get; }
}