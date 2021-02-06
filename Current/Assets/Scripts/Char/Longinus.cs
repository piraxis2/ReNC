using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longinus : BaseChar 
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Longinus");
        m_classname = "Longinus";
        m_projectile = Resources.Load("Prefab/FireBall") as GameObject;
        m_hitfx = Resources.Load("Prefab/boom") as GameObject;
        m_projectileangle = 150;
        m_skill = Skillname.ShockWave;
        m_projectiletype = ProjectileType.Invisible;
    }
    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Spike");

    }
    public override PixelFx ProjectileCall()
    {
        return null;
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
