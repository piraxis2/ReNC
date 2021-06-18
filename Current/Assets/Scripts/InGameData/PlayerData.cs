using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerData
{

    private static PlayerData s_playerdata;
    public static PlayerData Instance
    {
        get
        {
            if (s_playerdata == null)
            {
                s_playerdata = new PlayerData();
                s_playerdata.Init();
            }
            return s_playerdata;
        }
    }


    private int m_turn = 1;
    private int m_playerlV = 1;
    private int m_exp = 0;
    private int m_gold = 10000;
    private int m_HP = 100;

    private string m_playername;
    private List<int> m_skills = new List<int>();
    private int[] m_onskills = new int[3];
    private int[] m_builds = new int[4];
    private Sprite[] m_icons = new Sprite[3];
    private int[] m_exptable = { 2, 2, 6, 10, 20, 36, 56, 80, 10000 };
    private bool m_iwin;
    private int m_winstack = 0;


    public void Init()
    {
              
    }

    public int GOLD
    {
        get { return m_gold; }
    }

    public int LV
    {
        get { return m_playerlV; }
    }

    public int EXP
    {
        get { return m_exp; }
    }

    public int WINSTACK
    {
        get { return m_winstack; }
    }


    public int HP
    {
        get { return m_HP; }
    }

    public int[] EXPTABLE
    {
        get { return m_exptable; }
    }

    public int[] Build
    {
        get { return m_builds; }
    }

    public int SkillCount
    {
        get
        {
            int num = 0;
            for (int i = 0; i < 3; i++)
            {
                num += m_onskills[i];
            }
            return num;
        }
    }

    public void GoldIncome()
    {


        if (m_gold >= 50)
        {
            m_gold += 5;
        }
        else
        {
            m_gold += m_gold / 10;
        }
        if (m_iwin)
        {
            m_gold++;
            m_winstack++;
        }
        else
        {
            m_winstack = 0;
        }

        if (Math.Abs(m_winstack) >= 2)
            m_gold++;
        else if (Math.Abs(m_winstack) >= 4)
            m_gold += 2;
        else if (Math.Abs(m_winstack) >= 5)
            m_gold += 3;

            m_gold += 5;


        PlayerINFO.Instatnce.Goldupdate();

    }

    public int IncomeCal(string name)
    {
        int re = 0;
        switch (name)
        {
            case "Passive": re = 5; break; 
            case "Interest":
                if (m_gold >= 50)
                {
                    re = 5;
                }
                else
                {
                    re = m_gold / 10;
                }
                break;
            case "Win/Loss":
                if (Math.Abs(m_winstack) >= 2)
                    re = 1;
                else if (Math.Abs(m_winstack) >= 4)
                    re = 2;
                else if (Math.Abs(m_winstack) >= 5)
                    re = 3;
                break;
            case "Total": re = IncomeCal("Passive") + IncomeCal("Interest") + IncomeCal("Win/Loss"); break;
        }

        return re;
    }



    public void GoldCunsume(int gold)
    {

        if (m_gold - gold == 0)
        {
            m_gold = 0;
            PlayerINFO.Instatnce.Goldupdate();
        }

        if (m_gold - gold <= 0)
            return;

        m_gold -= gold;
        PlayerINFO.Instatnce.Goldupdate();
    }

    public void GoldGet(int gold)
    {
        m_gold += gold;
        PlayerINFO.Instatnce.Goldupdate();
    }
    public void ExpUp(int exp)
    {
        if (LV > 8)
            return;

        m_exp += exp;


        if (m_exp >= m_exptable[LV - 1])
        {
            m_exp -= m_exptable[LV - 1];
            LVUP();
            if (m_exp >= m_exptable[LV - 1])
            {
                m_exp -= m_exptable[LV - 1];
                LVUP();
            }

            PlayerINFO.Instatnce.HPupdate();
        }


        PlayerINFO.Instatnce.XPupdate();
    }

    private void LVUP()
    {
        m_playerlV++;
        ProbabilityUI.Instance.SetNumber();
    }


    public void HPscale(int damage)
    {

        m_HP -= damage;
        if(m_HP<=0)
        {
            m_HP = 0;
            PlayerINFO.Instatnce.HPupdate();
            PlayerIsDead();
            return;
        }
        PlayerINFO.Instatnce.HPupdate();
    }



    public void PlayerIsDead()
    {

    }

    public void SkillUP(int idx)
    {
        if (m_onskills[idx] >= 3)
            return;

        m_onskills[idx]++;
    }


    public List<int> SkillTray
    {
        get { return m_skills; }
    }

    public Sprite Icons(int idx)
    {

        if (SkillTray.Count - 1 < idx)
            return null;


        if (SkillTray[idx] < 0)
            return null;

        return (TableMng.Instance.Table(TableType.PlayerSkillTable, SkillTray[idx]) as PlayerSkill).m_icon;
    }

    public int Skill(int idx)
    {
        if (idx >= 3)
            return -1;

        return m_onskills[idx];
    }



}
