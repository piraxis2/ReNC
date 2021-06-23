using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    None, Wood, Rock
}

public class Node : MonoBehaviour
{
    [SerializeField]
    private NodeType m_nodeType = NodeType.None;

    private int m_row;
    private int m_col;
    private Node m_parent;
    [SerializeField]
    private BaseChar m_chara = null;
    [SerializeField]
    private WarningTile[] m_warningtile;

    private int m_Hcost;
    private int m_Gcost;
    private int m_nodenum;
    private Color m_oriColor;
    private Trap m_trap;

    [SerializeField]
    private bool m_ishere;
    private bool m_ishand;
    [SerializeField]
    public bool m_squadhere;


    public SpriteRenderer m_sprite;
    

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>(); 
        m_warningtile = GetComponentsInChildren<WarningTile>(true);
        m_oriColor = m_sprite.color;
        m_trap = GetComponentInChildren<Trap>(true);

        if (NodeType == NodeType.Rock)
            m_sprite.color = Color.black;
        else if( NodeType == NodeType.Wood)
            m_sprite.color = Color.green;
    }

    public void HandNode()
    {
        
        m_ishand = true;    
    }

    public NodeType NodeType
    {
        get { return m_nodeType; }
    }
 
    public BaseChar CurrCHAR
    {
        get { return m_chara; }
    }

    public WarningTile WarningTile
    {
        get { return m_warningtile[0]; }
    }

    public WarningTile PositiveWarningTile
    {
        get { return m_warningtile[1]; }
    }


    public Node Parent
    {
    get { return m_parent; }
    }

    public bool IsHere
    {
        get { return m_ishere; }
    }

    public bool IsHand
    {
        get { return m_ishand; }
    }

    public int NodeNum
    {
        get { return m_nodenum; }
    }

    public int Row
    {
        get { return m_row; }
    }

    public int Col
    {
        get { return m_col; }
    }
  
    public int FCost
    {
        get { return m_Hcost + m_Gcost; }
    }

    public int HCost
    {
        get { return m_Hcost; }
    }

    public int GCost
    {
        get { return m_Gcost; }
    }
    
    public Color OriColor
    {
        get { return m_oriColor; }
    }

    public void SetNodeType(NodeType nodeType)
    {
        m_nodeType = nodeType;
    }

   
    public void NodeClean()
    {
        m_chara = null;
    }

    public void Setparent(Node mom)
    {
        m_parent = mom;
    }

    public void Setbool(bool cani)
    {
        m_ishere = cani;
    }

    public void SetHCost(int cost)
    {
        m_Hcost = cost;
    }

    public void SetGCost(int cost)
    {
        m_Gcost = cost;
    }

    public void SetNodeNum(int val)
    {
        m_nodenum = val;
    }
    public void ResetNode()
    {
        m_parent = null;
    }

    public void Set(int row, int col)
    {
        m_row = row;
        m_col = col;
    }

    public void CharEE(BaseChar onchar)//Enter,Exit
    {
        m_chara = onchar;
        if (m_trap != null)
        {
            if(onchar==null)
            {
                return;
            }
            if (onchar.FOE)
            {
                m_trap.ActiveTrap(onchar);
            }
        }
    }
}
