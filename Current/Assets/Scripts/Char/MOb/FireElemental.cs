using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : Elemental 
{

    public override void Init()
    {
        base.Init();
        m_skill = Skillname.Incinerate;
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
