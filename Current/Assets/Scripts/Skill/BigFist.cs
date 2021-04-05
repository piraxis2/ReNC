using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFist : Skill
{

    PixelFx m_fx;

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

        m_fx = FxMng.Instance.FxCall("Fist");
        m_fx.gameObject.SetActive(true);
        Vector3 spos = skillrange[4].transform.position;
        Vector3 epos = new Vector3(spos.x, spos.y + 5, spos.z);


        float elapsedtime = 0;
        bool stop = false;
        while(!stop)
        {

            elapsedtime += Time.deltaTime * 8;
            m_fx.transform.position = Vector3.Lerp(epos, spos, elapsedtime);

            if(elapsedtime>=1f)
            {

                m_fx.transform.GetChild(0).gameObject.SetActive(true);
                foreach (var x in skillrange)
                {
                    if (x.CurrCHAR != null && x != null)
                    {
                        if (caster.FOE != x.CurrCHAR.FOE)
                            x.CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star - 1], null, x, DamageType.Skill);
                    }
                }
                stop = true;
            }

            yield return null;
        }

        caster.SetAttacking(false);




        yield return null;
    }



}

