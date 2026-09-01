using CommunityToolkit.HighPerformance;
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CS0660, CS0661

namespace Create.Registry;

public readonly ref struct RefElementIdent : IEquatable<RefElementIdent>
{
    private readonly IMod _mod;
    private readonly ReadOnlySpan<char> _element;
    
    public RefElementIdent(IMod mod, ReadOnlySpan<char> element): this(mod, element, false) { }
    private RefElementIdent(IMod mod, ReadOnlySpan<char> element, bool isValid)
    {
        if(!isValid && !ElementIdent.IsOnlyElementValid(element))
            throw new ArgumentException("Invalid element identity, format only allows a-z, A-Z, 0-9, [.-_/\\]", nameof(element));
        _mod = mod;
        _element = element;
    }
    
    public RefElementIdent(ReadOnlySpan<char> fullIdentity)
    {
        if(!ElementIdent.IsFormatValid(fullIdentity))
            throw new ArgumentException($"Invalid identity format: {{{fullIdentity}}}, expected {{mod:element}}");
        var semIndex = fullIdentity.IndexOf(':');
        _mod = IMod.FromIdentityOrAbstract(fullIdentity[..semIndex]);
        _element = fullIdentity[(semIndex + 1)..];
    }

    public IMod Mod => _mod;
    public ReadOnlySpan<char> Element => _element;
    
    public override string ToString() => $"{_mod.Identity}:{_element}";
    
    public static bool TryParse(ReadOnlySpan<char> text, out RefElementIdent identity)
    {
        if (!ElementIdent.IsFormatValid(text))
        {
            identity = default;
            return false;
        }
        var semIndex = text.IndexOf(':');
        identity = new(IMod.FromIdentityOrAbstract(text[..semIndex]), text[(semIndex + 1)..], true);
        return true;
    }
    
    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(_mod);
        hc.AddBytes(_element.AsBytes());
        return hc.ToHashCode();
    }

    public bool Equals(RefElementIdent other) => this == other;
    
    public static bool operator !=(RefElementIdent a, RefElementIdent b) => !(a == b);
    public static bool operator ==(RefElementIdent a, RefElementIdent b)
    {
        if (!ReferenceEquals(a._mod, b._mod))
            return false;
        return a._element == b._element;
    }

    public static bool operator !=(RefElementIdent a, ReadOnlySpan<char> b) => !(a == b);
    public static bool operator ==(RefElementIdent a, ReadOnlySpan<char> b) //Format: mod:identity
    {
        if (!ElementIdent.IsFormatValid(b))
            return false;
        var modIden = a._mod.Identity.AsSpan();
        if(modIden.Length + 1 + a._element.Length != b.Length)
            return false;
        if(b[modIden.Length] != ':')
            return false;
        if (!b[..modIden.Length].Equals(modIden, StringComparison.Ordinal))
            return false;
        return b[(modIden.Length + 1)..].Equals(a._element, StringComparison.Ordinal);
    }
    
    public static bool operator !=(RefElementIdent a, string b) => !(a == b);
    public static bool operator ==(RefElementIdent a, string b) => a == b.AsSpan();
    
    public static bool operator !=(ReadOnlySpan<char> b, RefElementIdent a) => !(a == b);
    public static bool operator ==(ReadOnlySpan<char> b, RefElementIdent a) => a == b;
    
    public static bool operator !=(string b, RefElementIdent a) => !(a == b);
    public static bool operator ==(string b, RefElementIdent a) => a == b;
    
    public static implicit operator RefElementIdent(string identity) => new(identity);
    public static implicit operator RefElementIdent(ReadOnlySpan<char> identity) => new(identity);
    public static implicit operator RefElementIdent((IMod mod, string element) data) => new(data.mod, data.element);
    public static implicit operator RefElementIdent(ElementIdent identity) => new(identity.Mod, identity.Element);

    public static explicit operator (IMod, string)(RefElementIdent ident) => (ident._mod, new(ident._element));
    public static explicit operator ElementIdent(RefElementIdent ident) => new(ident._mod, new(ident._element));
}