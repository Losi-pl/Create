using System.Collections.Concurrent;
using System.Collections.Frozen;
using Create.Assets;
// ReSharper disable UnusedMember.Global

namespace Create.Registry;

public interface IMod
{
    private static readonly ConcurrentDictionary<IMod, IResources> AutoGenResources = new();
    
    /// Name of the modification
    public string Name { get; }
    
    /// Description of the modification
    public string Description => string.Empty;
    /// Creators of the modification
    public string[] Authors => [];
    /// Version of the modification
    public Version Version { get; }
    /// Optional links to a webside related to the mod
    public string[]? Urls => [];
    /// The source of the game assets contained in this mod
    public IResources Resources => AutoGenResources.GetOrAdd(this, mod => new AssemblyResources(mod.GetType().Assembly));
    /// <summary>
    /// For registering of the loading processes related to this mod
    /// </summary>
    /// <param name="entry">The API to allow for registration</param>
    internal void RegisterLoadingPrecesses(LoadingSystem entry);

    public sealed string Identity => Identitys[this] ?? throw new InvalidOperationException("This is an abstract mod");
    
    /// <summary>
    /// Marks if this is a virtual mod meaning that it does not actually exist
    /// </summary>
    /// <remarks>For cases where a reference to specific a mod is needed but the mod in question is absent</remarks>
    public sealed bool IsAbstract => !Identitys.TryGetValue(this, out _);
    
    /// <summary>
    /// List of all mods in the game
    /// </summary>
    /// <exception cref="InvalidOperationException">Mod list has not yet been loaded</exception>
    public static sealed FrozenDictionary<string, IMod> Mods { get => field ?? throw new InvalidOperationException("Mod list has not yet been loaded");
        internal set
        {
            field = value;
            Identitys = value.Select(kvp => new KeyValuePair<IMod, string>(kvp.Value, kvp.Key)).ToFrozenDictionary();
        }
    }
    
    private static FrozenDictionary<IMod, string> Identitys { get => field ?? throw new InvalidOperationException("Mod list has not yet been loaded"); set; }
}