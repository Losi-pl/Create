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

    // ReSharper disable once InconsistentNaming
    public static readonly SingleTextureFace NO_TEXTURE = new(BlockTexture.NULL);
    protected IBlockModelFace? MainTexture { get; set; }
    public virtual IBlockModelFace GetTexture(in GetTextureArgs args)
    {
        if (MainTexture is not null)
            return MainTexture;
        
        MainTexture = AssetManager.Find<BlockTexture>(Identity) is { IsSet: true, AsSet: var texture } ? new SingleTextureFace(texture) : NO_TEXTURE;
        return MainTexture;
    }
    
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