using Create.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Create.OpenGL.Textures;

public sealed class Texture2D : Texture, IDisposable
{
    readonly int handle;
    readonly Vector2i size;
    bool disposable;

    Texture2D(int handle, Vector2i size)
    {
        this.handle = handle;
        this.size = size;
    }

    public static Texture2D Create(Image image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.Width, image.Height);
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }
    public static Texture2D Create<T>(Image<T> image) where T : unmanaged, IPixel<T>
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.Width, image.Height);
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }
    public static Texture2D Create<T>(T[,] image) where T : unmanaged, IPixel<T>
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.GetLength(0), image.GetLength(1));
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }

    static int create_texture(byte[] bytes, Vector2i size)
    {
        int handle = GL.GenTexture();
        //MainTask.Run(() =>
        {
            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, size.X, size.Y,
                0, PixelFormat.Rgba, PixelType.UnsignedByte, bytes);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        }//);
        return handle;
    }

    internal static byte[] get_gl_bytes_array(Image image) => image switch
    {
        Image<Rg32> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rg32>)image)[t.x,t.y]
            .Cast(c => new Rgba32(c.PackedValue))),
        Image<Rgb24> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rgb24>)image)[t.x, t.y]
            .Cast(c => new Rgba32(c.R, c.G, c.B, byte.MaxValue))),
        Image<Rgb48> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rgb48>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                byte.MaxValue))),
        Image<Rgba32> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba32>)image)[t.x, t.y]),
        Image<Rgba64> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba64>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.A / ushort.MaxValue * byte.MaxValue)))),
        Image<Rgba1010102> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba1010102>)image)[t.x, t.y]
            .Cast(c => new Rgba32(c.PackedValue))),
        Image<RgbaVector> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<RgbaVector>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)(byte.MaxValue * c.R),
                (byte)(byte.MaxValue * c.G),
                (byte)(byte.MaxValue * c.B),
                (byte)(byte.MaxValue * c.A)))),
        Image<Short2> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Short2>)image)[t.x, t.y]
            .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),
        Image<Short4> => get_gl_bytes_array(new(image.Width, image.Height), t => ((Image<Short4>)image)[t.x, t.y]
            .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),

        _ => throw new NotImplementedException($"Type \"{image.GetType()}\" is not supported")
    };
    internal static byte[] get_gl_bytes_array<T>(T[,] array) where T : unmanaged, IPixel<T>
    {
        Vector2i size = new(array.GetLength(0), array.GetLength(1));
        return array switch
        {
            Rg32[,] => get_gl_bytes_array(size, t => ((Rg32[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.PackedValue))),
            Rgb24[,] => get_gl_bytes_array(size, t => ((Rgb24[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.R, c.G, c.B, byte.MaxValue))),
            Rgb48[,] => get_gl_bytes_array(size, t => ((Rgb48[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                    byte.MaxValue))),
            Rgba32[,] => get_gl_bytes_array(size, t => ((Rgba32[,])(Array)array)[t.x, t.y]),
            Rgba64[,] => get_gl_bytes_array(size, t => ((Rgba64[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.A / ushort.MaxValue * byte.MaxValue)))),
            Rgba1010102[,] => get_gl_bytes_array(size, t => ((Rgba1010102[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.PackedValue))),
            RgbaVector[,] => get_gl_bytes_array(size, t => ((RgbaVector[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)(byte.MaxValue * c.R),
                    (byte)(byte.MaxValue * c.G),
                    (byte)(byte.MaxValue * c.B),
                    (byte)(byte.MaxValue * c.A)))),
            Short2[,] => get_gl_bytes_array(size, t => ((Short2[,])(Array)array)[t.x, t.y]
                .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),
            Short4[,] => get_gl_bytes_array(size, t => ((Short4[,])(Array)array)[t.x, t.y]
                .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),

            _ => throw new NotImplementedException($"Type \"{typeof(T[,])}\" is not supported")
        };
    }
    static byte[] get_gl_bytes_array(Vector2i size, Func<(int x, int y), Rgba32> get_pix)
    {
        byte[] bytes = new byte[size.X * size.Y * 4];
        {
            int i = 0;
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    var pix = get_pix((x, size.Y - y - 1));
                    bytes[(i * 4) + 0] = pix.R;
                    bytes[(i * 4) + 1] = pix.G;
                    bytes[(i * 4) + 2] = pix.B;
                    bytes[(i * 4) + 3] = pix.A;
                    i++;
                }
        }
        return bytes;
    }
    internal static byte[] get_bytes_array(Image image) => image switch
    {
        Image<Rg32> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rg32>)image)[t.x, t.y]
            .Cast(c => new Rgba32(c.PackedValue))),
        Image<Rgb24> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rgb24>)image)[t.x, t.y]
            .Cast(c => new Rgba32(c.R, c.G, c.B, byte.MaxValue))),
        Image<Rgb48> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rgb48>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                byte.MaxValue))),
        Image<Rgba32> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba32>)image)[t.x, t.y]),
        Image<Rgba64> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba64>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                (byte)((float)c.A / ushort.MaxValue * byte.MaxValue)))),
        Image<Rgba1010102> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Rgba1010102>)image)[t.x, t.y]
            .Cast(c => new Rgba32(c.PackedValue))),
        Image<RgbaVector> => get_bytes_array(new(image.Width, image.Height), t => ((Image<RgbaVector>)image)[t.x, t.y]
            .Cast(c => new Rgba32(
                (byte)(byte.MaxValue * c.R),
                (byte)(byte.MaxValue * c.G),
                (byte)(byte.MaxValue * c.B),
                (byte)(byte.MaxValue * c.A)))),
        Image<Short2> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Short2>)image)[t.x, t.y]
            .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),
        Image<Short4> => get_bytes_array(new(image.Width, image.Height), t => ((Image<Short4>)image)[t.x, t.y]
            .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),

        _ => throw new NotImplementedException($"Type \"{image.GetType()}\" is not supported")
    };
    internal static byte[] get_bytes_array<T>(T[,] array) where T : unmanaged, IPixel<T>
    {
        Vector2i size = new(array.GetLength(0), array.GetLength(1));
        return array switch
        {
            Rg32[,] => get_bytes_array(size, t => ((Rg32[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.PackedValue))),
            Rgb24[,] => get_bytes_array(size, t => ((Rgb24[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.R, c.G, c.B, byte.MaxValue))),
            Rgb48[,] => get_bytes_array(size, t => ((Rgb48[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                    byte.MaxValue))),
            Rgba32[,] => get_bytes_array(size, t => ((Rgba32[,])(Array)array)[t.x, t.y]),
            Rgba64[,] => get_bytes_array(size, t => ((Rgba64[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)((float)c.R / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.G / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.B / ushort.MaxValue * byte.MaxValue),
                    (byte)((float)c.A / ushort.MaxValue * byte.MaxValue)))),
            Rgba1010102[,] => get_bytes_array(size, t => ((Rgba1010102[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(c.PackedValue))),
            RgbaVector[,] => get_bytes_array(size, t => ((RgbaVector[,])(Array)array)[t.x, t.y]
                .Cast(c => new Rgba32(
                    (byte)(byte.MaxValue * c.R),
                    (byte)(byte.MaxValue * c.G),
                    (byte)(byte.MaxValue * c.B),
                    (byte)(byte.MaxValue * c.A)))),
            Short2[,] => get_bytes_array(size, t => ((Short2[,])(Array)array)[t.x, t.y]
                .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),
            Short4[,] => get_bytes_array(size, t => ((Short4[,])(Array)array)[t.x, t.y]
                .Cast(s => { Rgba32 c = new(); s.ToRgba32(ref c); return c; })),

            _ => throw new NotImplementedException($"Type \"{typeof(T[,])}\" is not supported")
        };
    }
    static byte[] get_bytes_array(Vector2i size, Func<(int x, int y), Rgba32> get_pix)
    {
        byte[] bytes = new byte[size.X * size.Y * 4];
        {
            int i = 0;
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    var pix = get_pix((x, y));
                    bytes[(i * 4) + 0] = pix.R;
                    bytes[(i * 4) + 1] = pix.G;
                    bytes[(i * 4) + 2] = pix.B;
                    bytes[(i * 4) + 3] = pix.A;
                    i++;
                }
        }
        return bytes;
    }



    public Rgba32[,] GetTexture()
    {
        //return MainTask.Run(() =>
        {
            byte[] bytes = new byte[size.X * size.Y * 4];
            GL.GetTextureImage(handle, 0, PixelFormat.Rgba, PixelType.UnsignedByte, size.X * size.Y * 4, bytes);
            return decode_image_from_buffer(bytes, size, 0);
        }//);
    }

    internal static Rgba32[,] decode_image_from_buffer(byte[] bytes, Vector2i size, int offset)
    {
        var image = new Rgba32[size.X, size.Y];
        for (int x = 0; x < size.X; x++)
            for (int y = 0; y < size.Y; y++)
            {
                int _offset = (((y * size.X) + x) * 4) + offset;
                Rgba32 pixel = new(bytes[_offset + 0], bytes[_offset + 1], bytes[_offset + 2], bytes[_offset + 3]);
                image[x, size.Y - y - 1] = pixel;
            }
        return image;
    }

    public void Dispose()
    {
        if (disposable) return;
        disposable = true;

        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.DeleteTexture(handle);
        GC.SuppressFinalize(this);
    }

    public override int Handle => handle;
    public (int Width, int Height) Size => size.ToTumple();
    public bool IsDisposed => disposable;
    public override TextureTarget Target => TextureTarget.Texture2D;
}