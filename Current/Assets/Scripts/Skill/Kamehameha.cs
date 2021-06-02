using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Kamehameha : Skill 
{



    PixelFx m_fx;
    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 250;
        m_damage[1] = 675;
        m_damage[2] = 1500;

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        List<Node> range = new List<Node>();


        int row = caster.CurrNode.Row;
        int col = caster.CurrNode.Col;

        row = Math.Abs(target.Row) - Math.Abs(row);
        col = Math.Abs(target.Col) - Math.Abs(col);

        range.Add(target);

        if (Math.Abs(row + col) == 1)
        {
            if (row == -1)
            {
                if (target.Row - 1 >= 0)
                {
                    range.Add(nodearr[target.Row - 1, target.Col]);
                }
                else
                {
                    range.Add(null);
                }

            }
            else if (row == 1)
            {
                if (target.Row + 1 <= 7)
                {
                    range.Add(nodearr[target.Row + 1, target.Col]);
                }
                else
                {
                    range.Add(null);
                }

            }
            else if (col == -1)
            {
                if (target.Col - 1 >= 0)
                {
                    range.Add(nodearr[target.Row, target.Col - 1]);
                }
                else
                {
                    range.Add(null);
                }

            }
            else if (col == 1)
            {
                if (target.Col + 1 <= 7)
                {
                    range.Add(nodearr[target.Row, target.Col + 1]);
                }
                else
                {
                    range.Add(null);
                }
            }
        }

        return range;
    }


    private float Dir(Node target,Node caster)
    {



        return 0;
    }
  
    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {


        bool stop = false;
        float elapsedtime = 0;
        float dotdamgecount = 0;
        PixelFx fx = FxMng.Instance.FxCall("Kamehameha");
        int damage = m_damage[caster.Star - 1];
        fx.gameObject.SetActive(true);
        float angle = 0;
        int x = skillrange[0].Row - caster.CurrNode.Row;
        int y = skillrange[0].Col - caster.CurrNode.Col;


        switch (x)
        {

            case -1:
                if (y == -1)
                {
                    fx.transform.position = caster.transform.GetChild(2).position;
                    angle = 90;
                }
                else if (y == 0)
                {
                    fx.transform.position = caster.transform.GetChild(3).position;
                    angle = 45;
                }
                else if (y == 1)
                {
                    fx.transform.position = caster.transform.GetChild(4).position;
                    angle = 360;
                }
                break;
            case 0:
                if (y == -1)
                {
                    fx.transform.position = caster.transform.GetChild(5).position;
                    angle = 315;
                }
                else if (y == 1)
                {
                    fx.transform.position = caster.transform.GetChild(6).position;
                    angle = 270;
                }
                break;
            case 1:
                if (y == -1)
                {
                    fx.transform.position = caster.transform.GetChild(7).position;
                    angle = 225;
                }
                else if (y == 0)
                {
                    fx.transform.position = caster.transform.GetChild(8).position;
                    angle = 180;
                }
                else if (y == 1)
                {
                    fx.transform.position = caster.transform.GetChild(9).position;
                    angle = 135;
                }
                break;

        }

        fx.transform.rotation = Quaternion.Euler(45, 45, angle);


        if (skillrange.Count < 2)
        { 
            damage = (int)(damage * 1.5f);
        }

        while (!stop)
        {

            elapsedtime += Time.deltaTime;
            if (elapsedtime >= dotdamgecount)
            {
                foreach (var z  in skillrange)
                {
                    if (z.CurrCHAR != null)
                    {
                        if (caster.FOE != z.CurrCHAR.FOE)
                            z.CurrCHAR.MyStatus.DamagedLife(damage / 3, null, z, DamageType.Skill);
                    }
                }
                dotdamgecount += 0.333f;

            }

            if (elapsedtime >= 1)
            {
                stop = true;
                fx.gameObject.SetActive(false);

            }

            yield return null;


        }

        caster.SetAttacking(false);

        yield return null;
    }


}
