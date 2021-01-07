using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Itemtype
{
    WEAPON, ARMOR, TRINKET
}


public class Passive  
{
    public int m_idx;
    public string m_name;
    public string m_optiontxt;
    public System.Action<Status> m_option;

}


public class Item
{
    public int m_idx;
    public string m_name;
    public string m_type;
    public string m_ad;
    public string m_ap;
    public string m_as;
    public string m_df;
    public string m_avoid;
    public string m_mana;
    public string m_life;
    public string m_liferecovery;
    public string m_manarecovery;
    public string m_quality;
    public string m_loer;
    public string m_cli;
    public int m_price;

    public int Quality
    {
        get
        {
            if (m_quality == "Rare")
                return 2;
            else if (m_quality == "Common")
                return 1;
            else
                return 0;
        }
    }

    public Sprite m_sprite;

    public static int SplitString(string text)
    {
        int num = 0;
        int length = text.Length - 2;
        for (int i = 1; i < text.Length; i++)
        {
            num += (text[i] - '0') * (int)Mathf.Pow(10, length);
            length--;
        }
        return num;

    }


    public static float SplitType(string type, float oristat)
    {
        float data = SplitString(type);
        switch (type[0])
        {
            case 'p': return oristat + data;
            case 'm': return oristat * (1 + (data / 100));
            case 'd': return oristat - data;
        }
        return oristat;
    }
}

public class Perk
{
    public int m_idx;
    public string m_name;
    public string m_type;
    public string m_option;
    public string m_option2;
    public bool m_on = false;
}


public class Skillinfo
{
    public int m_idx;
    public string m_name;
    public string m_text;
    public string m_info;
    public Sprite m_icon;

}

public class Dungeon
{
    public int m_idx;
    public string m_name;
    public string m_path;
    public List<string[,]> m_maps = new List<string[,]>(); 
}


public class PlayerSkill
{

    public int m_idx;
    public string m_name;
    public string m_info;
    public int m_cost;
    public Sprite m_icon;
    public System.Action m_option;

}


public class Build
{

    public int m_idx;
    public string m_name;
    public string m_info;
    public int m_cost;
    public System.Action m_option;
}

public class Buff
{
    public int m_idx;
    public string m_name;
    public float m_time;
    public System.Action<Status> m_option;
    public bool m_permanent = false;

    public int m_val = 0;
    public int m_val2 = 0;
    public float m_percentage = 0;

    public PixelFx Bufffx()
    {
        switch(m_name)
        {
            case "Bleeding": return FxMng.Instance.FxCall("Bleeding");
            case "Stun": return FxMng.Instance.FxCall("Stun");
            case "Silence": return FxMng.Instance.FxCall("Silence");

        }
        return null;
    }


    public void Buffoff(Status target)
    {
        if (m_name == "Stun")
            target.m_stuned = false;

        target.BuffList.Remove(this);

    }

    public void Enhance(Status status)
    {
        status.m_equad += 10;
    }

    public void SlowSpeed(Status status)
    {
        float speed = status.m_equas;
        status.m_equas = speed * 0.9f;
    }


}


public static class TierColor
{
    private static Color[] m_color = {
        new Color(1, 1, 1), //white;
        new Color(0.3f, 1, 0.4f),//green
        new Color(0.4f, 0.4f, 1),//blue
        new Color(0.8f, 0.3f, 1),//puple
        new Color(1, 0.86f, 0.4f) };//gold 

    public static Color GetColor(int tier)
    {
        if (tier > 4 || tier < 0)
            return Color.white;

        return m_color[tier];
    }
}

