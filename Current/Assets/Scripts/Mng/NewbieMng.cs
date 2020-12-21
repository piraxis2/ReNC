using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieMng : Mng
{
    public static List<Hero> s_heros = new List<Hero>();
    private static Transform m_transform;
    public  static int s_recruitLV = 5;
    private static int m_idx = 0;

    private GameObject m_hunter;
    private GameObject m_knight;
    private GameObject m_mage;
    private GameObject m_warrior;
    private GameObject m_bluechicken;


    public override void Init()
    {
        m_transform = transform;

        m_hunter= Resources.Load("Prefab/Character/Hunter") as GameObject;
        m_knight = Resources.Load("Prefab/Character/Knight") as GameObject;
        m_mage = Resources.Load("Prefab/Character/Mage") as GameObject;
        m_warrior = Resources.Load("Prefab/Character/Warrior") as GameObject;
        m_bluechicken = Resources.Load("Prefab/Character/BlueChicken") as GameObject;
        for (int i = 0; i < s_recruitLV; i++)
        {
            CreateNewbie();
        }
    }

    public static void Reload()
    {
        for (int i = 0; i < s_heros.Count; i++)
        {
            s_heros[i].CardSet(i);
        }
        m_idx = s_heros.Count;
    }

    public void CreateNewbie()
    {
        int ran = Random.Range(0, 5);
        Hero hero = null;
        switch (ran)
        {
            case 0: hero = Instantiate(m_hunter.GetComponent<Hero>(), transform); break;
            case 1: hero = Instantiate(m_knight.GetComponent<Hero>(), transform); break;
            case 2: hero = Instantiate(m_mage.GetComponent<Hero>(), transform); break;
            case 3: hero = Instantiate(m_warrior.GetComponent<Hero>(), transform); break;
            case 4: hero = Instantiate(m_bluechicken.GetComponent<Hero>(), transform); break;
        }

        if (hero != null)
        {
            hero.CardSet(m_idx);
            m_idx++;
            Recruit.PageAddRemove(1);
            hero.Init();

            s_heros.Add(hero);
        }

    }

    
    public static Hero FindHero(int id)
    {
        foreach (var x in s_heros)
        {
            if (x.HeroCardID == id)
                return x;
        }
        return null;
    }
}
