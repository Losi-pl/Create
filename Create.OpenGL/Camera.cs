using OpenTK.Mathematics;

namespace Create.OpenGL;

public sealed class Camera
{
    Vector3 pozition;
    Vector3 rotation;

    Matrix4 projection = Engine.NeutralMatrix, model = Engine.NeutralMatrix;

    CameraAxis revert = new();

    public CameraAxis RevertAxis => revert;

    public Vector3 Pozition { get => pozition; set => pozition = value; }
    internal Quaternion RotationQuaternion => new(rotation / 180f * MathF.PI);
    public Vector3 Rotation { get => rotation; set => rotation = value; }
    public Matrix4 Projection { get => projection; set => projection = value; }
    public Matrix4 Model { get => model; set => model = value; }

    public class CameraAxis
    {
        (bool x, bool y, bool z) asixs;
        public bool x{ get => asixs.x; set => asixs.x = value; }
        public bool y{ get => asixs.y; set => asixs.y = value; }
        public bool z{ get => asixs.z; set => asixs.z = value; }
    }
}
