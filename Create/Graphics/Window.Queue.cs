using System.Collections.Concurrent;

namespace Create.Graphics;

// ReSharper disable once ClassCannotBeInstantiated
partial class Window
{
    private readonly ConcurrentQueue<Action> _queue = new();

    public static void Queue(Action action)
    {
        if (IsMainThread)
            action();
        else
            Main._queue.Enqueue(action);
    }

    public static Task Query(Action action)
    {
        if (IsMainThread)
        {
            try
            {
                action();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        var task = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        Main._queue.Enqueue(() =>
        {
            try
            {
                action();
                task.SetResult();
            }
            catch (Exception e)
            {
                task.SetException(e);
            }
        });
        return task.Task.WaitAsync(CancellationToken.None);
    }
    
    public static Task<T> Query<T>(Func<T> action)
    {
        if (IsMainThread)
        {
            try
            {
                return Task.FromResult(action());
            }
            catch (Exception e)
            {
                return Task.FromException<T>(e);
            }
        }

        var task = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
        Main._queue.Enqueue(() =>
        {
            try
            {
                task.SetResult(action());
            }
            catch (Exception e)
            {
                task.SetException(e);
            }
        });
        return task.Task.ContinueWith(t => t.Result, TaskScheduler.Default);
    }

    private void RunQueuedTasks()
    {
        while (_queue.TryDequeue(out var action))
            action.Invoke();
    }
}