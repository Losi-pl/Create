using Create.Conteiner;
using Create.Net;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements.Bazic.Items;

public sealed class BlockItem : Item
{
    public override ItemModel GetItemModel(ItemStack itemStack, Player player)
    {
        ReadOnlySpan<char> blockName;
        {
            int i = itemStack.Meta.IndexOf(';');
            blockName = i == -1 ? itemStack.Meta.AsSpan() : itemStack.Meta.AsSpan().Slice(0, i);
        }
        Block block;
        foreach (var b in Register.Blocks)
            if (b.CodeName.AsSpan().Equals(blockName, StringComparison.Ordinal))
                block = b;

        var model = ModelConstructor.WorldModel(new SingleBlockWorld(itemStack.AsPlacedBlock()), (0, 0), (0, 0), (0, 0));

        return new() { model = new BLockModel() { model = model }, statusBar = null};
    }

    public override bool OnClick(OnClickArgs args)
    {
        if(!args.BlockArgs.HasValue)
            return base.OnClick(args);
        if (args.Button != OpenGL.GUI.ClickEventButton.Right)
            return false;
        args.World.SetBlock(args.BlockArgs.Value.BlockOnSide, args.InHand.Stack.AsPlacedBlock());
        return true;
    }
}

file class SingleBlockWorld : World
{
    PlacedBlock block;

    public SingleBlockWorld(PlacedBlock block) => this.block = block;
    
    public override PlacedBlock GetBlock(int x, int y, int z)
    {
        if ((x, y, z) == (0, 0, 0))
            return block;
        else
            return new(Elements.Blocks.AIR);
    }

    public override void SetBlock(int x, int y, int z, PlacedBlock block)
    {
        throw new NotImplementedException();
    }
}

file struct BLockModel : IDrawable, IDisposable
{
    static Matrix4 transformMatrix = Matrix4.CreateTranslation(-.5f, -.5f, -.5f) *
           Matrix4.CreateRotationY((45 / 180f) * MathF.PI) *
           Matrix4.CreateRotationX((-30 / 180f) * MathF.PI) *
           Matrix4.CreateScale(1.1851851851851f);
    public WorldModel model;

    public void Draw(Matrix4 projection, Matrix4 model) => this.model?.Draw(projection, transformMatrix * model);
    public void Dispose() => model?.Dispose();
}