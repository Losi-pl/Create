using Create.Assets;
using Create.Graphics;
using Silk.NET.Maths;

namespace Create.World;

public class ChunkModeler: WorldModeler
{
    // ReSharper disable once InconsistentNaming
    private static Shader _shader
    {
        get => field ??= AssetManager.Find<Shader>("create:blocks/single-texture") is { IsSet: true, AsSet: var set } ? set : throw new Exception("Shader not found");
    } = null!;

    private readonly List<Vector3D<float>> _positions = [];
    private readonly List<Vector2D<float>> _uvs = [];
    private readonly List<uint> _triangles = [];
    
    private void AddModelFacet<T>(uint vertexes, uint triangles, T extraData, Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, T> fillOut)
    {
        Span<Vector3D<float>> positions = stackalloc Vector3D<float>[(int)vertexes];
        Span<Vector2D<float>> uvs = stackalloc Vector2D<float>[(int)vertexes];
        Span<uint> trianglesElem = stackalloc uint[(int)triangles * 3];

        fillOut(positions, uvs, trianglesElem, extraData);
        {
            var prevCount = (uint)_positions.Count;
            for (var i = 0; i < trianglesElem.Length; i++)
                trianglesElem[i] += prevCount;
        }
        _positions.AddRange(positions);
        _uvs.AddRange(uvs);
        _triangles.AddRange(trianglesElem);
    }

    public Mesh GenerateModel(RealmWorld world)
    {
        for (var x = 0; x < RealmWorld.CHUNK_CUBE_SIZE; x++)
            for (var y = 0; y < RealmWorld.CHUNK_CUBE_SIZE; y++)
                for (var z = 0; z < RealmWorld.CHUNK_CUBE_SIZE; z++)
                {
                    var hasBl = world[x, y, z];
                    if(!hasBl)
                        continue;
                    
                    if(!world[x, y, z - 1])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), SouthFaced);
                    if(!world[x, y, z + 1])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), NorthFaced);
                    if(!world[x + 1, y, z])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), EastFaced);
                    if(!world[x - 1, y, z])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), WestFaced);
                    if(!world[x, y + 1, z])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), TopFaced);
                    if(!world[x, y - 1, z])
                        AddModelFacet(4, 2, new Vector3D<int>(x, y, z), BottomFaced);
                }

        return Mesh.Create(_shader).ManualFillOut()
            .SetAttribute("pos", _positions.ToArray())
            .SetAttribute("uvPos", _uvs.ToArray())
            .SetAttribute("atlasInd", new uint[_uvs.Count].Fill(1u))
            .Triangles(_triangles.ToArray())
            .Finish();
    }

    private static void SetUvAndTriangles(Span<Vector2D<float>> uvs, Span<uint> triangles)
    {
        uvs[0] = new(0f, 0f);
        uvs[1] = new(1f, 0f);
        uvs[2] = new(1f, 1f);
        uvs[3] = new(0f, 1f);

        triangles[0] = 0u;
        triangles[1] = 1u;
        triangles[2] = 3u;
        triangles[3] = 1u;
        triangles[4] = 2u;
        triangles[5] = 3u;
    }
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> SouthFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(0f, 1f, 0f) + blPos;
            positions[1] = new Vector3D<float>(1f, 1f, 0f) + blPos;
            positions[2] = new Vector3D<float>(1f, 0f, 0f) + blPos;
            positions[3] = new Vector3D<float>(0f, 0f, 0f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> NorthFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(1f, 1f, 1f) + blPos;
            positions[1] = new Vector3D<float>(0f, 1f, 1f) + blPos;
            positions[2] = new Vector3D<float>(0f, 0f, 1f) + blPos;
            positions[3] = new Vector3D<float>(1f, 0f, 1f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> EastFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(1f, 1f, 0f) + blPos;
            positions[1] = new Vector3D<float>(1f, 1f, 1f) + blPos;
            positions[2] = new Vector3D<float>(1f, 0f, 1f) + blPos;
            positions[3] = new Vector3D<float>(1f, 0f, 0f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> WestFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(0f, 1f, 1f) + blPos;
            positions[1] = new Vector3D<float>(0f, 1f, 0f) + blPos;
            positions[2] = new Vector3D<float>(0f, 0f, 0f) + blPos;
            positions[3] = new Vector3D<float>(0f, 0f, 1f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> TopFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(0f, 1f, 1f) + blPos;
            positions[1] = new Vector3D<float>(1f, 1f, 1f) + blPos;
            positions[2] = new Vector3D<float>(1f, 1f, 0f) + blPos;
            positions[3] = new Vector3D<float>(0f, 1f, 0f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
    
    private static readonly Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, Vector3D<int>> BottomFaced =
        (positions, uvs, triangles, position) => {
            var blPos = position.As<float>();
            positions[0] = new Vector3D<float>(0f, 0f, 0f) + blPos;
            positions[1] = new Vector3D<float>(1f, 0f, 0f) + blPos;
            positions[2] = new Vector3D<float>(1f, 0f, 1f) + blPos;
            positions[3] = new Vector3D<float>(0f, 0f, 1f) + blPos;
            
            SetUvAndTriangles(uvs, triangles);
        };
}