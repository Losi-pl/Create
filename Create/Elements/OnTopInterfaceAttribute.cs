namespace Create.Elements;

/// <summary>
/// Czy interfejs jest zawsze na wieżchu
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class OnTopInterfaceAttribute : Attribute { }