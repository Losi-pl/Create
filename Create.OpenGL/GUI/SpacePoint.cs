using OpenTK.Mathematics;
using System.Diagnostics;

namespace Create.OpenGL.GUI;

/// <summary>
/// Lokalizacja obiektu na ekranie
/// </summary>
[DebuggerDisplay("""{(string.IsNullOrEmpty(name) ? string.Empty : $"Name: \"{name}\", "), nq}Pozition: (x:{Pozition.x}, y:{Pozition.y})""")]
public sealed class SpacePoint
{
    WeakReference<SpacePoint> parentReference = new(null!);
    List<SpacePoint> childs = new List<SpacePoint>();
    Vector2 ancor1 = new(.5f, .5f), ancor2 = new(.5f, .5f);
    Dictionary<string, (Action<SpacePoint, object> @event, object sender)> events = new();
    Interface? @interface;
    Element? element;
    bool active = true, interactable = true;
    string name = string.Empty;
    public event Action<SpacePoint, ClickEventButton>? OnClick;
    public event Action<SpacePoint>? OnEnter;
    public event Action<SpacePoint>? OnExit;

    float width, height;
    float poz_x, poz_y;

    internal Action<SpacePoint, ClickEventButton>? _onClick => OnClick;
    internal Action<SpacePoint>? _onEnter => OnEnter;
    internal Action<SpacePoint>? _onExit => OnExit;
    private SpacePoint? parent
    {
        get => parentReference.TryGetTarget(out var p) ? p : null;
        set => parentReference.SetTarget(value!);
    }

    public SpacePoint() { }

    [DebuggerNonUserCode]
    public SpacePoint(Interface @interface)
    {
        if (@interface is null) throw new ArgumentNullException(nameof(@interface));
        (poz_x, poz_y) = (0, 0);
        try { @interface.MainElements.AddChild(this); }
        catch (NullReferenceException) { this.@interface = @interface; }
    }

    public SpacePoint(SpacePoint point)
    {
        (poz_x, poz_y) = point.GlobalPozition;
        @interface = null!;
        point.Childs.AddChild(this);
    }

    ~SpacePoint()
    {
        element?.Unbind(this);
    }

