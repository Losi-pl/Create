using Create.Storage;
using Silk.NET.Direct3D12;

namespace Create.World;

public interface IWorld
{
    public PlacedBlock this[long x, long y, long z] { get; set; }
    
    public RangeUint64 RangeByX { get; }
    public RangeUint64 RangeByY { get; }
    public RangeUint64 RangeByZ { get; }
}