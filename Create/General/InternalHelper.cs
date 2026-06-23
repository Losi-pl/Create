using Create.Graphics;

namespace Create.General;

internal static class InternalHelper
{
    extension(Shader.Uniform[] uniforms)
    {
        public ref Shader.Uniform Find(string name)
        {
            foreach (var i in uniforms.Length)
            {
                if (uniforms[i].Name == name)
                    return ref uniforms[i];
            }
            throw new KeyNotFoundException($"Uniform \"{name}\" not found.");
        }
    }
}