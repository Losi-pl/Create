using Range = Arch.Core.Range;

namespace Create.General;

public struct LongRange(long start, long end) : IEquatable<LongRange>
{
    public long Start => start;
    public long End => end;
    
    public bool Equals(LongRange other) => this == other;
    public static bool operator !=(LongRange left, LongRange right) => !(left == right);
    public static bool operator ==(LongRange left, LongRange right) => left.Start == right.Start && left.End == right.End;
}