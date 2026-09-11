namespace Create.Graphics;

public static class TaskGL
{
    extension(Task)
    {
        public static Task RunGraphics(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);
            var task = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var context = Window.IsMainThread ? new GraphicContext() : Window.Query(() => new GraphicContext()).Result;
            var thread = new Thread(() =>
            {
                try { context.ThreadBind(); }
                catch (Exception e) { task.SetException(e); return; }
                
                try { action(); task.SetResult(); }
                catch (Exception e) { task.SetException(e); }
                finally
                {
                    context.Unbind();
                    context.Dispose();
                }
            }) { IsBackground = true };
            thread.Start();
            return task.Task;
        }

        public static Task<T> RunGraphics<T>(Func<T> func)
        {
            ArgumentNullException.ThrowIfNull(func);
            var task = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            var context = Window.IsMainThread ? new GraphicContext() : Window.Query(() => new GraphicContext()).Result;
            var thread = new Thread(() =>
            {
                try { context.ThreadBind(); }
                catch (Exception e) { task.SetException(e); return; }
                
                try { task.SetResult(func()); }
                catch (Exception e) { task.SetException(e); }
                finally
                {
                    context.Unbind();
                    context.Dispose();
                }
            }) { IsBackground = true };
            thread.Start();
            return task.Task;
        }
    }
}