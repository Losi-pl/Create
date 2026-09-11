using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;
using Create.Graphics;
using Silk.NET.OpenGL;
using Shader = Create.Graphics.Shader;

namespace Create.World;

public class RealmModel : IDisposable
{
    private readonly RealmWorld _world;
    private readonly Dictionary<Shader, HashSet<Mesh>> _meshes = new();
    private readonly Dictionary<ChunkPos, CompositeMesh> _chunks = new();
    private readonly Lock _lock = new(), _modelMessing = new();
    private readonly CancellationTokenSource _token = new();

    private readonly HashSet<ChunkPos> _inGeneration = new();
    private readonly ConcurrentQueue<ChunkPos> _toGenerate = new();
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private RawModelThreads _modelThreads;
    // ReSharper disable once NotAccessedField.Local
    private readonly Thread _modelFinisher;
    private readonly ConcurrentQueue<(ChunkPos Pos, WorldModeler.RawModel Chunk)> _toFinishGeneration = new();
    private readonly ConcurrentQueue<(ChunkPos Pos, CompositeMesh Chunk)> _finished = new();

    public RealmModel(RealmWorld world)
    {
        _world = world;

        foreach (var i in 5)
        {
            var thread = new Thread(RawModelGeneratorThread)
            {
                Priority = ThreadPriority.AboveNormal,
                Name = $"Raw World Model generation #{i}",
                IsBackground = true
            };
            thread.Start();
            _modelThreads[i] = thread;
        }

        {
            var context = new GraphicContext();
            _modelFinisher = new Thread(() =>
            {
                try { context.ThreadBind(); }
                catch (Exception) { return; }
                
                try { ModelFinisherThread(); }
                finally
                {
                    context.Unbind();
                    context.Dispose();
                }
            })
            {
                Priority = ThreadPriority.AboveNormal,
                Name = "Finish World Model generation",
                IsBackground = true
            };
            _modelFinisher.Start();
        }
    }
    
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
           if(_inGeneration.Contains(position))
               return;
           _toGenerate.Enqueue(position);
           _inGeneration.Add(position);
        }
    }

    private void RawModelGeneratorThread()
    {
        while (!_token.IsCancellationRequested)
        {
            if(!_toGenerate.TryDequeue(out var toGenerate))
                Thread.Sleep(100);

            var raw = WorldModeler.GenerateRawModel(_world,
                x: new(toGenerate.X * IChunk.CHUNK_CUBE_SIZE, (toGenerate.X + 1) * IChunk.CHUNK_CUBE_SIZE),
                z: new(toGenerate.Z * IChunk.CHUNK_CUBE_SIZE, (toGenerate.Z + 1) * IChunk.CHUNK_CUBE_SIZE),
                y: new(0, IChunk.CHUNK_HEIGHT));
            
            _toFinishGeneration.Enqueue((toGenerate, raw));
        }
    }

    private void ModelFinisherThread()
    {
        while (!_token.IsCancellationRequested)
        {
            if(!_toFinishGeneration.TryDequeue(out var toFinish))
                Thread.Sleep(100);
            
            _finished.Enqueue((toFinish.Pos, toFinish.Chunk.Finish()));
        }
    }
    
    public void Update()
    {
        while (_finished.TryDequeue(out var toFinish))
        {
            lock (_lock)
            {
                _inGeneration.Remove(toFinish.Pos);
                _chunks[toFinish.Pos] = toFinish.Chunk;
                
                (Shader me, HashSet<Mesh> parts) current = (null!, null!);
                    
                foreach (var part in toFinish.Chunk)
                {
                    if (current.me != part.Shader)
                    {
                        if (_meshes.TryGetValue(part.Shader, out var set))
                            current = (part.Shader, set);
                        else
                        {
                            part.Shader.TrySetModelUniform(ModelMatrix);
                            part.Shader.TrySetViewUniform(ViewMatrix);
                            part.Shader.TrySetProjectionUniform(ProjectionMatrix);
                                    
                            current = (part.Shader, _meshes[part.Shader] = new());
                        }
                    }
                    current.parts.Add(part);
                    part.ThreadBind();
                }
            }
        }
    }

    [InlineArray(5)] private struct RawModelThreads { private Thread element; }
}