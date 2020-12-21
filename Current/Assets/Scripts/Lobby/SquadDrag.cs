using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadDrag : Mng
{
    private DragHelper m_draghelper;
    private Node m_targetNode;
    private Hero m_hero;
    private bool m_click = false;
    private Color m_green = new Color(0.7f, 1, 0.8f);
    private Color m_red = new Color(1, 0.3f, 0.3f);
    private Transform m_lobby;
    private Transform m_squad;
    private HeroCard m_card;
    private bool m_playing = false;



    private void Start()
    {
        
    }

    public override void Init()
    {
        m_squad = transform;
        m_lobby = transform.parent.Find("Lobby");
    }
    
    private void returncard()
    {
        m_hero.transform.position = m_draghelper.m_oripos;
        m_hero.transform.SetParent(m_lobby);
        m_card.m_checkmark.SetActive(false);
    }


    private void Update()
    {

        if (m_playing)
            return;

        
        if(Input.GetMouseButtonDown(0))
        {
            m_hero = HRay();



            if (m_hero != null)
            {
                m_card = CardMng.Instance.FindCard(m_hero.HeroCardID);
                m_draghelper = m_hero.GetComponent<DragHelper>();
                m_draghelper.m_nodemng.NodeClear();
                if (m_draghelper.m_sitnode != null)
                    m_draghelper.m_sitnode.m_squadhere = false;
                m_draghelper.m_sitnode = null;
                m_click = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Node node = NRay();
            if (m_hero != null)
            {
                m_hero.CurrNode.m_sprite.color = m_hero.CurrNode.OriColor;
                if (node != null)
                {
                    if (m_hero.CurrNode.Col <= 3)
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
                            returncard();
                        }
                    }
                    else
                    {
                        returncard();
                    }
                }
                else
                {
                    returncard();
                }


                m_draghelper.m_targetnode = null;
                m_draghelper.m_prevnode = null;
            }

            HeroLobbyMng.ReloadSquad();
            m_click = false;
        }

        if(m_click)
        {
            m_draghelper.m_prevnode = m_draghelper.m_targetnode;
            m_draghelper.m_targetnode = NRay();

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
            ///
        }
    }

    private Hero HRay()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 9))
        {
            Hero se = hit.transform.GetComponent<Hero>();


            if (se != null)
            {
                return se;
            }
        }
        return null;
    }

    private Node NRay()
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
