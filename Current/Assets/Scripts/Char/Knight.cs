using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Hero
{

    public override void Init()
    {
        base.Init();
        Status.SetName("Knight");
        m_classtype = ClassType.Knight;
        m_classname = "Knight";
        m_hitfx = Resources.Load("Prefab/Sting") as GameObject;
        m_projectileangle = 180;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {
        Status.RangeSet(1);
        Status.PrioritySet(10);
        Status.SetAS(0.6f);
        base.StatusSet();

    }
}
