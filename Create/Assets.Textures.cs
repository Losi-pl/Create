using Create.OpenGL.Textures;
using SixLabors.ImageSharp;
using Create.Linq;

namespace Create;

partial class Assets
{
    static Dictionary<string, WeakReference<Texture2D>> textures = new();

    public static Texture2D GetTexture(string name)
    {
        ThrowIfInvalidName(name);
        name = name.Replace("/", "\\");

        lock (textures)
        {
            // Check if textures is already loaded
            if (textures.TryGetValue(name, out var text))
                if (text.TryGetTarget(out var texture))
                    return texture;

            // Process textures location
            var group = name.Remove(name.IndexOf(':'));
            var path = name.Substring(name.IndexOf(':') + 1);
            var file = name.Substring(name.LastIndexOfAny(':', '\\') + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            // Load texture from resources
            var stream = _resources!.GetPath($"{group}/textures/{path}").GetFile(file).GetStream();
            var img = Image.Load(stream);
            var tex = Texture2D.Create(img);

            // Store texture in cache
            if(!textures.TryAdd(name, new(tex)));
                textures[name] = new(tex);
            return tex;
        }
    }
    
    static void ThrowIfInvalidName(string name)
    {
        if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
            throw new Exception($"Name {{{name}}} is invalid");
    }
}
