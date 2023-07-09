using OpenTK.Mathematics;

namespace Create;

partial class MathC
{
    public static bool InView(Matrix4 projection, ((float x, float y, float z) pozition, (float x, float y, float z) size) cube)
    {
        Span<(float normX, float normY, float normZ, float distance)> span = stackalloc (float normX, float normY, float normZ, float distance)[6];
        (float minX, float minY, float minZ, float maxX, float maxY, float maxZ) cube_ = 
           (cube.pozition.x - (cube.size.x / 2),
            cube.pozition.y - (cube.size.y / 2),
            cube.pozition.z - (cube.size.z / 2),
            cube.pozition.x + (cube.size.x / 2),
            cube.pozition.y + (cube.size.y / 2),
            cube.pozition.z + (cube.size.z / 2));
        
        planes(projection, span);

        for (int i = 0; i < 6; i++)
        {
            float distance_min = span[i].normX * cube_.minX + span[i].normY * cube_.minY + span[i].normZ * cube_.minZ + span[i].distance;
            float distance_max = span[i].normX * cube_.maxX + span[i].normY * cube_.maxY + span[i].normZ * cube_.maxZ + span[i].distance;
            if (distance_min > 0 && distance_max > 0)
                return false;
        }

        return true;

        //Methods
        void planes(Matrix4 matrix, Span<(float normX, float normY, float normZ, float distance)> s)
        {
            s[0] = (matrix.M41 + matrix.M11,
                    matrix.M42 + matrix.M12,
                    matrix.M43 + matrix.M13,
                    matrix.M44 + matrix.M14);

            s[1] = (matrix.M41 - matrix.M11,
                    matrix.M42 - matrix.M12,
                    matrix.M43 - matrix.M13,
                    matrix.M44 - matrix.M14);

            s[2] = (matrix.M41 - matrix.M21,
                    matrix.M42 - matrix.M22,
                    matrix.M43 - matrix.M23,
                    matrix.M44 - matrix.M24);

            s[3] = (matrix.M41 + matrix.M21,
                    matrix.M42 + matrix.M22,
                    matrix.M43 + matrix.M23,
                    matrix.M44 + matrix.M24);

            s[4] = (matrix.M41 + matrix.M31,
                    matrix.M42 + matrix.M32,
                    matrix.M43 + matrix.M33,
                    matrix.M44 + matrix.M34);

            s[5] = (matrix.M41 - matrix.M31,
                    matrix.M42 - matrix.M32,
                    matrix.M43 - matrix.M33,
                    matrix.M44 - matrix.M34);

            /*s[0] = (matrix.M14 + matrix.M11,
                    matrix.M24 + matrix.M21,
                    matrix.M34 + matrix.M31,
                    matrix.M44 + matrix.M41);

            s[1] = (matrix.M14 - matrix.M11,
                    matrix.M24 - matrix.M21,
                    matrix.M34 - matrix.M31,
                    matrix.M44 - matrix.M41);

            s[2] = (matrix.M14 - matrix.M12,
                    matrix.M24 - matrix.M22,
                    matrix.M34 - matrix.M32,
                    matrix.M44 - matrix.M42);

            s[3] = (matrix.M14 + matrix.M12,
                    matrix.M24 + matrix.M22,
                    matrix.M34 + matrix.M32,
                    matrix.M44 + matrix.M42);

            s[4] = (matrix.M13,
                    matrix.M23,
                    matrix.M33,
                    matrix.M43);

            s[5] = (matrix.M14 - matrix.M13,
                    matrix.M24 - matrix.M23,
                    matrix.M34 - matrix.M33,
                    matrix.M44 - matrix.M43);*/

            for (int i = 0; i < 6; i++)
            {
                float length = MathF.Sqrt(s[i].normX * s[i].normX +
                                          s[i].normY * s[i].normY +
                                          s[i].normZ * s[i].normZ);
                s[i].normX /= length;
                s[i].normY /= length;
                s[i].normZ /= length;
                s[i].distance /= length;
            }
        }
    }
}
