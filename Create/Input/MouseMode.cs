namespace Create.Input;

public enum MouseMode
{
    /// Cursor is visible and has no restrictions on mobility.
    Normal,
    /// Cursor is invisible, and has no restrictions on mobility.
    Hidden,
    /// Cursor is invisible, and is restricted to the center of the screen. Mouse motion is not scaled.
    LockHidden
}