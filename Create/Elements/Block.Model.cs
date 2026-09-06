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
        var world = args.World;
        var texArgs = new GetTextureArgs
        {
            Position = args.Position,
            Target = args.Target,
            World = args.World
        };
        var solidArgs = new IsSideSolidArgs
        {
            World = args.World
        };
                        
        DoFacet(GeneralDirection.North,  in args, ref texArgs, ref solidArgs, BlockModelFaces.NorthFaced);
        DoFacet(GeneralDirection.East,   in args, ref texArgs, ref solidArgs, BlockModelFaces.EastFaced);
        DoFacet(GeneralDirection.South,  in args, ref texArgs, ref solidArgs, BlockModelFaces.SouthFaced);
        DoFacet(GeneralDirection.West,   in args, ref texArgs, ref solidArgs, BlockModelFaces.WestFaced);
        DoFacet(GeneralDirection.Top,    in args, ref texArgs, ref solidArgs, BlockModelFaces.TopFaced);
        DoFacet(GeneralDirection.Bottom, in args, ref texArgs, ref solidArgs, BlockModelFaces.BottomFaced);
        
        // ReSharper disable VariableHidesOuterVariable
        void DoFacet(GeneralDirection direction, in CalculateModelArgs args, ref GetTextureArgs texArgs, ref IsSideSolidArgs solidArgs, WorldModeler.FillOutData<Vector3D<int>> fillOut)
        {
            var position = texArgs.Position + direction.AsVector().As<long>();
            var target = args.World[position.X, position.Y, position.Z];

            if (target.BlockIndex != Blocks.Air.Index)
            {
                solidArgs.Position = position;
                solidArgs.Direction = direction.Inverted;
                solidArgs.Target = target;
                    
                if(target.Block.IsSideSolid(in solidArgs))
                    return;
            }

            texArgs.Direction = direction;
            var texture = texArgs.Target.Block.GetTexture(in texArgs);
                
            args.Modeler.AddModelFacet(4, 2, texArgs.Position.As<int>(), fillOut, texture);
        }
    }
}

file static class BlockModelFaces
{
    private static void SetUvAndTriangles(Span<Vector2D<float>> uvs, Span<uint> triangles)
        {
            uvs[0] = new(0f, 0f);
            uvs[1] = new(1f, 0f);
            uvs[2] = new(1f, 1f);
            uvs[3] = new(0f, 1f);

            triangles[0] = 0u;
            triangles[1] = 1u;
            triangles[2] = 3u;
            triangles[3] = 1u;
            triangles[4] = 2u;
            triangles[5] = 3u;
        }
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> SouthFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(0f, 1f, 0f) + blPos;
                positions[1] = new Vector3D<float>(1f, 1f, 0f) + blPos;
                positions[2] = new Vector3D<float>(1f, 0f, 0f) + blPos;
                positions[3] = new Vector3D<float>(0f, 0f, 0f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> NorthFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(1f, 1f, 1f) + blPos;
                positions[1] = new Vector3D<float>(0f, 1f, 1f) + blPos;
                positions[2] = new Vector3D<float>(0f, 0f, 1f) + blPos;
                positions[3] = new Vector3D<float>(1f, 0f, 1f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> EastFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(1f, 1f, 0f) + blPos;
                positions[1] = new Vector3D<float>(1f, 1f, 1f) + blPos;
                positions[2] = new Vector3D<float>(1f, 0f, 1f) + blPos;
                positions[3] = new Vector3D<float>(1f, 0f, 0f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> WestFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(0f, 1f, 1f) + blPos;
                positions[1] = new Vector3D<float>(0f, 1f, 0f) + blPos;
                positions[2] = new Vector3D<float>(0f, 0f, 0f) + blPos;
                positions[3] = new Vector3D<float>(0f, 0f, 1f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> TopFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(0f, 1f, 1f) + blPos;
                positions[1] = new Vector3D<float>(1f, 1f, 1f) + blPos;
                positions[2] = new Vector3D<float>(1f, 1f, 0f) + blPos;
                positions[3] = new Vector3D<float>(0f, 1f, 0f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
        
        public static readonly WorldModeler.FillOutData<Vector3D<int>> BottomFaced =
            (positions, uvs, triangles, position) => {
                var blPos = position.As<float>();
                positions[0] = new Vector3D<float>(0f, 0f, 0f) + blPos;
                positions[1] = new Vector3D<float>(1f, 0f, 0f) + blPos;
                positions[2] = new Vector3D<float>(1f, 0f, 1f) + blPos;
                positions[3] = new Vector3D<float>(0f, 0f, 1f) + blPos;
                
                SetUvAndTriangles(uvs, triangles);
            };
}