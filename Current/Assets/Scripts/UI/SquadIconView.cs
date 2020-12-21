using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadIconView : Mng 
{

    private static Sprite m_blank;
    private static Image[] m_icons = new Image[6];

    public override void Init()
    {
        m_blank = transform.GetComponent<Image>().sprite;
        m_icons = GetComponentsInChildren<Image>(true);
    }
    public static void ReloadView()
    {
        for (int i = 0; i < 5; i++)
        {
            if (HeroLobbyMng.s_onstageheros[i] == null)
                m_icons[i + 1].sprite = m_blank;
            else
                m_icons[i + 1].sprite = SpriteMng.s_classimage[(int)HeroLobbyMng.s_onstageheros[i].ClassType].sprite;
        }
    }

}
