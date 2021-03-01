using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpellGas : Skill 
{

    PixelFx m_gas;
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
                if (Mathf.Abs(col) <= Mathf.Abs(Mathf.Abs(row) - 1))
                {
                    if (!(target.Row + row > 7 || target.Row + row < 0 || target.Col + col > 7 || target.Col + col < 0))
                    {
                        skillrange.Add(nodearr[target.Row + row, target.Col + col]);
                    }
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

        bool stop = false;
        float elapsedtime = 0;




        while (!stop)
        {
            elapsedtime += Time.deltaTime * 8;


            

            if(elapsedtime>=1)
            {
                stop = true;
            }


            yield return null;

        }

        yield return null;
    }



}
