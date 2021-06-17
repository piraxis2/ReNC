using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decimate : Skill 
{

    int[] m_cure = new int[3]; 

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 450;
        m_damage[1] = 675;
        m_damage[2] = 1125;
        m_cure[0] = 50;
        m_cure[1] = 75;
        m_cure[2] = 125;
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> range = new List<Node>();
        return caster.RangeList;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        int count = 0;

        for (int j = 0; j < skillrange.Count; j++)
        {
            if (skillrange[j].CurrCHAR != null)
            {
                BaseChar target = skillrange[j].CurrCHAR;
                if (caster.FOE != target.FOE)
                {
                    target.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[j], DamageType.Skill);
                    count++;
                }
            }
        }

        if (count >= 0)
        {
            PixelFx heal = FxMng.Instance.FxCall("Heal");
            heal.gameObject.SetActive(true);
            heal.transform.position = caster.transform.position;
            caster.MyStatus.CuredLife(m_cure[count - 1]);
        }




        caster.SetAttacking(false);

        yield return null;
    }

}
