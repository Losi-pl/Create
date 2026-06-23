using System.Collections;
using System.Diagnostics;
using Create.Elements;
using Create.Linq;
using Create.Virtuals;

namespace Create.Registry;

/// <summary>
/// Rejestr elementów typu <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T">Typ elementu</typeparam>
[DebuggerDisplay("{class_name(),nq}")]
[DebuggerTypeProxy(typeof(ElementRegister<>.Proxy))]
public class ElementRegister<T> : IEnumerable<T> where T : Baze
{
    List<T> list;

    /// <summary>
    /// Debugowy podgląd klasy
    /// </summary>
    /// <returns></returns>
    string class_name() => $"{typeof(T).Name} Register: {list.Count}";

    /// <summary>
    /// Enumerator wrzystkich elementów w rejestrze
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

    /// <summary>
    /// <inheritdoc cref="GetEnumerator"/>
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    VirtualDictionaty<ushort, T> by_id;
    VirtualDictionaty<string, T> by_name;
    VirtualList<T> v_list;
    object task_lock = new();
    static object static_task_lock = new();

    /// <summary>
    /// Elementy rejestru po nazwie
    /// </summary>
    public VirtualDictionaty<string, T> ByName => by_name; // TODO - Switch to use ReadOnlySpan<>

    /// <summary>
    /// Elementy rejestru po numerze
    /// </summary>
    public VirtualDictionaty<ushort, T> ById => by_id;

    /// <summary>
    /// Lista elementów w kolejności od dodania
    /// </summary>
    public VirtualList<T> List => v_list;

    public ElementRegister()
    {
        list = new();
        v_list = VirtualList.Create(list).Finish();
        by_name = new VirtualDictionaty<string, T>.Constructor()
            .GetMethod(n => list.Find(l => l.CodeName == n, new KeyNotFoundException()))
            .CountMethod(() => list.Count)
            .IsConteinedMethod(e => list.FindAny(E => E.CodeName == e))
            .EnumerableMethod(() => list.Select(e => new KeyValuePair<string, T>(e.CodeName, e)))
            .Finsh();

        by_id = new VirtualDictionaty<ushort, T>.Constructor()
            .GetMethod(id =>
            {
                if (!Baze.are_ids_binded)
                    throw new Exception("Id's are not binded");
                return list.Find(l => l.Id == id, new KeyNotFoundException());
            })
            .CountMethod(() => Baze.are_ids_binded ? list.Count : 0)
            .IsConteinedMethod(id => list.FindAny(E => E.IdBinded ? E.Id == id : false))
            .EnumerableMethod(() =>
            {
                if (!Baze.are_ids_binded)
                    throw new Exception("Id's are not binded");
                return list.Select(e => new KeyValuePair<ushort, T>(e.Id, e));
            })
            .Finsh();
    }

    /// <summary>
    /// Konsole do edycji rejestru
    /// </summary>
    public class Console
    {
        ElementRegister<T> register = new();

        /// <summary>
        /// Rejestr sparowany z konsolą
        /// </summary>
        public ElementRegister<T> Register => register;

        /// <summary>
        /// Rejestrowanie nowego elementu w rejestrze
        /// </summary>
        /// <param name="element">Element do zajerestrowania</param>
        /// <param name="name">Nazwa elementu</param>
        /// <param name="mod">Modyfikacja pochodzenia</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterElement(T element, string name, Mod mod)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (mod == null)
                throw new ArgumentNullException(nameof(mod));

            if (element.IsRegistered)
                throw new ArgumentException($"This element is alredy registered");
            if (register.ByName.ContainsKey($"{mod.Name}:{name}"))
                throw new ArgumentException($"Element width name \"{mod.Name}:{name}\" is alredy registered");

            lock (register.task_lock)
                register.list.Add(element);
            lock (ElementRegister<T>.static_task_lock)
                element.set_bazic_informations(mod, name);
        }
    }

    /// <summary>
    /// Debugowy podgląd rejestru
    /// </summary>
    private class Proxy
    {
        ElementRegister<T> elms;

        public Proxy(ElementRegister<T> elements) => elms = elements;

        /// <summary>
        /// Elementy po nazwie
        /// </summary>
        public VirtualDictionaty<string, T> Names => elms.ByName;

        /// <summary>
        /// Elementy po numerze
        /// </summary>
        public VirtualDictionaty<ushort, T> IDs => elms.ById;
    }
}