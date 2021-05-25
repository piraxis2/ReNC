using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMng : MonoBehaviour
{
    public static int s_seceneloadcount = 0;
    List<Mng> m_mngs = new List<Mng>();
    private void Awake()
    {
        if (!GameInit.Load)
        {
            GameInit.LoadTable();
            GameInit.RegisterScene();
            GameInit.Load = true;
        }
        m_mngs.AddRange(GetComponentsInChildren<Mng>(true));
        foreach (var x in m_mngs)
        {
            x.Init();
        }

        s_seceneloadcount++;
        HandMng hand = HandMng.Instance;
        PlayerData playerData = PlayerData.Instance;
        MulliganMng mulligan = MulliganMng.instance;
        TurnMng turn = TurnMng.Instance;
        PlayerINFO playerINFO = PlayerINFO.Instatnce;
        GetComponentInChildren<SkillContainer>(true).init();

    }
}
