using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpellGas : Skill 
{

    PixelFx[] m_gas = new PixelFx[5];
    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 250;
        m_damage[1] = 375;
        m_damage[2] = 1500;

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> skillrange = new List<Node>();
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (Mathf.Abs(col) <= Mathf.Abs(Mathf.Abs(row) - 1))
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
        }
        return skillrange;
    }


    private void SilenceFiled(List<Node> skillrange)
    {


    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        bool stop = false;
        float elapsedtime = 0;
        float dotdamgecount = 0;
        int damage = m_damage[caster.Star - 1];
        caster.SetAttacking(false);

        for (int i = 0; i < 5; i++)
        {
            m_gas[i] = FxMng.Instance.FxCall("Incinerate");
            m_gas[i].gameObject.SetActive(true);
        }

        int idx = 0;
        foreach (var x in skillrange)
        {
            if (x != null)
            { m_gas[idx].transform.position = x.transform.position; }
            idx++;

        }


        while (!stop)
        {
            elapsedtime += Time.deltaTime;

            if (elapsedtime >= dotdamgecount)
            {
                foreach (var x in skillrange)
                {
                    if (x.CurrCHAR != null)
                    {
                        if (caster.FOE != x.CurrCHAR.FOE)
                            x.CurrCHAR.MyStatus.DamagedLife(damage / 10, null, x, DamageType.Skill);
                    }
                }
                dotdamgecount += 0.5f;
            }




            if (elapsedtime >= 2)
            {
                for(int i= 0; i<5;i++)
                {
                    m_gas[i].gameObject.SetActive(false);
                }
                stop = true;
            }


            yield return null;

        }

        yield return null;
    }



}
