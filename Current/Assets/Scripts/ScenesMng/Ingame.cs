using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : Scene
{

    Transform m_lobbyout;
    public override void Enter()
    {
        m_lobbyout = GameObject.Find("LobbyOut/Onstage").transform;
        List<Hero> heros = new List<Hero>();
        heros.AddRange(m_lobbyout.GetComponentsInChildren<Hero>(true));
        int num = heros.Count;
        Transform obj = CharMng.Instance.transform.Find("Heros");
        for (int i = 0;i<num;i++)
        {
            heros[i].transform.SetParent(obj);
            CharMng.Instance.AddHero(heros[i]);
        }

        CharMng.Instance.InvisibleCharters(0);
        SummonerSkillMng s = SummonerSkillMng.Instance;

        LoadingMng.Instance.Fade(true);
    }

    public override void Exit()
    {
        foreach(var x in CharMng.Instance.CurrHeros)
        {
            x.transform.parent = m_lobbyout.parent.Find("Standby");
        }
    }

    public void test12()
    {
        CharMng.Instance.CharSummon();
        CanvasMng.Instance.LoadStateBar();
    }

    public override void Init()
    {
        AddChannel(Channel.C1, SceneType.Lobby);
        AddChannel(Channel.C2, SceneType.Title);
    }

    public override void Progress(float delta)
    {
        if (delta < 1)
            LoadingMng.Instance.Loading(true);
        else
            LoadingMng.Instance.Loading(false);
    }
}
