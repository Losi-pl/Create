using System.Diagnostics;

namespace Create.OpenGL;

partial class Shader
{
    /// <summary>
    /// Debuger do <see cref="Shader"/>a
    /// </summary>
    class Proxy
    {
        Shader shader;
        public Proxy(Shader shader)
        {
            this.shader = shader;
        }

        public ReadOnlyDictionaryView<string, AttributInfo> Attributes =>
            new(shader.attributInfos.ToDictionary(attr => attr.Name));
        public ReadOnlyDictionaryView<string, UniformInfo> Uniforms =>
            new(shader.uniformInfos.ToDictionary(uni => uni.Name));
        public VertexAttributesBind VerticesBind => shader.bind;
        public int Handle => shader.handle;
        public StandardMechanisc_ CreateMechanisc => new()
        {
            Pozition = shader.pozition_variable,
            Rotation = shader.rotation_variable,
            Matrix = shader.use_default_matrix_mehanic
        };
        public OpenGLMechanisc_ OpenGLMechanisc => new(shader);

        public object get_status() => shader.disposed ? "Dispose" : $"Handle: {shader.handle}";

        [DebuggerDisplay("")]
        public class StandardMechanisc_
        {
            public (string Name, int Handle)? Pozition;
            public (string Name, int Handle)? Rotation;
            public (string Name, int Handle)? Matrix;
            public override string ToString() => string.Empty;
        }
        [DebuggerDisplay("")]
        public class OpenGLMechanisc_
        {
            public OpenGLMechanisc_(Shader shader)
            {
                AlphaTest = new() { active = shader.simple_mekanizms.alphatest };
                DepthTest = new() { active = shader.simple_mekanizms.depthtest };
                CullFace = shader.cull_face != CullFaceMode.FrontAndBack ? shader.cull_face : new System() { active = false };
                Blend = shader.blend.HasValue ? shader.blend : new System() { active = false };
            }

            public readonly System AlphaTest;
            public readonly System DepthTest;
            public readonly object CullFace;
            public readonly object Blend;
        }
        [DebuggerDisplay("{(active ? \"Enabled\" : \"Disabled\"),nq}")]
        public struct System
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public bool active;
        }

    }
}
