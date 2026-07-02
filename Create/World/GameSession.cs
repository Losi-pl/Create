using System.Drawing;
using System.Numerics;
using Create.Graphics;
using Create.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.World;

public sealed class GameSession: Scene
{
    private readonly Camera _camera = new();
    private RealmWorld _world = null!;
    private static Mesh _worldMesh = null!;
    private bool _lockedIn = true;
    
    protected override void OnConnect()
    {
        Title = "Create";
        BackgroundColor = Color.FromArgb(255, 62, 182, 204);

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

        Mouse.Mode = MouseMode.LockHidden;
    }

    public override void OnKeyboardPress(Key key)
    {
        if (key is Key.X) _camera.Orientation = new(0, -45);
        if (key is not Key.Escape) return;
        _lockedIn = false;
        Mouse.Mode = MouseMode.Normal;
    }

    public override void OnMouseClick(MouseButton button, Vector2 position)
    {
        if(_lockedIn)
            return;
        _lockedIn = true;
        Mouse.Mode = MouseMode.LockHidden;
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
    
    public override void LogicUpdate(double delta)
    {
        if(!_lockedIn)
            return;
        
        var k = Keyboard;

        var forward = k.W.IsPressed;
        var back = k.S.IsPressed;
        var left = k.A.IsPressed;
        var right = k.D.IsPressed;
        var up = k.Space.IsPressed;
        var down = k.ShiftLeft.IsPressed;

        var movementHor = _camera.MovementVector(forward, back, left, right);

        Vector3 move = new(movementHor?.X ?? 0, 0, movementHor?.Y ?? 0);

        if (down != up)
        {
            const float sin45 = 0.70710678f; //Sin(45°)
            move *= sin45;
            if(up)
                move.Y = sin45;
            else if (down)
                move.Y = -sin45;
        }
        
        var mDelta = Mouse.Delta;
        _camera.View = (_camera.Position + move * (float)delta * 5f, _camera.Orientation + -mDelta / 4f);
        
        _worldMesh.Shader.SetUniform("view", _camera.ViewMatrix);
    }
}