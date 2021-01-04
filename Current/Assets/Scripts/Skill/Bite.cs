using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Skill
{

    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> range = new List<Node>();
        range.Add(target);

        return range;
    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        PixelFx fx = FxMng.Instance.FxCall("Bite");
        fx.gameObject.SetActive(true);
        fx.transform.position = skillrange[0].transform.position;

        if (skillrange[0].CurrCHAR == null)
        {
            caster.SetAttacking(false);
            yield break;
        }

        if (skillrange[0].CurrCHAR.FOE != caster.FOE)
        {
            skillrange[0].CurrCHAR.MyStatus.DamagedLife(225, caster, skillrange[0], DamageType.Skill);
            skillrange[0].CurrCHAR.MyStatus.GetBuff("SlowAS", 3f);
        }


        caster.SetAttacking(false);

        yield return null;
    }


}
