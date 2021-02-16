using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : Skill
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
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (!(target.Row + row > 7 || target.Row + row < 0 || target.Col + col > 7 || target.Col + col < 0))
                {
                    skillrange.Add(nodearr[target.Row + row, target.Col + col]);
                }
                else
                {
                    skillrange.Add(null);
                }
            }
        }
        return skillrange;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        for (int i = 0; i < skillrange.Count; i++)
        {
            if (skillrange[i] == null)
                continue;

            if (skillrange[i].WarningTile.m_Isrunning)
                continue;

            skillrange[i].WarningTile.gameObject.SetActive(true);
            skillrange[i].WarningTile.StartWarning();
        }
        yield return new WaitForSeconds(1f);

        if (caster.MyStatus.m_stuned)
        {
            caster.SetAttacking(false);
            yield break;
        }


        int index2 = 0;
        for (int i = 0; i < 3; i++)
        {
            index2 = i - 3 ;
            for (int j = 0; j < 3; j++)
            {
                index2 +=3;

                if (skillrange[index2] == null)
                    continue;

                if (skillrange[index2].CurrCHAR!= null)
                {
                    if (skillrange[index2].CurrCHAR.FOE == caster.FOE)
                        continue;
                }
                m_thunder = FxMng.Instance.FxCall("Thunder");
                m_thunder.gameObject.SetActive(true);
                m_thunder.transform.position = skillrange[index2].transform.position;

                if (skillrange[index2].CurrCHAR == null)
                    continue;

                if (skillrange[index2].CurrCHAR.FOE != caster.FOE)
                    skillrange[index2].CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star-1], caster, skillrange[index2],DamageType.Skill);

               
            }
           
            yield return new WaitForSeconds(0.15f);
        }

   

        caster.SetAttacking(false);
       
        yield return null;
    }


}
