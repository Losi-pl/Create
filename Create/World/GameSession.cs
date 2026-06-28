using System.Numerics;
using Create.Graphics;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.World;

public sealed class GameSession: Scene
{
    private RealmWorld _world = null!;
    private static Mesh _worldMesh = null!;
    
    protected override void OnConnect()
    {
        Title = "Create";
        
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

        var fovRadians = 70f * MathF.PI / 180f;
        var aspect = Size.X / (float)Size.Y;

        var projection = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(fovRadians, aspect, .1f, 512f * 3 / 2);
        var view = Matrix4x4.CreateLookAtLeftHanded(new(0, 5, -5), new(0, 0, 0), new(0, 1, 0));

        _worldMesh.Shader.SetUniform("projection", projection);
        _worldMesh.Shader.SetUniform("view", view);
        _worldMesh.Shader.SetUniform("model", Matrix4X4<float>.Identity);
        
        Window.GL.Enable(EnableCap.DepthTest);
    }
    
    public override void WindowResize(Vector2D<int> newSize)
    {
        var fovRadians = 70f * MathF.PI / 180f;
        var aspect = Size.X / (float)Size.Y;

        var projection = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(fovRadians, aspect, .1f, 512f * 3 / 2);
        
        _worldMesh.Shader.SetUniform("projection", projection);
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