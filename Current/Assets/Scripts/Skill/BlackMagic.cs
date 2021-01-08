using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMagic : Skill 
{

    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 450;
        m_damage[1] = 650;
        m_damage[2] = 900;
    }

    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> targets = SkillTargets(CharMng.Instance.CurrEnemys, target.CurrCHAR, caster);
        StartCoroutine(IESkillaction(targets, caster));
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
        projectile.gameObject.SetActive(true);
        Vector3 pos = caster.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 enemypos = target.transform.position + new Vector3(0, 0.5f, 0);

        bool stop = false;
        float elasedtime = 0;



        while (!stop)
        {
            elasedtime += Time.deltaTime*8;
            projectile.transform.position = Vector3.Lerp(pos, enemypos, elasedtime);

            if (elasedtime>=1)
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
        yield return null;
    }


}
