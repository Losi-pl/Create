using Create.Elements;
using Create.OpenGL;
using Create.Space;
using System.Reflection;
using Create.Initialization;
using SixLabors.ImageSharp;
using Create.Resource;
using Create.Linq;
using Create.Render;
using Create.Render.ModelCreators.BlockModels;
using Create.Elements.Recipes;
using Create.Registry;

namespace Create;

/// <summary>
/// Base class containing all Registers and an entry point for logic of their loading 
/// </summary>
[Mod("1.0.0.0", "create")]
public static class Register
{
    internal static readonly ElementRegister<Block>.Console BlocksConsole = new();
    internal static readonly ElementRegister<Dimention>.Console DimensionsConsole = new();
    internal static readonly ElementRegister<Entity>.Console EntitiesConsole = new();
    internal static readonly ElementRegister<Item>.Console ItemsConsole = new();
    internal static readonly ElementRegister<CreativeTab>.Console CreativeTabsConsole = new();

    public static readonly ElementRegister<Block> Blocks = BlocksConsole.Register;
    public static readonly ElementRegister<Dimention> Dimensions = DimensionsConsole.Register;
    public static readonly ElementRegister<Entity> Entities = EntitiesConsole.Register;
    public static readonly ElementRegister<Item> Items = ItemsConsole.Register;
    public static readonly ElementRegister<CreativeTab> CreativeTabs = CreativeTabsConsole.Register;

    internal static (Dictionary<(Mod mod, string name), (IRecipeBaze recipe, int index)> recipes, List<(Type, Func<RecipeIngredients, object?>)> types) recipes = (new(), new());
    internal static readonly Dictionary<(string name, Type type), Func<UserInterface.InterfaceCreatorArgs, (UserInterface status, OpenGL.GUI.SpacePoint point)>> userinterfaces = new();

    /// <summary>
    /// Loads resources of the Game itself
    /// </summary>
    /// <returns></returns>
    internal static Resources GetPrimaryResources()
    {
        #if DEBUG
        // Get path to the resources folder
        var resourcePath = Assembly.GetExecutingAssembly().Location;
        resourcePath = resourcePath.Remove(resourcePath.LastIndexOf('\\'));
        resourcePath = Path.GetFullPath($"{resourcePath}/../../../../../Create/Resources/");

        // Load resources from the folder
        var resources = Resources.CreateFromDirectory().AddFolder(resourcePath, "assets/create/", path =>
        {
            var tPath = path.RegisterPath.ToLower();
            string root, file;

            // Separating root path and file
            if (tPath.LastIndexOfAny(out var poz, '/', '\\'))
            {
                root = tPath.Remove(poz + 1);
                file = tPath.Substring(poz + 1);
            }
            else
                (root, file) = ("", tPath);

            // Removing extension
            if (file[0] != '.')
                file = file.SubstringBeforeLast('.');

            path.SubPath($"{root}{file}");
        }).Finish();
        #else
        var resourcesPath = Assembly.GetExecutingAssembly().Location;
        
        resourcesPath = resourcesPath.Remove(resourcesPath.LastIndexOf('\\'));
        resourcesPath = Path.GetFullPath($"{resourcesPath}/create.resources");
        
        var resources = Resources.CreateFromFile().FromFile(resourcesPath).Finish();
        #endif

        LoadIcon();
        LoadMainShaders();

        return resources;

        // Load data components
        void LoadIcon()
        {
            // Accessing icon file
            var iconFile = resources.GetPath("assets/create/textures/create").GetFile("icon");

            // Load icon
            var icon = Image.Load(iconFile.GetStream());

            // Set icon
            OpenGL.Engine.SetIcon(icon);
        }
        void LoadMainShaders()
        {
            var renderLayer = LoadShader("assets/create/shaders/basic", "render-layer");
            var imageElement = LoadShader("assets/create/shaders/interface", "image");
            MainTask.Run(() => RenderLayer.set_shader(renderLayer));
            MainTask.Run(() => OpenGL.GUI.Elements.Image.set_shader(imageElement));

            Shader LoadShader(string path, string file)
            {
                var stream = resources.GetPath(path).GetFile(file).GetStream();
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
        MainTask.Run(() => RenderLayer.set_shader(Assets.GetShader("create:basic/render-layer")));
        MainTask.Run(() => OpenGL.GUI.Elements.Image.set_shader(Assets.GetShader("create:interface/image")));
    }

    [ModIniter(InitjalizationStage.Finishing)]
    static void finishing_toutches(Mod mod)
    {
        foreach (var ct in CreativeTabs)
            ct.load_stacks();
    }
}
