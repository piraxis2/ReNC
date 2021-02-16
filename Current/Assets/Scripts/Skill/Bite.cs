using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Skill
{
    PixelFx m_fx;
    PixelFx m_fx2;

    public override void Init(FxMng fx)
    {

        base.Init(fx);
        m_damage[0] = 225;
        m_damage[1] = 350;
        m_damage[2] = 550;
        m_fx = FxMng.Instance.FxCall("Bite");
        m_fx2 = FxMng.Instance.FxCall("Heal");

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> range = new List<Node>();
        range.Add(target);

        return range;
    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

          m_fx2.gameObject.SetActive(true);
        m_fx.gameObject.SetActive(true);
        m_fx.transform.position = skillrange[0].transform.position;
        m_fx2.transform.position = caster.transform.position;

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

        if (skillrange[0].CurrCHAR.FOE != caster.FOE)
        {
            skillrange[0].CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star-1], caster, skillrange[0], DamageType.Skill);
            skillrange[0].CurrCHAR.MyStatus.GetBuff("ASChange", 3f, -1, -1, 0.5f);
            caster.MyStatus.CuredLife(m_damage[caster.Star-1]/10);
        }



        caster.SetAttacking(false);

        yield return null;
    }


}
