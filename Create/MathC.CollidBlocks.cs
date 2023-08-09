using Create.Elements;
using Create.Linq;
using OpenTK.Mathematics;

namespace Create
{
    partial class MathC
    {
        /// <summary>
        /// Podaje wszystkie bloki przez które przechodzi promień z punktu <paramref name="camera_poz"/>, pod kątem <paramref name="camera_rotation"/> i o długości <paramref name="distance"/>
        /// </summary>
        /// <param name="camera_poz">Punkt wyjściowy promienia</param>
        /// <param name="camera_rotation">Kierunek promienia z punktu wyjścia</param>
        /// <param name="distance">Długość promienia</param>
        /// <returns></returns>
        public static IEnumerable<Vector3i> CollidBlocks(Vector3 camera_poz, Vector2 camera_rotation, float distance)
        {
            var start_block = MathC.Section(camera_poz, 1);
            var ray = CreateRay(camera_poz, camera_rotation, distance);
            yield return start_block;
            Ray.BoxHit? hit = null;
            int i = 0;
            do
            {
                hit = ray.CastBox(start_block + new Vector3(.5f, .5f, .5f), new(1, 1, 1));
                if (hit.HasValue ? hit.Value.Exit.HasValue : false)
                    start_block += hit!.Value.Exit!.Value.Side switch
                    {
                        Block.BlockSide.Top => new(0, 1, 0),
                        Block.BlockSide.Bottom => new(0, -1, 0),
                        Block.BlockSide.North => new(0, 0, 1),
                        Block.BlockSide.South => new(0, 0, -1),
                        Block.BlockSide.West => new(-1, 0, 0),
                        Block.BlockSide.East => new(1, 0, 0),

                        _ => throw new("Unknown error")
                    };
                yield return start_block;
                i++;
            }
            while ((hit.HasValue ? hit.Value.Exit.HasValue : false) && i < 20);
        }

        /// <summary>
        /// Tworzy instancje promienia
        /// </summary>
        /// <param name="start">Punkt startowy promienia</param>
        /// <param name="rotation">Nachylenie promienia</param>
        /// <param name="distance">Opcjonalna długość promienia</param>
        /// <returns></returns>
        public static Ray CreateRay(Vector3 start, Vector2 rotation, float? distance = null)
        {
            Ray r = new();
            r.Set(start, rotation, distance);
            return r;
        }

        /// <summary>
        /// Technologia Ray-Cast
        /// </summary>
        public class Ray : IDisposable
        {
            (float x, float y, float z) _start;
            (float x, float y, float z) _delta;
            float? _lenght;
            (float x, float y) _orientaction;
            bool _disposed;

            /// <summary>
            /// Czyszczenie ustawień promienia
            /// </summary>
            public void Dispose()
            {
                if (_disposed)
                    return;
                GC.SuppressFinalize(this);
                _start = (0, 0, 0);
                _delta = (0, 0, 0);
                _lenght = null;
                _orientaction = (0, 0);
                _disposed = true;
            }

            /// <summary>
            /// Ustawienie ustawień promienia
            /// </summary>
            /// <param name="start"></param>
            /// <param name="rotation"></param>
            /// <param name="distance"></param>
            public void Set(Vector3 start, Vector2 rotation, float? distance = null)
            {
                _start = start.ToTumple();
                _orientaction = rotation.ToTumple();
                _lenght = distance;
                _disposed = false;
                { // _delta
                    var xz = new Vector2(MathF.Sin(cast_angle_to_pi(rotation.X)), MathF.Cos(cast_angle_to_pi(rotation.X))) * MathF.Cos(cast_angle_to_pi(rotation.Y));
                    _delta = (xz.X, MathF.Sin(cast_angle_to_pi(rotation.Y)), xz.Y);
                } // _delta
            }

