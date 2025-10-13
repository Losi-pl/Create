
namespace Create.Elements.Bazic.Blocks;

partial class DebugBlock
{
	public class Slab : SlabBase
	{
		public override void OnRegistered(Mod mod)
		{
			SetModel(Assets.LoadBlockModel("create:debug-slab"));
		}
	}
}
