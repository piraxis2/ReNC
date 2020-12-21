using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sans : BaseChar
{
    public override void Init()
    {
        base.Init();
        Status.SetName("Sans");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {
        Status.RangeSet(1);
        Status.PrioritySet(11);
        Status.SetLife(600);
        Status.SetAS(0.6f);

    }
}
