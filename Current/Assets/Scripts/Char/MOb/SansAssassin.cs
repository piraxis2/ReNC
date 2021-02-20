using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansAssassin : Sans 
{



    public override void Init()
    {
        base.Init();
        m_hitfx = Resources.Load("Prefab/Attack") as GameObject;
        m_projectileangle = 0;
        m_skill = Skillname.FanofKnives;
    }



    protected override void StatusSet()
    {
        MyStatus.SetName("SansAssassin");
        MyStatus.PrioritySet(11);
        MyStatus.SetLife(600);
        MyStatus.SetAS(0.6f);
        base.StatusSet();
    }

}
