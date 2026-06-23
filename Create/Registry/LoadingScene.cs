using System.Drawing;
using System.Reflection;
using Create.General;
using Create.Graphics;
using Ico.Reader.Data;
using Silk.NET.Core;
using Silk.NET.OpenGL;
using StbImageSharp;
using Shader = Create.Graphics.Shader;

namespace Create.Registry;

/// <summary>
/// This scene is focused around loading of elements and resources and error handling of such
/// </summary>
internal sealed class LoadingScene: Scene
{
    // Shader program handle
    private static Shader _shader = null!;

    // Vertex array object and buffer
    private static uint _vao;
    private static uint _vbo;

    // Vertex shader source
    private const string VertexShaderSource = @"
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 ourColor;

void main()
{
    gl_Position = vec4(aPos, 1.0);
    ourColor = aColor;
}
";

    // Fragment shader source
    private const string FragmentShaderSource = @"
#version 330 core
in vec3 ourColor;
out vec4 FragColor;

uniform float border;

void main()
{
    FragColor = vec4(ourColor, 1.0);
    if(FragColor.r < border && FragColor.g < border && FragColor.b < border)
        FragColor = vec4(0.0, 0.0, 0.0, 1.0);
}
";
    
    protected override void OnConnect()
    {
        Title = "Create: Loading";
        LoadIcon();
        
        Window.GL.ClearColor(Color.FromArgb(255, 27, 72, 8));

        _shader = Shader.Create()
            .Vertex(VertexShaderSource)
            .Fragment(FragmentShaderSource)
            .Finish();
        
        _shader.SetUniform("border", .5f);
        
        float[] vertices =
        {
            -0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,
            0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f
        };

        var gl = Window.GL;
        
        _vao = gl.GenVertexArray();
        _vbo = gl.GenBuffer();

        gl.BindVertexArray(_vao);

        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        gl.BufferData(BufferTargetARB.ArrayBuffer, vertices, BufferUsageARB.StaticDraw);

        const int stride = 6 * sizeof(float);
        gl.UseProgram(_shader.Handle);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));

        gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        gl.BindVertexArray(0);
        gl.UseProgram(0);
    }

    public override void RenderUpdate(double delta)
    {
        var gl = Window.GL;
        
        gl.Clear(ClearBufferMask.ColorBufferBit);

        gl.UseProgram(_shader.Handle);
        gl.BindVertexArray(_vao);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
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