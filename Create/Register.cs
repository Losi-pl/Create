using Create.Elements;
using Create.Virtuals;
using Create.OpenGL;
using Create.Space;
using System.Reflection;
using Create.Initialization;
using SixLabors.ImageSharp;
using Create.Resource;
using Create.Render;
using System.Xml.Linq;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Create;

[Mod("1.0.0.0", "create")]
public static class Register
{
    internal static readonly ElementRegister<Block>.Console blocks_console = new();
    internal static readonly ElementRegister<Dimention>.Console dimentions_console = new();
    internal static readonly ElementRegister<Entity>.Console entitys_console = new();

    public static readonly ElementRegister<Block> Blocks = blocks_console.Register;
    public static readonly ElementRegister<Dimention> Dimentions = dimentions_console.Register;
    public static readonly ElementRegister<Entity> Entitys = entitys_console.Register;

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

        foreach (var resource in all_mods)
            load_resources(resource.resource);

        foreach (var mod in all_mods)
            mod.method(mod.mod);

        Textures.finish_attlas();

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
    }

    static void bazic_setup()
    {

    }

    static void load_resources(Resources resources)
    {
        var assest = resources.GetPath("assets");
        foreach(var resors in assest.SubPaths)
        {
            #if DEBUG
            textures(resors);
            #else
            try
            {
                textures(resors);
            }
            catch (Exception ex)
            { throw new($"Ładowanie pakietu {resors.Name} niepowiodło się", ex); }
            #endif
        }

        //Methods
        void textures(ResourceDirectory directory)
        {
            string pack_name = directory.Name;
            directory = directory.GetSubPath("textures");
            foreach (var texture in directory.GetSubPath("blocks").Files)
                Textures.set_texture(load_image(texture.GetStream(), texture.Name), $"{pack_name}:{texture.Name}");

            //Methods
            Image load_image(Stream stream, string name)
            {
                #if DEBUG
                return Image.Load(stream);
                #else
                try
                { return Image.Load(stream); }
                catch (Exception ex)
                { throw new($"Konwertowanie tekstury {pack_name}:{name} niepowiodło się", ex); }
                #endif
            }
        }
    }

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

    internal class ElementNameAttribute : Attribute
    {
        string new_name;
        public ElementNameAttribute(string name)
        {
            new_name = name;
        }
        public string Name => new_name;
    }
    internal class IgnoreAttribute : Attribute { }

    [DebuggerDisplay("{class_name}")]
    [DebuggerTypeProxy(typeof(ElementRegister<>.Proxy))]
    public class ElementRegister<T> : IEnumerable<T> where T : Baze
    {
        List<T> list;

        string class_name => $"{typeof(T).Name} elements: {list.Count}";

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        VirtualDictionaty<ushort, T> by_id;
        VirtualDictionaty<string, T> by_name;
        object task_lock = new();
        static object static_task_lock = new();

        public VirtualDictionaty<string, T> ByName => by_name;
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
        public class Console
        {
            ElementRegister<T> register = new();

            public ElementRegister<T> Register => register;

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

        private class Proxy
        {
            ElementRegister<T> elms;

            public Proxy(ElementRegister<T> elements) => elms = elements;

            public VirtualDictionaty<string, T> Names => elms.ByName;
            public VirtualDictionaty<ushort, T> IDs => elms.ById;
        }
    }
}
