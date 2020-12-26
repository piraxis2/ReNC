using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoBox : MonoBehaviour
{

    private static CharacterInfoBox s_infobox;
    public static CharacterInfoBox Instance
    {
        get
        {

            if (s_infobox == null)
            {
                s_infobox = GameObject.Find("Main/Canvas/CharacterInfo").GetComponent<CharacterInfoBox>();
                s_infobox.Init();
            }

            return s_infobox;
        }
    }

    Image[] m_equipicon = new Image[3];
    Text[] m_texts = new Text[7];
    Sprite m_blank;
    RectTransform m_rect;



    private void Init()
    {
        m_blank = Resources.Load<Sprite>("SkillIcon/Blank");

        m_texts = GetComponentsInChildren<Text>();
        for (int i = 0; i < 3; i++)
        {
            m_equipicon[i] = transform.Find("Equipment/Icon" + i.ToString()+"/Blank").GetComponent<Image>();
        }
        m_rect = GetComponent<RectTransform>();
    }

    public void SetBox(Status status, Vector3 pos)
    {

        if (pos.x > Screen.width / 2)
            m_rect.pivot = new Vector2(1, 0);
        else
            m_rect.pivot = new Vector2(0, 0);

        m_texts[0].text = status.Name;
        m_texts[1].text = status.Life.ToString() + '/' + status.MaxLife.ToString();
        m_texts[2].text = status.Mana.ToString() + "/100";
        m_texts[3].text = string.Format("{0}({1}) {2}", status.ORIAD, status.AD - status.ORIAD, status.Token("AD"));
        m_texts[4].text = string.Format("{0}({1}) {2}", status.ORIAP, status.AP - status.ORIAP, status.Token("AP"));
        m_texts[5].text = string.Format("{0}({1}) {2}", status.ORIDF, status.DF - status.ORIDF, status.Token("DF"));
        m_texts[6].text = string.Format("{0}({1:F2})", status.ORIAS, status.AS - status.ORIAS);

        for (int i = 0; i < 3; i++)
        {
            Item sp = status.EquipMent[i];
            if (sp != null)
                m_equipicon[i].sprite = sp.m_sprite;
        }
        gameObject.SetActive(true);
        transform.position = pos + new Vector3(0, 30, 0);
    }


    public void OffBox()
    {
        for(int i =0;i<3;i++)
        {
            m_equipicon[i].sprite = m_blank;
        }

        gameObject.SetActive(false);
    }




}
