using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair
{
    public Node p_right;
    public Node p_left;
    public Pair(Node x, Node y)
    {
        p_right = y;
        p_left = x;
    }
}

public class PathFind : Mng
{
    private NodeMng m_nodeMng;
    private List<Node> m_openlist = new List<Node>();
    private List<Node> m_closelist = new List<Node>();
       private List<Pair> m_orderList = new List<Pair>();
    private NodeComparer nodeComparer = new NodeComparer();


    private Node m_currnode;
    private Node m_passenger;
    private Node m_target;





    public override void Init()
    {
        m_nodeMng = GameObject.FindObjectOfType<NodeMng>();
    }

    public int GetDistance(Node a, Node b)
    {

        int x = Mathf.Abs(a.Col - b.Col);
        int y = Mathf.Abs(a.Row - b.Row);

        return x + y;
           // 14 * Mathf.Min(x, y) + 10 * Mathf.Abs(x - y);
    }

    public void PathReady(Node passenger,Node target)
    {
        if (passenger == null || target == null)
            return;
        

        m_openlist.Clear();
        m_closelist.Clear();
        m_passenger = passenger;
        m_target = target;
        m_passenger.ResetNode();
        m_target.ResetNode();
        m_currnode = m_passenger;

        m_passenger.SetGCost(0);
        m_passenger.SetHCost(GetDistance(m_passenger,m_target));
    }

    public List<Node> Pathfind()
    {
        List<Node> completepath = new List<Node>();
        do
        {
            Node[] neighbours = m_nodeMng.Neighbours(m_currnode);

            for (int i = 0; i < neighbours.Length; i++)
            {
                if (m_closelist.Contains(neighbours[i]))
                    continue;

                if (neighbours[i].NodeType == NodeType.Rock || neighbours[i].NodeType == NodeType.Wood)
                    continue;

                if (neighbours[i] != m_target && neighbours[i].IsHere)
                    continue;

                int gCost = m_currnode.GCost + GetDistance(neighbours[i], m_currnode);

                if (!m_openlist.Contains(neighbours[i]) || gCost < neighbours[i].GCost)
                {
                    neighbours[i].SetGCost(gCost);
                    neighbours[i].SetHCost(GetDistance(neighbours[i], m_target));
                    neighbours[i].Setparent(m_currnode);
                    if (!m_openlist.Contains(neighbours[i]))
                    {
                        m_openlist.Add(neighbours[i]);
                    }
                }
            }
            m_closelist.Add(m_currnode);

            if (m_openlist.Contains(m_currnode))
                m_openlist.Remove(m_currnode);

            if (m_openlist.Count > 0)
            {
                m_openlist.Sort(nodeComparer);
                if (m_currnode != null)
                {

                }
                m_currnode = m_openlist[0];
            }

            if (m_currnode == m_target)
            {
                completepath = Completepath(m_currnode, m_passenger);
                completepath.Reverse();

            }
        } while (m_openlist.Count > 0);
        return completepath;

    }




    public List<Node> Completepath(Node currnode, Node passengernode)
    {
        List<Node> path = new List<Node>();
        Node upstream = currnode;
        path.Add(upstream);
        while(upstream!=passengernode)
        {
            upstream = upstream.Parent;
            path.Add(upstream);
        }
        return path;

    }
    public List<Node> FindPath(Node start, Node end)
    {
        PathReady(start, end);

        return Pathfind();
    }


}
