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



    private List<List<PixelFx>> m_FXs = new List<List<PixelFx>>();
    private List<GameObject> m_prefabs = new List<GameObject>();
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
        m_prefabs.Add(Resources.Load("Prefab/FX/Wind") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Bleeding") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Stun") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Silence") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/BlackSkull") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/BlackBall") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/ChainLightning") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/PolyMorphBomb") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/PolyMorpFx") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/MushroomCloud") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Dynamite") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/TNT") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/ProtectProjectile") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Spike") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/HealingWave") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/BombMine") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Knives") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Incinerate") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Fist") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/Kamehameha") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/WhtieSkull") as GameObject);
        m_prefabs.Add(Resources.Load("Prefab/FX/SilverBullet") as GameObject);


        for (int i = 0; i < m_prefabs.Count; i++)
        {
            m_FXs.Add(new List<PixelFx>());
            m_FXs[i].AddRange(s_fxmng.transform.GetChild(i).GetComponentsInChildren<PixelFx>(true));
            ListIndexer(m_FXs[i]);
        }

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
            case "Wind":
                return FxInstant(15);
            case "Bleeding":
                return FxInstant(16);
            case "Stun":
                return FxInstant(17);
            case "Silence":
                return FxInstant(18);
            case "BlackSkull":
                return FxInstant(19);
            case "BlackBall":
                return FxInstant(20);
            case "ChainLightning":
                return FxInstant(21);
            case "PolyMorphBomb":
                return FxInstant(22);
            case "PolyMorphFx":
                return FxInstant(23);
            case "MushroomCloud":
                return FxInstant(24);
            case "Dynamite":
                return FxInstant(25);
            case "TNT":
                return FxInstant(26);
            case "ProtectProjectile":
                return FxInstant(27);
            case "Spike":
                return FxInstant(28);
            case "HealingWave":
                return FxInstant(29);
            case "BombMine":
                return FxInstant(30);
            case "Knives":
                return FxInstant(31);
            case "Incinerate":
                return FxInstant(32);
            case "Fist":
                return FxInstant(33);
            case "Kamehameha":
                return FxInstant(34);
            case "WhiteSkull":
                return FxInstant(35);
            case "SilverBullet":
                return FxInstant(36);
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

            x.Init();
            return x;
        }
        PixelFx fx = Instantiate(m_prefabs[index], s_fxmng.transform.GetChild(index).transform).GetComponent<PixelFx>();
        fx.Init();
        fx.SetID(pixels.Count);
        return fx;
    }
}
