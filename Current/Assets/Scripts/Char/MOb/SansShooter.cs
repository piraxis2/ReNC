using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansShooter : BaseChar
{
    public override void Init()
    {
        base.Init();
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    protected override void StatusSet()
    {
        Status.RangeSet(4);
        Status.SetName("SansShooter");
        Status.PrioritySet(11);
        Status.SetLife(600);
        Status.SetAS(0.6f);
        base.StatusSet();

    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

  }
