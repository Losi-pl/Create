using Create.OpenGL.Textures;
using SixLabors.ImageSharp;

namespace Create;

partial class Assets
{
    static Dictionary<string, WeakReference<Texture2D>> textures = new();
    
    public static Texture2D GetTexture(string name)
    {
        if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
            throw new Exception($"name {{{name}}} is invalid");

        name = name.Replace("/", "\\");

        lock (textures)
        {
            if (textures.TryGetValue(name, out var text))
                if (text.TryGetTarget(out var texture))
                    return texture;

            string group = name.Remove(name.IndexOf(':'));
            string path = name.Substring(name.IndexOf(':') + 1);
            string file = name.Substring(name.LastIndexOfAny(new[] { ':', '\\' }) + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            var stream = resources!.GetPath($"{group}/textures/{path}").GetFile(file).GetStream();
            var img = Image.Load(stream);
            var tex = Texture2D.Create(img);

            textures.Remove(name);
            textures.Add(name, new(tex));
            return tex;
        }
    }
}
