using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipBox : MonoBehaviour
{
    private static ToolTipBox s_tooltipbox;
    public static ToolTipBox Instance
    {
        get
        {
            if (s_tooltipbox == null)
            {
                s_tooltipbox = GameObject.Find("Main/Canvas/ToolTipBox").GetComponent<ToolTipBox>();
                s_tooltipbox.Init();
            }
            return s_tooltipbox;
        }
    }

    private Text[] m_texts = new Text[3];

    public void Init()
    {
        m_texts = GetComponentsInChildren<Text>(true); 
    }

    public void SetBox(string name, string text, string text2, Vector3 pos)
    {
        m_texts[0].text = name;
        m_texts[1].text = text;
        m_texts[2].text = text2;
        gameObject.SetActive(true);
        transform.position = pos + new Vector3(0, 15, 0);
    }

    public void OffBox()
    {
        gameObject.SetActive(false);
    }

}
