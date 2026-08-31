using System.Collections.Frozen;
using System.Drawing;
using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using StbImageSharp;
using ImageData = CodeOfChaos.Unions.Union<
    (System.IO.Stream stream, bool close), 
    (int width, int height, System.Memory<byte>), 
    (int width, int height, System.Memory<System.Drawing.Color>), 
    Silk.NET.Core.RawImage, 
    CodeOfChaos.Unions.None>;
using SetImageData = CodeOfChaos.Unions.Union<
    (System.IO.Stream stream, bool close), 
    (int width, int height, System.Memory<byte>), 
    (int width, int height, System.Memory<System.Drawing.Color>), 
    Silk.NET.Core.RawImage>;
// ReSharper disable UnusedMember.Global

namespace Create.Graphics;

partial class Texture2DAtlas
{
    public static Constructor Create() => new();
    
    public class Constructor
    {
        private readonly List<ImageData> _images = [];
        /// If set to None, exception throwing enabled. If set to anything else replace all faulty images with this
        /// <remarks>If parsing of this image fails as well an exception will be thrown regardless</remarks>
        private ImageData _defaultImage = new None();

        private Vector2D<uint>? _imageSize;
        
        public SetImageData? this[int index]
        {
            get
            {
                if (index >= _images.Count)
                    return null;
                var image = _images[index];
                return image switch
                {
                    { IsT0: true, AsT0: var value } => value,
                    { IsT1: true, AsT1: var value } => value,
                    { IsT2: true, AsT2: var value } => value,
                    { IsT3: true, AsT3: var value } => value,
                    _ => null
                };
            }
            set
            {
                if (index < _images.Count)
                {
                    _images[index] = value switch
                    {
                        null => new None(),
                        { IsT0: true, AsT0: var data } => data,
                        { IsT1: true, AsT1: var data } => data,
                        { IsT2: true, AsT2: var data } => data,
                        { IsT3: true, AsT3: var data } => data,
                        _ => new None()
                    };
                    return;
                }

                for (var i = _images.Count; i <= index; i++)
                    _images.Add(i == index ? value switch
                    {
                        null => new None(),
                        { IsT0: true, AsT0: var data } => data,
                        { IsT1: true, AsT1: var data } => data,
                        { IsT2: true, AsT2: var data } => data,
                        { IsT3: true, AsT3: var data } => data,
                        _ => new None()
                    } : new None());
            }
        }

        /// <summary>
        /// Used suppress exception throwing during the process of parsing image data
        /// </summary>
        /// <param name="defaultImage">Will replace all images that failed to be properly parsed, exception will be thrown regardless if parsing of this image fails</param>
        public Constructor SuppressExceptions(SetImageData defaultImage)
        {
            _defaultImage = defaultImage switch
            {
                { IsT0: true, AsT0: var value } => value,
                { IsT1: true, AsT1: var value } => value,
                { IsT2: true, AsT2: var value } => value,
                { IsT3: true, AsT3: var value } => value,
                _ => throw new ArgumentException("Invalid default Texture")
            };
            return this;
        }

        /// Specify size of the images the Atlas is supposed to expect, if none is set the size of the first image will be treated as expected
        public Constructor SpecifyImageSize(uint with, uint height)
        {
            _imageSize = new(with, height);
            return this;
        }
        
