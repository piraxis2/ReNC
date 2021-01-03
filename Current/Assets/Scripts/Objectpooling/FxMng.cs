using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxMng : MonoBehaviour
{
    static FxMng s_fxmng = new FxMng();

    public static FxMng Instance
    {
        get
        {
            if (s_fxmng == null)
            {
                s_fxmng = GameObject.Find("Main/"+ s_fxmng.GetType().Name).GetComponent<FxMng>();
                
                s_fxmng.Init();
            }
            return s_fxmng;

        }
    }



    private List<List<PixelFx>> m_FXs= new List<List<PixelFx>>();
    private List<GameObject> m_prefabs= new List<GameObject>();
    //_Thunders
    //_Hits
    //_Arrows
    //_FireBalls
    //_ArrowHits 
    //_FireBallHits
    //_Shot(penetrate)
    //_Summon

    public void Init()
    {
        m_prefabs.Add(Resources.Load("Prefab/FX/Thunder") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Attack") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Arrow") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/FireBall") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/ArrowHit") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/boom") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Shot2") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Summon") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/ManaGet") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Heal") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Buff") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Rain5") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/FireStrike") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Sniping") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Bite") as GameObject);

        for (int i = 0; i < m_prefabs.Count; i++)
        {
            m_FXs.Add(new List<PixelFx>());
            m_FXs[i].AddRange(s_fxmng.transform.GetChild(i).GetComponentsInChildren<PixelFx>(true));
            ListIndexer(m_FXs[i]);
        }
        GetComponent<SkillContainer>().init();

    }

    void ListIndexer(List<PixelFx> fxlist)
    {
        int index = 0;
        foreach (var x in fxlist)
        {
            x.SetID(index);
            x.SetCount(fxlist.Count);
            index++;
        }

    }

    public PixelFx FxCall(string fxname)
    {
        switch (fxname)
        {
            case "Thunder":
                return FxInstant(0);
            case "Hit":
                return FxInstant(1);
            case "Arrow":
                return FxInstant(2);
            case "FireBall":
                return FxInstant(3);
            case "ArrowHit":
                return FxInstant(4);
            case "Boom":
                return FxInstant(5);
            case "Shot":
                return FxInstant(6);
            case "Summon":
                return FxInstant(7);
            case "ManaGet":
                return FxInstant(8);
            case "Heal":
                return FxInstant(9);
            case "Buff":
                return FxInstant(10);
            case "Rain5":
                return FxInstant(11);
            case "FireStrike":
                return FxInstant(12);
            case "Sniping":
                return FxInstant(13);
            case "Bite":
                return FxInstant(14);
                

        }
        return null;
    }

    private PixelFx FxInstant(int index)
    {
        List<PixelFx> pixels = m_FXs[index];
        foreach (var x in pixels)
        {
            if (x.GetActive())
                continue;

            return x;
        }
        PixelFx fx = Instantiate(m_prefabs[index], s_fxmng.transform.GetChild(index).transform).GetComponent<PixelFx>();
        fx.SetID(pixels.Count);
        return fx;
    }

}
