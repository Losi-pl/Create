using OpenTK.Mathematics;

namespace Create.OpenGL.GUI;

/// <summary>
/// Pechanizm renderowania emenetu interfejsu za pomocą <see cref="SpacePoint"/>
/// </summary>
public abstract class Element
{
    public abstract void Draw(Matrix4 projection, Matrix4 model, SpacePoint point);
}
