using Silk.NET.Direct3D12;

namespace Create.World;

public interface IWorld
{
    public bool this[long x, long y, long z] { get; set; }
    
    public RangeUint64 RangeByX { get; }
    public RangeUint64 RangeByY { get; }
    public RangeUint64 RangeByZ { get; }
}