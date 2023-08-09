using Create.Linq;
using OpenTK.Mathematics;

namespace Create.OpenGL;

partial class RenderLayer
{
    public static Constructor Create() => new();
    
    /// <summary>
    /// Konstruktor do <see cref="RenderLayer"/>
    /// </summary>
    public sealed class Constructor
    {
        #region variables
        Vector2i buffer_size = Engine.Size;
        bool draw_only = false;
        (Shader shader, Mesh model, object? sender, Action<RenderLayer, object?> action)? custom_model;
        Camera? camera;
        BufferChanel[] chanels = new BufferChanel[]
        {
            new() {
                buffer_chanel = FramebufferAttachment.ColorAttachment0,
                in_format = PixelInternalFormat.Rgba,
                out_format = PixelFormat.Rgba,
                type = PixelType.UnsignedByte,
                filter = TextureMagFilter.Nearest
            }
        };
        #endregion

        #region SetSize
        public Constructor SetSize(int width, int height) => SetSize(new(width, height));
        public Constructor SetSize(Vector2i size)
        {
            buffer_size = size;
            return this;
        }
        #endregion

        /// <summary>
        /// Ustawia instancje kamery urzywanej w tym <see cref="RenderLayer"/>
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public Constructor Camera(Camera camera)
        {
            this.camera = camera;
            return this;
        }

        #region CustomShader
        /// <summary>
        /// Ustaw jekiego <see cref="Mesh"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="shader">Shader do użycia</param>
        /// <param name="uniforms">Kanały z <see cref="RenderLayer"/> i gdzie mają być podpięte</param>
        /// <returns></returns>
        public Constructor CustomShader(Shader shader, (FramebufferAttachment chanel, string uniform)[] uniforms) =>
            CustomShader(shader, calculate_texture_idis(this, shader, uniforms), custom_chanels_apply!);

        /// <summary>
        /// Ustaw jekiego <see cref="Mesh"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="custom_model">Model do użycia</param>
        /// <param name="uniforms">Kanały z <see cref="RenderLayer"/> i gdzie mają być podpięte</param>
        /// <returns></returns>
        public Constructor CustomShader(Mesh custom_model, (FramebufferAttachment chanel, string uniform)[] uniforms) =>
            CustomShader(custom_model, calculate_texture_idis(this, custom_model.Shader, uniforms), custom_chanels_apply!);

        /// <summary>
        /// Podłączenie kanałów do zmiennych na tekstury do <see cref="Shader"/>a
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="o"></param>
        static void custom_chanels_apply(RenderLayer layer, object o)
        {
            var binds = ((int shader, int render)[])o;
            foreach (var b in binds)
                layer.custom_model!.Value.shader.set_texture(b.shader, layer.textures![b.render].texture);
        }
        
        /// <summary>
        /// Testowanie czy <see cref="Shader"/> ma statyczne parametry do połączenia kanałów z <see cref="RenderLayer"/>
        /// </summary>
        /// <param name="cons">Konstruktor <see cref="RenderLayer"/>a</param>
        /// <param name="shader">Shader do urzycia</param>
        /// <param name="uniforms">Kanały z <see cref="RenderLayer"/> i nazwy zmiennych z którymi mają być połączone</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        static (int shader, int render)[] calculate_texture_idis(Constructor cons, Shader shader, (FramebufferAttachment chanel, string uniform)[] uniforms)
        {
            if (cons.chanels == null)
                throw new Exception("First define the channel before binding them");
            foreach (var uniform in uniforms)
                cons.chanels.Find(c => c.buffer_chanel == uniform.chanel, new Exception($"The {uniform.chanel} channel is undefined"));
            foreach (var uniform in uniforms)
                test_uniform(shader, uniform.uniform);

            return uniforms.Convert(uniforms => (
                shader.Uniforms.Find(u => u.Name == uniforms.uniform, null).TextureNumer!.Value,
                cons.chanels.FindAndWhere(c => c.buffer_chanel == uniforms.chanel)!.Value.index));

            void test_uniform(Shader shader, string name)
            {
                var unif = shader.Uniforms.Find(u => u.Name == name, new Exception($"Uniform {name} don't exist"));
                if (unif.Type != ActiveUniformType.Sampler2D && unif.Type != ActiveUniformType.UnsignedIntImage2D)
                    throw new Exception($"Uniform {name} is not Sampler2D");
            }
        }
        
        /// <summary>
        /// Metoda urzywana przed renderowaniemmodelu gdy nie jest do niej przekazywany rzaden obiekt 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="o"></param>
        static void no_obiect(RenderLayer layer, object o) => ((Action<RenderLayer>)o)(layer);
        #endregion

