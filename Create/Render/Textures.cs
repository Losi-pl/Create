using Create.OpenGL.Textures;
using Create.Virtuals;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Create.Render;

public static class Textures
{
    static Dictionary<string, BlockTextureHandle> block_atlas_handles = new();
    static VirtualDictionaty<string, BlockTextureHandle> handle_attlas = VirtualDictionaty.Create(block_atlas_handles).GetMethod(g =>
    {
        if(block_atlas_handles.TryGetValue(g, out var wyn))
            return wyn;
        return BlockTextureHandle.None;
    }).Finsh();
    static List<Image> texture = new();
    static Texture2DArray? atlas;

    public static VirtualDictionaty<string, BlockTextureHandle> Handles => handle_attlas;
    public static BlockTextureHandle NoneHandle => BlockTextureHandle.None;
    public static bool IsAttlasComplited => atlas != null;
    public static Texture2DArray Attlas => atlas ?? throw new Exception("Attlas not complited");

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

    internal static void finish_attlas()
    {
        {
            var cons = Texture2DArray.Create();
            var null_tex = null_texture();
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
    static Rgba32[,] null_texture()
    {
        Rgba32[,] tex = new Rgba32[16, 16];
        Rgba32 p = new(172, 29, 129, 255);
        Rgba32 b = new(0,0,0,225);

        print(0, 0, 8, 8, b);
        print(8, 8, 8, 8, b);
        print(0, 8, 8, 8, p);
        print(8, 0, 8, 8, p);

        return tex;
        //Methods
        void print(int start_x, int start_y,int len_x, int len_y, Rgba32 color)
        {
            for (int x = 0; x < len_x; x++)
                for (int y = 0; y < len_y; y++)
                    tex[x + start_x, y + start_y] = color;
        }
    }
}
