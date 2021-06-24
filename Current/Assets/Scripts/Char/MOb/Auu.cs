using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auu : BaseChar 
{
    public override void Init()
    {
        base.Init();
        MyStatus.SetName("Auu");
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        SetRace(Race.White);
        SetRace(Race.Hero);

    }

    public override PixelFx FxCall()
    {
        return FxMng.Instance.FxCall("ArrowHit");

    }

    protected override void StatusSet()
    {
        MyStatus.RangeSet(1);
        MyStatus.PrioritySet(11);
        MyStatus.SetLife(650);
        MyStatus.SetAS(0.65f);
        MyStatus.SetDF(40);
        m_skill = Skillname.Kamehameha;
        base.StatusSet();
         }

}
