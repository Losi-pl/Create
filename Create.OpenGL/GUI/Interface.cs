using OpenTK.Mathematics;
using System.Drawing;

namespace Create.OpenGL.GUI;

/// <summary>
/// Interfejs interakcji z użytkownikiem
/// </summary>
public sealed class Interface
{
    RenderLayer main_layer;
    SpacePoint main;

    public Interface(int width, int height)
    {
        main_layer = RenderLayer.Create()
            .SetSize(width, height)
            .Finisch();
        main = new(this);
        main.Size = (width, height);
        main_layer.Color = Color.Transparent;
    }

    /// <summary>
    /// Ile na ile pikseli ma obraz interfejsu
    /// </summary>
    public (int Width, int Height) Size
    {
        get => ((int)main.Size.Width, (int)main.Size.Height);
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

    public SpacePoint.ChildrenList MainElements => main.Childs;

    /// <summary>
    /// Renderuj zawartość interfejsu
    /// </summary>
    public void Refrasch()
    {
        Matrix4 proj = Matrix4.CreateScale(2f / main_layer.Size.Width, 2f / main_layer.Size.Height, 1) * Matrix4.CreateTranslation(-1, -1, 0);
        Matrix4 mod = Matrix4.CreateTranslation(new Vector3(main_layer.Size.ToVector() / 2));

        main_layer.Clear();
        main_layer.ExecuteIn(() =>
        {
            foreach (var sp in this.main.Childs)
                draw_models(sp);
        });

        //Methods
        void draw_models(SpacePoint point)
        {
            if (!point.Active)
                return;
            
            point.Element?.Draw(mod * Matrix4.CreateTranslation(new Vector3(point.GlobalPozition.ToVector())) * proj);
            
            foreach (var sp in point.Childs)
                draw_models(sp);
        }
    }
}
