using OpenTK.Mathematics;

namespace Create.OpenGL;

/// <summary>
/// Urzywany do renderowania elementów na ekranie
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Renderowanie modelu na obecnie aktywnym płutnie
    /// </summary>
    /// <param name="projection">Modyfikacja projekcji jak pozycja kamery i orjentacja</param>
    /// <param name="model">Modyfikacja geometri modelu przed jego zmianą w przestrzeni</param>
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
    /// <summary>
    /// <inheritdoc cref="IDrawable.Draw(Matrix4, Matrix4)"/>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="projectionMatrix">Modyfikacja projekcji jak pozycja kamery i orjentacja</param>
    public static void Draw(this IDrawable model, Matrix4 projectionMatrix) =>
        model.Draw(projectionMatrix, Engine.NeutralMatrix);

    /// <summary>
    /// <inheritdoc cref="IDrawable.Draw(Matrix4, Matrix4)"/>
    /// </summary>
    public static void Draw(this IDrawable model) =>
        model.Draw(Engine.NeutralMatrix, Engine.NeutralMatrix);
}