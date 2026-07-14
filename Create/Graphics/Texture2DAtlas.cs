using System.Collections.Frozen;
using Silk.NET.Maths;

namespace Create.Graphics;

public partial class Texture2DAtlas: IDisposable
{
    // ReSharper disable once InconsistentNaming
    private static readonly FrozenDictionary<uint, Exception> NO_ERRORS =
        new Dictionary<uint, Exception>().ToFrozenDictionary();
    
    private readonly Vector3D<uint> _atlasDimensions;
    private readonly uint _handle;
    private FrozenDictionary<uint, Exception>? _errors;

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
        if (removeAfter)
        {
            var e = _errors;
            _errors = null;
            return e;
        }
        return _errors;
    }

    public void Dispose()
    {
        if(Disposed)
            return;
        Disposed = true;
    }

    ~Texture2DAtlas() => Dispose();
}