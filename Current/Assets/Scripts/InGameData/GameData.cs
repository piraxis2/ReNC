using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    Gold, Crest, relic
}



public class GameData
{

    private static GameData s_gamedata;

    public static GameData Instance
    {
        get
        {
            if (s_gamedata == null)
            {
                s_gamedata = new GameData();
            }
            return s_gamedata;
        }
    }

    GameData()
    {

    }

    public bool m_IsAC = true;

    public int m_globalturn = 0;
   
}


public class InGameResource
{

    private int m_gold = 500;
    private int m_crest;
    private int m_relic;

    private static InGameResource s_inGameResource;

    public static InGameResource instance
    {
        get
        {
            if (s_inGameResource == null)
            {
                s_inGameResource = new InGameResource();
            }
            return s_inGameResource;
        }
    }

    InGameResource()
    {

    }

    public int GetResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold: return m_gold;
            case ResourceType.Crest: return m_crest;
            case ResourceType.relic: return m_relic;
        }
        return -10;
    }

    public void ResourceGain(ResourceType type, int price)
    {

        switch (type)
        {
            case ResourceType.Gold: m_gold += price; break;
            case ResourceType.Crest: m_crest += price; break;
            case ResourceType.relic: m_relic += price; break;
        }

    }

    public bool ResourceConsume(ResourceType type, int price)
    {

        switch (type)
        {
            case ResourceType.Gold:
                if (m_gold - price < 0)
                    return false;
                m_gold -= price;
                return true;
            case ResourceType.Crest:
                if (m_crest - price < 0)
                    return false;
                m_crest -= price;
                return true;
            case ResourceType.relic:
                if (m_relic - price < 0)
                    return false;
                m_relic -= price;
                return true;
        }
        return false;
    }



}
