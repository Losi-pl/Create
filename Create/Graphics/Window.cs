using System.Diagnostics.CodeAnalysis;
using Create.General;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Create.Graphics;

/// <summary>
/// The primary window mechanic creating the window instance and providing some tools for context management
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class Window
{
    /// <summary>
    /// Ensures that when there is any context manipulation done only one can be done at a time to prevent cases where correctly aligned call can confuse the program
    /// </summary>
    private static readonly Lock _contextOperations = new();
    /// <summary>
    /// A most efficient way to store a thread local GL context avoiding use and delay of <see cref="ThreadLocal{T}"/>
    /// </summary>
    [ThreadStatic] 
    private static GL? _currentGL;
    /// <summary>
    /// A global pointer to the main window of this game
    /// </summary>
    internal static Window Main { get; } = new();

    /// <summary>
    /// A reference to the current to the current OpenGL context of this thread
    /// </summary>
    /// <exception cref="ArgumentNullException">There is no OpenGL context connected to this Thread</exception>
    internal static GL Context => _currentGL ?? throw new ArgumentNullException(nameof(Context), "There is no OpenGL context connected to this Thread");
    /// <summary>
    /// A flag specifying if there is a OpenGL context available at the moment
    /// </summary>
    public static bool HasContext => _currentGL != null;
    /// <summary>
    /// A flag for startup telling if the Run initiation of the window already happened
    /// </summary>
    private bool _initialized;
    /// <summary>
    /// The Silk.NET handler for the window mechanics
    /// </summary>
    internal IWindow InnerWindow { get; }
    /// <summary>
    /// The GL context of this specific window
    /// </summary>
    private GL MyGL { get; }
    /// <summary>
    /// The scene that is being actively used by this window
    /// </summary>
    private Scene? _usedScene;
    /// <summary>
    /// A scene to be used by this window if its changed the relevant logics are switched out during the next logic update
    /// </summary>
    public Scene? Scene { get; set; }

    /// <summary>
    /// Creates a window instance and ataches required logic to it
    /// </summary>
    private Window()
    {
        var initOptions = WindowOptions.Default with
        {
            IsVisible = false,
            VSync = false,
            Size = new(1280, 720),
            ShouldSwapAutomatically = true
        };
        InnerWindow = Silk.NET.Windowing.Window.Create(initOptions);
        InnerWindow.Initialize();
        MyGL = InnerWindow.CreateOpenGL();
        
        InnerWindow.Update += RenderUpdate;
        InnerWindow.Render += LogicUpdate;
    }

    /// <summary>
    /// Contains al logic related logic to be executed per frame
    /// </summary>
    /// <param name="delta">Time from last update</param>
    private void LogicUpdate(double delta)
    {
        if (!_initialized)
        {
            OnInit();
            _initialized = true;
        }

        if (_usedScene != Scene)
        {
            _usedScene?.Unbind();
            Scene?.Unbind();
            _usedScene = Scene;
        }
        
        _usedScene?.LogicUpdate(delta);
    }
    /// <summary>
    /// Contains all rendering related logic to be executed per frame
    /// </summary>
    /// <param name="delta">Time from last update</param>
    private void RenderUpdate(double delta)
    {
        Scene?.RenderUpdate(delta);
    }

    /// <summary>
    /// Run once during the first logic update
    /// </summary>
    private void OnInit()
    {
        InnerWindow.Center();
        InnerWindow.IsVisible = true;
    }
    /// <summary>
    /// Connects this window to a thread and sets the thread local context
    /// </summary>
    public void ThreadBind()
    {
        lock (_contextOperations)
        {
            Guard.IsNull(_currentGL, () => "A Window or a context is already bound to this thread");
            
            InnerWindow.MakeCurrent();
            _currentGL = MyGL;
        }
    }
    /// <summary>
    /// Start the logic of this window
    /// </summary>
    public void Run()
    {
        Guard.Equal(MyGL, _currentGL, (_, _) => "This window is not bound to this thread");
        InnerWindow.Run();
    }
}