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
using Create.Linq;
using Create.Render;
using Create.Render.ModelCreators.BlockModels;
using Create.Elements.Recipes;

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
    internal static readonly ElementRegister<Item>.Console items_console = new();
    internal static readonly ElementRegister<CreativeTab>.Console creativetab_console = new();

    public static readonly ElementRegister<Block> Blocks = blocks_console.Register;
    public static readonly ElementRegister<Dimention> Dimentions = dimentions_console.Register;
    public static readonly ElementRegister<Entity> Entitys = entitys_console.Register;
    public static readonly ElementRegister<Item> Items = items_console.Register;
    public static readonly ElementRegister<CreativeTab> CreativeTabs = creativetab_console.Register;

    internal static (Dictionary<(Mod mod, string name), (IRecipeBaze recipe, int index)> recipes, List<(Type, Func<RecipeIngredients, object?>)> types) recipes = (new(), new());
    internal static readonly Dictionary<(string name, Type type), Func<UserInterface.InterfaceCreatorArgs, (UserInterface status, OpenGL.GUI.SpacePoint point)>> userinterfaces = new();

    /// <summary>
    /// Załadowanie podstawowego pakietu zasobów
    /// </summary>
    /// <returns></returns>
    internal static Resources create_resource()
    {
        #if DEBUG
        
        // Get path to the resources folder
        var resource_path = Assembly.GetExecutingAssembly().Location;
        resource_path = resource_path.Remove(resource_path.LastIndexOf('\\'));
        resource_path = Path.GetFullPath($"{resource_path}/../../../../../Create/Resources/");

        // Load resources from the folder
        var resources = Resources.CreateFromDirectory().AddFolder(resource_path, "assets/create/", path =>
        {
            string pat = path.RegisterPath.ToLower();
            string root, file;

            // Seperating root path and file
            if (pat.LastIndexOfAny(out var poz, '/', '\\'))
            {
                root = pat.Remove(poz + 1);
                file = pat.Substring(poz + 1);
            }
            else
            {
                root = "";
                file = pat;
            }

            // Removing extension
            if (file[0] != '.')
                file = file.Remove(file.LastIndexOf('.'));

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
        load_main_shaders();

        return resources;

        // Load data components
        void load_icon()
        {
            // Accessing icon file
            var icon_file = resources.GetPath("assets/create/textures/create").GetFile("icon");

            // Load icon
            var icon = Image.Load(icon_file.GetStream());

            // Set icon
            OpenGL.Engine.SetIcon(icon);
        }
        void load_main_shaders()
        {
            var renderLayer = load_shader("assets/create/shaders/bazic", "renderlayer");
            var imageElement = load_shader("assets/create/shaders/interface", "image");
            MainTask.Run(() => RenderLayer.set_shader(renderLayer));
            MainTask.Run(() => OpenGL.GUI.Elements.Image.set_shader(imageElement));

            Shader load_shader(string path, string file)
            {
                var stream = resources!.GetPath(path).GetFile(file).GetStream();
                return MainTask.Run(() => Shader.Load(stream));
            }
        }
    }

    /// <summary>
    /// Załadowanie wrzystkich modyfikacji z <paramref name="mod_assemblys"/>
    /// </summary>
    /// <param name="mod_assemblys"></param>
    internal static void load_mods((Assembly? assembly, Resources resource)[] mod_assemblys)
    {
        var all_mods = mod_assemblys.Select(z =>
        {
            if (z.assembly is null)
            {
                Stream assembly;
                Stream? symbols = null;

                var code = z.resource.GetPath("src");

                assembly = code.GetFile("assembly").GetStream();
                if (code.IsFileExist("symbols"))
                    symbols = code.GetFile("symbols").GetStream();

                z.assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(assembly, symbols);
            }

            return z;
        }).Select(a =>
        {
            if (!FindModToutchPoint(a.assembly!).IsNotNull(out var main))
                return (null, new(), null)!;
            var method = FindModStartupMethods(main.@class);
            if (method.initial == null && method.main == null && method.finishing == null) // TODO - Shorten this
                return (null, new(), null)!;
            Mod mod = new(main.attribute.CodeName, new(main.attribute.Version), a.resource);
            return (mod, method, a.resource);
        })
        .Where(m => m.mod != null)
        .ToArray();

        foreach (var mod in all_mods)
            Mod.add_to_mod_list(mod.mod);

        Assets.LoadGlobalResources();
        Assets.first_proces_resources();

        foreach (var mod in all_mods)
            mod.method.initial?.Invoke(mod.mod);

        foreach (var mod in all_mods)
            mod.method.main?.Invoke(mod.mod);

        foreach (var mod in all_mods)
            mod.method.finishing?.Invoke(mod.mod);

        //Methods
        (Action<Mod>? initial, Action<Mod>? main, Action<Mod>? finishing) FindModStartupMethods(Type @class)
        {
            Action<Mod>? initial = null, main = null, finishing = null;


            var methods = new[] {
                @class.GetMethods(BindingFlags.Static | BindingFlags.Public),
                @class.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
            }.Combine();

            foreach (var m in methods.Select(m => (Method: m, Attr: m.GetCustomAttribute<ModIniterAttribute>())))
            {
                if (m.Attr is null)
                    continue;
                
                var inputs = m.Method.GetParameters();
                if (inputs.Length != 1)
                    continue;
                
                if (inputs[0].ParameterType != typeof(Mod))
                    continue;

                switch(m.Attr.Stage)
                {
                    case InitjalizationStage.Initial: initial = mod => m.Method.Invoke(null, new[] { mod }); break;
                    case InitjalizationStage.Main: default: main = mod => m.Method.Invoke(null, new[] { mod }); break;
                    case InitjalizationStage.Finishing: finishing = mod => m.Method.Invoke(null, new[] { mod }); break;
                };
            }
            return (initial, main, finishing);
        }

        (Type @class, ModAttribute attribute)? FindModToutchPoint(Assembly assembly)
        {
            foreach (var obj in assembly.GetTypes()
                .Select(t => (Type: t, ModData: t.GetCustomAttribute<ModAttribute>())))
            {
                if (obj.ModData != null)
                    return (obj.Type, obj.ModData);
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
        IBlockSideModel.Load(mod);
        IBlockModel.Load(mod);
        SourceGenerators.Registers.LoadBlocks(mod);
        SourceGenerators.Registers.LoadDimentions(mod);
        SourceGenerators.Registers.LoadEntitys(mod);
        SourceGenerators.Registers.LoadItems(mod);
        SourceGenerators.Registers.LoadCreativeTabs(mod);
        IRecipe.Load(mod);
        Assets.LoadInterfaceElements(mod);
        UserInterface.LoadInterfaces(mod);
    }

    /// <summary>
    /// Wywoływana przed załadowaniem elementów
    /// </summary>
    [ModIniter(InitjalizationStage.Initial)]
    static void bazic_setup(Mod mod)
    {
        MainTask.Run(() => RenderLayer.set_shader(Assets.GetShader("create:bazic/renderlayer")));
        MainTask.Run(() => OpenGL.GUI.Elements.Image.set_shader(Assets.GetShader("create:interface/image")));
    }

    [ModIniter(InitjalizationStage.Finishing)]
    static void finishing_toutches(Mod mod)
    {
        foreach (var ct in CreativeTabs)
            ct.load_stacks();
    }

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
}
