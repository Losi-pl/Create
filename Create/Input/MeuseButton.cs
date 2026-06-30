using SilkButton = Silk.NET.Input.MouseButton;

namespace Create.Input;

/// <summary>
/// Represents the indices of the mouse buttons.
/// </summary>
/// <remarks>
/// <para>
/// The number of buttons provided depends on the input backend currently being used.
/// </para>
/// </remarks>
public enum MouseButton
{
    /// <summary>
    /// Indicates the input backend was unable to determine a button name for the button in question, or it does not support it.
    /// </summary>
    Unknown = SilkButton.Unknown,

    /// <summary>
    /// The left mouse button.
    /// </summary>
    Left = SilkButton.Left,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right = SilkButton.Right,
    /// <summary>
    /// The middle mouse button.
    /// </summary>
    Middle = SilkButton.Middle,

    /// <summary>
    /// The fourth mouse button.
    /// </summary>
    Button4 = SilkButton.Button4,

    /// <summary>
    /// The fifth mouse button.
    /// </summary>
    Button5 = SilkButton.Button5,

    /// <summary>
    /// The sixth mouse button.
    /// </summary>
    Button6 = SilkButton.Button6,

    /// <summary>
    /// The seventh mouse button.
    /// </summary>
    Button7 = SilkButton.Button7,

    /// <summary>
    /// The eighth mouse button.
    /// </summary>
    Button8 = SilkButton.Button8,

    /// <summary>
    /// The ninth mouse button.
    /// </summary>
    Button9 = SilkButton.Button9,

    /// <summary>
    /// The tenth mouse button.
    /// </summary>
    Button10 = SilkButton.Button10,

    /// <summary>
    /// The eleventh mouse button.
    /// </summary>
    Button11 = SilkButton.Button11,

    /// <summary>
    /// The twelth mouse button.
    /// </summary>
    Button12 = SilkButton.Button12
}