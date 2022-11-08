using Create.Elements;
using OpenTK.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create
{
    partial class MathC
    {
        public static IEnumerable<Vector3i> CollidBlocks(Vector3 camera_poz, Vector2 camera_rotation, float distance)
        {
            return old_system();

            IEnumerable<Vector3i> old_system()
            {
                var start = point_(camera_poz);
                var start_f = camera_poz;
                Vector3 delta;
                {
                    var x = camera_rotation.X / 180 * MathF.PI;
                    var xz = new Vector2(MathF.Sin(x), MathF.Cos(x));
                    xz *= MathF.Cos(camera_rotation.Y / 180 * MathF.PI);
                    delta = new(xz.X, -MathF.Sin(camera_rotation.Y / 180 * MathF.PI), xz.Y);
                }
                delta *= .0001f;
                yield return start;
                while (true)
                {
                    camera_poz += delta;
                    if (camera_poz.Distance(start_f) >= distance)
                        yield break;
                    var @new = point_(camera_poz);
                    if(@new != start)
                    {
                        start = @new;
                        yield return start;
                    }
                }
                Vector3i point_(Vector3 vector) => new(MathC.Section(vector.X, 1), MathC.Section(vector.Y, 1), MathC.Section(vector.Z, 1));
            }
        }

        public static (bool Top, bool Bottom, bool North, bool East, bool South, bool West)? ReyIsCollision(
            (Vector3 pozition, Vector2 rotation) camera,
            (Vector3 pozition, Vector3 size) point,
            float distance)
        {
            var v = ReyCollisionStatus(camera, point, distance);
            if (v.HasValue)
                return (
                    v.Value.Top.HasValue,
                    v.Value.Bottom.HasValue,
                    v.Value.North.HasValue,
                    v.Value.East.HasValue,
                    v.Value.South.HasValue,
                    v.Value.West.HasValue);
            else return null;
        }
        public static (Vector3? Top, Vector3? Bottom, Vector3? North, Vector3? East, Vector3? South, Vector3? West)? ReyCollisionStatus(
            (Vector3 pozition, Vector2 rotation) camera,
            (Vector3 pozition, Vector3 size) point, 
            float distance)
        {
            var w = int_xz_by_x(point.pozition.X - (point.size.X / 2));
            var e = int_xz_by_x(point.pozition.X + (point.size.X / 2));
            var n = int_xz_by_y(point.pozition.Z + (point.size.Z / 2));
            var s = int_xz_by_y(point.pozition.Z - (point.size.Z / 2));

            var W = MathF.Abs(w - point.pozition.Z) < point.size.Z / 2;
            var E = MathF.Abs(e - point.pozition.Z) < point.size.Z / 2;
            var N = MathF.Abs(n - point.pozition.X) < point.size.X / 2;
            var S = MathF.Abs(s - point.pozition.X) < point.size.X / 2;

            var rot_x = calc_rot(camera.rotation.X);
            var rot_z = calc_rot(camera.rotation.X - 90);

            var wy = int_y_by_z(point.pozition.Z - (point.size.Z / 2));
            var ey = int_y_by_z(point.pozition.Z + (point.size.Z / 2));
            var ny = int_y_by_x(point.pozition.X + (point.size.X / 2));
            var sy = int_y_by_x(point.pozition.X - (point.size.X / 2));

            var WY = MathF.Abs(wy - point.pozition.Y) < point.size.Y / 2;
            var EY = MathF.Abs(ey - point.pozition.Y) < point.size.Y / 2;
            var NY = MathF.Abs(ny - point.pozition.Y) < point.size.Y / 2;
            var SY = MathF.Abs(sy - point.pozition.Y) < point.size.Y / 2;

            var xpy = int_xy_by(point.pozition.Y + (point.size.Y / 2));
            var zpy = int_zy_by(point.pozition.Y + (point.size.Y / 2));
            var xny = int_xy_by(point.pozition.Y - (point.size.Y / 2));
            var zny = int_zy_by(point.pozition.Y - (point.size.Y / 2));

            var XPY = MathF.Abs(xpy - point.pozition.X) < point.size.X / 2;
            var ZPY = MathF.Abs(zpy - point.pozition.Z) < point.size.Z / 2;
            var XNY = MathF.Abs(xny - point.pozition.X) < point.size.X / 2;
            var ZNY = MathF.Abs(zny - point.pozition.Z) < point.size.Z / 2;

            (bool n, bool e, bool s, bool w) xz = (N && NY, E && EY, S && SY, W && WY);
            (bool t, bool b) y_ = (XPY && ZPY, XNY && ZNY);

            var xyz_b = (y_.t, y_.b, xz.n, xz.e, xz.s, xz.w);

            if (xyz_b.Any())
                return (
                    xyz_b.t ? new(xpy, point.pozition.Y + (point.size.Y / 2), zpy) : null,
                    xyz_b.b ? new(xny, point.pozition.Y - (point.size.Y / 2), zny) : null,
                    xyz_b.n ? new(n, ny, point.pozition.Z + (point.size.Z / 2)) : null,
                    xyz_b.e ? new(point.pozition.X - (point.size.X / 2), ey, e) : null,
                    xyz_b.s ? new(point.pozition.X - (point.size.X / 2), sy, s) : null,
                    xyz_b.w ? new(w, wy, point.pozition.Z - (point.size.Z / 2)) : null);
            else
                return null;

            //Mathods
            float tang_c(float x) => -MathF.Tan(x - (MathF.PI / 2));
            float int_xz_by_x(float x)
            {
                if (camera.rotation.X is 0 or 360)
                    return float.PositiveInfinity;
                if (camera.rotation.X is 180)
                    return float.NegativeInfinity;
                var a = tang_c(camera.rotation.X / 180 * MathF.PI);
                return ((x - camera.pozition.X) * a) + camera.pozition.Z;
            }
            float int_xz_by_y(float y)
            {
                if (camera.rotation.X is 90)
                    return float.PositiveInfinity;
                if (camera.rotation.X is 270)
                    return float.NegativeInfinity;
                var a = tang_c(camera.rotation.X / 180 * MathF.PI);
                return ((y - camera.pozition.Z) / a) + camera.pozition.X;
            }
            float int_y_by_z(float x)
            {
                if (rot_z is 90)
                    return float.PositiveInfinity;
                if (rot_z is -90)
                    return float.NegativeInfinity;
                var a = MathF.Tan(rot_z / 180 * MathF.PI);
                return ((x - camera.pozition.X) * a) + camera.pozition.Y;
            }
            float int_y_by_x(float x)
            {
                if (rot_x is 90)
                    return float.PositiveInfinity;
                if (rot_x is -90)
                    return float.NegativeInfinity;
                var a = MathF.Tan(rot_x / 180 * MathF.PI);
                return ((x - camera.pozition.Z) * a) + camera.pozition.Y;
            }
            float int_xy_by(float y)
            {
                if (rot_x is 0)
                    return float.PositiveInfinity;
                if (rot_x is 180)
                    return float.NegativeInfinity;
                var a = MathF.Tan(rot_x / 180 * MathF.PI);
                return ((y - camera.pozition.X) / a) + camera.pozition.X;
            }
            float int_zy_by(float y)
            {
                if (rot_x is 0)
                    return float.PositiveInfinity;
                if (rot_x is 180)
                    return float.NegativeInfinity;
                var a = MathF.Tan(rot_z / 180 * MathF.PI);
                return ((y - camera.pozition.Z) / a) + camera.pozition.X;
            }

            float calc_rot(float x)
            {
                if(camera.rotation.Y > 0)
                {
                    if(x > 0 && x < 180)
                        return 90 - (camera.rotation.Y * MathF.Sin(x / 180 * MathF.PI));
                    if (x > 180 && x < 360)
                        return 270 - (camera.rotation.Y * MathF.Sin((x - 180) / 180 * MathF.PI));
                    if (x == 0 || x == 180 || x == 260)
                        return 90;
                }
                if (camera.rotation.Y < 0)
                {
                    if (x > 0 && x < 180)
                        return -90 + (camera.rotation.Y * MathF.Sin(x / 180 * MathF.PI));
                    if (x > 180 && x < 360)
                        return 270 - (camera.rotation.Y * MathF.Sin((x - 180) / 180 * MathF.PI));
                    if (x == 0 || x == 180 || x == 260)
                        return 90;
                }
                return 0;
            }
        }
    }
}
