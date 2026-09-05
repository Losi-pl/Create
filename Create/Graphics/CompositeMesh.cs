using System.Collections;
using System.Collections.Immutable;
using System.Numerics;

namespace Create.Graphics;

public class CompositeMesh: IReadOnlySet<Mesh>
{
    private readonly ImmutableSortedSet<Mesh> _parts;

    // ReSharper disable once ConvertToPrimaryConstructor
    public CompositeMesh(IEnumerable<Mesh> parts) => _parts = parts.ToImmutableSortedSet(MeshComparer.Default);
    
    public ImmutableSortedSet<Mesh>.Enumerator GetEnumerator() => _parts.GetEnumerator();
    IEnumerator<Mesh> IEnumerable<Mesh>.GetEnumerator() => _parts.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _parts.GetEnumerator();
    
    public int Count => _parts.Count;

    public Matrix4x4 ModelMatrix
    {
        get;
        set
        {
            field = value;
            Shader? last = null;
            foreach (var mesh in this)
            {
                if (last != mesh.Shader)
                    last?.TrySetModelUniform(value);
                last = mesh.Shader;
            }
            last?.TrySetModelUniform(value);
        }
    }
    
    public Matrix4x4 ViewMatrix
    {
        get;
        set
        {
            field = value;
            Shader? last = null;
            foreach (var mesh in this)
            {
                if (last != mesh.Shader)
                    last?.TrySetViewUniform(value);
                last = mesh.Shader;
            }
            last?.TrySetViewUniform(value);
        }
    }
    
    public Matrix4x4 ProjectionMatrix
    {
        get;
        set
        {
            field = value;
            Shader? last = null;
            foreach (var mesh in this)
            {
                if (last != mesh.Shader)
                    last?.TrySetProjectionUniform(value);
                last = mesh.Shader;
            }
            last?.TrySetProjectionUniform(value);
        }
    }
    
    public void Draw()
    {
        Shader current = null!;
        foreach (var mesh in this)
        {
            mesh.Draw(current != mesh.Shader);
            current = mesh.Shader;
        }
    }
    
    public bool Contains(Mesh item) => _parts.Contains(item);
    public bool IsProperSubsetOf(IEnumerable<Mesh> other) => _parts.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<Mesh> other) => _parts.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<Mesh> other) => _parts.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<Mesh> other) => _parts.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<Mesh> other) => _parts.Overlaps(other);
    public bool SetEquals(IEnumerable<Mesh> other) => _parts.SetEquals(other);
    
    private class MeshComparer : IComparer<Mesh>
    {
        public static MeshComparer Default { get; } = new();
        
        public int Compare(Mesh? x, Mesh? y)
        {
            if(x is null or { Disposed: true } && y is null or { Disposed: true })
                return 0;
            if(x is null or { Disposed: true } || y is null or { Disposed: true })
                return x is null or { Disposed: true } ? -1 : 1;
            
            return x.Shader.GetHashCode().CompareTo(y.Shader.GetHashCode());
        }
    }
}