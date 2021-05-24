using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

                    foreach (var xz in temp)
                    {
                        if (xz != null)
                            xz.m_sprite.color = Color.blue;
                    }

                    onoff = true;
                }
            }
            else
            {
                if(temp.Count>0)
                {
                    foreach(var x in temp)
                    {
                        if (x != null)
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
        int row = caster.Row;
        int col = caster.Col;

        switch (dir)
        {
            case DIR.WEST: x = -1; y = -1; wall = 0; count = col-1; vec = -1; break;
            case DIR.NORTH: x = -1; y = -1; wall = 0; count = row-1; vec = -1; break;
            case DIR.SOUTH: x = 1; y = -1; wall = 7; count = row+1; vec = 1; break;
            case DIR.EAST: x = -1; y = 1; wall = 7; count = col+1; vec = 1; break;
        }


        row = row + x;
        col = col + y;
        Debug.Log(row.ToString()+ col.ToString());

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


    public void Testshot(Node caster, Node target)
    {
        List<Node> skillrange = DebuGG(caster, target);

        if (skillrange.Count == 0)
        {
            return;
        }


        StartCoroutine(IESkillaction(skillrange, caster));

    }
    public IEnumerator IESkillaction(List<Node> skillrange, Node caster)
    {

        int idxnum = skillrange.Count / 3;
        int idx = 0; 

        for (int i = 0; i < idxnum; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (skillrange[idx] != null)
                    skillrange[idx].m_sprite.color = Color.green;

                idx++;
            }
            yield return new WaitForSeconds(0.15f);
        }


        yield return null;
    }


    private bool touchwall(int wall, int count)
    {
        if (wall == 7)
        {
            if (count > wall)
            {
                return false;
            }
        }
        else if (wall == 0)
        {
            if (count < wall)
            {
                return false;
            }
        }
        return true;
    }


    public void Button1()
    {

        if (m_caster == null)
            return;

        if (m_target == null)
            return;
        Testshot(m_caster, m_target);
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

