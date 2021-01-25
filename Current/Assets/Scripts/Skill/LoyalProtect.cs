using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoyalProtect : Skill
{

    int[] m_targetcount = new int[3];
    int[] m_buffval = new int[3];
    WaitForSeconds m_wait = new WaitForSeconds(0.5f);

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
        m_targetless = true;
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {

        List<BaseChar> targets = new List<BaseChar>();
        BaseChar temptarget = null;



        for (int j = 0; j < m_targetcount[caster.Star - 1]; j++)
        {
            int templife = 99999;
            temptarget = null;
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
            if (temptarget != null)
                targets.Add(temptarget);
        }




        return targets;

    }
    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> range = SkillTargets(CharMng.Instance.CurrChars(caster.FOE), null, caster);

        if (range.Count == 0)
        {
            caster.SetAttacking(false);
            return;
        }


        caster.MyStatus.ManaCost();

        for (int i = 0; i < range.Count; i++)
        {
            StartCoroutine(IESkillaction(range[i], caster));
        }
    }

    public IEnumerator IESkillaction(BaseChar skillrange, BaseChar caster)
    {


        yield return m_wait;
        PixelFx projectile = FxMng.Instance.FxCall("ProtectProjectile");
        PixelFx ShieldFx = FxMng.Instance.FxCall("Buff");
        projectile.gameObject.SetActive(true);

        caster.SetRunning(true);
        BaseChar target = skillrange;
        float elapsedtime = 0;
        bool stop = false;
        while (!stop)
        {

            elapsedtime += Time.deltaTime * 3;
            projectile.transform.position = Vector3.Lerp(caster.transform.position, target.transform.position, elapsedtime);

            if (elapsedtime >= 1)
            {
                stop = true;
                ShieldFx.gameObject.SetActive(true);
                ShieldFx.transform.position = target.transform.position;
                projectile.gameObject.SetActive(false);
                target.MyStatus.AddShield(target.MyStatus.Shield + (m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100)), 4f);
                target.MyStatus.GetBuff("Enhance", 4f, m_buffval[caster.Star - 1]);
                caster.SetRunning(false);
            }

            yield return null;

        }


        caster.SetAttacking(false);
        yield return null;

    }
}
