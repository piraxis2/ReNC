using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SummnerSkill
{
    Heal,
    Enhance,
    Camping,
    Supportfire,
    Sniping,
    Firewall,
    Run,
    Money,
    Hearthstone
}


public class SummonerSkillMng : MonoBehaviour
{

    private static SummonerSkillMng s_summoner;

    public static SummonerSkillMng Instance
    {
        get
        {
            if (s_summoner == null)
            {
                s_summoner = FindObjectOfType<SummonerSkillMng>();
            }

            if (s_summoner.m_manaCrystal[0]==null)
            { s_summoner.Init(); }
            return s_summoner;
        }
    }


    private int m_mana = 0;
    private Image[] m_manaCrystal = new Image[5];
    private Button[] m_buttons = new Button[3];
    private Image[] m_icons = new Image[3];


    public int Mana
    {
        get
        {
            return m_mana;
        }
    }

    public Vector3[] ManaPositon
    {
        get
        {
            Vector3[] pos = new Vector3[5];
            for (int i = 0; i < 5; i++)
            {
                pos[i] = Camera.main.ScreenToWorldPoint(m_manaCrystal[i].transform.position);
            }
            return pos;
        }
    }


    public void Init()
    {
        if (SceneMng.Instance.GetCurrSceneName() != "Ingame")
        { return; }

        m_manaCrystal = transform.Find("ManaTray/Full").GetComponentsInChildren<Image>(true);
        Manaview();
        m_buttons = transform.GetComponentsInChildren<Button>(true);

        for (int i = 0; i < 3; i++)
        {
            m_icons[i] =m_buttons[i].transform.Find("Image").GetComponent<Image>();
            if (PlayerData.Instance.Icons(i) != null)
            {
                Debug.Log(PlayerData.Instance.Icons(i));
                m_icons[i].sprite = PlayerData.Instance.Icons(i);
            }


            if (PlayerData.Instance.SkillTray.Count - 1 < i)
                continue;

            int idxx = i;
            if (PlayerData.Instance.SkillTray[i] >= 0)
                m_buttons[i].onClick.AddListener(
                    () =>
                    {
                        PlayerSkill ps =
                        (TableMng.Instance.Table
                        (TableType.PlayerSkillTable,
                        PlayerData.Instance.SkillTray[idxx])
                        as PlayerSkill);
                        if (CostMana(0))
                            ps.m_option();

                        Manaview();
                    }
                    );
        }
    }

    private void Manaview()
    {
        for (int i = 0; i < 5; i++)
        {
            if (m_mana == 0)
                return;

            int manaval = m_mana - ((i + 1) * 2);

            if (manaval >= 1)
            {
                m_manaCrystal[i].fillAmount = 1;
                continue;
            }
            if (manaval < -1)
            {
                m_manaCrystal[i].fillAmount = 0;
                continue;
            }
            switch (manaval)
            {
                case -1: m_manaCrystal[i].fillAmount = 0.5f; break;
                case 0: m_manaCrystal[i].fillAmount = 1; break;
            }

        }
    }

    public bool CostMana(int cost)
    {
        if (m_mana < cost)
            return false;

        m_mana -= cost;
        Manaview();
        return true;
    }


    public void GetMana()
    {
        m_mana++;

        if (m_mana > 10)
            m_mana = 10;

        Manaview();
    }


}

