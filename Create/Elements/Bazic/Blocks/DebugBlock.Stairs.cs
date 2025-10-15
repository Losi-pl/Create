
namespace Create.Elements.Bazic.Blocks;

partial class DebugBlock
{
	public class Stairs : StairsBase
	{
		public override void OnRegistered(Mod mod)
		{
			SetModel(Assets.LoadBlockModel("create:debug-stairs"));
		}
	}
}
