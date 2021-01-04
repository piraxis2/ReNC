using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPenetrate : Skill
{

    private float m_angle;

    public override void Init(FxMng fx)
    {
        base.Init(fx);
    }


    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        m_angle = 0;
        List<Node> skillrange = new List<Node>();
        int x, y;
        x = target.Row - caster.CurrNode.Row;
        y = target.Col - caster.CurrNode.Col;

        if (x + y == 0)
        {
            if (x > 0)
            {
                { /*south,east*/}
                m_angle = 50;
                for (int i = 0; i < 4; i++)
                {
                    skillrange.Add(CheckNode(caster.CurrNode.Row + i + 1, caster.CurrNode.Col - i - 1, nodearr));
                    if (skillrange[i] == null)
                    {
                        skillrange.RemoveAt(i);
                        return skillrange;
                    }
                }
                return skillrange;

            }
            else if (x < 0)
            {
                { /*north,west*/}
                m_angle = -130;
                for (int i = 0; i < 4; i++)
                {
                    skillrange.Add(CheckNode(caster.CurrNode.Row - i - 1, caster.CurrNode.Col + i + 1, nodearr));
                    if (skillrange[i] == null)
                    {
                        skillrange.RemoveAt(i);
                        return skillrange;
                    }
                }
                return skillrange;

            }
        }
        else if (x - y == 0)
        {
            if (x > 0)
            {
                { /*north,east*/}
                m_angle = -220;
                for (int i = 0; i < 4; i++)
                {
                    skillrange.Add(CheckNode(caster.CurrNode.Row + i + 1, caster.CurrNode.Col + i + 1, nodearr));
                    if (skillrange[i] == null)
                    {
                        skillrange.RemoveAt(i);
                        return skillrange;
                    }
                }
                return skillrange;
            }
            else if (x < 0)
            {
                { /*south,west*/}
                m_angle = -40;
                for (int i = 0; i < 4; i++)
                {
                    skillrange.Add(CheckNode(caster.CurrNode.Row - i - 1, caster.CurrNode.Col - i - 1, nodearr));
                    if (skillrange[i] == null)
                    {
                        skillrange.RemoveAt(i);
                        return skillrange;
                    }
                }
                return skillrange;
            }
        }
        switch (x)
        {
            case 0:
                if (y > 0)
                {
                    {/*north*/ }
                    m_angle = 180;
                    for (int i = 0; i < 4; i++)
                    {
                        skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col + i + 1, nodearr));
                        if (skillrange[i] == null)
                        {
                            skillrange.RemoveAt(i);
                            return skillrange;
                        }
                    }

                    return skillrange;
                }
                if (y < 0)
                {
                    { /*south*/}
                    m_angle = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col - i - 1, nodearr));
                        if (skillrange[i] == null)
                        {
                            skillrange.RemoveAt(i);
                            return skillrange;
                        }
                    }
                    return skillrange;
                }
                break;
            case 1:
                if (y == 0)
                    break;

                if (x + y > 0)
                {
                    m_angle = 150;

                    skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col + 2, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col + 3, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 2, caster.CurrNode.Col + 4, nodearr));
                    return skillrange;
                }
                if (x + y < 0)
                {
                    m_angle = 35;
                    skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col - 2, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col - 3, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 2, caster.CurrNode.Col - 4, nodearr));
                    return skillrange;
                }
                break;
            case -1:
                if (y == 0)
                    break;

                if (x + y > 0)
                {
                    m_angle = 215;
                    skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col + 2, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col + 3, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 2, caster.CurrNode.Col + 4, nodearr));
                    return skillrange;
                }
                if (x + y < 0)
                {
                    m_angle = -30;
                    skillrange.Add(CheckNode(caster.CurrNode.Row, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col - 2, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col - 3, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 2, caster.CurrNode.Col - 4, nodearr));
                    return skillrange;
                }
                break;

        }
        switch (y)
        {
            case 0:
                if (x > 0)
                {
                    { /*east*/}
                    m_angle = 100;
                    for (int i = 0; i < 4; i++)
                    {
                        skillrange.Add(CheckNode(caster.CurrNode.Row + i + 1, caster.CurrNode.Col, nodearr));
                        if (skillrange[i] == null)
                        {
                            skillrange.RemoveAt(i);
                            return skillrange;
                        }
                    }
                    return skillrange;
                }
                if (x < 0)
                {
                    { /*west*/}
                    m_angle = -80;
                    for (int i = 0; i < 4; i++)
                    {
                        skillrange.Add(CheckNode(caster.CurrNode.Row - i - 1, caster.CurrNode.Col, nodearr));
                        if (skillrange[i] == null)
                        {
                            skillrange.RemoveAt(i);
                            return skillrange;
                        }

                    }
                    return skillrange;
                }
                break;
            case 1:
                if (x + y > 0)
                {
                    m_angle = 130;
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 2, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 3, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 4, caster.CurrNode.Col + 2, nodearr));
                    return skillrange;
                }
                if (x + y < 0)
                {
                    m_angle = -120;
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 2, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 3, caster.CurrNode.Col + 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 4, caster.CurrNode.Col + 2, nodearr));
                    return skillrange;
                }
                break;
            case -1:
                if (x + y > 0)
                {
                    m_angle = 60;
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 1, caster.CurrNode.Col, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 2, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 3, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row + 4, caster.CurrNode.Col - 2, nodearr));
                    return skillrange;
                }
                if (x + y < 0)
                {
                    m_angle = -50;
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 1, caster.CurrNode.Col, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 2, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 3, caster.CurrNode.Col - 1, nodearr));
                    skillrange.Add(CheckNode(caster.CurrNode.Row - 4, caster.CurrNode.Col - 2, nodearr));
                    return skillrange;
                }
                break;
        }


        return skillrange;
    }

    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {
        //for (int i = 0; i < skillrange.Count; i++)
        //{
        //    if (skillrange[i] == null)
        //        continue;

        //    if (skillrange[i].WarningTile.m_Isrunning)
        //        continue;

        //    skillrange[i].WarningTile.gameObject.SetActive(true);
        //    skillrange[i].WarningTile.StartWarning();
        //}
        yield return new WaitForSeconds(0.5f);

        PixelFx shot = m_skillmng.FxCall("Shot");
        shot.gameObject.SetActive(true);
        shot.transform.position = skillrange[0].transform.position + new Vector3(0.1f, 0.5f, 0);
        shot.transform.rotation = Quaternion.Euler(45, 45, m_angle-110);



        foreach (var x in skillrange)
        {
            if (x == null)
                continue;

            if (x.CurrCHAR != null)
            {
                if (x.CurrCHAR.FOE != caster.FOE)
                    continue;
            }

            if (x.CurrCHAR == null)
                continue;

            if (x.CurrCHAR.FOE == caster.FOE)
                x.CurrCHAR.MyStatus.DamagedLife(50 + caster.MyStatus.AP, caster, x, DamageType.Skill);
        }


        caster.SetAttacking(false);



    }

    public Node CheckNode(int row, int col, Node[,] nodes)
    {
        if ((row > 7 || row < 0 || col > 7 || col < 0))
            return null;

        return nodes[row, col];

    }
}
