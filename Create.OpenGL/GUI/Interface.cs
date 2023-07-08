using OpenTK.Mathematics;
using System.Drawing;

namespace Create.OpenGL.GUI;

/// <summary>
/// Interfejs interakcji z użytkownikiem
/// </summary>
public sealed class Interface
{
    RenderLayer main_layer;
    SpacePoint main = new();

    public Interface(int width, int height)
    {
        main_layer = RenderLayer.Create()
            .SetSize(width, height)
            .Finisch();
        main.Size = (width, height);
        main_layer.Color = Color.Transparent;
    }

    /// <summary>
    /// Ile na ile pikseli ma obraz interfejsu
    /// </summary>
    public (int Width, int Height) Size
    {
        get => main.Size;
        set
        {
            main_layer.Resize(value.ToVector());
            main.Size = value;
        }
    }

    /// <summary>
    /// Płutno w wyrenderowanym obrazem
    /// </summary>
    public RenderLayer Canvas => main_layer;

    public List<SpacePoint> MainElements => main.Children;

    /// <summary>
    /// Renderuj zawartość interfejsu
    /// </summary>
    public void Refrasch()
    {
        Matrix4 main = Matrix4.CreateScale(1f / main_layer.Size.Width, 1f / main_layer.Size.Height, 1);
        main_layer.Clear();
        main_layer.ExecuteIn(() =>
        {
            foreach(var sp in this.main.Children)
                draw_models(sp);
        });

        //Methods
        void draw_models(SpacePoint point)
        {
            point.Element?.Draw(Matrix4.CreateTranslation(new Vector3(point.GlobalPozition.ToVector())) * main, Engine.NeutralMatrix, point);
            
            foreach (var sp in point.Children)
                draw_models(sp);
        }
    }
}
