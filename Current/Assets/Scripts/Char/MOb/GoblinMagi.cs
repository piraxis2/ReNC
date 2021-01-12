using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMagi : Goblin 
{

    protected override void StatusSet()
    {
        base.StatusSet();
        m_skill = Skillname.ChainLightning;

    }
}
