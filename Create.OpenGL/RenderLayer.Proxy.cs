using Create.OpenGL.Textures;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace Create.OpenGL;

partial class RenderLayer
{
    /// <summary>
    /// Debuger do <see cref="RenderLayer"/>
    /// </summary>
    class Proxy
    {
        RenderLayer render;
        public Proxy(RenderLayer render)
        {
            this.render = render;
        }
        public string get_name() => render.disposed ? "Render Layer: Disposed" : $"Render Layer: Handle: {render.handles.frame_buffer}";

        public ReadOnlyDictionaryView<FramebufferAttachment, RenderTexture> FrameLayers => new(render.textures!.ToDictionary(t => t.chanel, t => t.texture));
        public bool DrawOnly => render.draw_only;
        public Color4 BackgroundColor => render.color;
        public List<IDrawable> Meshes => render.Meshes;
        public CustomModel_? CustomModel => render.custom_model.HasValue ? new(render) : null;
        public (int Width, int Height) Size => (render.buffer_creation_data.width, render.buffer_creation_data.height);

        [DebuggerDisplay("")]
        public class CustomModel_
        {
            public CustomModel_(RenderLayer render)
            {
                Shader = render.custom_model!.Value.shader;
                Mesh = render.custom_model!.Value.model;
            }
            public Shader Shader;
            public Mesh Mesh;
        }
    }
}
