using Create.Linq;
using OpenTK.Mathematics;

namespace Create;

partial class MathC
{
    public static bool InView(Matrix4 projection, ((float x, float y, float z) pozition, (float x, float y, float z) size) cube)
    {
        var chun = projection * new Vector4(cube.pozition.ToVector(), 1);
        return true;
        // TODO - Do that anoying part of testing chunk visibility
    }
}
