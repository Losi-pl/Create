using Create.OpenGL.Textures;
using Create.Render;
using Create.Virtuals;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Create;

partial class Assets
{
    /// <summary>
    /// Przechowuje przekonwertowane informacje o teksturach bloków
    /// </summary>
    public static class BlockAtlas
    {
        /// <summary>
        /// Odwołania do tekstur zapisanych w atlasie <see cref="atlas"/>
        /// </summary>
        static Dictionary<string, BlockTextureHandle> block_atlas_handles = new();
        
        /// <summary>
        /// Wirtualny strukt odwołująca się do <see cref="block_atlas_handles"/> i zwracająca <see cref="BlockTextureHandle.None"/> jeżeli tekstura nie może być zlokalizowana
        /// </summary>
        static VirtualDictionaty<string, BlockTextureHandle> handle_attlas = VirtualDictionaty.Create(block_atlas_handles).GetMethod(g =>
        {
            if (block_atlas_handles.TryGetValue(g, out var wyn))
                return wyn;
            return BlockTextureHandle.None;
        }).Finsh();

        /// <summary>
        /// Lista tekstór przed wprowadzeniem ich do <see cref="atlas"/>
        /// </summary>
        static List<Image> texture = new();

        /// <summary>
        /// Attlas...
        /// </summary>
        static Texture2DArray? atlas;

        /// <summary>
        /// <inheritdoc cref="handle_attlas"/>
        /// </summary>
        public static VirtualDictionaty<string, BlockTextureHandle> Handles => handle_attlas;

        /// <summary>
        /// Odwołanie do braku tekstury w <see cref="atlas"/>
        /// </summary>
        public static BlockTextureHandle NoneHandle => BlockTextureHandle.None;

        /// <summary>
        /// Sprawdza czy <see cref="Attlas"/> został wygenerowany
        /// </summary>
        public static bool IsAttlasComplited => atlas != null;

        /// <summary>
        /// <inheritdoc cref="atlas"/>
        /// </summary>
        public static Texture2DArray Attlas => atlas ?? throw new NullReferenceException("Attlas not complited");

        /// <summary>
        /// Dodanie teksture do <see cref="texture"/> i znacznik odwołania do <see cref="block_atlas_handles"/> albo jeżeli znacznik już istnieje zastępuje ją w <see cref="texture"/>
        /// </summary>
        /// <param name="im">Tekstura <para>Tekstury nie o wymiarach 16x16 są automatycznie odrzucane</para></param>
        /// <param name="path">Ścieżka do tekstury np. <c>create:stone</c></param>
        internal static void set_texture(Image im, string path)
        {
            if (im.Width != 16 || im.Height != 16)
                return;
            if (block_atlas_handles.TryGetValue(path, out var ex_handle))
                texture[ex_handle.Handle] = im;
            else
            {
                BlockTextureHandle handle = new(texture.Count + 1);
                texture.Add(im);
                block_atlas_handles.Add(path, handle);
            }
        }

        /// <summary>
        /// Generuje gotowy atlas na podstawie <see cref="texture"/> i gdy atlas jest gotowy <see cref="texture"/> jest czyszczony
        /// </summary>
        internal static void FinishTextureAttlas()
        {
            {
                var cons = Texture2DArray.Create();
                var null_tex = GenerateNullTexture();
                cons.Add(null_tex);
                int at = 0;
                foreach (var texture in texture)
                {
                    at++;
                    if (texture != null)
                        cons.Add(texture);
                    else
                        cons.Add(null_tex);
                }
                atlas = OpenGL.MainTask.Run(cons.Finish);
                texture = null!;
            }
            GC.Collect();
        }

        /// <summary>
        /// Generuje teksture braku tekstury
        /// </summary>
        /// <returns>Tablica kolorów braku tekstury</returns>
        static Rgba32[,] GenerateNullTexture()
        {
            Rgba32[,] tex = new Rgba32[16, 16];
            Rgba32 p = new(172, 29, 129, 255);
            Rgba32 b = new(0, 0, 0, 225);

            FillPixels(0, 0, 8, 8, b);
            FillPixels(8, 8, 8, 8, b);
            FillPixels(0, 8, 8, 8, p);
            FillPixels(8, 0, 8, 8, p);

            return tex;
            //Methods
            void FillPixels(int start_x, int start_y, int len_x, int len_y, Rgba32 color)
            {
                for (int x = 0; x < len_x; x++)
                    for (int y = 0; y < len_y; y++)
                        tex[x + start_x, y + start_y] = color;
            }
        }
    }
}
