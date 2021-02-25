using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerate : Skill 
{


    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 250;
        m_damage[1] = 325;
        m_damage[2] = 800;

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



        float elapsedtime = 0;
        float dotdamgecount = 0;
        bool stop = false;
        PixelFx fx = FxMng.Instance.FxCall("FireStrike");
        int damage = m_damage[caster.Star - 1];
        fx.gameObject.SetActive(true);
        fx.transform.position = skillrange[4].transform.position;


        while (!stop)
        {

     

            elapsedtime += Time.deltaTime;


            if (elapsedtime >= dotdamgecount)
            {
                foreach(var x in skillrange)
                {
                    if (x.CurrCHAR != null)
                    {
                        if (caster.FOE != x.CurrCHAR.FOE)
                            x.CurrCHAR.MyStatus.DamagedLife(damage / 10, null, x, DamageType.Skill);
                    }
                }
                dotdamgecount += 0.5f;
            }



            if(elapsedtime>=5)
            {
                stop = true;
                fx.gameObject.SetActive(false);
            }


            yield return null;

        }



        caster.SetAttacking(false);

        yield return null;
    }


}
