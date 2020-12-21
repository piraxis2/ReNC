using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHelper : MonoBehaviour
{
    public HeroCard m_card;
    public HeroLobbyMng m_heroLobby;
    public BaseChar m_hero;
    public Node m_targetnode;
    public Node m_prevnode;
    public Node m_sitnode;
    public NodeMng m_nodemng;
    public Color m_green = new Color(0.7f, 1, 0.8f);
    public Color m_red = new Color(1, 0.3f, 0.3f);
    public Vector3 m_oripos = new Vector3();

    public void Init()
    {

        m_heroLobby = transform.root.GetComponentInChildren<HeroLobbyMng>();
        m_card = GetComponent<HeroCard>();

        m_hero = GetComponent<BaseChar>();
        m_oripos = new Vector3(1000, 1000, 0);
        m_nodemng = transform.root.GetComponentInChildren<NodeMng>();

    }
}
