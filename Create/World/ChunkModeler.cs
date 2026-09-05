using Create.Assets;
using Create.Elements;
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
    private readonly List<uint> _textures = [];
    private readonly List<uint> _triangles = [];
    
    private void AddModelFacet<T>(uint vertexes, uint triangles, T extraData, Action<Span<Vector3D<float>>, Span<Vector2D<float>>, Span<uint>, T> fillOut, BlockTexture texture)
    {
        Span<Vector3D<float>> positions = stackalloc Vector3D<float>[(int)vertexes];
        Span<Vector2D<float>> uvs = stackalloc Vector2D<float>[(int)vertexes];
        Span<uint> trianglesElem = stackalloc uint[(int)triangles * 3];
        Span<uint> textureInd = stackalloc uint[(int)vertexes];

        textureInd.Fill(texture.Index);
        fillOut(positions, uvs, trianglesElem, extraData);
        {
            var prevCount = (uint)_positions.Count;
            for (var i = 0; i < trianglesElem.Length; i++)
                trianglesElem[i] += prevCount;
        }
        _positions.AddRange(positions);
        _uvs.AddRange(uvs);
        _textures.AddRange(textureInd);
        _triangles.AddRange(trianglesElem);
    }

    public Mesh GenerateModel(RealmWorld world)
    {
        var airIndex = Blocks.Air.Index;
        
        for (var x = 0; x < RealmWorld.CHUNK_CUBE_SIZE; x++)
            for (var y = 0; y < RealmWorld.CHUNK_CUBE_SIZE; y++)
                for (var z = 0; z < RealmWorld.CHUNK_CUBE_SIZE; z++)
                {
                    var block = world[x, y, z];
                    if(block.BlockIndex == airIndex)
                        continue;

                    var pos = new Vector3D<int>(x, y, z);

                    var args = new Block.GetTextureArgs
                    {
                        Position = pos.As<long>(),
                        Target = block,
                        World = world
                    };
                    
                    DoFacet(GeneralDirection.North, ref args, NorthFaced);
                    DoFacet(GeneralDirection.East, ref args, EastFaced);
                    DoFacet(GeneralDirection.South, ref args, SouthFaced);
                    DoFacet(GeneralDirection.West, ref args, WestFaced);
                    DoFacet(GeneralDirection.Top, ref args, TopFaced);
                    DoFacet(GeneralDirection.Bottom, ref args, BottomFaced);
                }

        return Mesh.Create(_shader).ManualFillOut()
            .SetAttribute("pos", _positions.ToArray())
            .SetAttribute("uvPos", _uvs.ToArray())
            .SetAttribute("atlasInd", _textures.ToArray())
            .Triangles(_triangles.ToArray())
            .Finish();

        void DoFacet(GeneralDirection direction, ref Block.GetTextureArgs texArgs, Action<Span<Vector3D<float>>,Span<Vector2D<float>>,Span<uint>,Vector3D<int>> fillOut)
        {
            var position = texArgs.Position + direction.AsVector().As<long>();
            var target = world[position.X, position.Y, position.Z];

            if (target.BlockIndex != airIndex)
            {
                var solidArgs = new Block.IsSideSolidArgs
                {
                    Position = position,
                    Direction = direction.Inverted,
                    Target = target,
                    World = world
                };
                if(target.Block.IsSideSolid(in solidArgs))
                    return;
            }

            texArgs.Direction = direction;
            var texture = texArgs.Target.Block.GetTexture(in texArgs);
            
            AddModelFacet(4, 2, texArgs.Position.As<int>(), fillOut, texture);
        }
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