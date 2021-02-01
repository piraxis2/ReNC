using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : Skill 
{


    public override void Init(FxMng fx)
    {
        base.Init(fx);
        m_damage[0] = 300;
        m_damage[1] = 400;
        m_damage[2] = 550;
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> range = new List<Node>();

        range.Add(target);

        return range;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        bool stop = false;
        float elapsedtime = 0;
        PixelFx fx = FxMng.Instance.FxCall("TNT");
        PixelFx Bomb = FxMng.Instance.FxCall("MushroomCloud");
        BaseChar target = skillrange[0].CurrCHAR;
        Vector3 start = caster.transform.position;
        Vector3 end = target.transform.position;
        fx.gameObject.SetActive(true);

        while(!stop)
        {
            elapsedtime += Time.deltaTime * 4;
            fx.transform.position = MathHelper.BezierCurve(start, new Vector3(start.x, start.y + 3, start.z), new Vector3(end.x, end.y + 3, end.z), end, elapsedtime);

            if (elapsedtime >=1)
            {
                stop = true;
                fx.gameObject.SetActive(false);
                Bomb.transform.position = target.transform.position;
                Bomb.gameObject.SetActive(true);
                target.MyStatus.GetBuff("Stun", 2f);
                target.MyStatus.DamagedLife(m_damage[caster.Star - 1] * (caster.MyStatus.AP / 100), caster, target.CurrNode, DamageType.Skill);
            }

            yield return null;
        }



        caster.SetAttacking(false);


        yield return null;
    }




}
