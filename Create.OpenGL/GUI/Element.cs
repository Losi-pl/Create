using OpenTK.Mathematics;

namespace Create.OpenGL.GUI;

/// <summary>
/// Pechanizm renderowania emenetu interfejsu za pomocą <see cref="SpacePoint"/>
/// </summary>
public abstract class Element
{
    SpacePoint point;
    public abstract void Draw(Matrix4 projection);

    internal void set_element(SpacePoint point) => this.point = point;

    protected internal virtual void Bind(SpacePoint point) { }
    protected internal virtual void Unbind(SpacePoint point) { }

    /// <summary>
    /// <see cref="SpacePoint"/> z którym ten <see cref="Element"/> jest połączony
    /// </summary>
    public SpacePoint Point => point;
}
