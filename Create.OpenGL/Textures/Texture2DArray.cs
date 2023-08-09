using Create.Linq;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Create.OpenGL.Textures;

/// <summary>
/// Atlas 2-wymiarowych tekstur w standardzie RGBA
/// </summary>
public sealed class Texture2DArray : Texture
{
    int handle;
    Vector2i size;
    int layers;
    public static Constructor Create() => new();

    /// <summary>
    /// Konstruktor atlasu
    /// </summary>
    public class Constructor
    {
        List<object> images = new();
        (int w, int h)? size;

        /// <summary>
        /// Dodanie obrazu do atlasu
        /// <para>
        /// Wspierane standardy:
        /// <br></br>
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
        /// <exception cref="NotImplementedException"></exception>
        public Constructor Add(Image image)
        {
            switch (image)
            {
                case Image<Rgb24>:
                case Image<Rgb48>:
                case Image<Rgba1010102>:
                case Image<Rgba32>:
                case Image<Rgba64>:
                case Image<RgbaVector>:
                case Image<Short2>:
                case Image<Short4>:
                    test_size((image.Width, image.Height));
                    images.Add(image);
                    break;
                default:
                    throw new NotImplementedException("That type is not supported");
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc cref="Add(Image)"/>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public Constructor Add<T>(Image<T> image) where T : unmanaged, IPixel<T>
        {
            switch (image)
            {
                case Image<Rgb24>:
                case Image<Rgb48>:
                case Image<Rgba32>:
                case Image<Rgba64>:
                case Image<Rgba1010102>:
                case Image<RgbaVector>:
                case Image<Short2>:
                case Image<Short4>:
                    test_size((image.Width, image.Height));
                    images.Add(image);
                    break;
                default:
                    throw new NotImplementedException("That type is not supported");
            }

            return this;
        }

        /// <summary>
        /// <inheritdoc cref="Add(Image)"/>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public Constructor Add<T>(T[,] image) where T : unmanaged, IPixel<T>
        {
            switch (image)
            {
                case Rgb24[,]:
                case Rgb48[,]:
                case Rgba1010102[,]:
                case Rgba32[,]:
                case Rgba64[,]:
                case RgbaVector[,]:
                case Short2[,]:
                case Short4[,]:
                    test_size(image.GetSize());
                    images.Add(image);
                    break;
                default:
                    throw new NotImplementedException("That type is not supported");
            }

            return this;
        }
        
        /// <summary>
        /// Test czy wymiary wrzystkich obrazów są takie same
        /// </summary>
        /// <param name="size"></param>
        /// <exception cref="Exception"></exception>
        void test_size((int w, int h) size)
        {
            if (!this.size.HasValue)
                this.size = size;
            if (this.size != size)
                throw new Exception("Sizes of images are not match");
        }

        /// <summary>
        /// Składa wrzystkie obrazy w jeden atlas
        /// </summary>
        public Texture2DArray Finish()
        {
            if (images.Count == 0)
                throw new Exception("You need to specyfy more images");

            Texture2DArray t2a = new();

            var bytes_array = generate_byte_buffer();

            t2a.size = size!.Value.ToVector();
            t2a.layers = images.Count;

            //MainTask.Run(() =>
            {
                int handle = GL.GenTexture();
                t2a.handle = handle;
                GL.BindTexture(TextureTarget.Texture2DArray, handle);

                GL.TexStorage3D(TextureTarget3d.Texture2DArray, 1, SizedInternalFormat.Rgba8, size!.Value.w, size.Value.h, images.Count);
                GL.TexSubImage3D(TextureTarget.Texture2DArray, 0, 0, 0, 0, size!.Value.w, size.Value.h, images.Count, PixelFormat.Rgba, PixelType.UnsignedByte, bytes_array);

                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToBorder);

                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.BindTexture(TextureTarget.Texture2DArray, 0);
            }//);

            return t2a;

            //Methods
            byte[] generate_byte_buffer()
            {
                var converted_images = images.ConvertAll(i =>
                i switch
                {
                    Image im => Texture2D.get_gl_bytes_array(im),
                    Rgb24[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Rgb48[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Rgba32[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Rgba64[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Rgba1010102[,] arr => Texture2D.get_gl_bytes_array(arr),
                    RgbaVector[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Short2[,] arr => Texture2D.get_gl_bytes_array(arr),
                    Short4[,] arr => Texture2D.get_gl_bytes_array(arr),

                    _ => new byte[0]
                });
                int size = 0, I = 0;
                {
                    foreach (var array in converted_images)
                        size += array.Length;
                }
                byte[] data = new byte[size];
                foreach(var array in converted_images)
                {
                    for(int i = 0; i < array.Length; i++)
                        data[I + i] = array[i];
                    I += array.Length;
                }
                return data;
            }
        }
    }

    /// <summary>
    /// Zwraca Tablice kolorów jednego obrazu z atlasu
    /// </summary>
    public Rgba32[,] GerTexture(int index)
    {
        //return MainTask.Run(() =>
        {
            byte[] bytes = new byte[size.X * size.Y * 4];
            GL.GetTextureSubImage(handle, 0, 0, 0, index, size.X, size.Y, 1, PixelFormat.Rgba, PixelType.UnsignedByte, bytes.Length, bytes);
            return Texture2D.decode_image_from_buffer(bytes, size, 0);
        }//);
    }

    public override int Handle => handle;
    public override TextureTarget Target => TextureTarget.Texture2DArray;
}