    /// <summary>
    /// Nazwa logiczna urzywana w identyfikowaniu tego elementu
    /// </summary>
    public string Name { get => name; set => name = value; }
    
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
            if(value?.Point is not null)
            {
                value.Unbind(value.Point);
                value.Point.element = null;
                value?.set_element(null!);
            }
            element?.Unbind(this);
            element?.set_element(null!);
            value?.set_element(this);
            element = value;
            element?.Bind(this);
        }
    }

    /// <summary>
    /// <see cref="GUI.Interface"/> z którym ten <see cref="SpacePoint"/> jest połączony
    /// </summary>
    public Interface? Interface
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
    public (float x, float y) Pozition
    {
        get => (poz_x, poz_y);
        set
        {
            var old = (poz_x, poz_y);
            if (old == value)
                return;
            (poz_x, poz_y) = value;
            element?.OnPozitionChanget(old, value);
        }
    }
    
    /// <summary>
    /// Pozycja globalna na płutnie
    /// </summary>
    public (float x, float y) GlobalPozition
    {
        get
        {
            int count = 0;
            SpacePoint? point = parent;

            while (point != null)
                (count, point) = (++count, point.parent);

            Span<(Vector2 delt_anch, Vector2 poz, Vector2 size)> hierarchy = stackalloc (Vector2, Vector2, Vector2)[count];

            (count, point) = (--count, parent);
            (Vector2 a1, Vector2 a2) anc = (ancor1, ancor2);

            while(point != null)
            {
                hierarchy[count] = ((anc.a1 + anc.a2) / 2, point.Pozition.ToVector(), point.Size.ToVector());
                anc = (point.ancor1, point.ancor2);
                point = point.parent;
                count--;
            }

            Span<Vector2> simplyf = stackalloc Vector2[hierarchy.Length];
            count = 0;
            for (; count < simplyf.Length; count++)
                simplyf[count] = hierarchy[count].poz - (hierarchy[count].size / 2) + (hierarchy[count].delt_anch * hierarchy[count].size);

            Vector2 sum = new();

            for (count = 0; count < simplyf.Length; count++)
                sum += simplyf[count];

            return (sum + new Vector2(poz_x, poz_y)).ToTumple();
        }
        set
        {
            var old = Parent?.GlobalPozition ?? new();
            Pozition = (value.x - old.x, value.y - old.y);
        }
    }

    /// <summary>
    /// Rozmiary na płutnie
    /// </summary>
    public (float Width, float Height) Size
    {
        get => (width, height);
        set
        {
            var old = Size;
            if (old == value)
                return;

            foreach (var point in childs)
                move_elm(point);

            (width, height) = value;
            element?.OnSizeChanget(old, value);

            void move_elm(SpacePoint point)
            {
                float s_l = -(this.width / 2f) + (point.ancor1.X * this.width) - (point.poz_x - (point.width / 2));
                float s_r = -(this.width / 2f) + (point.ancor2.X * this.width) - (point.poz_x + (point.width / 2));
                float s_d = -(this.height / 2f) + (point.ancor1.Y * this.height) - (point.poz_y - (point.height / 2));
                float s_u = -(this.height / 2f) + (point.ancor2.Y * this.height) - (point.poz_y + (point.height / 2));

                float e_l = -(value.Width / 2f) + (point.ancor1.X * value.Width) - s_l;
                float e_r = -(value.Width / 2f) + (point.ancor2.X * value.Width) - s_r;
                float e_d = -(value.Height / 2f) + (point.ancor1.Y * value.Height) - s_d;
                float e_u = -(value.Height / 2f) + (point.ancor2.Y * value.Height) - s_u;

                float width = -e_l + e_r;
                float height = -e_d + e_u;
                point.Size = (width, height);
            }
        }
    }

    /// <summary>
    /// Czy ten element jest aktywny w fizyce interfejsu
    /// </summary>
    public bool Active { get => active; set => active = value; }

    /// <summary>
    /// Czy ten obiekt i jego podrzędne biorą udzał w fizyce <see cref="OnEnter"/>, <see cref="OnExit"/>, <see cref="OnClick"/>
    /// </summary>
    public bool Interactable { get => interactable; set => interactable = value; }

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

    public void AddEvent(string name, Action<SpacePoint, object> action, object sender)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        if (events.TryGetValue(name, out _))
            throw new ArgumentException($"Event witch name \"{name}\" alredy exists");
        events.Add(name, (action, sender));
    }
    public bool RemoveEvent(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        return events.Remove(name);
    }
    public bool RunEvent(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        if (events.TryGetValue(name, out var e))
        { e.@event(this, e.sender); return true; }
        return false;
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
            (float, float)? startpoz = (point.@interface != this.point.@interface && point.@interface != null) ? point.GlobalPozition : null;
            if (point.parent != null)
                point.parent.Childs.RemoveChild(point);
            point.@interface = this.point.@interface;
            point.parent = this.point;
            this.point.childs.Add(point);
            if(startpoz.HasValue)
            point.GlobalPozition = startpoz.Value;
            i_(point, this.point.@interface);

            //Methods
            void i_(SpacePoint point, Interface? i)
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
            (float, float)? startpoz = (point.@interface != this.point.@interface && point.@interface != null) ? point.GlobalPozition : null;
            if (point.parent != null)
                point.parent.Childs.RemoveChild(point);
            point.@interface = this.point.@interface;
            point.parent = this.point;
            this.point.childs.Insert(index, point);
            if (startpoz.HasValue)
                point.GlobalPozition = startpoz.Value;
            i_(point, this.point.@interface);

            //Methods
            void i_(SpacePoint point, Interface? i)
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

        public SpacePoint? Find(string name, bool recursive = false)
        {
            if (recursive)
                return find(point);
            else
                return point.childs.FirstOrDefault(x => x.name == name);

            SpacePoint? find(SpacePoint source)
            {
                var p = source.childs.FirstOrDefault(x => x.name == name);
                if (p is not null)
                    return p;

                foreach(var ch in source.childs)
                {
                    p = find(ch);
                    if (p is not null) return p;
                }

                return null;
            }
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

public enum ClickEventButton
{
    Unknown,
    Left,
    Right,
    Scroll,
}