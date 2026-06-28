using System.Reflection;
using Create.Graphics;
using System.Drawing;
using Create.World;

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
        
        Window.GL.ClearColor(Color.FromArgb(255, 27, 72, 8)); 
    }

    /// <summary>
    /// Loads and parses the Icon from the game resources
    /// </summary>
    /// <exception cref="FileNotFoundException">If the file of the icon could not be found</exception>
    /// <exception cref="FileLoadException">If the process of icon parsing had failed</exception>
    private void LoadIcon()
    {
        using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream("Icon.ico");
        if(stream == null)
            throw new FileNotFoundException("Game icon not found");

        var ico = new Ico.Reader.IcoReader().Read(stream);
        if(ico == null)
            throw new FileLoadException("Game icon failed to load properly");

        var icons = new Silk.NET.Core.RawImage[ico.ImageReferences.Count(i => i.IcoType == Ico.Reader.Data.IcoType.Icon)];
        int index = 0;
        foreach (var image in ico.ImageReferences.GetRefEnum())
        {
            if(image.IcoType != Ico.Reader.Data.IcoType.Icon)
                continue;
            
            var decoded = StbImageSharp.ImageResult.FromMemory(ico.GetImage(image), StbImageSharp.ColorComponents.RedGreenBlueAlpha);
            
            icons[index] = new Silk.NET.Core.RawImage(image.Width, image.Height, decoded.Data);
        }

        Icon = icons;
    }

    private double _foeLoading = 3;
    public override void LogicUpdate(double delta)
    {
        _foeLoading -= delta;
        if(_foeLoading <= 0)
            SwapScene(new GameSession());
    }
}