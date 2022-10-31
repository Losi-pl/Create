using Create.Render;
using Create.Resource;
using SixLabors.ImageSharp;

namespace Create;

public static partial class Assets
{
    static MargedResources? resources;

    internal static void load_resources()
    {
        var cons = Resources.FromOthers();
        foreach (var r in all_resources())
            cons.MargeFiles(r, "assets", "");

        resources = cons.Finish();

        //Methods
        IEnumerable<Resources> all_resources() => (new[] { mod_resources(), resource_packs() }).Combine();
        IEnumerable<Resources> mod_resources() => ((IEnumerable<Mod>)Mod.All).ConvertAll(m => m.Resources);
        IEnumerable<Resources> resource_packs() => Enumerable.Empty<Resources>();
    }

    public static Resources? Resources => resources;

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

        BlockAtlas.finish_attlas();

        void load_package(ResourceDirectory directory)
        {
            textures(directory);
        }

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
    }
}
