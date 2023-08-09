using Create.Linq;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Create.OpenGL.Textures;

/// <summary>
/// Standardowa 2-wymiarowa tekstura w standardzie RGBA
/// </summary>
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

    /// <summary>
    /// Tworzy teksture z obrazu(<see cref="Image"/>)
    /// <para>
    /// Wspierane standardy:
    /// <br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </para>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static Texture2D Create(Image image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.Width, image.Height);
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }

    /// <summary>
    /// Tworzy teksture z obrazu(<see cref="Image{T}"/>)
    /// <para>
    /// Wspierane standardy dla <typeparamref name="T"/>:
    /// <br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </para>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static Texture2D Create<T>(Image<T> image) where T : unmanaged, IPixel<T>
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.Width, image.Height);
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }

    /// <summary>
    /// Tworzy teksture z tablicy kolorów
    /// <para>
    /// Wspierane standardy dla <typeparamref name="T"/>:
    /// <br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </para>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static Texture2D Create<T>(T[,] image) where T : unmanaged, IPixel<T>
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));
        var size = new Vector2i(image.GetLength(0), image.GetLength(1));
        var handle = create_texture(get_gl_bytes_array(image), size);
        return new(handle, size);
    }

    /// <summary>
    /// Zapisuje bufor danych w pamięci karty graficznej i zwraca odnośnik do niego
    /// </summary>
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

    /// <summary>
    /// Konwertuje obraz w jednym ze wsperanych standardów w bufor kolorów dla karty graficznej
    /// <para>
    ///   Wspierane  standardy:<br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </para>
    /// </summary>
    /// <exception cref="NotImplementedException">Kiedy standard obrazu nie jest wspierany</exception>
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

    /// <summary>
    /// Konwertuje tablice kolorów w jednym ze wsperanych standardów w bufor kolorów dla karty graficznej
    /// </summary>
    /// <typeparam name="T">
    /// Wspierane standardy:
    /// <br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </typeparam>
    /// <exception cref="NotImplementedException"></exception>
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
    
    /// <summary>
    /// Podstawa do konwertowania Tablic kolorów w bufor danych dla karty graficznej
    /// </summary>
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

    /// <summary>
    /// <inheritdoc cref="get_gl_bytes_array(Image)"/>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
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

    /// <summary>
    /// <inheritdoc cref="get_gl_bytes_array{T}(T[,])"/>
    /// </summary>
    /// <typeparam name="T">
    /// Wspierane standardy:
    /// <br></br>
    ///   <see cref="Rg32"/>,
    ///   <see cref="Rgb24"/>,
    ///   <see cref="Rgb48"/>,
    ///   <see cref="Rgba32"/>,
    ///   <see cref="Rgba64"/>,
    ///   <see cref="Rgba1010102"/>,
    ///   <see cref="RgbaVector"/>,
    ///   <see cref="Short2"/>,
    ///   <see cref="Short4"/>
    /// </typeparam>
    /// <exception cref="NotImplementedException"></exception>
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
    
    /// <summary>
    /// <inheritdoc cref="get_gl_bytes_array(Vector2i, Func{(int x, int y), Rgba32})"/>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="get_pix"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Zwraca tablice kolorów tej tekstury
    /// <para>Za karzdym razem jest tworzona nowa instancja</para>
    /// </summary>
    public Rgba32[,] GetTexture()
    {
        //return MainTask.Run(() =>
        {
            byte[] bytes = new byte[size.X * size.Y * 4];
            GL.GetTextureImage(handle, 0, PixelFormat.Rgba, PixelType.UnsignedByte, size.X * size.Y * 4, bytes);
            return decode_image_from_buffer(bytes, size, 0);
        }//);
    }

    /// <summary>
    /// Konwertuje bufor danych z karty graficznej i konwertuje go w teblece kolorów obrazu
    /// </summary>
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

    /// <summary>
    /// Wymiary tekstury
    /// </summary>
    public (int Width, int Height) Size => size.ToTumple();
    public bool IsDisposed => disposable;
    public override TextureTarget Target => TextureTarget.Texture2D;
}