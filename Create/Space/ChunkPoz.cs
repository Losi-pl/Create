using System.Diagnostics.CodeAnalysis;

namespace Create.Space;

/// <summary>
/// Pozycja kawałku terenu <see cref="Chunk"/> w światach opartych na <see cref="DimentionSpace"/>
/// </summary>
public struct ChunkPoz
{
    public int X { get; set; }
    public int Z { get; set; }
    public ChunkPoz(int x, int z)
    {
        X = x;
        Z = z;
    }

    public static bool operator ==(ChunkPoz a, ChunkPoz b) => a.X == b.X && a.Z == b.Z;
    public static bool operator !=(ChunkPoz a, ChunkPoz b) => !(a.X == b.X && a.Z == b.Z);

    public static ChunkPoz operator +(ChunkPoz a, ChunkPoz b) => new(a.X + b.X, a.Z + b.Z);
    public static ChunkPoz operator +(ChunkPoz a, int i) => new(a.X + i, a.Z + i);

    public static ChunkPoz operator -(ChunkPoz a, ChunkPoz b) => new(a.X - b.X, a.Z - b.Z);
    public static ChunkPoz operator -(ChunkPoz a, int i) => new(a.X - i, a.Z - i);
    public static ChunkPoz operator -(ChunkPoz a) => new(-a.X, -a.Z);

    public static ChunkPoz operator *(ChunkPoz a, ChunkPoz b) => new(a.X * b.X, a.Z * b.Z);
    public static ChunkPoz operator *(ChunkPoz a, int i) => new(a.X * i, a.Z * i);

    public static ChunkPoz operator /(ChunkPoz a, ChunkPoz b) => new(a.X / b.X, a.Z / b.Z);
    public static ChunkPoz operator /(ChunkPoz a, int i) => new(a.X / i, a.Z / i);

    public static ChunkPoz operator %(ChunkPoz a, ChunkPoz b) => new(a.X % b.X, a.Z % b.Z);
    public static ChunkPoz operator %(ChunkPoz a, int i) => new(a.X % i, a.Z % i);

    public override int GetHashCode() => base.GetHashCode();
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ChunkPoz)
            return false;
        return (ChunkPoz)obj == this;
    }
}
