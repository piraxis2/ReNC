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
        SetRace(Race.White);
        SetRace(Race.Sniper);
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
        MyStatus.SetName("Shururu");
        MyStatus.SetMovePoint(0);
        MyStatus.RangeSet(6);
        MyStatus.PrioritySet(11);
        MyStatus.SetLife(350);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }

}
