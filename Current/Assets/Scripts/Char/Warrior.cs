using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Warrior");
        m_classtype = ClassType.Warrior;
        m_classname = "Warrior";
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.WhirlWind;
        SetRace(Race.Fighter);
    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("Hit");

    }

    protected override void StatusSet()
    {
        MyStatus.RangeSet(1);
        MyStatus.SetLife(600);
        MyStatus.PrioritySet(9);
        MyStatus.SetAS(0.6f);
        base.StatusSet();

    }
}
