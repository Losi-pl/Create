using OpenTK.Mathematics;

namespace Create.OpenGL;

/// <summary>
/// Kamera definjująca punk w przestrzeni który ma być traktowany jako obecna pozycja z której obiekty mają być widoczne na ekranie
/// </summary>
public sealed class Camera
{
    Vector3 pozition;
    Vector3 rotation;

    Matrix4 projection = Engine.NeutralMatrix, model = Engine.NeutralMatrix;

    CameraAxis revert = new();

    /// <summary>
    /// Definjuje czy poszczegulne osie orjentacji kamery w przestrzeni są odwrucone
    /// </summary>
    public CameraAxis RevertAxis => revert;

    /// <summary>
    /// Pozycja kamery w świecie
    /// </summary>
    public Vector3 Pozition { get => pozition; set => pozition = value; }

    /// <summary>
    /// Orjentacja kamery w systemie <see cref="Quaternion"/> wygenerowany z <see cref="Rotation"/>
    /// </summary>
    internal Quaternion RotationQuaternion => new((rotation * new Vector3(-1, 1, 1)) / 180f * MathF.PI);

    /// <summary>
    /// Orjentacja kamery w systemie <see cref="Vector3"/>
    /// </summary>
    public Vector3 Rotation { get => rotation; set => rotation = value; }

    /// <summary>
    /// Dodatkowy <see cref="Matrix4"/> nakładany na całość po transformacji poszczegulnych obiektów
    /// </summary>
    public Matrix4 Projection { get => projection; set => projection = value; }

    /// <summary>
    /// Dodatkowy <see cref="Matrix4"/> nakładany na poszczegulne model przed innymi transformacjami
    /// </summary>
    public Matrix4 Model { get => model; set => model = value; }

    /// <summary><inheritdoc cref="RevertAxis"/>
    /// </summary>
    public class CameraAxis
    {
        (bool x, bool y, bool z) asixs;
        public bool x{ get => asixs.x; set => asixs.x = value; }
        public bool y{ get => asixs.y; set => asixs.y = value; }
        public bool z{ get => asixs.z; set => asixs.z = value; }
    }
}
