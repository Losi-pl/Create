using Create.Linq;
using Create.Resource;
using SixLabors.ImageSharp;

namespace Create;

/// <summary>
/// Klasa przechowująca globalny pakiet zasobów i podklasy ułatwiające dostęp do danych
/// </summary>
public static partial class Assets
{
    static MargedResources? resources;

    /// <summary>
    /// Ładuje wszystkie pakiety zasobów i łączy je w jeden globalny pakiet
    /// </summary>
    internal static void load_resources()
    {
        var cons = Resources.FromOthers();
        foreach (var r in all_resources())
            cons.MargeFiles(r, "assets", "");

        resources = cons.Finish();

        //Methods
        IEnumerable<Resources> all_resources() => mod_resources().Concat(resource_packs());
        IEnumerable<Resources> mod_resources() => Mod.All.Select(m => m.Resources);
        IEnumerable<Resources> resource_packs() => Enumerable.Empty<Resources>();
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
            load_package(resors);
            #else
            try
            {
                textures(resors);
            }
            catch (Exception ex)
            { throw new($"Ładowanie pakietu {resors.Name} niepowiodło się", ex); }
            #endif
        }

        clear_textures();
        BlockAtlas.finish_attlas();


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
        void load_package(ResourceDirectory directory)
        {
            textures(directory);
        }
        void clear_textures()
        {
            foreach(var kvp in Assets.textures)
                if(kvp.Value.TryGetTarget(out var tex))
                    tex.Dispose();
        }
    }
}
