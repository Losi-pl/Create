using Create.Assets;
using Create.Elements;
using Create.Graphics;
using Silk.NET.Maths;

namespace Create.World;

public abstract class WorldModeler
{
    public delegate void FillOutData<in T>(Span<Vector3D<float>> positions, Span<Vector2D<float>> uvs, Span<uint> triangles, T arg);

    public static Mesh GenerateModel(IWorld world)
    {
        API api = new(world);
        return api.GenerateModel();
    }
    
    // ReSharper disable once InconsistentNaming
    public class API
    {
        private static Shader _shader
        {
            get => field ??= AssetManager.Find<Shader>("create:blocks/single-texture") is { IsSet: true, AsSet: var set } ? set : throw new Exception("Shader not found");
        } = null!;
        
        private readonly List<Vector3D<float>> _positions = [];
        private readonly List<Vector2D<float>> _uvs = [];
        private readonly List<uint> _textures = [];
        private readonly List<uint> _triangles = [];
        
        private readonly IWorld _world;
        
        internal API(IWorld world)
        {
            _world = world;
        }
        
        public void AddModelFacet<T>(uint vertexes, uint triangles, T extraData, FillOutData<T> fillOut, BlockTexture texture)
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

        internal Mesh GenerateModel()
        {
            var airIndex = Blocks.Air.Index;

            var args = new Block.CalculateModelArgs
            {
                Modeler = this,
                World = _world
            };
            
            for (var x = 0; x < RealmWorld.CHUNK_CUBE_SIZE; x++)
                for (var y = 0; y < RealmWorld.CHUNK_CUBE_SIZE; y++)
                    for (var z = 0; z < RealmWorld.CHUNK_CUBE_SIZE; z++)
                    {
                        args.Target = _world[x, y, z];
                        if(args.Target.BlockIndex == airIndex)
                            continue;

                        args.Position = new(x, y, z);
                        args.Target.Block.CalculateModel(in args);
                    }

            return Mesh.Create(_shader).ManualFillOut()
                .SetAttribute("pos", _positions.ToArray())
                .SetAttribute("uvPos", _uvs.ToArray())
                .SetAttribute("atlasInd", _textures.ToArray())
                .Triangles(_triangles.ToArray())
                .Finish();

            
        }
    }
}