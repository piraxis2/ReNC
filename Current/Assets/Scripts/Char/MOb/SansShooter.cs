using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansShooter : Sans
{
    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {
        base.StatusSet();
        Status.RangeSet(4);
    }
}
