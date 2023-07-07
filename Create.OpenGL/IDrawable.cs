using OpenTK.Mathematics;

namespace Create.OpenGL;

/// <summary>
/// Urzywany do renderowania elementów na ekranie
/// </summary>
public interface IDrawable
{
    public void Draw(Matrix4 projection, Matrix4 model);

    /// <summary>
    /// Pusty model który nic nie renderuje
    /// </summary>
    public static IDrawable None { get; } = new empty_model();
    
    /// <summary>
    /// <inheritdoc cref="None"/>
    /// </summary>
    private struct empty_model : IDrawable
    {
        public void Draw(Matrix4 projection, Matrix4 model) { }
    }
}

/// <summary>
/// Dodatkowe warianty dla metody <see cref="IDrawable.Draw(Matrix4, Matrix4)"/>
/// </summary>
public static class Drawable
{
    public static void Draw(this IDrawable model, Matrix4 projectionMatrix) =>
        model.Draw(projectionMatrix, Engine.NeutralMatrix);
    public static void Draw(this IDrawable model) =>
        model.Draw(Engine.NeutralMatrix, Engine.NeutralMatrix);
}