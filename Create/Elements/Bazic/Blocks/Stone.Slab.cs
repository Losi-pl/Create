
namespace Create.Elements.Bazic.Blocks;

partial class Stone
{
	public class Slab : SlabBase
	{
		public override void OnRegistered(Mod mod)
		{
			SetModel(Assets.LoadBlockModel("create:stone-slab"));
		}
	}
}
