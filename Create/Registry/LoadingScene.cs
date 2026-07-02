using System.Collections.Frozen;
using System.Reflection;
using Create.Graphics;
using System.Drawing;
using Create.Assets;
using Create.World;

namespace Create.Registry;

/// <summary>
/// This scene is focused around loading of elements and resources and error handling of such
/// </summary>
internal sealed class LoadingScene: Scene
{
    private Task _elementLoading = null!;
    
    protected override void OnConnect()
    {
        Title = "Create: Loading";
        LoadIcon();
        _elementLoading = AsyncLoadGameElements();

        BackgroundColor = Color.FromArgb(255, 27, 72, 8);
    }

    public override void LogicUpdate(double delta)
    {
        if(_elementLoading.IsCompletedSuccessfully)
            SwapScene(new GameSession());
        else if (_elementLoading.IsFaulted)
        {
            Console.WriteLine(_elementLoading.Exception);
            Window.Main.MeGLFW.Close();
        }
    }
    
    private IEnumerable<(Assembly assembly, string identity)> FindAllMods() => [(Assembly.GetCallingAssembly(), "create")];

    private void LoadGameElements()
    {
        var mods = FindAllMods().Select(PhaseTwo).ToArray();
        {
            var dic = new Dictionary<string, IMod>();
            foreach (var mod in mods)
                dic[mod.identity] = mod.entry;
            IMod.Mods = dic.ToFrozenDictionary();
        }
        Dictionary<IMod, List<Action<ElementRegister>>> elementRegisterProcesses = new();
        foreach (var mod in mods)
        {
            var regis = new LoadingSystem(mod.entry, elementRegisterProcesses);
            mod.entry.RegisterLoadingPrecesses(regis);
            regis.Dispose();
        }
        AssetManager.FreezeProcessors();
        AssetManager.LoadResources();
        foreach (var processes in elementRegisterProcesses)
        {
            var elemRegis = new ElementRegister(processes.Key);
            foreach (var process in processes.Value)
                process(elemRegis);
            elemRegis.Dispose();
        }

        GC.Collect();
        Thread.Sleep(500);
        return;

        static (IMod entry, Assembly assembly, string identity) PhaseTwo((Assembly assembly, string identity) data)
        {
            if (data.assembly == typeof(IMod).Assembly)
                return (new CreateEntryPoint(), data.assembly, data.identity);

            foreach (var type in data.assembly.GetTypes())
            {
                if(!type.IsClass)
                    continue;
                if(type.IsAbstract)
                    continue;
                if(!typeof(IMod).IsAssignableFrom(type))
                    continue;
                var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null, Type.EmptyTypes, null);
                if(constructor == null)
                    continue;
                var mod = (IMod)constructor.Invoke(null);
                
                return (mod, data.assembly, data.identity);
            }
            throw new  Exception($"Failed to find valid {nameof(IMod)} class in the {data.identity} mod");
        }
    }
    
    private Task AsyncLoadGameElements()
    {
        var task = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var context = new GraphicContext();
        
        var thread = new Thread(() =>
        {
            try { context.ThreadBind(); }
            catch (Exception e) { task.SetException(e); return; }
                
            try
            {
                LoadGameElements();
                task.SetResult();
            }
            catch (Exception e)
            {
                task.SetException(e);
            }
            finally
            {
                context.Unbind();
                context.Dispose();
            }
        }) { Name = "Loading Game Elements", IsBackground = true };
        thread.Start();
        return task.Task;
    }
    
    /// <summary>
    /// Loads and parses the Icon from the game resources
    /// </summary>
    /// <exception cref="FileNotFoundException">If the file of the icon could not be found</exception>
    /// <exception cref="FileLoadException">If the process of icon parsing had failed</exception>
    private void LoadIcon()
    {
        using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream("Icon.ico");
        if(stream == null)
            throw new FileNotFoundException("Game icon not found");

        var ico = new Ico.Reader.IcoReader().Read(stream);
        if(ico == null)
            throw new FileLoadException("Game icon failed to load properly");

        var icons = new Silk.NET.Core.RawImage[ico.ImageReferences.Count(i => i.IcoType == Ico.Reader.Data.IcoType.Icon)];
        int index = 0;
        foreach (var image in ico.ImageReferences.GetRefEnum())
        {
            if(image.IcoType != Ico.Reader.Data.IcoType.Icon)
                continue;
            
            var decoded = StbImageSharp.ImageResult.FromMemory(ico.GetImage(image), StbImageSharp.ColorComponents.RedGreenBlueAlpha);
            
            icons[index] = new Silk.NET.Core.RawImage(image.Width, image.Height, decoded.Data);
        }

        Icon = icons;
    }

    
}