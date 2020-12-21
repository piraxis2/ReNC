using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pair<key, value>
{
    public Pair()
    {

    }
    public Pair(key key, value value)
    {
        this.Key = key;
        this.Value = value;
    }

    public key Key { get; set; }
    public value Value { get; set; }
}



public class MulliganMng : MonoBehaviour
{


    private static MulliganMng s_mulliganMng;
    public static MulliganMng instance
    {
        get
        {
            if (s_mulliganMng == null)
            {
                s_mulliganMng = FindObjectOfType<MulliganMng>();
                s_mulliganMng.Init();
            }
            return s_mulliganMng;
        }
    }


    public int[,] CardCount
    {
        get { return m_cardcount; }
    }

    public bool IsLock
    {
        get { return m_lock; }
    }

    private bool m_lock = false;
    private MulliganCard[] m_cards;
    private ButtonSwap m_buttonswap;





    public void Init()
    {


        m_cards = GetComponentsInChildren<MulliganCard>();
        m_buttonswap = GetComponentInChildren<ButtonSwap>();
        ProbabilityUI.Instance.SetNumber();
        for(int i = 0; i<5;i++)
        {
            m_cards[i].Init();
        }
        reroll();
    }


    private int[,] m_cardcount =  {
            { 29, 22, 18, 12, 10 },
            { 29, 22, 18, 12, 10 },
            { 29, 22, 18, 12, 10 },
            { 29, 22, 18, 12, 10 },
            { 29, 22, 18, 12, 0  },
            { 29, 22, 18, 12, 0  },
            { 29, 22, 18, 0 , 0  },
            { 29, 22, 18, 0 , 0  },
            { 29, 22, 18, 0 , 0  } };
    private int[] m_tier = { 9, 9, 9, 6, 4 };
    private int[,] m_rerollprobability = {
            { 100,  0,  0,  0, 0 },
            { 100,  0,  0,  0, 0 },
            {  75, 25,  0,  0, 0 }, 
            {  55, 30, 15,  0, 0 },
            {  40, 35, 20,  5, 0 },
            {  25, 35, 30, 10, 0 },
            {  19, 30, 35, 15, 1 },
            {  14, 20, 35, 25, 6 },
            {  10, 15, 30, 30, 15}};
 

    public int[,] Rerollprobability
    {
        get { return m_rerollprobability; }
    }

    //public void debug()
    //{
    //    int a = randomroll(1);

    //    Debug.Log(randomintier(a));

    //}

    public Pair<int,int> randomhand(int lv)
    {

        Pair<int, int> dic = new Pair<int, int>();

        int a = randomroll(lv - 1);

        dic.Key = a;
        dic.Value = randomintier(a);
        
        return dic;

    }

    private int randomintier(int key)
    {
        int value = Random.Range(0, m_tier[key]);


        if(m_cardcount[value, key] <= 0)
        {
            value = randomintier(key);
        }
        return value;

    }

    public int randomroll(int lv)
    {
        int target = Random.Range(1, 100);
        int idx = 0;
        int probability = 0;
     
        while (m_rerollprobability[lv, idx] != 0)
        {
            probability += m_rerollprobability[lv, idx];
            if (target<probability)
            {
                return idx;
            }
            idx++;
        }
        return idx;
    }


    private void turncycle()
    {
        if (IsLock)
            return;

        reroll();
    }

    public void Buyreroll()
    {
        if (PlayerData.Instance.GOLD - 2 < 0)
            return;
        
        reroll();
        PlayerData.Instance.GoldCunsume(2);
    }

    public void reroll()
    {
        if(IsLock)
        {
            m_lock = false;
            m_buttonswap.swap();
        }

        if(m_cards[0].temp)
        {
            for (int i = 0; i < 5; i++)
            {
                if (m_cards[i].THISCHAR.IsOnline)
                    continue;

                if (m_cards[i].THISCHAR.IsExclude)
                    continue;

                m_cards[i].THISCHAR.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            Pair<int, int> pair = randomhand(PlayerData.Instance.LV);
            m_cards[i].CreateCard(pair.Key, pair.Value);
        }
    }

    public void handlock()
    {
        m_buttonswap.swap();
        m_lock = !m_lock;
    }

    public void pickhand()
    {

    }

}
