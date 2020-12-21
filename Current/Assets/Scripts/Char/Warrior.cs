using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    public override void Init()
    {
        base.Init();
        Status.SetName("Warrior");
        m_classtype = ClassType.Warrior;
        m_classname = "Warrior";
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {
        Status.RangeSet(1);
        Status.PrioritySet(9);
        Status.SetAS(0.6f);
    }
}
