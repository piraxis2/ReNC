using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WhirlWind : Skill
{

    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        return null;
    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        yield return null;
    }

}
