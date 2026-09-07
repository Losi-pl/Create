using Create.World;
using Silk.NET.Maths;

namespace Create.Graphics.Block;

public interface IBlockModelFace
{
    // ReSharper disable UnusedMember.Global
    internal static virtual object CreateNewModelData() => null!;
    internal void AddToModel<T>(object modelData, uint vertexCount, uint triangleCount, WorldModeler.FillOutData<T> fillOutData, Vector3D<long> blockPosition, T fillOutArg);
    internal static virtual Mesh FinishModel(object modelData) => throw new NotSupportedException();
    // ReSharper restore UnusedMember.Global
}