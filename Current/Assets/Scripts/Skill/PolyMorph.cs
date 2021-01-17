using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyMorph : Skill
{



    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }

    public override List<BaseChar> SkillTargets(List<BaseChar> chararr, BaseChar target, BaseChar caster)
    {
        List<BaseChar> Returntarget = new List<BaseChar>();
        List<BaseChar> targets;

        if (caster.FOE)
            targets = CharMng.Instance.CurrEnemys;
        else
            targets = CharMng.Instance.CurrHeros;


        BaseChar realtarget = new BaseChar();
        int count = 0;


        for (int i = 3; i > 0; i--)
        {
            for (int j = 0; j < targets.Count; j++)
            {

                if (targets[j].MyStatus.FindBuff("Polymorph") >= 0)
                    continue;

                if (caster.Star <= count)
                {
                    i = 0;
                    break;
                }


                if (targets[j].Star == i)
                {
                    Returntarget.Add(targets[j]);
                    count++;
                }

            }

        }

 
        return Returntarget;
    }
    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> nodes = new List<Node>();
     




        return base.SkillRange(nodearr, target, caster);
    }


    public override void Skillshot(BaseChar caster, Node target)
    {


        List<BaseChar> range = SkillTargets(null, null, caster);

        if(range.Count == 0)
        {
            caster.SetAttacking(false);
            return;
        }


        caster.MyStatus.ManaCost();

        StartCoroutine(IESkillaction(range, caster));
    }


    private WaitForSeconds m_wait = new WaitForSeconds(0.3f);

    public override IEnumerator IESkillaction(List<BaseChar> skillrange, BaseChar caster)
    {

     
        for (int i = 0; i < skillrange.Count; i++)
        {
            yield return m_wait;
            bool stop = false;
            float elapsedtime = 0;
            PixelFx projectile = FxMng.Instance.FxCall("PolyMorphFx");
  
            BaseChar target = skillrange[i];
            Vector3 pos = caster.transform.position + new Vector3(0, 0.5f, 0);
            Vector3 enemypos = target.transform.position + new Vector3(0, 0.5f, 0);
            while (!stop)
            {

                elapsedtime += Time.deltaTime * 8;
                projectile.transform.position = Vector3.Lerp(pos, enemypos, elapsedtime);

                if (elapsedtime>=1)
                {
                    target.MyStatus.GetBuff("Polymorph", 3);
                    PixelFx hit = FxMng.Instance.FxCall("PolyMorphBomb");
                    hit.gameObject.SetActive(true);
                    hit.transform.position = skillrange[i].transform.position;
                    stop = true;
                    projectile.ShutActive();
                }
                yield return null;
            }

        }

        caster.SetAttacking(false);

        yield return null;
    }

 




}
