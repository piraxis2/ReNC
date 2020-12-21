using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private HeroCard m_card;
    private Hero m_hero;
    private Color m_green = new Color(0.7f, 1, 0.8f);
    private Color m_red = new Color(1, 0.3f, 0.3f);
    private DragHelper m_draghelper;
    private Transform m_Lobby;
    private Transform m_squad;
    private GameObject m_checkmark;


    public void Init()
    {
        m_card = GetComponent<HeroCard>();
        m_hero = HeroLobbyMng.FindHero(m_card.CardID);
        m_draghelper = m_hero.GetComponent<DragHelper>();
        if (m_draghelper != null)
        {
            m_squad = m_draghelper.m_heroLobby.transform.Find("Squad");
            m_Lobby = m_draghelper.m_heroLobby.transform.Find("Lobby");
        }
    }

    public void RetrunCard()
    {
        m_hero.transform.position = m_draghelper.m_oripos;
        m_hero.transform.SetParent(m_Lobby);
        m_card.m_checkmark.SetActive(false);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        m_draghelper. m_nodemng.NodeClear();
        if (m_draghelper.m_sitnode != null)
            m_draghelper.m_sitnode.m_squadhere = false;
        m_draghelper.m_sitnode = null;

    }

    public void OnDrag(PointerEventData eventData)
    {
        m_draghelper.m_prevnode = m_draghelper.m_targetnode;
        m_draghelper.m_targetnode = Ray();
    
        if (m_draghelper.m_prevnode != m_draghelper.m_targetnode)
        {
            m_draghelper.m_nodemng.NodeClear();
            if (m_draghelper.m_prevnode != null)
                m_draghelper.m_prevnode.m_sprite.color = m_draghelper.m_prevnode.OriColor;
            if (m_draghelper.m_targetnode != null)
            {
                if (m_draghelper.m_targetnode.Col > 3)
                    m_draghelper.m_targetnode.m_sprite.color = m_red;
                else
                    m_draghelper.m_targetnode.m_sprite.color = m_green;
            }
        }
        if (m_draghelper.m_targetnode == null)
        {
            if (m_hero.CurrNode != null)
                m_hero.CurrNode.m_sprite.color = m_red;
            return;
        }


        Vector3 currpos = m_draghelper.m_targetnode.transform.position;
        m_hero.transform.position = currpos;
               
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Node node = Ray();
        if (m_hero.CurrNode != null)
            m_hero.CurrNode.m_sprite.color = m_hero.CurrNode.OriColor;
        if (node != null)
        {
            if (m_hero.CurrNode.Col <= 3  )
            {
                Node sub = LobbyNodeMng.Subcount();
                if (LobbyNodeMng.Herocount() < 4)
                {
                    m_draghelper.m_targetnode.m_squadhere = true;
                    m_draghelper.m_sitnode = m_draghelper.m_targetnode;
                    m_hero.transform.SetParent(m_squad);
                    m_card.m_checkmark.SetActive(true);
                }
                else if (sub != null)
                {
                    sub.m_squadhere = true;
                    m_hero.transform.position = sub.transform.position;
                    m_draghelper.m_sitnode = sub;
                    m_hero.transform.SetParent(m_squad);
                    m_card.m_checkmark.SetActive(true);
                }
                else
                {
                    RetrunCard();
                }
            }
            else
            {
                RetrunCard();
            }
        }
        else
        {
            RetrunCard();
        }

        HeroLobbyMng.ReloadSquad();
        m_draghelper.m_targetnode = null;
        m_draghelper.m_prevnode = null;
    }

    private Node Ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            Node se = hit.transform.GetComponent<Node>();


            if (se != null)
            {
                if (se.m_squadhere)
                    return null;

                return se;
            }
        }
        return null;
    }
}
