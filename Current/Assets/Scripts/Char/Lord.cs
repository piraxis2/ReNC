using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lord : BaseChar 
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Lord");
        m_classtype = ClassType.Lord;
        m_classname = "Load";
        m_projectile = Resources.Load("Prefab/FireBall") as GameObject;
        m_hitfx = Resources.Load("Prefab/boom") as GameObject;
        m_projectileangle = 150;
        m_skill = Skillname.LoyalProtect;
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

        MyStatus.SetLife(1000);
        MyStatus.RangeSet(2);
        MyStatus.PrioritySet(13);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }



}
