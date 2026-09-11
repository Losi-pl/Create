using System.Numerics;
using Create.Graphics;
using Silk.NET.OpenGL;
using Shader = Create.Graphics.Shader;

namespace Create.World;

public class RealmModel(RealmWorld world) : IDisposable
{
    private readonly Dictionary<Shader, HashSet<Mesh>> _meshes = new();
    private readonly Dictionary<ChunkPos, CompositeMesh[]> _chunks = new();
    private readonly Dictionary<ChunkPos, Task<WorldModeler.RawModel[]>> _inProcess = new();
    private readonly Dictionary<ChunkPos, Task<CompositeMesh[]>> _generated = new();
    private readonly Lock _lock = new(), _modelMessing = new();
    private readonly CancellationTokenSource _token = new();

    public Matrix4x4 ModelMatrix
    {
        get;
        set
        {
            field = value;
            foreach (var shader in _meshes.Keys)
                shader.TrySetModelUniform(value);
        }
    } = Matrix4x4.Identity;
    public Matrix4x4 ViewMatrix
    {
        get;
        set
        {
            field = value;
            foreach (var shader in _meshes.Keys)
                shader.TrySetViewUniform(value);
        }
    } = Matrix4x4.Identity;
    public Matrix4x4 ProjectionMatrix
    {
        get;
        set
        {
            field = value;
            foreach (var shader in _meshes.Keys)
                shader.TrySetProjectionUniform(value);
        }
    } = Matrix4x4.Identity;
    
    public bool IsDisposed => _token.IsCancellationRequested;
    public void Dispose()
    {
        if (IsDisposed)
            return;
        _token.Cancel();
    }

    internal void Draw() => Draw(Window.GL);
    public void Draw(GL gl)
    {
        lock (_modelMessing)
        {
            foreach (var parts in _meshes)
            {
                parts.Key.Bind(gl);
                foreach (var mesh in parts.Value)
                    mesh.Draw(gl, false);
            }
        }
    }
    
    public void AddChunkToModel(ChunkPos position)
    {
        if (IsDisposed)
            throw new InvalidOperationException("Object disposed");
        
        lock (_lock)
        {
           if(_chunks.ContainsKey(position))
               return;
           if(_inProcess.ContainsKey(position))
               return;
           _inProcess[position] = GenerateModel(position, _token.Token);
        }
    }

    public void Update()
    {
        List<KeyValuePair<ChunkPos, Task<CompositeMesh[]>>> toAdd = null!;
        lock (_lock)
        {
            if (_generated.Count > 0)
            {
                (toAdd ??= new()).AddRange(_generated);
                _generated.Clear();
                foreach (var chunk in toAdd)
                    _chunks[chunk.Key] = chunk.Value.Result;
            }
        }

        lock (_modelMessing)
        {
            if(toAdd is not null)
                foreach (var chunk in toAdd)
                {
                    var cubes = chunk.Value.Result;
                    (Shader me, HashSet<Mesh> parts) current = (null!, null!);
                    
                    foreach (var cube in cubes)
                        foreach (var mesh in cube)
                        {
                            if (current.me != mesh.Shader)
                            {
                                if (_meshes.TryGetValue(mesh.Shader, out var set))
                                    current = (mesh.Shader, set);
                                else
                                {
                                    mesh.Shader.TrySetModelUniform(ModelMatrix);
                                    mesh.Shader.TrySetViewUniform(ViewMatrix);
                                    mesh.Shader.TrySetProjectionUniform(ProjectionMatrix);
                                    
                                    current = (mesh.Shader, _meshes[mesh.Shader] = new());
                                }
                            }
                            current.parts.Add(mesh);
                            mesh.ThreadBind();
                        }
                }
        }
    }

    private Task<WorldModeler.RawModel[]> GenerateModel(ChunkPos chunkPos, CancellationToken token)
    {
        var task = _inProcess[chunkPos] = Task.Run(async () =>
        {
            var parts = Enumerable.Range(0, IChunk.CHUNK_CUBE_STACK).Select(i =>
                WorldModeler.GenerateRawModel(world,
                    x: new((long)chunkPos.X * IChunk.CHUNK_CUBE_SIZE, (long)(chunkPos.X + 1) * IChunk.CHUNK_CUBE_SIZE),
                    y: new((long)i * IChunk.CHUNK_CUBE_SIZE,          (long)(i + 1) * IChunk.CHUNK_CUBE_SIZE),
                    z: new((long)chunkPos.Z * IChunk.CHUNK_CUBE_SIZE, (long)(chunkPos.Z + 1) * IChunk.CHUNK_CUBE_SIZE))
            ).ToArray();
            token.ThrowIfCancellationRequested();

            return parts;
        }, token);
        
        // ReSharper disable once VariableHidesOuterVariable
        task.ContinueWith(task =>
        {
            lock (_lock)
            {
                _inProcess.Remove(chunkPos);
                _generated[chunkPos] = Task.RunGraphics(() =>
                {
                    var cubes = new CompositeMesh[IChunk.CHUNK_CUBE_STACK];
                    for (var i = 0; i < IChunk.CHUNK_CUBE_STACK; i++)
                    {
                        token.ThrowIfCancellationRequested();
                        cubes[i] = task.Result[i].Finish();
                    }

                    return cubes;
                });
            }
        }, token);
        
        return task;
    }
}