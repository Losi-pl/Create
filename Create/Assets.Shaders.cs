using Create.OpenGL;
using Create.OpenGL.Textures;
using Create.Linq;

namespace Create;

partial class Assets
{
    static Dictionary<string, WeakReference<Shader>> shaders = new();
    
    public static Shader GetShader(string name)
    {
        if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
            throw new Exception($"name {{{name}}} is invalid");

        name = name.Replace("/", "\\");

        lock (shaders)
        {
            if (shaders.TryGetValue(name, out var shad))
                if (shad.TryGetTarget(out var shader))
                    return shader;

            string group = name.Remove(name.IndexOf(':'));
            string path = name.Substring(name.IndexOf(':') + 1);
            string file = name.Substring(name.LastIndexOfAny(':', '\\') + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            var stream = _resources!.GetPath($"{group}/shaders/{path}").GetFile(file).GetStream();
            var shade = Shader.Load(stream);

            shaders.Remove(name);
            shaders.Add(name, new(shade));
            return shade;
        }
    }
}
