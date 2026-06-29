using System.Numerics;

namespace Create.Graphics;

public class Camera
{
    public Vector2 Orientation
    {
        get;
        set
        {
            if(field == value)
                return;
            field = value;
            RecalculateView();
        }
    }
    
    public Vector3 Position
    {
        get;
        set
        {
            if(field == value)
                return;
            field = value;
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