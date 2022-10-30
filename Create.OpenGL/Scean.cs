using OpenTK.Windowing.Common;
using System.ComponentModel;

namespace Create.OpenGL;

public abstract class Scean
{
    protected virtual void Closing(CancelEventArgs args) { }
    internal Action<CancelEventArgs> m_Closing => Closing;
    protected virtual void FileDrop(FileDropEventArgs args) { }
    internal Action<FileDropEventArgs> m_FileDrop => FileDrop;
    protected virtual void FocusedChanged(FocusedChangedEventArgs args) { }
    internal Action<FocusedChangedEventArgs> m_FocusedChanged => FocusedChanged;
    protected virtual void JoystickConnected(JoystickEventArgs args) { }
    internal Action<JoystickEventArgs> m_JoystickConnected => JoystickConnected;
    protected virtual void KeyDown(KeyboardKeyEventArgs args) { }
    internal Action<KeyboardKeyEventArgs> m_KeyDown => KeyDown;
    protected virtual void KeyUp(KeyboardKeyEventArgs args) { }
    internal Action<KeyboardKeyEventArgs> m_KeyUp => KeyUp;
    protected virtual void Load() { }
    internal Action m_Load => Load;
    protected virtual void Maximized(MaximizedEventArgs args) { }
    internal Action<MaximizedEventArgs> m_Maximized => Maximized;
    protected virtual void Minimized(MinimizedEventArgs args) { }
    internal Action<MinimizedEventArgs> m_Minimized => Minimized;
    protected virtual void MouseDown(MouseButtonEventArgs args) { }
    internal Action<MouseButtonEventArgs> m_MouseDown => MouseDown;
    protected virtual void MouseEnter() { }
    internal Action m_MouseEnter => MouseEnter;
    protected virtual void MouseLeave() { }
    internal Action m_MouseLeave => MouseLeave;
    protected virtual void MouseMove(MouseMoveEventArgs args) { }
    internal Action<MouseMoveEventArgs> m_MouseMove => MouseMove;
    protected virtual void MouseUp(MouseButtonEventArgs args) { }
    internal Action<MouseButtonEventArgs> m_MouseUp => MouseUp;
    protected virtual void MouseWheel(MouseWheelEventArgs args) { }
    internal Action<MouseWheelEventArgs> m_MouseWheel => MouseWheel;
    protected virtual void Move(WindowPositionEventArgs args) { }
    internal Action<WindowPositionEventArgs> m_Move => Move;
    protected virtual void Refresh() { }
    internal Action m_Refresh => Refresh;
    protected virtual void RenderFrame(FrameEventArgs args) { }
    internal Action<FrameEventArgs> m_RenderFrame => RenderFrame;
    protected virtual void RenderThreadStarted() { }
    internal Action m_RenderThreadStarted => RenderThreadStarted;
    protected virtual void Resize(ResizeEventArgs args) { }
    internal Action<ResizeEventArgs> m_Resize => Resize;
    protected virtual void TextInput(TextInputEventArgs args) { }
    internal Action<TextInputEventArgs> m_TextInput => TextInput;
    protected virtual void Unload() { }
    internal Action m_Unload => Unload;
    protected virtual void UpdateFrame(FrameEventArgs args) { }
    internal Action<FrameEventArgs> m_UpdateFrame => UpdateFrame;
    protected virtual void SceanLoad() { }
    internal Action m_SceanLoad => SceanLoad;
    protected virtual void SceanUnload() { }
    internal Action m_SceanUnload => SceanUnload;
}
