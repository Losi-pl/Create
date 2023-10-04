namespace Create.Elements;

/// <summary>
/// Czy interfejs pest pasywny i nie możesz z nim wchodzić aktywnie w interakcje 
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class PassiveInterface : Attribute { }