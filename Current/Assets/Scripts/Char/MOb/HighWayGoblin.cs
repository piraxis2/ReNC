using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighWayGoblin : Goblin 
{


    public override void Init()
    {
        base.Init();
        SetRace(Race.Assassin);
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");
    }


    protected override void StatusSet()
    {
        base.StatusSet();
        m_skill = Skillname.PointBlankShot;
    }


}
