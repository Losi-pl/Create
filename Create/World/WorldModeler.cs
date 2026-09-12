using System.Reflection;
using Create.Elements;
using Create.Graphics;
using Create.Graphics.Block;
using Silk.NET.Maths;

namespace Create.World;

public abstract class WorldModeler
{
    public delegate void FillOutData<in T>(Span<Vector3D<float>> positions, Span<Vector2D<float>> uvs, Span<uint> triangles, Vector3D<long> blockPositon, T arg);

    public static CompositeMesh GenerateModel(IWorld world, LongRange x, LongRange y, LongRange z)
    {
        API api = new(world);
        api.GenerateModel(x, y, z, CancellationToken.None);
        return api.Finish();
    }
    
    public static RawModel GenerateRawModel(IWorld world, LongRange x, LongRange y, LongRange z)
    {
        API api = new(world);
        api.GenerateModel(x, y, z, CancellationToken.None);
        return api.GetRaw();
    }
    
    public static CompositeMesh GenerateModel(IWorld world, LongRange x, LongRange y, LongRange z, CancellationToken token)
    {
        API api = new(world);
        api.GenerateModel(x, y, z, token);
        return api.Finish();
    }
    
    public static RawModel GenerateRawModel(IWorld world, LongRange x, LongRange y, LongRange z, CancellationToken token)
    {
        API api = new(world);
        api.GenerateModel(x, y, z, token);
        return api.GetRaw();
    }

    public static Task<CompositeMesh> GenerateModelAsync(IWorld world, LongRange x, LongRange y, LongRange z) =>
        Task.RunGraphics(() =>
        {
            API api = new(world);
            api.GenerateModel(x, y, z, CancellationToken.None);
            return api.Finish();
        });
    
    public static Task<RawModel> GenerateRawModelAsync(IWorld world, LongRange x, LongRange y, LongRange z) =>
        Task.Run(() =>
        {
            API api = new(world);
            api.GenerateModel(x, y, z, CancellationToken.None);
            return api.GetRaw();
        });
    
    public static Task<CompositeMesh> GenerateModelAsync(IWorld world, LongRange x, LongRange y, LongRange z, CancellationToken token) =>
        Task.RunGraphics(() =>
        {
            API api = new(world);
            api.GenerateModel(x, y, z, token);
            return api.Finish();
        });

    public static Task<RawModel> GenerateRawModelAsync(IWorld world, LongRange x, LongRange y, LongRange z, CancellationToken token) =>
        Task.Run(() =>
        {
            API api = new(world);
            api.GenerateModel(x, y, z, token);
            return api.GetRaw();
        }, token);
    
    
    private static MethodInfo FindModelTypeMethod(Type type, string name)
    {
        return type.GetMethod($"{typeof(IBlockModelFace).FullName}.{name}", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
               ?? type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
               ?? throw  new InvalidOperationException($"No `{name}()` method found");
    }
    
    public readonly struct RawModel
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IWorld _world;
        private readonly Dictionary<Type, object> _submeshData;
        internal RawModel(IWorld world, Dictionary<Type, object> submeshData) => (_world, _submeshData) = (world, submeshData);
        
        public CompositeMesh Finish()
        {
            if(_submeshData is null || _submeshData.Count == 0)
                return CompositeMesh.Empty;
            
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
    
    // ReSharper disable once InconsistentNaming
    public class API
    {
        private readonly IWorld _world;
        private readonly Dictionary<Type, object> _submeshData = [];
        
        internal API(IWorld world)
        {
            _world = world;
        }
        
        public void AddModelFacet<T>(uint vertexes, uint triangles, IBlockModelFace facet, FillOutData<T> fillOut, Vector3D<long> position, T fillOutArg)
        {
            var type = facet.GetType();
            if(!_submeshData.TryGetValue(type, out var model))
                _submeshData[type] = model = FindModelTypeMethod(type, "CreateNewModelData").Invoke(null, [])!;
            
            facet.AddToModel(model, vertexes, triangles, fillOut, position, fillOutArg);
        }

        internal void GenerateModel(LongRange xRange, LongRange yRange, LongRange zRange, CancellationToken token)
        {
            var airIndex = Blocks.Air.Index;

            var args = new Block.CalculateModelArgs
            {
                Modeler = this,
                World = _world
            };
            
            for (var x = xRange.Start; x < xRange.End; x++)
                for (var y = yRange.Start; y < yRange.End; y++)
                    for (var z = zRange.Start; z < zRange.End; z++)
                    {
                        token.ThrowIfCancellationRequested();
                        
                        args.Target = _world[x, y, z];
                        if(args.Target.BlockIndex == airIndex)
                            continue;

                        args.Position = new(x, y, z);
                        args.Target.Block.CalculateModel(in args);
                    }
        }

        internal RawModel GetRaw() => new(_world, _submeshData);
        
        internal CompositeMesh Finish()
        {
            if(_submeshData.Count == 0)
                return CompositeMesh.Empty;
            
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