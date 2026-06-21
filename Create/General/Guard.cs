namespace Create.General;

public static class Guard
{
    /// <summary>
    /// Used to validate that the <paramref name="obj"/> is null otherwise will throw an error
    /// </summary>
    /// <param name="obj">Value to validate</param>
    /// <param name="error">Content of the exception</param>
    /// <exception cref="InvalidOperationException">If the <paramref name="obj"/> is not empty</exception>
    public static void IsNull(object? obj, Func<object> error)
    {
        if (obj is not null)
            throw new InvalidOperationException(error().ToString());
    }
    
    /// <summary>
    /// Used to validate that two objects are the same if they are not the error will be thrown
    /// </summary>
    /// <param name="obj1">The first object</param>
    /// <param name="obj2">The second object</param>
    /// <param name="error">Message of the error</param>
    /// <typeparam name="T">Type of the value to be compared</typeparam>
    /// <exception cref="InvalidOperationException">If the objects are not equal</exception>
    public static void Equal<T>(T obj1, T obj2, Func<T, T, object> error)
    {
        if (!EqualityComparer<T>.Default.Equals(obj1, obj2))
            throw new InvalidOperationException(error(obj1, obj2).ToString());
    }
}