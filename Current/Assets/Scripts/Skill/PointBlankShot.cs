using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBlankShot : Skill 
{

    PixelFx m_fx;
    PixelFx m_fx2;

    public override void Init(FxMng fx)
    {

        base.Init(fx);
        m_damage[0] = 225;
        m_damage[1] = 350;
        m_damage[2] = 550;
        m_fx = FxMng.Instance.FxCall("Boom");
    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {

        List<Node> range = new List<Node>();
        range.Add(target);

        return range;
    }


    private Node RearNode(BaseChar caster ,Node target)
    {
        if (target == null)
            return null;

        int castercol = caster.CurrNode.Col;
        int casterrow = caster.CurrNode.Row;
        int tcol = target.Col;
        int trow = target.Row;

        int acol = tcol - castercol;
        int arow = trow - casterrow;

        acol = tcol + acol;
        arow = trow + arow;

        if (arow < 0 || arow > 7 || acol < 0 || acol > 7)
            return null;


        return NodeMng.instance.NodeArr[arow, acol];


    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {

        m_fx.gameObject.SetActive(true);
        m_fx.transform.position = skillrange[0].transform.position;

        BaseChar target = skillrange[0].CurrCHAR;
        if (skillrange[0].CurrCHAR == null)
        {
            caster.SetAttacking(false);
            yield break;
        }

        if (target.FOE != caster.FOE)
        {
            target.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[0], DamageType.Skill);
            target.SetAttacking(false);

            bool stop = false;
            float elapsedtime = 0;


            Node rear = RearNode(caster, skillrange[0]);

            if (rear == null)
            {
                target.MyStatus.GetBuff("Stun", caster.Star + 1);
            }
            else
            {
                if (rear.CurrCHAR != null)
                {
                    if (rear.CurrCHAR.FOE != caster.FOE)
                    {
                        rear.CurrCHAR.MyStatus.GetBuff("Stun", caster.Star + 1);
                        rear.CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[0], DamageType.Skill);
                    }
                    target.MyStatus.GetBuff("Stun", caster.Star + 1);
                   
                }
                else
                {
                    target.MyStatus.GetBuff("Stun", 0.5f);
                    target.SetRunning(true);
                    rear.Setbool(true);
                    Node prevenode = target.CurrNode;
                    Vector3 pos = target.transform.position;

                    while (!stop)
                    {
                        elapsedtime += Time.deltaTime * 8;
                        target.transform.position = Vector3.Lerp(pos, rear.transform.position, elapsedtime);


                        if(target.Dying)
                        {
                            prevenode.Setbool(false);
                            rear.Setbool(false);
                            target.SetRunning(false);
                            caster.SetAttacking(false);
                            yield break;
                        }


                        if(elapsedtime>=1)
                        {
                            prevenode.Setbool(false);
                            target.SetRunning(false);
                            stop = true;
                        }


                        yield return null;

                    }
                }
            }

            
            



        }



        caster.SetAttacking(false);

        yield return null;
    }



}
