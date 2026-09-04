using Create.Assets;
using Create.Registry;
using Create.Storage;
using Create.World;
using Silk.NET.Maths;

namespace Create.Elements;

public abstract class Block : ElementBase
{
    public struct GetTextureArgs
    {
        public GeneralDirection Direction;
        public PlacedBlock Target;
        public IWorld World;
        public Vector3D<long> Position;
    }

    public virtual BlockTexture GetTexture(in GetTextureArgs args) => BlockTexture.NULL;
}