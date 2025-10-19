using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create.Elements.Bazic.Blocks;

partial class OakPlanks
{
    public class Slab : SlabBase
    {
        public override void OnRegistered(Mod mod)
        {
            SetModel(Assets.LoadBlockModel("create:oak-planks-slab"));
        }
    }
}
