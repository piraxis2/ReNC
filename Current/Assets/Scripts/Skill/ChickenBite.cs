using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBite : Skill 
{

    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 450;
        m_damage[1] = 675;
        m_damage[2] = 1500;

    }

    public override void Skillshot(BaseChar caster, Node target)
    {


        List<BaseChar> skillrange = null;
        caster.MyStatus.ManaCost();
        caster.SetAttacking(false);
        StartCoroutine(IESkillaction(skillrange, caster));

    }

    public override IEnumerator IESkillaction(List<BaseChar> skillrange, BaseChar caster)
    {

        PixelFx fx = FxMng.Instance.FxCall("Heal");
        fx.gameObject.SetActive(true);
        fx.transform.position = caster.transform.position;

        caster.MyStatus.CuredLife(m_damage[caster.Star - 1]);

        yield return new WaitForSeconds(0.5f);
        caster.SetAttacking(false);
        yield return null;
    }



}
