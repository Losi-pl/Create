using System.Xml.Linq;

namespace Create.OpenGL
{
    partial class Shader
    {
        static readonly (BlendingFactor @enum, string name)[] _blendingFactorNames = Enum.GetValues<BlendingFactor>().ConvertAll(e => (e, e.ToString().ToLower()));
        static readonly (CullFaceMode @enum, string name)[] _cullFaceMode = Enum.GetValues<CullFaceMode>().ConvertAll(e => (e, e.ToString().ToLower()));

        /// <summary>
        /// Ładowanie <see cref="Shader"/>a z <see cref="Stream"/>a
        /// </summary>
        /// <param name="stream">Źrudło danych</param>
        /// <param name="constructor">Dodatkowe modyfikacje przed zbudowańiem <see cref="Shader"/>a</param>
        /// <param name="shader">Dodatkowe operacje do wykonania na gotowym <see cref="Shader"/>ze</param>
        public static Shader Load(Stream stream, Action<Constructor>? constructor, Action<Shader>? shader)
        {
            var xml = XDocument.Load(stream);

            var shader_version = xml.Root!.Element("version")?.Cast(v => (v.Attribute("number")?.Value, v.Attribute("core")?.Value)).Cast(v =>
            {
                if (string.IsNullOrEmpty(v.Item1))
                    return null;
                return $"#version {v.Item1}{(bool.TryParse(v.Item2, out var c) ? (c ? " core" : null) : null)}";
            });
            var vertex = load_code(xml.Root!.Element("vertex")); 
            var fragment = load_code(xml.Root!.Element("fragment"));

            if (string.IsNullOrWhiteSpace(vertex) || string.IsNullOrWhiteSpace(fragment))
                throw new Exception("Vertex shader or Fragment shader is not set");

            var con = Create()
                .VertexCode(vertex)
                .FragmentCode(fragment);

            alpha_test();
            depth_test();
            blend_func();
            cullface_f();
            properties();

            constructor?.Invoke(con);
            var shad = con.Finish();
            shader?.Invoke(shad);
            return shad;

            string tream(string value)
            {
                int minLeadingSpaces = int.MaxValue;
                string[] lines = value.Split('\n');
                foreach (string line in lines)
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        int leadingSpaces = line.TakeWhile(char.IsWhiteSpace).Count();
                        minLeadingSpaces = Math.Min(minLeadingSpaces, leadingSpaces);
                    }
                string[] trimmedLines = lines.Select(line => line.Length >= minLeadingSpaces ? line.Substring(minLeadingSpaces) : line).ToArray();
                value = string.Join('\n', trimmedLines);

                return value;
            }
            string load_code(XElement? element)
            {
                if (element is null)
                    return string.Empty;
                var code = tream(element.Value).Trim();
                return shader_version != null ? $"{shader_version}\n\n{code}" : code;
            }
            void alpha_test()
            {
                var var = xml!.Root!.Element("alpha");
                if (var == null)
                    return;
                var atr = var.Attribute("status");
                if (atr == null)
                    con.AlphaTest();
                else if(bool.TryParse(atr.Value, out var t))
                    con.AlphaTest(t);
                else
                    throw new Exception($"Invalid value {{{atr.Value}}} in <alpha/>");
            }
            void depth_test()
            {
                var var = xml!.Root!.Element("depth");
                if (var == null)
                    return;
                var atr = var.Attribute("status");
                if (atr == null)
                    con.AlphaTest();
                else if (bool.TryParse(atr.Value, out var t))
                    con.AlphaTest(t);
                else
                    throw new Exception($"Invalid value {{{atr.Value}}} in <depth/>");
            }
            void blend_func()
            {
                var var = xml!.Root!.Element("blend");
                if (var == null)
                    return;
                var atr = (var.Attribute("sfactor")?.Value.ToLower(), var.Attribute("dfactor")?.Value.ToLower());
                if (string.IsNullOrWhiteSpace(atr.Item1) && string.IsNullOrWhiteSpace(atr.Item2))
                    con.Blend();
                else if (string.IsNullOrWhiteSpace(atr.Item1) || string.IsNullOrWhiteSpace(atr.Item2))
                    throw new Exception("Not all parametrers are set in blend func");
                else
                {
                    var sfac = _blendingFactorNames.Find(b => b.name == atr.Item1, new Exception("SFactor attribute in blend is invalid")).@enum;
                    var dfac = _blendingFactorNames.Find(b => b.name == atr.Item2, new Exception("DFactor attribute in blend is invalid")).@enum;
                    con.Blend(sfac, dfac);
                }
            }
            void cullface_f()
            {
                var var = xml!.Root!.Element("cullface");
                if (var == null)
                    return;
                var atr = var.Attribute("mode")?.Value.ToLower();
                if (atr == null)
                    throw new Exception("CullFace Mode not specified");
                else
                    con.CullFace(_cullFaceMode.Find(b => b.name == atr, new("Mode specivied in <cullface> si invalid")).@enum);
            }
            void properties()
            {
                var props = xml!.Root!.Element("properties");
                if(props == null) return;

                var matrix = props.Element("matrix")?.Value;
                var pozition = props.Element("pozition")?.Value;
                var rotation = props.Element("rotation")?.Value;

                if(matrix != null)
                    con.ProjectionMatrixUniform(matrix);
                if (pozition != null)
                    con.PozitionUniform(pozition);
                if (rotation != null)
                    con.RotationUniform(rotation);
            }
        }

        /// <summary>
        /// Ładowanie <see cref="Shader"/>a z <see cref="Stream"/>a
        /// </summary>
        /// <param name="stream">Źrudło danych</param>
        /// <param name="constructor">Dodatkowe modyfikacje przed zbudowańiem <see cref="Shader"/>a</param>
        /// <param name="shader">Dodatkowe operacje do wykonania na gotowym <see cref="Shader"/>ze</param>
        public static Shader Load(Stream stream, Action<Shader>? shader, Action<Constructor>? constructor) => Load(stream, constructor, shader);

        /// <summary>
        /// Ładowanie <see cref="Shader"/>a z <see cref="Stream"/>a
        /// </summary>
        /// <param name="stream">Źrudło danych</param>
        /// <param name="constructor">Dodatkowe modyfikacje przed zbudowańiem <see cref="Shader"/>a</param>
        public static Shader Load(Stream stream, Action<Constructor>? constructor) => Load(stream, constructor, null);

        /// <summary>
        /// Ładowanie <see cref="Shader"/>a z <see cref="Stream"/>a
        /// </summary>
        /// <param name="stream">Źrudło danych</param>
        /// <param name="shader">Dodatkowe operacje do wykonania na gotowym <see cref="Shader"/>ze</param>
        public static Shader Load(Stream stream, Action<Shader>? shader) => Load(stream, null, shader);

        /// <summary>
        /// Ładowanie <see cref="Shader"/>a z <see cref="Stream"/>a
        /// </summary>
        /// <param name="stream">Źrudło danych</param>
        public static Shader Load(Stream stream) => Load(stream, (Action<Constructor>?)null, null);
    }
}
