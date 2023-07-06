using Create.Conteiner;
using OpenTK.Mathematics;

namespace Create.Space;

/// <summary>
/// Podstawowa platforma do interakcji z terenem dowolnego pochodenia
/// </summary>
public abstract class World
{
    /// <summary>
    /// Opcjonalny obiekt definjujący pochodenie terenu
    /// <para><c>np. <see cref="DimentionSpace"/></c></para>
    /// </summary>
    public virtual object? Owner => null;
    
    /// <summary>
    /// Metoda do pozyskania informacji na temat bloku o pozycji (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>)
    /// </summary>
    public abstract PlacedBlock GetBlock(int x, int y, int z);

    /// <summary>
    /// Metoda do pozyskania informacji na temat bloku o pozycji <paramref name="poz"/>
    /// </summary>
    public PlacedBlock GetBlock(Vector3i poz)
    {
        var w = this;
        return w.GetBlock(poz.X, poz.Y, poz.Z);
    }

    /// <summary>
    /// Metoda do pozyskania informacji na temat bloku o pozycji <paramref name="poz"/>
    /// </summary>
    public PlacedBlock GetBlock((int X, int Y, int Z) poz)
    {
        var w = this;
        return w.GetBlock(poz.X, poz.Y, poz.Z);
    }

    /// <summary>
    /// Metoda do zmiany parametrów bloku o pozycji (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>)
    /// </summary>
    public abstract void SetBlock(int x, int y, int z, PlacedBlock block);

    /// <summary>
    /// Metoda do zmiany parametrów bloku o pozycji <paramref name="poz"/>
    /// </summary>
    public void SetBlock(Vector3i poz, PlacedBlock block)
    {
        var w = this;
        w.SetBlock(poz.X, poz.Y, poz.Z, block);
    }

    /// <summary>
    /// Metoda do zmiany parametrów bloku o pozycji <paramref name="poz"/>
    /// </summary>
    public void SetBlock((int x, int y, int z) poz, PlacedBlock block)
    {
        var w = this;
        w.SetBlock(poz.x, poz.y, poz.z, block);
    }
}
