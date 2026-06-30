using System.Numerics;
using System.Runtime.CompilerServices;
using Create.Graphics;
using Silk.NET.Input;
// ReSharper disable MemberCanBePrivate.Global

namespace Create.Input;

public class Mouse
{
    private readonly IMouse _mouse;
    private readonly Window _window;
    private Buttons _buttons;

    // ReSharper disable EventNeverSubscribedTo.Global
    /// Called when any button on the mouse is pressed
    public event Action<MouseButton>? ButtonPressed;
    /// Called when any button on the mouse is released
    public event Action<MouseButton>? ButtonReleased;
    /// Called when a button is clicked is called immediately after releasing the button
    public event Action<MouseButton, Vector2>? Click;
    /// Called when a button is clicked
    /// <remarks>Is exclusive with the <see cref="DoubleClick"/> and will only be called if the time for the other has elapsed</remarks>
    public event Action<MouseButton, Vector2>? ExclusiveClick;
    /// Called when a button is double-clicked
    public event Action<MouseButton, Vector2>? DoubleClick;
    // ReSharper restore EventNeverSubscribedTo.Global

    private Vector2 _lastPos;
    private MouseButton _lastPressed;
    private Vector2 _pressPosition;
    
    internal Mouse(IMouse mouse, Window window)
    {
        _mouse = mouse;
        _window = window;

        for (int i = 0; i <= (int)MouseButton.Button12; i++)
        {
            var b = new Button((MouseButton)i)
            {
                Combined = (mouse.IsButtonPressed((Silk.NET.Input.MouseButton)i), false, false)
            };
            _buttons[i] = b;
        }

        _lastPos = Position;
        
        mouse.MouseDown += PressButton;
        mouse.MouseUp += ReleaseButton;
        mouse.Click += ClickEvent;
        mouse.DoubleClick += DoubleClickEvent;
        mouse.MouseMove += MouseMoved;
    }

    private void PressButton(IMouse _, Silk.NET.Input.MouseButton buttonCode)
    {
        if((int)buttonCode is < 0 or >= 12)
            return;
        ref var button = ref _buttons[(int)buttonCode];
        button.Combined = button.Combined with { pressed = true, status = true };
        _lastPressed = (MouseButton)buttonCode;
        _pressPosition = Position;
        ButtonPressed?.Invoke((MouseButton)buttonCode);
    }
    
    private void ReleaseButton(IMouse _, Silk.NET.Input.MouseButton buttonCode)
    {
        if((int)buttonCode is < 0 or >= 12)
            return;
        ref var button = ref _buttons[(int)buttonCode];
        button.Combined = button.Combined with { released = true, status = false };
        ButtonReleased?.Invoke((MouseButton)buttonCode);
        if ((MouseButton)buttonCode == _lastPressed)
        {
            if (Vector2.Distance(Position, _pressPosition) < ClickRange)
            {
                button.Clicked = true;
                Click?.Invoke((MouseButton)buttonCode, Position);
            }
        }
    }

    private void ClickEvent(IMouse _, Silk.NET.Input.MouseButton buttonCode, Vector2 position)
    {
        if(Vector2.Distance(position, _pressPosition) > ClickRange)
            return;
        if((int)buttonCode is < 0 or >= 12)
            return;
        if((MouseButton)buttonCode != _lastPressed)
            return;
        
        ref var button = ref _buttons[(int)buttonCode];
        button.ExclusivelyClicked = true;
        ExclusiveClick?.Invoke((MouseButton)buttonCode, position);
    }
    
    private void DoubleClickEvent(IMouse _, Silk.NET.Input.MouseButton buttonCode, Vector2 position)
    {
        if((int)buttonCode is < 0 or >= 12)
            return;
        ref var button = ref _buttons[(int)buttonCode];
        button.DoubleClicked = true;
        DoubleClick?.Invoke((MouseButton)buttonCode, position);
    }

    private void MouseMoved(IMouse _, Vector2 newPos)
    {
        Delta += newPos - _lastPos;
        _lastPos = newPos;
    }
    
    /// <summary>
    /// Used to clear the events that have been called during the last frame
    /// </summary>
    internal void ClearEvents()
    {
        for (var i = 0; i <= (int)MouseButton.Button12; i++)
        {
            ref var button = ref _buttons[i];
            button.Clicked = false;
            button.ExclusivelyClicked = false;
            button.DoubleClicked = false;
            button.Combined = button.Combined with { pressed = false, released = false };
        }
        Delta = new();
    }

    /// <summary>
    /// A security against the event where a press or release event have been missed for some reason
    /// </summary>
    internal void RefreshButtonStates()
    {
        for (var i = 0; i <= (int)MouseButton.Button12; i++)
        {
            ref var button = ref _buttons[i];
            button.Combined = button.Combined with { status = _mouse.IsButtonPressed((Silk.NET.Input.MouseButton)i) };
        }
    }

    public MouseMode Mode
    {
        get => _mouse.Cursor.CursorMode switch
        {
            CursorMode.Normal => MouseMode.Normal,
            CursorMode.Hidden => MouseMode.Hidden,
            CursorMode.Raw or CursorMode.Disabled => MouseMode.LockHidden,
            _ => throw new ArgumentException("Unknown Mouse Mode")
        };
        set
        {
            var m = value switch
            {
                MouseMode.Normal => CursorMode.Normal,
                MouseMode.Hidden => CursorMode.Hidden,
                MouseMode.LockHidden => CursorMode.Raw,
                _ => throw new ArgumentException("Unknown Mouse Mode")
            };
            
            if(_mouse.Cursor.CursorMode == m)
                return;
            
            if (value is MouseMode.LockHidden || _mouse.Cursor.CursorMode is CursorMode.Raw or CursorMode.Disabled)
            {
                var nP = _window.MeGLFW.Size / 2;
                Position = new(nP.X, nP.Y);
                _lastPos = new(nP.X, nP.Y);
            }

            _mouse.Cursor.CursorMode = m;
        }
    }
    
    public Vector2 Position
    {
        get => _mouse.Position;
        set => _mouse.Position = value;
    }

    /// The maximal range in pixels between which the click and double-click events can be performed
    public int ClickRange
    {
        get => _mouse.DoubleClickRange;
        set => _mouse.DoubleClickRange = value;
    }
    
    /// The maximum time in milliseconds between two consecutive clicks to count as a double click
    public int DoubleClickTime
    {
        get => _mouse.DoubleClickTime;
        set => _mouse.DoubleClickTime = value;
    }
    
    /// The amount by which the mouse moved since the last logic update
    public Vector2 Delta { get; private set; }

    [InlineArray((int)MouseButton.Button12 + 1)]
    private struct Buttons
    {
        public Button _;
    }
    
    public struct Button
    {
        internal Button(MouseButton code) => ButtonCode = code;

        public (bool status, bool pressed, bool released) Combined
        {
            get => (IsPressed, JustPressed, JustReleased);
            set => (IsPressed, JustPressed, JustReleased) = value;
        }
        
        // ReSharper disable MemberCanBePrivate.Global, UnusedAutoPropertyAccessor.Global
        public MouseButton ButtonCode { get; }
        public bool IsPressed { get; private set; }
        public bool JustPressed { get; private set; }
        public bool JustReleased { get; private set; }
        public bool Clicked { get; internal set; }
        public bool ExclusivelyClicked { get; internal set; }
        public bool DoubleClicked { get; internal set; }
        // ReSharper restore MemberCanBePrivate.Global, UnusedAutoPropertyAccessor.Global
    }
}