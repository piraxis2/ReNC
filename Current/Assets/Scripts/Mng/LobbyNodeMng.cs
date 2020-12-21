using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNodeMng : Mng 
{
    private const int m_arrsize = 8;

    
    private static Node[,] m_nodearr = new Node[m_arrsize, m_arrsize];
    private static Node[] m_subnode = new Node[2];



    public static Node[,] NodeArr
    {
        get { return m_nodearr; }
    }

    public static Node[] SubNode
    {
        get { return m_subnode; }
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
        m_subnode[0] = tempnode[64];
        m_subnode[1] = tempnode[65];
    }
    public static int Herocount()
    {
        int count = 0;
        foreach (var x in NodeArr)
        {
            if (x.m_squadhere)
            { count++; }
        }
        return count;
    }
    public static Node Subcount()
    {

        for (int i = 0; i < 2; i++)
        {
            if (!m_subnode[i].m_squadhere)
                return m_subnode[i];
        }
        return null;
    }

}
