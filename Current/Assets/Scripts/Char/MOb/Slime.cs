using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseChar
{
    public override void Init()
    {
        base.Init();
        Status.SetName("Slime");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {
        Status.RangeSet(2);
        Status.PrioritySet(11);
        Status.SetLife(500);
        Status.SetAS(0.65f);
        base.StatusSet();


    }
}
