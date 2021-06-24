using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : Elemental 
{
    public override void Init()
    {
        base.Init();
        m_skill = Skillname.DeSpellGas;
        SetRace(Race.Undead);
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
