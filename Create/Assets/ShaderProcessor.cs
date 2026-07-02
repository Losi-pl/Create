using System.Collections.Frozen;
using System.Xml.Linq;
using Create.Graphics;
using Create.Registry;
using CodeSource = OneOf.OneOf<string, System.IO.Stream>;

namespace Create.Assets;

internal class ShaderProcessor: IResourceProcessor<Shader>
{
    // ReSharper disable once InconsistentNaming
    public const string ASSET_PATH = "shaders/";
    
    private FrozenDictionary<IMod, FrozenDictionary<string, Shader>>? _shaders;
    
    public void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest)
    {
        var allShaders = new Dictionary<IMod, Dictionary<string, (CodeSource vertex, CodeSource fragment, ShaderSettings? settings)>>();
        foreach (var resource in fileManifest)
        {
            var shaders = new Dictionary<IMod, Dictionary<string, (CodeSource vertexCode, CodeSource fragmentCode, ShaderSettings? settings)>>();
            foreach (var modData in resource.Value)
            {
                var myShaders = shaders[modData.Key] = new();
                foreach (var xml in modData.Value.Where(path => path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)))
                {
                    var fullMe = new Uri($"file:///{modData.Key.Identity}/{ASSET_PATH}{xml}");
                    
                    var stream = resource.Key.GetStream(fullMe.LocalPath.TrimStart('/'));
                    if(stream is null) throw new NullReferenceException($"Could not find file \"{modData.Key.Identity}/{ASSET_PATH}{xml}\"");
                    var shaderData = XDocument.Load(stream).Root!;
                    
                    var code = GetShaderCode(resource.Key, shaderData, fullMe);

                    myShaders[xml[..xml.LastIndexOf('.')]] = (code.vertex, code.fragment, new());
                }
            }

            foreach (var perMod in shaders)
            {
                foreach (var shader in perMod.Value)
                {
                    if (!allShaders.TryGetValue(perMod.Key, out var folder))
                        folder = allShaders[perMod.Key] = new();
                    folder[shader.Key] = shader.Value;
                }
            }
        }

        Dictionary<IMod, FrozenDictionary<string, Shader>> compiled = new();
        foreach (var perMod in allShaders)
        {
            Dictionary<string, Shader> shaders = new();
            foreach (var shader in perMod.Value)
            {
                var cons = Shader.Create().Name(shader.Key);
                
                if(shader.Value.vertex.IsT0)
                    cons.Vertex(shader.Value.vertex.AsT0);
                else
                    cons.Vertex(shader.Value.vertex.AsT1, true);
                
                if(shader.Value.fragment.IsT0)
                    cons.Fragment(shader.Value.fragment.AsT0);
                else
                    cons.Fragment(shader.Value.fragment.AsT1, true);

                shaders[shader.Key] = cons.Finish();
            }
            compiled[perMod.Key] = shaders.ToFrozenDictionary();
        }
        _shaders = compiled.ToFrozenDictionary();

        (Stream fragment, Stream vertex) GetShaderCode(IResources source, XElement config, Uri configPath)
        {
            string? fragmentPath = null;
            string? vertexPath = null;
            
            var sources = config.Element("sources");
            if (sources is not null)
            {
                var fragment = sources.Element("fragment");
                var vertex = sources.Element("vertex");
                if (fragment is not null)
                    fragmentPath = string.IsNullOrEmpty(fragment.Value) ? fragment.Attribute("path")?.Value : fragment.Value;
                if (vertex is not null)
                    vertexPath = string.IsNullOrEmpty(vertex.Value) ? vertex.Attribute("path")?.Value : vertex.Value;
            }

            fragmentPath = fragmentPath is not null ? 
                new Uri(configPath, fragmentPath).LocalPath.TrimStart('/') : 
                Path.ChangeExtension(configPath.LocalPath.TrimStart('/'), ".frag");
                    
            vertexPath = vertexPath is not null ? 
                new Uri(configPath, vertexPath).LocalPath.TrimStart('/') : 
                Path.ChangeExtension(configPath.LocalPath.TrimStart('/'), ".vert");
            
            return (source.GetStream(fragmentPath) ?? throw new FileNotFoundException($"File {fragmentPath} not found"), 
                source.GetStream(vertexPath) ?? throw new FileNotFoundException($"File {vertexPath} not found"));
        }
    }

    private class ShaderSettings 
    {
        // Later, for when shaders have more settings
    }
    
    public void ClearResources()
    {
        var hand = _shaders;
        _shaders = null;
        if (hand is null) return;
        foreach (var shader in hand.SelectMany(perMod => perMod.Value))
            Console.Write(shader);//TODO Shader.Dispose()
    }

    public Shader? Find(string identity)
    {
        if (_shaders is null)
            return null;

        if (!IMod.Mods.GetAlternateLookup().TryGetValue(identity.AsSpan()[..identity.IndexOf(':')], out var mod)) return null;
        
        if (!_shaders.TryGetValue(mod, out var shaders)) return null;
        
        return shaders.GetAlternateLookup().TryGetValue(identity.AsSpan()[(identity.IndexOf(':') + 1)..], out var shader) ? shader : null;
    }
    public Shader? Find(IMod source, string identity)
    {
        if (_shaders is null)
            return null;

        if (!_shaders.TryGetValue(source, out var shaders)) return null;
        return shaders.TryGetValue(identity, out var shader) ? shader : null;
    }
}