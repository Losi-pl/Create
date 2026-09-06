using Create.Assets;
using Create.Graphics.Block;
using Create.Registry;
using Create.Storage;
using Create.World;
using Silk.NET.Maths;

namespace Create.Elements;

public abstract partial class Block : ElementBase
{
    
    public struct GetTextureArgs
    {
        public GeneralDirection Direction;
        public PlacedBlock Target;
        public IWorld World;
        public Vector3D<long> Position;
    }

    private static readonly SingleTextureFace NoTexture = new(BlockTexture.NULL);
    public virtual IBlockModelFace GetTexture(in GetTextureArgs args) => NoTexture;
    
    public struct IsSideSolidArgs
    {
        public GeneralDirection Direction;
        public PlacedBlock Target;
        public IWorld World;
        public Vector3D<long> Position;

        public static implicit operator IsSideSolidArgs(GetTextureArgs args) => new()
        {
            Direction = args.Direction,
            Target = args.Target,
            World = args.World,
            Position = args.Position
        };
    }
    
    public virtual bool IsSideSolid(in IsSideSolidArgs args) => true;
}