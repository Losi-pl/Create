using Create.Storage;
using Create.World;
using Silk.NET.Maths;

namespace Create.Elements;

partial class Block
{
    public struct CalculateModelArgs
    {
        public PlacedBlock Target;
        public IWorld World;
        public Vector3D<long> Position;
        public WorldModeler.API Modeler;
    }

    public virtual void CalculateModel(in CalculateModelArgs args)
    {
        
    }
}