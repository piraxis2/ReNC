using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammering : Skill 
{
      public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 300;
        m_damage[1] = 400;
        m_damage[2] = 550;
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> range = new List<Node>();

        range.Add(target);

        return range;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {



        if (caster.MyStatus.m_stuned)
        {
            caster.SetAttacking(false);
            yield break;
        }

        if (skillrange[0].CurrCHAR == null)
        {
            caster.SetAttacking(false);
            yield break;
        }


        yield return new WaitForSeconds(0.5f);

        if (skillrange[0].CurrCHAR.FOE != caster.FOE)
        {
            skillrange[0].CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[0], DamageType.Skill);
            skillrange[0].CurrCHAR.MyStatus.GetBuff("Stun", 1f);
            caster.MyStatus.CuredLife(m_damage[caster.Star - 1] / 10);
        }



        caster.SetAttacking(false);

        yield return null;
    }


}
