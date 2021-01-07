using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulliganCard : MonoBehaviour
{

    private Image m_mold;
    private Image m_faceimage;
    private Color[] m_color = new Color[5];
    private Text m_gold;
    private Text m_name;
    private GameObject m_race;
    private MulliganMng m_mulligan;
    private BaseChar m_char;
    private int m_tier;
    private int m_idx;
    private bool tempbool = false;


    public bool temp
    {
        get { return tempbool; }
    }

    public BaseChar THISCHAR
    {
        get { return m_char; }
    }



      public void Init()
    {
        m_char = null;
        m_mold = transform.Find("mold").GetComponent<Image>();
        m_faceimage = transform.Find("mold/Face").GetComponent<Image>();
        m_gold = transform.Find("mold/Gold").GetComponent<Text>();
        m_name = transform.Find("mold/Name").GetComponent<Text>();
        m_race = transform.Find("mold/Race").gameObject;
        m_mulligan = MulliganMng.instance;
        m_color[0] = new Color(1, 1, 1);//white
        m_color[1] = new Color(0.3f, 1, 0.4f);//green
        m_color[2] = new Color(0.4f, 0.4f, 1);//blue
        m_color[3] = new Color(0.8f, 0.3f, 1);//puple
        m_color[4] = new Color(1, 0.86f, 0.4f);//gold
    }

    public void CreateCard(int tier, int idx)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        tempbool = true;
        m_tier = tier+1;
        m_idx = idx;
        m_mold.color = m_color[tier];
        m_gold.text = m_tier.ToString();
        m_char = ArticleListMng.Instance.Articles(tier, idx).ArticleLoad();
        m_name.text = m_char.name;
        m_char.gameObject.SetActive(true);

        m_faceimage.sprite = m_char.Face;
        //m_faceimage.color = TierColor.GetColor(tier);



    }

    public void buy()
    {
        if (!HandMng.Instance.AddChar(m_char))
            return;

        MulliganMng.instance.CardCount[m_idx, m_tier]--;
        transform.GetChild(0).gameObject.SetActive(false);
        THISCHAR.Isonline(true);
        THISCHAR.SetFoe(true);
        PlayerData.Instance.GoldCunsume(m_tier);
    }
}
