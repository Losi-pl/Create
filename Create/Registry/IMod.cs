using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
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
    /// Optional links to a websides related to the mod
    public string[]? Urls => [];
    /// The source of the game assets contained in this mod
    public IResources Resources => AutoGenResources.GetOrAdd(this, mod => new AssemblyResources(mod.GetType().Assembly));
    /// <summary>
    /// For registering of the loading processes related to this mod
    /// </summary>
    /// <param name="entry">The API with registration tools</param>
    internal void RegisterLoadingPrecesses(LoadingSystem entry);

    /// <summary>
    /// The code based name of the mod
    /// </summary>
    /// <exception cref="NotSupportedException">If this is an unregistered instance of a Mod</exception>
    public sealed string Identity
    {
        get
        {
            if(Identities.TryGetValue(this, out var identity))
                return identity;
            if(this is AbstractMod abs)
                lock (_abstracts)
                    if(_abstracts.TryGetBySecond(abs, out identity))
                        return identity;
            throw new NotSupportedException("This object was not created by create code");
        }
    }
    /// <summary>
    /// Marks if this is a virtual mod meaning that it does not actually exist
    /// </summary>
    /// <remarks>For cases where a reference to specific a mod is needed but the mod in question is absent</remarks>
    public sealed bool IsAbstract => !Identities.ContainsKey(this);
    /// <summary>
    /// List of all mods in the game
    /// </summary>
    /// <exception cref="InvalidOperationException">Mod list has not yet been loaded</exception>
    public static sealed FrozenDictionary<string, IMod> Mods { get => field ?? throw new InvalidOperationException("Mod list has not yet been loaded");
        internal set
        {
            field = value;
            Identities = value.Select(kvp => new KeyValuePair<IMod, string>(kvp.Value, kvp.Key)).ToFrozenDictionary();
        }
    }
    /// <summary>
    /// Finds a mod by its identity eather direct or from an element
    /// </summary>
    /// <param name="identity">Format <c>create</c> or <c>create:stone</c></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException">When a mod of that identity could not be found</exception>
    /// <exception cref="ArgumentException">When the formating of the identity is completely incorrect</exception>
    public static IMod FromIdentity(string identity)
    {
        var semPos = identity.IndexOf(':');
        
        if (semPos == -1)// No : => That's just mod identity
        {
            if(Mods.TryGetValue(identity, out var mod))
                return mod;
            throw new KeyNotFoundException($"Mod {identity} not found");
        }
            
        if(semPos != identity.LastIndexOf(':'))// Last and first : are not the same => There is more than one :
            throw new ArgumentException($"Identity '{identity}' has invalid format");
        {
            var modName = identity.AsSpan()[..semPos];
            if(Mods.GetAlternateLookup().TryGetValue(modName, out var mod))
                return mod;
            throw new KeyNotFoundException($"Mod {modName} not found");
        }
    }
    /// <summary>
    /// A version of the <see cref="FromIdentity"/> method with added checks for identity validity or mod presence.
    /// Will still throw an exception if the full mod list has not yet been loaded.
    /// </summary>
    /// <param name="identity">Format <c>create</c> or <c>create:stone</c></param>
    /// <param name="mod">The found mod</param>
    /// <returns><c>true</c> on success, otherwise <c>false</c></returns>
    public static bool TryGetFromIdentity(string identity, [MaybeNullWhen(false)] out IMod mod)
    {
        var semPos = identity.IndexOf(':');
        
        if (semPos == -1)// No : => That's just mod identity
            return Mods.TryGetValue(identity, out mod);
            
        if(semPos != identity.LastIndexOf(':'))// Last and first : are not the same => There is more than one :
        { mod = null; return false; }
        
        var modName = identity.AsSpan()[..semPos];
        return Mods.GetAlternateLookup().TryGetValue(modName, out mod);
    }
    /// <summary>
    /// Returns an abstract mod of a specific identity if no mod with it is registered.
    /// </summary>
    /// <param name="identity">Expected identity</param>
    /// <exception cref="ArgumentException">When the format of the identity is invalid.</exception>
    /// <exception cref="ArgumentException">When a genuine mod with that identity is present.</exception>
    public static IMod GetAbstract(string identity)
    {
        if(Mods.ContainsKey(identity))
            throw new ArgumentException($"Genuine Mod '{identity}' is present");
        if(!IsIdentityValid(identity))
            throw new ArgumentException($"Identity '{identity}' is not in valid format");
        lock (_abstracts)
        {
            if (_abstracts.TryGetByFirst(identity, out var mod))
                return mod;
            _abstracts.Add(identity, mod = new(identity));
            return mod;
        }
    }
    /// <summary>
    /// Will try to find a mod of a specified identity, if none is found will return an abstract mod object of that identity.
    /// </summary>
    /// <param name="identity">Format <c>create</c> or <c>create:stone</c></param>
    /// <returns>A mod object or it's abstract form if none is present</returns>
    /// <exception cref="ArgumentException">When the format of the identity is invalid.</exception>
    public static IMod FromIdentityOrAbstract(string identity)
    {
        if (TryGetFromIdentity(identity, out var found))
            return found;
        
        var semPos = identity.IndexOf(':');
        
        if (semPos == -1)// No : => That's just mod identity
            return GetAbstract(identity);
            
        if(semPos != identity.LastIndexOf(':'))// Last and first : are not the same => There is more than one :
            throw new ArgumentException($"Identity '{identity}' has invalid format");
        
        var modName = identity.AsSpan()[..semPos];
        return GetAbstract(new(modName));
    }
    /// <summary>
    /// Checks if the format of a mods identity is valid
    /// </summary>
    /// <param name="identity">Allowed chars <c>a-z</c>, <c>A-Z</c>, <c>0-9</c>, <c>.</c>, <c>-</c>, <c>_</c></param>
    /// <returns></returns>
    public static bool IsIdentityValid(string identity)
    {
        foreach (var letter in identity)
        {
            if (char.IsAsciiLetter(letter))
                continue;
            if(char.IsNumber(letter))
                continue;
            if(letter is '.' or '-' or '_')
                continue;
            return false;
        }
        return true;
    }
    
    private static FrozenDictionary<IMod, string> Identities { get => field ?? throw new InvalidOperationException("Mod list has not yet been loaded"); set; }
    private static BiDictionaryOneToOne<string, AbstractMod> _abstracts = new();
    
    /// <summary>
    /// If no mod of that identity is registered but a mod object is needed regardless this abstraction is created for that identity holding only that information
    /// </summary>
    /// <param name="name">Abstract Mod Identity</param>
    private class AbstractMod(string name): IMod
    {
        public string Name => name;
        public Version Version { get => field ??= Mods["create"].Version; } = null;
        void IMod.RegisterLoadingPrecesses(LoadingSystem entry) => throw new InvalidOperationException();
    }
}