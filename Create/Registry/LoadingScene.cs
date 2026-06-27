using System.Drawing;
using System.Numerics;
using System.Reflection;
using Create.General;
using Create.Graphics;
using Create.World;
using Ico.Reader.Data;
using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using StbImageSharp;

namespace Create.Registry;

/// <summary>
/// This scene is focused around loading of elements and resources and error handling of such
/// </summary>
internal sealed class LoadingScene: Scene
{
    private RealmWorld _world = null!;
    private static Mesh _worldMesh = null!;
    
    protected override void OnConnect()
    {
        Title = "Create: Loading";
        LoadIcon();
        
        Window.GL.ClearColor(Color.FromArgb(255, 27, 72, 8));

        _world = new()
        {
            [0, 0, 0] = true,
            [0, 0, 1] = true,
            [1, 0, 0] = true,
            [1, 0, 1] = true,
            [0, 1, 0] = true,
            [1, 1, 1] = true
        };

        _worldMesh = new ChunkModeler().GenerateModel(_world).ThreadBind();

        var fovRadians = 70f * MathF.PI / 180f;
        var aspect = Size.X / (float)Size.Y;

        var projection = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(fovRadians, aspect, .1f, 512f * 3 / 2);
        var view = Matrix4x4.CreateLookAtLeftHanded(new(0, 5, -5), new(0, 0, 0), new(0, 1, 0));

        _worldMesh.Shader.SetUniform("projection", projection);
        _worldMesh.Shader.SetUniform("view", view);
        _worldMesh.Shader.SetUniform("model", Matrix4X4<float>.Identity);
        
        Window.GL.Enable(EnableCap.DepthTest);
    }

    public override void WindowResize(Vector2D<int> newSize)
    {
        var fovRadians = 70f * MathF.PI / 180f;
        var aspect = Size.X / (float)Size.Y;

        var projection = Matrix4x4.CreatePerspectiveFieldOfViewLeftHanded(fovRadians, aspect, .1f, 512f * 3 / 2);
        
        _worldMesh.Shader.SetUniform("projection", projection);
    }

    public override void RenderUpdate(double delta)
    {
        var gl = Window.GL;
        
        gl.Clear(ClearBufferMask.ColorBufferBit);

        _worldMesh.Draw();
    }

    private double _rotate;
    public override void LogicUpdate(double delta)
    {
        _rotate += delta * 90;
        var mod = Matrix4x4.CreateTranslation(-1, -1, -1) * Matrix4x4.CreateRotationY((float)_rotate * (MathF.PI / 180f));
        _worldMesh.Shader.SetUniform("model", mod);
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