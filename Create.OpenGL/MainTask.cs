using System;

namespace Create.OpenGL;

public static class MainTask
{
    static List<Action> actions = new();
    static object task_lock = new();
    static Thread? main_thread;

    internal static void set_main_task() => main_thread = main_thread ?? Thread.CurrentThread;

    internal static void make_listed_tasks()
    {
        lock(task_lock)
        {
            if (actions.Count == 0)
                return;
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                var tex = actions[0].ToString();
                Engine.Title = $"Method => {tex}";
                actions[0]();
                actions.RemoveAt(0);
                Engine.Title = $"Method finished => {tex}";
            }
        }
    }

    public static void Run(Action action)
    {
        if (main_thread == Thread.CurrentThread)
            action();
        else
            lock(task_lock)
                actions.Add(action);
    }
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
    public static Task RunAsync(Action action)
    {
        return Task.Run(() =>
        {
            lock (task_lock)
                actions.Add(action);
        }); 
    }
}
