using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMagi : Goblin 
{

    public override void Init()
    {
        base.Init();
        SetRace(Race.Mage);
    }
    protected override void StatusSet()
    {
        base.StatusSet();
        m_skill = Skillname.PolyMorph;
        MyStatus.RangeSet(4);



    }
}
