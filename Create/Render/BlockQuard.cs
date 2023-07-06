using OpenTK.Mathematics;

namespace Create.Render;

/// <summary>
/// Podstawa elementu modelu terenu
/// </summary>
public class BlockQuard
{
    Vector2[] uvs;
    Vector3[] pozitions;

    public BlockQuard()
    {
        uvs = Array.Empty<Vector2>();
        pozitions = Array.Empty<Vector3>();
    }
}
