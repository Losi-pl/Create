global using OpenTK.Graphics.OpenGL;
global using OpenTK.Windowing.Common;
global using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Create", AllInternalsVisible = false)]
namespace Create.OpenGL;

/// <summary>
/// Podstawowe mechanizmy modyfikacji okna
/// </summary>
public static class Engine
{
    #region variable
    internal static GameWindow window = create_window(settings);
    static object tasl_lock = new();
    static Scean? scean;
    #endregion

    /// <summary>
    /// <see cref="Matrix4"/> który nie wykonuje rzdnych zmian
    /// </summary>
    public static Matrix4 NeutralMatrix { get; } = new(
        new(1, 0, 0, 0),
        new(0, 1, 0, 0),
        new(0, 0, 1, 0),
        new(0, 0, 0, 1));

    #region game window
    /// <summary>
    /// Ustawienia okna przechowywane na wypadek potrzeby odtworzenia okna
    /// </summary>
    static window_settings settings = new();
    
    /// <summary>
    /// Tworzy okno na podstawie ustawień w <paramref name="s"/>
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    static GameWindow create_window(window_settings s)
    {
        MainTask.set_main_task();
        GameWindowSettings gws = GameWindowSettings.Default;
        gws.UpdateFrequency = 60;

        NativeWindowSettings nws = new();
        nws.API = ContextAPI.OpenGL;
        nws.StartVisible = s.IsVisible;
        nws.WindowState = s.FullScreen ? WindowState.Fullscreen : WindowState.Normal;
        nws.APIVersion = new(4, 1);
        nws.Profile = ContextProfile.Compatability;
        nws.Title = s.Title;

        settings = s;
        GameWindow gw = new(gws, nws);

        gw.Closing += w_gl_Closing;
        gw.FileDrop += w_gl_FileDrop;
        gw.FocusedChanged += w_gl_FocusedChanged;
        gw.JoystickConnected += w_gl_JoystickConnected;
        gw.KeyDown += w_gl_KeyDown;
        gw.KeyUp += w_gl_KeyUp;
        gw.Load += w_gl_Load;
        gw.Maximized += w_gl_Maximized;
        gw.Minimized += w_gl_Minimized;
        gw.MouseDown += w_gl_MouseDown;
        gw.MouseEnter += w_gl_MouseEnter;
        gw.MouseLeave += w_gl_MouseLeave;
        gw.MouseMove += w_gl_MouseMove;
        gw.MouseUp += w_gl_MouseUp;
        gw.MouseWheel += w_gl_MouseWheel;
        gw.Move += w_gl_Move;
        gw.Refresh += w_gl_Refresh;
        gw.RenderFrame += w_gl_RenderFrame;
        gw.Resize += w_gl_Resize;
        gw.TextInput += w_gl_TextInput;
        gw.Unload += w_gl_Unload;
        gw.UpdateFrame += w_gl_UpdateFrame;

        return gw;
    }
    
    /// <summary>
    /// <inheritdoc cref="settings"/>
    /// </summary>
    struct window_settings
    {
        string? title;
        bool? fullscreen;
        bool? isvisible;

        /// <summary>
        /// Nazwa okna widoczna dla użytkownika
        /// </summary>
        public string Title
        {
            get => title ?? "Create.OpenGL";
            set => title = value;
        }

        /// <summary>
        /// Czy okno jest w trybie pełno-ekranowym
        /// </summary>
        public bool FullScreen
        {
            get => fullscreen ?? false;
            set => fullscreen = value;
        }

        /// <summary>
        /// Czy okno ma zostać stworzone widoczne
        /// </summary>
        public bool IsVisible
        {
            get => isvisible ?? false;
            set => isvisible = value;
        }
    }

