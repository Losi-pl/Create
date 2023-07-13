using Create.Elements.Gui;
using Create.OpenGL.GUI;
using Create.Sceans;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

namespace Create;

partial class Assets
{
    internal static Dictionary<string, (Mod mod, Func<XElement, Element>)> interfaceElementTypes = new();
    static readonly (SpacePoint.Anker, string name)[] _ankerModes = Enum.GetValues<SpacePoint.Anker>().ConvertAll(a => (a, a.ToString().ToLower()));
    static readonly (Color4 color, string name)[] _colors = typeof(Color4).GetProperties(BindingFlags.Static | BindingFlags.Public).ConvertAll(c => ((Color4)c.GetValue(null)!, c.Name.ToLower()));

    public static SpacePoint GetInterface(string path)
    {
        var xml = load(path);

        return point(xml.Root!);

        XDocument load(string name)
        {
            if (name.Count(c => c == ':') != 1 || name.Any(c => c is '<' or '>' or '\"' or '|' or '?' or '*'))
                throw new Exception($"Name {{{name}}} is invalid");

            name = name.Replace("/", "\\");
            string group = name.Remove(name.IndexOf(':'));
            string path = name.Substring(name.IndexOf(':') + 1);
            string file = name.Substring(name.LastIndexOfAny(new[] { ':', '\\' }) + 1);
            path = path.Length - file.Length - 1 < 0 ? string.Empty : path.Remove(path.Length - file.Length - 1);

            var stream = resources!.GetPath($"{group}/interfaces/{path}").GetFile(file).GetStream();
            return XDocument.Load(stream);
        }
        SpacePoint point(XElement element)
        {
            var s = new SpacePoint();

            var poz = float_parse(sizes(value(element.Element("pozition"))));
            var siz = float_parse(sizes(value(element.Element("size"))));

            s.Pozition = (poz.x * 4, poz.y * 4);
            s.Size = (siz.x * 4, siz.y * 4);
            s.Element = elem(element.Element("element"));
            anker(element.Element("anker"));


            foreach (var el in element.Elements("point"))
                s.Childs.AddChild(point(el));
            return s;

            void anker(XElement? ank)
            {
                if (ank is null)
                    return;
                var atr = ank.Attribute("mode")?.Value.ToLower();
                if (atr is not null)
                    s!.AnkerMode = _ankerModes.Find(a => a.name == atr, new("Invalid variable structure")).Item1;
            }
            Element? elem(XElement? element)
            {
                if (element is null)
                    return null;

                var attr = element.Attribute("type")?.Value;
                if (string.IsNullOrEmpty(attr))
                    return null;
                if (!interfaceElementTypes.TryGetValue(attr, out var converter))
                    return null;
                return converter.Item2(element);
            }
        }
        string value(XElement? element)
        {
            if(element is null)
                return string.Empty;
            var a = element.Attribute("v");
            if (a != null)
                return a.Value;
            else
                return element.Value;
        }
        (string x, string y)? sizes(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Count(c => c == ';') > 1)
                throw new Exception("Invalid variable structure");
            else if (value.Count(c => c == ';') == 0)
                return (value, value);
            else
                return value.Split(';').Cast(v => (v[0], v[1]));
            throw new Exception("Invalid variable structure");
        }
        (int x, int y) int_parse((string x, string y)? value)
        {
            if (!value.HasValue)
                return (0, 0);
            return (int.Parse(value.Value.x), int.Parse(value.Value.y));
        }
        (float x, float y) float_parse((string x, string y)? value)
        {
            if (!value.HasValue)
                return (0, 0);
            return (ParseFloat(value.Value.x), ParseFloat(value.Value.y));
        }
        float ParseFloat(string value)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            if (value[0] == '.')
            {
                value = "0" + value;
            }
            else if (value[0] == '-' && value[1] == '.')
            {
                value = "-0" + value.Substring(1);
            }

            float result = float.Parse(value, NumberStyles.Any, ci);
            return result;
        }
    }

    internal static void load_elements(Mod mod)
    {

    }
}
