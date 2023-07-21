using Create.Elements.Gui;
using Create.OpenGL.GUI;
using Create.Sceans;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using static Create.Static;

namespace Create;

partial class Assets
{
    internal static Dictionary<string, (Mod mod, Func<XElement, Element>)> interfaceElementTypes = new();

    public static SpacePoint GetInterface(string path)
    {
        var xml = load(path);
        var poin = point(xml.Root!);
        poin.Name = path.Replace('/', '\\');
        return poin;

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
            var act = bool.TryParse(value(element.Element("active")) ?? string.Empty, out var v) ? v : true;
            var nam = (element.Attribute("name")?.Value) ?? string.Empty;
            var evn = element.Element("events");

            s.Pozition = (poz.x * 4, poz.y * 4);
            s.Size = (siz.x * 4, siz.y * 4);
            s.Element = elem(element.Element("element"));
            anker(element.Element("anker"));
            s.Active = act;
            s.Name = nam;

            if (evn is not null)
                load_events(s, evn);
            foreach (var el in element.Elements("point"))
                s.Childs.AddChild(point(el));
            return s;

            void anker(XElement? ank)
            {
                if (ank is null)
                    return;
                var atr = ank.Attribute("mode")?.Value.ToLower();
                if (atr is not null)
                    s!.AnkerMode = ankerModes.Find(a => a.name == atr, new("Invalid variable structure")).Item1;
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
        void load_events(SpacePoint point, XElement events)
        {
            foreach(var evnt in events.Elements())
            {
                var name = evnt.Attribute("name")?.Value;
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                
                switch (evnt.Name.LocalName)
                {
                    case "change":
                        point.AddEvent(name, change_event, evnt); break;
                    default:
                        continue;
                }
            }

            void change_event(SpacePoint point, object sender)
            {
                point_upt(point, (XElement)sender);
                void point_upt(SpacePoint P, XElement element)
                {
                    var val = element.Element("pozition");
                    if(val is not null)
                    {
                        P.Pozition = float_parse(sizes(value(val)));
                        P.Pozition = (P.Pozition.x * 4, P.Pozition.y * 4);
                    }
                    val = element.Element("size");
                    if (val is not null)
                    {
                        P.Size = float_parse(sizes(value(val)));
                        P.Size = (P.Size.Width * 4, P.Size.Height * 4);
                    }
                    val = element.Element("active");
                    if (val is not null)
                        P.Active = bool.TryParse(value(val) ?? string.Empty, out var v) ? v : true;
                    val = element.Element("anker");
                    if ( val is not null)
                    {
                        var atr = val.Attribute("mode")?.Value.ToLower();
                        if (atr is not null)
                            P.AnkerMode = Static.ankerModes.Find(a => a.name == atr, new("Invalid variable structure")).Item1;
                    }

                    foreach (var p in element.Elements("point"))
                    {
                        var name = p.Attribute("name")?.Value;
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            var index = p.Attribute("index")?.Value;
                            if (string.IsNullOrWhiteSpace(index))
                                continue;
                            var point = P.Childs[int.Parse(index)];
                            point_upt(point, p);
                        }
                        else
                        {
                            var point = P.Childs.Find(name);
                            point_upt(point, p);
                        }
                    }
                }
            }
        }
    }

