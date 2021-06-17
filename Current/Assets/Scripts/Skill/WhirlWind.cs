using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WhirlWind : Skill
{
    private WaitForSeconds wfs = new WaitForSeconds(0.5f);

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 450;
        m_damage[1] = 675;
        m_damage[2] = 1125;
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> range = new List<Node>();
        return caster.RangeList;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        PixelFx fx = FxMng.Instance.FxCall("Wind");


        fx.gameObject.SetActive(true);
        fx.transform.position = caster.transform.position;
        int damage = (int)(m_damage[caster.Star - 1] / 8);
        float elapsedtime = 0;
        float count = 0.5f;
        bool stop = false;
        while(!stop)
        {

            if (caster.Dying)
            {
                stop = true;
                fx.ShutActive();
                CharActionMng.ChangeDeadAni(CharActionMng.Direction(caster.CurrNode, null), caster);
            }


            if (caster.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Casting")||caster.m_animator.GetCurrentAnimatorStateInfo(0).IsName("Casting2"))
            {
                caster.m_animator.Play("Casting");
            }
         
            fx.transform.position = caster.transform.position;
            elapsedtime += Time.deltaTime;
            if(elapsedtime>=count)
            {
                count += 0.5f;
                foreach (var x in skillrange)
                {
                    if (x.CurrCHAR != null)
                    {
                        if (x.CurrCHAR == caster)
                            continue;

                        if (x.CurrCHAR.FOE != caster.FOE)
                            x.CurrCHAR.MyStatus.DamagedLife(damage, caster, x, DamageType.Skill);
                    }
                }
            }

            if(elapsedtime>=4)
            {
                stop = true;
                fx.ShutActive();
            }
       

            yield return null;
        }

         
        caster.SetAttacking(false);
        yield return null;
    }

}
