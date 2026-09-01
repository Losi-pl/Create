using System.Collections;
using System.Collections.Frozen;
using System.Runtime.CompilerServices;
using Create.Registry;

// ReSharper disable MemberCanBePrivate.Global

namespace Create.Assets;

public static class AssetManager
{
    private static Union<Dictionary<Type, (ProcessorSet set, Func<ProcessorSet, ProcessorSet> freezer)>, FrozenDictionary<Type, ProcessorSet>> 
        _processors = new Dictionary<Type, (ProcessorSet set, Func<ProcessorSet, ProcessorSet> frezzer)>();

    private static Union<Dictionary<string, IResourceProcessor>, FrozenDictionary<string, IResourceProcessor>>
        _processorResourceSources = new Dictionary<string, IResourceProcessor>();

    internal static void RegisterProcessor<T>(IResourceProcessor<T> processor, string inAssetPath)
    {
        if(_processors.IsT1)
            throw new InvalidOperationException("Cannot register a processor, register closed");

        if (_processorResourceSources.AsT0.ContainsValue(processor))
            throw new ArgumentException("The processor is already registered");
        
        if (inAssetPath[0] is '\\' or '/')
            inAssetPath = inAssetPath[1..];
        if (inAssetPath[^1] is not ('\\' or '/'))
            inAssetPath += "/";
        if (inAssetPath.Contains('\\'))
            inAssetPath = inAssetPath.Replace('\\', '/');

        foreach (var path in _processorResourceSources.AsT0.Keys)
            if (path.StartsWith(inAssetPath) || inAssetPath.StartsWith(path))
                throw new ArgumentException($"Path for you're processor is locked by \"{path}\"");
        
        if(_processors.AsT0.TryGetValue(typeof(T), out var set))
            ((HashSet<IResourceProcessor<T>>)set.set.GetOut<T>()).Add(processor);
        else
        {
            ProcessorSet Freezer(ProcessorSet s) => ProcessorSet.Create(((HashSet<IResourceProcessor<T>>)s.GetOut<T>()).ToFrozenSet());
            _processors.AsT0[typeof(T)] = (ProcessorSet.Create(new HashSet<IResourceProcessor<T>>{ processor }), Freezer);
        }

        _processorResourceSources.AsT0[inAssetPath] = processor;
    }

    internal static void FreezeProcessors()
    {
        _processors = _processors.AsT0.ToFrozenDictionary(kvp => kvp.Key, kvp => kvp.Value.freezer(kvp.Value.set));
        _processorResourceSources = _processorResourceSources.AsT0.ToFrozenDictionary();
    }

