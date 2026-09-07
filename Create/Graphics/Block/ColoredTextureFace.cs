using System.Drawing;
using CommunityToolkit.HighPerformance.Helpers;
using Create.Assets;
using Create.World;
using Silk.NET.Maths;

using ModelData = (System.Collections.Generic.List<Silk.NET.Maths.Vector3D<float>> Positions,
                   System.Collections.Generic.List<Silk.NET.Maths.Vector2D<float>> UVs,
                   System.Collections.Generic.List<uint> Textures,
                   System.Collections.Generic.List<Silk.NET.Maths.Vector3D<float>> Colors,
                   System.Collections.Generic.List<uint> Triangles);

namespace Create.Graphics.Block;

public class ColoredTextureFace(BlockTexture texture, Color color) : IBlockModelFace
{
    public BlockTexture Texture => texture;
    public Color Color => Color.FromArgb(byte.MaxValue, color);
    
    private static Shader Shader
    {
        get => field ??= AssetManager.Find<Shader>("create:blocks/colored-texture") is { IsSet: true, AsSet: var set } ? set : throw new Exception("Shader not found");
    } = null!;
    
    static object IBlockModelFace.CreateNewModelData() => new ModelData([], [], [], [], []);

    void IBlockModelFace.AddToModel<T>(object modelData, uint vertexCount, uint triangleCount, WorldModeler.FillOutData<T> fillOutData, Vector3D<long> blockPosition, T fillOutArg)
    {
        if(!modelData.TryUnbox<ModelData>(out var model))
            throw new ArgumentException("ModelData is not a valid type");
        
        Span<Vector3D<float>> positions = stackalloc Vector3D<float>[(int)vertexCount];
        Span<Vector2D<float>> uvs = stackalloc Vector2D<float>[(int)vertexCount];
        Span<uint> trianglesElem = stackalloc uint[(int)triangleCount * 3];
        Span<uint> textureInd = stackalloc uint[(int)vertexCount];
        Span<Vector3D<float>> colors = stackalloc Vector3D<float>[(int)vertexCount];

        fillOutData(positions, uvs, trianglesElem, blockPosition, fillOutArg);
        {
            var prevCount = (uint)model.Positions.Count;
            for (var i = 0; i < trianglesElem.Length; i++)
                trianglesElem[i] += prevCount;
            
            textureInd.Fill(texture.Index);
            var col = Color.AsFloatVector();
            colors.Fill(col.RGB);
        }
        
        model.Positions.AddRange(positions);
        model.UVs.AddRange(uvs);
        model.Textures.AddRange(textureInd);
        model.Colors.AddRange(colors);
        model.Triangles.AddRange(trianglesElem);
    }

    static Mesh IBlockModelFace.FinishModel(object modelData)
    {
        if(!modelData.TryUnbox<ModelData>(out var model))
            throw new ArgumentException("modelData is not a valid model data type");
        
        return Mesh.Create(Shader).ManualFillOut()
            .SetAttribute("pos", model.Positions.ToArray())
            .SetAttribute("uvPos", model.UVs.ToArray())
            .SetAttribute("atlasInd", model.Textures.ToArray())
            .SetAttribute("color", model.Colors.ToArray())
            .Triangles(model.Triangles.ToArray())
            .Finish();
    }
}