using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Race { Assassin, Beast, Bomber, Elemental, Fighter, Goblin, Healer, Hero, Loyal, Mage, Robot, Slime, Sniper, Undead, White, }



public class Synergy : MonoBehaviour
{

    private static Synergy m_synergy;

    public static Synergy instace
    {
        get
        {
            if(m_synergy == null)
            {
                m_synergy = FindObjectOfType<Synergy>();
                m_synergy.Init();
            }
            return m_synergy;
        }
    }

    private int[] m_races = new int[15];
    private int[] m_maximumrace = { 4, 5, 4, 4, 4, 5, 3, 8, 4, 4, 2, 4, 4, 6, 4 };
    private List<RaceCard> m_card = new List<RaceCard>();
    private List<Button> m_buttons = new List<Button>();

    private void Init()
    {
        m_card.AddRange(GetComponentsInChildren<RaceCard>(true));

        int idx = 0;

        foreach (var x in m_card)
        {
            x.Init(idx);
            m_buttons.Add(x.m_button);
            idx++;

        }

    }

    public int GetSynergy(Race race)
    {
        return m_races[(int)race];
    }

    public int SynergyLV(Race race, int count) // Method mold
    {
        return 0;
    }

    public int SynergyLVCount(Race race, int count)// Method mold
    {
        return 0;
    }

    public void CountSynergy()
    {

        List<BaseChar> fieldch = HandMng.Instance.FieldChars;
        int[] temp = new int[15];

        foreach (var x in fieldch)
        {
            foreach (var z in x.Races)
            {
                temp[(int)z]++;
            }
        }
        m_races = temp;

        for (int i = 0; i < 15; i++)
        {
            m_card[i].gameObject.SetActive(false);
            if (m_races[i] > 0)
            {
                m_card[i].gameObject.SetActive(true);
                m_card[i].RaceCount(m_races[i]);
            }
        }
    }

}