    internal static void load_elements(Mod mod)
    {
        mod.RegisterInterfaceLoadingMethod("Crosshair", e =>
        {
            var cr = new GameView.Crosshair();

            var texture = value(e.Element("texture"));
            if (!string.IsNullOrWhiteSpace(texture))
                cr.Interface = GetTexture(texture);

            var chan = value(e.Element("screen"))?.ToLower()
                .Cast(c => 
                    framebufferAttachment.Find(a => a.name == c, new("Invalid variable structure")).Item1);
            if (chan.HasValue)
                cr.Terrain = ((GameView)OpenGL.Engine.Scean!)._Terrain.Finisched.Textures[chan.Value];

            var tex_poz = get_tex_poz(e.Element("pozition"));
            if(tex_poz.HasValue)
                (cr.Offset, cr.Size) = tex_poz.Value;

            return cr;
        });
        mod.RegisterInterfaceLoadingMethod("InterfaceImage", e =>
        {
            var ii = new InterfaceImage();

            var texture = value(e.Element("texture"));
            if (!string.IsNullOrWhiteSpace(texture))
                ii.Texture = GetTexture(texture);

            var tex_poz = get_tex_poz(e.Element("pozition"));
            if (tex_poz.HasValue)
                (ii.Offset, ii.Size) = tex_poz.Value;

            return ii;
        });
        mod.RegisterInterfaceLoadingMethod("Image", e =>
        {
            var i = new OpenGL.GUI.Elements.Image();

            var texture = value(e.Element("texture"));
            if (!string.IsNullOrWhiteSpace(texture))
                i.Texture = GetTexture(texture);

            var color = load_color(e.Element("color"));
            if(color.HasValue)
                i.Color = color.Value;

            return i;
        });
        mod.RegisterInterfaceLoadingMethod("StatusBar", e =>
        {
            var sb = new StatusBar();

            var texture = value(e.Element("texture"));
            if (!string.IsNullOrWhiteSpace(texture))
                sb.Texture = GetTexture(texture);

            var back_poz = get_tex_poz(e.Element("background"));
            var full_poz = get_tex_poz(e.Element("full"));
            var half_poz = get_tex_poz(e.Element("half"));
            if (back_poz.HasValue)
                sb.Background = back_poz.Value;
            if (full_poz.HasValue)
                sb.FullPoint = full_poz.Value;
            if (half_poz.HasValue)
                sb.HalfPoint = half_poz.Value;

            var points = value(e.Element("points"));
            if (!string.IsNullOrWhiteSpace(points))
                sb.Points = int.TryParse(points, out var pos) ? pos : throw new Exception("Invalid variable structure");
            else
                sb.Points = 1;


            var filled = value(e.Element("filled"));
            if (!string.IsNullOrWhiteSpace(filled))
                sb.Filled = int.TryParse(filled, out var pos) ? pos : throw new Exception("Invalid variable structure");

            return sb;
        });
        mod.RegisterInterfaceLoadingMethod("Slot", e =>
        {
            var item = new OpenGL.GUI.Elements.Image();

            item.Color = Color4.LightGreen;

            return item;
        });

        //Mothods
        string value(XElement? element)
        {
            if (element is null)
                return string.Empty;
            var a = element.Attribute("v");
            if (a != null)
                return a.Value;
            else
                return element.Value;
        }
        ((int x, int y) offset, (int w, int h) size)? get_tex_poz(XElement? element)
        {
            if (element is null)
                return null;

            var off = element.Attribute("offset")?.Value;
            var siz = element.Attribute("size")?.Value;

            var _off = int_parse(sizes(off));
            var _siz = int_parse(sizes(siz));
            return (_off, _siz);
        }
        (string x, string y)? sizes(string? value)
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
        Color4? load_color(XElement? element)
        {
            if(element is null)
                return null;
            var color_v = value(element);
            Color4 color;
            var w = colors.FirstOrDefault(c => c.name == color_v);
            if (!string.IsNullOrEmpty(w.name))
                color = w.color;
            else
            {
                if (color_v.Length == 6)
                {
                    var r = (byte)Convert.ToInt32(color_v[0..1], 16);
                    var g = (byte)Convert.ToInt32(color_v[2..3], 16);
                    var b = (byte)Convert.ToInt32(color_v[4..5], 16);
                    color = new(r, g, b, 255);
                }
                else if (color_v.Length == 8)
                {

                    var r = (byte)Convert.ToInt32(color_v[0..1], 16);
                    var g = (byte)Convert.ToInt32(color_v[2..3], 16);
                    var b = (byte)Convert.ToInt32(color_v[4..5], 16);
                    var a = (byte)Convert.ToInt32(color_v[6..7], 16);
                    color = new(r, g, b, a);
                }
                else
                    throw new("Invalid variable structure");
            }
            return color;
        }
    }
}

