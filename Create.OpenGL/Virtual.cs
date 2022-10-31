using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Create.Virtuals;

public static class VirtualList
{

    public static VirtualList<T>.Creator Create<T>() => new();
    public static VirtualList<T>.Creator Create<T>(T[] array) =>
        Create<T>()
        .GetMethod(i => array[i])
        .CountMethod(() => array.Length)
        .IsContainMethod(o => ((IList<T>)array).Contains(o))
        .EnumerableMethod(() => array);
    public static VirtualList<T>.Creator Create<T>(IList<T> list) =>
        Create<T>()
        .GetMethod(i => list[i])
        .CountMethod(() => list.Count)
        .IsContainMethod(list.Contains)
        .EnumerableMethod(() => list);
}
public static class VirtualDictionaty
{
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>() => new();
    public static VirtualDictionaty<TKey, TValue>.Constructor Create<TKey, TValue>(IDictionary<TKey, TValue> dictionary) => Create<TKey, TValue>()
        .GetMethod(d => dictionary[d])
        .CountMethod(() => dictionary.Count)
        .EnumerableMethod(() => dictionary)
        .IsConteinedMethod(dictionary.ContainsKey);
}