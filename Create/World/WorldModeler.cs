using Silk.NET.Maths;

namespace Create.World;

public abstract class WorldModeler
{
    public delegate void FillOutData<in T>(Span<Vector3D<float>> positions, Span<Vector2D<float>> uvs, Span<uint> triangles, T arg);
}