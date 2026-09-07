using System.Drawing;
using CommunityToolkit.HighPerformance.Helpers;
using Create.Assets;
using Create.World;
using Silk.NET.Maths;

using ModelData = (System.Collections.Generic.List<Silk.NET.Maths.Vector3D<float>> Positions, 
                   System.Collections.Generic.List<Silk.NET.Maths.Vector2D<float>> UVs, 
                   System.Collections.Generic.List<uint> TexturesTop, 
                   System.Collections.Generic.List<uint> TexturesBottom,
                   System.Collections.Generic.List<Silk.NET.Maths.Vector3D<float>> ColorTop,
                   System.Collections.Generic.List<Silk.NET.Maths.Vector3D<float>> ColorBottom,
                   System.Collections.Generic.List<uint> Triangles);

namespace Create.Graphics.Block;

public class DoubleColoredTextureFace(BlockTexture top, BlockTexture bottom, Color topColor, Color bottomColor) : IBlockModelFace
{
    public BlockTexture TopTexture => top;
    public Color TopColor => Color.FromArgb(byte.MaxValue, topColor);
    public BlockTexture BottomTexture => bottom;
    public Color BottomColor => Color.FromArgb(byte.MaxValue, bottomColor);
    
    private static Shader Shader
    {
        get => field ??= AssetManager.Find<Shader>("create:blocks/double-colored-texture") is { IsSet: true, AsSet: var set } ? set : throw new Exception("Shader not found");
    } = null!;
    
    static object IBlockModelFace.CreateNewModelData() => new ModelData([], [], [], [], [], [], []);

    void IBlockModelFace.AddToModel<T>(object modelData, uint vertexCount, uint triangleCount, WorldModeler.FillOutData<T> fillOutData, Vector3D<long> blockPosition, T fillOutArg)
    {
        if(!modelData.TryUnbox<ModelData>(out var model))
            throw new ArgumentException("ModelData is not a valid type");
        
        Span<Vector3D<float>> positions = stackalloc Vector3D<float>[(int)vertexCount];
        Span<Vector2D<float>> uvs = stackalloc Vector2D<float>[(int)vertexCount];
        Span<uint> trianglesElem = stackalloc uint[(int)triangleCount * 3];
        
        Span<uint> textureIndTop = stackalloc uint[(int)vertexCount];
        Span<uint> textureIndBottom = stackalloc uint[(int)vertexCount];
        
        Span<Vector3D<float>> colorTop = stackalloc Vector3D<float>[(int)vertexCount];
        Span<Vector3D<float>> colorBottom = stackalloc Vector3D<float>[(int)vertexCount];

        textureIndTop.Fill(top.Index);
        textureIndBottom.Fill(bottom.Index);
        
        fillOutData(positions, uvs, trianglesElem, blockPosition, fillOutArg);
        {
            var prevCount = (uint)model.Positions.Count;
            for (var i = 0; i < trianglesElem.Length; i++)
                trianglesElem[i] += prevCount;
            
            var colTop = TopColor.AsFloatVector();
            colorTop.Fill(colTop.RGB);
            var colBott = BottomColor.AsFloatVector();
            colorBottom.Fill(colBott.RGB);
        }
        model.Positions.AddRange(positions);
        model.UVs.AddRange(uvs);
        
        model.TexturesTop.AddRange(textureIndTop);
        model.TexturesBottom.AddRange(textureIndBottom);
        
        model.ColorTop.AddRange(colorTop);
        model.ColorBottom.AddRange(colorBottom);
        
        model.Triangles.AddRange(trianglesElem);
    }

    static Mesh IBlockModelFace.FinishModel(object modelData)
    {
        if(!modelData.TryUnbox<ModelData>(out var model))
            throw new ArgumentException("modelData is not a valid model data type");
        
        return Mesh.Create(Shader).ManualFillOut()
            .SetAttribute("pos", model.Positions.ToArray())
            .SetAttribute("uvPos", model.UVs.ToArray())
            .SetAttribute("atlasIndTop", model.TexturesTop.ToArray())
            .SetAttribute("atlasIndBottom", model.TexturesBottom.ToArray())
            .SetAttribute("colorTop", model.ColorTop.ToArray())
            .SetAttribute("colorBottom", model.ColorBottom.ToArray())
            .Triangles(model.Triangles.ToArray())
            .Finish();
    }
}