namespace Create.OpenGL;

internal static class disposing
{
    static List<gl_element> to_dispose = new();
    static object task_lock = new();

    public static void add(gl_element element)
    {
        lock (task_lock)
            to_dispose.Add(element);
    }

    public static void execute()
    {
        lock (task_lock)
        {
            if (to_dispose.Count == 0)
                return;
            for (int i = 0; i < to_dispose.Count; i++)
                to_dispose[i].Dispose();
            to_dispose.Clear();
        }
    }

    public interface gl_element { public void Dispose(); }
}
