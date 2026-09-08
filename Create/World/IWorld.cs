using Create.Storage;
using Silk.NET.Direct3D12;

namespace Create.World;

public interface IWorld
{
    public PlacedBlock this[long x, long y, long z] { get; set; }
    
    public LongRange RangeByX { get; }
    public LongRange RangeByY { get; }
    public LongRange RangeByZ { get; }
}