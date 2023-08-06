using Create.OpenGL.Textures;
using System.Text.Json.Nodes;

namespace Create.Render;

public sealed class Font
{
    (Texture2D, int)[] textures;
    Dictionary<char, (int index, int width, (int x, int y) poz, (int x, int y) size, (int x, int y) offs)> chars = new();
    (float horizontal, float vertical) spaceing;
    float tabs;
    float static_char_width;
    char default_char;

    private Font(
        (Texture2D, int)[] textures,
        Dictionary<char, (int index, int width, (int x, int y) poz, (int x, int y) size, (int x, int y) offs)> chars,
        (float horizontal, float vertical) spaceing,
        float tabs,
        char default_char,
        float static_char_width)
    {
        this.textures = textures;
        this.chars = chars;
        this.spaceing = spaceing;
        this.tabs = tabs;
        this.default_char = default_char;
        this.static_char_width = static_char_width;
    }

    internal (float Horizontal, float Vertical) Spaceing => spaceing;
    internal float Tabs => tabs;
    internal (Texture2D texture, int heigth)[] Textures => textures;
    internal Dictionary<char, (int index, int width, (int x, int y) poz, (int x, int y) size, (int x, int y) offs)> Chars => chars;
    internal float StaticCharWidth => static_char_width;
    internal char DefaultChar => default_char;

