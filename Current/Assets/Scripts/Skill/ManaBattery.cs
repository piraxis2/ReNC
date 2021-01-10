using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBattery : Skill
{
    private WaitForSeconds m_wait = new WaitForSeconds(0.1f);


    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {
        return chararr;
    }
    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        return null;
    }

    public override void Skillshot(BaseChar caster, Node target)
    {
        List<BaseChar> targets = CharMng.Instance.CurrHeros; //SkillTargets(CharMng.Instance.CurrHeros, null, caster);
        StartCoroutine(IESkillaction(targets, caster));

    }

    public override IEnumerator IESkillaction(List<BaseChar> skillrange, BaseChar caster)
    {

        yield return m_wait;
       

        foreach(var x in skillrange)
        {
            PixelFx fx = FxMng.Instance.FxCall("ManaGet");
            if (x != caster)
            {
                fx.gameObject.SetActive(true);
                fx.transform.position = x.transform.position;
                x.MyStatus.ManaGet(12);
            }
        }
        caster.SetAttacking(false);

        yield return null;
    }

}
