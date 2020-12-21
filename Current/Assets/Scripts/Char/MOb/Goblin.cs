using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseChar
{
    public override void Init()
    {
        base.Init();
        Status.SetName("Goblin");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {
        Status.RangeSet(1);
        Status.PrioritySet(11);
        Status.SetLife(650);
        Status.SetAS(0.65f);
        Status.SetDF(40);
    }
}
