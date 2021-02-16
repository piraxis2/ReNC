using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagic : Skill 
{
    private WaitForSeconds m_wait = new WaitForSeconds(0.5f);

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 450;
        m_damage[1] = 650;
        m_damage[2] = 900;
    }

    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> targets = new List<BaseChar>();
        if (caster.FOE)
            targets = SkillTargets(CharMng.Instance.CurrEnemys, target.CurrCHAR, caster);
        else
            targets = SkillTargets(CharMng.Instance.CurrHeros, target.CurrCHAR, caster);


        if (targets.Count != 0)
        {
            caster.MyStatus.ManaCost();
            StartCoroutine(IESkillaction(targets, caster));
        }
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {
        List<BaseChar> targets = new List<BaseChar>();

        BaseChar realtarget= null;
        foreach(var x in chararr)
        {
            int hp = 9999999;
            if (hp > x.MyStatus.Life)
            {
                hp = x.MyStatus.Life;
                realtarget = x;
            }
        }

        targets.Add(realtarget);

        return targets;
    }


    public override IEnumerator IESkillaction(List<BaseChar> chararr, BaseChar caster)
    {

        BaseChar target = chararr[0];
        PixelFx projectile = FxMng.Instance.FxCall("BlackBall");
        yield return m_wait;

        if (caster.MyStatus.m_stuned)
        {
            caster.SetAttacking(false);
            yield break;
        }


        bool stop = false;
        float elapsdtime = 0;
        projectile.gameObject.SetActive(true);
        Vector3 pos = caster.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 enemypos = target.transform.position + new Vector3(0, 0.5f, 0);

        while (!stop)
        {
            elapsdtime += Time.deltaTime*8;
            projectile.transform.position = Vector3.Lerp(pos, enemypos, elapsdtime);

            if (elapsdtime>=1)
            {
                stop = true;
            }
            yield return null;
        }

        if (target != null)
        {
            if (target.MyStatus.DamagedLife(m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100), caster, target.CurrNode, DamageType.Skill))
            {
                caster.MyStatus.SetAP(caster.MyStatus.ORIAP + caster.Star);
            }
            PixelFx hit = FxMng.Instance.FxCall("BlackSkull");
            hit.gameObject.SetActive(true);
            hit.transform.position = enemypos;
        }
        projectile.ShutActive();

        caster.SetAttacking(false);

        yield return null;
    }


}
