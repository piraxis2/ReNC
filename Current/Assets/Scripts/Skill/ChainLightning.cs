using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : Skill
{



    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {

        List<BaseChar> Range = new List<BaseChar>();
       

        BaseChar ch = caster;
        List<BaseChar> chrange = ch.RangeCall();

        if (chrange[0] != null)
        {
            Range.Add(chrange[0]);
            ch = chrange[0];
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

            if (se[0] != null)
            {
                Debug.Log(se[0].name);
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


        for (int i = 0; i < skillrange.Count; i++)
        {
           // Debug.Log(skillrange[i].name);


            LightningBoltFx fx = FxMng.Instance.FxCall("LightningBolt") as LightningBoltFx;
            fx.gameObject.SetActive(true);
            fx.BoltStart(caster.transform, skillrange[i].transform);
            yield return m_wait;
        }


        caster.SetAttacking(false);

        yield return null;
    }

 




}
