using System.Collections.Frozen;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Create.Graphics;

public partial class Texture2DAtlas: IDisposable, GLObject
{
    // ReSharper disable once InconsistentNaming
    private static readonly FrozenDictionary<uint, Exception> NO_ERRORS =
        new Dictionary<uint, Exception>().ToFrozenDictionary();
    
    private readonly Vector3D<uint> _atlasDimensions;
    private readonly uint _handle;
    private FrozenDictionary<uint, Exception>? _errors;

    // ReSharper disable once MemberCanBePrivate.Global
    public bool Disposed { get; private set; }

    private Texture2DAtlas(uint handle, Vector3D<uint> atlasDimensions, FrozenDictionary<uint, Exception>? errors)
    {
        _handle = handle;
        _atlasDimensions = atlasDimensions;
        _errors = errors;
    }

    /// <summary>
    /// Allows to get a dictionary of all errors that have occured during Atlas compilation
    /// </summary>
    /// <param name="removeAfter">Will clear the dictionary afterword (Free up memory)</param>
    public FrozenDictionary<uint, Exception> TakeOutErrors(bool removeAfter = false)
    {
        if(_errors is null)
            return NO_ERRORS;
        if (!removeAfter) return _errors;
        
        var e = _errors;
        _errors = null;
        return e;
    }

    void GLObject.Bind(GL gl) => gl.BindTexture(TextureTarget.Texture2DArray, _handle);
    
    public void Dispose()
    {
        if(Disposed)
            return;
        Disposed = true;
    }

    ~Texture2DAtlas() => Dispose();
}