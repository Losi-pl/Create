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
    public event Func<(int x, int y)>? CursorGet;
    public event Func<(bool up, bool down, bool status)>? MouseLeft;
    public event Func<(bool up, bool down, bool status)>? MouseRight;
    public event Func<(bool up, bool down, bool status, int delta)>? MouseScroll;
    SpacePoint? last_hovered_over;

    public Interface(int width, int height)
    {
        main_layer = RenderLayer.Create()
            .SetSize(width, height)
            .Finisch();
        main = new(this);
        main.Size = (width, height);
        main_layer.Color = new Color();
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

    public void Phizic()
    {
        var cursor = (CursorGet ?? (() => (0, 0))).Invoke();
        var mouseleft = (MouseLeft ?? (() => (false, false, false))).Invoke();
        var mouseright = (MouseRight ?? (() => (false, false, false))).Invoke();
        var mousescroll = (MouseScroll ?? (() => (false, false, false, 0))).Invoke();

        var element = pointingAt(main, new())!;

        if (mouseleft.down || mouseright.down || mousescroll.down)
            if (element is not null)
            {
                var el = element;
                while (el.Parent is not null)
                {
                    el._onClick?.Invoke(el, 
                        mouseleft.down? 
                            ClickEventButton.Left: 
                       (mouseright.down? 
                            ClickEventButton.Right: 
                       (mousescroll.down? 
                            ClickEventButton.Scroll: 
                        ClickEventButton.Unknown)));

                    el = el.Parent;
                }
            }

        var hoverable = element;

        while (hoverable is null ? false : hoverable._onEnter is null && hoverable._onExit is null)
            hoverable = hoverable!.Parent;

        if (hoverable is not null)
        {
            if (last_hovered_over != hoverable)
                last_hovered_over?._onExit?.Invoke(last_hovered_over);

            hoverable._onEnter?.Invoke(hoverable);
            last_hovered_over = hoverable;
        }
        else if (last_hovered_over is not null)
        {
            last_hovered_over?._onExit?.Invoke(last_hovered_over);
            last_hovered_over = null!;
        }

        SpacePoint? pointingAt(SpacePoint space, Vector2 parent)
        {
            if (!space.Active)
                return null;

            var l_poz = space.Parent is not null ? 
                (space.Parent!.Size.ToVector() / -2) + 
                    (space.Parent!.Size.ToVector() * ((space.AnkerPoints.point1 + space.AnkerPoints.point2) / 2)) + 
                    space.Pozition.ToVector() :
                (main.Size.ToVector() / -2) + (main.Size.ToVector() * ((space.AnkerPoints.point1 + space.AnkerPoints.point2) / 2)) + space.Pozition.ToVector();
            l_poz = parent + l_poz;

            foreach(var sp in space.Childs.GetEnumerable().Reverse().Where(p => p.Interactable))
            {
                var v = pointingAt(sp, l_poz);
                if (v is not null)
                    return v;
            }
            
            var dis = (MathF.Abs(l_poz.X - cursor.x), MathF.Abs(l_poz.Y - cursor.y));
            if (dis.Item1 < space.Size.Width / 2 && dis.Item2 < space.Size.Height / 2)
                if (space != main)
                    return space;
            return null;
        }
    }
}
