using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLobbyMng :Mng 
{

    public static List<Hero> s_heros = new List<Hero>();
    public static List<Hero> s_standbyheros = new List<Hero>();
    public static Hero[] s_onstageheros = new Hero[6];
    public static Transform m_transform;
    public static int s_squadcount = 0;
    public GameObject obj;

    public override void Init()
    {
        m_transform = transform;

        s_heros.AddRange(GetComponentsInChildren<Hero>(true));
        foreach(var x in s_heros)
        {
            x.Init();
        }
        CharacterBook cb = CharacterBook.Instance;



    }

    public static int ReloadSquad()
    {
        List<Hero> temp = new List<Hero>();
        s_standbyheros = new List<Hero>();
        temp.AddRange(m_transform.Find("Squad").GetComponentsInChildren<Hero>());
        s_standbyheros.AddRange(m_transform.Find("Lobby").GetComponentsInChildren<Hero>());
        s_onstageheros = new Hero[6];
        for (int i = 0; i < temp.Count; i++)
            s_onstageheros[i] = temp[i];

        return temp.Count;
    }

    public void GameStart()
    {
        GameObject lobbyout = new GameObject("LobbyOut");
        GameObject onstage = new GameObject("Onstage");
        onstage.transform.parent = lobbyout.transform;
        GameObject standby = new GameObject("Standby");
        standby.transform.parent = lobbyout.transform;
        int idx = ReloadSquad();

        for (int i = 0; i < idx; i++)
        {
            s_onstageheros[i].transform.parent = onstage.transform;
        }
        foreach(var x in s_standbyheros)
        {
            x.transform.parent = standby.transform;
        }

        DontDestroyOnLoad(lobbyout);
        SceneMng.Instance.Enable(SceneType.Ingame,true,1f);

    }

    public static Hero FindHero(int id)
    {
        foreach(var x in s_heros)
        {
            if (x.HeroCardID == id)
                return x; 
        }
        return null;
    }
}
