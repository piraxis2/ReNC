using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article
{

    private int m_tier;
    private int m_idx;
    private List<BaseChar> m_articlelist = new List<BaseChar>();

    private int[] m_countlist = { 29, 22, 18, 12, 10 };


    public List<BaseChar> ArticleList
    {
        get { return m_articlelist; }
    }

    public void InitArticle(Transform transform,int Tier, int idx)
    {
        m_idx = idx;
        m_tier = Tier;
        m_articlelist.AddRange(transform.GetComponentsInChildren<BaseChar>(true));
        foreach(var x in m_articlelist)
        {
            x.Init();
           // x.SetColor(TierColor.GetColor(m_tier));
            x.SetTier(m_tier);
            x.SetIDX(m_idx);
        }
    }

    public BaseChar ArticleLoad()
    {

        foreach(var x in ArticleList)
        {
            if (x.gameObject.activeInHierarchy)
                continue;

            if (x.IsExclude)
                continue;

            return x;
        }

        return null;
    }

}
