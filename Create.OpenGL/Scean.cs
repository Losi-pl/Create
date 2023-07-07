using OpenTK.Windowing.Common;
using System.ComponentModel;

namespace Create.OpenGL;

public abstract class Scean
{
    /// <summary>
    /// Wywoływana gdy okno gry jest zamykane
    /// </summary>
    /// <param name="args"></param>
    protected virtual void Closing(CancelEventArgs args) { }
    internal Action<CancelEventArgs> m_Closing => Closing;

    /// <summary>
    /// Wywoływany gdy plik zostanie upuszczony za pomocą mechanizmu Drag &amp; Drop
    /// </summary>
    /// <param name="args"></param>
    protected virtual void FileDrop(FileDropEventArgs args) { }
    internal Action<FileDropEventArgs> m_FileDrop => FileDrop;

    /// <summary>
    /// Gdy okno z fokusem zostało zmienione
    /// </summary>
    /// <param name="args"></param>
    protected virtual void FocusedChanged(FocusedChangedEventArgs args) { }
    internal Action<FocusedChangedEventArgs> m_FocusedChanged => FocusedChanged;

    /// <summary>
    /// Gdy joystick został podłączony
    /// </summary>
    /// <param name="args"></param>
    protected virtual void JoystickConnected(JoystickEventArgs args) { }
    internal Action<JoystickEventArgs> m_JoystickConnected => JoystickConnected;

    /// <summary>
    /// Gdy przycisk został wciśnięty
    /// </summary>
    /// <param name="args"></param>
    protected virtual void KeyDown(KeyboardKeyEventArgs args) { }
    internal Action<KeyboardKeyEventArgs> m_KeyDown => KeyDown;

    /// <summary>
    /// Gdy przycisko został puszczony
    /// </summary>
    /// <param name="args"></param>
    protected virtual void KeyUp(KeyboardKeyEventArgs args) { }
    internal Action<KeyboardKeyEventArgs> m_KeyUp => KeyUp;

    /// <summary>
    /// Gdy okno zostało załadowane
    /// </summary>
    protected virtual void Load() { }
    internal Action m_Load => Load;

    /// <summary>
    /// Gdy okno zostało zmaksymalizowane
    /// </summary>
    /// <param name="args"></param>
    protected virtual void Maximized(MaximizedEventArgs args) { }
    internal Action<MaximizedEventArgs> m_Maximized => Maximized;

    /// <summary>
    /// Gdy okno zostało zminimalizowane
    /// </summary>
    /// <param name="args"></param>
    protected virtual void Minimized(MinimizedEventArgs args) { }
    internal Action<MinimizedEventArgs> m_Minimized => Minimized;

    /// <summary>
    /// Gdzy przycisk na myszcze został wciśnięty
    /// </summary>
    /// <param name="args"></param>
    protected virtual void MouseDown(MouseButtonEventArgs args) { }
    internal Action<MouseButtonEventArgs> m_MouseDown => MouseDown;

    /// <summary>
    /// Gdy myszka wjechała na obszar okna
    /// </summary>
    protected virtual void MouseEnter() { }
    internal Action m_MouseEnter => MouseEnter;

    /// <summary>
    /// Gdzy myszka opuszcza obszar okna
    /// </summary>
    protected virtual void MouseLeave() { }
    internal Action m_MouseLeave => MouseLeave;

    /// <summary>
    /// Gdy myszka rusza się po obszarze okna
    /// </summary>
    /// <param name="args"></param>
    protected virtual void MouseMove(MouseMoveEventArgs args) { }
    internal Action<MouseMoveEventArgs> m_MouseMove => MouseMove;

    /// <summary>
    /// Gdy przycisk na myrzce zostanie puszczony
    /// </summary>
    /// <param name="args"></param>
    protected virtual void MouseUp(MouseButtonEventArgs args) { }
    internal Action<MouseButtonEventArgs> m_MouseUp => MouseUp;

    /// <summary>
    /// Gdy skrol myszki zostanie urzyty
    /// </summary>
    /// <param name="args"></param>
    protected virtual void MouseWheel(MouseWheelEventArgs args) { }
    internal Action<MouseWheelEventArgs> m_MouseWheel => MouseWheel;

    /// <summary>
    /// Gdy pozycja okna na ekranie zostanie zmieniona
    /// </summary>
    /// <param name="args"></param>
    protected virtual void Move(WindowPositionEventArgs args) { }
    internal Action<WindowPositionEventArgs> m_Move => Move;

    /// <summary>
    /// Gdy okno zostanie odświerzone
    /// </summary>
    protected virtual void Refresh() { }
    internal Action m_Refresh => Refresh;

    /// <summary>
    /// Gdy obraz na ekranie jest odświerzany
    /// </summary>
    /// <param name="args"></param>
    protected virtual void RenderFrame(FrameEventArgs args) { }
    internal Action<FrameEventArgs> m_RenderFrame => RenderFrame;

    /// <summary>
    /// Gdy wontek poboczny do renderowania obrazu został stworzony
    /// </summary>
    protected virtual void RenderThreadStarted() { }
    internal Action m_RenderThreadStarted => RenderThreadStarted;

    /// <summary>
    /// Gdy rozmiar okna gry został zmieniony
    /// </summary>
    /// <param name="args"></param>
    protected virtual void Resize(ResizeEventArgs args) { }
    internal Action<ResizeEventArgs> m_Resize => Resize;

    /// <summary>
    /// Gdy litera w kodowaniu Unicode zostanie wpisana
    /// </summary>
    /// <param name="args"></param>
    protected virtual void TextInput(TextInputEventArgs args) { }
    internal Action<TextInputEventArgs> m_TextInput => TextInput;

    /// <summary>
    /// Wykonywana gdy onko jest niszczone
    /// <para>np. <c>zniszczone z Menedżera Zadań</c> / <c>zamknięte przez użytkownika</c></para>
    /// </summary>
    protected virtual void Unload() { }
    internal Action m_Unload => Unload;

    /// <summary>
    /// Gdy obliczenia w tle są wykonywane
    /// <para>Nie związana z renderowaniem obrazu / dźwięku</para>
    /// </summary>
    /// <param name="args"></param>
    protected virtual void UpdateFrame(FrameEventArgs args) { }
    internal Action<FrameEventArgs> m_UpdateFrame => UpdateFrame;

    /// <summary>
    /// Gdy ta scena zostanie ustawiona na obecnie używaną
    /// </summary>
    protected virtual void SceanLoad() { }
    internal Action m_SceanLoad => SceanLoad;

    /// <summary>
    /// Gdy ta scene zostanie zmieniona na inną
    /// </summary>
    protected virtual void SceanUnload() { }
    internal Action m_SceanUnload => SceanUnload;
}
