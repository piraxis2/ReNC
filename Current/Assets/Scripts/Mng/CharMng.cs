using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMng : Mng
{


    public bool m_isready = false;

    private static CharMng s_charmng;

    public static CharMng Instance
    {
        get
        {
            if (s_charmng == null)
            {
                
                GameObject obj = GameObject.Find("Main/Stage/CharMng");
                if (obj == null)
                    return null;

                s_charmng = obj.GetComponent<CharMng>();
                s_charmng.Init();

            }

            return s_charmng;
        }
    }

    private List<BaseChar> m_currEnemys = new List<BaseChar>();
    private List<BaseChar> m_currHeros = new List<BaseChar>();
    private List<BaseChar> m_totalChars = new List<BaseChar>();

    int m_count = 0;

    public List<BaseChar> CurrEnemys
    {
        get { return m_currEnemys; }
    }
    public List<BaseChar> CurrHeros
    {
        get { return m_currHeros; }
    }
    public List<BaseChar> TotalHeros 
    {
        get { return m_totalChars; }
    }

    public void AddHero(BaseChar hero)
    {
        hero.SetFoe(true);
        m_currHeros.Add(hero);
        m_totalChars.Add(hero);
        hero.SetUniqueID(m_count);
        hero.Init();
        m_count++;
    }

    public void AddEnemy(BaseChar enemy)
    {

        enemy.SetFoe(false);
        m_currEnemys.Add(enemy);
        m_totalChars.Add(enemy);
        enemy.SetUniqueID(m_count);
        enemy.Init();
        m_count++;
    }

    public override void Init()
    {
        Clear();
        
        m_currEnemys.AddRange(GetComponentsInChildren<BaseChar>());
        m_currHeros.AddRange(GetComponentsInChildren<Hero>());
        m_totalChars.AddRange(m_currHeros);
        m_totalChars.AddRange(m_currEnemys);

        
        foreach(var x in m_totalChars)
        {
            x.SetUniqueID(m_count);
            x.Init();
            m_count++;

        }

    }


    public void CharSummon()
    {

        foreach (var x in TotalHeros)
        {
            if (x.FOE)
            {
                x.transform.position = x.TecticsNode.transform.position;
                TailMng.Instance.TailGo(x.transform.position, IFF.Friend, true);
            }
            else
                TailMng.Instance.TailGo(x.transform.position, IFF.Foe, true);
        }
    }

    public void CharUnSummon()
    {
        foreach (var x in TotalHeros)
        {
            if (x.FOE)
                TailMng.Instance.TailGo(x.transform.position, IFF.Friend, false);
            else
                TailMng.Instance.TailGo(x.transform.position, IFF.Foe, false);
        }
    }

    public void InvisibleCharters(int alp)
    {
        foreach(var x in TotalHeros)
        {
            x.CharSprite.color =  new Color(1,1,1, alp);
        }
    }


    public void Activeon()
    {
        if (m_isready)
            return;

        foreach(var x in TotalHeros)
        {
            x.gameObject.SetActive(true);
            x.ReLoadChar();
        }
        m_isready = true;

    }


    public int FindID(int val)
    {
        for (int i = 0; i < TotalHeros.Count; i++)
        {
            if (TotalHeros[i].UniqueID == val)
                return i;
        }
        return -1;

    }

    public int FindHeroID(int val)
    {
        for (int i = 0; i < CurrHeros.Count; i++)
        {
            if (CurrHeros[i].UniqueID == val)
                return i;
        }
        return -1;
    }
    public int FindEnemyID(int val)
    {
        for (int i = 0; i < CurrEnemys.Count; i++)
        {
            if (CurrEnemys[i].UniqueID == val)
                return i;
        }
        return -1;
    }
    public  Hero FindHero(int id)
    {
        foreach (var x in m_currHeros)
        {
            if (x.UniqueID == id)
                return x as Hero;
        }
        return null;
    }

    public void Clear()
    {
        m_totalChars.Clear();
        m_currEnemys.Clear();
        m_currHeros.Clear();
    }
}
