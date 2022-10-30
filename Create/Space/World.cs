using Create.Conteiner;
using OpenTK.Mathematics;

namespace Create.Space;

public abstract class World
{
    public virtual object? Owner => null;
    
    public abstract PlacedBlock GetBlock(int x, int y, int z);
    public PlacedBlock GetBlock(Vector3i poz)
    {
        var w = this;
        return w.GetBlock(poz.X, poz.Y, poz.Z);
    }
    public PlacedBlock GetBlock((int X, int Y, int Z) poz)
    {
        var w = this;
        return w.GetBlock(poz.X, poz.Y, poz.Z);
    }

    public abstract void SetBlock(int x, int y, int z, PlacedBlock block);
    public void SetBlock(Vector3i poz, PlacedBlock block)
    {
        var w = this;
        w.SetBlock(poz.X, poz.Y, poz.Z, block);
    }
    public void SetBlock((int x, int y, int z) poz, PlacedBlock block)
    {
        var w = this;
        w.SetBlock(poz.x, poz.y, poz.z, block);
    }
}
