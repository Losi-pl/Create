using Create.General;
using JetBrains.Annotations;
using Silk.NET.Core;

namespace Create.Graphics;

/// <summary>
/// A set of logic of a specific window allowing for separation of game specific mechanics from mechanics of the window itself
/// </summary>
public abstract class Scene
{
    /**A handle to the window that is currently using this <see cref="Scene"/>*/
    private Window? _window;

    /// <summary>
    /// Calls logic to facilitate binding of this <see cref="Scene"/> to the using <see cref="Window"/>
    /// </summary>
    /// <param name="window">The Caller</param>
    internal void Bind(Window window)
    {
        _window = window;
        OnConnect();
    }

    /// <summary>
    /// Calls to facilitate disconnecting of this <see cref="Scene"/> from the <see cref="Window"/>
    /// </summary>
    internal void Unbind()
    {
        OnRemove();
        _window = null;
    }
    
    /// <summary>
    /// Used when this window is connected to the Window during next Logic Update
    /// </summary>
    protected virtual void OnConnect() { }
    /// <summary>
    /// Used when this window is disconnected from the Window, called during next Logic Update
    /// </summary>
    protected virtual void OnRemove() { }
    
    /// <summary>
    /// Called every frame for logic. All rendering done on this thread will be lost.
    /// </summary>
    /// <param name="delta">Time elapsed from last update</param>
    public virtual void LogicUpdate(double delta) { }
    /// <summary>
    /// Called every frame for rendering.
    /// </summary>
    /// <param name="delta">Time elapsed from last update</param>
    public virtual void RenderUpdate(double delta) { }

    /// <summary>
    /// The title of the window
    /// </summary>
    /// <exception cref="NullReferenceException">If this Scene is not bound to a Window</exception>
    protected string Title
    {
        [UsedImplicitly] get => _window?.InnerWindow.Title ?? 
                                throw new NullReferenceException("This Scene is not connected to a window at the moment");
        set => _window?.InnerWindow.Title = value;
    }

    protected ReadOnlySpan<RawImage> Icon
    {
        set
        {
            Guard.NotNull(_window, () => "This Scene is not connected to a window at the moment");
            _window!.InnerWindow.SetWindowIcon(value);
        }
    }
}