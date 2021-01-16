using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoyalProtect : Skill 
{

    int[] m_targetcount = new int[3];
    int[] m_buffval = new int[3];

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 250;
        m_damage[1] = 300;
        m_damage[2] = 400;
        m_targetcount[0] = 2;
        m_targetcount[1] = 3;
        m_targetcount[2] = 4;
        m_buffval[0] = 20;
        m_buffval[1] = 30;
        m_buffval[2] = 60;
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {

        List<BaseChar> targets = new List<BaseChar>();
        BaseChar temptarget = null;
       


        for (int j = 0; j < m_targetcount[caster.Star - 1]; j++)
        {
            int templife = 99999;
            for (int i = 0; i < chararr.Count; i++)
            {
                if (targets.Contains(chararr[i]))
                {
                    continue;
                }

                if (chararr[i].MyStatus.Life < templife)
                {
                    temptarget = chararr[i];
                    templife = temptarget.MyStatus.Life;
                }
            }
            targets.Add(temptarget);
        }




        return targets;

    }
    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> range = SkillTargets(CharMng.Instance.CurrChars(caster.FOE), null, caster);

        StartCoroutine(IESkillaction(range, caster));
    }

    public override IEnumerator IESkillaction(List<BaseChar> skillrange, BaseChar caster)
    {

        float elapsedtime = 0;
        bool stop = false;
        PixelFx projectile = FxMng.Instance.FxCall("ProtectProjectile");
        PixelFx ShieldFx = FxMng.Instance.FxCall("Buff");
        projectile.gameObject.SetActive(true);
        for (int i = 0; i < skillrange.Count; i++)
        {
            BaseChar target = skillrange[i];
            while (!stop)
            {

                elapsedtime += Time.deltaTime * 8;
                projectile.transform.position = Vector3.Lerp(caster.transform.position, target.transform.position, elapsedtime);

                if (elapsedtime >= 1)
                {
                    stop = true;
                    ShieldFx.gameObject.SetActive(true);
                    projectile.gameObject.SetActive(false);
                    target.MyStatus.SetShield(target.MyStatus.Shield + (m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100)));
                    target.MyStatus.GetBuff("Enhance", 4f, m_buffval[caster.Star - 1]);
                }

                yield return null;

            }
        }


        caster.SetAttacking(false);
        yield return null;
    }

}
