using System.Drawing;
using System.Numerics;
using Create.Assets;
using Create.Graphics;
using Create.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.World;

public sealed class GameSession: Scene
{
    private readonly Camera _camera = new();
    private RealmWorld _world = null!;
    private static RealmModel _worldModel = null!;
    private bool _lockedIn = true;

    private static readonly ChunkPos[] Circle = Enumerable.Range(-12, 25)
        .SelectMany(x => Enumerable.Range(-12, 25).Select(z => new ChunkPos(x, z)))
        .Where(pos => (pos.X * pos.X) + (pos.Z * pos.Z) <= 10 * 10)
        .ToArray().Apply(array =>
        {
            // Sort the full circle in order of closest to farthest
            array.Sort((a, b) =>
            {
                var aD = (a.X * a.X) + (a.Z * a.Z);
                var bD = (b.X * b.X) + (b.Z * b.Z);
                return aD.CompareTo(bD);
            });
        });
    
    protected override void OnConnect()
    {
        // Set environment
        Title = "Create";
        BackgroundColor = Color.FromArgb(255, 62, 182, 204);

        // Set camera settings
        _camera.ProjectionAngle = 70;
        _camera.ScreenDimensions = Size;

        // Set camera position
        _camera.Position = new(1.5f, 6, -4);
        _camera.Orientation = new(0, -40);

        // Create world and its chunks
        _world = new();
        foreach (var pos in Circle)
            _world.CreateChunk(pos);

        // Create world model and task chunk model generations
        _worldModel = new(_world);
        foreach (var pos in Circle)
            _worldModel.AddChunkToModel(pos);
        
        // Set rendering matrices
        _worldModel.ProjectionMatrix = _camera.ProjectionMatrix;
        _worldModel.ViewMatrix = _camera.ViewMatrix;
        _worldModel.ModelMatrix = Matrix4x4.CreateTranslation(-.5f, 0, -.5f);
        
        // TODO: Move into Shader logic
        Window.GL.Enable(EnableCap.DepthTest);

        // Center lock cursor
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
        _worldModel.ProjectionMatrix = _camera.ProjectionMatrix;
    }
    
    public override void RenderUpdate(double delta)
    {
        _worldModel.Draw();
    }
    
    public override void LogicUpdate(double delta)
    {
        _worldModel.Update();
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

        _worldModel.ViewMatrix = _camera.ViewMatrix;
    }
}