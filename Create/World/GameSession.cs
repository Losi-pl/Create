using System.Numerics;
using Create.Graphics;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.World;

public sealed class GameSession: Scene
{
    private readonly Camera _camera = new();
    private RealmWorld _world = null!;
    private static Mesh _worldMesh = null!;
    
    protected override void OnConnect()
    {
        Title = "Create";

        _camera.ProjectionAngle = 70;
        _camera.ScreenDimensions = Size;

        _camera.Position = new(0, 3, -3);
        _camera.Orientation = new(0, -45);
        
        _world = new()
        {
            [0, 0, 0] = true,
            [0, 0, 1] = true,
            [1, 0, 0] = true,
            [1, 0, 1] = true,
            [0, 1, 0] = true,
            [1, 1, 1] = true
        };

        _worldMesh = new ChunkModeler().GenerateModel(_world).ThreadBind();

        _worldMesh.Shader.SetUniform("projection", _camera.ProjectionMatrix);
        _worldMesh.Shader.SetUniform("view", _camera.ViewMatrix);
        _worldMesh.Shader.SetUniform("model", Matrix4x4.CreateTranslation(-.5f, 0, -.5f));
        
        Window.GL.Enable(EnableCap.DepthTest);
    }
    
    public override void WindowResize(Vector2D<int> newSize)
    {
        _camera.ScreenDimensions = newSize;
        _worldMesh.Shader.SetUniform("projection", _camera.ProjectionMatrix);
    }
    
    public override void RenderUpdate(double delta)
    {
        _worldMesh.Draw();
    }
    
    private double _rotate;
    public override void LogicUpdate(double delta)
    {
        _rotate += delta * 90;
        var mod = Matrix4x4.CreateTranslation(-1, -1, -1) * Matrix4x4.CreateRotationY((float)_rotate * (MathF.PI / 180f));
        _worldMesh.Shader.SetUniform("model", mod);
    }
}