using System.Diagnostics.CodeAnalysis;
using Create.General;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ARB;
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
    /// Stores an OpenGL extension for support of Int 64-bit (Long) variables
    /// </summary>
    [ThreadStatic] 
    private static ArbGpuShaderInt64? _contextGLInt64;
    /// <summary>
    /// A global pointer to the main window of this game
    /// </summary>
    internal static Window Main { get; } = new();

    /// <summary>
    /// A reference to the current to the current OpenGL context of this thread
    /// </summary>
    /// <exception cref="ArgumentNullException">There is no OpenGL context connected to this Thread</exception>
    internal static GL GL => _currentGL ?? throw new ArgumentNullException(nameof(GL), "There is no OpenGL context connected to this Thread");

    /// <summary>
    /// A reference to the current to the current OpenGL context of this thread
    ///
    /// Extension for Long (Int 64-bit)
    /// </summary>
    /// <exception cref="ArgumentNullException">There is no OpenGL context connected to this Thread</exception>
    internal static ArbGpuShaderInt64 GLong => _contextGLInt64 ?? (GL.TryGetExtension(out ArbGpuShaderInt64 context) ? 
        _contextGLInt64 = context : throw new Exception("The Int64 extension for OpenGL is not supported on this device"));
    /// <summary>
    /// A flag specifying if there is a OpenGL context available at the moment
    /// </summary>
    internal static bool HasGL => _currentGL != null;
    /// <summary>
    /// A flag for startup telling if the Run initiation of the window already happened
    /// </summary>
    private bool _initialized;
    /// <summary>
    /// The Silk.NET handler for the window mechanics
    /// </summary>
    internal IWindow MeGLFW { get; }
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
    /// Creates a window instance and attaches required logic to it
    /// </summary>
    private Window()
    {
        var initOptions = WindowOptions.Default with
        {
            IsVisible = false,
            VSync = false,
            Size = new(1280, 720), 
            API = new GraphicsAPI
            {
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.ForwardCompatible,
                Version = new APIVersion(4, 6)
            },
            FramesPerSecond = 60
        };
        MeGLFW = Silk.NET.Windowing.Window.Create(initOptions);
        MeGLFW.Initialize();
        MyGL = MeGLFW.CreateOpenGL();
        
        MeGLFW.Update += RenderUpdate;
        MeGLFW.Render += LogicUpdate;
        MeGLFW.Resize += WindowResize;
    }

    /// <summary>
    /// Contains all logic to be run then the window is being resized
    /// </summary>
    /// <param name="newSize"></param>
    private void WindowResize(Vector2D<int> newSize)
    {
        MyGL.Viewport(new(0, 0), newSize);
        _usedScene?.WindowResize(newSize);
    }
    
    /// <summary>
    /// Contains al logic related logic to be executed per frame
    /// </summary>
    /// <param name="delta">Time from last update</param>
    private void LogicUpdate(double delta)
    {
        if (_usedScene != Scene)
        {
            _usedScene?.Unbind();
            Scene?.Bind(this);
            _usedScene = Scene;
        }
        
        if (!_initialized)
        {
            OnInit();
            _initialized = true;
        }
        
        _usedScene?.LogicUpdate(delta);
    }
    /// <summary>
    /// Contains all rendering related logic to be executed per frame
    /// </summary>
    /// <param name="delta">Time from last update</param>
    private void RenderUpdate(double delta)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _usedScene?.RenderUpdate(delta);
    }

    /// <summary>
    /// Run once during the first logic update
    /// </summary>
    private void OnInit()
    {
        MeGLFW.Center();
        MeGLFW.IsVisible = true;
    }
    /// <summary>
    /// Connects this window to a thread and sets the thread local context
    /// </summary>
    public void ThreadBind()
    {
        lock (_contextOperations)
        {
            Guard.IsNull(_currentGL, () => "A Window or a context is already bound to this thread");
            
            MeGLFW.MakeCurrent();
            _currentGL = MyGL;
        }
    }
    /// <summary>
    /// Start the logic of this window
    /// </summary>
    public void Run()
    {
        Guard.Equal(MyGL, _currentGL, (_, _) => "This window is not bound to this thread");
        MeGLFW.Run();
    }
}