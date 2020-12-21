using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sale : MonoBehaviour
{

    private static Sale m_sale;
    public static Sale Instance
    {
        get
        {
            m_sale = GameObject.Find("Main/Canvas/sale").GetComponent<Sale>();
            m_sale.Init();
            return m_sale;
        }
    }

    private Color m_green = new Color(0.4f, 1, 0.3f, 0.6f);
    private Color m_oricol = new Color(0, 0, 0, 0.6f);
    private BaseChar m_target;
    private Image m_image;
    private Text m_text;
    private bool m_isSale;
    private int[,] m_saleprice =
    {
        { 1,2,3,4,5 },
        { 3,4,5,6,7 },
        { 4,5,7,8,9 }
    };

    public bool IsSale
    {
        get { return m_isSale; }
    }
    

    private void Init()
    {
        m_text = m_sale.GetComponentInChildren<Text>();
        m_image = m_sale.GetComponent<Image>();

    }

    public void GoldView(int star, int tier)
    {
        m_text.text = "판매 가격 : " + m_saleprice[star-1, tier].ToString();
    }


    public void PopUp(BaseChar target)
    {
        m_target = target;
        m_sale.gameObject.SetActive(true);
        StartCoroutine(IESale());
    }

    public void PopOff()
    {
        if (m_isSale)
            VSale();

        m_target = null;
        m_sale.gameObject.SetActive(false);
        StopCoroutine(IESale());
    }

    public void VSale()
    {
        int gold = m_saleprice[m_target.Star - 1, m_target.Tier];
        HandMng.Instance.RemoveChar(m_target);
        m_target.KillThis("sale");
        PlayerData.Instance.GoldGet(gold);

    }

    public IEnumerator IESale( )
    {
        RectTransform rt = m_sale.transform.GetComponent<RectTransform>();
        GoldView(m_target.Star, m_target.Tier);

        while (true)
        {
            bool boxoff = false;
            int idx = 0;

            if (MathHelper.CalculateBounds(rt, rt.lossyScale.x).Contains(Input.mousePosition))
            {
                m_isSale = true;
                boxoff = true;
                m_image.color = m_green;
            }
            idx++;


            if (!boxoff)
            {
                m_image.color = m_oricol;
                m_isSale = false;
            }


            yield return null;
        }
    }
}
