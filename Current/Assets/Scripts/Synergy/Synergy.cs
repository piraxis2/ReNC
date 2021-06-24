using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race { Beast, Elemental, Goblin, Hero, Robot, Slime, Undead, White, Assassin, Bomber, Fighter, Healer, Loyal, Mage, Sniper, }

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


    private void Init()
    {

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
    }

}
