using System.Drawing;
using System.Reflection;
using Create.General;
using Create.Graphics;
using Ico.Reader.Data;
using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using StbImageSharp;
using Shader = Create.Graphics.Shader;

namespace Create.Registry;

/// <summary>
/// This scene is focused around loading of elements and resources and error handling of such
/// </summary>
internal sealed class LoadingScene: Scene
{
    private static Shader _shader = null!;
    private static Mesh _mesh = null!;
    
    protected override void OnConnect()
    {
        Title = "Create: Loading";
        LoadIcon();
        
        Window.GL.ClearColor(Color.FromArgb(255, 27, 72, 8));

        _shader = Shader.Create()
            .Vertex(Assembly.GetCallingAssembly().GetManifestResourceStream("main/create/shaders/base.vert")!)
            .Fragment(Assembly.GetCallingAssembly().GetManifestResourceStream("main/create/shaders/base.frag")!)
            .Finish();
        
        _shader.SetUniform("border", .5f);

        _mesh = Mesh.Create(_shader).ManualFillOut()
            .SetDataLayout(Mesh.DataLayout.Interleaved)
            .SetAttribute("aPos", new Vector3D<float>[]
            {
                new(-0.5f, -0.5f, 0.0f), 
                new( 0.5f, -0.5f, 0.0f), 
                new( 0.0f,  0.5f, 0.0f)
            }).SetAttribute("aColor", new Vector3D<int>[]
            {
                new(1, 0, 0), 
                new(0, 1, 0), 
                new(0, 0, 1)
            }).Finish().ThreadBind();
    }

    public override void RenderUpdate(double delta)
    {
        var gl = Window.GL;
        
        gl.Clear(ClearBufferMask.ColorBufferBit);

        _mesh.Draw();
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