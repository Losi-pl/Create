namespace Create.Elements;

[AttributeUsage(AttributeTargets.Field)]
public class ElementNameAttribute(string name) : Attribute
{
    public string Name => name;
}

[AttributeUsage(AttributeTargets.Field)]
public class IgnoreElementAttribute : Attribute;