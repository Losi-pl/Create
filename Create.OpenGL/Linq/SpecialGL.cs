namespace Create.Linq;

public static class SpecialGL
{
    /// <summary>
    /// Kombinacja <see cref="Task.Wait"/> i <see cref="Task{TResult}.Result"/>
    /// </summary>
    public static T WaitResult<T>(this Task<T> task)
    {
        task.Wait();
        return task.Result;
    }
}