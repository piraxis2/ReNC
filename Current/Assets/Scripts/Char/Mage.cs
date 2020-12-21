using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{

    public override void Init()
    {
        base.Init();
        Status.SetName("mage");
        m_classtype = ClassType.Mage;
        m_classname = "Mage";
        m_projectile = Resources.Load("Prefab/FireBall") as GameObject;
        m_hitfx = Resources.Load("Prefab/boom") as GameObject;
        m_projectileangle = 150;
        m_skill = Skillname.thunderstrike;
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
        Status.RangeSet(3);
        Status.PrioritySet(13);
        Status.SetAS(0.6f);
    }

}
