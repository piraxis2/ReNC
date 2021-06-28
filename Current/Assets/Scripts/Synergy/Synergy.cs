using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race { Assassin, Beast, Bomber, Elemental, Goblin, Healer, Hero, Loyal, Mage, Robot, Slime, Sniper, Undead, Fighter, White, }

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

    private int[] m_Synergys = new int[15];
    private List<RaceCard> m_card = new List<RaceCard>();

    private void Init()
    {
        m_card.AddRange(GetComponentsInChildren<RaceCard>(true));

    }

    public int GetSynergy(Race race)
    {
        return m_Synergys[(int)race];
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
        m_Synergys = temp;

        for (int i = 0; i < 15; i++)
        {
            m_card[i].gameObject.SetActive(false);
            if (m_Synergys[i] > 0)
            {
                m_card[i].gameObject.SetActive(true);
                m_card[i].RaceCount(m_Synergys[i]);
            }
        }
    }

}
