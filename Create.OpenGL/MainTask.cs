using System;
using static System.Collections.Specialized.BitVector32;

namespace Create.OpenGL;

/// <summary>
/// Do wykonywania operacji w wątku głównym z wątków pobocznych
/// </summary>
public static class MainTask
{
    static List<Action> actions = new();
    static object task_lock = new();
    static int main_thread_id = 0;

    /// <summary>
    /// Uzyskanie kłównego wątku
    /// </summary>
    internal static void set_main_task() => main_thread_id = Thread.CurrentThread.ManagedThreadId;

    /// <summary>
    /// Wykonuje funkcje w wątku głównym
    /// </summary>
    internal static void make_listed_tasks()
    {
        while(loop(out var ex))
            ex?.Invoke();

        bool loop(out Action action)
        {
            lock(task_lock)
            {
                if (actions.Count == 0)
                {
                    action = null!;
                    return false;
                }
                action = actions[0];
                actions.RemoveAt(0);
                return true;
            }
        }
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym wątek wywołujący zostanie zatrzymany do momentu zakończenia operacji
    /// </summary>
    public static void Run(Action action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));
        if (main_thread_id == Thread.CurrentThread.ManagedThreadId)
            action();
        else
            RunAsync(action).Wait();
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym wątek wywołujący zostanie zatrzymany do momentu zakończenia operacji
    /// <para>Funkcja może zwracać wartość</para>
    /// </summary>
    public static T Run<T>(Func<T> func)
    {
        ArgumentNullException.ThrowIfNull(func, nameof(func));
        if (main_thread_id == Thread.CurrentThread.ManagedThreadId)
            return func();
        else
        {
            var t = RunAsync(func);
            t.Wait();
            return t.Result;
        }   
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym
    /// <para>Funkcja może zwracać wartość</para>
    /// </summary>
    public static async Task<T> RunAsync<T>(Func<T> func)
    {
        ArgumentNullException.ThrowIfNull(func, nameof(func));
        bool f = true;
        T value = default!;
        Exception ex = null!;
        Action ax = () => { try { value = func(); } catch (Exception e) { ex = e; } finally { f = false; } };
        lock (task_lock)
            actions.Add(ax);
        while (f)
        {
            await Task.Delay(2);
            if (main_thread_id == Thread.CurrentThread.ManagedThreadId)
            {
                lock (task_lock)
                    actions.Remove(ax);
                return func();
            }
        }
        if (ex is not null)
            throw new(ex.Message, ex);
        return value;
    }

    /// <summary>
    /// Wywołuje funkce w wątku głównym
    /// </summary>
    public static async Task RunAsync(Action action)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));
        bool f = true;
        Exception ex = null!;
        Action ax = () => { try { action(); } catch (Exception e) { ex = e; } finally { f = false; } };
        lock (task_lock)
            actions.Add(ax);
        while (f) 
        {
            await Task.Delay(2);
            if (main_thread_id == Thread.CurrentThread.ManagedThreadId)
            {
                lock (task_lock)
                    actions.Remove(ax);
                action();
                return;
            }
        }
        if (ex is not null)
            throw new(ex.Message, ex);
    }
}