file class Static
{
    public static string value(XElement? element)
    {
        if (element is null)
            return string.Empty;
        var a = element.Attribute("v");
        if (a != null)
            return a.Value;
        else
            return element.Value;
    }
    public static (string x, string y)? sizes(string value)
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
    public static (int x, int y) int_parse((string x, string y)? value)
    {
        if (!value.HasValue)
            return (0, 0);
        return (int.Parse(value.Value.x), int.Parse(value.Value.y));
    }
    public static (float x, float y) float_parse((string x, string y)? value)
    {
        if (!value.HasValue)
            return (0, 0);
        return (ParseFloat(value.Value.x), ParseFloat(value.Value.y));
    }
    public static float ParseFloat(string value)
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

    public static readonly (SpacePoint.Anker, string name)[] ankerModes = Enum.GetValues<SpacePoint.Anker>().ConvertAll(a => (a, a.ToString().ToLower()));
    public static readonly (Color4 color, string name)[] colors = typeof(Color4).GetProperties(BindingFlags.Static | BindingFlags.Public).ConvertAll(c => ((Color4)c.GetValue(null)!, c.Name.ToLower()));
    public static readonly (FramebufferAttachment, string name)[] framebufferAttachment = (new[] {
        (FramebufferAttachment.Aux0, "Aux0"),
        (FramebufferAttachment.Aux1, "Aux1"),
        (FramebufferAttachment.Aux2, "Aux2"),
        (FramebufferAttachment.Aux3, "Aux3"),
        (FramebufferAttachment.BackLeft, "BackLeft"),
        (FramebufferAttachment.BackRight, "BackRight"),
        (FramebufferAttachment.Color, "Color"),
        (FramebufferAttachment.ColorAttachment0, "ColorAttachment0"),
        (FramebufferAttachment.ColorAttachment0Ext, "ColorAttachment0Ext"),
        (FramebufferAttachment.ColorAttachment0Nv, "ColorAttachment0Nv"),
        (FramebufferAttachment.ColorAttachment0Oes, "ColorAttachment0Oes"),
        (FramebufferAttachment.ColorAttachment1, "ColorAttachment1"),
        (FramebufferAttachment.ColorAttachment10, "ColorAttachment10"),
        (FramebufferAttachment.ColorAttachment10Ext, "ColorAttachment10Ext"),
        (FramebufferAttachment.ColorAttachment10Nv, "ColorAttachment10Nv"),
        (FramebufferAttachment.ColorAttachment11, "ColorAttachment11"),
        (FramebufferAttachment.ColorAttachment11Ext, "ColorAttachment11Ext"),
        (FramebufferAttachment.ColorAttachment11Nv, "ColorAttachment11Nv"),
        (FramebufferAttachment.ColorAttachment12, "ColorAttachment12"),
        (FramebufferAttachment.ColorAttachment12Ext, "ColorAttachment12Ext"),
        (FramebufferAttachment.ColorAttachment12Nv, "ColorAttachment12Nv"),
        (FramebufferAttachment.ColorAttachment13, "ColorAttachment13"),
        (FramebufferAttachment.ColorAttachment13Ext, "ColorAttachment13Ext"),
        (FramebufferAttachment.ColorAttachment13Nv, "ColorAttachment13Nv"),
        (FramebufferAttachment.ColorAttachment14, "ColorAttachment14"),
        (FramebufferAttachment.ColorAttachment14Ext, "ColorAttachment14Ext"),
        (FramebufferAttachment.ColorAttachment14Nv, "ColorAttachment14Nv"),
        (FramebufferAttachment.ColorAttachment15, "ColorAttachment15"),
        (FramebufferAttachment.ColorAttachment15Ext, "ColorAttachment15Ext"),
        (FramebufferAttachment.ColorAttachment15Nv, "ColorAttachment15Nv"),
        (FramebufferAttachment.ColorAttachment16, "ColorAttachment16"),
        (FramebufferAttachment.ColorAttachment17, "ColorAttachment17"),
        (FramebufferAttachment.ColorAttachment18, "ColorAttachment18"),
        (FramebufferAttachment.ColorAttachment19, "ColorAttachment19"),
        (FramebufferAttachment.ColorAttachment1Ext, "ColorAttachment1Ext"),
        (FramebufferAttachment.ColorAttachment1Nv, "ColorAttachment1Nv"),
        (FramebufferAttachment.ColorAttachment2, "ColorAttachment2"),
        (FramebufferAttachment.ColorAttachment20, "ColorAttachment20"),
        (FramebufferAttachment.ColorAttachment21, "ColorAttachment21"),
        (FramebufferAttachment.ColorAttachment22, "ColorAttachment22"),
        (FramebufferAttachment.ColorAttachment23, "ColorAttachment23"),
        (FramebufferAttachment.ColorAttachment24, "ColorAttachment24"),
        (FramebufferAttachment.ColorAttachment25, "ColorAttachment25"),
        (FramebufferAttachment.ColorAttachment26, "ColorAttachment26"),
        (FramebufferAttachment.ColorAttachment27, "ColorAttachment27"),
        (FramebufferAttachment.ColorAttachment28, "ColorAttachment28"),
        (FramebufferAttachment.ColorAttachment29, "ColorAttachment29"),
        (FramebufferAttachment.ColorAttachment2Ext, "ColorAttachment2Ext"),
        (FramebufferAttachment.ColorAttachment2Nv, "ColorAttachment2Nv"),
        (FramebufferAttachment.ColorAttachment3, "ColorAttachment3"),
        (FramebufferAttachment.ColorAttachment30, "ColorAttachment30"),
        (FramebufferAttachment.ColorAttachment31, "ColorAttachment31"),
        (FramebufferAttachment.ColorAttachment3Ext, "ColorAttachment3Ext"),
        (FramebufferAttachment.ColorAttachment3Nv, "ColorAttachment3Nv"),
        (FramebufferAttachment.ColorAttachment4, "ColorAttachment4"),
        (FramebufferAttachment.ColorAttachment4Ext, "ColorAttachment4Ext"),
        (FramebufferAttachment.ColorAttachment4Nv, "ColorAttachment4Nv"),
        (FramebufferAttachment.ColorAttachment5, "ColorAttachment5"),
        (FramebufferAttachment.ColorAttachment5Ext, "ColorAttachment5Ext"),
        (FramebufferAttachment.ColorAttachment5Nv, "ColorAttachment5Nv"),
        (FramebufferAttachment.ColorAttachment6, "ColorAttachment6"),
        (FramebufferAttachment.ColorAttachment6Ext, "ColorAttachment6Ext"),
        (FramebufferAttachment.ColorAttachment6Nv, "ColorAttachment6Nv"),
        (FramebufferAttachment.ColorAttachment7, "ColorAttachment7"),
        (FramebufferAttachment.ColorAttachment7Ext, "ColorAttachment7Ext"),
        (FramebufferAttachment.ColorAttachment7Nv, "ColorAttachment7Nv"),
        (FramebufferAttachment.ColorAttachment8, "ColorAttachment8"),
        (FramebufferAttachment.ColorAttachment8Ext, "ColorAttachment8Ext"),
        (FramebufferAttachment.ColorAttachment8Nv, "ColorAttachment8Nv"),
        (FramebufferAttachment.ColorAttachment9, "ColorAttachment9"),
        (FramebufferAttachment.ColorAttachment9Ext, "ColorAttachment9Ext"),
        (FramebufferAttachment.ColorAttachment9Nv, "ColorAttachment9Nv"),
        (FramebufferAttachment.Depth, "Depth"),
        (FramebufferAttachment.DepthAttachment, "DepthAttachment"),
        (FramebufferAttachment.DepthAttachmentExt, "DepthAttachmentExt"),
        (FramebufferAttachment.DepthAttachmentOes, "DepthAttachmentOes"),
        (FramebufferAttachment.DepthStencilAttachment, "DepthStencilAttachment"),
        (FramebufferAttachment.FrontLeft, "FrontLeft"),
        (FramebufferAttachment.FrontRight, "FrontRight"),
        (FramebufferAttachment.MaxColorAttachments, "MaxColorAttachments"),
        (FramebufferAttachment.MaxColorAttachmentsExt, "MaxColorAttachmentsExt"),
        (FramebufferAttachment.MaxColorAttachmentsNv, "MaxColorAttachmentsNv"),
        (FramebufferAttachment.Stencil, "Stencil"),
        (FramebufferAttachment.StencilAttachment, "StencilAttachment"),
        (FramebufferAttachment.StencilAttachmentExt, "StencilAttachmentExt"),
    }).ConvertAll(a => (a.Item1, a.Item2.ToLower()));
}
