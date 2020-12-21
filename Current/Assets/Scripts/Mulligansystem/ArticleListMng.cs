using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleListMng : MonoBehaviour
{
    private int[] m_tier = { 9, 9, 9, 6, 4 };
    public Color[] m_color = new Color[5];

    private static ArticleListMng m_alticlelistmng;
    public static ArticleListMng Instance
    {
        get
        {
            if (m_alticlelistmng == null)
            {
                m_alticlelistmng = GameObject.Find("Main/ArticleList").GetComponent<ArticleListMng>();
                m_alticlelistmng.Init();
            }

            return m_alticlelistmng;
        }
    }

    private List<Article[]> m_articles = new List<Article[]>();

    public Article Articles(int tier, int idx)
    {
        return m_articles[tier][idx];
    }
    
    private void Init()
    {
        m_color[0] = new Color(1, 1, 1);//white
        m_color[1] = new Color(0.3f, 1, 0.4f);//green
        m_color[2] = new Color(0.4f, 0.4f, 1);//blue
        m_color[3] = new Color(0.8f, 0.3f, 1);//puple
        m_color[4] = new Color(1, 0.86f, 0.4f);//gold

        for (int i = 0; i < 5; i++)
        {
            Article[] articles = new Article[m_tier[i]];

            for (int j = 0; j < m_tier[i]; j++)
            {
                Article Newticle = new Article();
                Newticle.InitArticle(transform.GetChild(i).GetChild(j), i, j);
                articles[j] = Newticle;
            }
            m_articles.Add(articles);
        }
    }

}
