using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMng : MonoBehaviour
{
    private static HandMng m_handmng;

    public static HandMng Instance
    {
        get
        {
            if (m_handmng == null)
            {

                m_handmng = GameObject.Find("Main/hand").GetComponent<HandMng>();
                m_handmng.Init();
            }

            return m_handmng;

        }
    }

    private int m_handid = 0;
    private List<BaseChar> m_allchars = new List<BaseChar>();
    private BaseChar[] m_handchars = new BaseChar[9];
    private List<BaseChar> m_fieldchars = new List<BaseChar>();

    private Node[] m_nodes;

    public List<BaseChar> FieldChars
    {
        get { return m_fieldchars; }
    }


    public Node Node(int idx)
    {
        if (idx > 8 && idx < 0)
        {
            return null;
        }

        return m_nodes[idx];

    }

    public void NodeClear()
    {
        foreach (var x in m_nodes)
        {
            x.CharEE(null);
            x.m_sprite.color = x.OriColor;
            x.Setbool(false);
        }
    }

    public bool AddChar(BaseChar ch)
    {
        int num = FindEmptyNodenum();

        if (num < 0)
            return false;


        m_allchars.Add(ch);
        m_handchars[num] = ch;


        ch.transform.position = m_nodes[num].transform.position;
        ch.transform.GetComponent<DragHelper>().m_sitnode = m_nodes[num];
        m_nodes[num].m_squadhere = true;
        ch.SetHandID(m_handid);
        m_handid++;
        FusionArticle(ch);
        return true;
    }

    public void RemoveChar(BaseChar target)
    {
        if (target.CurrNode.IsHand)
        {
            m_handchars[target.CurrNode.NodeNum] = null;
            for (int i = 0; i < m_allchars.Count; i++)
            {
                if (m_allchars[i].HandID == target.HandID)
                { m_allchars.RemoveAt(i); }
            }
            TargetClear(target);
        }
        else
        {
            for (int i = 0; i < m_fieldchars.Count; i++)
            {
                if (m_fieldchars[i].HandID == target.HandID)
                { m_fieldchars.RemoveAt(i); }
            }
            for (int i = 0; i < m_allchars.Count; i++)
            {
                if (m_allchars[i].HandID == target.HandID)
                { m_allchars.RemoveAt(i); }
            }

            TargetClear(target);
        }
    }


    public void ReturnField(BaseChar ch)
    {
        for (int i = 0; i < 9; i++)
        {
            if (m_handchars[i] == null)
                continue;

            if (ch.HandID == m_handchars[i].HandID)
            {
                m_handchars[i] = null;
            }

        }

        ch.Isonfield(true);
        m_fieldchars.Add(ch);
    }

    public void ReturnHand(BaseChar ch)
    {
        RemoveOnfiled(ch);
        m_handchars[ch.CurrNode.NodeNum] = ch;
    }


    private void TargetClear(BaseChar target)
    {
        target.transform.GetComponent<DragHelper>().m_sitnode = null;
        target.CurrNode.m_squadhere = false;
        target.CurrNode.Setbool(false);
        target.CurrNode.NodeClean();
    }

    public Node FindEmptyNode()
    {
        int num = FindEmptyNodenum();
        return Node(num);
    }

    public void Test()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(m_handchars[i]);
        }
    }

    private int FindEmptyNodenum()
    {
        for (int i = 0; i < 9; i++)
        {
            if (m_nodes[i].IsHere)
                continue;

            return i;
        }
        return -1;
    }

    public int CountHand()
    {
        int num = 0;
        for (int i = 0; i < 9; i++)
        {
            if (!m_nodes[i].IsHere)
                continue;

            num++;
        }
        return num;

    }

    public List<int> SearchFusion(int Tier, int IDX, int Star)
    {
        int num = 0;
        List<int> ids = new List<int>();

        foreach (var x in m_allchars)
        {
            if (x.Tier != Tier)
                continue;
            if (x.IDX != IDX)
                continue;
            if (x.Star != Star)
                continue;

            ids.Add(x.HandID);

            num++;
        }
 
        return ids;
    }

    public void FusionArticle(BaseChar article)
    {
        List<int> ids = SearchFusion(article.Tier, article.IDX, article.Star);

        if (article.Star > 2)
            return;

        if (ids.Count < 3)
            return;



        LinkFusionParent(article, FindHand(ids[0]));
        LinkFusionParent(FindHand(ids[0]), FindHand(ids[1]));

        for (int i = 0; i < 2; i++)
        {
            BaseChar temp = FindHand(ids[i]);
            RemoveChar(temp);
            TailMng.Instance.TailGo(temp.transform.position, article.transform.position, () => FusionFx(article));
            temp.KillThis("fusion");
        }

        article.Fusion(true);

        FusionArticle(article);

    }

    private void LinkFusionParent(BaseChar child, BaseChar parent)
    {
        if (child.FusionParent != null)
        {
            LinkFusionParent(EndLink(parent),child.FusionParent);
        }
        child.SetFusionParent(parent);
    }

    private BaseChar EndLink(BaseChar article)
    {
        BaseChar upstream = article;

        if (upstream != null)
        {
            if (article.FusionParent != null)
            {
                upstream = article.FusionParent;
                upstream = EndLink(upstream);
            }

        }
        return upstream;
    }


    private void FusionFx(BaseChar article)
    {
        PixelFx fx;
        fx = FxMng.Instance.FxCall("Summon");
        fx.transform.position = article.transform.position + new Vector3(0, 0, 0);
        fx.gameObject.SetActive(true);
    }

    public void RemoveOnfiled(BaseChar ch)
    {
        for (int i = 0; i < m_fieldchars.Count; i++)
        {
            if (m_fieldchars[i].HandID == ch.HandID)
            {
                m_fieldchars[i].Isonfield(false);
                m_fieldchars.RemoveAt(i);

            }
        }

    }



    public BaseChar FindHand(int id)
    {

        foreach (var x in m_allchars)
        {
            if (x.HandID == id)
                return x;
        }

        return null;
    }


    private void Init()
    {
        m_nodes = GetComponentsInChildren<Node>(true);
        int idx = 0;
        foreach (var x in m_nodes)
        {
            x.SetNodeNum(idx);
            idx++;
            x.HandNode();

        }
    }


}
