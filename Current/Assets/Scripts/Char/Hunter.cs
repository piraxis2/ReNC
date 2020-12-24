using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Hero
{

    public override void Init()
    {
        base.Init();
        Status.SetName("hunter");
        m_classtype = ClassType.Hunter;
        m_classname = "Hunter";
        m_projectile = Resources.Load("Prefab/Arrow") as GameObject;
        m_hitfx = Resources.Load("Prefab/HitEx") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.arrowpenetrate;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    public override PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("Arrow");
    }


    protected override void StatusSet()
    {

        Status.RangeSet(5);
        Status.PrioritySet(5);
        Status.SetAS(0.75f);
        base.StatusSet();

    }

}
