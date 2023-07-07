namespace Create.OpenGL;

/// <summary>
/// Gdy kompilacja shedera w karcie graficznej się nie powiedze
/// </summary>
public class ShaderCompilationException : Exception
{
    string? vertex, fragment;

    public string? VertexErrors => vertex;
    public string? FragmentErrors => fragment;

    public ShaderCompilationException(string? vertex, string? fragment)
    {
        if (string.IsNullOrWhiteSpace(vertex) && string.IsNullOrWhiteSpace(fragment))
            throw new ArgumentException("You need to specyfy al least one error");

        this.vertex = vertex;
        this.fragment = fragment;
    }

    public override string Message
    {
        get
        {
            if (vertex != null)
            {
                if (fragment != null)
                    return $"Vertex:\n{vertex}\n\nFragment:\n{fragment}";
                else
                    return $"Vertex:\n{vertex}";
            }
            else
            {
                return $"Fragment:\n{fragment}";
            }
        }
    }
}
