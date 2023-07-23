namespace Create.OpenGL;

/// <summary>
/// Mechanizm do usuwania z pamięci kartu graficznej danych które nie są już używane
/// </summary>
internal static class Disposing
{
    static List<gl_element> to_dispose = new();
    static object task_lock = new();

    /// <summary>
    /// Dodanie obiektu do listy rzeczy do usunięcia
    /// </summary>
    /// <param name="element"></param>
    public static void add(gl_element element)
    {
        lock (task_lock)
            to_dispose.Add(element);
    }

    /// <summary>
    /// Usunięcie wrzystkich elementów które zostały uzbierane do momentu wykonania funkcji od ostatniego wykonania
    /// </summary>
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

    /// <summary>
    /// Interfejs używany do usuwania obiektów z pamięci
    /// </summary>
    public interface gl_element 
    {
        /// <summary>
        /// Wywoływane aby usunąc dane
        /// </summary>
        public void Dispose();
    }
}
