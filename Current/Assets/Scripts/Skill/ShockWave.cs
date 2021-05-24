using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : Skill
{
    PixelFx m_fx;
    public override void Init(FxMng fx)
    {
        base.Init(fx);

        m_damage[0] = 450;
        m_damage[1] = 675;
        m_damage[2] = 1500;

    }

    public override List<Node> SkillRange(Node[,] nodearr, Node target, BaseChar caster)
    {
        DIR dir = CharActionMng.Direction(caster.CurrNode, target);

        List<Node> range = new List<Node>();

        int x = 0;
        int y = 0;
        int wall = 0;
        int count = 0;
        int vec = 0;
        int row = caster.CurrNode.Row;
        int col = caster.CurrNode.Col;

        switch (dir)
        {
            case DIR.WEST: x = -1; y = -1; wall = 0; count = col - 1; vec = -1; break;
            case DIR.NORTH: x = -1; y = -1; wall = 0; count = row - 1; vec = -1; break;
            case DIR.SOUTH: x = 1; y = -1; wall = 7; count = row + 1; vec = 1; break;
            case DIR.EAST: x = -1; y = 1; wall = 7; count = col + 1; vec = 1; break;
        }


        row = row + x;
        col = col + y;

        if (row < 0 || row > 7 || col < 0 || col > 7)
        {

        }
        else
        {
            while (touchwall(wall, count))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (dir == DIR.EAST || dir == DIR.WEST)
                    {
                        if ((row + i) >= 0 && (row + i) <= 7)
                            range.Add(nodearr[row + i, count]);
                        else
                            range.Add(null);
                    }
                    else if (dir == DIR.NORTH || dir == DIR.SOUTH)
                    {
                        if ((col + i) >= 0 && (col + i) <= 7)
                            range.Add(nodearr[count, col + i]);
                        else
                            range.Add(null);
                    }

                }
                count += vec;
            }
        }

        return range;

    }


    private bool touchwall(int wall, int count)
    {
        if(wall == 7)
        {
            if (count > wall)
            {
                return false;
            }
        }
        else if( wall == 0)
        {
            if (count < wall)
            {
                return false;
            }
        }
        return true;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {


        int idxnum = skillrange.Count / 3;
        int idx = 0;

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < idxnum; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                PixelFx fx = FxMng.Instance.FxCall("Spike");
                if (skillrange.Count <= idx)
                    break;

                if (skillrange[idx] != null)
                {
                    fx.gameObject.SetActive(true);
                    fx.transform.position = skillrange[idx].transform.position;
                }



                if (skillrange[idx] == null)
                {
                    idx++;
                    continue;
                }

                if (skillrange[idx].CurrCHAR == null)
                {
                    idx++;
                    continue;
                }

                if (skillrange[idx].CurrCHAR.FOE != caster.FOE)
                {
                    skillrange[idx].CurrCHAR.MyStatus.DamagedLife(m_damage[caster.Star - 1], caster, skillrange[idx], DamageType.Skill);
                    Debug.Log(skillrange[idx].CurrCHAR);
                }
                idx++;

            }
            yield return new WaitForSeconds(0.1f);
        }


        caster.SetAttacking(false);

        yield return null;
    }

}
