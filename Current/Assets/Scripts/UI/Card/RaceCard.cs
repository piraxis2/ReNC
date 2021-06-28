using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCard : MonoBehaviour
{
    private Text m_racename;
    private Text m_racecount;
    private Text m_level;
    private Transform m_icon;

    public void Init()
    {
        m_racecount = transform.Find("RaceCount").GetComponent<Text>();
        m_level = transform.Find("LevelTray/Level").GetComponent<Text>();
        m_icon = transform.Find("IconTray");
    }

   
    public void RaceCount(int val)
    {
//        m_racecount.text = val.ToString();

    }



}
