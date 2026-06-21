using System.Reflection;
using Create.General;
using Create.Graphics;
using Ico.Reader.Data;
using Silk.NET.Core;
using StbImageSharp;

namespace Create.Registry;

/// <summary>
/// This scene is focused around loading of elements and resources and error handling of such
/// </summary>
internal sealed class LoadingScene: Scene
{
    protected override void OnConnect()
    {
        Title = "Create: Loading";
        LoadIcon();
    }


    void LoadIcon()
    {
        using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream("Icon.ico");
        if(stream == null)
            throw new FileNotFoundException("Game icon not found");

        var ico = new Ico.Reader.IcoReader().Read(stream);
        if(ico == null)
            throw new FileLoadException("Game failed to load properly");

        var icons = new RawImage[ico.ImageReferences.Count(i => i.IcoType == IcoType.Icon)];
        int index = 0;
        foreach (var image in ico.ImageReferences.GetRefEnum())
        {
            if(image.IcoType != IcoType.Icon)
                continue;
            
            ImageResult decoded = ImageResult.FromMemory(ico.GetImage(image), ColorComponents.RedGreenBlueAlpha);
            
            icons[index] = new RawImage(image.Width, image.Height, decoded.Data);
        }

        Icon = icons;
    }
}