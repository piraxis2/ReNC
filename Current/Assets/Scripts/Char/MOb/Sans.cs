using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sans : BaseChar
{
    public override void Init()
    {
        base.Init();
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.Bite;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {

        Status.SetName("Sans");
        Status.PrioritySet(11);
        Status.SetLife(600);
        Status.SetAS(0.6f);
        base.StatusSet();

    }
}
