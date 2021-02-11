using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWave : Skill
{


    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 200;
        m_damage[1] = 350;
        m_damage[2] = 525;
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {

        List<BaseChar> Range = new List<BaseChar>();


        BaseChar ch = caster;
        List<BaseChar> chrange;



        if(caster.FOE)
        {
            chrange = CharMng.Instance.CurrHeros;
        }
        else
        {
            chrange = CharMng.Instance.CurrEnemys;
        }

       
        if (chrange != null)
        {
            int hp = 999999;
            for (int i = 0; i < chrange.Count; i++)
            {
                if (hp >= chrange[i].MyStatus.Life)
                {
                    hp = chrange[i].MyStatus.Life;
                    ch = chrange[i];
                }
            }
        }
        Range.Add(ch);


        int count = caster.Star+2;

        for (int i = 0; i < count; i++)
        {
            List<BaseChar> se = ch.FoeRangeCall(5);

            if (se.Count <= 0)
            {
                return Range;
            }

           
            int hp = 9999;
            BaseChar temp = null;
            for (int j = 0; j < se.Count; j++)
            {
                if (hp > se[j].MyStatus.Life)
                {
                    temp = se[j];
                    hp = temp.MyStatus.Life;
                }

            }


            if (temp != null)
            {
                Range.Add(temp);
                ch = temp;
            }
            else
            {
                return Range;
            }
        }

        return Range;
    }

    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> range = SkillTargets(null, null, caster);

        if (range.Count == 0)
        {
            caster.SetAttacking(false);
            return;
        }

        caster.MyStatus.ManaCost();
        StartCoroutine(IESkillaction(range, caster));
    }


    private WaitForSeconds m_wait = new WaitForSeconds(0.1f);

    public override IEnumerator IESkillaction(List<BaseChar> skillrange, BaseChar caster)
    {

        Transform start = caster.transform;
        Transform end = skillrange[0].transform;

        int healingval = m_damage[caster.Star - 1];

        for (int i = 0; i < skillrange.Count; i++)
        {


            PixelFx fx = FxMng.Instance.FxCall("HealingWave");
            fx.transform.position = start.position;
            fx.gameObject.SetActive(true);
            end = skillrange[i].transform;
            bool stop = false;
            float elapsedtime = 0;
            Vector3 vec = new Vector3();
            while (!stop)
            {

                elapsedtime += Time.deltaTime * 4;
                vec = Vector3.Lerp(start.position, end.position, elapsedtime);

                if (elapsedtime <= 0.1f)
                    fx.transform.position = vec;

                else if (elapsedtime <= 0.9f)
                    fx.transform.position = vec;

                if (elapsedtime >= 1)
                {
                    fx.transform.position = vec;
                    stop = true;
                    PixelFx heal = FxMng.Instance.FxCall("Heal");
                    fx.ShutActive();
                    heal.transform.position = end.position;
                    heal.gameObject.SetActive(true);
                }

                yield return null;
            }

            start = end;
            skillrange[i].MyStatus.CuredLife(healingval,caster);
            healingval = healingval - (healingval / 4);
            //DamagedLife(m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100), caster, skillrange[i].CurrNode, DamageType.Skill);


            yield return null;
        }


        caster.SetAttacking(false);

        yield return null;
    }






}

