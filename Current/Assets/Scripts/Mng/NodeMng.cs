using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMng : Mng 
{



    private const int m_arrsize = 8;
    private bool m_over64 = false;

    private Node[,] m_nodearr = new Node[m_arrsize, m_arrsize];
    public Node[] m_subnode;

    public Node[,] NodeArr
    {
        get { return m_nodearr; }
    }

    private static NodeMng s_nodemng;
    public static NodeMng instance
    {
        get
        {
            if (s_nodemng == null)
                s_nodemng = FindObjectOfType<NodeMng>();

            return s_nodemng;
        }
    }
    


    public override void Init()
    {
        Node[] tempnode = GetComponentsInChildren<Node>(true);
        int count = 0;
        for (int i = 0; i < m_arrsize; i++)
        {
            for (int j = 0; j < m_arrsize; j++)
            {
                m_nodearr[i, j] = tempnode[count];
                m_nodearr[i, j].Set(i, j);
                count++;
            }
        }
        
        if (tempnode.Length > 64)
        {
            m_over64 = true;
            m_subnode[0] = tempnode[64];
            m_subnode[1] = tempnode[65];
            m_subnode[0].Set(100, 0);
            m_subnode[1].Set(100, 1);
        }
    }

    public void NodeClear()
    {
        foreach(var x in m_nodearr)
        {
            x.CharEE(null);
            x.Setbool(false);
        }

        if (m_over64)
        {
            for (int i = 0; i < 2; i++)
            {
                m_subnode[i].m_sprite.color = m_subnode[i].OriColor;
            }
        }

    }

    public void NodeClear(BaseChar ch)
    {
        foreach (var x in m_nodearr)
        {
            x.CharEE(null);
            x.Setbool(false);
            if (ch.CurrNode!=x)
            {
                x.m_sprite.color = x.OriColor;
            }

        }

    }





    public Node[] Neighbours(Node node)
    {

        List<Node> neighbours = new List<Node>();

        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if ((row == 0 && col == 0) /*CrossMove*/|| (row == 1 && col == -1) || (row == -1 && col == -1) ||
                    (row == 1 && col == 1) || (row == -1 && col == 1))
                {
                    continue;
                }

                if (CheckNode(node.Row + row, node.Col + col))
                {
                    neighbours.Add(m_nodearr[node.Row + row, node.Col + col]);
                }
            }
        }

       

        return neighbours.ToArray();
    }


    private bool CheckNode(int row, int col)
    {

        if (row < 0 || row >= m_arrsize)
            return false;
        if (col < 0 || col >= m_arrsize)
            return false;

        return true;
    }


    public Node FindNode(Vector3 pos)
    {
        for (int row = 0; row < m_arrsize; row++)
        {
            for (int col = 0; col < m_arrsize; col++)
            {
                RectTransform r = m_nodearr[row, col].GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(r, pos))
                {
                    return m_nodearr[row, col];
                }
            }
        }
        return null;
    }

}
