using OpenTK.Mathematics;

namespace Create.OpenGL.GUI;

/// <summary>
/// Lokalizacja obiektu na ekranie
/// </summary>
public sealed class SpacePoint
{
    SpacePoint parent = null!;
    List<SpacePoint> childs = new List<SpacePoint>();
    Vector2 ancor1 = new(.5f, .5f), ancor2 = new(.5f, .5f);
    Element? element;

    int width, height;
    int poz_x, poz_y;

    /// <summary>
    /// Wrzystkie podrzędne elementy tego obiektu
    /// </summary>
    public List<SpacePoint> Children => childs;

    /// <summary>
    /// Jak ma zostać wyrenderowany ten obiekt w <see cref="Interface"/>
    /// </summary>
    public Element? Element { get => element; set => element = value; }

    /// <summary>
    /// Pozycja na płutnie
    /// </summary>
    public (int x, int y) Pozition
    {
        set
        {
            (poz_x, poz_y) = value;
        }
        get => (poz_x, poz_y);
    }
    
    public (int x, int y) GlobalPozition
    {
        get
        {
            (SpacePoint? point, (int x, int y) poz) data = (parent, Pozition);

            while (data.point?.parent is not null)
                data = (data.point.parent, (data.point.poz_x + data.poz.x, data.point.poz_y + data.poz.y));

            return data.poz;
        }
        set
        {
            (SpacePoint? point, (int x, int y) poz) data = (parent, (0, 0));

            while (data.point?.parent is not null)
                data = (data.point.parent, (data.point.poz_x + data.poz.x, data.point.poz_y + data.poz.y));

            poz_x = value.x - data.poz.x;
            poz_y = value.y - data.poz.y;
        }
    }

    /// <summary>
    /// Rozmiary na płutnie
    /// </summary>
    public (int Width, int Height) Size
    {
        get => (width, height);
        set
        {
            (width, height) = value;

        }
    }

    /// <summary>
    /// Tryb zamocowania elementu w przestrzeni
    /// </summary>
    public Anker AnkerMode
    {
        set
        {
            switch (value)
            {
                case Anker.Center:           (ancor1, ancor2) = (new(.5f, .5f), new(.5f, .5f)); break;
                case Anker.Left:             (ancor1, ancor2) = (new(0,   .5f), new(0,   .5f)); break;
                case Anker.Right:            (ancor1, ancor2) = (new(1,   .5f), new(1,   .5f)); break;
                case Anker.Up:               (ancor1, ancor2) = (new(.5f,   1), new(.5f,   1)); break;
                case Anker.Down:             (ancor1, ancor2) = (new(.5f,   0), new(.5f,   0)); break;
                case Anker.LeftUp:           (ancor1, ancor2) = (new(0,     1), new(0,     1)); break;
                case Anker.RightUp:          (ancor1, ancor2) = (new(1,     1), new(1,     1)); break;
                case Anker.LeftDown:         (ancor1, ancor2) = (new(0,     0), new(0,     0)); break;
                case Anker.RightDown:        (ancor1, ancor2) = (new(1,     0), new(1,     0)); break;
                case Anker.HorizontalCenter: (ancor1, ancor2) = (new(0,   .5f), new(1,   .5f)); break;
                case Anker.VerticalCenter:   (ancor1, ancor2) = (new(.5f,   0), new(.5f,   1)); break;
                case Anker.All:              (ancor1, ancor2) = (new(0,     0), new(1,     1)); break;

                case Anker.Custome: break;
            }
        }
        get
        {
            return ((ancor1, ancor2)) switch
            {
                {ancor1: {X: .5f, Y: .5f}, ancor2: {X: .5f, Y: .5f}} => Anker.Center,
                {ancor1: {X:   0, Y: .5f}, ancor2: {X:   0, Y: .5f}} => Anker.Left,
                {ancor1: {X:   1, Y: .5f}, ancor2: {X:   1, Y: .5f}} => Anker.Right,
                {ancor1: {X: .5f, Y:   1}, ancor2: {X: .5f, Y:   1}} => Anker.Up,
                {ancor1: {X: .5f, Y:   0}, ancor2: {X: .5f, Y:   0}} => Anker.Down,
                {ancor1: {X:   0, Y:   1}, ancor2: {X:   0, Y:   1}} => Anker.LeftUp,
                {ancor1: {X:   1, Y:   1}, ancor2: {X:   1, Y:   1}} => Anker.RightUp,
                {ancor1: {X:   0, Y:   0}, ancor2: {X:   0, Y:   0}} => Anker.LeftDown,
                {ancor1: {X:   1, Y:   0}, ancor2: {X:   1, Y:   0}} => Anker.RightDown,
                {ancor1: {X:   0, Y: .5f}, ancor2: {X:   1, Y: .5f}} => Anker.HorizontalCenter,
                {ancor1: {X: .5f, Y:   0}, ancor2: {X: .5f, Y:   1}} => Anker.VerticalCenter,
                {ancor1: {X:   0, Y:   0}, ancor2: {X:   1, Y:   1}} => Anker.All,

                _ => Anker.Custome,
            };
        }
    }

    /// <summary>
    /// Punkty zamocowania modelu w przestrzeni
    /// </summary>
    public (Vector2 point1, Vector2 point2) AnkerPoints
    {
        get => (ancor1, ancor2);
        set => (ancor1, ancor2) = value;
    }

    /// <summary>
    /// <inheritdoc cref="AnkerMode"/>
    /// </summary>
    [Flags]
    public enum Anker
    {
        Center = 1,
        Left = 2,
        Right = 4,
        Up = 8,
        Down = 16,
        Custome = 32,
        LeftUp = Left | Up,
        RightUp = Right | Up,
        LeftDown = Left | Down,
        RightDown = Right | Down,
        HorizontalCenter = Left | Right,
        VerticalCenter = Up | Down,
        All = Up | Down | Left | Right
    }
}
