using System.Collections.Frozen;
using System.Xml.Linq;
using Create.Graphics;
using Create.Registry;
using CodeSource = OneOf.OneOf<string, System.IO.Stream>;

namespace Create.Assets;

internal class ShaderProcessor: IResourceProcessor<Shader>
{
    // ReSharper disable InconsistentNaming, MemberCanBePrivate.Global
    public const string ASSET_PATH = "shaders/";
    
    public const string FRAGMENT_SETTINGS = "fragment";
    public const string FRAGMENT_OUTPUT = "output";
    
    public const string UNIFORM_SETTINGS = "uniforms";
    
    public const string CODE_SOURCE_SETTING = "sources";
    
    public const string NAME_ATTRIBUTE = "name";
    public const string NONE_ATTRIBUTE = "none";
    public const string PATH_ATTRIBUTE = "path";
    public const string INDEX_ATTRIBUTE = "index";
    // ReSharper restore InconsistentNaming, MemberCanBePrivate.Global
    
    
    
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
                    var settings = new ShaderSettings();
                    
                    if (shaderData.Element(FRAGMENT_SETTINGS) is { } fragData)
                    {
                        settings.FragmentOutputs = GetFragmentBindings(fragData);
                    }

                    if (shaderData.Element(UNIFORM_SETTINGS) is { } uniformData)
                    {
                        settings.ModelMatrix = CheckUniformSpecification(uniformData, Shader.MODEL_UNIFORM);
                        settings.ViewMatrix = CheckUniformSpecification(uniformData, Shader.VIEW_UNIFORM);
                        settings.ProjectionMatrix = CheckUniformSpecification(uniformData, Shader.PROJECTION_UNIFORM);
                    }
                    
                    myShaders[xml[..xml.LastIndexOf('.')]] = (code.vertex, code.fragment, settings);
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

        CompileShaders(allShaders);
        return;

        // Local Methods
        (string name, uint handle)[] GetFragmentBindings(XElement config)
        {
            List<(string, uint)> list = new();
            foreach (var output in config.Elements(FRAGMENT_OUTPUT))
            {
                var name = output.Attribute(NAME_ATTRIBUTE)?.Value;
                var index = uint.TryParse(output.Attribute(INDEX_ATTRIBUTE)?.Value ?? "", out var result) ? result : (uint?)null;
                if(name is null || index is null)
                    continue;
                list.Add((name, index.Value));
            }
            return list.Count > 0 ? list.ToArray() : [];
        }
        
        (Stream fragment, Stream vertex) GetShaderCode(IResources source, XElement config, Uri configPath)
        {
            string? fragmentPath = null;
            string? vertexPath = null;
            
            var sources = config.Element(CODE_SOURCE_SETTING);
            if (sources is not null)
            {
                var fragment = sources.Element(Shader.FRAGMENT_SHADER);
                var vertex = sources.Element(Shader.VERTEX_SHADER);
                if (fragment is not null)
                    fragmentPath = string.IsNullOrEmpty(fragment.Value) ? fragment.Attribute(PATH_ATTRIBUTE)?.Value : fragment.Value;
                if (vertex is not null)
                    vertexPath = string.IsNullOrEmpty(vertex.Value) ? vertex.Attribute(PATH_ATTRIBUTE)?.Value : vertex.Value;
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
        
        OneOf<string?, None> CheckUniformSpecification(XElement config, string uniformName)
        {
            var spec = config.Element(uniformName);
            
            if (spec is null)
                return new None();
            if (spec.Attribute(NONE_ATTRIBUTE) is not null)
                return null;

            if (spec.Attribute(NAME_ATTRIBUTE) is { } nameAttrib)
                return nameAttrib.Value;
            return spec.Value;
        }
    }

    private void CompileShaders(Dictionary<IMod, Dictionary<string, (CodeSource vertex, CodeSource fragment, ShaderSettings? settings)>> data)
    {
        Dictionary<IMod, FrozenDictionary<string, Shader>> compiled = new();
        foreach (var perMod in data)
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

                if (shader.Value.settings is { } settings)
                {
                    foreach (var output in settings.FragmentOutputs ?? [])
                        cons.BindFragmentOutput(output.name, output.index);
                    
                    if(settings.ModelMatrix.IsT0)
                        cons.SpecifyModelMatrix(settings.ModelMatrix.AsT0);
                    
                    if(settings.ViewMatrix.IsT0)
                        cons.SpecifyViewMatrix(settings.ViewMatrix.AsT0);
                    
                    if(settings.ProjectionMatrix.IsT0)
                        cons.SpecifyProjectionMatrix(settings.ProjectionMatrix.AsT0);
                }
                
                shaders[shader.Key] = cons.Finish();
            }
            compiled[perMod.Key] = shaders.ToFrozenDictionary();
        }
        _shaders = compiled.ToFrozenDictionary();
    }

    private class ShaderSettings
    {
        public (string name, uint index)[]? FragmentOutputs;
        public OneOf<string?, None> ModelMatrix = new None();
        public OneOf<string?, None> ViewMatrix = new None();
        public OneOf<string?, None> ProjectionMatrix = new None();
    }
    
    public void ClearResources()
    {
        var hand = _shaders;
        _shaders = null;
        if (hand is null) return;
        foreach (var shader in hand.SelectMany(perMod => perMod.Value))
            Console.Write(shader.Value);//TODO Shader.Dispose()
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