            /// <summary>
            /// Sprawdzenie czy promień przechodzi przez sześcien o pozycji <paramref name="pozition"/> i wymiarach <paramref name="size"/>
            /// </summary>
            /// <param name="pozition">Pozycja sześcianu</param>
            /// <param name="size">Wymiary sześcianu</param>
            /// <returns>Informacje kolizji. Jeżeli funkcja nic nie zwruci nie doszło do kolizji</returns>
            public BoxHit? CastBox(Vector3 pozition, Vector3 size)
            {
                if (size.X <= 0 || size.Y <= 0 || size.Z <= 0)
                    return null;

                Vector3? north = null;
                {
                    var p = pozition - _start;
                    var _p = p.Z + (size.Z / 2);
                    var d = _delta.ToVector() / _delta.z;
                    d *= _p;
                    if (MathF.Abs(p.X - d.X) < (size.X / 2) && MathF.Abs(p.Y - d.Y) < (size.Y / 2))
                    {
                        north = d + _start;
                        if (_delta.z > 0 ? d.Z < 0 : d.Z > 0)
                            north = null;
                    }
                }

                Vector3? south = null;
                {
                    var p = pozition - _start;
                    var _p = p.Z - (size.Z / 2);
                    var d = _delta.ToVector() / _delta.z;
                    d *= _p;
                    if (MathF.Abs(p.X - d.X) < (size.X / 2) && MathF.Abs(p.Y - d.Y) < (size.Y / 2))
                    {
                        south = d + _start;
                        if (_delta.z > 0 ? d.Z < 0 : d.Z > 0)
                            south = null;
                    }
                }

                Vector3? west = null;
                {
                    var p = pozition - _start;
                    var _p = p.X - (size.X / 2);
                    var d = _delta.ToVector() / _delta.x;
                    d *= _p;
                    if (MathF.Abs(p.Z - d.Z) < (size.Z / 2) && MathF.Abs(p.Y - d.Y) < (size.Y / 2))
                    {
                        west = d + _start;
                        if (_delta.x > 0 ? d.X < 0 : d.X > 0)
                            west = null;
                    }
                }

                Vector3? east = null;
                {
                    var p = pozition - _start;
                    var _p = p.X + (size.X / 2);
                    var d = _delta.ToVector() / _delta.x;
                    d *= _p;
                    if (MathF.Abs(p.Z - d.Z) < (size.Z / 2) && MathF.Abs(p.Y - d.Y) < (size.Y / 2))
                    {
                        east = d + _start;
                        if (_delta.x > 0 ? d.X < 0 : d.X > 0)
                            east = null;
                    }
                }

                Vector3? up = null;
                {
                    var p = pozition - _start;
                    var _p = p.Y + (size.Y / 2);
                    var d = _delta.ToVector() / _delta.y;
                    d *= _p;
                    if (MathF.Abs(p.X - d.X) < (size.X / 2) && MathF.Abs(p.Z - d.Z) < (size.Z / 2))
                    {
                        up = d + _start;
                        if (_delta.y > 0 ? d.Y < 0 : d.Y > 0)
                            up = null;
                    }
                }

                Vector3? down = null;
                {
                    var p = pozition - _start;
                    var _p = p.Y - (size.Y / 2);
                    var d = _delta.ToVector() / _delta.y;
                    d *= _p;
                    if (MathF.Abs(p.X - d.X) < (size.X / 2) && MathF.Abs(p.Z - d.Z) < (size.Z / 2))
                    {
                        down = d + _start;
                        if (_delta.y > 0 ? d.Y < 0 : d.Y > 0)
                            down = null;
                    }
                }

                int count = 0;
                
                if(north.HasValue) count++;
                if(south.HasValue) count++;
                if(west.HasValue)  count++;
                if(east.HasValue)  count++;
                if(up.HasValue)    count++;
                if(down.HasValue)  count++;

                if (count == 0)
                    return null;

                if (count == 1)
                {
                    if (MathF.Abs(pozition.X - _start.x) < size.X / 2 &&
                       MathF.Abs(pozition.Y - _start.y) < size.Y / 2 &&
                       MathF.Abs(pozition.Z - _start.z) < size.Z / 2)
                    {
                        if (north.HasValue)
                            return new BoxHit(null, (Block.BlockSide.North, north.Value), this);
                        if (south.HasValue)
                            return new BoxHit(null, (Block.BlockSide.South, south.Value), this);
                        if (east.HasValue)
                            return new BoxHit(null, (Block.BlockSide.East, east.Value), this);
                        if (west.HasValue)
                            return new BoxHit(null, (Block.BlockSide.West, west.Value), this);
                        if (up.HasValue)
                            return new BoxHit(null, (Block.BlockSide.Top, up.Value), this);
                        if (down.HasValue)
                            return new BoxHit(null, (Block.BlockSide.Bottom, down.Value), this);
                    }
                    else
                    {
                        if (north.HasValue)
                            return new BoxHit((Block.BlockSide.North, north.Value), null, this);
                        if (south.HasValue)
                            return new BoxHit((Block.BlockSide.South, south.Value), null, this);
                        if (west.HasValue)
                            return new BoxHit((Block.BlockSide.West, west.Value), null, this);
                        if (east.HasValue)
                            return new BoxHit((Block.BlockSide.East, east.Value), null, this);
                        if (up.HasValue)
                            return new BoxHit((Block.BlockSide.Top, up.Value), null, this);
                        if (down.HasValue)
                            return new BoxHit((Block.BlockSide.Bottom, down.Value), null, this);
                    }
                }

                if(count == 2)
                {
                    (Block.BlockSide s, Vector3 p)? point1 = null, point2 = null;

                    if (north.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.North, north.Value);
                        else
                            point2 = (Block.BlockSide.North, north.Value);

                    if (south.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.South, south.Value);
                        else
                            point2 = (Block.BlockSide.South, south.Value);

                    if (west.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.West, west.Value);
                        else
                            point2 = (Block.BlockSide.West, west.Value);

                    if (east.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.East, east.Value);
                        else
                            point2 = (Block.BlockSide.East, east.Value);

                    if (up.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.Top, up.Value);
                        else
                            point2 = (Block.BlockSide.Top, up.Value);

                    if (down.HasValue)
                        if (!point1.HasValue)
                            point1 = (Block.BlockSide.Bottom, down.Value);
                        else
                            point2 = (Block.BlockSide.Bottom, down.Value);

                    var closer = (point1, Vector3.Distance(point1!.Value.p, _start));
                    var father = (point2, Vector3.Distance(point2!.Value.p, _start));

                    if (father.Item2 < closer.Item2)
                        (closer, father) = (father, closer);

                    if (_lenght.HasValue)
                    {
                        if (closer.Item2 > _lenght.Value)
                            return null;
                        else if (father.Item2 > _lenght.Value)
                            return new BoxHit(closer.point1, null, this);
                        else
                            return new BoxHit(closer.point1, father.point2, this);
                    }
                    else
                        return new BoxHit(closer.point1, father.point2, this);
                }
                return null;
            }

