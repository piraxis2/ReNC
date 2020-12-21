using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardMng : MonoBehaviour
{

    private static CardMng s_cardmng;
    public static CardMng Instance
    {
        get
        {
            if (s_cardmng == null)
            {
                s_cardmng = FindObjectOfType<CardMng>();
                s_cardmng.Init();
            }
            return s_cardmng;
        }
    }


    private int m_deckidx = 0;
    private List<HeroCard> m_card = new List<HeroCard>();
    private GameObject m_defaultcard;
    private HeroCard m_heroCard;
    private HeroLobbyMng m_herolobby;


    public List<HeroCard> Deck
    {
        get { return m_card; }
    }



    public void Init()
    {
        m_herolobby = transform.root.GetComponentInChildren<HeroLobbyMng>(true);
        m_defaultcard = Resources.Load("Prefab/HeroCard") as GameObject;

        m_heroCard = m_defaultcard.GetComponent<HeroCard>();

        foreach(var x in HeroLobbyMng.s_heros )
        {
            m_card.Add(Instantiate(m_heroCard,transform));
            m_card[m_deckidx].CreateCard(x, m_deckidx);
            m_card[m_deckidx].transform.name = "HeroCard";
            m_deckidx++;
        }
    }

    public void AddCard(Hero hero)
    {
        HeroLobbyMng.s_heros.Add(hero);
        hero.transform.SetParent(HeroLobbyMng.m_transform.Find("Lobby"));
        CharacterBook.Instance.PageAddRemove(1);
        m_card.Add(Instantiate(m_heroCard, transform));
        m_card[m_deckidx].CreateCard(hero, m_deckidx);
        m_deckidx++;
    }

    public  HeroCard FindCard(int id)
    {
        foreach (var x in m_card)
        {
            if (x.CardID == id)
                return x;
        }
        return null;
    }



}
