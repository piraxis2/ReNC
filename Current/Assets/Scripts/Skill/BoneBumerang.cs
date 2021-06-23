using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBumerang : Skill 
{

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
        PixelFx fx = FxMng.Instance.FxCall("BoneBumerang");
        fx.gameObject.SetActive(true);
        Shuriken shu = fx.GetComponent<Shuriken>();

        shu.BoneStart(caster);


        Vector3 oripos = caster.transform.position;
        Vector3 targetpos = MathHelper.AngleDistance(oripos, MathHelper.GetAngle(oripos, skillrange[0].transform.position),10);

        for (int i = 0; i < 2; i++)
        {
            while (!stop)
            {
                elapsedtime += Time.deltaTime;

                fx.transform.position = Vector3.Lerp(oripos, targetpos, elapsedtime);


                if (elapsedtime >= 1)
                {
                    Vector3 temp = oripos;
                    oripos = targetpos;
                    targetpos = temp;
                    elapsedtime = 0;
                    stop = true;

                    if(i>=1)
                    {

                        shu.BoneEnd();
                    }
                }


                yield return null;
            }
            stop = false;

        }

        caster.SetAttacking(false);

        yield return null;
    }

}
