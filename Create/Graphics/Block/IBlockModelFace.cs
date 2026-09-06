using Create.World;
using Silk.NET.Maths;

namespace Create.Graphics.Block;

public interface IBlockModelFace
{
    // ReSharper disable UnusedMember.Global
    internal static abstract object CreateNewModelData();
    internal void AddToModel<T>(object modelData, uint vertexCount, uint triangleCount, WorldModeler.FillOutData<T> fillOutData, Vector3D<long> blockPosition, T fillOutArg);
    internal static abstract Mesh FinishModel(object modelData);
    // ReSharper restore UnusedMember.Global
}