        public Texture2DAtlas Finish()
        {
            for (var i = _images.Count - 1; i >= 0; i++)
            { // Trims the end of the list of all images set to None
                if(!_images[i].IsT4)
                    break;
                _images.RemoveAt(i);
            }

            if (_images.Count == 0)
                throw new InvalidOperationException("You can't create an empty atlas");
            
            var imageSize = _imageSize;
            RawImage? @default = null;
            if (_defaultImage.IsT4)
            {
                if (_images.Any(static img => img.IsT4))
                    throw new InvalidOperationException("Not all textures in the atlas are set");
            }
            else
            {
                var img = ParseImage(_defaultImage);
                @default = img switch
                {
                    { IsResult: true } => img.AsResult,
                    { IsError: true } => throw new InvalidDataException("Image parsing failed", img.AsError),
                    _ => throw new InvalidDataException("Image parsing failed")
                };
                if(imageSize.HasValue)
                    if (@default.Value.Width != imageSize.Value.X || @default.Value.Height != imageSize.Value.Y)
                        throw new InvalidDataException($"The default image is wrong size. Expected: {
                            imageSize.Value.X} x {imageSize.Value.Y} Current: {@default.Value.Width} x {@default.Value.Height}");
            }
            
                
            var gl = Window.GL;
            var image = gl.CreateTexture(TextureTarget.Texture2DArray);
            
            List<(int index, Exception exception)> problems = [];
            
            gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            for (var i = 0; i < _images.Count; i++)
            {
                RawImage img;
                {
                    var rezImg = ParseImage(_images[i]);
                    if (!rezImg.IsResult)
                        FailedParsing(rezImg.IsError ? rezImg.AsError : null);
                    else
                        img = rezImg.AsResult;
                }
                imageSize ??= new((uint)img.Width, (uint)img.Height);
                if (img.Width != imageSize.Value.X || img.Height != imageSize.Value.Y)
                    FailedParsing(new InvalidDataException($"The image is wrong size. Expected: {
                        imageSize.Value.X} x {imageSize.Value.Y} Current: {img.Width} x {img.Height}"));
                
                if (i == 0)
                    gl.TextureStorage3D(image, 1, SizedInternalFormat.Rgba8, imageSize.Value.X, imageSize.Value.Y, (uint)_images.Count);

                gl.TextureSubImage3D(image, 0, 0, 0, i, imageSize.Value.X, imageSize.Value.Y, 1u, PixelFormat.Rgba, PixelType.UnsignedByte, ref img.Pixels.Span[0]);
                
                void FailedParsing(Exception? reason)
                {
                    if (@default.HasValue)
                    {
                        img = @default.Value;
                        if(reason is not null)
                            problems.Add((i, reason));
                        return;
                    }
                    
                    List<Exception> extraErrors = [];
                    for (; i < _images.Count; i++)
                        if (_images[i] is { IsT0: true, AsT0: { close: true, stream: var stream } })
                            try { stream.Dispose(); }
                            catch (Exception e) { extraErrors.Add(e); }

                    if (extraErrors.Count == 0)
                        throw new InvalidDataException("Image Parsing failed", reason);
                    throw new AggregateException(reason is null ? extraErrors : Enumerable.Single(reason).Append(extraErrors));
                }
            }
            
            {// TODO: Engineer some options for this
                gl.TextureParameter(image, TextureParameterName.TextureMinFilter, (int)GLEnum.Nearest);
                gl.TextureParameter(image, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);
                
                gl.TextureParameter(image, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge); // Horizontal
                gl.TextureParameter(image, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge); // Vertical
                gl.TextureParameter(image, TextureParameterName.TextureWrapR, (int)GLEnum.Repeat);      // Layers
            }
            
            gl.GenerateTextureMipmap(image);
            
            return new(image, new(imageSize!.Value.X, imageSize.Value.Y, (uint)_images.Count),
                problems.Count == 0 ? null : problems.ToFrozenDictionary(p => (uint)p.index, p => p.exception));
        }

        private static ExcResult<RawImage> ParseImage(ImageData image) =>
            image switch
            {
                { IsT0: true, AsT0: var value } => LoadImage(value.stream, value.close),
                { IsT1: true, AsT1: var value } => ValidateFormat(value),
                { IsT2: true, AsT2: var value } => LoadImage(value),
                { IsT3: true, AsT3: var value } => ValidateFormat(value),
                _ => new ArgumentNullException(null, "No data for image provided")
            };
        
        private static ExcResult<RawImage> LoadImage(Stream stream, bool shouldClose)
        {
            Exception? ex;
            Exception? ex2 = null;
            try
            {
                var img = ImageResult.FromStream(stream);
                return new RawImage(img.Width, img.Height, img.Data);
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                try
                {
                    if (shouldClose)
                        stream.Dispose();
                }
                catch (Exception e2)
                {
                    ex2 = e2;
                }
            }

            if (ex2 is not null)
                return new AggregateException("Problems encountered during both image parsing and stream disposal", ex, ex2);
            return ex;
        }

        private static ExcResult<RawImage> LoadImage((int width, int height, Memory<Color> data) _)
        {
            try
            {
                if (_.width * _.height != _.data.Length)
                    return new InvalidDataException($"Amount of data for Image {
                        _.width} x {_.height} is not correct. Expected: {_.width * _.height} Current: {_.data.Length}");
                var bytes = new byte[_.width * _.height * 4];
                var span = _.data.Span;
                for (var i = 0; i < span.Length; i++)
                {
                    var off = i * 4;
                    ref var color = ref span[i];
                    bytes[off + 0] = color.R;
                    bytes[off + 1] = color.G;
                    bytes[off + 2] = color.B;
                    bytes[off + 3] = color.A;
                }

                return new RawImage(_.width, _.height, bytes);
            }
            catch (Exception e)
            {
                return e;
            }
        }
        
        private static ExcResult<RawImage> ValidateFormat(RawImage image)
        {
            if (image.Width * image.Height * 4 == image.Pixels.Length)
                return image;
            return new InvalidDataException($"Amount of data for Image {
                image.Width} x {image.Height} is not correct. Expected: {image.Width * image.Height * 4} Current: {image.Pixels.Length}");
        }
        
        private static ExcResult<RawImage> ValidateFormat((int w, int h, Memory<byte> pixels) image)
        {
            if (image.w * image.h * 4 == image.pixels.Length)
                return new RawImage(image.w, image.h, image.pixels);
            return new InvalidDataException($"Amount of data for Image {
                image.w} x {image.h} is not correct. Expected: {image.w * image.h * 4} Current: {image.pixels.Length}");
        }
    }
}