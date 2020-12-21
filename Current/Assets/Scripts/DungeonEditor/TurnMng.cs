using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnMng : MonoBehaviour
{

    private static TurnMng m_turnmng;
    public static TurnMng Instance
    {
        get
        {
            if(m_turnmng == null)
            {
                m_turnmng = GameObject.Find("Main/Canvas/Turn").GetComponent<TurnMng>();
                m_turnmng.Init();
            }
            return m_turnmng;
        }
    }

    private int m_round = 0;
    private int m_turn = 0;
    private bool m_slidemove = false;
    private List<Image> m_icons;
    private Text m_text;
    private float m_speed = 1;

    private void Init()
    {

        m_text = GetComponentInChildren<Text>();
        m_text.text = (m_round + 1).ToString() + '-' + (m_turn + 1).ToString();
        StartCoroutine(IETurnChange());
       // m_icons.AddRange(transform.Find("Mask/IconList").GetComponentsInChildren<Image>());
        
    }


    public void temp()
    {
        Instance.NextTurn();
    }

    public int Turn
    {
        get { return m_turn; }
    }

    public void NextTurn()
    {
        m_turn++;

        if (m_round == 0)
        {
            if (m_turn >= 4)
            {
                m_turn = 0;
                m_round++;
            }
        }
        else
        {
            if (m_turn >= 7)
            {
                m_turn = 0;
                m_round++;
            }
        }
        m_text.text = (m_round + 1).ToString() + '-' + (m_turn + 1).ToString();
        PlayerData.Instance.ExpUp(2);
        PlayerData.Instance.GoldIncome();


     
    }

    private IEnumerator IETurnChange()
    {
        float elapsedtime = 0;
        bool stop = false;
        while(!stop)
        {
            elapsedtime += Time.deltaTime * m_speed;
            






            yield return null;
        }

        yield return null;
    }


}
