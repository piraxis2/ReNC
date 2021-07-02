using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCard : MonoBehaviour
{
    private Text m_racename;
    private Text m_racecount;
    private Text m_level;
    private Transform m_icontray;
    private Image m_icon;
    private Race m_race;
    public Button m_button;


    public void Init(int idx)
    {
        m_racecount = transform.Find("RaceCount").GetComponent<Text>();
        m_level = transform.Find("LevelTray/Level").GetComponent<Text>();
        m_icontray = transform.Find("IconTray");
        m_icon = m_icontray.transform.Find("Icon").GetComponent<Image>();
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(() => ToolTipPopup());
        m_race = (Race)idx;
    }

   
    public void RaceCount(int val)
    {
        m_level.text = Synergy.instace.SynergyLV(m_race, val).ToString();
        m_racecount.text = val.ToString() + " / " + Synergy.instace.SynergyLVCount(m_race, val);
    }

    public void ToolTipPopup()
    {
        RaceCardTooltip.Instance.SetBox(m_race.ToString(), m_icon.sprite, new List<string>(), transform);

    }

}
