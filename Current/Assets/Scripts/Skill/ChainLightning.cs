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

            if (!chrange[0].Dying)
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

            int random = Random.Range(0, se.Count - 1);

            if (se[random] != null)
            {
                Range.Add(se[random]);
                ch = se[random];
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


            PixelFx fx = FxMng.Instance.FxCall("ChainLightning");
            fx.transform.position = start.position;
            fx.gameObject.SetActive(true);
            end = skillrange[i].transform;
            bool stop = false;
            float elapsedtime = 0;
            Vector3 vec = new Vector3();
            while(!stop)
            {

                elapsedtime += Time.deltaTime * 4;
                vec = Vector3.Lerp(start.position, end.position, elapsedtime);

                float x = Random.Range(-0.25f, 0.25f);

                if (elapsedtime <= 0.1f)
                {
                    fx.transform.position = vec;
                }
                else if (elapsedtime <= 0.9f)
                    fx.transform.position = new Vector3(vec.x, vec.y, vec.z + x);



                if (elapsedtime>=1)
                {
                    fx.transform.position = vec;
                    stop = true;
                    PixelFx lightning = FxMng.Instance.FxCall("Thunder");
                    fx.ShutActive();
                    lightning.transform.position = end.position;
                    lightning.gameObject.SetActive(true);
                }

                yield return null;
            }

            start = end;
            skillrange[i].MyStatus.DamagedLife(m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100), caster, skillrange[i].CurrNode, DamageType.Skill);

            yield return null;
        }


        caster.SetAttacking(false);

        yield return null;
    }

 




}
