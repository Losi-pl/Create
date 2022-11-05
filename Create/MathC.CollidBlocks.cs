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
    }
}
