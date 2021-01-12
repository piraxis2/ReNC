using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDrag : Mng
{
    private DragHelper m_draghelper;
    private Node m_targetNode;
    private BaseChar m_hero;
    private bool m_click = false;
    private Color m_green = new Color(0.7f, 1, 0.8f,0.6f);
    private Color m_red = new Color(1, 0.3f, 0.3f, 0.6f);
    private static bool m_playing = false;



    public override void Init()
    {
    }

    private void returnhand()
    {


        m_draghelper.m_nodemng.NodeClear();
        if (HandMng.Instance.CountHand() < 9)
        {
            if (m_hero.gameObject.activeInHierarchy)
            {
                HandMng.Instance.RemoveOnfiled(m_hero);
                m_hero.transform.position = HandMng.Instance.FindEmptyNode().transform.position;
            }
            else
                m_hero.transform.position = m_draghelper.m_oripos;
        }
        else
        {

            m_hero.transform.position = m_draghelper.m_oripos;
        }

     //   m_hero.transform.position = m_draghelper.m_oripos;
    }


    public static bool Play(string key)
    {
        switch(key)
        {
            case "on": m_playing = false; break;
            case "off": m_playing = true; break;
        }

        return m_playing;
    }

    private void Update()
    {

        //if (m_playing)
        //    return;



        if (Input.GetMouseButtonDown(0))
        {
            m_hero = HRay();
            if (m_hero != null)
            {
                Sale.Instance.PopUp(m_hero);
                m_draghelper = m_hero.GetComponent<DragHelper>();
                m_draghelper.m_oripos = m_hero.CurrNode.transform.position;
                m_draghelper.m_nodemng.NodeClear();
                HandMng.Instance.NodeClear();
                if (m_draghelper.m_sitnode != null)
                    m_draghelper.m_sitnode.m_squadhere = false;
                m_draghelper.m_sitnode = null;
                m_click = true;
            }
            else
            {
                return;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {

            Node node = NRay();
            if (m_hero != null)
            {
                m_hero.CurrNode.m_sprite.color = m_hero.CurrNode.OriColor;
                Sale.Instance.PopOff();

                if (node != null)
                {
                    if (m_hero.CurrNode.Row > 3)
                    {
                        if (PlayerData.Instance.LV > HandMng.Instance.FieldChars.Count)//1019 lv char limit add plz 
                        {
                            HandMng.Instance.ReturnField(m_hero);
                            m_draghelper.m_targetnode.m_squadhere = true;
                            m_draghelper.m_sitnode = m_draghelper.m_targetnode;
                        }
                        //else if (sub != null)
                        //{
                        //    sub.m_squadhere = true;
                        //    m_hero.transform.position = sub.transform.position;
                        //    m_draghelper.m_sitnode = sub;
                        //}
                        else
                        {
                            if(m_hero.IsOnField)
                            {
                                HandMng.Instance.ReturnField(m_hero);
                                m_draghelper.m_targetnode.m_squadhere = true;
                                m_draghelper.m_sitnode = m_draghelper.m_targetnode;
                            }

                            else
                                returnhand();
                        }
                    }
                    else if (m_hero.CurrNode.IsHand)
                    {
                        HandMng.Instance.ReturnHand(m_hero);
                        m_draghelper.m_targetnode.m_squadhere = true;
                        m_draghelper.m_sitnode = m_draghelper.m_targetnode;
                        m_hero.Isonfield(false);
                    }
                    else
                    {
                        returnhand();
                    }
                }
                else
                {
                    returnhand();
                }


                m_draghelper.m_targetnode = null;
                m_draghelper.m_prevnode = null;
            }


            m_click = false;


        }

        if (m_click)
        {


            m_draghelper.m_prevnode = m_draghelper.m_targetnode;
            m_draghelper.m_targetnode = NRay();


            m_draghelper.m_nodemng.NodeClear(m_hero);
            HandMng.Instance.NodeClear(m_hero);


            if (m_draghelper.m_prevnode != m_draghelper.m_targetnode)
            {
                if (m_draghelper.m_prevnode != null)
                    m_draghelper.m_prevnode.m_sprite.color = m_draghelper.m_prevnode.OriColor;


                if (m_draghelper.m_targetnode != null)
                {
                    if (m_draghelper.m_targetnode.Row > 3 || m_draghelper.m_targetnode.IsHand)
                    {
                        if (PlayerData.Instance.LV <= HandMng.Instance.FieldChars.Count)
                        {
                            if(m_hero.IsOnField)
                            m_draghelper.m_targetnode.m_sprite.color = m_green;
                            else
                            m_draghelper.m_targetnode.m_sprite.color = m_red;
                        }
                        else
                            m_draghelper.m_targetnode.m_sprite.color = m_green;

                    }
                    else
                        m_draghelper.m_targetnode.m_sprite.color = m_red;
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
    }



    private BaseChar HRay()
    {
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 10 | 1 << 9))
        {
            BaseChar se = hit.transform.GetComponent<BaseChar>();

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
