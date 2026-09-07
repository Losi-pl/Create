using System.Reflection;
using Create.Elements;
using Create.Graphics;
using Create.Graphics.Block;
using Silk.NET.Maths;

namespace Create.World;

public abstract class WorldModeler
{
    public delegate void FillOutData<in T>(Span<Vector3D<float>> positions, Span<Vector2D<float>> uvs, Span<uint> triangles, Vector3D<long> blockPositon, T arg);

    public static CompositeMesh GenerateModel(IWorld world)
    {
        API api = new(world);
        return api.GenerateModel();
    }
    
    // ReSharper disable once InconsistentNaming
    public class API
    {
        private readonly IWorld _world;
        private readonly Dictionary<Type, object> _submeshData = [];
        
        internal API(IWorld world)
        {
            _world = world;
        }
        
        private MethodInfo FindModelTypeMethod(Type type, string name)
        {
            return type.GetMethod($"{typeof(IBlockModelFace).FullName}.{name}", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                   ?? type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                   ?? throw  new InvalidOperationException($"No `{name}()` method found");
        }
        
        public void AddModelFacet<T>(uint vertexes, uint triangles, IBlockModelFace facet, FillOutData<T> fillOut, Vector3D<long> position, T fillOutArg)
        {
            var type = facet.GetType();
            if(!_submeshData.TryGetValue(type, out var model))
                _submeshData[type] = model = FindModelTypeMethod(type, "CreateNewModelData").Invoke(null, [])!;
            
            facet.AddToModel(model, vertexes, triangles, fillOut, position, fillOutArg);
        }

        internal CompositeMesh GenerateModel()
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

            List<Mesh> parts = [];
            foreach (var modelPart in _submeshData)
            {
                var finisher = FindModelTypeMethod(modelPart.Key, "FinishModel");
                var mesh = finisher.Invoke(null, [modelPart.Value]) as Mesh;
                parts.Add(mesh ?? throw new InvalidOperationException($"No model created for `{modelPart.Key}()`"));
            }

            return new(parts);
        }
    }
}