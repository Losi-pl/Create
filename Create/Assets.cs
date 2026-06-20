using Create.Linq;
using Create.Resource;
using SixLabors.ImageSharp;
using System.IO;
using System.Text.Json.Nodes;

namespace Create;

/// <summary>
/// Klasa przechowująca globalny pakiet zasobów i podklasy ułatwiające dostęp do danych
/// </summary>
public static partial class Assets
{
    static MargedResources? resources;

    /// <summary>
    /// Takes all available sources of assets and merges them into a single source<br/>
    /// TODO Stop that and separate them back out
    /// </summary>
    internal static void LoadGlobalResources()
    {
        var cons = Resources.FromOthers();
        foreach (var r in LoadAllResources())
            cons.MargeFiles(r, "assets", "");

        resources = cons.Finish();

        //Methods
        IEnumerable<Resources> LoadAllResources() => LoadModResources().Concat(LoadExternalResources());
        IEnumerable<Resources> LoadModResources() => Mod.All.ToEnumerable().Select(m => m.Resources);
        IEnumerable<Resources> LoadExternalResources() => Enumerable.Empty<Resources>();
    }

    /// <summary>
    /// Globalny pakiet zasobów
    /// </summary>
    public static Resources? Resources => resources;

    /// <summary>
    /// Pobiera dane z globalnedo pakieto i dystrybuuje je do odpowiednich elementów gry
    /// <para>
    ///   Rozmieszcza tekstury bloków do <see cref="BlockAtlas"/>
    /// </para>
    /// </summary>
    internal static void first_proces_resources()
    {
        if (resources == null)
            return;

        foreach (var resors in resources.MainDirectories)
        {
            #if DEBUG
            OnLoadPackage(resors);
            #else
            try
            { OnLoadPackage(resors); }
            catch (Exception ex)
            { throw new($"Ładowanie pakietu {resors.Name} niepowiodło się", ex); }
            #endif
        }

        ClearTextures();
        BlockAtlas.FinishTextureAttlas();


        //Methods
        void textures(ResourceDirectory directory)
        {
            string pack_name = directory.Name;
            directory = directory.GetSubPath("textures");
            foreach (var texture in directory.GetSubPath("blocks").Files)
                BlockAtlas.set_texture(load_image(texture.GetStream(), texture.Name), $"{pack_name}:{texture.Name}");

            //Methods
            Image load_image(Stream stream, string name)
            {
                #if DEBUG
                return Image.Load(stream);
                #else
                try
                { return Image.Load(stream); }
                catch (Exception ex)
                { throw new($"Konwertowanie tekstury {pack_name}:{name} niepowiodło się", ex); }
                #endif
            }
        }
        void ProcessLanguage(ResourceDirectory directory, string language)
        {
            // Name of the loadet package
            string pack_name = directory.Name;
            directory = directory.GetSubPath("language");

            // Load default (en-us) language file
            var @default = directory.GetFile("en-us");

            // Load used language file
            var used = directory.IsPathExist(language) ? directory.GetFile(language) : null;

            // Process words from the default language file
            foreach (var word in LoadAllKeys(JsonNode.Parse(@default.GetStream()) as JsonObject ?? new(), string.Empty))
                Language.AddWord($"{pack_name}.{word.key}", word.value);

            // Process words from the used language file
            if(used is not null)
                foreach (var word in LoadAllKeys(JsonNode.Parse(used.GetStream()) as JsonObject ?? new(), string.Empty))
                    Language.AddWord($"{pack_name}.{word.key}", word.value);

            //Methods
            IEnumerable<(string key, string value)> LoadAllKeys(JsonObject list, string key_dase)
            {
                foreach(var o in list)
                {
                    // Clean name. Remove dot's[.] from start and end. Convert name to lowwer case
                    var name = o.Key[0] == '.' ?
                        (o.Key[^1] == '.' ? o.Key.Substring(1, o.Key.Length - 2) : o.Key.Substring(1)) :
                        (o.Key[^1] == '.' ? o.Key.Remove(o.Key.Length - 2) : o.Key);
                    name = name.ToLower();

                    // Go through all keys
                    switch (o.Value)
                    {
                        // Hangle a single string value
                        case JsonValue v when v.TryGetValue<string>(out var s):
                            yield return (string.IsNullOrEmpty(key_dase) ? name : $"{key_dase}.{name}", s);
                            break;

                        // Hangle a sub-object with more keys
                        case JsonObject so:
                            foreach (var k in LoadAllKeys(so, string.IsNullOrEmpty(key_dase) ? name : $"{key_dase}.{name}"))
                                yield return k;
                            break;
                    }
                }
            }
        }
        void OnLoadPackage(ResourceDirectory directory)
        {
            textures(directory);
            ProcessLanguage(directory, "pl-pl");
        }
        void ClearTextures()
        {
            foreach(var kvp in Assets.textures)
                if(kvp.Value.TryGetTarget(out var tex))
                    tex.Dispose();
        }
    }
}
