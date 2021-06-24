using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChicken : Hero 
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("BlueChicken");
        m_classtype = ClassType.Warrior;
        m_classname = "Beast";
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.ChickenBite;
        SetRace(Race.Beast);
        SetRace(Race.Healer);
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
