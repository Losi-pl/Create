using OpenTK.Mathematics;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace Create.OpenGL.GUI;

/// <summary>
/// Pechanizm renderowania emenetu interfejsu za pomocą <see cref="SpacePoint"/>
/// </summary>
public abstract class Element
{
    WeakReference<SpacePoint>? point;
    public abstract void Draw(Matrix4 projection);

    internal void set_element(SpacePoint? point) => this.point = (point is null ? null : new(point));

    protected internal virtual void Bind(SpacePoint point) { }
    protected internal virtual void Unbind(SpacePoint point) { }
    protected internal virtual void OnSizeChanget((float Width, float Height) old, (float Width, float Height) @new) { }
    protected internal virtual void OnPozitionChanget((float Width, float Height) old, (float Width, float Height) @new) { }

    /// <summary>
    /// <see cref="SpacePoint"/> z którym ten <see cref="Element"/> jest połączony, jeżeli jest pusty nie jest połączony z niczym
    /// </summary>
    public SpacePoint? Point => (point?.TryGetTarget(out var p) ?? false) ? p : null;

}

[RequiresPreviewFeatures]
public interface IElementLoading<T>
{
    internal static abstract T Parse(XElement element);
    internal static abstract void ChangeEvent(T point, object sender);
    internal static abstract object ChangeEventParameter(XElement element);
}