    internal static Font Load(Stream stream)
    {
        List<(Texture2D texture, int heigth)> textures = new();
        Dictionary<char, (int index, int witch, (int x, int y) poz, (int x, int y) size, (int x, int y) offs)> chars = new();
        var json = JsonNode.Parse(stream);
        foreach (var s in (json?["sources"] as JsonArray)?.Cast<JsonObject>() ?? Enumerable.Empty<JsonObject>())
        {
            var f = (string?)s?["type"] switch
            {
                "bitmap" => FromBitmap,
                _ => (Action)(() => { throw new("Value \"type\" is invalid"); })
            };
            f.Invoke();
            void FromBitmap()
            {
                var tex = s?["source"] is JsonValue val1 ?
                    val1.TryGetValue<string>(out var S) ?
                        Assets.GetTexture(S) :
                        throw new("Value \"source\" is invalid") :
                    throw new("Value \"source\" is not set");

                var heigth = s?["pixel-height"] is JsonValue val2 ?
                        val2.TryGetValue<int>(out var i) ? i :
                        throw new("Value \"pixel-height\" is invalid") :
                    throw new("Value \"pixel-height\" is not set");

                var inde = textures.IndexOf(t => t.texture == tex);
                if (inde == -1)
                { textures.Add((tex, heigth)); inde = textures.Count - 1; }

                foreach (var l in (s?["loaders"] as JsonArray)?.OfType<JsonObject>() ?? Enumerable.Empty<JsonObject>())
                {
                    switch (((l["type"] as JsonValue)?.TryGetValue<string>(out var s) ?? false) ? s : string.Empty)
                    {
                        case "automatic":
                            {
                                var text_array = tex.GetTexture();
                                (int x, int y) pozition, size;
                                int bazeOffset, inLine;

                                bazeOffset = l["baze-offset"] is JsonValue jVal ?
                                        jVal.TryGetValue<int>(out var I) ? I :
                                        throw new($"Invalid value for baze-offset in loader") :
                                    throw new($"Invalid value for baze-offset in loader");

                                inLine = l["in-line"] is JsonValue jVal2 ?
                                        jVal2.TryGetValue<int>(out var I2) ? I2 :
                                        throw new($"Invalid value for in-line in loader") :
                                    throw new($"Invalid value for in-line in loader");

                                if (l["size"] is JsonArray array1)
                                {
                                    if (array1.Count < 2)
                                        throw new($"Invalid value for size in loader");

                                    size = (array1[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                throw new($"Invalid value for size in loader") :
                                            throw new($"Invalid value for size in loader"),
                                            array1[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                throw new($"Invalid value for size in loader") :
                                            throw new($"Invalid value for size in loader"));
                                }
                                else
                                    throw new($"Invalid value for size in loader");
                                if (l["pozition"] is JsonArray array2)
                                {
                                    if (array2.Count < 2)
                                        throw new($"Invalid value for pozition in loader");

                                    pozition = (array2[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                    throw new($"Invalid value for pozition in loader") :
                                                throw new($"Invalid value for pozition in loader"),
                                                array2[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                    throw new($"Invalid value for pozition in loader") :
                                                throw new($"Invalid value for pozition in loader"));
                                }
                                else
                                    throw new($"Invalid value for pozition in loader");

                                foreach (var c in (l["chars"] as JsonArray ?? throw new("Invalid value in chars")).SelectMany(o => o switch
                                {
                                    JsonValue str => str.TryGetValue<string>(out var s) ? s :
                                                     str.TryGetValue<int>(out var i) ? Enumerable.Repeat((char)i, 1) :
                                                     throw new("Invalid string value in chars"),
                                    JsonObject obj => (obj["start"] is JsonValue v1 ?
                                                           v1.TryGetValue<string>(out var s) ? (s.Length == 1) ?
                                                               (int)s[0] : throw new("More or less than one start value in range") :
                                                       throw new("Invalid start value in range") : throw new("No start value in range"),
                                                       obj["end"] is JsonValue v2 ?
                                                           v2.TryGetValue(out s) ? (s.Length == 1) ?
                                                               (int)s[0] : throw new("More or less than one end value in range") :
                                                       throw new("Invalid end value in range") : throw new("No end value in range"))
                                                       .Cast(k => k.Item1 < k.Item2 ? k : (k.Item2, k.Item1))
                                                       .Cast(r => Enumerable.Range(r.Item1, r.Item2 - r.Item1 + 1))
                                                       .Select(i => (char)i),
                                    _ => Enumerable.Empty<char>()
                                }).Numerate())
                                {
                                    if (c.item == '\u0000')
                                        continue;

                                    var poz = pozition.ToVector();
                                    poz += ((c.index % inLine) * size.x, (c.index / inLine) * -size.y).ToVector();
                                    (int? min, int? max) x = (null, null), y = (null, null);

                                    foreach (var px in Enumerable.Range(0, size.y)
                                                       .SelectMany(y =>
                                                           Enumerable.Range(0, size.x)
                                                           .Select(x => (x, y)))
                                                       .Where(p => text_array[poz.X + p.x, tex.Size.Height - (poz.Y + p.y + 1)].A > 0))
                                    {
                                        if (!x.min.HasValue)
                                            x.min = px.x;
                                        else if (x.min > px.x)
                                            x.min = px.x;
                                        if (!x.max.HasValue)
                                            x.max = px.x;
                                        else if (x.max < px.x)
                                            x.max = px.x;
                                        if (!y.min.HasValue)
                                            y.min = px.y;
                                        else if (y.min > px.y)
                                            y.min = px.y;
                                        if (!y.max.HasValue)
                                            y.max = px.y;
                                        else if (y.max < px.y)
                                            y.max = px.y;
                                    }
                                    (x, y) = ((x.min ?? 0, x.max ?? size.x), (y.min ?? 0, y.max ?? size.x));
                                    chars.Add(c.item, (inde, (x.max - x.min).Value + 1,
                                                      (poz + new OpenTK.Mathematics.Vector2i(x.min.Value, y.min.Value)).ToTumple(),
                                                      ((x.max - x.min).Value + 1, (y.max - y.min).Value + 1),
                                                      (0, y.min.Value - bazeOffset)));
                                }
                            }
                            break;
                        case "fast":
                            {
                                var text_array = tex.GetTexture();
                                (int x, int y) pozition, size;
                                int offset;
                                int? inLine, staticWitdh;
                                ((int x, int y) size, (int x, int y) pozition, (int x, int y) offset, int? width)? charData;

                                {
                                    charData = l["char"] is JsonObject jVal0 ?
                                               (jVal0["size"] is JsonArray arraym1 ?
                                                   (arraym1[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                            throw new($"Invalid value for size in loader") :
                                                        throw new($"Invalid value for size in loader"),
                                                    arraym1[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                            throw new($"Invalid value for size in loader") :
                                                        throw new($"Invalid value for size in loader")) :
                                                throw new($"No value for size in loader"),
                                                jVal0["pozition"] is JsonArray arraym2 ?
                                                   (arraym2[0] is JsonValue v3 ? v3.TryGetValue<int>(out var i3) ? i3 :
                                                            throw new($"Invalid value for pozition in loader") :
                                                        throw new($"Invalid value for pozition in loader"),
                                                    arraym2[1] is JsonValue v4 ? v4.TryGetValue<int>(out var i4) ? i4 :
                                                            throw new($"Invalid value for pozition in loader") :
                                                        throw new($"Invalid value for pozition in loader")) :
                                                throw new($"No value for pozition in loader"),
                                                jVal0["offset"] is JsonArray arraym3 ?
                                                   (arraym3[0] is JsonValue v5 ? v5.TryGetValue<int>(out var i5) ? i5 :
                                                            throw new($"Invalid value for pozition in loader") :
                                                        throw new($"Invalid value for pozition in loader"),
                                                    arraym3[1] is JsonValue v6 ? v6.TryGetValue<int>(out var i6) ? i6 :
                                                            throw new($"Invalid value for pozition in loader") :
                                                        throw new($"Invalid value for pozition in loader")) : (0, 0), 
                                                jVal0["width"] is JsonValue v7 ? v7.TryGetValue<int>(out var i7) ? i7 :
                                                    throw new($"Invalid value for width in loader") : null) : 
                                            null;
                                }

                                inLine = l["in-line"] is JsonValue jVal1 ?
                                        jVal1.TryGetValue<int>(out var I1) ? I1 :
                                        throw new($"Invalid value for in-line in loader") : null;

                                offset = l["offset"] is JsonValue jVal2 ?
                                        jVal2.TryGetValue<int>(out var I2) ? I2 :
                                        throw new($"Invalid value for in-line in loader") : 0;

                                staticWitdh = l["static-witdh"] is JsonValue jVal3 ?
                                        jVal3.TryGetValue<int>(out var I3) ? I3 :
                                        throw new($"Invalid value for in-line in loader") : null;

                                if (l["size"] is JsonArray array1)
                                {
                                    if (array1.Count < 2)
                                        throw new($"Invalid value for size in loader");

                                    size = (array1[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                throw new($"Invalid value for size in loader") :
                                            throw new($"Invalid value for size in loader"),
                                            array1[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                throw new($"Invalid value for size in loader") :
                                            throw new($"Invalid value for size in loader"));
                                }
                                else
                                    throw new($"Invalid value for size in loader");
                                if (l["pozition"] is JsonArray array2)
                                {
                                    if (array2.Count < 2)
                                        throw new($"Invalid value for pozition in loader");

                                    pozition = (array2[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                    throw new($"Invalid value for pozition in loader") :
                                                throw new($"Invalid value for pozition in loader"),
                                                array2[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                    throw new($"Invalid value for pozition in loader") :
                                                throw new($"Invalid value for pozition in loader"));
                                }
                                else
                                    throw new($"Invalid value for pozition in loader");

                                foreach (var c in (l["chars"] as JsonArray ?? throw new("Invalid value in chars")).SelectMany(o => o switch
                                {
                                    JsonValue str => str.TryGetValue<string>(out var s) ? s :
                                                     str.TryGetValue<int>(out var i) ? Enumerable.Repeat((char)i, 1) :
                                                     throw new("Invalid string value in chars"),
                                    JsonObject obj => (obj["start"] is JsonValue v1 ?
                                                           v1.TryGetValue<string>(out var s) ? (s.Length == 1) ?
                                                               (int)s[0] : throw new("More or less than one start value in range") :
                                                       throw new("Invalid start value in range") : throw new("No start value in range"),
                                                       obj["end"] is JsonValue v2 ?
                                                           v2.TryGetValue<string>(out s) ? (s.Length == 1) ?
                                                               (int)s[0] : throw new("More or less than one end value in range") :
                                                       throw new("Invalid end value in range") : throw new("No end value in range"))
                                                       .Cast(k => k.Item1 < k.Item2 ? k : (k.Item2, k.Item1))
                                                       .Cast(r => Enumerable.Range(r.Item1, r.Item2 - r.Item1 + 1))
                                                       .Select(i => (char)i),
                                    _ => Enumerable.Empty<char>()
                                }).Numerate())
                                {
                                    if (c.item == '\u0000')
                                        continue;

                                    var poz = pozition.ToVector();
                                    poz += ((inLine.HasValue ? c.index % inLine.Value : c.index) * size.x, (inLine.HasValue ? c.index / inLine.Value : 0) * -size.y).ToVector();
                                    if (charData.HasValue)
                                        poz += charData.Value.pozition.ToVector();
                                    chars.Add(c.item, (inde,
                                                       staticWitdh ?? (charData.HasValue ? charData.Value.size.x : size.x),
                                                       poz.ToTumple(),
                                                       charData.HasValue ? charData.Value.size : size,
                                                       charData.HasValue ? charData.Value.offset : (0, offset)));
                                }
                            }
                            break;
                        case "manual":
                            {
                                var text_array = tex.GetTexture();
                                foreach(var c in (l["chars"] is JsonObject obj1) ? obj1
                                    .Select(v => v.Key.Length != 1 ? throw new("Invalid key") : 
                                        new KeyValuePair<char, JsonNode>(v.Key[0], v.Value as JsonObject ?? 
                                            throw new($"Value of '{v.Key[0]}' is invalid"))) : 
                                    Enumerable.Empty<KeyValuePair<char, JsonNode>>())
                                {
                                    (int x, int y) pozition, size, offset;
                                    int charSize;

                                    if (c.Value["size"] is JsonArray array2)
                                    {
                                        if (array2.Count < 2)
                                            throw new($"Invalid value for size in loader");

                                        size = (array2[0] is JsonValue v3 ? v3.TryGetValue<int>(out var i3) ? i3 :
                                                    throw new($"Invalid value for size in loader") :
                                                throw new($"Invalid value for size in loader"),
                                                array2[1] is JsonValue v4 ? v4.TryGetValue<int>(out var i4) ? i4 :
                                                    throw new($"Invalid value for size in loader") :
                                                throw new($"Invalid value for size in loader"));
                                    }
                                    else
                                        throw new($"Invalid value for size in loader");
                                    if(size.y > 0)
                                    {
                                        if (c.Value["pozition"] is JsonArray array1)
                                        {
                                            if (array1.Count < 2)
                                                throw new($"Invalid value for pozition in loader");

                                            pozition = (array1[0] is JsonValue v1 ? v1.TryGetValue<int>(out var i1) ? i1 :
                                                        throw new($"Invalid value for pozition in loader") :
                                                    throw new($"Invalid value for pozition in loader"),
                                                    array1[1] is JsonValue v2 ? v2.TryGetValue<int>(out var i2) ? i2 :
                                                        throw new($"Invalid value for pozition in loader") :
                                                    throw new($"Invalid value for pozition in loader"));
                                        }
                                        else
                                            throw new($"Invalid value for pozition in loader");

                                        if (c.Value["offset"] is JsonArray array3)
                                        {
                                            if (array3.Count < 2)
                                                throw new($"Invalid value for offset in loader");

                                            offset = (array3[0] is JsonValue v5 ? v5.TryGetValue<int>(out var i5) ? i5 :
                                                        throw new($"Invalid value for offset in loader") :
                                                    throw new($"Invalid value for offset in loader"),
                                                    array3[1] is JsonValue v6 ? v6.TryGetValue<int>(out var i6) ? i6 :
                                                        throw new($"Invalid value for offset in loader") :
                                                    throw new($"Invalid value for offset in loader"));
                                        }
                                        else
                                            throw new($"Invalid value for offset in loader");
                                    }
                                    else
                                        (pozition, offset) = ((0, 0), (0, 0));
                                    charSize = c.Value["char-size"] is JsonValue v7 ? v7.TryGetValue<int>(out var i7) ? i7 :
                                                    throw new($"Invalid value for char-size in loader") :
                                                size.x;

                                    chars.Add(c.Key, (inde, charSize, pozition, size, offset));
                                }
                            }
                            break;
                    }
                }
            }
        }

        var defChar = json?["default-char"] is JsonValue vald ?
                vald.TryGetValue<string>(out var sd) ? (sd.Length == 1 ? sd[0] : 
                    throw new("Value \"default-char\" is invalid")) :
                throw new("Value \"default-char\" is invalid") : '?';
        var staticWidth = json?["static-char-width"] is JsonValue vals ?
                vals.TryGetValue<float>(out var @is) ? @is :
                throw new("Value \"static-char-width\" is invalid") : 0;

        return new(textures.ToArray(), chars, (
            json?["spaceing"]?["horizontal"] is JsonValue val1 ?
                    val1.TryGetValue<float>(out var f_1) ? f_1 :
                    throw new("Value \"spaceing/horizontal\" is invalid") :
                throw new("Value \"spaceing/horizontal\" is not set"),
            json?["spaceing"]?["vertical"] is JsonValue val2 ?
                    val2.TryGetValue<float>(out var f_2) ? f_2 :
                    throw new("Value \"spaceing/vertical\" is invalid") :
                throw new("Value \"spaceing/vertical\" is not set")),
            json?["tabs"] is JsonValue val3 ?
                    val3.TryGetValue<float>(out var f_3) ? f_3 :
                    throw new("Value \"spaceing/vertical\" is invalid") :
                throw new("Value \"spaceing/vertical\" is not set"),
            json?["default-char"] is JsonValue val4 ?
                val4.TryGetValue<string>(out var s4) ? 
                    (s4.Length == 1 ? s4[0] :
                        throw new("Value \"default-char\" is invalid")) :
                throw new("Value \"default-char\" is invalid") : '?',
            json?["static-char-width"] is JsonValue val5 ?
                val5.TryGetValue<float>(out var s5) ? s5 :
                throw new("Value \"static-char-width\" is invalid") : 0);
    }
}
