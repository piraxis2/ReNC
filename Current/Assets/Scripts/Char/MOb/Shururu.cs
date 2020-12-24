using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shururu : BaseChar 
{
    public override void Init()
    {
        base.Init();
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 150;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Boom");
    }

    public override PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("FireBall");
    }



    protected override void StatusSet()
    {
        Status.SetName("Shururu");
        Status.SetMovePoint(0);
        Status.RangeSet(6);
        Status.PrioritySet(11);
        Status.SetLife(350);
        Status.SetAS(0.6f);
        base.StatusSet();

    }

}
