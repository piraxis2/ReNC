using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOfKnives : Skill 
{


    private float[] m_times = new float[3];
    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 125;
        m_damage[1] = 200;
        m_damage[2] = 450;
        m_times[0] = 2f;
        m_times[1] = 2.5f;
        m_times[2] = 3f;

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> skillrange = new List<Node>();

        skillrange = caster.RangeList;

        return skillrange;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {


        WaitForSeconds wait = new WaitForSeconds(0.125f);
        PixelFx Fan = FxMng.Instance.FxCall("Knives");
        Fan.transform.position = caster.transform.position;
        Fan.gameObject.SetActive(true);



        for (int j = 0; j < skillrange.Count; j++)
        {
            if (skillrange[j].CurrCHAR != null)
            {
                BaseChar target = skillrange[j].CurrCHAR;
                if (caster.FOE != target.FOE)
                {
                    target.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[j], DamageType.Skill);
                    target.MyStatus.GetBuff("Bleeding", m_times[caster.Star - 1], m_damage[caster.Star - 1]);
                }
            }
        }



        caster.SetAttacking(false);

        



        yield return null;
    }



}
