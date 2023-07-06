using Create.OpenGL;

namespace Create.Render;

/// <summary>
/// Podstawa mechanizmu generowania elementu modelu terenu
/// </summary>
public abstract class ChunkModel
{
    /// <summary>
    /// Wywoływane gdy dane modelu zostały zebrane i można złożyć je do kupy
    /// </summary>
    /// <returns></returns>
    public abstract Mesh FinischModel();
}
