using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElemental : Elemental
{

    public override void Init()
    {
        base.Init();
        m_skill = Skillname.ManaBattery;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Boom");
    }

    public override PixelFx ProjectileCall()
    {
        return FxMng.Instance.FxCall("FireBall");
    }




}
