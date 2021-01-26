using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : Skill
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
        List<BaseChar> chrange = ch.RangeCall();

        if (chrange[0] != null)
        {

            if (chrange[0].Dying)
            {

            }
            else
            {
                Range.Add(chrange[0]);
                ch = chrange[0];
            }
        }
        else
        {
            return Range;
        }
        int count = 2;
        if (caster.Star >= 3)
            count = 3;

        for (int i = 0; i < count; i++)
        {
            List<BaseChar> se = ch.FoeRangeCall(3);

            if(se.Count<=0)
            {
                return Range;
            }

            if (se[0] != null)
            {
                Range.Add(se[0]);
                ch = se[0];
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

        Transform start =caster.transform;
        Transform end = skillrange[0].transform;


        for (int i = 0; i < skillrange.Count; i++)
        {


            LightningBoltFx fx = FxMng.Instance.FxCall("LightningBolt") as LightningBoltFx;
            fx.gameObject.SetActive(true);
            end = skillrange[i].transform;
            fx.BoltStart(start, end);
            start = end;
            skillrange[i].MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[i].CurrNode, DamageType.Skill);

            yield return m_wait;
        }


        caster.SetAttacking(false);

        yield return null;
    }

 




}
