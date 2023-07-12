namespace Create.OpenGL;

/// <summary>
/// Do wykonywania operacji w wątku głównym z wątków pobocznych
/// </summary>
public static class MainTask
{
    static List<Action> actions = new();
    static object task_lock = new();
    static Thread? main_thread;

    /// <summary>
    /// Uzyskanie kłównego wątku
    /// </summary>
    internal static void set_main_task() => main_thread = main_thread ?? Thread.CurrentThread;

    /// <summary>
    /// Wykonuje funkcje w wątku głównym
    /// </summary>
    internal static void make_listed_tasks()
    {
        lock(task_lock)
        {
            if (actions.Count == 0)
                return;
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                var tex = actions[0].ToString();
                actions[0]();
                actions.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym wątek wywołujący zostanie zatrzymany do momentu zakończenia operacji
    /// </summary>
    public static void Run(Action action)
    {
        if (main_thread == Thread.CurrentThread)
            action();
        else
            lock(task_lock)
                actions.Add(action);
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym wątek wywołujący zostanie zatrzymany do momentu zakończenia operacji
    /// <para>Funkcja może zwracać wartość</para>
    /// </summary>
    public static T Run<T>(Func<T> func)
    {
        if (main_thread == Thread.CurrentThread)
            return func();
        else
        {
            T wyn = default!;
            bool kompl = false;
            lock (task_lock)
                actions.Add(() => (kompl, wyn) = (true, func()));
            while (!kompl) { Engine.Title = Engine.Title; }
            return wyn;
        }
            
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym
    /// <para>Funkcja może zwracać wartość</para>
    /// </summary>
    public static Task<T> RunAsync<T>(Func<T> func)
    {
        return Task.Run(() =>
        {
            T wyn = default!;
            bool kompl = false;
            lock (task_lock)
                actions.Add(() => (kompl, wyn) = (true, func()));
            while (!kompl) { }
            return wyn;
        });
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym
    /// </summary>
    public static Task RunAsync(Action action)
    {
        return Task.Run(() =>
        {
            lock (task_lock)
                actions.Add(action);
        }); 
    }
}
