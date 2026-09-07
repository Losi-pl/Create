using System.Drawing;
using System.Numerics;
using Create.Assets;
using Create.Elements;
using Create.Graphics;
using Create.Input;
using Create.Storage;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.World;

public sealed class GameSession: Scene
{
    private readonly Camera _camera = new();
    private RealmWorld _world = null!;
    private static CompositeMesh _worldMesh = null!;
    private bool _lockedIn = true;
    
    protected override void OnConnect()
    {
        Title = "Create";
        BackgroundColor = Color.FromArgb(255, 62, 182, 204);

        _camera.ProjectionAngle = 70;
        _camera.ScreenDimensions = Size;

        _camera.Position = new(1.5f, 6, -4);
        _camera.Orientation = new(0, -40);

        {
            var stone = new PlacedBlock(Blocks.Stone);
            var bedrock = new PlacedBlock(Blocks.Bedrock);
            var dirt = new PlacedBlock(Blocks.Dirt);
            var grass = new PlacedBlock(Blocks.GrassyDirt);
            _world = new();
            foreach (var x in 4)
                foreach (var z in 4)
                {
                    _world[x, 0, z] = bedrock;
                    _world[x, 1, z] = stone;
                    _world[x, 2, z] = dirt;
                    _world[x, 3, z] = grass;
                }
        }

        _worldMesh = WorldModeler.GenerateModel(_world).ThreadBind();

        foreach (var shader in _worldMesh.GetShaders())
        {
            shader.SetProjectionUniform(_camera.ProjectionMatrix);
            shader.SetViewUniform(_camera.ViewMatrix);
            shader.SetModelUniform(Matrix4x4.CreateTranslation(-.5f, 0, -.5f));
            shader.SetUniform("atlas", BlockTexture.Atlas);
        }
        
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
        
        foreach (var shader in _worldMesh.GetShaders())
            shader.SetProjectionUniform(_camera.ProjectionMatrix);
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
        
        foreach (var shader in _worldMesh.GetShaders())
            shader.SetViewUniform(_camera.ViewMatrix);
    }
}