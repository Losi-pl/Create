using OpenTK.Mathematics;
using System.Diagnostics;

namespace Create.OpenGL.GUI;

/// <summary>
/// Lokalizacja obiektu na ekranie
/// </summary>
public sealed class SpacePoint
{
    SpacePoint? parent = null;
    List<SpacePoint> childs = new List<SpacePoint>();
    Vector2 ancor1 = new(.5f, .5f), ancor2 = new(.5f, .5f);
    Interface @interface;
    Element? element;
    bool active = true;

    int width, height;
    int poz_x, poz_y;

    public SpacePoint() { @interface = null!; }

    public SpacePoint(Interface @interface)
    {
        if (@interface is null) throw new ArgumentNullException(nameof(@interface));
        
        this.@interface = @interface;
        try   { @interface.MainElements.AddChild(this); }
        catch { }
    }

    public SpacePoint(SpacePoint point)
    {
        (poz_x, poz_y) = point.GlobalPozition;
        @interface = null!;
        point.Childs.AddChild(this);
    }

    /// <summary>
    /// Wrzystkie podrzędne elementy tego obiektu
    /// </summary>
    public ChildrenList Childs => new(this);

    /// <summary>
    /// Obiekt do kturego ten <see cref="SpacePoint"/> jest włorzony
    /// </summary>
    public SpacePoint? Parent
    {
        get => parent?.parent != null ? parent : null;
        set
        {
            if (@interface is null)
                return;
            if(value is null)
            {
                parent?.Childs.RemoveChild(this);
                @interface.MainElements.AddChild(this);
            }
            else
            {
                parent?.Childs.RemoveChild(this);
                this.Childs.AddChild(this);
            }    
        }
    }

    /// <summary>
    /// Jak ma zostać wyrenderowany ten obiekt w <see cref="Interface"/>
    /// </summary>
    public Element? Element
    {
        get => element; 
        set
        {
            element?.set_element(null!);
            value?.set_element(this);
            element = value;
        }
    }

    /// <summary>
    /// <see cref="GUI.Interface"/> z którym ten <see cref="SpacePoint"/> jest połączony
    /// </summary>
    public Interface Interface
    {
        get => @interface;
        set
        {
            if(value is null)
            {
                parent?.Childs.RemoveChild(this);
                parent = null;
                @interface = null!;
            }
            else if(value != @interface)
            {
                parent?.Childs.RemoveChild(this);
                value.MainElements.AddChild(this);
            }
        }
    }

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
    
    /// <summary>
    /// Pozycja globalna na płutnie
    /// </summary>
    public (int x, int y) GlobalPozition
    {
        get
        {
            int count = 0;
            SpacePoint? point = parent;

            while (point != null)
                (count, point) = (++count, point.parent);

            Span<(Vector2 delt_anch, Vector2i poz, Vector2i size)> hierarchy = stackalloc (Vector2, Vector2i, Vector2i)[count];

            (count, point) = (--count, parent);
            (Vector2 a1, Vector2 a2) anc = (ancor1, ancor2);

            while(point != null)
            {
                hierarchy[count] = ((anc.a1 + anc.a2) / 2, point.Pozition.ToVector(), point.Size.ToVector());
                anc = (point.ancor1, point.ancor2);
                point = point.parent;
                count--;
            }

            Span<Vector2i> simplyf = stackalloc Vector2i[hierarchy.Length];
            count = 0;
            for (; count < simplyf.Length; count++)
                simplyf[count] = hierarchy[count].poz - (hierarchy[count].size / 2) + (Vector2i)(hierarchy[count].delt_anch * hierarchy[count].size);

            Vector2i sum = new();

            for (count = 0; count < simplyf.Length; count++)
                sum += simplyf[count];

            return (sum + new Vector2i(poz_x, poz_y)).ToTumple();
        }
        set
        {
            var old = GlobalPozition;

            poz_x = value.x - old.x;
            poz_y = value.y - old.y;
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

    public bool Active { get => active; set => active = value; }

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

    /// <summary>
    /// Lista pod obiektów dla <see cref="SpacePoint"/>
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(Proxy))]
    public ref struct ChildrenList
    {
        SpacePoint point;

        public ChildrenList(SpacePoint point) => this.point = point;

        /// <summary>
        /// Zwraca Kolekcje
        /// </summary>
        public IEnumerable<SpacePoint> GetEnumerable() => point.childs.Cast<SpacePoint>();

        /// <summary>
        /// Jest kolekcją
        /// </summary>
        /// <returns></returns>
        public IEnumerator<SpacePoint> GetEnumerator() => point.childs.GetEnumerator();

        /// <summary>
        /// Dodaj nowy pod-obiekt do listy
        /// </summary>
        public void AddChild(SpacePoint point)
        {
            (int, int)? startpoz = (point.@interface != this.point.@interface && point.@interface != null) ? point.GlobalPozition : null;
            if (point.parent != null)
                point.parent.Childs.RemoveChild(point);
            point.@interface = this.point.@interface;
            point.parent = this.point;
            this.point.childs.Add(point);
            if(startpoz.HasValue)
            point.GlobalPozition = startpoz.Value;
            i_(point, this.point.@interface);

            //Methods
            void i_(SpacePoint point, Interface i)
            {
                point.@interface = i;

                foreach (var ch in point.childs)
                    i_(ch, i);
            }
        }

        /// <summary>
        /// Dodaj nowy pod-obiekt do listy
        /// </summary>
        public void InsertChild(SpacePoint point, int index)
        {
            (int, int)? startpoz = (point.@interface != this.point.@interface && point.@interface != null) ? point.GlobalPozition : null;
            if (point.parent != null)
                point.parent.Childs.RemoveChild(point);
            point.@interface = this.point.@interface;
            point.parent = this.point;
            this.point.childs.Insert(index, point);
            if (startpoz.HasValue)
                point.GlobalPozition = startpoz.Value;
            i_(point, this.point.@interface);

            //Methods
            void i_(SpacePoint point, Interface i)
            {
                point.@interface = i;

                foreach (var ch in point.childs)
                    i_(ch, i);
            }
        }

        /// <summary>
        /// Usuwa pod-obiekt z listy
        /// </summary>
        public void RemoveChild(int index)
        {
            point.childs[index].parent = null;
            point.childs[index].@interface = null!;
            this.point.childs.RemoveAt(index);
        }

        /// <summary>
        /// Usuwa pod-obiekt z listy
        /// </summary>
        public void RemoveChild(SpacePoint point)
        {
            point.parent = null;
            point.@interface = null!;
            this.point.childs.Remove(point);
        }

        /// <summary>
        /// Ile pod-obiektów jest na liście
        /// </summary>
        public int Count => point.childs.Count;

        public SpacePoint this[int index] => point.childs[index];

        /// <summary>
        /// Debuger dla <see cref="SpacePoint.ChildrenList"/>
        /// </summary>
        struct Proxy
        {
            SpacePoint point;
            public Proxy(ChildrenList point) => this.point = point.point;
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public SpacePoint[] elements => point.childs.ToArray();
        }
    }
}
