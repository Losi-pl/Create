namespace Create.Virtuals;

/// <summary>
/// <inheritdoc cref="VirtualList{TValue}"/>
/// </summary>
public static class VirtualList
{
    /// <summary>
    /// Tworzenie nie skonfigurowanego konstruktora
    /// </summary>
    public static VirtualList<T>.Creator Create<T>() => new();

    /// <summary>
    /// Tworzenie konstruktora z tablicy
    /// </summary>
    public static VirtualList<T>.Creator Create<T>(T[] array) =>
        Create<T>()
        .GetMethod(i => array[i])
        .CountMethod(() => array.Length)
        .IsContainMethod(o => ((IList<T>)array).Contains(o))
        .EnumerableMethod(() => array);

    /// <summary>
    /// Tworzenie konstruktora z <see cref="IList{T}"/>
    /// </summary>
    public static VirtualList<T>.Creator Create<T>(IList<T> list) =>
        Create<T>()
        .GetMethod(i => list[i])
        .CountMethod(() => list.Count)
        .IsContainMethod(list.Contains)
        .EnumerableMethod(() => list);
}

/// <summary>
/// <inheritdoc cref="VirtualDictionaty{TKey, TValue}"/>
/// </summary>
public static class VirtualDictionaty
{
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>() => new();
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>(IDictionary<TKey, TValue> dictionary) => Create<TKey, TValue>()
        .GetMethod(d => dictionary[d])
        .CountMethod(() => dictionary.Count)
        .EnumerableMethod(() => dictionary)
        .IsConteinedMethod(dictionary.ContainsKey);
}