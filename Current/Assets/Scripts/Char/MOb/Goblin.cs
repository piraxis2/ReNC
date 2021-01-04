using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseChar
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Goblin");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {
        MyStatus.RangeSet(1);
        MyStatus.PrioritySet(11);
        MyStatus.SetLife(650);
        MyStatus.SetAS(0.65f);
        MyStatus.SetDF(40);
        base.StatusSet();

    }
}
