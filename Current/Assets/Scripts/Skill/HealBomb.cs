using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBomb : Skill
{

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 300;
        m_damage[1] = 450;
        m_damage[2] = 900;
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> range = new List<Node>();
        for (int row = -2; row <= 2; row++)
        {
            for (int col = -2; col <= 2; col++)
            {
                if (Mathf.Abs(col) <= Mathf.Abs(Mathf.Abs(row) - 2))
                {
                    if (!(target.Row + row > 7 || target.Row + row < 0 || target.Col + col > 7 || target.Col + col < 0))
                    {
                        range.Add(NodeMng.instance.NodeArr[target.Row + row, target.Col + col]);
                    }
                }
            }
        }

        return range;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        for (int i = 0; i < skillrange.Count; i++)
        {
            if (skillrange[i] == null)
                continue;

            if (skillrange[i].PositiveWarningTile.m_Isrunning)
                continue;

            skillrange[i].PositiveWarningTile.gameObject.SetActive(true);
            skillrange[i].PositiveWarningTile.StartWarning();
        }
        yield return new WaitForSeconds(1f);


        foreach (var x in skillrange)
        {
            if (x.CurrCHAR != null)
            {
                if (x.CurrCHAR == caster)
                    continue;


                if (x.CurrCHAR.FOE == caster.FOE)
                {
                    PixelFx fx2 = FxMng.Instance.FxCall("Heal");
                    fx2.gameObject.SetActive(true);
                    fx2.transform.position = x.CurrCHAR.transform.position;
                    x.CurrCHAR.MyStatus.CuredLife(m_damage[caster.Star - 1]);
                }
            }
        }

        yield return null;
    }

}
