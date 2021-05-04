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
        int row = target.Row;
        int col = target.Col;

        switch (dir)
        {
            case DIR.WEST: x = 1; y = -1; wall = 0; count = col; vec = -1; break;
            case DIR.NORTH: x = -1;y = -1; wall = 0; count = row; vec = -1; break;
            case DIR.SOUTH: x = 1; y = 1; wall = 7;  count = row; vec = 1; break;
            case DIR.EAST: x = -1; y = 1; wall = 7; count = col; vec = 1; break;
        }
    

        row = row + x; 
        col = col + y;

        if (row < 0 || row > 7 || col < 0 || col > 7)
        {

        }
        else
        {
            while (touchwall(wall,count))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (dir == DIR.EAST || dir == DIR.WEST)
                    {
                        if ((col - 1 + i) >= 0 && (col - 1 + i) <= 7)
                            range.Add(nodearr[count, col - 1 + i]);
                    }
                    else if (dir == DIR.NORTH || dir == DIR.SOUTH)
                    {
                        if ((row - 1 + i) >= 0 && (row - 1 + i) <= 7)
                            range.Add(nodearr[row - 1 + i, count]);
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
            if (count >= wall)
            {
                return false;
            }
        }
        else if( wall == 0)
        {
            if(count<=wall)
            {
                return false;
            }
        }
        return true;
    }


    public override IEnumerator IESkillaction(List<Node> skillrange, BaseChar caster)
    {


        yield return null;
    }

}
