using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRange : MonoBehaviour
{


    Node m_caster;
    Node m_target;
    int count = 0;
    bool onoff = false;

    List<Node> temp = new List<Node>();
    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Node node = NRay();
            if (node != null)
            {
                if (count > 1)
                {
                    m_caster.m_sprite.color = m_caster.OriColor;
                    m_target.m_sprite.color = m_target.OriColor;
                    m_caster = null;
                    m_target = null;
                    count = 0;
                }
                else
                {
                    node.m_sprite.color = Color.red;
                    switch (count)
                    {
                        case 0: count++; m_caster = node; break;
                        case 1: count++; m_target = node; break;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (m_caster != null) m_caster.m_sprite.color = m_caster.OriColor;
            if (m_target != null) m_target.m_sprite.color = m_target.OriColor;
            m_caster = null;
            m_target = null;
            count = 0;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            if (!onoff)
            {
                if (m_caster != null && m_target != null)
                {
                    temp = DebuGG(m_caster, m_target);
                    onoff = true;
                }
            }
            else
            {
                if(temp.Count>0)
                {
                    foreach(var x in temp)
                    {
                        x.m_sprite.color = x.OriColor;
                    }
                }
                temp = new List<Node>();
                onoff = false;

            }

        }

    }

    private List<Node> DebuGG(Node caster,Node target)
    {
      Node[,] nodearr = NodeMng.instance.NodeArr;

        DIR dir = CharActionMng.Direction(caster, target);

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
            case DIR.NORTH: x = -1; y = -1; wall = 0; count = row; vec = -1; break;
            case DIR.SOUTH: x = 1; y = 1; wall = 7; count = row; vec = 1; break;
            case DIR.EAST: x = -1; y = 1; wall = 7; count = col; vec = 1; break;
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

        foreach(var xz in range)
        {
            xz.m_sprite.color = Color.blue;
        }
        return range;

    }

    private bool touchwall(int wall, int count)
    {
        if (wall == 7)
        {
            if (count >= wall)
            {
                return false;
            }
        }
        else if (wall == 0)
        {
            if (count <= wall)
            {
                return false;
            }
        }
        return true;
    }


    private Node NRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            Node se = hit.transform.GetComponent<Node>();


            if (se != null)
            {
                if (se.m_squadhere)
                    return null;

                return se;
            }
        }
        return null;
    }
}

