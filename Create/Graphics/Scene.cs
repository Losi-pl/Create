using System.Numerics;
using Create.Input;
using JetBrains.Annotations;
using Silk.NET.Core;
using Silk.NET.Maths;
// ReSharper disable VirtualMemberNeverOverridden.Global

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
    /// Called when the window is resized
    /// </summary>
    public virtual void WindowResize(Vector2D<int> newSize) { }

    /// <summary>
    /// Called when a key is pressed
    /// </summary>
    public virtual void OnKeyboardPress(Key key) { }
    
    /// <summary>
    /// Called when a key is released
    /// </summary>
    public virtual void OnKeyboardRelease(Key key) { }
    
    /// <summary>
    /// Called when a button on the mouse is pressed
    /// </summary>
    public virtual void OnMousePress(MouseButton button) { }
    
    /// <summary>
    /// Called when a button on the mouse is pressed
    /// </summary>
    public virtual void OnMouseRelease(MouseButton button) { }
    
    /// <summary>
    /// Called when a click even is registered on the mouse
    /// </summary>
    public virtual void OnMouseClick(MouseButton button, Vector2 position) { }
    
    /// <summary>
    /// Called when a click even is registered on the mouse
    /// </summary>
    /// <remarks>Is exclusive with <see cref="OnMouseDoubleClick"/> and will only be called if it's certain tha the prior method will not be</remarks>
    public virtual void OnMouseExclusiveClick(MouseButton button, Vector2 position) { }
    
    /// <summary>
    /// Called when a button on the mouse is pressed
    /// </summary>
    public virtual void OnMouseDoubleClick(MouseButton button, Vector2 position) { }
    
    /// <summary>
    /// Allows to change the scene currently used in the window
    /// </summary>
    /// <remarks>Change will be only applied during the next logic update</remarks>
    /// <param name="scene">Target To swap to</param>
    protected void SwapScene(Scene scene) => _window!.Scene = scene;
    
    /// <summary>
    /// Keyboard inputs of the window
    /// </summary>
    protected Keyboard Keyboard => _window!.Keyboard;
    
    /// <summary>
    /// Mouse inputs of the window
    /// </summary>
    protected Mouse Mouse => _window!.Mouse;
    
    /// <summary>
    /// The title of the window
    /// </summary>
    /// <exception cref="NullReferenceException">If this Scene is not bound to a Window</exception>
    protected string Title
    {
        [UsedImplicitly] get => _window?.MeGLFW.Title ?? 
                                throw new NullReferenceException("This Scene is not connected to a window at the moment");
        set => _window?.MeGLFW.Title = value;
    }

    /// <summary>
    /// The size of the game window in buffer pixels and excluding window border
    /// </summary>
    protected Vector2D<int> Size
    {
        get => _window!.MeGLFW.FramebufferSize;
        set
        {
            var buff = _window!.MeGLFW.FramebufferSize;
            var logical = _window!.MeGLFW.Size;

            var scaleW = buff.X / (decimal)logical.X;
            var scaleH = buff.Y / (decimal)logical.Y;
            
            _window!.MeGLFW.Size = new((int)(value.X * scaleW), (int)(value.Y * scaleH));
        }
    }
    
    /// <summary>
    /// Used to set the Icon for the window
    /// </summary>
    protected ReadOnlySpan<RawImage> Icon
    {
        set
        {
            Guard.NotNull(_window, () => "This Scene is not connected to a window at the moment");
            _window!.MeGLFW.SetWindowIcon(value);
        }
    }
}