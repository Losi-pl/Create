using System.Xml.Linq;
using Create.Graphics;
using Create.Registry;
using CodeSource = CodeOfChaos.Unions.Union<string, System.IO.Stream>;

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
    
    private FrozenElementDictionary<Shader>? _shaders;
    
    public void LoadResources(Dictionary<IResources, Dictionary<IMod, List<string>>> fileManifest)
    {
        var shaders = new ElementDictionary<(CodeSource vertex, CodeSource fragment, ShaderSettings? settings)>();
        foreach (var resource in fileManifest)
        {
            foreach (var modData in resource.Value)
            {
                foreach (var xml in modData.Value.Where(path => path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)))
                {
                    var fullMe = new Uri($"file:///{modData.Key.Identity}/{ASSET_PATH}{xml}"); // For easy path alterations
                    
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
                    
                    shaders[(modData.Key, xml[..xml.LastIndexOf('.')])] = (code.vertex, code.fragment, settings);
                }
            }
        }

        CompileShaders(shaders);
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
        
        PossibleResult<string?> CheckUniformSpecification(XElement config, string uniformName)
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

    private void CompileShaders(ElementDictionary<(CodeSource vertex, CodeSource fragment, ShaderSettings? settings)> data)
    {
        _shaders = data.ToFrozenDictionary(shaderData =>
        {
            var cons = Shader.Create().Name(shaderData.Key.ToString());
            var shader = shaderData.Value;
            
            if (shader.vertex.IsT0)
                cons.Vertex(shader.vertex.AsT0); // Code directly passed
            else
                cons.Vertex(shader.vertex.AsT1, true); // A link to the file with code

            if (shader.fragment.IsT0)
                cons.Fragment(shader.fragment.AsT0); // Code directly passed
            else
                cons.Fragment(shader.fragment.AsT1, true); // A link to the file with code

            if (shader.settings is { } settings)
            {
                foreach (var output in settings.FragmentOutputs ?? [])
                    cons.BindFragmentOutput(output.name, output.index);

                if (settings.ModelMatrix.IsSet)
                    cons.SpecifyModelMatrix(settings.ModelMatrix.AsSet);

                if (settings.ViewMatrix.IsSet)
                    cons.SpecifyViewMatrix(settings.ViewMatrix.AsSet);

                if (settings.ProjectionMatrix.IsSet)
                    cons.SpecifyProjectionMatrix(settings.ProjectionMatrix.AsSet);
            }

            return cons.Finish();
        });
    }

    private class ShaderSettings
    {
        public (string name, uint index)[]? FragmentOutputs;
        public PossibleResult<string?> ModelMatrix = new None();
        public PossibleResult<string?> ViewMatrix = new None();
        public PossibleResult<string?> ProjectionMatrix = new None();
    }
    
    public void ClearResources()
    {
        var hand = _shaders;
        _shaders = null;
        if (hand is null) return;
        foreach (var shader in hand)
            shader.Value.Dispose();
    }

    public PossibleResult<Shader> Find(RefElementIdent identity)
    {
        if (_shaders is null || !_shaders.TryGetValue(identity, out var shader))
            return new None();
        return shader;
    }
}