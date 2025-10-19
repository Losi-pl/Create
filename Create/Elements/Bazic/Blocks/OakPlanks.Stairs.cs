namespace Create.Elements.Bazic.Blocks;

partial class OakPlanks
{
    public class Stairs : StairsBase
    {
        public override void OnRegistered(Mod mod)
        {
            SetModel(Assets.LoadBlockModel("create:oak-planks-stairs"));
        }
    }
}
