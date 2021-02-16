using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidShot : Skill 
{

    private float[] m_plusasval = new float[3];
    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_plusasval[0] = 1.45f;
        m_plusasval[1] = 1.65f;
        m_plusasval[2] = 3f;
    }

    public override void Skillshot(BaseChar caster, Node target)
    {

        caster.MyStatus.ManaCost();
        caster.SetAttacking(false);
        caster.MyStatus.GetBuff("RapidShot", 5f, -1, -1, m_plusasval[caster.Star-1]);


    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        caster.SetAttacking(false);
        Hunter hunter = (caster) as Hunter;


        hunter.RapidOnOff(true);
        float elapsedtime = 0;
        bool stop = false;
        while (!stop)
        {
            elapsedtime += Time.deltaTime;

            if(elapsedtime>=5)
            {
                hunter.RapidOnOff(false);
                stop = true;
            }
            yield return null;
        }



        yield return null;


    }


}
