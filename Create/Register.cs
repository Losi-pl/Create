using Create.Elements;
using Create.Virtuals;
using Create.OpenGL;
using Create.Space;
using System.Reflection;
using Create.Initialization;
using SixLabors.ImageSharp;
using Create.Resource;
using System.Collections;
using System.Diagnostics;
using Create.Sceans;
using System.Runtime.Versioning;

namespace Create;

/// <summary>
/// Podstawowa klasa zawierająca rejesty elementów
/// </summary>
[Mod("1.0.0.0", "create")]
public static class Register
{
    internal static readonly ElementRegister<Block>.Console blocks_console = new();
    internal static readonly ElementRegister<Dimention>.Console dimentions_console = new();
    internal static readonly ElementRegister<Entity>.Console entitys_console = new();

    public static readonly ElementRegister<Block> Blocks = blocks_console.Register;
    public static readonly ElementRegister<Dimention> Dimentions = dimentions_console.Register;
    public static readonly ElementRegister<Entity> Entitys = entitys_console.Register;

    internal static readonly Dictionary<(string name, Type type), Func<object?, (UserInterface status, OpenGL.GUI.SpacePoint point)>> userinterfaces = new();

    public static UserInterface CreateUserInterface(string name, object? sender = null)
    {
        if (!userinterfaces.TryGetValue(k => k.name == name, out var func))
            throw new ArgumentException("Interface shemat not found", nameof(name));

        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        var rez = func.Invoke(sender);

        gam.Interface.MainElements.Find("Active Interface", false)?.Childs.AddChild(rez.point);
        return rez.status;
    }

    [RequiresPreviewFeatures]
    public static T CreateUserInterface<T>(object? sender = null) where T : UserInterface, IUserInterface<T>
    {
        if (!userinterfaces.TryGetValue(k => k.type == typeof(T), out var func))
            throw new ArgumentException("Interface shemat with that type doesn't exist");

        var gam = OpenGL.Engine.Scean as GameView;
        if (gam is null)
            throw new Exception("Game isn't active");
        var rez = func.Invoke(sender);

        gam.Interface.MainElements.Find("Active Interface", false)?.Childs.AddChild(rez.point);
        return (T)rez.status;
    }

    /// <summary>
    /// Załadowanie podstawowego pakietu zasobów
    /// </summary>
    /// <returns></returns>
    internal static Resources create_resource()
    {
        #if DEBUG
        var resource_path = Assembly.GetExecutingAssembly().Location;
        {
            int rem_from = 0;
            for (int i = 0; i < resource_path.Length; i++)
                if (resource_path[i] is '\\')
                    rem_from = i;
            resource_path = resource_path?.Remove(rem_from);
            resource_path = Path.GetFullPath($"{resource_path}/../../../../../Create/Resources/");
        }

        var resources = Resources.CreateFromDirectory().AddFolder(resource_path, "assets/create/", path =>
        {
            string pat = path.RegisterPath.ToLower();

            string root, file;

            {
                int last = 0;
                for (int i = 0; i < pat.Length; i++)
                    if (pat[i] is '\\' or '/')
                        last = i;
                root = pat.Remove(last + 1);
                file = pat.Substring(last + 1);
            }
            {
                if (file[0] != '.')
                {
                    int last = 0;
                    for (int i = 0; i < file.Length; i++)
                        if (file[i] is '.')
                            last = i;
                    file = file.Remove(last);
                }
            }
            path.SubPath($"{root}{file}");
        }).Finish();
        #else
        var resource_path = Assembly.GetExecutingAssembly().Location;
        {
            int rem_from = 0;
            for (int i = 0; i < resource_path.Length; i++)
                if (resource_path[i] is '\\')
                    rem_from = i;
            resource_path = resource_path?.Remove(rem_from);
            resource_path = Path.GetFullPath($"{resource_path}/create.resources");
        }
        var resources = Resources.CreateFromFile().FromFile(resource_path).Finish();
        #endif

        load_icon();

        return resources;

        //Metohds
        void load_icon()
        {
            var icon_file = resources.GetPath("assets/create/textures/create").GetFile("icon");
            var icon = Image.Load(icon_file.GetStream());
            OpenGL.Engine.SetIcon(icon);
        }
    }

