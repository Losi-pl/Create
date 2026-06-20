using Create.Render.ModelCreators.BlockModels;
using System.Xml.Linq;

namespace Create;

partial class Assets
{
    static Dictionary<string, WeakReference<IBlockModel>> block_models = new();

    public static IBlockModel LoadBlockModel(string name)
    {
        if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
            throw new Exception($"name {{{name}}} is invalid");

        name = name.Replace("/", "\\");

        lock (block_models)
        {
            if (block_models.TryGetValue(name, out var bl_mod))
                if (bl_mod.TryGetTarget(out var blo_mod))
                    return blo_mod;

            string group = name.Remove(name.IndexOf(':'));
            string path = name.Substring(name.IndexOf(':') + 1);
            string file = name.Substring(name.LastIndexOfAny(new[] { ':', '\\' }) + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            var stream = _resources!.GetPath($"{group}/models/blocks/{path}").GetFile(file).GetStream();

            XElement? xml;
            using (var reader = new StreamReader(stream))
            {
                xml = XDocument.Parse(reader.ReadToEnd()).Root;
            }
            var modSource = find_mod(xml?.Name?.Namespace?.NamespaceName ?? "create");
            var converter = xml?.Name.LocalName;

            return IBlockModel.interpreters[(modSource!, converter!)].Invoke(xml!);
            Mod? find_mod(string name)
            {
                var l = Mod.All;
                for (int i = 0; i < l.Length; i++)
                    if (l[i].Name == name)
                        return l[i];
                return null;
            }
        }
    }
}
