using static Create.Elements.Bazic.Items.IClothing;
namespace Create.Elements.Bazic.Items;

using Create.Conteiner;
using Create.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class IronHelmet : Item, IClothing
{
    public virtual Placement GetArmorPlacement(GetArmorPlacementData data) =>
        Placement.Head;

    public override uint MaxStackCount(StackData stackData) => 1;
}
