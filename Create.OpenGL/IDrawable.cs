using OpenTK.Mathematics;

namespace Create.OpenGL;

public interface IDrawable
{
    public void Draw(Matrix4 projection, Matrix4 model);
}

public static class Drawable
{
    public static void Draw(this IDrawable model, Matrix4 projectionMatrix) =>
        model.Draw(projectionMatrix, Engine.NeutralMatrix);
    public static void Draw(this IDrawable model) =>
        model.Draw(Engine.NeutralMatrix, Engine.NeutralMatrix);
}