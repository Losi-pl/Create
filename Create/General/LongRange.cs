using System.Diagnostics.CodeAnalysis;
using Range = Arch.Core.Range;

namespace Create.General;

public readonly struct LongRange(long start, long end) : IEquatable<LongRange>
{
    public long Start => start;
    public long End => end;
    
    public bool Equals(LongRange other) => this == other;
    public static bool operator !=(LongRange left, LongRange right) => !(left == right);
    public static bool operator ==(LongRange left, LongRange right) => left.Start == right.Start && left.End == right.End;

    public override int GetHashCode() => HashCode.Combine(Start, End);
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is LongRange other && this == other;
}