    /// <summary>
    /// Załadowanie wrzystkich modyfikacji z <paramref name="mod_assemblys"/>
    /// </summary>
    /// <param name="mod_assemblys"></param>
    internal static void load_mods((Assembly? assembly, Resources resource)[] mod_assemblys)
    {
        var all_mods = ((IEnumerable<(Assembly? assembly, Resources resource)>)mod_assemblys).ConvertAll(z =>
        {
            (Assembly assembly, Resources resource) @as = (null!, z.resource);

            if (z.assembly != null)
                @as.assembly = z.assembly;
            else
            {
                Stream assembly;
                Stream? symbols = null;

                {
                    var code = z.resource.GetPath("src");
                    assembly = code.GetFile("assembly").GetStream();
                    if (code.IsFileExist("symbols"))
                        symbols = code.GetFile("symbols").GetStream();
                }
                @as.assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(assembly, symbols);
            }

            return @as;
        })
        .ConvertAll(a =>
        {
            var main = find_main_class(a.assembly);
            if (!main.HasValue)
                return (null, null, null)!;
            var method = find_starter_method(main.Value.@class);
            if (method == null)
                return (null, null, null)!;
            Mod mod = new(main.Value.attribute.CodeName, new(main.Value.attribute.Version), a.resource);
            return (mod, method, a.resource);
        })
        .Where(m => m.mod != null)
        .ToArray();

        foreach (var mod in all_mods)
            Mod.add_to_mod_list(mod.mod);

        Assets.load_resources();
        Assets.first_proces_resources();

        foreach (var mod in all_mods)
            mod.method(mod.mod);

        //Methods
        Action<Mod>? find_starter_method(Type @class)
        {
            var methods = new[] {
                @class.GetMethods(BindingFlags.Static | BindingFlags.Public),
                @class.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
            }.Combine();
            foreach (var method in methods)
                if (method.GetCustomAttribute<ModIniterAttribute>() != null)
                {
                    var inputs = method.GetParameters();
                    if (inputs.Length != 1)
                        continue;
                    if (inputs[0].ParameterType != typeof(Mod))
                        continue;
                    return (m => method.Invoke(null, new[] { m }));
                }
            return null;
        }
        (Type @class, ModAttribute attribute)? find_main_class(Assembly assembly)
        {
            foreach (var obj in assembly.GetTypes())
            {
                var ModData = obj.GetCustomAttribute<ModAttribute>();
                if (ModData != null)
                {
                    return (obj, ModData);
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Metoda inicjalizacyjna elementów
    /// </summary>
    /// <param name="mod">Klasa modyfikacji</param>
    [ModIniter]
    static void load_create(Mod mod)
    {
        bazic_setup();
        get_all_elements<Block>(typeof(Blocks))
            .ForEvery(e => mod.RegisterElement(e.name, e.element));
        get_all_elements<Dimention>(typeof(Dimentions))
            .ForEvery(e => mod.RegisterElement(e.name, e.element));
        get_all_elements<Entity>(typeof(Entitys))
            .ForEvery(e => mod.RegisterElement(e.name, e.element));
        Assets.load_elements(mod);
        UserInterface.LoadInterfaces(mod);
    }

    /// <summary>
    /// Wywoływana przed załadowaniem elementów
    /// </summary>
    static void bazic_setup()
    {

    }

    /// <summary>
    /// Ładuje wrzystkie elementy z klasy <paramref name="where"/>
    /// </summary>
    /// <typeparam name="T">Typ ładowanych elementów</typeparam>
    /// <param name="where">Z kąd te elementy mają być ładowane</param>
    /// <returns></returns>
    internal static IEnumerable<(T element, string name)> get_all_elements<T>(Type where) where T : Baze =>
        where.GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(T))
            .Where(t => t.GetCustomAttribute<IgnoreAttribute>() == null)
            .ConvertAll(t =>
            {
                T d = (T)t.GetValue(null)!;
                string name;
                var att = t.GetCustomAttribute<ElementNameAttribute>();
                if (att != null)
                    name = att.Name;
                else
                    name = t.Name.ToLower().Replace('_', '-');
                return (d, name);
            });

    /// <summary>
    /// Ustawienie innej nazwy elementu
    /// </summary>
    internal class ElementNameAttribute : Attribute
    {
        string new_name;
        public ElementNameAttribute(string name)
        {
            new_name = name;
        }
        public string Name => new_name;
    }
    
    /// <summary>
    /// Pominięcie w ładowaniu danego elementu
    /// </summary>
    internal class IgnoreAttribute : Attribute { }

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
        object task_lock = new();
        static object static_task_lock = new();

        /// <summary>
        /// Elementy rejestru po nazwie
        /// </summary>
        public VirtualDictionaty<string, T> ByName => by_name;

        /// <summary>
        /// Elementy rejestru po numerze
        /// </summary>
        public VirtualDictionaty<ushort, T> ById => by_id;

        public ElementRegister()
        {
            list = new();
            by_name = new VirtualDictionaty<string, T>.Constructor()
                .GetMethod(n => list.Find(l => l.CodeName == n, new KeyNotFoundException()))
                .CountMethod(() => list.Count)
                .IsConteinedMethod(e => list.FindAny(E => E.CodeName == e))
                .EnumerableMethod(() => ((IEnumerable<T>)list).ConvertAll(e => new KeyValuePair<string, T>(e.CodeName, e)))
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
                    return ((IEnumerable<T>)list).ConvertAll(e => new KeyValuePair<ushort, T>(e.Id, e));
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
}