            /// <summary>
            /// Start point of Ray
            /// </summary>
            public Vector3 Start => _start.ToVector();

            /// <summary>
            /// Optional end point if it has a distance
            /// </summary>
            public Vector3? End => _lenght.HasValue ? _start + (_delta.ToVector() * _lenght) : null;
            
            /// <summary>
            /// Orientation of a Ray
            /// </summary>
            public Vector2 Orientaction => _orientaction.ToVector();

            /// <summary>
            /// Lenght of a ray if it's set
            /// </summary>
            public float? Lenght => _lenght;

            /// <summary>
            /// Information about hit points in Box
            /// </summary>
            public struct BoxHit
            {
                Block.BlockSide? _enter_s;
                Block.BlockSide? _exit_s;
                Vector3? _enter_p;
                Vector3? _exit_p;
                Ray _ray;

                /// <summary>
                /// Enter point
                /// </summary>
                public (Block.BlockSide Side, Vector3 Point)? Enter => _enter_s.HasValue && _enter_p.HasValue ? (_enter_s.Value, _enter_p.Value) : null;

                /// <summary>
                /// Exit point
                /// </summary>
                public (Block.BlockSide Side, Vector3 Point)? Exit => _exit_s.HasValue && _exit_p.HasValue ? (_exit_s.Value, _exit_p.Value) : null;

                internal BoxHit((Block.BlockSide, Vector3)? enter, (Block.BlockSide, Vector3)? exit, Ray ray)
                {
                    _ray = ray;
                    if (enter.HasValue)
                        (_enter_s, _enter_p) = enter.Value;
                    else
                        (_enter_s, _enter_p) = (null, null);
                    if (exit.HasValue)
                        (_exit_s, _exit_p) = exit.Value;
                    else
                        (_exit_s, _exit_p) = (null, null);
                }
            }
        }
    }
}
