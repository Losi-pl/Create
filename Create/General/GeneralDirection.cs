using Silk.NET.Maths;

namespace Create.General;

[Flags]
public enum GeneralDirection : sbyte
{
    Nothing = 0,
    North =  1 << 5,
    East =   1 << 1,
    South =  1 << 2,
    West =   1 << 4,
    Top =    1 << 3,
    Bottom = 1 << 0,
    Vertical = Top | Bottom,
    Horizontal = North | East | South | West,
    All = North | East | South | West | Top | Bottom,
    X = West | East,
    Y = Top | Bottom,
    Z = North | East,
}

public static class EnumExtensions
{
    extension(GeneralDirection direction)
    {
        public GeneralDirection Inverted => (GeneralDirection)((((int)direction & 0b00000111) << 3) | (((int)direction & 0b00111000) >> 3));

        public Vector3D<int> AsVector()
        {
            var code = (int)direction;
            int x = ((code & (int)GeneralDirection.East) >> 1) - ((code & (int)GeneralDirection.West) >> 4);
            int y = ((code & (int)GeneralDirection.Top) >> 3) - ((code & (int)GeneralDirection.Bottom) >> 0);
            int z = ((code & (int)GeneralDirection.North) >> 5) - ((code & (int)GeneralDirection.South) >> 2);
            
            return new(x, y, z);
        }
    }
}