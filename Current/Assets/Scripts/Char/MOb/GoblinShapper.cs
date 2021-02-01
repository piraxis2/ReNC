using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinShapper : Goblin
{


    public override PixelFx ProjectileCall()
    {

        return FxMng.Instance.FxCall("Dynamite");
    }

    protected override void StatusSet()
    {
        base.StatusSet();
        MyStatus.RangeSet(4);
        m_skill = Skillname.TNT;
        m_projectiletype = ProjectileType.Howitzer;
    }


}
