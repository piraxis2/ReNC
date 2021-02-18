using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOfKnives : Skill 
{

    PixelFx m_thunder;
    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 450;
        m_damage[1] = 675;
        m_damage[2] = 1500;

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> skillrange = new List<Node>();

        skillrange = caster.RangeList;

        return skillrange;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {



        PixelFx Fan = FxMng.Instance.FxCall("Knives");

        


        yield return null;
    }



}
