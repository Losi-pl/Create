using System.Diagnostics.CodeAnalysis;

namespace Create.Registry;

public sealed class ElementIdentComparer : IAlternateEqualityComparer<RefElementIdent, ElementIdent>
{
    public static readonly ElementIdentComparer Default = new();

    public int GetHashCode([DisallowNull] ElementIdent obj) => obj.GetHashCode();

    public bool Equals(RefElementIdent alternate, ElementIdent other) => alternate == other;

    public int GetHashCode(RefElementIdent alternate) => alternate.GetHashCode();

    public ElementIdent Create(RefElementIdent alternate) => (ElementIdent)alternate;
}