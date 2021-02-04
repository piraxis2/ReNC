using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engel : BaseChar 
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Engel");
        m_classname = "Engel";
        m_projectile = Resources.Load("Prefab/FireBall") as GameObject;
        m_hitfx = Resources.Load("Prefab/boom") as GameObject;
        m_projectileangle = 150;
        m_skill = Skillname.HealingWave;
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
        MyStatus.SetAD(50);
        MyStatus.RangeSet(3);
        MyStatus.SetLife(500);
        MyStatus.PrioritySet(13);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }


}