    internal static void LoadResources()
    {
        Dictionary<IResourceProcessor, Dictionary<IResources, Dictionary<IMod, List<string>>>> sortedResources = new();
        {
            var sources = IResources.GetActiveResources();
            foreach (var source in sources)
            {
                foreach (var pathS in source.GetManifest())
                {
                    var path = pathS.AsSpan();
                    
                    var mod = FindMod(ref path);
                    if(mod is null)
                        continue;
                    
                    var processor = FindProcessor(ref path);
                    if(processor is null)
                        continue;

                    if (!sortedResources.TryGetValue(processor, out var step1))
                        step1 = sortedResources[processor] = new();

                    if (!step1.TryGetValue(source, out var step2))
                        step2 = step1[source] = new();

                    if (!step2.TryGetValue(mod, out var step3))
                        step3 = step2[mod] = [];
                    
                    step3.Add(new(path));
                }
            }

            IMod? FindMod(ref ReadOnlySpan<char> path)
            {
                var index = path.IndexOf('/');
                if(index == -1)
                    return null;
                
                var modIdent = path[..index];
                path = path[(index + 1)..];
                return IMod.FromIdentityOrAbstract(modIdent);
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            IResourceProcessor? FindProcessor(ref ReadOnlySpan<char> path)
            {
                foreach (var process in _processorResourceSources.Match(
                             d => d.AsEnumerable(), 
                             d => d.AsEnumerable()))
                    if (path.StartsWith(process.Key))
                    {
                        path = path[process.Key.Length..];
                        return process.Value;
                    }
                return null;
            }
        }
        var processors = _processors.Match(d => d.Values.Select(s => s.set), d => d.Values);

        // ReSharper disable once LocalVariableHidesMember, InconsistentNaming
        foreach (var _processors in processors)
        {
            foreach (var processor in _processors)
            {
                if(sortedResources.TryGetValue(processor, out var data))
                    processor.LoadResources(data);
            }
        }
    }
    
    public static IReadOnlySet<IResourceProcessor<T>> GetProcessors<T>()
    {
        if (_processors.IsT0)
        {
            if (_processors.AsT0.TryGetValue(typeof(T), out var set)) //Inefficient but an edge case will rarely be used, if ever 
                return new HashSet<IResourceProcessor<T>>(set.set.GetOut<T>());
        }
        else
        {
            if (_processors.AsT1.TryGetValue(typeof(T), out var set))
                return set.GetOut<T>() ?? throw new InvalidOperationException("Unknown error, invalid data type");
        }

        return System.Collections.Immutable.ImmutableHashSet<IResourceProcessor<T>>.Empty;
    }

    public static IResourceProcessor<T> GetProcessor<T>()
    {
        if (_processors.IsT0)
        {
            if (_processors.AsT0.TryGetValue(typeof(T), out var set))
                return set.set.GetOut<T>().First();
        }
        else
        {
            if (_processors.AsT1.TryGetValue(typeof(T), out var set))
                return set.GetOut<T>().First();
        }
        throw new KeyNotFoundException($"No processors for type {typeof(T).Name} found.");
    }
    
    public static PossibleResult<T> Find<T>(IMod mod, string identity)
    {
        if (_processors.IsT0)
        {
            if (!_processors.AsT0.TryGetValue(typeof(T), out var set)) return default;
            foreach (var processor in (HashSet<IResourceProcessor<T>>)set.set.GetOut<T>())
            {
                var value = processor.Find(mod, identity);
                if (value.IsSet)
                    return value;
            }
        }
        else if(_processors.IsT1)
            if(_processors.AsT1.TryGetValue(typeof(T), out var set))
                foreach (var processor in (FrozenSet<IResourceProcessor<T>>)set.GetOut<T>())
                {
                    var value = processor.Find(mod, identity);
                    if (value.IsSet)
                        return value;
                }
        
        return new None();
    }
    
    public static PossibleResult<T> Find<T>(string identity)
    {
        if (_processors.IsT0)
        {
            if (!_processors.AsT0.TryGetValue(typeof(T), out var set)) return default;
            foreach (var processor in (HashSet<IResourceProcessor<T>>)set.set.GetOut<T>())
            {
                var value = processor.Find(identity);
                if (value.IsSet)
                    return value;
            }
        }
        else if(_processors.IsT1)
            if(_processors.AsT1.TryGetValue(typeof(T), out var set))
                foreach (var processor in (FrozenSet<IResourceProcessor<T>>)set.GetOut<T>())
                {
                    var value = processor.Find(identity);
                    if (value.IsSet)
                        return value;
                }
        
        return new None();
    }
    
    private readonly struct ProcessorSet: IEnumerable<IResourceProcessor>
    {
        // ReSharper disable InconsistentNaming
        private object _set { get; init; }
        // ReSharper restore InconsistentNaming

        public static ProcessorSet Create<T>(IReadOnlySet<IResourceProcessor<T>> set) => new ProcessorSet { _set = set };
        
        public IReadOnlySet<IResourceProcessor<T>> GetOut<T>() => _set as IReadOnlySet<IResourceProcessor<T>> ?? throw new InvalidCastException();
        
        public IEnumerator<IResourceProcessor> GetEnumerator() => (_set as IEnumerable ?? throw new InvalidCastException()).Cast<IResourceProcessor>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}