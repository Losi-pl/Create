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

        if ((forward || left || right || back) && (!forward || !left || !back || !right))
        {
            var d = -1f;

            if (forward && !back)
            {
                if (left == right)
                    d = 0;
                else if (left)
                    d = 7;
                else if (right)
                    d = 1;
            }
            else if (back && !forward)
            {
                if (left == right)
                    d = 4;
                else if (left)
                    d = 5;
                else if (right)
                    d = 3;
            }
            else if (left && !right)
                d = 6;
            else if (right && !left)
                d = 2;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (d != -1)
            {
                d = d * MathF.PI / 4;
                var move = new Vector3(MathF.Sin(d), 0, MathF.Cos(d));
                {
                
                    if (up && !down)
                        move.Y = 1;
                    if (down && !up)
                        move.Y = -1;
                    if (down != up)
                    {
                        var rad = MathF.PI / 4;
                        move *= MathF.Sin(rad);
                    }
                }
            
                _camera.Position += move * (float)delta * 5f;
            
                _worldMesh.Shader.SetUniform("view", _camera.ViewMatrix);
            }
        }
        else if(up != down)
        {
            if (up)
                _camera.Position += new Vector3(0, (float)delta * 5f, 0);
            if (down)
                _camera.Position += new Vector3(0, -(float)delta * 5f, 0);
            
            _worldMesh.Shader.SetUniform("view", _camera.ViewMatrix);
        }

        var mDelta = Mouse.Delta;
        Console.WriteLine($"X: {mDelta.X}, Y: {mDelta.Y}");
        _camera.Orientation += -mDelta / 4f;
        _worldMesh.Shader.SetUniform("view", _camera.ViewMatrix);
    }
}