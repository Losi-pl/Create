using System.Numerics;

namespace Create.Graphics;

public class Camera
{
    private Vector2 _orie;
    private Vector3 _pos;
    
    public (Vector3 position, Vector2 orientation) View
    {
        get => (Position, Orientation);
        set
        {
            if (View == value)
                return;
            _pos = value.position;
            _orie = value.orientation;
            
            RecalculateView();
        }
    }
    
    public Vector2 Orientation
    {
        get => _orie;
        set
        {
            if(_orie == value)
                return;
            _orie = value;
            RecalculateView();
        }
    }
    
    public Vector3 Position
    {
        get => _pos;
        set
        {
            if(_pos == value)
                return;
            _pos = value;
            RecalculateView();
        }
    }
    
    public float ProjectionAngle
    {
        get;
        set
        {
            field = value;
            RecalculateProjection();
        }
    }

    public Silk.NET.Maths.Vector2D<int> ScreenDimensions
    {
        get;
        set
        {
            if(field == value)
                return;
            field = value;
            RecalculateProjection();
        }
    }

    public Vector2? MovementVector(bool forward, bool back, bool left, bool right)
    {
        float? d = null;
        
        if ((forward || left || right || back) && (!forward || !left || !back || !right))
        {
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
        }

        if (!d.HasValue)
            return null;
        d = (d.Value / 4f + Orientation.X / -180f) * MathF.PI;
        return new(MathF.Sin(d.Value), MathF.Cos(d.Value));
    }
    
    public Matrix4x4 ProjectionMatrix { get; private set; } = Matrix4x4.Identity;
    public Matrix4x4 ViewMatrix { get; private set; } = Matrix4x4.Identity;

    private void RecalculateView()
    {
        var mat = Matrix4x4.CreateTranslation(-Position.X, -Position.Y, -Position.Z);
        if (Orientation != new Vector2(0, 0))
            mat *= Matrix4x4.CreateRotationY(Orientation.X * MathF.PI / 180f) * Matrix4x4.CreateRotationX(Orientation.Y * MathF.PI / 180f);
        ViewMatrix = mat;
    }
    private void RecalculateProjection()
    {
        ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(ProjectionAngle * MathF.PI / 180f, 
            ScreenDimensions.X / (float)ScreenDimensions.Y, .1f, 512f * 3 / 2);
    }
}