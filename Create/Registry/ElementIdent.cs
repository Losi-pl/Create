using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Helpers;
// ReSharper disable MemberCanBePrivate.Global

namespace Create.Registry;

public readonly struct ElementIdent: IEquatable<ElementIdent>
{
    private readonly IMod _mod;
    private readonly string _element;

    public ElementIdent(IMod mod, string element): this(mod, element, false) { }
    private ElementIdent(IMod mod, string element, bool isValid)
    {
        ArgumentNullException.ThrowIfNull(mod);
        if(!isValid && !IsOnlyElementValid(element))
            throw new ArgumentException("Invalid element identity, format only allows a-z, A-Z, 0-9, [.-_/\\]", nameof(element));
        (_mod, _element) = (mod, element);
    }

    public ElementIdent(ReadOnlySpan<char> fullIdentity)
    {
        if(!IsFormatValid(fullIdentity))
            throw new ArgumentException($"Invalid identity format: {{{fullIdentity}}}, expected {{mod:element}}");
        var semIndex = fullIdentity.IndexOf(':');
        _mod = IMod.FromIdentityOrAbstract(fullIdentity[..semIndex]);
        _element = new(fullIdentity[(semIndex + 1)..]);
    }
    
    public IMod Mod => _mod;
    public string Element => _element;
    
    public override string ToString() => $"{_mod.Identity}:{_element}";

    public static bool IsOnlyElementValid(ReadOnlySpan<char> elementIdentity)
    {
        foreach (var letter in elementIdentity)
        {
            if(char.IsAsciiLetter(letter))
                continue;
            if(char.IsNumber(letter))
                continue;
            if(letter is '.' or '-' or '_' or '/' or '\\')
                continue;
            return false;
        }

        return true;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsFormatValid(ReadOnlySpan<char> fullIdentity)
    {
        var semIndex = fullIdentity.IndexOf(':');
        if (semIndex == -1 || semIndex != fullIdentity.LastIndexOf(':'))
            return false;
        if (!IMod.IsIdentityValid(fullIdentity[..semIndex]))
            return false;
        
        return IsOnlyElementValid(fullIdentity[(semIndex + 1)..]);
    }

    public static bool IsElementOrModIdentity(ReadOnlySpan<char> identity)
    {
        var semPos = identity.IndexOf(':');
        if(semPos == -1)
            return IMod.IsIdentityValid(identity);
        return IsFormatValid(identity);
    }

    public static bool TryParse(ReadOnlySpan<char> text, out ElementIdent identity)
    {
        if (!IsFormatValid(text))
        {
            identity = default;
            return false;
        }
        var semIndex = text.IndexOf(':');
        identity = new(IMod.FromIdentityOrAbstract(text[..semIndex]), new(text[(semIndex + 1)..]), true);
        return true;
    }
    
    public bool Equals(ElementIdent other) => this == other;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj?.TryUnbox<ElementIdent>(out var other) ?? false)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(_mod);
        hc.AddBytes(_element.AsSpan().AsBytes());
        return hc.ToHashCode();
    }

    public static bool operator !=(ElementIdent a, ElementIdent b) => !(a == b);
    public static bool operator ==(ElementIdent a, ElementIdent b)
    {
        if (!ReferenceEquals(a._mod, b._mod))
            return false;
        return a._element == b._element;
    }
    
    public static bool operator !=(ElementIdent a, RefElementIdent b) => !(a == b);
    public static bool operator ==(ElementIdent a, RefElementIdent b)
    {
        if (!ReferenceEquals(a._mod, b.Mod))
            return false;
        return b.Element.Equals(a._element.AsSpan(), StringComparison.Ordinal);
    }
    
    public static bool operator !=(RefElementIdent b, ElementIdent a) => !(a == b);
    public static bool operator ==(RefElementIdent b, ElementIdent a) => a == b;

    public static bool operator !=(ElementIdent a, ReadOnlySpan<char> b) => !(a == b);
    public static bool operator ==(ElementIdent a, ReadOnlySpan<char> b) //Format: mod:identity
    {
        if (!IsFormatValid(b))
            return false;
        var modIden = a._mod.Identity.AsSpan();
        if(modIden.Length + 1 + a._element.Length != b.Length)
            return false;
        if(b[modIden.Length] != ':')
            return false;
        if (!b[..modIden.Length].Equals(modIden, StringComparison.Ordinal))
            return false;
        return b[(modIden.Length + 1)..].Equals(a._element.AsSpan(), StringComparison.Ordinal);
    }
    
    public static bool operator !=(ElementIdent a, string b) => !(a == b);
    public static bool operator ==(ElementIdent a, string b) => a == b.AsSpan();
    
    public static bool operator !=(ReadOnlySpan<char> b, ElementIdent a) => !(a == b);
    public static bool operator ==(ReadOnlySpan<char> b, ElementIdent a) => a == b;
    
    public static bool operator !=(string b, ElementIdent a) => !(a == b);
    public static bool operator ==(string b, ElementIdent a) => a == b;

    public static implicit operator ElementIdent(string identity) => new(identity);
    public static implicit operator ElementIdent(ReadOnlySpan<char> identity) => new(identity);
    public static implicit operator ElementIdent((IMod mod, string element) data) => new(data.mod, data.element);

    public static explicit operator (IMod, string)(ElementIdent ident) => (ident._mod, ident._element);
}