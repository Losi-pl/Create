using Create.OpenGL;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Render;

partial class ModelConstructor
{
    public static WorldModel WorldModel(World world, (int start, int end) X, (int start, int end) Y, (int start, int end) Z)
    {
        ModelConstructor mc = new();
        if (X.end < X.start)
            X = (X.end, X.start);
        if (Y.end < Y.start)
            Y = (Y.end, Y.start);
        if (Y.end < Y.start)
            Y = (Y.end, Y.start);

        for (int x = X.start; x <= X.end; x++)
            for (int y = Y.start; y <= Y.end; y++)
                for (int z = Z.start; z <= Z.end; z++)
                {
                    var bl = world.GetBlock(x, y, z);
                    bl.Block.GenerateModel(new()
                    {
                        pozition = (x, y, z),
                        block = bl,
                        world = world
                    }, mc);
                }
        Dictionary<Type, Mesh> quard_m = new();
        foreach (var elem in mc.ModelMekanizm)
            quard_m.Add(elem.Key, elem.Value.FinischModel());
        return new(quard_m);
    }
}

public sealed class WorldModel : IDrawable, IDisposable
{
    Dictionary<Type, Mesh> meshes;

    internal WorldModel(Dictionary<Type, Mesh> meshes) => this.meshes = meshes;
    public void Draw(Matrix4 projection, Matrix4 model)
    {
        foreach (var kvp in meshes)
            if (kvp.Value?.TrianglesCount > 0)
                kvp.Value?.Draw(projection, model);
    }

    public void Dispose()
    {
        foreach (var m in meshes)
            m.Value?.Dispose();
    }

    public static readonly WorldModel Empty = new(new());
}