        #region CustomShader
        /// <summary>
        /// Ustaw jekiego <see cref="Shader"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="customShader">Jaki Shader ma dyć urzyty</param>
        /// <param name="sender">Dodatkowy obiekt który ma być przekazany do <paramref name="action"/></param>
        /// <param name="action">Wywoływany przed wyrenderowaniem tego obiektu na innych płutnach</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Constructor CustomShader(Shader customShader, object? sender, Action<RenderLayer, object?> action)
        {
            if (customShader == null)
                throw new ArgumentNullException(nameof(customShader));
            var attribut = get_info(customShader);
            Mesh mesh = Mesh.Create(customShader)
                .SetVertex(attribut.Name, new Vector2[] { new(0, 1), new(1, 1), new(0, 0), new(1, 0) })
                .SetTrangles(new[] { 0, 1, 2, 1, 2, 3 })
                .Finish();
            CustomShader(mesh, sender, action);
            return this;
            //Methods
            Shader.AttributInfo get_info(Shader shader)
            {
                if (shader.Attributes.Count != 1)
                    throw new ArgumentOutOfRangeException("Shader must have only one attribute");
                var typ = shader.Attributes[0].GLType;
                if (typ == ActiveAttribType.FloatVec4) { }
                else if (typ == ActiveAttribType.DoubleVec2) { }
                else if (typ == ActiveAttribType.IntVec2) { }
                else if (typ == ActiveAttribType.UnsignedIntVec2) { }
                else
                    throw new ArgumentException("Attribute must be Vector2 of any type");
                return shader.Attributes[0];
            }
        }

        /// <summary>
        /// Ustaw jekiego <see cref="Mesh"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="custom_model">Wybrany model</param>
        /// <param name="sender">Dodatkowy obiekt który ma być przekazany do <paramref name="action"/></param>
        /// <param name="action">Wywoływany przed wyrenderowaniem tego obiektu na innych płutnach</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor CustomShader(Mesh custom_model, object? sender, Action<RenderLayer, object?> action)
        {
            if (custom_model == null)
                throw new ArgumentNullException(nameof(custom_model));
            this.custom_model = (custom_model.Shader, custom_model, sender, action);
            return this;
        }

        /// <summary>
        /// Ustaw jekiego <see cref="Shader"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="customShader">Jaki Shader ma dyć urzyty</param>
        /// <param name="action">Wywoływany przed wyrenderowaniem tego obiektu na innych płutnach</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Constructor CustomShader(Shader customShader, Action<RenderLayer> action) => CustomShader(customShader, action ?? throw new ArgumentNullException(nameof(action)), no_obiect!);

        /// <summary>
        /// Ustaw jekiego <see cref="Mesh"/>a ma urzyć <see cref="RenderLayer"/> podczas automatycznego rysowania na innych płutnach rysowania
        /// </summary>
        /// <param name="custom_model">Wybrany model</param>
        /// <param name="action">Wywoływany przed wyrenderowaniem tego obiektu na innych płutnach</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Constructor CustomShader(Mesh custom_model, Action<RenderLayer> action) => CustomShader(custom_model, action ?? throw new ArgumentNullException(nameof(action)), no_obiect!);
        #endregion

        #region DrawOnly
        /// <summary>
        /// Ustaw <see cref="RenderLayer"/> na niemożliwy do ręcznego odczytania
        /// </summary>
        public Constructor DrawOnly() => DrawOnly(true);

        /// <summary>
        /// Ustaw czy <see cref="RenderLayer"/> jest możliwy do ręcznego odczytania
        /// </summary>
        public Constructor DrawOnly(bool only)
        {
            draw_only = only;
            return this;
        }
        #endregion

        /// <summary>
        /// Zakończ budowanie <see cref="RenderLayer"/>
        /// </summary>
        public RenderLayer Finisch()
        {
            RenderLayer render_layer = new();
            creation_data cd = new();
            {
                cd.width = buffer_size.X;
                cd.height = buffer_size.Y;
                cd.draw_only = draw_only;
                cd.chanels = chanels;
            }
            render_layer.buffer_creation_data = cd;
            render_layer.camera = camera;
            render_layer.custom_model = custom_model;
            render_layer.create_buffers();
            return render_layer;
        }

        /// <summary>
        /// Parametry pojedyńczego kanału w <see cref="RenderLayer"/>
        /// </summary>
        public struct BufferChanel
        {
            public FramebufferAttachment buffer_chanel;
            public PixelInternalFormat in_format;
            public PixelFormat out_format;
            public PixelType type;
            public TextureMagFilter filter;
        }
    }
}