    #region methods
    /// <summary>
    /// <inheritdoc cref="NativeWindow.Closing"/>
    /// </summary>
    static void w_gl_Closing(CancelEventArgs args)
    {
        if (OnClosing != null)
            OnClosing?.Invoke(args);
        Scean?.m_Closing(args);
        window.IsVisible = false;
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.FileDrop"/>
    /// </summary>
    static void w_gl_FileDrop(FileDropEventArgs args)
    {
        if (OnFileDrop != null)
            OnFileDrop?.Invoke(args);
        Scean?.m_FileDrop(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.FocusedChanged"/>
    /// </summary>
    static void w_gl_FocusedChanged(FocusedChangedEventArgs args)
    {
        if (OnFocusedChanged != null)
            OnFocusedChanged?.Invoke(args);
        Scean?.m_FocusedChanged(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.JoystickConnected"/>
    /// </summary>
    static void w_gl_JoystickConnected(JoystickEventArgs args)
    {
        if (OnJoystickConnected != null)
            OnJoystickConnected?.Invoke(args);
        Scean?.m_JoystickConnected(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.KeyDown"/>
    /// </summary>
    static void w_gl_KeyDown(KeyboardKeyEventArgs args)
    {
        Input.Keyboard.KeyDown(args);
        OnKeyDown?.Invoke(args);
        Scean?.m_KeyDown(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.KeyUp"/>
    /// </summary>
    static void w_gl_KeyUp(KeyboardKeyEventArgs args)
    {
        Input.Keyboard.KeyUp(args);
        OnKeyUp?.Invoke(args);
        Scean?.m_KeyUp(args);
    }

    /// <summary>
    /// <inheritdoc cref="GameWindow.Load"/>
    /// </summary>
    static void w_gl_Load()
    {
        GL.Enable(EnableCap.DebugOutput);
        GL.DebugMessageCallback(gl_debig_method, IntPtr.Zero);

        GL.Viewport(0, 0, Size.X, Size.Y);
        GL.ClearColor(0, 0, 0, 0);
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

        OnLoad?.Invoke();
        Scean?.m_Load();
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.Maximized"/>
    /// </summary>
    static void w_gl_Maximized(MaximizedEventArgs args)
    {
        OnMaximized?.Invoke(args);
        Scean?.m_Maximized(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.Minimized"/>
    /// </summary>
    static void w_gl_Minimized(MinimizedEventArgs args)
    {
        OnMinimized?.Invoke(args);
        Scean?.m_Minimized(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseDown"/>
    /// </summary>
    static void w_gl_MouseDown(MouseButtonEventArgs args)
    {
        OnMouseDown?.Invoke(args);
        Scean?.m_MouseDown(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseEnter"/>
    /// </summary>
    static void w_gl_MouseEnter()
    {
        OnMouseEnter?.Invoke();
        Scean?.m_MouseEnter();
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseLeave"/>
    /// </summary>
    static void w_gl_MouseLeave()
    {
        OnMouseLeave?.Invoke();
        Scean?.m_MouseLeave();
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseMove"/>
    /// </summary>
    static void w_gl_MouseMove(MouseMoveEventArgs args)
    {
        Input.Mouse.mouse_move(args);
        OnMouseMove?.Invoke(args);
        Scean?.m_MouseMove(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseUp"/>
    /// </summary>
    static void w_gl_MouseUp(MouseButtonEventArgs args)
    {
        OnMouseUp?.Invoke(args);
        Scean?.m_MouseUp(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.MouseWheel"/>
    /// </summary>
    static void w_gl_MouseWheel(MouseWheelEventArgs args)
    {
        OnMouseWheel?.Invoke(args);
        Scean?.m_MouseWheel(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.Move"/>
    /// </summary>
    static void w_gl_Move(WindowPositionEventArgs args)
    {
        OnMove?.Invoke(args);
        Scean?.m_Move(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.Refresh"/>
    /// </summary>
    static void w_gl_Refresh()
    {
        OnRefresh?.Invoke();
        Scean?.m_Refresh();
    }

    /// <summary>
    /// <inheritdoc cref="GameWindow.RenderFrame"/>
    /// </summary>
    static void w_gl_RenderFrame(FrameEventArgs args)
    {
        OnRenderFrame?.Invoke(args);
        Scean?.m_RenderFrame(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.Resize"/>
    /// </summary>
    static void w_gl_Resize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
        OnResize?.Invoke(args);
        Scean?.m_Resize(args);
    }

    /// <summary>
    /// <inheritdoc cref="NativeWindow.TextInput"/>
    /// </summary>
    static void w_gl_TextInput(TextInputEventArgs args)
    {
        OnTextInput?.Invoke(args);
        Scean?.m_TextInput(args);
    }

    /// <summary>
    /// <inheritdoc cref="GameWindow.Unload"/>
    /// </summary>
    static void w_gl_Unload()
    {
        OnUnload?.Invoke();
        Scean?.m_Unload();
    }

    /// <summary>
    /// <inheritdoc cref="GameWindow.UpdateFrame"/>
    /// </summary>
    static void w_gl_UpdateFrame(FrameEventArgs args)
    {
        MainTask.make_listed_tasks();
        Input.Mouse.standard_mode(args);
        clear_memory((float)args.Time);
        Scean?.m_UpdateFrame(args);
        OnUpdateFrame?.Invoke(args);
        Input.Mouse.clear_data();
        Disposing.execute();
        Input.Keyboard.clear();
    }
    #endregion
    #endregion

    static DebugProc gl_debig_method = OnDebugMessage;

    /// <summary>
    /// Wywoływany kiedy w OpenGL wystąpi jakiś Wyjątek
    /// </summary>
    private static void OnDebugMessage(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr pMessage, IntPtr pUserParam)
    {
        string message = Marshal.PtrToStringAnsi(pMessage, length);
        if (message.Contains("GL_INVALID_ENUM"))
            return;
        if (type == DebugType.DebugTypeError)
            throw new Exception(message);
    }

    #region get only
    /// <summary>
    /// Urzywany do zamrarzania wątków pobocznych przy modyfikowaniu globalnych wartości
    /// </summary>
    public static object TaskLock => tasl_lock;
    #endregion

    #region get set
    /// <summary>
    /// Wyświetlana nazwa okna
    /// </summary>
    public static string Title
    {
        get => window.Title; 
        set
        {
            window.Title = value;
            settings.Title = value;
        }
    }
    
    /// <summary>
    /// Czy okno jest widzoczne
    /// </summary>
    public static bool Visible
    {
        get => window.IsVisible; 
        set
        {
            window.IsVisible = value;
            settings.IsVisible = value;
        }
    }
    
    /// <summary>
    /// Czy okno jest w trybie pełno ekranowym
    /// </summary>
    public static bool FullScreen
    {
        get => window.IsFullscreen; 
        set
        {
            settings.FullScreen = value;
            window.WindowState = value ? WindowState.Fullscreen : WindowState.Normal;
        }
    }
    
    /// <summary>
    /// Obecnie wyświetlana na ekranie scena
    /// </summary>
    public static Scean? Scean
    {
        get => scean;
        set
        {
            if (scean != null)
                scean.m_SceanUnload();
            scean = value;
            if (value != null)
                value.m_SceanLoad();
            GC.Collect();
        }
    }
    
    /// <summary>
    /// Wymiary okna
    /// </summary>
    public static Vector2i Size
    {
        get => window.Size;
        set => window.Size = value;
    }
    #endregion

    #region memory clear
    static float last_memory_clear = 0;
    static float? memory_clear_query = null;
    
    /// <summary>
    /// Częstotliwość wykonywania przymusowego czyszczenia pamięci
    /// </summary>
    public static float? MemoryClearFrequency
    {
        get => memory_clear_query;
        set
        {
            if (value.HasValue)
                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must by grater than 0");
            memory_clear_query = value;
        }
    }
    
    /// <summary>
    /// Mechanizm wykonywania rutynowego czyszczenia pamięci w odstępach czasowych określonych w <see cref="MemoryClearFrequency"/>
    /// </summary>
    /// <param name="time_left"></param>
    static void clear_memory(float time_left)
    {
        if (!MemoryClearFrequency.HasValue)
            return;
        last_memory_clear += time_left;
        if(last_memory_clear >= MemoryClearFrequency)
        {
            last_memory_clear -= MemoryClearFrequency.Value;
            GC.Collect();
        }
    }
    #endregion

    /// <summary>
    /// Zmienia wyświetlaną ikone okna
    /// </summary>
    /// <param name="image"></param>
    public static void SetIcon(SixLabors.ImageSharp.Image image)
    {
        var tex_buff = Textures.Texture2D.get_bytes_array(image);
        OpenTK.Windowing.Common.Input.Image im = new(image.Width, image.Height, tex_buff);
        var icon = new OpenTK.Windowing.Common.Input.WindowIcon(new[] { im });
        window.Icon = icon;
    }

    /// <summary>
    /// Włącza i wyłącza mechanizm  <paramref name="cap"/> w silniku OpenGL
    /// </summary>
    /// <param name="cap"></param>
    /// <param name="status"></param>
    internal static void SetMekanizm(EnableCap cap, bool status)
    {
        if(GL.IsEnabled(cap) != status)
        {
            if (status)
                GL.Enable(cap);
            else
                GL.Disable(cap);
        }
    }

    /// <summary>
    /// Wywoływany gdy renderowanie klatki zostało skończone i można ją wyświetlić na ekranie
    /// </summary>
    public static void FinishFrame()
    {
        window.SwapBuffers();
        GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
    }

    #region events
    #pragma warning disable CS8618
    public static event Action<CancelEventArgs> OnClosing;
    public static event Action<FileDropEventArgs> OnFileDrop;
    public static event Action<FocusedChangedEventArgs> OnFocusedChanged;
    public static event Action<JoystickEventArgs> OnJoystickConnected;
    public static event Action<KeyboardKeyEventArgs> OnKeyDown;
    public static event Action<KeyboardKeyEventArgs> OnKeyUp;
    public static event Action OnLoad;
    public static event Action<MaximizedEventArgs> OnMaximized;
    public static event Action<MinimizedEventArgs> OnMinimized;
    public static event Action<MouseButtonEventArgs> OnMouseDown;
    public static event Action OnMouseEnter;
    public static event Action OnMouseLeave;
    public static event Action<MouseMoveEventArgs> OnMouseMove;
    public static event Action<MouseButtonEventArgs> OnMouseUp;
    public static event Action<MouseWheelEventArgs> OnMouseWheel;
    public static event Action<WindowPositionEventArgs> OnMove;
    public static event Action OnRefresh;
    public static event Action<FrameEventArgs> OnRenderFrame;
    public static event Action<ResizeEventArgs> OnResize;
    public static event Action<TextInputEventArgs> OnTextInput;
    public static event Action OnUnload;
    public static event Action<FrameEventArgs> OnUpdateFrame;
    #pragma warning restore CS8618
    #endregion
}