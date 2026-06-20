using Create.Render;

namespace Create;

partial class Assets
{
    static Dictionary<string, WeakReference<Font>> fonts = new();

    public static Font GetFont(string name)
    {
        if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
            throw new Exception($"Name {{{name}}} is invalid");

        name = name.Replace("/", "\\");

        lock (fonts)
        {
            Font font;
            if (fonts.TryGetValue(name, out var fon))
                if (fon.TryGetTarget(out font!))
                    return font;

            string group = name.Remove(name.IndexOf(':'));
            string path = name.Substring(name.IndexOf(':') + 1);
            string file = name.Substring(name.LastIndexOfAny(new[] { ':', '\\' }) + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            var stream = _resources!.GetPath($"{group}/font/{path}").GetFile(file).GetStream();
            font = Font.Load(stream);
            fonts.Remove(name);
            fonts.Add(name, new(font));
            return font;
        }
    }
}
