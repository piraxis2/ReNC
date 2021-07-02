using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCardTooltip : MonoBehaviour
{

    static private RaceCardTooltip s_racecardtooltip;
    static public RaceCardTooltip Instance
    {
        get
        {
            if (s_racecardtooltip == null)
            {
                s_racecardtooltip = GameObject.Find("Main/Canvas/RaceCardTooltip").GetComponent<RaceCardTooltip>();
                s_racecardtooltip.Init();
            }
            return s_racecardtooltip;
        }
    }

    private Image m_icon;
    private Text m_name;
    private Text m_basetext;
    private List<Text> m_texts;
    private ObjectSize m_size;


    public void Init()
    {
        m_name = transform.Find("Name").GetComponent<Text>();
        m_icon = transform.Find("Icon/Blank").GetComponent<Image>();
        m_basetext = transform.Find("BackGroundImage/View/Text").GetComponent<Text>();
        m_size = GetComponentInChildren<ObjectSize>(true);
        
    }



    public void SetBox(string name , Sprite sprite, List<string> texts, Transform trans )
    {
        OffBox();
        m_name.text = name;
        m_icon.sprite = sprite;

        if (texts.Count > 0)
        {
            m_basetext.text = texts[0];
            for (int i = 0; i < texts.Count - 1; i++)
            {
                Text instex = Instantiate(m_basetext, m_basetext.transform.parent);
                instex.text = texts[i + 1];
            }
        }
        transform.position = trans.position;
        gameObject.SetActive(true);
    }

    public void OffBox()
    {

        var texts = m_basetext.transform.parent.GetComponentsInChildren<Text>();
        foreach(var x in texts)
        {
            if (x != m_basetext)
                Destroy(x.gameObject);
        }

    }



}
