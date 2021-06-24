using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilloWisp : Elemental
{

    public override void Init()
    {
        base.Init();
        m_skill = Skillname.ChainLightning;
        SetRace(Race.Mage);
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
