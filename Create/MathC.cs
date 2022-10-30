using OpenTK.Mathematics;

namespace Create;

static class MathC
{
    #region InSection
    public static int InSection(int value, int section)
    {
        if (value > 0)
            return value % section;
        else
            return (value % section) == 0 ? 0 : section + (value % section);
    }
    public static float InSection(float value, float section)
    {
        if (value > 0)
            return value % section;
        else
            return (value % section) == 0 ? 0 : section + (value % section);
    }
    public static int InSection(int value, int section, int offset) => InSection(value - offset, section);
    public static float InSection(float value, float section, float offset) => InSection(value - offset, section);
    public static Vector2 InSection(Vector2 value, float section) => new(InSection(value.X, section), InSection(value.Y, section));
    public static Vector2 InSection(Vector2 value, float section, float offset) => InSection(new Vector2(value.X - offset, value.Y - offset), section);
    public static Vector2i InSection(Vector2i value, int section) => new(InSection(value.X, section), InSection(value.Y, section));
    public static Vector2i InSection(Vector2i value, int section, int offset) => InSection(new Vector2i(value.X - offset, value.Y - offset), section);
    public static Vector3 InSection(Vector3 value, float section) => new(InSection(value.X, section), InSection(value.Y, section), InSection(value.Z, section));
    public static Vector3 InSection(Vector3 value, float section, float offset) => InSection(new Vector3(value.X - offset, value.Y - offset, value.Z - offset), section);
    public static Vector3i InSection(Vector3i value, int section) => new(InSection(value.X, section), InSection(value.Y, section), InSection(value.Z, section));
    public static Vector3i InSection(Vector3i value, int section, int offset) => InSection(new Vector3i(value.X - offset, value.Y - offset, value.Z - offset), section);
    public static Vector4 InSection(Vector4 value, float section) => new(InSection(value.X, section), InSection(value.Y, section), InSection(value.Z, section), InSection(value.W, section));
    public static Vector4 InSection(Vector4 value, float section, float offset) => InSection(new Vector4(value.X - offset, value.Y - offset, value.Z - offset, value.W - offset), section);
    public static Vector4i InSection(Vector4i value, int section) => new(InSection(value.X, section), InSection(value.Y, section), InSection(value.Z, section), InSection(value.W, section));
    public static Vector4i InSection(Vector4i value, int section, int offset) => InSection(new Vector4i(value.X - offset, value.Y - offset, value.Z - offset, value.W - offset), section);
    public static Vector2 InSection(Vector2 value, float section, Vector2 offset) => InSection(value - offset, section);
    public static Vector2i InSection(Vector2i value, int section, Vector2i offset) => InSection(value - offset, section);
    public static Vector3 InSection(Vector3 value, float section, Vector3 offset) => InSection(value - offset, section);
    public static Vector3i InSection(Vector3i value, int section, Vector3i offset) => InSection(value - offset, section);
    public static Vector4 InSection(Vector4 value, float section, Vector4 offset) => InSection(value - offset, section);
    public static Vector4i InSection(Vector4i value, int section, Vector4i offset) => InSection(value - offset, section);
    #endregion

    #region Section
    public static int Section(float value, float sect)
    {
        if (value < 0)
            return value % sect == 0 ? (int)(value / sect) : (int)(value / sect) - 1;
        else
            return (int)(value / sect);
    }
    public static int Section(int value, int sect)
    {
        if (value < 0)
            return value % sect == 0 ? value / sect : (value / sect) - 1;
        else
            return value / sect;
    }
    public static int Section(int value, int section, int offset) => Section(value - offset, section);
    public static int Section(float value, float section, float offset) => Section(value - offset, section);
    public static Vector2i Section(Vector2 value, float sect) => new(Section(value.X, sect), Section(value.Y, sect));
    public static Vector2i Section(Vector2 value, float sect, float offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect));
    public static Vector2i Section(Vector2i value, int sect) => new(Section(value.X, sect), Section(value.Y, sect));
    public static Vector2i Section(Vector2i value, int sect, int offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect));
    public static Vector3i Section(Vector3 value, float sect) => new(Section(value.X, sect), Section(value.Y, sect), Section(value.Z, sect));
    public static Vector3i Section(Vector3 value, float sect, float offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect), Section(value.Z - offset, sect));
    public static Vector3i Section(Vector3i value, int sect) => new(Section(value.X, sect), Section(value.Y, sect), Section(value.Z, sect));
    public static Vector3i Section(Vector3i value, int sect, int offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect), Section(value.Z - offset, sect));
    public static Vector4i Section(Vector4 value, float sect) => new(Section(value.X, sect), Section(value.Y, sect), Section(value.Z, sect), Section(value.W, sect));
    public static Vector4i Section(Vector4 value, float sect, float offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect), Section(value.Z - offset, sect), Section(value.W - offset, sect));
    public static Vector4i Section(Vector4i value, int sect) => new(Section(value.X, sect), Section(value.Y, sect), Section(value.Z, sect), Section(value.W, sect));
    public static Vector4i Section(Vector4i value, int sect, int offset) => new(Section(value.X - offset, sect), Section(value.Y - offset, sect), Section(value.Z - offset, sect), Section(value.W - offset, sect));
    public static Vector2i Section(Vector2 value, float sect, Vector2 offset) => Section(value - offset, sect);
    public static Vector3i Section(Vector3 value, float sect, Vector3 offset) => Section(value - offset, sect);
    public static Vector4i Section(Vector4 value, float sect, Vector4 offset) => Section(value - offset, sect);
    public static Vector2i Section(Vector2i value, int sect, Vector2i offset) => Section(value - offset, sect);
    public static Vector3i Section(Vector3i value, int sect, Vector3i offset) => Section(value - offset, sect);
    public static Vector4i Section(Vector4i value, int sect, Vector4i offset) => Section(value - offset, sect);
    #endregion

    public static IEnumerable<(int x, int y)> GetElementsFromCenter(int range)
    {
        List<(int x, int y)> elems = new();
        for (int x = -range; x < range; x++)
            for (int y = -range; y < range; y++)
                if ((x * x) + (y * y) < (range * range))
                    elems.Add((x, y));
        elems.Sort((p1, p2) =>
        {
            var dir = dist(p1) - dist(p2);
            if (dir < 0)
                return -1;
            if (dir > 0)
                return 1;
            return 0;
        });
        return elems;

        float dist((int x, int y) point)
        {
            var X = MathF.Abs(0 - point.x);
            var Y = MathF.Abs(0 - point.y);
            var squ = (X * X) + (Y * Y);
            return MathF.Sqrt(squ);
        }
    }
}
