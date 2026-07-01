using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Create.Graphics;

public class GraphicContext: IDisposable
{
    private readonly IWindow _window;
    private GL _glContext;

    public GraphicContext()
    {
        var initOptions = WindowOptions.Default with
        {
            IsVisible = false,
            VSync = false,
            Size = new(1, 1), 
            API = new GraphicsAPI
            {
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.ForwardCompatible,
                Version = new APIVersion(4, 6)
            },
            SharedContext = Window.Main.MeGLFW.GLContext
        };
        if (Window.IsMainThread)
        {
            Window.Main.MeGLFW.ClearContext();
            try
            {
                _window = Silk.NET.Windowing.Window.Create(initOptions);
                _window.Initialize();
                _glContext = _window.CreateOpenGL();
            }
            finally { Window.Main.MeGLFW.MakeCurrent(); }
        }
        else
        {
            (_window, _glContext) = Window.Query(() =>
            {
                Window.Main.MeGLFW.ClearContext();
                try
                {
                    var win = Silk.NET.Windowing.Window.Create(initOptions);
                    win.Initialize();
                    return (win, win.CreateOpenGL());
                }
                finally { Window.Main.MeGLFW.MakeCurrent(); }
            }).Result;
        }
    }
    
    public void ThreadBind()
    {
        lock (Window.ContextOperations)
        {
            if(Window.HasGL)
                throw new Exception("A Window or a context is already bound to this thread");
            
            _window.MakeCurrent();
            Window.GL = _glContext;
        }
    }

    public void Unbind()
    {
        lock (Window.ContextOperations)
        {
            if(_glContext is null)
                throw new Exception("This Context is not bound anywhere");
            if(!Window.HasGL || Window.GL != _glContext)
                throw new Exception("This Context is not bound on this thread");
            Window.GL = _glContext = null!;
            _window.ClearContext();
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public bool IsDisposed { get; private set; }
    public void Dispose()
    {
        if (IsDisposed) return;
        IsDisposed = true;

        GC.SuppressFinalize(this);

        _window.Close();
        _window.Dispose();

        _glContext = null!;
    }

    ~GraphicContext() => Dispose();
}